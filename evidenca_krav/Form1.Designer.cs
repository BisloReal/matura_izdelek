namespace evidenca_krav
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelPrikaz = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonGlavnoOkno = new System.Windows.Forms.Button();
            this.buttonKrave = new System.Windows.Forms.Button();
            this.buttonTelice = new System.Windows.Forms.Button();
            this.buttonBiki = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 452);
            this.panel1.TabIndex = 0;
            // 
            // panelPrikaz
            // 
            this.panelPrikaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelPrikaz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrikaz.Location = new System.Drawing.Point(150, 0);
            this.panelPrikaz.Name = "panelPrikaz";
            this.panelPrikaz.Size = new System.Drawing.Size(650, 452);
            this.panelPrikaz.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonBiki, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonTelice, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonKrave, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonGlavnoOkno, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(146, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonGlavnoOkno
            // 
            this.buttonGlavnoOkno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGlavnoOkno.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGlavnoOkno.Location = new System.Drawing.Point(3, 3);
            this.buttonGlavnoOkno.Name = "buttonGlavnoOkno";
            this.buttonGlavnoOkno.Size = new System.Drawing.Size(140, 68);
            this.buttonGlavnoOkno.TabIndex = 0;
            this.buttonGlavnoOkno.Text = "Glavno okno";
            this.buttonGlavnoOkno.UseVisualStyleBackColor = true;
            this.buttonGlavnoOkno.Click += new System.EventHandler(this.buttonGlavnoOkno_Click);
            // 
            // buttonKrave
            // 
            this.buttonKrave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonKrave.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonKrave.Location = new System.Drawing.Point(3, 77);
            this.buttonKrave.Name = "buttonKrave";
            this.buttonKrave.Size = new System.Drawing.Size(140, 68);
            this.buttonKrave.TabIndex = 1;
            this.buttonKrave.Text = "Krave";
            this.buttonKrave.UseVisualStyleBackColor = true;
            this.buttonKrave.Click += new System.EventHandler(this.buttonKrave_Click);
            // 
            // buttonTelice
            // 
            this.buttonTelice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTelice.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTelice.Location = new System.Drawing.Point(3, 151);
            this.buttonTelice.Name = "buttonTelice";
            this.buttonTelice.Size = new System.Drawing.Size(140, 68);
            this.buttonTelice.TabIndex = 2;
            this.buttonTelice.Text = "Telice";
            this.buttonTelice.UseVisualStyleBackColor = true;
            this.buttonTelice.Click += new System.EventHandler(this.buttonTelice_Click);
            // 
            // buttonBiki
            // 
            this.buttonBiki.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBiki.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBiki.Location = new System.Drawing.Point(3, 225);
            this.buttonBiki.Name = "buttonBiki";
            this.buttonBiki.Size = new System.Drawing.Size(140, 69);
            this.buttonBiki.TabIndex = 3;
            this.buttonBiki.Text = "Biki";
            this.buttonBiki.UseVisualStyleBackColor = true;
            this.buttonBiki.Click += new System.EventHandler(this.buttonBiki_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 452);
            this.Controls.Add(this.panelPrikaz);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelPrikaz;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonGlavnoOkno;
        private System.Windows.Forms.Button buttonTelice;
        private System.Windows.Forms.Button buttonKrave;
        private System.Windows.Forms.Button buttonBiki;
    }
}

