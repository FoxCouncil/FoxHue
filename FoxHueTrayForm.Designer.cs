namespace FoxHue
{
    partial class FoxHueTrayForm
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelBridgeName = new System.Windows.Forms.Label();
            this.flowLayoutPanelDevices = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.Black;
            this.buttonClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.ForeColor = System.Drawing.Color.White;
            this.buttonClose.Location = new System.Drawing.Point(253, 8);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(23, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "❌";
            this.buttonClose.UseVisualStyleBackColor = false;
            // 
            // labelBridgeName
            // 
            this.labelBridgeName.AutoSize = true;
            this.labelBridgeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBridgeName.Location = new System.Drawing.Point(8, 11);
            this.labelBridgeName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBridgeName.Name = "labelBridgeName";
            this.labelBridgeName.Size = new System.Drawing.Size(70, 20);
            this.labelBridgeName.TabIndex = 1;
            this.labelBridgeName.Text = "WOOOF";
            // 
            // flowLayoutPanelDevices
            // 
            this.flowLayoutPanelDevices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.flowLayoutPanelDevices.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelDevices.Location = new System.Drawing.Point(8, 40);
            this.flowLayoutPanelDevices.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanelDevices.Name = "flowLayoutPanelDevices";
            this.flowLayoutPanelDevices.Size = new System.Drawing.Size(268, 213);
            this.flowLayoutPanelDevices.TabIndex = 2;
            // 
            // buttonSettings
            // 
            this.buttonSettings.BackColor = System.Drawing.Color.Black;
            this.buttonSettings.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.buttonSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSettings.ForeColor = System.Drawing.Color.White;
            this.buttonSettings.Location = new System.Drawing.Point(222, 8);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(23, 23);
            this.buttonSettings.TabIndex = 3;
            this.buttonSettings.Text = "⚙";
            this.buttonSettings.UseVisualStyleBackColor = false;
            // 
            // FoxHueTrayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.flowLayoutPanelDevices);
            this.Controls.Add(this.labelBridgeName);
            this.Controls.Add(this.buttonClose);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FoxHueTrayForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelBridgeName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDevices;
        private System.Windows.Forms.Button buttonSettings;
    }
}