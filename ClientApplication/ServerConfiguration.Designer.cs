namespace ClientApplication
{
    partial class ServerConfiguration
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
            this.label1 = new System.Windows.Forms.Label();
            this.IP1 = new System.Windows.Forms.TextBox();
            this.IP2 = new System.Windows.Forms.TextBox();
            this.IP3 = new System.Windows.Forms.TextBox();
            this.IP4 = new System.Windows.Forms.TextBox();
            this.port_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.login_tb = new System.Windows.Forms.TextBox();
            this.pwd_tb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.hide_bt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Address";
            // 
            // IP1
            // 
            this.IP1.Location = new System.Drawing.Point(87, 12);
            this.IP1.Name = "IP1";
            this.IP1.Size = new System.Drawing.Size(33, 22);
            this.IP1.TabIndex = 2;
            this.IP1.Text = "150";
            this.IP1.TextChanged += new System.EventHandler(this.IP1_TextChanged);
            // 
            // IP2
            // 
            this.IP2.Location = new System.Drawing.Point(126, 12);
            this.IP2.Name = "IP2";
            this.IP2.Size = new System.Drawing.Size(33, 22);
            this.IP2.TabIndex = 3;
            this.IP2.Text = "254";
            this.IP2.TextChanged += new System.EventHandler(this.IP2_TextChanged);
            // 
            // IP3
            // 
            this.IP3.Location = new System.Drawing.Point(165, 12);
            this.IP3.Name = "IP3";
            this.IP3.Size = new System.Drawing.Size(33, 22);
            this.IP3.TabIndex = 4;
            this.IP3.Text = "41";
            this.IP3.TextChanged += new System.EventHandler(this.IP3_TextChanged);
            // 
            // IP4
            // 
            this.IP4.Location = new System.Drawing.Point(204, 12);
            this.IP4.Name = "IP4";
            this.IP4.Size = new System.Drawing.Size(33, 22);
            this.IP4.TabIndex = 5;
            this.IP4.Text = "195";
            this.IP4.TextChanged += new System.EventHandler(this.IP4_TextChanged);
            // 
            // port_tb
            // 
            this.port_tb.Location = new System.Drawing.Point(87, 40);
            this.port_tb.Name = "port_tb";
            this.port_tb.Size = new System.Drawing.Size(100, 22);
            this.port_tb.TabIndex = 6;
            this.port_tb.Text = "8080";
            this.port_tb.TextChanged += new System.EventHandler(this.port_tb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Port";
            // 
            // login_tb
            // 
            this.login_tb.Location = new System.Drawing.Point(87, 69);
            this.login_tb.Name = "login_tb";
            this.login_tb.Size = new System.Drawing.Size(100, 22);
            this.login_tb.TabIndex = 8;
            this.login_tb.Text = "kuba";
            this.login_tb.TextChanged += new System.EventHandler(this.login_tb_TextChanged);
            // 
            // pwd_tb
            // 
            this.pwd_tb.Location = new System.Drawing.Point(87, 98);
            this.pwd_tb.Name = "pwd_tb";
            this.pwd_tb.PasswordChar = '*';
            this.pwd_tb.Size = new System.Drawing.Size(100, 22);
            this.pwd_tb.TabIndex = 9;
            this.pwd_tb.Text = "noga111";
            this.pwd_tb.TextChanged += new System.EventHandler(this.pwd_tb_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Login";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Password";
            // 
            // hide_bt
            // 
            this.hide_bt.Location = new System.Drawing.Point(87, 126);
            this.hide_bt.Name = "hide_bt";
            this.hide_bt.Size = new System.Drawing.Size(75, 23);
            this.hide_bt.TabIndex = 12;
            this.hide_bt.Text = "Done";
            this.hide_bt.UseVisualStyleBackColor = true;
            this.hide_bt.Click += new System.EventHandler(this.hide_bt_Click);
            // 
            // ServerConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 161);
            this.Controls.Add(this.hide_bt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pwd_tb);
            this.Controls.Add(this.login_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.port_tb);
            this.Controls.Add(this.IP4);
            this.Controls.Add(this.IP3);
            this.Controls.Add(this.IP2);
            this.Controls.Add(this.IP1);
            this.Controls.Add(this.label1);
            this.Name = "ServerConfiguration";
            this.Text = "ServerConfiguration";
            this.Load += new System.EventHandler(this.ServerConfiguration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IP1;
        private System.Windows.Forms.TextBox IP2;
        private System.Windows.Forms.TextBox IP3;
        private System.Windows.Forms.TextBox IP4;
        private System.Windows.Forms.TextBox port_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox login_tb;
        private System.Windows.Forms.TextBox pwd_tb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button hide_bt;
    }
}