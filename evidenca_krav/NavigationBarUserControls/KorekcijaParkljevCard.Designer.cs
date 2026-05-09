namespace evidenca_krav.NavigationBarUserControls
{
    partial class KorekcijaParkljevCard
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
            this.labelStanje = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelIzvajalec = new System.Windows.Forms.Label();
            this.labelDatum = new System.Windows.Forms.Label();
            this.buttonUrediTel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelStanje
            // 
            this.labelStanje.AutoSize = true;
            this.labelStanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStanje.Location = new System.Drawing.Point(208, 53);
            this.labelStanje.Name = "labelStanje";
            this.labelStanje.Size = new System.Drawing.Size(53, 25);
            this.labelStanje.TabIndex = 62;
            this.labelStanje.Text = "label";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(104, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 25);
            this.label7.TabIndex = 61;
            this.label7.Text = "Stanje:";
            // 
            // labelIzvajalec
            // 
            this.labelIzvajalec.AutoSize = true;
            this.labelIzvajalec.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIzvajalec.Location = new System.Drawing.Point(208, 87);
            this.labelIzvajalec.Name = "labelIzvajalec";
            this.labelIzvajalec.Size = new System.Drawing.Size(53, 25);
            this.labelIzvajalec.TabIndex = 59;
            this.labelIzvajalec.Text = "label";
            // 
            // labelDatum
            // 
            this.labelDatum.AutoSize = true;
            this.labelDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDatum.Location = new System.Drawing.Point(208, 18);
            this.labelDatum.Name = "labelDatum";
            this.labelDatum.Size = new System.Drawing.Size(53, 25);
            this.labelDatum.TabIndex = 57;
            this.labelDatum.Text = "label";
            // 
            // buttonUrediTel
            // 
            this.buttonUrediTel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUrediTel.Location = new System.Drawing.Point(39, 139);
            this.buttonUrediTel.Name = "buttonUrediTel";
            this.buttonUrediTel.Size = new System.Drawing.Size(222, 43);
            this.buttonUrediTel.TabIndex = 56;
            this.buttonUrediTel.Text = "Uredi / Pogled";
            this.buttonUrediTel.UseVisualStyleBackColor = true;
            this.buttonUrediTel.Click += new System.EventHandler(this.buttonUrediKorekcijo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(81, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 25);
            this.label3.TabIndex = 54;
            this.label3.Text = "Izvajalec:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(104, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 25);
            this.label1.TabIndex = 52;
            this.label1.Text = "Datum:";
            // 
            // KorekcijaParkljevCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelStanje);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelIzvajalec);
            this.Controls.Add(this.labelDatum);
            this.Controls.Add(this.buttonUrediTel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "KorekcijaParkljevCard";
            this.Size = new System.Drawing.Size(542, 201);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStanje;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelIzvajalec;
        private System.Windows.Forms.Label labelDatum;
        private System.Windows.Forms.Button buttonUrediTel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}
