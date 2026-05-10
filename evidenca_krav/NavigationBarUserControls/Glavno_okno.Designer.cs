namespace evidenca_krav.NavigationBar
{
    partial class Glavno_okno
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPosodobi = new System.Windows.Forms.Button();
            this.flowLayoutPanelObvestila = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelObvestila.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(347, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 55);
            this.label1.TabIndex = 1;
            this.label1.Text = "Glavno okno";
            // 
            // buttonPosodobi
            // 
            this.buttonPosodobi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPosodobi.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPosodobi.Location = new System.Drawing.Point(844, 76);
            this.buttonPosodobi.Name = "buttonPosodobi";
            this.buttonPosodobi.Size = new System.Drawing.Size(147, 70);
            this.buttonPosodobi.TabIndex = 2;
            this.buttonPosodobi.Text = "Posodobi stanja";
            this.buttonPosodobi.UseVisualStyleBackColor = true;
            this.buttonPosodobi.Click += new System.EventHandler(this.buttonPosodobi_Click);
            // 
            // flowLayoutPanelObvestila
            // 
            this.flowLayoutPanelObvestila.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelObvestila.AutoScroll = true;
            this.flowLayoutPanelObvestila.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanelObvestila.Controls.Add(this.flowLayoutPanel6);
            this.flowLayoutPanelObvestila.Location = new System.Drawing.Point(53, 191);
            this.flowLayoutPanelObvestila.Name = "flowLayoutPanelObvestila";
            this.flowLayoutPanelObvestila.Size = new System.Drawing.Size(911, 382);
            this.flowLayoutPanelObvestila.TabIndex = 50;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel6.TabIndex = 0;
            // 
            // Glavno_okno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelObvestila);
            this.Controls.Add(this.buttonPosodobi);
            this.Controls.Add(this.label1);
            this.Name = "Glavno_okno";
            this.Size = new System.Drawing.Size(1019, 623);
            this.flowLayoutPanelObvestila.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPosodobi;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelObvestila;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
    }
}
