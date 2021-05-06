namespace SwordIsWorldServer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonOpenServer = new System.Windows.Forms.Button();
            this.textBoxMain = new System.Windows.Forms.TextBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelTopBack = new System.Windows.Forms.Panel();
            this.textBoxMainBack = new System.Windows.Forms.TextBox();
            this.panelMainLeft = new System.Windows.Forms.Panel();
            this.panelMainRight = new System.Windows.Forms.Panel();
            this.buttonSet = new System.Windows.Forms.Button();
            this.labelMemoryFootprintR = new System.Windows.Forms.Label();
            this.labelNumberOfPeopleR = new System.Windows.Forms.Label();
            this.labelMemoryFootprintL = new System.Windows.Forms.Label();
            this.labelNumberOfPeopleL = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.panelDown = new System.Windows.Forms.Panel();
            this.panelDownBack = new System.Windows.Forms.Panel();
            this.textBoxIntroduce = new System.Windows.Forms.TextBox();
            this.labelIntroduce = new System.Windows.Forms.Label();
            this.panelDownRight = new System.Windows.Forms.Panel();
            this.labelTargetIP = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxTargetIP = new System.Windows.Forms.TextBox();
            this.textBoxSendMessage = new System.Windows.Forms.TextBox();
            this.panelDownLeft = new System.Windows.Forms.Panel();
            this.checkBoxIP = new System.Windows.Forms.CheckBox();
            this.checkBoxPort = new System.Windows.Forms.CheckBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.timerBack = new System.Windows.Forms.Timer(this.components);
            this.timerOffLineDetection = new System.Windows.Forms.Timer(this.components);
            this.panelMain.SuspendLayout();
            this.panelTopBack.SuspendLayout();
            this.panelMainLeft.SuspendLayout();
            this.panelMainRight.SuspendLayout();
            this.panelDown.SuspendLayout();
            this.panelDownBack.SuspendLayout();
            this.panelDownRight.SuspendLayout();
            this.panelDownLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenServer
            // 
            this.buttonOpenServer.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonOpenServer.FlatAppearance.BorderSize = 0;
            this.buttonOpenServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenServer.ForeColor = System.Drawing.Color.White;
            this.buttonOpenServer.Location = new System.Drawing.Point(195, 9);
            this.buttonOpenServer.Name = "buttonOpenServer";
            this.buttonOpenServer.Size = new System.Drawing.Size(150, 75);
            this.buttonOpenServer.TabIndex = 0;
            this.buttonOpenServer.Text = "打开服务器";
            this.buttonOpenServer.UseVisualStyleBackColor = false;
            this.buttonOpenServer.Click += new System.EventHandler(this.buttonOpenServer_Click);
            // 
            // textBoxMain
            // 
            this.textBoxMain.AcceptsReturn = true;
            this.textBoxMain.AcceptsTab = true;
            this.textBoxMain.BackColor = System.Drawing.Color.DodgerBlue;
            this.textBoxMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMain.ForeColor = System.Drawing.SystemColors.Menu;
            this.textBoxMain.Location = new System.Drawing.Point(3, 3);
            this.textBoxMain.MaxLength = 3276;
            this.textBoxMain.Multiline = true;
            this.textBoxMain.Name = "textBoxMain";
            this.textBoxMain.ReadOnly = true;
            this.textBoxMain.Size = new System.Drawing.Size(630, 344);
            this.textBoxMain.TabIndex = 1;
            this.textBoxMain.TextChanged += new System.EventHandler(this.textBoxMain_TextChanged);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelTopBack);
            this.panelMain.Controls.Add(this.panelMainLeft);
            this.panelMain.Controls.Add(this.panelMainRight);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(800, 350);
            this.panelMain.TabIndex = 2;
            // 
            // panelTopBack
            // 
            this.panelTopBack.Controls.Add(this.textBoxMainBack);
            this.panelTopBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTopBack.Location = new System.Drawing.Point(633, 0);
            this.panelTopBack.Name = "panelTopBack";
            this.panelTopBack.Size = new System.Drawing.Size(6, 350);
            this.panelTopBack.TabIndex = 4;
            // 
            // textBoxMainBack
            // 
            this.textBoxMainBack.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMainBack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMainBack.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBoxMainBack.Location = new System.Drawing.Point(3, 3);
            this.textBoxMainBack.MaxLength = 3276;
            this.textBoxMainBack.Multiline = true;
            this.textBoxMainBack.Name = "textBoxMainBack";
            this.textBoxMainBack.ReadOnly = true;
            this.textBoxMainBack.Size = new System.Drawing.Size(374, 344);
            this.textBoxMainBack.TabIndex = 0;
            // 
            // panelMainLeft
            // 
            this.panelMainLeft.BackColor = System.Drawing.Color.DodgerBlue;
            this.panelMainLeft.Controls.Add(this.textBoxMain);
            this.panelMainLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMainLeft.ForeColor = System.Drawing.Color.White;
            this.panelMainLeft.Location = new System.Drawing.Point(0, 0);
            this.panelMainLeft.Name = "panelMainLeft";
            this.panelMainLeft.Size = new System.Drawing.Size(633, 350);
            this.panelMainLeft.TabIndex = 3;
            // 
            // panelMainRight
            // 
            this.panelMainRight.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelMainRight.Controls.Add(this.buttonSet);
            this.panelMainRight.Controls.Add(this.labelMemoryFootprintR);
            this.panelMainRight.Controls.Add(this.labelNumberOfPeopleR);
            this.panelMainRight.Controls.Add(this.labelMemoryFootprintL);
            this.panelMainRight.Controls.Add(this.labelNumberOfPeopleL);
            this.panelMainRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelMainRight.Location = new System.Drawing.Point(639, 0);
            this.panelMainRight.Name = "panelMainRight";
            this.panelMainRight.Size = new System.Drawing.Size(161, 350);
            this.panelMainRight.TabIndex = 2;
            // 
            // buttonSet
            // 
            this.buttonSet.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonSet.FlatAppearance.BorderSize = 0;
            this.buttonSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSet.ForeColor = System.Drawing.Color.White;
            this.buttonSet.Location = new System.Drawing.Point(12, 305);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(140, 35);
            this.buttonSet.TabIndex = 1;
            this.buttonSet.Text = "设置";
            this.buttonSet.UseVisualStyleBackColor = false;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // labelMemoryFootprintR
            // 
            this.labelMemoryFootprintR.AutoSize = true;
            this.labelMemoryFootprintR.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelMemoryFootprintR.ForeColor = System.Drawing.Color.White;
            this.labelMemoryFootprintR.Location = new System.Drawing.Point(84, 35);
            this.labelMemoryFootprintR.Name = "labelMemoryFootprintR";
            this.labelMemoryFootprintR.Size = new System.Drawing.Size(55, 15);
            this.labelMemoryFootprintR.TabIndex = 0;
            this.labelMemoryFootprintR.Text = "000.00";
            // 
            // labelNumberOfPeopleR
            // 
            this.labelNumberOfPeopleR.AutoSize = true;
            this.labelNumberOfPeopleR.ForeColor = System.Drawing.Color.White;
            this.labelNumberOfPeopleR.Location = new System.Drawing.Point(84, 9);
            this.labelNumberOfPeopleR.Name = "labelNumberOfPeopleR";
            this.labelNumberOfPeopleR.Size = new System.Drawing.Size(55, 15);
            this.labelNumberOfPeopleR.TabIndex = 0;
            this.labelNumberOfPeopleR.Text = "000.00";
            // 
            // labelMemoryFootprintL
            // 
            this.labelMemoryFootprintL.AutoSize = true;
            this.labelMemoryFootprintL.ForeColor = System.Drawing.Color.White;
            this.labelMemoryFootprintL.Location = new System.Drawing.Point(3, 35);
            this.labelMemoryFootprintL.Name = "labelMemoryFootprintL";
            this.labelMemoryFootprintL.Size = new System.Drawing.Size(75, 15);
            this.labelMemoryFootprintL.TabIndex = 0;
            this.labelMemoryFootprintL.Text = "内存占用:";
            // 
            // labelNumberOfPeopleL
            // 
            this.labelNumberOfPeopleL.AutoSize = true;
            this.labelNumberOfPeopleL.ForeColor = System.Drawing.Color.White;
            this.labelNumberOfPeopleL.Location = new System.Drawing.Point(3, 9);
            this.labelNumberOfPeopleL.Name = "labelNumberOfPeopleL";
            this.labelNumberOfPeopleL.Size = new System.Drawing.Size(75, 15);
            this.labelNumberOfPeopleL.TabIndex = 0;
            this.labelNumberOfPeopleL.Text = "在线人数:";
            // 
            // textBoxIP
            // 
            this.textBoxIP.ForeColor = System.Drawing.SystemColors.Highlight;
            this.textBoxIP.Location = new System.Drawing.Point(60, 13);
            this.textBoxIP.MaxLength = 100;
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(129, 25);
            this.textBoxIP.TabIndex = 3;
            this.textBoxIP.Text = "127.0.0.1";
            // 
            // panelDown
            // 
            this.panelDown.Controls.Add(this.panelDownBack);
            this.panelDown.Controls.Add(this.panelDownRight);
            this.panelDown.Controls.Add(this.panelDownLeft);
            this.panelDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDown.Location = new System.Drawing.Point(0, 356);
            this.panelDown.Name = "panelDown";
            this.panelDown.Size = new System.Drawing.Size(800, 94);
            this.panelDown.TabIndex = 4;
            // 
            // panelDownBack
            // 
            this.panelDownBack.Controls.Add(this.textBoxIntroduce);
            this.panelDownBack.Controls.Add(this.labelIntroduce);
            this.panelDownBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDownBack.Location = new System.Drawing.Point(360, 0);
            this.panelDownBack.Name = "panelDownBack";
            this.panelDownBack.Size = new System.Drawing.Size(6, 94);
            this.panelDownBack.TabIndex = 9;
            // 
            // textBoxIntroduce
            // 
            this.textBoxIntroduce.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxIntroduce.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxIntroduce.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBoxIntroduce.Location = new System.Drawing.Point(3, 27);
            this.textBoxIntroduce.Multiline = true;
            this.textBoxIntroduce.Name = "textBoxIntroduce";
            this.textBoxIntroduce.ReadOnly = true;
            this.textBoxIntroduce.Size = new System.Drawing.Size(377, 64);
            this.textBoxIntroduce.TabIndex = 1;
            this.textBoxIntroduce.Text = "    当前的官方网站地址:www.xyks.xyz\r\n    访问域名如果不能进入的话，可以试着使用IP地址进行访问\r\n    网站提供相关下载，我也希望能得到" +
    "更多的支持";
            // 
            // labelIntroduce
            // 
            this.labelIntroduce.AutoSize = true;
            this.labelIntroduce.ForeColor = System.Drawing.Color.BlueViolet;
            this.labelIntroduce.Location = new System.Drawing.Point(6, 9);
            this.labelIntroduce.Name = "labelIntroduce";
            this.labelIntroduce.Size = new System.Drawing.Size(120, 15);
            this.labelIntroduce.TabIndex = 0;
            this.labelIntroduce.Text = "剑与世界:紫鱼星";
            // 
            // panelDownRight
            // 
            this.panelDownRight.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelDownRight.Controls.Add(this.labelTargetIP);
            this.panelDownRight.Controls.Add(this.buttonSend);
            this.panelDownRight.Controls.Add(this.textBoxTargetIP);
            this.panelDownRight.Controls.Add(this.textBoxSendMessage);
            this.panelDownRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDownRight.Location = new System.Drawing.Point(366, 0);
            this.panelDownRight.Name = "panelDownRight";
            this.panelDownRight.Size = new System.Drawing.Size(434, 94);
            this.panelDownRight.TabIndex = 8;
            // 
            // labelTargetIP
            // 
            this.labelTargetIP.AutoSize = true;
            this.labelTargetIP.ForeColor = System.Drawing.Color.White;
            this.labelTargetIP.Location = new System.Drawing.Point(143, 57);
            this.labelTargetIP.Name = "labelTargetIP";
            this.labelTargetIP.Size = new System.Drawing.Size(53, 15);
            this.labelTargetIP.TabIndex = 8;
            this.labelTargetIP.Text = "目标IP";
            // 
            // buttonSend
            // 
            this.buttonSend.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonSend.FlatAppearance.BorderSize = 0;
            this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSend.ForeColor = System.Drawing.Color.White;
            this.buttonSend.Location = new System.Drawing.Point(287, 45);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(135, 39);
            this.buttonSend.TabIndex = 7;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = false;
            // 
            // textBoxTargetIP
            // 
            this.textBoxTargetIP.Location = new System.Drawing.Point(17, 50);
            this.textBoxTargetIP.MaxLength = 100;
            this.textBoxTargetIP.Name = "textBoxTargetIP";
            this.textBoxTargetIP.Size = new System.Drawing.Size(120, 25);
            this.textBoxTargetIP.TabIndex = 6;
            // 
            // textBoxSendMessage
            // 
            this.textBoxSendMessage.Location = new System.Drawing.Point(17, 15);
            this.textBoxSendMessage.Name = "textBoxSendMessage";
            this.textBoxSendMessage.Size = new System.Drawing.Size(405, 25);
            this.textBoxSendMessage.TabIndex = 6;
            // 
            // panelDownLeft
            // 
            this.panelDownLeft.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelDownLeft.Controls.Add(this.checkBoxIP);
            this.panelDownLeft.Controls.Add(this.checkBoxPort);
            this.panelDownLeft.Controls.Add(this.buttonOpenServer);
            this.panelDownLeft.Controls.Add(this.textBoxIP);
            this.panelDownLeft.Controls.Add(this.textBoxPort);
            this.panelDownLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelDownLeft.Location = new System.Drawing.Point(0, 0);
            this.panelDownLeft.Name = "panelDownLeft";
            this.panelDownLeft.Size = new System.Drawing.Size(360, 94);
            this.panelDownLeft.TabIndex = 7;
            // 
            // checkBoxIP
            // 
            this.checkBoxIP.AutoSize = true;
            this.checkBoxIP.ForeColor = System.Drawing.Color.White;
            this.checkBoxIP.Location = new System.Drawing.Point(12, 17);
            this.checkBoxIP.Name = "checkBoxIP";
            this.checkBoxIP.Size = new System.Drawing.Size(42, 19);
            this.checkBoxIP.TabIndex = 5;
            this.checkBoxIP.Text = "IP";
            this.checkBoxIP.UseVisualStyleBackColor = true;
            // 
            // checkBoxPort
            // 
            this.checkBoxPort.AutoSize = true;
            this.checkBoxPort.ForeColor = System.Drawing.Color.White;
            this.checkBoxPort.Location = new System.Drawing.Point(12, 56);
            this.checkBoxPort.Name = "checkBoxPort";
            this.checkBoxPort.Size = new System.Drawing.Size(71, 19);
            this.checkBoxPort.TabIndex = 5;
            this.checkBoxPort.Text = "端口号";
            this.checkBoxPort.UseVisualStyleBackColor = true;
            // 
            // textBoxPort
            // 
            this.textBoxPort.ForeColor = System.Drawing.SystemColors.Highlight;
            this.textBoxPort.Location = new System.Drawing.Point(89, 51);
            this.textBoxPort.MaxLength = 100;
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 25);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.Text = "10629";
            // 
            // timerMain
            // 
            this.timerMain.Enabled = true;
            this.timerMain.Interval = 1000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // timerBack
            // 
            this.timerBack.Enabled = true;
            this.timerBack.Interval = 1000;
            this.timerBack.Tick += new System.EventHandler(this.timerBack_Tick);
            // 
            // timerOffLineDetection
            // 
            this.timerOffLineDetection.Enabled = true;
            this.timerOffLineDetection.Interval = 100000;
            this.timerOffLineDetection.Tick += new System.EventHandler(this.timerOffLineDetection_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelDown);
            this.Controls.Add(this.panelMain);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "剑与世界服务端";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelMain.ResumeLayout(false);
            this.panelTopBack.ResumeLayout(false);
            this.panelTopBack.PerformLayout();
            this.panelMainLeft.ResumeLayout(false);
            this.panelMainLeft.PerformLayout();
            this.panelMainRight.ResumeLayout(false);
            this.panelMainRight.PerformLayout();
            this.panelDown.ResumeLayout(false);
            this.panelDownBack.ResumeLayout(false);
            this.panelDownBack.PerformLayout();
            this.panelDownRight.ResumeLayout(false);
            this.panelDownRight.PerformLayout();
            this.panelDownLeft.ResumeLayout(false);
            this.panelDownLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenServer;
        private System.Windows.Forms.TextBox textBoxMain;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelMainRight;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Panel panelMainLeft;
        private System.Windows.Forms.Panel panelDown;
        private System.Windows.Forms.CheckBox checkBoxPort;
        private System.Windows.Forms.CheckBox checkBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Panel panelDownRight;
        private System.Windows.Forms.TextBox textBoxSendMessage;
        private System.Windows.Forms.Panel panelDownLeft;
        private System.Windows.Forms.TextBox textBoxTargetIP;
        private System.Windows.Forms.Label labelTargetIP;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Label labelNumberOfPeopleL;
        private System.Windows.Forms.Label labelNumberOfPeopleR;
        private System.Windows.Forms.Label labelMemoryFootprintL;
        private System.Windows.Forms.Label labelMemoryFootprintR;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Panel panelTopBack;
        private System.Windows.Forms.TextBox textBoxMainBack;
        private System.Windows.Forms.Panel panelDownBack;
        private System.Windows.Forms.Label labelIntroduce;
        private System.Windows.Forms.TextBox textBoxIntroduce;
        private System.Windows.Forms.Timer timerBack;
        private System.Windows.Forms.Timer timerOffLineDetection;
    }
}

