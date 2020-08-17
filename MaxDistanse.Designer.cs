namespace ED_Systems
{
    partial class FormMaxDistanse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaxDistanse));
            this.lblMaxDist = new System.Windows.Forms.Label();
            this.cbxMaxDist = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMaxDist
            // 
            this.lblMaxDist.AutoSize = true;
            this.lblMaxDist.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMaxDist.Location = new System.Drawing.Point(12, 9);
            this.lblMaxDist.Name = "lblMaxDist";
            this.lblMaxDist.Size = new System.Drawing.Size(136, 20);
            this.lblMaxDist.TabIndex = 0;
            this.lblMaxDist.Text = "Set max. diatance";
            // 
            // cbxMaxDist
            // 
            this.cbxMaxDist.DropDownHeight = 150;
            this.cbxMaxDist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMaxDist.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbxMaxDist.FormattingEnabled = true;
            this.cbxMaxDist.Location = new System.Drawing.Point(12, 32);
            this.cbxMaxDist.Name = "cbxMaxDist";
            this.cbxMaxDist.Size = new System.Drawing.Size(136, 28);
            this.cbxMaxDist.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(39, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMaxDistanse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 104);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbxMaxDist);
            this.Controls.Add(this.lblMaxDist);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(176, 142);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(176, 142);
            this.Name = "FormMaxDistanse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Max distance";
            this.Load += new System.EventHandler(this.FormMaxDistanse_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMaxDist;
        private System.Windows.Forms.ComboBox cbxMaxDist;
        private System.Windows.Forms.Button button1;
    }
}