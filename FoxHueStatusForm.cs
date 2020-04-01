// Copyright (c) 2018 Fox Council - License: MIT - https://github.com/FoxCouncil/FoxHue

using System;
using System.Windows.Forms;

namespace FoxHue
{
    public partial class FoxHueStatusForm : Form
    {
        private readonly FoxHueContext _context;

        public string StatusText
        {
            get => label.Text;
            set => label.Text = value;
        }

        public string ButtonText
        {
            get => button.Text;
            set => button.Text = value;
        }

        public bool ButtonVisible
        {
            get => button.Visible;
            set => button.Visible = value;
        }

        public event Action ButtonClickEvent;

        public FoxHueStatusForm(FoxHueContext context)
        {
            _context = context;

            Closing += (sender, args) =>
            {
                args.Cancel = true;
                Hide();
            };

            InitializeComponent();

            button.Click += (sender, args) => ButtonClickEvent?.Invoke();
        }
    }
}
