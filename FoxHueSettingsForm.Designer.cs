namespace FoxHue
{
    partial class FoxHueSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label labelWhitelist;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FoxHueSettingsForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.checkedListBoxWhitelist = new System.Windows.Forms.CheckedListBox();
            this.labelBridgeName = new System.Windows.Forms.Label();
            this.buttonWhitelistRefresh = new System.Windows.Forms.Button();
            this.buttonWhitelistDelete = new System.Windows.Forms.Button();
            labelWhitelist = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelWhitelist
            // 
            labelWhitelist.AutoSize = true;
            labelWhitelist.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelWhitelist.Location = new System.Drawing.Point(8, 6);
            labelWhitelist.Name = "labelWhitelist";
            labelWhitelist.Size = new System.Drawing.Size(185, 25);
            labelWhitelist.TabIndex = 2;
            labelWhitelist.Text = "Bridge Whitelist:";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(537, 286);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxWhitelist
            // 
            this.checkedListBoxWhitelist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxWhitelist.CheckOnClick = true;
            this.checkedListBoxWhitelist.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxWhitelist.FormattingEnabled = true;
            this.checkedListBoxWhitelist.Location = new System.Drawing.Point(13, 39);
            this.checkedListBoxWhitelist.Name = "checkedListBoxWhitelist";
            this.checkedListBoxWhitelist.Size = new System.Drawing.Size(599, 140);
            this.checkedListBoxWhitelist.TabIndex = 1;
            // 
            // labelBridgeName
            // 
            this.labelBridgeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBridgeName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBridgeName.Location = new System.Drawing.Point(12, 243);
            this.labelBridgeName.Name = "labelBridgeName";
            this.labelBridgeName.Size = new System.Drawing.Size(199, 67);
            this.labelBridgeName.TabIndex = 3;
            this.labelBridgeName.Text = "WOOOF 0w0 AWOOO";
            this.labelBridgeName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // buttonWhitelistRefresh
            // 
            this.buttonWhitelistRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWhitelistRefresh.Location = new System.Drawing.Point(537, 10);
            this.buttonWhitelistRefresh.Name = "buttonWhitelistRefresh";
            this.buttonWhitelistRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonWhitelistRefresh.TabIndex = 4;
            this.buttonWhitelistRefresh.Text = "&Refresh";
            this.buttonWhitelistRefresh.UseVisualStyleBackColor = true;
            // 
            // buttonWhitelistDelete
            // 
            this.buttonWhitelistDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWhitelistDelete.Enabled = false;
            this.buttonWhitelistDelete.Location = new System.Drawing.Point(474, 185);
            this.buttonWhitelistDelete.Name = "buttonWhitelistDelete";
            this.buttonWhitelistDelete.Size = new System.Drawing.Size(138, 23);
            this.buttonWhitelistDelete.TabIndex = 6;
            this.buttonWhitelistDelete.Text = "&Delete";
            this.buttonWhitelistDelete.UseVisualStyleBackColor = true;
            // 
            // FoxHueSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 321);
            this.ControlBox = false;
            this.Controls.Add(this.buttonWhitelistDelete);
            this.Controls.Add(this.buttonWhitelistRefresh);
            this.Controls.Add(this.labelBridgeName);
            this.Controls.Add(labelWhitelist);
            this.Controls.Add(this.checkedListBoxWhitelist);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FoxHueSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "⚙ FoxHue Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckedListBox checkedListBoxWhitelist;
        private System.Windows.Forms.Label labelBridgeName;
        private System.Windows.Forms.Button buttonWhitelistRefresh;
        private System.Windows.Forms.Button buttonWhitelistDelete;
    }
}