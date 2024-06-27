namespace MilkManage_App
{
    partial class loginUp_form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginUp_form));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.textBox_login = new System.Windows.Forms.TextBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnSeePass = new System.Windows.Forms.PictureBox();
            this.btnNotSeePass = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSeePass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNotSeePass)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(23, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 153);
            this.panel1.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(97, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(356, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "У нас только самое свежое и вкусное!";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Monotype Corsiva", 24F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label3.Location = new System.Drawing.Point(123, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(305, 39);
            this.label3.TabIndex = 0;
            this.label3.Text = "Молочные Вкусняшки";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.Secret_Click);
            // 
            // textBox_password
            // 
            this.textBox_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_password.Location = new System.Drawing.Point(142, 258);
            this.textBox_password.Multiline = true;
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(360, 30);
            this.textBox_password.TabIndex = 14;
            this.textBox_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxToSubmit_KeyDown);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(63, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "Пароль:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(63, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Логин:";
            // 
            // LoginButton
            // 
            this.LoginButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.Location = new System.Drawing.Point(20, 324);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(528, 71);
            this.LoginButton.TabIndex = 10;
            this.LoginButton.Text = "Войти в систему";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // textBox_login
            // 
            this.textBox_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_login.Location = new System.Drawing.Point(142, 191);
            this.textBox_login.Multiline = true;
            this.textBox_login.Name = "textBox_login";
            this.textBox_login.Size = new System.Drawing.Size(406, 30);
            this.textBox_login.TabIndex = 20;
            this.textBox_login.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxToSubmit_KeyDown);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::MilkManage_App.Properties.Resources.pass_key;
            this.pictureBox4.Location = new System.Drawing.Point(20, 251);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(43, 41);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 22;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::MilkManage_App.Properties.Resources.user;
            this.pictureBox3.Location = new System.Drawing.Point(20, 188);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(43, 41);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 21;
            this.pictureBox3.TabStop = false;
            // 
            // btnSeePass
            // 
            this.btnSeePass.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSeePass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnSeePass.Image = global::MilkManage_App.Properties.Resources.pass_open;
            this.btnSeePass.Location = new System.Drawing.Point(508, 253);
            this.btnSeePass.Name = "btnSeePass";
            this.btnSeePass.Size = new System.Drawing.Size(40, 39);
            this.btnSeePass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSeePass.TabIndex = 18;
            this.btnSeePass.TabStop = false;
            this.btnSeePass.Click += new System.EventHandler(this.Show_Click);
            // 
            // btnNotSeePass
            // 
            this.btnNotSeePass.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnNotSeePass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnNotSeePass.Image = global::MilkManage_App.Properties.Resources.pass_closed;
            this.btnNotSeePass.Location = new System.Drawing.Point(508, 253);
            this.btnNotSeePass.Name = "btnNotSeePass";
            this.btnNotSeePass.Size = new System.Drawing.Size(40, 39);
            this.btnNotSeePass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnNotSeePass.TabIndex = 19;
            this.btnNotSeePass.TabStop = false;
            this.btnNotSeePass.Click += new System.EventHandler(this.Hide_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Monotype Corsiva", 24F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label5.Location = new System.Drawing.Point(141, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(267, 39);
            this.label5.TabIndex = 2;
            this.label5.Text = "Молочный Передоз";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loginUp_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 420);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.textBox_login);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.btnSeePass);
            this.Controls.Add(this.btnNotSeePass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "loginUp_form";
            this.Text = "Экран входа";
            this.Load += new System.EventHandler(this.loginUp_form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSeePass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNotSeePass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnSeePass;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.PictureBox btnNotSeePass;
        private System.Windows.Forms.TextBox textBox_login;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

