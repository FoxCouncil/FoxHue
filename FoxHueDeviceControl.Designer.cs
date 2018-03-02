namespace FoxHue
{
    partial class FoxHueDeviceControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelDeviceName = new System.Windows.Forms.Label();
            this.labelStateOn = new System.Windows.Forms.Label();
            this.trackBarDimmer = new System.Windows.Forms.TrackBar();
            this.buttonColor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDimmer)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDeviceName
            // 
            this.labelDeviceName.AutoSize = true;
            this.labelDeviceName.BackColor = System.Drawing.Color.Transparent;
            this.labelDeviceName.Location = new System.Drawing.Point(1, 10);
            this.labelDeviceName.Name = "labelDeviceName";
            this.labelDeviceName.Size = new System.Drawing.Size(132, 20);
            this.labelDeviceName.TabIndex = 2;
            this.labelDeviceName.Text = "labelDeviceName";
            // 
            // labelStateOn
            // 
            this.labelStateOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateOn.AutoSize = true;
            this.labelStateOn.BackColor = System.Drawing.Color.Transparent;
            this.labelStateOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStateOn.Location = new System.Drawing.Point(870, 3);
            this.labelStateOn.Name = "labelStateOn";
            this.labelStateOn.Size = new System.Drawing.Size(165, 29);
            this.labelStateOn.TabIndex = 3;
            this.labelStateOn.Text = "labelStateOn";
            this.labelStateOn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // trackBarDimmer
            // 
            this.trackBarDimmer.Location = new System.Drawing.Point(356, 3);
            this.trackBarDimmer.Maximum = 254;
            this.trackBarDimmer.Name = "trackBarDimmer";
            this.trackBarDimmer.Size = new System.Drawing.Size(261, 69);
            this.trackBarDimmer.TabIndex = 4;
            this.trackBarDimmer.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // buttonColor
            // 
            this.buttonColor.FlatAppearance.BorderSize = 0;
            this.buttonColor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.buttonColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColor.ForeColor = System.Drawing.Color.Transparent;
            this.buttonColor.Image = global::FoxHue.Properties.Resources.ColorPickerImage;
            this.buttonColor.Location = new System.Drawing.Point(275, 0);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(42, 42);
            this.buttonColor.TabIndex = 5;
            this.buttonColor.UseVisualStyleBackColor = true;
            // 
            // FoxHueDeviceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.buttonColor);
            this.Controls.Add(this.trackBarDimmer);
            this.Controls.Add(this.labelStateOn);
            this.Controls.Add(this.labelDeviceName);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "FoxHueDeviceControl";
            this.Size = new System.Drawing.Size(1035, 42);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDimmer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelDeviceName;
        public System.Windows.Forms.Label labelStateOn;
        private System.Windows.Forms.TrackBar trackBarDimmer;
        private System.Windows.Forms.Button buttonColor;
    }
}
