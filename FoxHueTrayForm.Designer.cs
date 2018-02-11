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
            this.buttonClose.Location = new System.Drawing.Point(380, 12);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(34, 35);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "❌";
            this.buttonClose.UseVisualStyleBackColor = false;
            // 
            // labelBridgeName
            // 
            this.labelBridgeName.AutoSize = true;
            this.labelBridgeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBridgeName.Location = new System.Drawing.Point(12, 17);
            this.labelBridgeName.Name = "labelBridgeName";
            this.labelBridgeName.Size = new System.Drawing.Size(107, 29);
            this.labelBridgeName.TabIndex = 1;
            this.labelBridgeName.Text = "WOOOF";
            // 
            // flowLayoutPanelDevices
            // 
            this.flowLayoutPanelDevices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.flowLayoutPanelDevices.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelDevices.Location = new System.Drawing.Point(12, 62);
            this.flowLayoutPanelDevices.Name = "flowLayoutPanelDevices";
            this.flowLayoutPanelDevices.Size = new System.Drawing.Size(402, 328);
            this.flowLayoutPanelDevices.TabIndex = 2;
            // 
            // FoxHueTrayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(426, 402);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanelDevices);
            this.Controls.Add(this.labelBridgeName);
            this.Controls.Add(this.buttonClose);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
    }
}