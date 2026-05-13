namespace evidenca_krav.Obrazci
{
    partial class DodajSpecifikoForm
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
            this.comboBoxKrave = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonPreklici = new System.Windows.Forms.Button();
            this.buttonPotrdi = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSpecifika = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBoxKrave
            // 
            this.comboBoxKrave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKrave.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxKrave.FormattingEnabled = true;
            this.comboBoxKrave.Location = new System.Drawing.Point(179, 188);
            this.comboBoxKrave.Name = "comboBoxKrave";
            this.comboBoxKrave.Size = new System.Drawing.Size(364, 33);
            this.comboBoxKrave.TabIndex = 119;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(96, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 25);
            this.label4.TabIndex = 118;
            this.label4.Text = "Žival";
            // 
            // buttonPreklici
            // 
            this.buttonPreklici.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPreklici.Location = new System.Drawing.Point(339, 254);
            this.buttonPreklici.Name = "buttonPreklici";
            this.buttonPreklici.Size = new System.Drawing.Size(100, 43);
            this.buttonPreklici.TabIndex = 112;
            this.buttonPreklici.Text = "Prekliči";
            this.buttonPreklici.UseVisualStyleBackColor = true;
            this.buttonPreklici.Click += new System.EventHandler(this.buttonPreklici_Click);
            // 
            // buttonPotrdi
            // 
            this.buttonPotrdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPotrdi.Location = new System.Drawing.Point(443, 254);
            this.buttonPotrdi.Name = "buttonPotrdi";
            this.buttonPotrdi.Size = new System.Drawing.Size(100, 43);
            this.buttonPotrdi.TabIndex = 111;
            this.buttonPotrdi.Text = "Dodaj";
            this.buttonPotrdi.UseVisualStyleBackColor = true;
            this.buttonPotrdi.Click += new System.EventHandler(this.buttonPotrdi_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(80, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 25);
            this.label3.TabIndex = 110;
            this.label3.Text = "Datum";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Checked = false;
            this.dateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker.Location = new System.Drawing.Point(179, 152);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.ShowCheckBox = true;
            this.dateTimePicker.Size = new System.Drawing.Size(364, 30);
            this.dateTimePicker.TabIndex = 109;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(410, 63);
            this.label1.TabIndex = 107;
            this.label1.Text = "Dodaj specifiko";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 25);
            this.label2.TabIndex = 121;
            this.label2.Text = "Specifika";
            // 
            // textBoxSpecifika
            // 
            this.textBoxSpecifika.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSpecifika.Location = new System.Drawing.Point(179, 108);
            this.textBoxSpecifika.Name = "textBoxSpecifika";
            this.textBoxSpecifika.Size = new System.Drawing.Size(364, 38);
            this.textBoxSpecifika.TabIndex = 120;
            // 
            // DodajSpecifikoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 321);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSpecifika);
            this.Controls.Add(this.comboBoxKrave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonPreklici);
            this.Controls.Add(this.buttonPotrdi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DodajSpecifikoForm";
            this.Text = "DodajSpecifikoForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxKrave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonPreklici;
        private System.Windows.Forms.Button buttonPotrdi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSpecifika;
    }
}