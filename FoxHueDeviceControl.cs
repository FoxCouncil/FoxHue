using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Q42.HueApi;

namespace FoxHue
{
    public partial class FoxHueDeviceControl : UserControl
    {
        private const int INTENSITY_MINIMUM = 16;
        private const int INTENSITY_MAXIMUM = 64;

        private readonly FoxHueContext _context;

        private Light _light;

        private bool _isMousedOver;
        private bool _isSettingDimmerValue;

        private int RedColor => _light.State.On ? 0 : INTENSITY_MINIMUM;

        private int GreenColor => _light.State.On ? INTENSITY_MINIMUM : 0;

        public FoxHueDeviceControl(FoxHueContext context, Light light)
        {
            _context = context;
            _light = light;

            InitializeComponent();
            InitializeEvents();

            HandleMouseLeave(null, new EventArgs());
        }

        private void InitializeEvents()
        {
            foreach (Control childControl in Controls)
            {
                childControl.MouseEnter += HandleMouseEnter;
                childControl.MouseLeave += HandleMouseLeave;

                if (childControl != trackBarDimmer)
                {
                    childControl.Click += HandleMouseClick;
                }
            }

            MouseEnter += HandleMouseEnter;
            MouseLeave += HandleMouseLeave;
            Click += HandleMouseClick;

            trackBarDimmer.ValueChanged += HandleDimmer;
        }

        private void HandleDimmer(object sender, EventArgs e)
        {
            if (!_isSettingDimmerValue)
            {
                _context.HueClientCurrent.SendCommandAsync(new LightCommand { Brightness = (byte?)trackBarDimmer.Value }, new[] { _light.Id }).ConfigureAwait(false);
            }
        }

        private void HandleMouseClick(object sender, EventArgs e)
        {
            _context.HueClientCurrent.SendCommandAsync(new LightCommand { On = !_light.State.On }, new[] { _light.Id }).ConfigureAwait(false);

            UpdateState();
        }

        private void HandleMouseEnter(object sender, EventArgs args)
        {
            _isMousedOver = true;

            UpdateState();
        }

        private void HandleMouseLeave(object sender, EventArgs args)
        {
            _isMousedOver = false;

            UpdateState();
        }

        private async void UpdateState()
        {
            _light = await _context.HueClientCurrent.GetLightAsync(_light.Id).ConfigureAwait(false);

            this.InvokeIfRequired(() =>
            {
                BackColor = _isMousedOver ?
                    Color.FromArgb(INTENSITY_MAXIMUM + RedColor, INTENSITY_MAXIMUM + GreenColor, INTENSITY_MAXIMUM) :
                    Color.FromArgb(INTENSITY_MINIMUM + RedColor, INTENSITY_MINIMUM + GreenColor, INTENSITY_MINIMUM);

                labelDeviceName.Text = _light.Name;

                labelStateOn.Text = _light.State.On ? "ON" : "OFF";
                labelStateOn.Location = new Point(Width - labelStateOn.Width, labelStateOn.Location.Y);

                _isSettingDimmerValue = true;

                trackBarDimmer.Enabled = _light.State.On;
                trackBarDimmer.Value = _light.State.Brightness;
                trackBarDimmer.Location = new Point(Width - 130, trackBarDimmer.Location.Y);
                trackBarDimmer.Width = 75;

                _isSettingDimmerValue = false;
            });
        }
    }
}