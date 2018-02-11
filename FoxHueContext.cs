﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
        private FoxHueTrayForm _trayForm;
        private NotifyIcon _trayIcon;

        // Hue Bridge
        private IPAddress _hueBridgeCurrent = null;
        private readonly List<IPAddress> _hueBridgeAddresses = new List<IPAddress>();
        private readonly Dictionary<IPAddress, ILocalHueClient> _hueClients = new Dictionary<IPAddress, ILocalHueClient>();
        private readonly Dictionary<string, string> _hueClientKeys = new Dictionary<string, string>();
        private readonly Dictionary<IPAddress, Bridge> _hueBridges = new Dictionary<IPAddress, Bridge>();

        // Lights
        public readonly List<Light> HueLights = new List<Light>();

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

            ControlCreate();

            TrayIconCreate();
        }

        private void ControlCreate()
        {
            _trayForm = new FoxHueTrayForm(this);
        }

        public ILocalHueClient HueClientCurrent => _hueClients[_hueBridgeCurrent];

        public Bridge HueBridgeCurrent => _hueBridges[_hueBridgeCurrent];

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

        public async Task HueGetLights()
        {
            var lights = await HueClientCurrent.GetLightsAsync().ConfigureAwait(false);

            HueLights.Clear();

            HueLights.AddRange(lights);
        }

        private void TrayIconCreate()
        {
            // Initialize Tray Icon
            _trayIcon = new NotifyIcon { Icon = Resources.FoxHueIcon };

            _trayIcon.MouseUp += (sender, args) =>
            {
                if (args.Button != MouseButtons.Left || _trayForm.Visible)
                {
                    return;
                }

                if (_trayForm.Location == Point.Empty)
                {
                    var pointToClient = _trayForm.PointToClient(Cursor.Position);
                    _trayForm.Location = new Point(pointToClient.X - _trayForm.Width / 2, Screen.PrimaryScreen.Bounds.Height - _trayForm.Height - (Screen.PrimaryScreen.Bounds.Bottom - Screen.PrimaryScreen.WorkingArea.Bottom));
                }

                HueGetLights().ConfigureAwait(false);

                _trayForm.Show();
                _trayForm.BringToFront();
                _trayForm.Activate();
            };

            _trayIcon.ContextMenu = new ContextMenu
            {
                MenuItems = { new MenuItem("&Exit", (sender, args) => Application.Exit()) }
            };

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
