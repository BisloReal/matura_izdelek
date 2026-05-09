namespace evidenca_krav.NavigationBarUserControls
{
    partial class ZdravljenjeCard
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
            this.labelVeterinar = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelZapSt = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelVzrok = new System.Windows.Forms.Label();
            this.labelDatum = new System.Windows.Forms.Label();
            this.buttonUrediOs = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelVeterinar
            // 
            this.labelVeterinar.AutoSize = true;
            this.labelVeterinar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVeterinar.Location = new System.Drawing.Point(245, 120);
            this.labelVeterinar.Name = "labelVeterinar";
            this.labelVeterinar.Size = new System.Drawing.Size(53, 25);
            this.labelVeterinar.TabIndex = 103;
            this.labelVeterinar.Text = "label";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(115, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 25);
            this.label3.TabIndex = 102;
            this.label3.Text = "Veterinar:";
            // 
            // labelZapSt
            // 
            this.labelZapSt.AutoSize = true;
            this.labelZapSt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZapSt.Location = new System.Drawing.Point(245, 46);
            this.labelZapSt.Name = "labelZapSt";
            this.labelZapSt.Size = new System.Drawing.Size(53, 25);
            this.labelZapSt.TabIndex = 101;
            this.labelZapSt.Text = "label";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(202, 25);
            this.label7.TabIndex = 100;
            this.label7.Text = "Zaporedna številka:";
            // 
            // labelVzrok
            // 
            this.labelVzrok.AutoSize = true;
            this.labelVzrok.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVzrok.Location = new System.Drawing.Point(245, 84);
            this.labelVzrok.Name = "labelVzrok";
            this.labelVzrok.Size = new System.Drawing.Size(53, 25);
            this.labelVzrok.TabIndex = 99;
            this.labelVzrok.Text = "label";
            // 
            // labelDatum
            // 
            this.labelDatum.AutoSize = true;
            this.labelDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDatum.Location = new System.Drawing.Point(245, 11);
            this.labelDatum.Name = "labelDatum";
            this.labelDatum.Size = new System.Drawing.Size(53, 25);
            this.labelDatum.TabIndex = 98;
            this.labelDatum.Text = "label";
            // 
            // buttonUrediOs
            // 
            this.buttonUrediOs.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUrediOs.Location = new System.Drawing.Point(60, 162);
            this.buttonUrediOs.Name = "buttonUrediOs";
            this.buttonUrediOs.Size = new System.Drawing.Size(222, 43);
            this.buttonUrediOs.TabIndex = 97;
            this.buttonUrediOs.Text = "Uredi / Pogled";
            this.buttonUrediOs.UseVisualStyleBackColor = true;
            this.buttonUrediOs.Click += new System.EventHandler(this.buttonUrediZdravljenje_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(147, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 25);
            this.label4.TabIndex = 96;
            this.label4.Text = "Vzrok:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(141, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 25);
            this.label1.TabIndex = 95;
            this.label1.Text = "Datum:";
            // 
            // ZdravljenjeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelVeterinar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelZapSt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelVzrok);
            this.Controls.Add(this.labelDatum);
            this.Controls.Add(this.buttonUrediOs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "ZdravljenjeCard";
            this.Size = new System.Drawing.Size(578, 220);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelVeterinar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelZapSt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelVzrok;
        private System.Windows.Forms.Label labelDatum;
        private System.Windows.Forms.Button buttonUrediOs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
    }
}
