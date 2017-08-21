using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{
    public partial class Form_MainConfig : Form
    {
        #region :::::::::::::::::::: init ::::::::::::::::::::::::::
        public Form_MainConfig(string textPath, List<string> filesName)
        {
            _configPath = textPath;
            AppFilesName = filesName;
            mainConfigRecord = new EventRecord(_configPath, ConfigType.MainConfig);
            InitializeComponent();
        }

        private void Form_MainConfig_Load(object sender, EventArgs e)
        {
            if (mainConfigRecord.EventRecord_Load())
            {
                if (AppFilesName.Count > 0)
                {
                    foreach (string s in AppFilesName)
                    {
                        comboBox_AppName.Items.Add(s);
                        comboBox_app1.Items.Add(s);
                    }
                }
                var appName = mainConfigRecord.FindConfigContent(label_appName.Text);
                comboBox_AppName.Text = AppFilesName.Contains(appName) ? appName : AppFilesName[0];

                comboBox_app1.Text = AppFilesName[0];//工程1
                //将所有次级config文本都创建一遍
                for (int i = 0; i < AppFilesName.Count; i++)
                {
                    EventRecord tmpEven = new EventRecord(@_appPath + "\\" + AppFilesName[i] + _subPath, ConfigType.SubConfig);
                    tmpEven.EventRecord_Load();
                    subConfigRecordDic.Add(AppFilesName[i], tmpEven);
                }

                #region :::::::::::::::::: config赋值 ::::::::::::::::::::::
                foreach (List<string> strList in mainConfigRecord.CouldModifyList.Values)
                {
                    if (strList.Count == 2)
                    {
                        //启用的工程数量
                        if (strList[0] == label_appNumb.Text)
                        {
                            Evaluation(comboBox_appNumb, strList[1]);
                        }
                        //工程1
                        else if (strList[0] == label_firstAppName.Text)
                        {
                            foreach (string tmp in AppFilesName)
                            {
                                if (strList[1] == tmp)
                                {
                                    comboBox_app1.Text = tmp;
                                    break;
                                }
                            }
                        }
                        //网络选择
                        else if (strList[0] == label_NetSelect.Text)
                            checkBox_NetSelect.Checked = (strList[1] == "1") ? true : false;
                        //声音模块
                        else if (strList[0] == label_soundMode.Text)
                            textBox_soundMode.Text = strList[1];
                        //声音个数
                        else if (strList[0] == label_soundNumb.Text)
                            textBox_soundNumb.Text = strList[1];
                        //工程识别信息
                        else if (strList[0] == label_appDiscern.Text)
                            textBox_appDiscern.Text = strList[1];
                        //教员ip
                        else if (strList[0] == TeachIP.Name)
                            ipAddressTextBox_Tech.Text = strList[1];
                        //教员端口
                        else if (strList[0] == TeachPort.Name)
                        {
                            Evaluation(comboBox_TeachPort, strList[1]);
                        }
                        //主控ip
                        else if (strList[0] == CpuIP.Name)
                            ipAddressTextBox_Cpu.Text = strList[1];
                        //主控端口
                        else if (strList[0] == CpuPort.Name)
                            Evaluation(comboBox_CpuPort, strList[1]);
                        //MMI监听端口
                        else if (strList[0] == ListerPort.Name)
                            Evaluation(comboBox_ListerPort, strList[1]);
                        //网卡编号
                        else if (strList[0] == LocIpNum.Name)
                        {
                            foreach (string tmp in comboBox_LocIpNum.Items)
                            {
                                if (strList[1] == tmp)
                                {
                                    comboBox_LocIpNum.Text = tmp;
                                    break;
                                }
                            }
                        }
                        //系统编号
                        else if (strList[0] == UinitiyNum.Name)
                            textBox_UinitiyNum.Text = strList[1];
                    }
                }
                #endregion

                #region ::::::::::::::::: 次级config的调试 :::::::::::::::::
                subConfigRecord = subConfigRecordDic[comboBox_app1.Text];
                foreach (List<string> strList in subConfigRecord.CouldModifyList.Values)
                {
                    if (strList.Count == 2)
                    {
                        //调试模式
                        if (strList[0] == checkBox_DebugMode.Text)
                            checkBox_DebugMode.Checked = (strList[1] == "1") ? true : false;
                        //开启网络通讯
                        else if (strList[0] == checkBox_NetTurnOn.Text)
                            checkBox_NetTurnOn.Checked = (strList[1] == "1") ? true : false;
                    }
                }
                #endregion

                LoadSucceed = true;
            }
        }

        /// <summary>
        /// 赋值方法
        /// </summary>
        /// <param name="comboBox">下拉框参数</param>
        /// <param name="str">从文本取出的值</param>
        private void Evaluation(ComboBox comboBox, String str)
        {
            foreach (string tmp in comboBox.Items)
            {
                if (str == tmp)
                {
                    comboBox.Text = str;
                    break;
                }
            }
        }
        #endregion

        #region :::::::::::::::::::: StateChange :::::::::::::::::::::::
        private void button_OK_Click(object sender, EventArgs e)
        {
            //工程名称
            mainConfigRecord.ModifyConfigContent(label_appName.Text, comboBox_AppName.Text);
            //工程数量
            mainConfigRecord.ModifyConfigContent(label_appNumb.Text, comboBox_appNumb.Text);
            //工程1
            mainConfigRecord.ModifyConfigContent(label_firstAppName.Text, comboBox_app1.Text);
            //网络选择
            mainConfigRecord.ModifyConfigContent(label_NetSelect.Text, checkBox_NetSelect.Checked ? "1" : "0");
            ///////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////
            //声音模块
            mainConfigRecord.ModifyConfigContent(label_soundMode.Text, textBox_soundMode.Text);
            //声音个数
            mainConfigRecord.ModifyConfigContent(label_soundNumb.Text, textBox_soundNumb.Text);
            //工程识别信息
            mainConfigRecord.ModifyConfigContent(label_appDiscern.Text, textBox_appDiscern.Text);
            ///////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////
            //教员ip
            mainConfigRecord.ModifyConfigContent(TeachIP.Name, ipAddressTextBox_Tech.Text);
            subConfigRecord.ModifyConfigContent("目标IP", ipAddressTextBox_Tech.Text);
            //教员端口
            mainConfigRecord.ModifyConfigContent(TeachPort.Name, comboBox_TeachPort.Text);
            subConfigRecord.ModifyConfigContent("目标端口", comboBox_TeachPort.Text);
            //主控ip
            mainConfigRecord.ModifyConfigContent(CpuIP.Name, ipAddressTextBox_Cpu.Text);
            subConfigRecord.ModifyConfigContent("目标IP2", ipAddressTextBox_Cpu.Text);
            //主控端口
            mainConfigRecord.ModifyConfigContent(CpuPort.Name, comboBox_CpuPort.Text);
            subConfigRecord.ModifyConfigContent("目标2端口", comboBox_CpuPort.Text);
            //MMI监听端口
            mainConfigRecord.ModifyConfigContent(ListerPort.Name, comboBox_ListerPort.Text);
            subConfigRecord.ModifyConfigContent("监听端口", comboBox_ListerPort.Text);
            //网卡编号
            mainConfigRecord.ModifyConfigContent(LocIpNum.Name, comboBox_LocIpNum.Text);
            //系统编号
            mainConfigRecord.ModifyConfigContent(UinitiyNum.Name, textBox_UinitiyNum.Text);
            //调试模式
            subConfigRecord.ModifyConfigContent(checkBox_DebugMode.Text, checkBox_DebugMode.Checked ? "1" : "0");
            //开启网络通讯
            subConfigRecord.ModifyConfigContent(checkBox_NetTurnOn.Text, checkBox_NetTurnOn.Checked ? "1" : "0");
            //////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////

            if (mainConfigRecord.SaveToTxtFile() && subConfigRecord.SaveToTxtFile())
            {
                MessageBox.Show("修改成功！");
                this.Close();
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region ::::::::::::::::::::: Var ::::::::::::::::::::::::
        /// <summary>
        /// 应用程序所在目录
        /// </summary>
        private string _appPath = Application.StartupPath;

        /// <summary>
        /// 次级config文件目录
        /// </summary>
        private string _subPath = "\\config\\config.ini";

        /// <summary>
        /// config目录
        /// </summary>
        private string _configPath = string.Empty;

        private List<string> AppFilesName;

        private EventRecord mainConfigRecord;

        private EventRecord subConfigRecord;

        private Dictionary<string, EventRecord> subConfigRecordDic = new Dictionary<string, EventRecord>(5);

        private string[] portName = { "Port1000", "Port_2000", "Port_3000" };

        private bool LoadSucceed = false;
        #endregion

        private void comboBox_AppName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadSucceed)
            {
                #region ::::::::::::::::: 次级config的调试 :::::::::::::::::
                subConfigRecord = subConfigRecordDic[comboBox_AppName.Text];
                foreach (List<string> strList in subConfigRecord.CouldModifyList.Values)
                {
                    if (strList.Count == 2)
                    {
                        //调试模式
                        if (strList[0] == checkBox_DebugMode.Text)
                            checkBox_DebugMode.Checked = (strList[1] == "1") ? true : false;
                        //开启网络通讯
                        else if (strList[0] == checkBox_NetTurnOn.Text)
                            checkBox_NetTurnOn.Checked = (strList[1] == "1") ? true : false;
                    }
                }
                #endregion
            }
        }
    }
}
