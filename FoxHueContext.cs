using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoxHue.Properties;
using Newtonsoft.Json;
using Q42.HueApi;
using Q42.HueApi.Interfaces;

namespace FoxHue
{
    public class FoxHueContext : ApplicationContext
    {
        private const string KEYS_DB_FILENAME = "k.dat";

        // Win32 UI
        private NotifyIcon _trayIcon;

        // Hue Bridge
        private IPAddress _hueBridgeCurrent = null;
        private readonly List<IPAddress> _hueBridgeAddresses = new List<IPAddress>();
        private readonly Dictionary<IPAddress, ILocalHueClient> _hueClients = new Dictionary<IPAddress, ILocalHueClient>();
        private readonly Dictionary<string, string> _hueClientKeys = new Dictionary<string, string>();
        private readonly Dictionary<IPAddress, Bridge> _hueBridges = new Dictionary<IPAddress, Bridge>();

        // Lights
        private readonly List<Light> _hueLights = new List<Light>();

        public FoxHueContext()
        {
            if (File.Exists(KEYS_DB_FILENAME))
            {
                var rawFile = File.ReadAllText(KEYS_DB_FILENAME);
                _hueClientKeys = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawFile);
            }

            Task.WaitAll(HueBridgeFind());
            Task.WaitAll(HueBridgeConnect());
            Task.WaitAll(HueGetLights());

            TrayIconCreate();
        }

        private ILocalHueClient HueClientCurrent => _hueClients[_hueBridgeCurrent];

        private Bridge HueBridgeCurrent => _hueBridges[_hueBridgeCurrent];

        private async Task HueBridgeFind()
        {
            IBridgeLocator locator = new HttpBridgeLocator();

            var bridgeIPs = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(2));

            foreach (var ipAddress in bridgeIPs)
            {
                _hueBridgeAddresses.Add(IPAddress.Parse(ipAddress.IpAddress));
            }
        }

        private async Task HueBridgeConnect()
        {
            foreach (var hueBridgeIp in _hueBridgeAddresses)
            {
                var hueBridgeIpStr = hueBridgeIp.ToString();

                if (!_hueClients.ContainsKey(hueBridgeIp))
                {
                    _hueClients.Add(hueBridgeIp, new LocalHueClient(hueBridgeIpStr));
                }

                var hueClient = _hueClients[hueBridgeIp];

                if (!_hueClientKeys.ContainsKey(hueBridgeIpStr))
                {
                    _hueClientKeys.Add(hueBridgeIpStr, await hueClient.RegisterAsync("FoxHue", Environment.MachineName));
                }
                else
                {
                    hueClient.Initialize(_hueClientKeys[hueBridgeIpStr]);
                }

                if (!_hueBridges.ContainsKey(hueBridgeIp))
                {
                    _hueBridges.Add(hueBridgeIp, await hueClient.GetBridgeAsync());
                }

                _hueBridgeCurrent = hueBridgeIp;
            }

            File.WriteAllText(KEYS_DB_FILENAME, JsonConvert.SerializeObject(_hueClientKeys, Formatting.Indented));
        }

        private async Task HueGetLights()
        {
            _hueLights.Clear();
            _hueLights.AddRange(await HueClientCurrent.GetLightsAsync());
        }

        private void ContextMenuGenerate()
        {
            if (_trayIcon == null)
            {
                return;
            }

            var newContextMenu = new ContextMenu();

            newContextMenu.MenuItems.Add($"{HueBridgeCurrent.Config.Name} - {HueBridgeCurrent.Config.SoftwareVersion} - {HueBridgeCurrent.Config.IpAddress}");

            newContextMenu.MenuItems.Add("-");

            // Lights
            foreach (var light in _hueLights)
            {
                var lightMenuItem = new MenuItem
                {
                    Text = $"{light.Name}\t\t{(light.State.On ? "ON" : "OFF")}",
                    Tag = light
                };

                lightMenuItem.Click += LightButtonClient;

                newContextMenu.MenuItems.Add(lightMenuItem);
            }

            newContextMenu.MenuItems.Add("-");

            newContextMenu.MenuItems.Add(new MenuItem("&Exit", Exit));

            _trayIcon.ContextMenu = newContextMenu;
        }

        private void LightButtonClient(object sender, EventArgs eventArgs)
        {
            if (!(sender is MenuItem menuItem))
            {
                return;
            }

            var light = (Light)menuItem.Tag;

            HueClientCurrent.SendCommandAsync(new LightCommand { On = !light.State.On }, new[] { light.Id }).Wait();

            HueGetLights().Wait();

            ContextMenuGenerate();
        }

        private void TrayIconCreate()
        {
            // Initialize Tray Icon
            _trayIcon = new NotifyIcon { Icon = Resources.FoxHueIcon };

            _trayIcon.MouseClick += (sender, args) =>
            {
                ContextMenuGenerate();

                if (args.Button != MouseButtons.Left)
                {
                    return;
                }

                var mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);

                mi.Invoke(_trayIcon, null);
            };

            ContextMenuGenerate();

            _trayIcon.Visible = true;
        }

        private void TrayIconHide()
        {
            _trayIcon.Visible = false;
        }

        private void Exit(object sender, EventArgs e)
        {
            TrayIconHide();

            Application.Exit();
        }
    }
}
