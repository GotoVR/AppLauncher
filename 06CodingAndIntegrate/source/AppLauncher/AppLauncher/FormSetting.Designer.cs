namespace AppLauncher
{
    partial class FormSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetting));
            this.button_reset = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            this.button_vol_minus = new System.Windows.Forms.Button();
            this.button_vol_plus = new System.Windows.Forms.Button();
            this.button_restart = new System.Windows.Forms.Button();
            this.ucKeyBorderNum1 = new HZH_Controls.Controls.UCKeyBorderNum();
            this.button_login = new System.Windows.Forms.Button();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_reset
            // 
            this.button_reset.BackgroundImage = global::AppLauncher.Properties.Resources.按键bj;
            this.button_reset.FlatAppearance.BorderSize = 0;
            this.button_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_reset.Location = new System.Drawing.Point(9, 9);
            this.button_reset.Margin = new System.Windows.Forms.Padding(0);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(136, 44);
            this.button_reset.TabIndex = 0;
            this.button_reset.Text = "重置";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // button_back
            // 
            this.button_back.BackgroundImage = global::AppLauncher.Properties.Resources.按键bj;
            this.button_back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_back.FlatAppearance.BorderSize = 0;
            this.button_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_back.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.button_back.Location = new System.Drawing.Point(876, 12);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(136, 44);
            this.button_back.TabIndex = 15;
            this.button_back.Text = "返回";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // button_vol_minus
            // 
            this.button_vol_minus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_vol_minus.BackgroundImage")));
            this.button_vol_minus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_vol_minus.FlatAppearance.BorderSize = 0;
            this.button_vol_minus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_vol_minus.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.button_vol_minus.Location = new System.Drawing.Point(876, 712);
            this.button_vol_minus.Name = "button_vol_minus";
            this.button_vol_minus.Size = new System.Drawing.Size(136, 44);
            this.button_vol_minus.TabIndex = 14;
            this.button_vol_minus.Text = "音量-";
            this.button_vol_minus.UseVisualStyleBackColor = true;
            this.button_vol_minus.Click += new System.EventHandler(this.button_vol_minus_Click);
            // 
            // button_vol_plus
            // 
            this.button_vol_plus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_vol_plus.BackgroundImage")));
            this.button_vol_plus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_vol_plus.FlatAppearance.BorderSize = 0;
            this.button_vol_plus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_vol_plus.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.button_vol_plus.Location = new System.Drawing.Point(876, 662);
            this.button_vol_plus.Name = "button_vol_plus";
            this.button_vol_plus.Size = new System.Drawing.Size(136, 44);
            this.button_vol_plus.TabIndex = 13;
            this.button_vol_plus.Text = "音量+";
            this.button_vol_plus.UseVisualStyleBackColor = true;
            this.button_vol_plus.Click += new System.EventHandler(this.button_vol_plus_Click);
            // 
            // button_restart
            // 
            this.button_restart.BackColor = System.Drawing.Color.Transparent;
            this.button_restart.BackgroundImage = global::AppLauncher.Properties.Resources.按键bj;
            this.button_restart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_restart.FlatAppearance.BorderSize = 0;
            this.button_restart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_restart.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.button_restart.Location = new System.Drawing.Point(12, 712);
            this.button_restart.Name = "button_restart";
            this.button_restart.Size = new System.Drawing.Size(136, 44);
            this.button_restart.TabIndex = 12;
            this.button_restart.Text = "重启";
            this.button_restart.UseVisualStyleBackColor = false;
            this.button_restart.Click += new System.EventHandler(this.button_restart_Click);
            // 
            // ucKeyBorderNum1
            // 
            this.ucKeyBorderNum1.BackColor = System.Drawing.Color.White;
            this.ucKeyBorderNum1.Location = new System.Drawing.Point(296, 221);
            this.ucKeyBorderNum1.Name = "ucKeyBorderNum1";
            this.ucKeyBorderNum1.Size = new System.Drawing.Size(424, 218);
            this.ucKeyBorderNum1.TabIndex = 11;
            this.ucKeyBorderNum1.UseCustomEvent = false;
            // 
            // button_login
            // 
            this.button_login.BackgroundImage = global::AppLauncher.Properties.Resources.按键bj;
            this.button_login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_login.FlatAppearance.BorderSize = 0;
            this.button_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_login.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.button_login.Location = new System.Drawing.Point(601, 81);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(136, 44);
            this.button_login.TabIndex = 10;
            this.button_login.Text = "登录";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // textBox_password
            // 
            this.textBox_password.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.textBox_password.Location = new System.Drawing.Point(376, 81);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(209, 39);
            this.textBox_password.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(275, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "输入密码：";
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_vol_minus);
            this.Controls.Add(this.button_vol_plus);
            this.Controls.Add(this.button_restart);
            this.Controls.Add(this.ucKeyBorderNum1);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_reset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.Button button_back;
        private System.Windows.Forms.Button button_vol_minus;
        private System.Windows.Forms.Button button_vol_plus;
        private System.Windows.Forms.Button button_restart;
        private HZH_Controls.Controls.UCKeyBorderNum ucKeyBorderNum1;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label1;
    }
}