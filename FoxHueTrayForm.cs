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

            LoadLights();
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
    }
}
