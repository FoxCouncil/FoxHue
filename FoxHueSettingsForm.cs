// Copyright (c) 2018 Fox Council - License: MIT - https://github.com/FoxCouncil/FoxHue

using Humanizer;
using Q42.HueApi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoxHue
{
    public partial class FoxHueSettingsForm : Form
    {
        private readonly FoxHueContext _context;
        private IList<WhiteList> _whiteList;

        public FoxHueSettingsForm(FoxHueContext context)
        {
            _context = context;

            InitializeComponent();

            // labelBridgeName.Text = $"{_context.HueBridgeCurrent.Config.Name} ({_context.HueBridgeCurrent.Config.IpAddress})";

            // Whitelist Events
            Shown += (s, e) => RefreshWhitelist();
            buttonWhitelistRefresh.Click += (s, e) => RefreshWhitelist();
            buttonWhitelistDelete.Click += (s, e) => DeleteWhitelistEntries();
            checkedListBoxWhitelist.SelectedIndexChanged += (s, e) =>
            {
                var totalCheked = checkedListBoxWhitelist.CheckedIndices.Count;

                if (totalCheked > 0)
                {
                    buttonWhitelistDelete.Enabled = true;
                    buttonWhitelistDelete.Text = $"Delete {totalCheked} Item(s)";
                }
                else
                {
                    buttonWhitelistDelete.Enabled = false;
                    buttonWhitelistDelete.Text = "Delete";
                }
                
            };

            buttonClose.Click += (s, e) => Hide();
        }

        private void RefreshWhitelist()
        {
            buttonWhitelistDelete.Enabled = buttonWhitelistRefresh.Enabled = checkedListBoxWhitelist.Enabled = false;
            buttonWhitelistDelete.Text = "Delete";

            Task.WaitAll(DoRefreshWhitelist());

            checkedListBoxWhitelist.Items.Clear();

            var maxLength = 0;

            if (_whiteList == null)
            {
                return;
            }

            foreach (var whitelist in _whiteList)
            {
                maxLength = Math.Max(maxLength, whitelist.Name.Length);
            }

            foreach (var whitelist in _whiteList)
            {
                checkedListBoxWhitelist.Items.Add($"{whitelist.Name.PadRight(maxLength + 1)} - {DateTime.Parse(whitelist.LastUsedDate).Humanize()}");
            }

            buttonWhitelistRefresh.Enabled = checkedListBoxWhitelist.Enabled = true;
        }

        private void DeleteWhitelistEntries()
        {
            var selectedItems = checkedListBoxWhitelist.CheckedIndices;
            var usernameList = new List<string>();

            foreach (int item in selectedItems)
            {
                usernameList.Add(_whiteList[item].Id);
            }

            if (MessageBox.Show($"Are you sure you want to delete:\n{string.Join("\n", checkedListBoxWhitelist.CheckedItems.Cast<string>())}", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly, false) == DialogResult.Yes)
            {
                Task.WaitAll(DoDeleteWhitelist(usernameList));

                RefreshWhitelist();
            }
        }

        private async Task DoDeleteWhitelist(IList<string> whitelistEntries)
        {
            foreach (var entry in whitelistEntries)
            {
                //var success = await _context.HueClientCurrent.DeleteWhiteListEntryAsync(entry).ConfigureAwait(false);
            }
        }

        private async Task DoRefreshWhitelist()
        {
            //var newWhitelist = await _context.HueClientCurrent.GetWhiteListAsync().ConfigureAwait(false);
            //_whiteList = newWhitelist.ToList();
        }
    }
}
