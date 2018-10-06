using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoxHue
{
    public partial class FoxHueTrayForm : Form
    {
        private readonly FoxHueContext _context;

        private readonly Timer _visibilityTimer = new Timer { Interval = 100 };

        public FoxHueTrayForm(FoxHueContext context)
        {
            _context = context;

            InitializeComponent();

            InitializeUi();

            TopMost = true;

            buttonClose.Click += HandleHideForm;
            buttonSettings.MouseUp += buttonSettings_MouseUp;

            _visibilityTimer.Tick += (sender, args) =>
            {
                if (ActiveForm == this)
                {
                    return;
                }

                HandleHideForm(sender, args);

                _visibilityTimer.Stop();
            };

            Activated += (sender, args) =>
            {
                _visibilityTimer.Start();
            };
        }

        private void InitializeUi()
        {
            labelBridgeName.Text = _context.HueBridgeCurrent.Config.Name;

            MakeRounded();

            LoadLights();
        }

        private void MakeRounded()
        {
            var bounds = new Rectangle(0, 0, Width, Height);

            const int cornerRadius = 20;

            var path = new System.Drawing.Drawing2D.GraphicsPath();

            path.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270, 90);
            path.AddLine(bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X + bounds.Width, bounds.Y + bounds.Height);
            path.AddLine(bounds.X, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);

            path.CloseAllFigures();

            Region = new Region(path);
        }

        private void LoadLights()
        {
            flowLayoutPanelDevices.Controls.Clear();

            // Lights
            foreach (var light in _context.HueLights)
            {
                var lightMenuItem = new FoxHueDeviceControl(_context, light)
                {
                    Width = flowLayoutPanelDevices.ClientSize.Width - 4
                };

                flowLayoutPanelDevices.Controls.Add(lightMenuItem);
            }
        }

        private void HandleHideForm(object sender, EventArgs args)
        {
            Hide();
        }

        private void buttonSettings_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_context.SettingsForm.Visible)
            {
                _context.ContextMenu.Show(buttonSettings, Point.Empty);
            }
        }
    }
}
