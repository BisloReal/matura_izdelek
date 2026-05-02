namespace evidenca_krav.Obrazci
{
    partial class ZadolzitveOsebForm
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
            this.buttonZapri = new System.Windows.Forms.Button();
            this.buttonUrediIzbrano = new System.Windows.Forms.Button();
            this.buttonPotrdi = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxZadolzitev = new System.Windows.Forms.TextBox();
            this.listBoxZadolzitve = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonZapri
            // 
            this.buttonZapri.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZapri.Location = new System.Drawing.Point(456, 395);
            this.buttonZapri.Name = "buttonZapri";
            this.buttonZapri.Size = new System.Drawing.Size(223, 43);
            this.buttonZapri.TabIndex = 31;
            this.buttonZapri.Text = "Nazaj na osebe";
            this.buttonZapri.UseVisualStyleBackColor = true;
            this.buttonZapri.Click += new System.EventHandler(this.buttonZapri_Click);
            // 
            // buttonUrediIzbrano
            // 
            this.buttonUrediIzbrano.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUrediIzbrano.Location = new System.Drawing.Point(456, 224);
            this.buttonUrediIzbrano.Name = "buttonUrediIzbrano";
            this.buttonUrediIzbrano.Size = new System.Drawing.Size(223, 43);
            this.buttonUrediIzbrano.TabIndex = 30;
            this.buttonUrediIzbrano.Text = "Uredi izbrano";
            this.buttonUrediIzbrano.UseVisualStyleBackColor = true;
            this.buttonUrediIzbrano.Click += new System.EventHandler(this.buttonUrediIzbrano_Click);
            // 
            // buttonPotrdi
            // 
            this.buttonPotrdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPotrdi.Location = new System.Drawing.Point(456, 175);
            this.buttonPotrdi.Name = "buttonPotrdi";
            this.buttonPotrdi.Size = new System.Drawing.Size(223, 43);
            this.buttonPotrdi.TabIndex = 29;
            this.buttonPotrdi.Text = "Dodaj";
            this.buttonPotrdi.UseVisualStyleBackColor = true;
            this.buttonPotrdi.Click += new System.EventHandler(this.buttonPotrdi_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(456, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 25);
            this.label2.TabIndex = 28;
            this.label2.Text = "Zadolžitev";
            // 
            // textBoxZadolzitev
            // 
            this.textBoxZadolzitev.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxZadolzitev.Location = new System.Drawing.Point(456, 131);
            this.textBoxZadolzitev.Name = "textBoxZadolzitev";
            this.textBoxZadolzitev.Size = new System.Drawing.Size(223, 38);
            this.textBoxZadolzitev.TabIndex = 27;
            // 
            // listBoxZadolzitve
            // 
            this.listBoxZadolzitve.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxZadolzitve.FormattingEnabled = true;
            this.listBoxZadolzitve.Location = new System.Drawing.Point(20, 105);
            this.listBoxZadolzitve.Name = "listBoxZadolzitve";
            this.listBoxZadolzitve.Size = new System.Drawing.Size(430, 342);
            this.listBoxZadolzitve.TabIndex = 26;
            this.listBoxZadolzitve.SelectedIndexChanged += new System.EventHandler(this.listBoxZadolzitve_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(136, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 63);
            this.label1.TabIndex = 25;
            this.label1.Text = "Zadolžitve oseb";
            // 
            // ZadolzitveOsebForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 450);
            this.Controls.Add(this.buttonZapri);
            this.Controls.Add(this.buttonUrediIzbrano);
            this.Controls.Add(this.buttonPotrdi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxZadolzitev);
            this.Controls.Add(this.listBoxZadolzitve);
            this.Controls.Add(this.label1);
            this.Name = "ZadolzitveOsebForm";
            this.Text = "ZadolzitveOsebForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonZapri;
        private System.Windows.Forms.Button buttonUrediIzbrano;
        private System.Windows.Forms.Button buttonPotrdi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxZadolzitev;
        private System.Windows.Forms.ListBox listBoxZadolzitve;
        private System.Windows.Forms.Label label1;
    }
}