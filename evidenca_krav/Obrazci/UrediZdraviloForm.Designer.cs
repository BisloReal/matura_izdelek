namespace evidenca_krav.Obrazci
{
    partial class UrediZdraviloForm
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
            this.buttonPreklici = new System.Windows.Forms.Button();
            this.buttonPotrdi = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxZdravilo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonPreklici
            // 
            this.buttonPreklici.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPreklici.Location = new System.Drawing.Point(122, 206);
            this.buttonPreklici.Name = "buttonPreklici";
            this.buttonPreklici.Size = new System.Drawing.Size(100, 43);
            this.buttonPreklici.TabIndex = 53;
            this.buttonPreklici.Text = "Prekliči";
            this.buttonPreklici.UseVisualStyleBackColor = true;
            this.buttonPreklici.Click += new System.EventHandler(this.buttonPreklici_Click);
            // 
            // buttonPotrdi
            // 
            this.buttonPotrdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPotrdi.Location = new System.Drawing.Point(241, 206);
            this.buttonPotrdi.Name = "buttonPotrdi";
            this.buttonPotrdi.Size = new System.Drawing.Size(100, 43);
            this.buttonPotrdi.TabIndex = 52;
            this.buttonPotrdi.Text = "Shrani";
            this.buttonPotrdi.UseVisualStyleBackColor = true;
            this.buttonPotrdi.Click += new System.EventHandler(this.buttonPotrdi_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(190, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 25);
            this.label2.TabIndex = 45;
            this.label2.Text = "Zdravilo";
            // 
            // textBoxZdravilo
            // 
            this.textBoxZdravilo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxZdravilo.Location = new System.Drawing.Point(122, 144);
            this.textBoxZdravilo.Name = "textBoxZdravilo";
            this.textBoxZdravilo.Size = new System.Drawing.Size(219, 38);
            this.textBoxZdravilo.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(373, 63);
            this.label1.TabIndex = 43;
            this.label1.Text = "Uredi zdravilo";
            // 
            // UrediZdraviloForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 276);
            this.Controls.Add(this.buttonPreklici);
            this.Controls.Add(this.buttonPotrdi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxZdravilo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UrediZdraviloForm";
            this.Text = "UrediZdraviloForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPreklici;
        private System.Windows.Forms.Button buttonPotrdi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxZdravilo;
        private System.Windows.Forms.Label label1;
    }
}