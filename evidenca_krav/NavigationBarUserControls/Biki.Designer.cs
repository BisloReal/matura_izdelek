namespace evidenca_krav.NavigationBar
{
    partial class Biki
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
            this.flowLayoutPanelBiki = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonDodajBika = new System.Windows.Forms.Button();
            this.flowLayoutPanelBiki.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(230, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 55);
            this.label1.TabIndex = 1;
            this.label1.Text = "Biki centra";
            // 
            // flowLayoutPanelBiki
            // 
            this.flowLayoutPanelBiki.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelBiki.AutoScroll = true;
            this.flowLayoutPanelBiki.AutoSize = true;
            this.flowLayoutPanelBiki.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanelBiki.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanelBiki.Location = new System.Drawing.Point(34, 106);
            this.flowLayoutPanelBiki.Name = "flowLayoutPanelBiki";
            this.flowLayoutPanelBiki.Size = new System.Drawing.Size(633, 247);
            this.flowLayoutPanelBiki.TabIndex = 5;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // buttonDodajBika
            // 
            this.buttonDodajBika.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDodajBika.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDodajBika.Location = new System.Drawing.Point(546, 48);
            this.buttonDodajBika.Name = "buttonDodajBika";
            this.buttonDodajBika.Size = new System.Drawing.Size(121, 44);
            this.buttonDodajBika.TabIndex = 4;
            this.buttonDodajBika.Text = "Dodaj bika";
            this.buttonDodajBika.UseVisualStyleBackColor = true;
            this.buttonDodajBika.Click += new System.EventHandler(this.buttonDodajBika_Click);
            // 
            // Biki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelBiki);
            this.Controls.Add(this.buttonDodajBika);
            this.Controls.Add(this.label1);
            this.Name = "Biki";
            this.Size = new System.Drawing.Size(700, 400);
            this.flowLayoutPanelBiki.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBiki;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonDodajBika;
    }
}
