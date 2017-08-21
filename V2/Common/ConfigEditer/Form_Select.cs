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

            //����config.ini
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
            //û���ҵ���config�ı����˳�����
            if (!_isFoundMainConfig) return -1;


            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            //�����ļ���
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
        /// Ӧ�ó�������Ŀ¼
        /// </summary>
        private string _appPath = Application.StartupPath;

        /// <summary>
        /// �μ�config�ļ�Ŀ¼
        /// </summary>
        private string _subPath = "\\config\\config.ini";

        /// <summary>
        /// ���������ļ��а����������ļ���
        /// </summary>
        private List<string> _fileNames = new List<string>();

        /// <summary>
        /// ���дμ�conifg�ı����ڵ��ļ�����
        /// �����й���Ŀ¼
        /// </summary>
        private List<string> _subConfigFilesName = new List<string>(5);

        public List<string> SubConfigFilesName
        {
            get { return _subConfigFilesName; }
            set { _subConfigFilesName = value; }
        }

        /// <summary>
        /// ��config�ļ�
        /// </summary>
        private string _mainConfig = string.Empty;

        /// <summary>
        /// �Ƿ��ҵ���config�ļ�
        /// </summary>
        private bool _isFoundMainConfig = false;

        private Form_MainConfig MainConfig;
        private Form_SubConfig SubConifg;
    }
}