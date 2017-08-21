namespace WindowsApplication1
{
    partial class Form_MainConfig
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
            this.label_appName = new System.Windows.Forms.Label();
            this.comboBox_AppName = new System.Windows.Forms.ComboBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label_appNumb = new System.Windows.Forms.Label();
            this.comboBox_appNumb = new System.Windows.Forms.ComboBox();
            this.label_firstAppName = new System.Windows.Forms.Label();
            this.comboBox_app1 = new System.Windows.Forms.ComboBox();
            this.label_NetSelect = new System.Windows.Forms.Label();
            this.checkBox_NetSelect = new System.Windows.Forms.CheckBox();
            this.label_soundMode = new System.Windows.Forms.Label();
            this.textBox_soundMode = new System.Windows.Forms.TextBox();
            this.label_soundNumb = new System.Windows.Forms.Label();
            this.textBox_soundNumb = new System.Windows.Forms.TextBox();
            this.label_appDiscern = new System.Windows.Forms.Label();
            this.textBox_appDiscern = new System.Windows.Forms.TextBox();
            this.ListerPort = new System.Windows.Forms.Label();
            this.TeachIP = new System.Windows.Forms.Label();
            this.comboBox_ListerPort = new System.Windows.Forms.ComboBox();
            this.TeachPort = new System.Windows.Forms.Label();
            this.CpuIP = new System.Windows.Forms.Label();
            this.CpuPort = new System.Windows.Forms.Label();
            this.comboBox_TeachPort = new System.Windows.Forms.ComboBox();
            this.comboBox_CpuPort = new System.Windows.Forms.ComboBox();
            this.LocIpNum = new System.Windows.Forms.Label();
            this.UinitiyNum = new System.Windows.Forms.Label();
            this.textBox_UinitiyNum = new System.Windows.Forms.TextBox();
            this.comboBox_LocIpNum = new System.Windows.Forms.ComboBox();
            this.label_ = new System.Windows.Forms.Label();
            this.ipAddressTextBox_Cpu = new IPAddressTextBox.IPAddressTextBox();
            this.ipAddressTextBox_Tech = new IPAddressTextBox.IPAddressTextBox();
            this.checkBox_DebugMode = new System.Windows.Forms.CheckBox();
            this.checkBox_NetTurnOn = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label_appName
            // 
            this.label_appName.AutoSize = true;
            this.label_appName.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_appName.Location = new System.Drawing.Point(65, 16);
            this.label_appName.Name = "label_appName";
            this.label_appName.Size = new System.Drawing.Size(67, 14);
            this.label_appName.TabIndex = 0;
            this.label_appName.Text = "工程名称";
            // 
            // comboBox_AppName
            // 
            this.comboBox_AppName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_AppName.FormattingEnabled = true;
            this.comboBox_AppName.Location = new System.Drawing.Point(138, 13);
            this.comboBox_AppName.Name = "comboBox_AppName";
            this.comboBox_AppName.Size = new System.Drawing.Size(121, 20);
            this.comboBox_AppName.TabIndex = 1;
            this.comboBox_AppName.SelectedIndexChanged += new System.EventHandler(this.comboBox_AppName_SelectedIndexChanged);
            // 
            // button_OK
            // 
            this.button_OK.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_OK.Location = new System.Drawing.Point(369, 279);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Cancel.Location = new System.Drawing.Point(505, 279);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // label_appNumb
            // 
            this.label_appNumb.AutoSize = true;
            this.label_appNumb.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_appNumb.Location = new System.Drawing.Point(20, 45);
            this.label_appNumb.Name = "label_appNumb";
            this.label_appNumb.Size = new System.Drawing.Size(112, 14);
            this.label_appNumb.TabIndex = 4;
            this.label_appNumb.Text = "启用的工程数量";
            // 
            // comboBox_appNumb
            // 
            this.comboBox_appNumb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_appNumb.FormattingEnabled = true;
            this.comboBox_appNumb.IntegralHeight = false;
            this.comboBox_appNumb.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBox_appNumb.Location = new System.Drawing.Point(138, 42);
            this.comboBox_appNumb.Name = "comboBox_appNumb";
            this.comboBox_appNumb.Size = new System.Drawing.Size(47, 20);
            this.comboBox_appNumb.TabIndex = 5;
            // 
            // label_firstAppName
            // 
            this.label_firstAppName.AutoSize = true;
            this.label_firstAppName.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_firstAppName.Location = new System.Drawing.Point(87, 71);
            this.label_firstAppName.Name = "label_firstAppName";
            this.label_firstAppName.Size = new System.Drawing.Size(45, 14);
            this.label_firstAppName.TabIndex = 6;
            this.label_firstAppName.Text = "工程1";
            // 
            // comboBox_app1
            // 
            this.comboBox_app1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_app1.FormattingEnabled = true;
            this.comboBox_app1.Location = new System.Drawing.Point(138, 68);
            this.comboBox_app1.Name = "comboBox_app1";
            this.comboBox_app1.Size = new System.Drawing.Size(121, 20);
            this.comboBox_app1.TabIndex = 7;
            // 
            // label_NetSelect
            // 
            this.label_NetSelect.AutoSize = true;
            this.label_NetSelect.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_NetSelect.Location = new System.Drawing.Point(65, 101);
            this.label_NetSelect.Name = "label_NetSelect";
            this.label_NetSelect.Size = new System.Drawing.Size(67, 14);
            this.label_NetSelect.TabIndex = 8;
            this.label_NetSelect.Text = "网络选择";
            // 
            // checkBox_NetSelect
            // 
            this.checkBox_NetSelect.AutoSize = true;
            this.checkBox_NetSelect.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_NetSelect.Location = new System.Drawing.Point(138, 100);
            this.checkBox_NetSelect.Name = "checkBox_NetSelect";
            this.checkBox_NetSelect.Size = new System.Drawing.Size(60, 16);
            this.checkBox_NetSelect.TabIndex = 9;
            this.checkBox_NetSelect.Text = "新网络";
            this.checkBox_NetSelect.UseVisualStyleBackColor = true;
            // 
            // label_soundMode
            // 
            this.label_soundMode.AutoSize = true;
            this.label_soundMode.Location = new System.Drawing.Point(356, 17);
            this.label_soundMode.Name = "label_soundMode";
            this.label_soundMode.Size = new System.Drawing.Size(53, 12);
            this.label_soundMode.TabIndex = 11;
            this.label_soundMode.Text = "声音模块";
            // 
            // textBox_soundMode
            // 
            this.textBox_soundMode.Location = new System.Drawing.Point(416, 13);
            this.textBox_soundMode.Name = "textBox_soundMode";
            this.textBox_soundMode.Size = new System.Drawing.Size(150, 21);
            this.textBox_soundMode.TabIndex = 12;
            this.textBox_soundMode.Text = "NULL";
            // 
            // label_soundNumb
            // 
            this.label_soundNumb.AutoSize = true;
            this.label_soundNumb.Location = new System.Drawing.Point(356, 46);
            this.label_soundNumb.Name = "label_soundNumb";
            this.label_soundNumb.Size = new System.Drawing.Size(53, 12);
            this.label_soundNumb.TabIndex = 13;
            this.label_soundNumb.Text = "声音个数";
            // 
            // textBox_soundNumb
            // 
            this.textBox_soundNumb.Location = new System.Drawing.Point(416, 42);
            this.textBox_soundNumb.Name = "textBox_soundNumb";
            this.textBox_soundNumb.Size = new System.Drawing.Size(63, 21);
            this.textBox_soundNumb.TabIndex = 14;
            this.textBox_soundNumb.Text = "0";
            // 
            // label_appDiscern
            // 
            this.label_appDiscern.AutoSize = true;
            this.label_appDiscern.Location = new System.Drawing.Point(332, 71);
            this.label_appDiscern.Name = "label_appDiscern";
            this.label_appDiscern.Size = new System.Drawing.Size(77, 12);
            this.label_appDiscern.TabIndex = 15;
            this.label_appDiscern.Text = "工程识别信息";
            // 
            // textBox_appDiscern
            // 
            this.textBox_appDiscern.Location = new System.Drawing.Point(415, 67);
            this.textBox_appDiscern.Name = "textBox_appDiscern";
            this.textBox_appDiscern.Size = new System.Drawing.Size(128, 21);
            this.textBox_appDiscern.TabIndex = 16;
            this.textBox_appDiscern.Text = "MES1";
            // 
            // ListerPort
            // 
            this.ListerPort.AutoSize = true;
            this.ListerPort.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ListerPort.Location = new System.Drawing.Point(41, 279);
            this.ListerPort.Name = "ListerPort";
            this.ListerPort.Size = new System.Drawing.Size(91, 14);
            this.ListerPort.TabIndex = 17;
            this.ListerPort.Text = "MMI监听端口";
            // 
            // TeachIP
            // 
            this.TeachIP.AutoSize = true;
            this.TeachIP.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TeachIP.Location = new System.Drawing.Point(79, 157);
            this.TeachIP.Name = "TeachIP";
            this.TeachIP.Size = new System.Drawing.Size(53, 14);
            this.TeachIP.TabIndex = 18;
            this.TeachIP.Text = "教员ip";
            // 
            // comboBox_ListerPort
            // 
            this.comboBox_ListerPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ListerPort.FormattingEnabled = true;
            this.comboBox_ListerPort.Items.AddRange(new object[] {
            "Port_1000",
            "Port_2000",
            "Port_3000"});
            this.comboBox_ListerPort.Location = new System.Drawing.Point(142, 276);
            this.comboBox_ListerPort.Name = "comboBox_ListerPort";
            this.comboBox_ListerPort.Size = new System.Drawing.Size(81, 20);
            this.comboBox_ListerPort.TabIndex = 19;
            // 
            // TeachPort
            // 
            this.TeachPort.AutoSize = true;
            this.TeachPort.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TeachPort.Location = new System.Drawing.Point(65, 188);
            this.TeachPort.Name = "TeachPort";
            this.TeachPort.Size = new System.Drawing.Size(67, 14);
            this.TeachPort.TabIndex = 20;
            this.TeachPort.Text = "教员端口";
            // 
            // CpuIP
            // 
            this.CpuIP.AutoSize = true;
            this.CpuIP.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CpuIP.Location = new System.Drawing.Point(79, 218);
            this.CpuIP.Name = "CpuIP";
            this.CpuIP.Size = new System.Drawing.Size(53, 14);
            this.CpuIP.TabIndex = 21;
            this.CpuIP.Text = "主控ip";
            // 
            // CpuPort
            // 
            this.CpuPort.AutoSize = true;
            this.CpuPort.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CpuPort.Location = new System.Drawing.Point(65, 249);
            this.CpuPort.Name = "CpuPort";
            this.CpuPort.Size = new System.Drawing.Size(67, 14);
            this.CpuPort.TabIndex = 22;
            this.CpuPort.Text = "主控端口";
            // 
            // comboBox_TeachPort
            // 
            this.comboBox_TeachPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TeachPort.FormattingEnabled = true;
            this.comboBox_TeachPort.Items.AddRange(new object[] {
            "Port_1000",
            "Port_2000",
            "Port_3000"});
            this.comboBox_TeachPort.Location = new System.Drawing.Point(142, 185);
            this.comboBox_TeachPort.Name = "comboBox_TeachPort";
            this.comboBox_TeachPort.Size = new System.Drawing.Size(81, 20);
            this.comboBox_TeachPort.TabIndex = 25;
            // 
            // comboBox_CpuPort
            // 
            this.comboBox_CpuPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_CpuPort.FormattingEnabled = true;
            this.comboBox_CpuPort.Items.AddRange(new object[] {
            "Port_1000",
            "Port_2000",
            "Port_3000"});
            this.comboBox_CpuPort.Location = new System.Drawing.Point(142, 246);
            this.comboBox_CpuPort.Name = "comboBox_CpuPort";
            this.comboBox_CpuPort.Size = new System.Drawing.Size(81, 20);
            this.comboBox_CpuPort.TabIndex = 26;
            // 
            // LocIpNum
            // 
            this.LocIpNum.AutoSize = true;
            this.LocIpNum.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LocIpNum.Location = new System.Drawing.Point(342, 157);
            this.LocIpNum.Name = "LocIpNum";
            this.LocIpNum.Size = new System.Drawing.Size(67, 14);
            this.LocIpNum.TabIndex = 29;
            this.LocIpNum.Text = "网卡编号";
            // 
            // UinitiyNum
            // 
            this.UinitiyNum.AutoSize = true;
            this.UinitiyNum.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UinitiyNum.Location = new System.Drawing.Point(342, 188);
            this.UinitiyNum.Name = "UinitiyNum";
            this.UinitiyNum.Size = new System.Drawing.Size(67, 14);
            this.UinitiyNum.TabIndex = 31;
            this.UinitiyNum.Text = "系统编号";
            // 
            // textBox_UinitiyNum
            // 
            this.textBox_UinitiyNum.Location = new System.Drawing.Point(416, 185);
            this.textBox_UinitiyNum.Name = "textBox_UinitiyNum";
            this.textBox_UinitiyNum.Size = new System.Drawing.Size(73, 21);
            this.textBox_UinitiyNum.TabIndex = 32;
            this.textBox_UinitiyNum.Text = "9";
            // 
            // comboBox_LocIpNum
            // 
            this.comboBox_LocIpNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_LocIpNum.FormattingEnabled = true;
            this.comboBox_LocIpNum.Items.AddRange(new object[] {
            "0",
            "1"});
            this.comboBox_LocIpNum.Location = new System.Drawing.Point(415, 152);
            this.comboBox_LocIpNum.Name = "comboBox_LocIpNum";
            this.comboBox_LocIpNum.Size = new System.Drawing.Size(74, 20);
            this.comboBox_LocIpNum.TabIndex = 33;
            // 
            // label_
            // 
            this.label_.AutoSize = true;
            this.label_.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_.ForeColor = System.Drawing.Color.DarkRed;
            this.label_.Location = new System.Drawing.Point(391, 91);
            this.label_.Name = "label_";
            this.label_.Size = new System.Drawing.Size(116, 12);
            this.label_.TabIndex = 34;
            this.label_.Text = "此处3个值暂不用改";
            // 
            // ipAddressTextBox_Cpu
            // 
            this.ipAddressTextBox_Cpu.BackColor = System.Drawing.SystemColors.Control;
            this.ipAddressTextBox_Cpu.IsAllowWarn = true;
            this.ipAddressTextBox_Cpu.Location = new System.Drawing.Point(142, 217);
            this.ipAddressTextBox_Cpu.Name = "ipAddressTextBox_Cpu";
            this.ipAddressTextBox_Cpu.Size = new System.Drawing.Size(158, 17);
            this.ipAddressTextBox_Cpu.TabIndex = 28;
            // 
            // ipAddressTextBox_Tech
            // 
            this.ipAddressTextBox_Tech.BackColor = System.Drawing.SystemColors.Control;
            this.ipAddressTextBox_Tech.IsAllowWarn = true;
            this.ipAddressTextBox_Tech.Location = new System.Drawing.Point(142, 155);
            this.ipAddressTextBox_Tech.Name = "ipAddressTextBox_Tech";
            this.ipAddressTextBox_Tech.Size = new System.Drawing.Size(158, 19);
            this.ipAddressTextBox_Tech.TabIndex = 27;
            // 
            // checkBox_DebugMode
            // 
            this.checkBox_DebugMode.AutoSize = true;
            this.checkBox_DebugMode.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_DebugMode.Location = new System.Drawing.Point(345, 216);
            this.checkBox_DebugMode.Name = "checkBox_DebugMode";
            this.checkBox_DebugMode.Size = new System.Drawing.Size(86, 18);
            this.checkBox_DebugMode.TabIndex = 36;
            this.checkBox_DebugMode.Text = "调试模式";
            this.checkBox_DebugMode.UseVisualStyleBackColor = true;
            // 
            // checkBox_NetTurnOn
            // 
            this.checkBox_NetTurnOn.AutoSize = true;
            this.checkBox_NetTurnOn.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_NetTurnOn.Location = new System.Drawing.Point(345, 246);
            this.checkBox_NetTurnOn.Name = "checkBox_NetTurnOn";
            this.checkBox_NetTurnOn.Size = new System.Drawing.Size(116, 18);
            this.checkBox_NetTurnOn.TabIndex = 37;
            this.checkBox_NetTurnOn.Text = "开启网络通讯";
            this.checkBox_NetTurnOn.UseVisualStyleBackColor = true;
            // 
            // Form_MainConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 311);
            this.ControlBox = false;
            this.Controls.Add(this.checkBox_NetTurnOn);
            this.Controls.Add(this.checkBox_DebugMode);
            this.Controls.Add(this.label_);
            this.Controls.Add(this.comboBox_LocIpNum);
            this.Controls.Add(this.textBox_UinitiyNum);
            this.Controls.Add(this.UinitiyNum);
            this.Controls.Add(this.LocIpNum);
            this.Controls.Add(this.ipAddressTextBox_Cpu);
            this.Controls.Add(this.ipAddressTextBox_Tech);
            this.Controls.Add(this.comboBox_CpuPort);
            this.Controls.Add(this.comboBox_TeachPort);
            this.Controls.Add(this.CpuPort);
            this.Controls.Add(this.CpuIP);
            this.Controls.Add(this.TeachPort);
            this.Controls.Add(this.comboBox_ListerPort);
            this.Controls.Add(this.TeachIP);
            this.Controls.Add(this.ListerPort);
            this.Controls.Add(this.textBox_appDiscern);
            this.Controls.Add(this.label_appDiscern);
            this.Controls.Add(this.textBox_soundNumb);
            this.Controls.Add(this.label_soundNumb);
            this.Controls.Add(this.textBox_soundMode);
            this.Controls.Add(this.label_soundMode);
            this.Controls.Add(this.checkBox_NetSelect);
            this.Controls.Add(this.label_NetSelect);
            this.Controls.Add(this.comboBox_app1);
            this.Controls.Add(this.label_firstAppName);
            this.Controls.Add(this.comboBox_appNumb);
            this.Controls.Add(this.label_appNumb);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.comboBox_AppName);
            this.Controls.Add(this.label_appName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_MainConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_MainConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_appName;
        private System.Windows.Forms.ComboBox comboBox_AppName;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label_appNumb;
        private System.Windows.Forms.ComboBox comboBox_appNumb;
        private System.Windows.Forms.Label label_firstAppName;
        private System.Windows.Forms.ComboBox comboBox_app1;
        private System.Windows.Forms.Label label_NetSelect;
        private System.Windows.Forms.CheckBox checkBox_NetSelect;
        private System.Windows.Forms.Label label_soundMode;
        private System.Windows.Forms.TextBox textBox_soundMode;
        private System.Windows.Forms.Label label_soundNumb;
        private System.Windows.Forms.TextBox textBox_soundNumb;
        private System.Windows.Forms.Label label_appDiscern;
        private System.Windows.Forms.TextBox textBox_appDiscern;
        private System.Windows.Forms.Label ListerPort;
        private System.Windows.Forms.Label TeachIP;
        private System.Windows.Forms.ComboBox comboBox_ListerPort;
        private System.Windows.Forms.Label TeachPort;
        private System.Windows.Forms.Label CpuIP;
        private System.Windows.Forms.Label CpuPort;
        private System.Windows.Forms.ComboBox comboBox_TeachPort;
        private System.Windows.Forms.ComboBox comboBox_CpuPort;
        private IPAddressTextBox.IPAddressTextBox ipAddressTextBox_Tech;
        private IPAddressTextBox.IPAddressTextBox ipAddressTextBox_Cpu;
        private System.Windows.Forms.Label LocIpNum;
        private System.Windows.Forms.Label UinitiyNum;
        private System.Windows.Forms.TextBox textBox_UinitiyNum;
        private System.Windows.Forms.ComboBox comboBox_LocIpNum;
        private System.Windows.Forms.Label label_;
        private System.Windows.Forms.CheckBox checkBox_DebugMode;
        private System.Windows.Forms.CheckBox checkBox_NetTurnOn;
    }
}