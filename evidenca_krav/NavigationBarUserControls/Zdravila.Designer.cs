namespace evidenca_krav.NavigationBarUserControls
{
    partial class Zdravila
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
            this.flowLayoutPanelZdravila = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonDodajZdravilo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanelZdravila.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanelZdravila
            // 
            this.flowLayoutPanelZdravila.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelZdravila.AutoScroll = true;
            this.flowLayoutPanelZdravila.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanelZdravila.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanelZdravila.Location = new System.Drawing.Point(34, 122);
            this.flowLayoutPanelZdravila.Name = "flowLayoutPanelZdravila";
            this.flowLayoutPanelZdravila.Size = new System.Drawing.Size(633, 247);
            this.flowLayoutPanelZdravila.TabIndex = 8;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // buttonDodajZdravilo
            // 
            this.buttonDodajZdravilo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDodajZdravilo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDodajZdravilo.Location = new System.Drawing.Point(522, 64);
            this.buttonDodajZdravilo.Name = "buttonDodajZdravilo";
            this.buttonDodajZdravilo.Size = new System.Drawing.Size(145, 44);
            this.buttonDodajZdravilo.TabIndex = 7;
            this.buttonDodajZdravilo.Text = "Dodaj zdravilo";
            this.buttonDodajZdravilo.UseVisualStyleBackColor = true;
            this.buttonDodajZdravilo.Click += new System.EventHandler(this.buttonDodajZdravilo_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(248, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 55);
            this.label1.TabIndex = 6;
            this.label1.Text = "Zdravila";
            // 
            // Zdravila
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelZdravila);
            this.Controls.Add(this.buttonDodajZdravilo);
            this.Controls.Add(this.label1);
            this.Name = "Zdravila";
            this.Size = new System.Drawing.Size(700, 400);
            this.flowLayoutPanelZdravila.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelZdravila;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonDodajZdravilo;
        private System.Windows.Forms.Label label1;
    }
}
