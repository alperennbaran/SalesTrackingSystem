namespace SalesManagementSystem.Forms.Tools
{
    partial class FormAbout
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
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl17.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl17.Appearance.Options.UseFont = true;
            this.labelControl17.Appearance.Options.UseForeColor = true;
            this.labelControl17.Enabled = false;
            this.labelControl17.Location = new System.Drawing.Point(418, 46);
            this.labelControl17.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(80, 33);
            this.labelControl17.TabIndex = 0;
            this.labelControl17.Text = "About";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(232)))), ((int)(((byte)(189)))));
            this.panel4.Controls.Add(this.simpleButton1);
            this.panel4.Controls.Add(this.richTextBox1);
            this.panel4.Controls.Add(this.labelControl17);
            this.panel4.Enabled = false;
            this.panel4.Location = new System.Drawing.Point(3, 1);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(951, 771);
            this.panel4.TabIndex = 16;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(357, 563);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.simpleButton1.Size = new System.Drawing.Size(183, 46);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Update";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(54, 101);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(825, 398);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1800, 772);
            this.Controls.Add(this.panel4);
            this.Name = "FormAbout";
            this.Text = "FormAbout";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl17;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}