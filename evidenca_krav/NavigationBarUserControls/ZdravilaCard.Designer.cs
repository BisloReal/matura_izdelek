namespace evidenca_krav.NavigationBarUserControls
{
    partial class ZdravilaCard
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
            this.labelZdravilo = new System.Windows.Forms.Label();
            this.buttonUrediZdravilo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelZdravilo
            // 
            this.labelZdravilo.AutoSize = true;
            this.labelZdravilo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZdravilo.Location = new System.Drawing.Point(149, 21);
            this.labelZdravilo.Name = "labelZdravilo";
            this.labelZdravilo.Size = new System.Drawing.Size(53, 25);
            this.labelZdravilo.TabIndex = 81;
            this.labelZdravilo.Text = "label";
            // 
            // buttonUrediZdravilo
            // 
            this.buttonUrediZdravilo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUrediZdravilo.Location = new System.Drawing.Point(27, 67);
            this.buttonUrediZdravilo.Name = "buttonUrediZdravilo";
            this.buttonUrediZdravilo.Size = new System.Drawing.Size(114, 43);
            this.buttonUrediZdravilo.TabIndex = 80;
            this.buttonUrediZdravilo.Text = "Uredi";
            this.buttonUrediZdravilo.UseVisualStyleBackColor = true;
            this.buttonUrediZdravilo.Click += new System.EventHandler(this.buttonUrediZdravilo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 25);
            this.label1.TabIndex = 78;
            this.label1.Text = "Zdravilo:";
            // 
            // ZdravilaCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelZdravilo);
            this.Controls.Add(this.buttonUrediZdravilo);
            this.Controls.Add(this.label1);
            this.Name = "ZdravilaCard";
            this.Size = new System.Drawing.Size(461, 136);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelZdravilo;
        private System.Windows.Forms.Button buttonUrediZdravilo;
        private System.Windows.Forms.Label label1;
    }
}
