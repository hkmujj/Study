using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsApplication1
{
    public partial class Form_Select : Form
    {
        public Form_Select()
        {
            InitializeComponent();
        }

        private void Config_Modifications_Load(object sender, EventArgs e)
        {
            ShowConfigLoadNumb();
        }

        private int ShowConfigLoadNumb()
        {
            DirectoryInfo theFolder = new DirectoryInfo(@_appPath);

            //查找config.ini
            FileInfo[] mainFileInfo = theFolder.GetFiles();
            foreach (FileInfo config in mainFileInfo)
            {
                if (config.Name.Trim() == "config.ini")
                {
                    _mainConfig = theFolder + "\\" + config.Name.Trim();
                    configlistBox.Items.Add(_mainConfig);
                    _isFoundMainConfig = true;
                    break;
                }
            }
            //没有找到主config文本就退出程序
            if (!_isFoundMainConfig) return -1;


            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            //遍历文件夹
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                this._fileNames.Add(NextFolder.Name);
                if (File.Exists(@_appPath + "\\" + NextFolder.Name +  _subPath))
                {
                    _subConfigFilesName.Add(NextFolder.Name);
                    configlistBox.Items.Add(NextFolder.Name + _subPath);
                }
            }


            return 0;
        }

        private void configlistBox_DoubleClick(object sender, EventArgs e)
        {
            ListBox listBox_config = sender as ListBox;

            if (listBox_config.SelectedItem.ToString().Trim() == _mainConfig)
            {
                MainConfig = new Form_MainConfig(_mainConfig, _subConfigFilesName);
                MainConfig.ShowDialog(this);
            }
            else
            {
                SubConifg = new Form_SubConfig(listBox_config.SelectedItem.ToString().Trim());
                SubConifg.ShowDialog();
            }
        }


        /// <summary>
        /// 应用程序所在目录
        /// </summary>
        private string _appPath = Application.StartupPath;

        /// <summary>
        /// 次级config文件目录
        /// </summary>
        private string _subPath = "\\config\\config.ini";

        /// <summary>
        /// 程序所在文件夹包含的所有文件夹
        /// </summary>
        private List<string> _fileNames = new List<string>();

        /// <summary>
        /// 所有次级conifg文本所在的文件夹名
        /// 即所有工程目录
        /// </summary>
        private List<string> _subConfigFilesName = new List<string>(5);

        public List<string> SubConfigFilesName
        {
            get { return _subConfigFilesName; }
            set { _subConfigFilesName = value; }
        }

        /// <summary>
        /// 主config文件
        /// </summary>
        private string _mainConfig = string.Empty;

        /// <summary>
        /// 是否找到主config文件
        /// </summary>
        private bool _isFoundMainConfig = false;

        private Form_MainConfig MainConfig;
        private Form_SubConfig SubConifg;
    }
}