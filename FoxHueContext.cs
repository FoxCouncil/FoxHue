// Copyright (c) 2018 Fox Council - License: MIT - https://github.com/FoxCouncil/FoxHue

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
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
        // State Management
        public FoxHueAppState CurrentState;

        // Win32 UI
        public FoxHueTrayForm TrayForm;
        public FoxHueStatusForm StatusForm;
        public FoxHueSettingsForm SettingsForm;
        public ContextMenu ContextMenu;
        
        // Lights
        public readonly List<Light> HueLights = new List<Light>();

        private const string KeysDbFilename = "k.dat";
        private NotifyIcon _trayIcon;

        // Hue Bridge
        private readonly Dictionary<IPAddress, ILocalHueClient> _hueClients = new Dictionary<IPAddress, ILocalHueClient>();
        private Dictionary<string, string> _hueClientKeys = new Dictionary<string, string>();

        //private readonly Dictionary<IPAddress, Bridge> _hueBridges = new Dictionary<IPAddress, Bridge>();
        //private IPAddress _hueBridgeCurrent = null;

        private bool _statusWindowTriggered = false;

        public FoxHueContext()
        {
            ControlsCreate();

            HookEvents();

            Task.Run(async () => await Initialize()).GetAwaiter().GetResult();
        }

        private void ControlsCreate()
        {
            // Forms
            TrayForm = new FoxHueTrayForm(this);
            StatusForm = new FoxHueStatusForm(this);
            SettingsForm = new FoxHueSettingsForm(this);

            // Initialize Tray Icon
            _trayIcon = new NotifyIcon { Icon = Resources.FoxHueIcon };

            ContextMenu = new ContextMenu
            {
                MenuItems = {
                    new MenuItem("&About", (sender, args) => MessageBox.Show("💡 Control Zee Lights! 💡\n\nLicense: MIT\n\nRepo: https://github.com/FoxCouncil/FoxHue", "About FoxHue")),
                    new MenuItem("&Settings", (sender, args) => SettingsForm.Show(TrayForm)),
                    new MenuItem("-"),
                    new MenuItem("&Exit", (sender, args) => Application.Exit()),
                }
            };

            _trayIcon.ContextMenu = ContextMenu;

            _trayIcon.Visible = true;
        }

        private void HookEvents()
        {
            Application.Idle += (sender, args) =>
            {
                if (_statusWindowTriggered || CurrentState == FoxHueAppState.Running)
                {
                    return;
                }

                _statusWindowTriggered = true;

                StatusForm.Show();
            };

            _trayIcon.MouseUp += (sender, args) =>
            {
                if (CurrentState != FoxHueAppState.Running)
                {
                    if (StatusForm.Visible)
                    {
                        StatusForm.Hide();
                    }
                    else
                    {
                        StatusForm.Show();
                    }

                    return;
                }

                if (args.Button != MouseButtons.Left || TrayForm.Visible)
                {
                    return;
                }

                if (TrayForm.Location == Point.Empty)
                {
                    var pointToClient = TrayForm.PointToClient(Cursor.Position);
                    TrayForm.Location = new Point(pointToClient.X - TrayForm.Width / 2, Screen.PrimaryScreen.Bounds.Height - TrayForm.Height - (Screen.PrimaryScreen.Bounds.Bottom - Screen.PrimaryScreen.WorkingArea.Bottom));
                }

                HueGetLights().ConfigureAwait(false);

                TrayForm.Show();
                TrayForm.BringToFront();
                TrayForm.Activate();
            };

            StatusForm.ButtonClickEvent += () =>
            {
                if (CurrentState == FoxHueAppState.Authorizing)
                {
                    MessageBox.Show("Holy Shit!");
                }
            };
        }

        public async Task Initialize()
        {
            CurrentState = FoxHueAppState.Loading;

            SecureDataLoad();

            await HueBridgeFind();

            if (_hueClients.Count == 0)
            {
                SetState(FoxHueAppState.NoHub);
            }
            else if (_hueClientKeys.Count == 0)
            {
                SetState(FoxHueAppState.Authorizing);
            }
            else
            {
                var success = false;

                foreach (var clientKey in _hueClientKeys)
                {
                    var parsedIpAddress = IPAddress.Parse(clientKey.Key);

                    if (!_hueClients.ContainsKey(parsedIpAddress))
                    {
                        continue;
                    }

                    _hueClients[parsedIpAddress].Initialize(clientKey.Value);

                    success = true;
                }

                SetState(success ? FoxHueAppState.Running : FoxHueAppState.Authorizing);
            }

            // DO CHECK HERE
            // _hueClientKeys.Add(hueBridgeIpStr, await hueClient.RegisterAsync("FoxHue", Environment.MachineName));

            //await HueGetLights();
        }

        private void SetState(FoxHueAppState state)
        {
            CurrentState = state;

            TrayForm.InvokeIfRequired(() =>
            {
                switch (state)
                {
                    case FoxHueAppState.Loading:
                    {
                        ContextMenu.MenuItems[1].Enabled = false;

                        StatusForm.ButtonText = string.Empty;
                        StatusForm.ButtonVisible = false;
                        StatusForm.StatusText = "Loading Fox Hue";

                        return;
                    }

                    case FoxHueAppState.NoHub:
                    {
                        ContextMenu.MenuItems[1].Enabled = false;

                        StatusForm.ButtonText = "Search Again";
                        StatusForm.ButtonVisible = true;
                        StatusForm.StatusText = "No Philips Hue Bridge Found\n\nCheck PC and Bridge are on the same network!";

                        return;
                    }

                    case FoxHueAppState.Authorizing:
                    {
                        ContextMenu.MenuItems[1].Enabled = false;

                        StatusForm.ButtonText = "Authorize";
                        StatusForm.ButtonVisible = true;
                        StatusForm.StatusText = "Not Authenticated\n\nPress center button on Hue Bridge and click button bellow\n\n\n[button]";

                        return;
                    }

                    case FoxHueAppState.Running:
                    {
                        ContextMenu.MenuItems[1].Enabled = true;

                        return;
                    }

                    default:
                        throw new ArgumentOutOfRangeException(nameof(state), state, null);
                }
            });
        }

        private void SecureDataLoad()
        {
            if (!File.Exists(KeysDbFilename))
            {
                return;
            }

            var rawFile = File.ReadAllText(KeysDbFilename);
            _hueClientKeys = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawFile.Unprotect());
        }

        private void SecureDataSave()
        {
            var rawEncryptedJson = JsonConvert.SerializeObject(_hueClientKeys).Protect();
            File.WriteAllText(KeysDbFilename, rawEncryptedJson);
        }

        //public ILocalHueClient HueClientCurrent => _hueClients[_hueBridgeCurrent];

        //public Bridge HueBridgeCurrent => _hueBridges[_hueBridgeCurrent];

        private async Task HueBridgeFind()
        {
            IBridgeLocator locator = new HttpBridgeLocator();

            var bridgeIPs = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(2));

            foreach (var ipAddress in bridgeIPs)
            {
                var ipAddressParsed = IPAddress.Parse(ipAddress.IpAddress);

                if (!_hueClients.ContainsKey(ipAddressParsed))
                {
                    _hueClients.Add(ipAddressParsed, new LocalHueClient(ipAddress.IpAddress));

                    if (_hueClientKeys.ContainsKey(ipAddress.IpAddress))
                    {
                        _hueClients[ipAddressParsed].Initialize(_hueClientKeys[ipAddress.IpAddress]);
                    }
                }
            }
        }

        public async Task HueGetLights()
        {
            //var lights = await HueClientCurrent.GetLightsAsync().ConfigureAwait(false);

            //HueLights.Clear();

            //HueLights.AddRange(lights);
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
