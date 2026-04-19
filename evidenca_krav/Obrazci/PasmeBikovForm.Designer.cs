namespace evidenca_krav.Obrazci
{
    partial class PasmeBikovForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxPasme = new System.Windows.Forms.ListBox();
            this.textBoxPasma = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonPotrdi = new System.Windows.Forms.Button();
            this.buttonUrediIzbrano = new System.Windows.Forms.Button();
            this.buttonZapri = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(90, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(518, 63);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pasme bikov centra";
            // 
            // listBoxPasme
            // 
            this.listBoxPasme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxPasme.FormattingEnabled = true;
            this.listBoxPasme.Location = new System.Drawing.Point(23, 105);
            this.listBoxPasme.Name = "listBoxPasme";
            this.listBoxPasme.Size = new System.Drawing.Size(430, 342);
            this.listBoxPasme.TabIndex = 3;
            this.listBoxPasme.SelectedIndexChanged += new System.EventHandler(this.listBoxPasme_SelectedIndexChanged);
            // 
            // textBoxPasma
            // 
            this.textBoxPasma.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPasma.Location = new System.Drawing.Point(459, 131);
            this.textBoxPasma.Name = "textBoxPasma";
            this.textBoxPasma.Size = new System.Drawing.Size(223, 38);
            this.textBoxPasma.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(459, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 25);
            this.label2.TabIndex = 15;
            this.label2.Text = "Pasma";
            // 
            // buttonPotrdi
            // 
            this.buttonPotrdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPotrdi.Location = new System.Drawing.Point(459, 175);
            this.buttonPotrdi.Name = "buttonPotrdi";
            this.buttonPotrdi.Size = new System.Drawing.Size(223, 43);
            this.buttonPotrdi.TabIndex = 22;
            this.buttonPotrdi.Text = "Dodaj";
            this.buttonPotrdi.UseVisualStyleBackColor = true;
            this.buttonPotrdi.Click += new System.EventHandler(this.buttonPotrdi_Click);
            // 
            // buttonUrediIzbrano
            // 
            this.buttonUrediIzbrano.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUrediIzbrano.Location = new System.Drawing.Point(459, 224);
            this.buttonUrediIzbrano.Name = "buttonUrediIzbrano";
            this.buttonUrediIzbrano.Size = new System.Drawing.Size(223, 43);
            this.buttonUrediIzbrano.TabIndex = 23;
            this.buttonUrediIzbrano.Text = "Uredi izbrano";
            this.buttonUrediIzbrano.UseVisualStyleBackColor = true;
            this.buttonUrediIzbrano.Click += new System.EventHandler(this.buttonUrediIzbrano_Click);
            // 
            // buttonZapri
            // 
            this.buttonZapri.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZapri.Location = new System.Drawing.Point(459, 395);
            this.buttonZapri.Name = "buttonZapri";
            this.buttonZapri.Size = new System.Drawing.Size(223, 43);
            this.buttonZapri.TabIndex = 24;
            this.buttonZapri.Text = "Nazaj na bike";
            this.buttonZapri.UseVisualStyleBackColor = true;
            this.buttonZapri.Click += new System.EventHandler(this.buttonZapri_Click);
            // 
            // PasmeBikovForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 450);
            this.Controls.Add(this.buttonZapri);
            this.Controls.Add(this.buttonUrediIzbrano);
            this.Controls.Add(this.buttonPotrdi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPasma);
            this.Controls.Add(this.listBoxPasme);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasmeBikovForm";
            this.Text = "PasmeBikovForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxPasme;
        private System.Windows.Forms.TextBox textBoxPasma;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonPotrdi;
        private System.Windows.Forms.Button buttonUrediIzbrano;
        private System.Windows.Forms.Button buttonZapri;
    }
}