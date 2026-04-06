namespace evidenca_krav.NavigationBar
{
    partial class Telice
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDodajTelico = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(260, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 55);
            this.label1.TabIndex = 1;
            this.label1.Text = "Telice";
            // 
            // buttonDodajTelico
            // 
            this.buttonDodajTelico.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDodajTelico.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDodajTelico.Location = new System.Drawing.Point(524, 72);
            this.buttonDodajTelico.Name = "buttonDodajTelico";
            this.buttonDodajTelico.Size = new System.Drawing.Size(141, 69);
            this.buttonDodajTelico.TabIndex = 2;
            this.buttonDodajTelico.Text = "Dodaj telico";
            this.buttonDodajTelico.UseVisualStyleBackColor = true;
            this.buttonDodajTelico.Click += new System.EventHandler(this.buttonDodajTelico_Click);
            // 
            // Telice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDodajTelico);
            this.Controls.Add(this.label1);
            this.Name = "Telice";
            this.Size = new System.Drawing.Size(700, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDodajTelico;
    }
}
