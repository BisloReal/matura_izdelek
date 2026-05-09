namespace evidenca_krav.NavigationBarUserControls
{
    partial class SpecifikaCard
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
            this.labelDatum = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelSpecifika = new System.Windows.Forms.Label();
            this.buttonUrediSpec = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDatum
            // 
            this.labelDatum.AutoSize = true;
            this.labelDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDatum.Location = new System.Drawing.Point(201, 62);
            this.labelDatum.Name = "labelDatum";
            this.labelDatum.Size = new System.Drawing.Size(53, 25);
            this.labelDatum.TabIndex = 86;
            this.labelDatum.Text = "label";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(97, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 25);
            this.label5.TabIndex = 85;
            this.label5.Text = "Datum:";
            // 
            // labelSpecifika
            // 
            this.labelSpecifika.AutoSize = true;
            this.labelSpecifika.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpecifika.Location = new System.Drawing.Point(201, 27);
            this.labelSpecifika.Name = "labelSpecifika";
            this.labelSpecifika.Size = new System.Drawing.Size(53, 25);
            this.labelSpecifika.TabIndex = 82;
            this.labelSpecifika.Text = "label";
            // 
            // buttonUrediSpec
            // 
            this.buttonUrediSpec.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUrediSpec.Location = new System.Drawing.Point(32, 103);
            this.buttonUrediSpec.Name = "buttonUrediSpec";
            this.buttonUrediSpec.Size = new System.Drawing.Size(222, 43);
            this.buttonUrediSpec.TabIndex = 80;
            this.buttonUrediSpec.Text = "Uredi / Pogled";
            this.buttonUrediSpec.UseVisualStyleBackColor = true;
            this.buttonUrediSpec.Click += new System.EventHandler(this.buttonUrediSpec_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(70, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 25);
            this.label4.TabIndex = 79;
            this.label4.Text = "Specifika:";
            // 
            // SpecifikaCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelDatum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelSpecifika);
            this.Controls.Add(this.buttonUrediSpec);
            this.Controls.Add(this.label4);
            this.Name = "SpecifikaCard";
            this.Size = new System.Drawing.Size(540, 177);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDatum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelSpecifika;
        private System.Windows.Forms.Button buttonUrediSpec;
        private System.Windows.Forms.Label label4;
    }
}
