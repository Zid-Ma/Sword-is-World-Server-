namespace SwordIsWorldServer
{
    partial class FormSet
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.checkBoxWhiteList = new System.Windows.Forms.CheckBox();
            this.textBoxWhiteList = new System.Windows.Forms.TextBox();
            this.labelWhiteList = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.textBoxMaximumPeople = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTimeInterval = new System.Windows.Forms.Label();
            this.checkBoxTimeInterval = new System.Windows.Forms.CheckBox();
            this.checkBoxLoginPassword = new System.Windows.Forms.CheckBox();
            this.checkBoxMaximumPeople = new System.Windows.Forms.CheckBox();
            this.textBoxTimeInterval = new System.Windows.Forms.TextBox();
            this.textBoxLoginPassword = new System.Windows.Forms.TextBox();
            this.checkBoxThreadCountLeft = new System.Windows.Forms.CheckBox();
            this.panelLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(125, 296);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(123, 46);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "保存设置";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // checkBoxWhiteList
            // 
            this.checkBoxWhiteList.AutoSize = true;
            this.checkBoxWhiteList.Location = new System.Drawing.Point(12, 6);
            this.checkBoxWhiteList.Name = "checkBoxWhiteList";
            this.checkBoxWhiteList.Size = new System.Drawing.Size(71, 19);
            this.checkBoxWhiteList.TabIndex = 1;
            this.checkBoxWhiteList.Text = "白名单";
            this.checkBoxWhiteList.UseVisualStyleBackColor = true;
            // 
            // textBoxWhiteList
            // 
            this.textBoxWhiteList.Location = new System.Drawing.Point(3, 46);
            this.textBoxWhiteList.Multiline = true;
            this.textBoxWhiteList.Name = "textBoxWhiteList";
            this.textBoxWhiteList.Size = new System.Drawing.Size(300, 305);
            this.textBoxWhiteList.TabIndex = 2;
            // 
            // labelWhiteList
            // 
            this.labelWhiteList.AutoSize = true;
            this.labelWhiteList.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelWhiteList.Location = new System.Drawing.Point(9, 28);
            this.labelWhiteList.Name = "labelWhiteList";
            this.labelWhiteList.Size = new System.Drawing.Size(241, 15);
            this.labelWhiteList.TabIndex = 3;
            this.labelWhiteList.Text = "里面放入IP,每一个地址都需要隔行";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.checkBoxWhiteList);
            this.panelLeft.Controls.Add(this.textBoxWhiteList);
            this.panelLeft.Controls.Add(this.labelWhiteList);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(316, 354);
            this.panelLeft.TabIndex = 4;
            // 
            // textBoxMaximumPeople
            // 
            this.textBoxMaximumPeople.Location = new System.Drawing.Point(98, 12);
            this.textBoxMaximumPeople.Name = "textBoxMaximumPeople";
            this.textBoxMaximumPeople.Size = new System.Drawing.Size(150, 25);
            this.textBoxMaximumPeople.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelTimeInterval);
            this.panel1.Controls.Add(this.checkBoxThreadCountLeft);
            this.panel1.Controls.Add(this.checkBoxTimeInterval);
            this.panel1.Controls.Add(this.checkBoxLoginPassword);
            this.panel1.Controls.Add(this.checkBoxMaximumPeople);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.textBoxTimeInterval);
            this.panel1.Controls.Add(this.textBoxLoginPassword);
            this.panel1.Controls.Add(this.textBoxMaximumPeople);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(322, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 354);
            this.panel1.TabIndex = 6;
            // 
            // labelTimeInterval
            // 
            this.labelTimeInterval.AutoSize = true;
            this.labelTimeInterval.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelTimeInterval.Location = new System.Drawing.Point(95, 120);
            this.labelTimeInterval.Name = "labelTimeInterval";
            this.labelTimeInterval.Size = new System.Drawing.Size(75, 15);
            this.labelTimeInterval.TabIndex = 6;
            this.labelTimeInterval.Text = "单位:毫秒";
            // 
            // checkBoxTimeInterval
            // 
            this.checkBoxTimeInterval.AutoSize = true;
            this.checkBoxTimeInterval.Location = new System.Drawing.Point(12, 88);
            this.checkBoxTimeInterval.Name = "checkBoxTimeInterval";
            this.checkBoxTimeInterval.Size = new System.Drawing.Size(86, 19);
            this.checkBoxTimeInterval.TabIndex = 1;
            this.checkBoxTimeInterval.Text = "发送间隔";
            this.checkBoxTimeInterval.UseVisualStyleBackColor = true;
            // 
            // checkBoxLoginPassword
            // 
            this.checkBoxLoginPassword.AutoSize = true;
            this.checkBoxLoginPassword.Location = new System.Drawing.Point(12, 52);
            this.checkBoxLoginPassword.Name = "checkBoxLoginPassword";
            this.checkBoxLoginPassword.Size = new System.Drawing.Size(86, 19);
            this.checkBoxLoginPassword.TabIndex = 1;
            this.checkBoxLoginPassword.Text = "登录密码";
            this.checkBoxLoginPassword.UseVisualStyleBackColor = true;
            // 
            // checkBoxMaximumPeople
            // 
            this.checkBoxMaximumPeople.AutoSize = true;
            this.checkBoxMaximumPeople.Location = new System.Drawing.Point(12, 18);
            this.checkBoxMaximumPeople.Name = "checkBoxMaximumPeople";
            this.checkBoxMaximumPeople.Size = new System.Drawing.Size(86, 19);
            this.checkBoxMaximumPeople.TabIndex = 1;
            this.checkBoxMaximumPeople.Text = "最大人数";
            this.checkBoxMaximumPeople.UseVisualStyleBackColor = true;
            // 
            // textBoxTimeInterval
            // 
            this.textBoxTimeInterval.Location = new System.Drawing.Point(98, 82);
            this.textBoxTimeInterval.Name = "textBoxTimeInterval";
            this.textBoxTimeInterval.Size = new System.Drawing.Size(150, 25);
            this.textBoxTimeInterval.TabIndex = 5;
            // 
            // textBoxLoginPassword
            // 
            this.textBoxLoginPassword.Location = new System.Drawing.Point(98, 46);
            this.textBoxLoginPassword.Name = "textBoxLoginPassword";
            this.textBoxLoginPassword.Size = new System.Drawing.Size(150, 25);
            this.textBoxLoginPassword.TabIndex = 5;
            // 
            // checkBoxThreadCountLeft
            // 
            this.checkBoxThreadCountLeft.AutoSize = true;
            this.checkBoxThreadCountLeft.Location = new System.Drawing.Point(12, 160);
            this.checkBoxThreadCountLeft.Name = "checkBoxThreadCountLeft";
            this.checkBoxThreadCountLeft.Size = new System.Drawing.Size(86, 19);
            this.checkBoxThreadCountLeft.TabIndex = 1;
            this.checkBoxThreadCountLeft.Text = "减少线程";
            this.checkBoxThreadCountLeft.UseVisualStyleBackColor = true;
            // 
            // FormSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 354);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelLeft);
            this.Name = "FormSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkBoxWhiteList;
        private System.Windows.Forms.TextBox textBoxWhiteList;
        private System.Windows.Forms.Label labelWhiteList;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.TextBox textBoxMaximumPeople;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxMaximumPeople;
        private System.Windows.Forms.CheckBox checkBoxLoginPassword;
        private System.Windows.Forms.TextBox textBoxLoginPassword;
        private System.Windows.Forms.CheckBox checkBoxTimeInterval;
        private System.Windows.Forms.TextBox textBoxTimeInterval;
        private System.Windows.Forms.Label labelTimeInterval;
        private System.Windows.Forms.CheckBox checkBoxThreadCountLeft;
    }
}