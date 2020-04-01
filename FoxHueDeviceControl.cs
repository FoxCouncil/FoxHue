// Copyright (c) 2018 Fox Council - License: MIT - https://github.com/FoxCouncil/FoxHue

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
using Q42.HueApi.ColorConverters.Original;

namespace FoxHue
{
    /// <inheritdoc />
    /// <summary>A C# WinForms control to control a Philips Hue IoT product.</summary>
    public partial class FoxHueDeviceControl : UserControl
    {
        // Used for hover effect
        private const int INTENSITY_MINIMUM = 16;
        private const int INTENSITY_MAXIMUM = 64;

        // The main system context, useful for calling back to the API
        private readonly FoxHueContext _context;

        // The 'thing' we are controlling, though a light for now.
        private Light _light;

        // UI state tracking booleans
        private bool _isMousedOver;
        private bool _isSettingDimmerValue;

        /// <summary>Utility property to calculate the RedColor each time.</summary>
        private int RedColor => _light.State.On ? 0 : INTENSITY_MINIMUM;

        /// <summary>Utility property to calculate the GreenColor each time.</summary>
        private int GreenColor => _light.State.On ? INTENSITY_MINIMUM : 0;

        /// <summary>The Id of this device</summary>
        public string Id { get; }

        /// <summary>The Id (as <c>int</c>) of this device</summary>
        public int IdInt { get; }

        public bool HasColor { get; }

        public bool HasColorTemperature { get; }

        /// <summary>The Fox Hue (<c>FoxHueDeviceControl</c>) custom control constructor</summary>
        /// <param name="context">The <c>FoxHueContext</c> instance for this control</param>
        /// <param name="light">The <c>HueThing</c>, this control's device</param>
        public FoxHueDeviceControl(FoxHueContext context, Light light)
        {
            _context = context;
            _light = light;

            Id = _light.Id;
            IdInt = int.Parse(Id);

            HasColor = _light.State.ColorMode != null && _light.State.ColorMode != "ct";
            HasColorTemperature = _light.State.ColorMode != null && _light.State.ColorMode == "ct";

            InitializeComponent();
            InitializeEvents();

            HandleMouseLeave(null, new EventArgs());
        }

        /// <summary>Initialize all of this controls events</summary>
        private void InitializeEvents()
        {
            foreach (Control childControl in Controls)
            {
                childControl.MouseEnter += HandleMouseEnter;
                childControl.MouseLeave += HandleMouseLeave;

                if (childControl != trackBarDimmer && childControl != buttonColor)
                {
                    childControl.Click += HandleToggleOn;
                }
            }

            MouseEnter += HandleMouseEnter;
            MouseLeave += HandleMouseLeave;
            Click += HandleToggleOn;

            trackBarDimmer.ValueChanged += HandleBrightness;

            buttonColor.Click += (sender, args) =>
            {
                var newControl = new Control
                {
                    Width = 100,
                    Height = 100,
                    BackColor = Color.Red,
                    ForeColor = Color.White
                };

                newControl.Controls.Add(new Label { Text = "Gayyy" });

                _context.TrayForm.Controls.Add(newControl);

            };
        }

        /// <summary>Perform and handle <c>TrackBar</c> events to control "Brightness" of the device</summary>
        /// <param name="sender">The event creator</param>
        /// <param name="e">The events default <c>EventArgs</c> object</param>
        private void HandleBrightness(object sender, EventArgs e)
        {
            if (!_isSettingDimmerValue)
            {
                //_context.HueClientCurrent.SendCommandAsync(new LightCommand { Brightness = (byte?)trackBarDimmer.Value }, new[] { _light.Id }).ConfigureAwait(false);
            }
        }

        /// <summary>Perform and handle <c>Control</c>(s) click event to control "On" of the device</summary>
        /// <param name="sender">The event creator</param>
        /// <param name="e">The events default <c>EventArgs</c> object</param>
        private void HandleToggleOn(object sender, EventArgs e)
        {
            //_context.HueClientCurrent.SendCommandAsync(new LightCommand { On = !_light.State.On }, new[] { _light.Id }).ConfigureAwait(false);

            UpdateState();
        }

        /// <summary>Perform and handle the "mouse over" event</summary>
        /// <param name="sender">The event creator</param>
        /// <param name="e">The events default <c>EventArgs</c> object</param>
        private void HandleMouseEnter(object sender, EventArgs e)
        {
            _isMousedOver = true;

            UpdateState();
        }

        /// <summary>Perform and handle the "mouse leave" event</summary>
        /// <param name="sender">The event creator</param>
        /// <param name="e">The events default <c>EventArgs</c> object</param>
        private void HandleMouseLeave(object sender, EventArgs e)
        {
            _isMousedOver = false;

            UpdateState();
        }

        /// <summary>Retrieve and handle the state.</summary>
        private async void UpdateState()
        {
            //_light = await _context.HueClientCurrent.GetLightAsync(_light.Id); // .ConfigureAwait(false);

            this.InvokeIfRequired(() =>
            {
                BackColor = _isMousedOver ?
                    Color.FromArgb(INTENSITY_MAXIMUM + RedColor, INTENSITY_MAXIMUM + GreenColor, INTENSITY_MAXIMUM) :
                    Color.FromArgb(INTENSITY_MINIMUM + RedColor, INTENSITY_MINIMUM + GreenColor, INTENSITY_MINIMUM);

                buttonColor.Visible = buttonColor.Enabled = _light.State.On && HasColor;

                labelDeviceName.Text = _light.Name;

                labelStateOn.Text = _light.State.On ? "ON" : "OFF";
                labelStateOn.Location = new Point(Width - labelStateOn.Width, labelStateOn.Location.Y);

                _isSettingDimmerValue = true;

                trackBarDimmer.Visible = _light.State.On;
                trackBarDimmer.Enabled = _light.State.On;
                trackBarDimmer.Value = _light.State.Brightness;
                trackBarDimmer.Location = new Point(Width - 130, trackBarDimmer.Location.Y);
                trackBarDimmer.Width = 75;

                _isSettingDimmerValue = false;
            });
        }
    }
}