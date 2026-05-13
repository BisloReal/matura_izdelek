namespace evidenca_krav.Obrazci
{
    partial class DodajOdhodForm
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
            this.comboBoxZival = new System.Windows.Forms.ComboBox();
            this.buttonPreklici = new System.Windows.Forms.Button();
            this.buttonPotrdi = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxGmid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLokacija = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxVzrok = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.richTextBoxOpombe = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // comboBoxZival
            // 
            this.comboBoxZival.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxZival.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxZival.FormattingEnabled = true;
            this.comboBoxZival.Location = new System.Drawing.Point(58, 456);
            this.comboBoxZival.Name = "comboBoxZival";
            this.comboBoxZival.Size = new System.Drawing.Size(219, 39);
            this.comboBoxZival.TabIndex = 35;
            // 
            // buttonPreklici
            // 
            this.buttonPreklici.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPreklici.Location = new System.Drawing.Point(58, 522);
            this.buttonPreklici.Name = "buttonPreklici";
            this.buttonPreklici.Size = new System.Drawing.Size(100, 43);
            this.buttonPreklici.TabIndex = 34;
            this.buttonPreklici.Text = "Prekliči";
            this.buttonPreklici.UseVisualStyleBackColor = true;
            this.buttonPreklici.Click += new System.EventHandler(this.buttonPreklici_Click);
            // 
            // buttonPotrdi
            // 
            this.buttonPotrdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPotrdi.Location = new System.Drawing.Point(177, 522);
            this.buttonPotrdi.Name = "buttonPotrdi";
            this.buttonPotrdi.Size = new System.Drawing.Size(100, 43);
            this.buttonPotrdi.TabIndex = 33;
            this.buttonPotrdi.Text = "Dodaj";
            this.buttonPotrdi.UseVisualStyleBackColor = true;
            this.buttonPotrdi.Click += new System.EventHandler(this.buttonPotrdi_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(136, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 25);
            this.label5.TabIndex = 32;
            this.label5.Text = "G-Mid";
            // 
            // textBoxGmid
            // 
            this.textBoxGmid.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGmid.Location = new System.Drawing.Point(58, 196);
            this.textBoxGmid.Name = "textBoxGmid";
            this.textBoxGmid.Size = new System.Drawing.Size(219, 38);
            this.textBoxGmid.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(127, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 25);
            this.label4.TabIndex = 30;
            this.label4.Text = "Lokacija";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(96, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 25);
            this.label3.TabIndex = 29;
            this.label3.Text = "Datum odhoda";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker.Location = new System.Drawing.Point(58, 120);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(219, 30);
            this.dateTimePicker.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(97, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(488, 63);
            this.label1.TabIndex = 25;
            this.label1.Text = "Dodaj odhod živali";
            // 
            // textBoxLokacija
            // 
            this.textBoxLokacija.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLokacija.Location = new System.Drawing.Point(58, 283);
            this.textBoxLokacija.Name = "textBoxLokacija";
            this.textBoxLokacija.Size = new System.Drawing.Size(219, 38);
            this.textBoxLokacija.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(141, 428);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 25);
            this.label2.TabIndex = 38;
            this.label2.Text = "Žival";
            // 
            // textBoxVzrok
            // 
            this.textBoxVzrok.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxVzrok.Location = new System.Drawing.Point(58, 371);
            this.textBoxVzrok.Name = "textBoxVzrok";
            this.textBoxVzrok.Size = new System.Drawing.Size(219, 38);
            this.textBoxVzrok.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(136, 343);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 25);
            this.label6.TabIndex = 39;
            this.label6.Text = "Vzrok";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(425, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 25);
            this.label7.TabIndex = 41;
            this.label7.Text = "Opombe";
            // 
            // richTextBoxOpombe
            // 
            this.richTextBoxOpombe.Location = new System.Drawing.Point(319, 120);
            this.richTextBoxOpombe.Name = "richTextBoxOpombe";
            this.richTextBoxOpombe.Size = new System.Drawing.Size(293, 445);
            this.richTextBoxOpombe.TabIndex = 42;
            this.richTextBoxOpombe.Text = "";
            // 
            // DodajOdhodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 586);
            this.Controls.Add(this.richTextBoxOpombe);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxVzrok);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxLokacija);
            this.Controls.Add(this.comboBoxZival);
            this.Controls.Add(this.buttonPreklici);
            this.Controls.Add(this.buttonPotrdi);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxGmid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DodajOdhodForm";
            this.Text = "DodajOdhodForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxZival;
        private System.Windows.Forms.Button buttonPreklici;
        private System.Windows.Forms.Button buttonPotrdi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxGmid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLokacija;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxVzrok;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox richTextBoxOpombe;
    }
}