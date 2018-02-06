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
    public partial class FoxHueControl : Form
    {
        private readonly FoxHueContext _context;

        public FoxHueControl(FoxHueContext context)
        {
            _context = context;

            InitializeComponent();

            TopMost = true;

            LostFocus += HandleHideForm;
            Deactivate += HandleHideForm;

            buttonClose.Click += HandleHideForm;

            Shown += (sender, args) =>
            {

            };
        }

        private void HandleHideForm(object sender, EventArgs args)
        {
            Hide();
        }
    }
}
