using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{
    public partial class Form_SubConfig : Form
    {
        public Form_SubConfig(string textPath)
        {
            InitializeComponent();
            _configPath = textPath;
            fileName.Text = FileName(_configPath);
            subConfigRecord = new EventRecord(_configPath, ConfigType.SubConfig);
        }

        private string FileName(string configPath)
        {
            string[] strSplit = configPath.Split('\\');
            return strSplit[0];
        }

        private void Form_SubConfig_Load(object sender, EventArgs e)
        {

            if (subConfigRecord.EventRecord_Load())
            {
                foreach (List<string> strList in subConfigRecord.CouldModifyList.Values)
                {
                    if (strList.Count == 2)
                    {
                        if (strList[0] == checkBox_isTop.Text)
                            checkBox_isTop.Checked = (strList[1] == "1") ? true : false;
                        else if (strList[0] == checkBox_isModel.Text)
                            checkBox_isModel.Checked = (strList[1] == "1") ? true : false;
                        else if (strList[0] == label_AppStartID.Text)
                            textBox_AppStartID.Text = strList[1];
                        else if (strList[0] == label_ClassStartID.Text)
                            textBox_ClassStartID.Text = strList[1];
                        else if (strList[0] == label_ClassEndID.Text)
                            textBox_ClassEndID.Text = strList[1];
                        else if (strList[0] == label_Screen_X.Text)
                            textBox_Screen_X.Text = strList[1];
                        else if (strList[0] == label_Screen_Y.Text)
                            textBox_Screen_Y.Text = strList[1];
                        else if (strList[0] == label_Screen_Width.Text)
                            textBox_Screen_Width.Text = strList[1];
                        else if (strList[0] == label_Screen_Hight.Text)
                            textBox_Screen_Hight.Text = strList[1];
                    }
                }
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            subConfigRecord.ModifyConfigContent(checkBox_isTop.Text, checkBox_isTop.Checked ? "1" : "0");
            subConfigRecord.ModifyConfigContent(checkBox_isModel.Text, checkBox_isModel.Checked ? "1" : "0");
            subConfigRecord.ModifyConfigContent(label_AppStartID.Text, textBox_AppStartID.Text);
            subConfigRecord.ModifyConfigContent(label_ClassStartID.Text, textBox_ClassStartID.Text);
            subConfigRecord.ModifyConfigContent(label_ClassEndID.Text, textBox_ClassEndID.Text);
            subConfigRecord.ModifyConfigContent(label_Screen_X.Text, textBox_Screen_X.Text);
            subConfigRecord.ModifyConfigContent(label_Screen_Y.Text, textBox_Screen_Y.Text);
            subConfigRecord.ModifyConfigContent(label_Screen_Width.Text, textBox_Screen_Width.Text);
            subConfigRecord.ModifyConfigContent(label_Screen_Hight.Text, textBox_Screen_Hight.Text);

            if (subConfigRecord.SaveToTxtFile())
            {
                MessageBox.Show("修改成功！");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// config目录
        /// </summary>
        private string _configPath = string.Empty;

        private EventRecord subConfigRecord;

        private bool TextBoxChecking(string textBox_Str)
        {
            if (textBox_Str.Length > 0)
            {
                int textBox_int = 0;
                if (!int.TryParse(textBox_Str, out textBox_int))
                {
                    MessageBox.Show("输入错误，请输入整数！");
                    return false;
                }
            }
            return true;
        }

        private void textBox_AppStartID_TextChanged(object sender, EventArgs e)
        {
            if (!TextBoxChecking(textBox_AppStartID.Text))
                textBox_AppStartID.Text = string.Empty;
        }

        private void textBox_ClassStartID_TextChanged(object sender, EventArgs e)
        {
            if (!TextBoxChecking(textBox_ClassStartID.Text))
                textBox_ClassStartID.Text = string.Empty;

        }

        private void textBox_ClassEndID_TextChanged(object sender, EventArgs e)
        {
            if (!TextBoxChecking(textBox_ClassEndID.Text))
                textBox_ClassEndID.Text = string.Empty;

        }

        private void textBox_Screen_X_TextChanged(object sender, EventArgs e)
        {
            if (!TextBoxChecking(textBox_Screen_X.Text))
                textBox_Screen_X.Text = string.Empty;

        }

        private void textBox_Screen_Y_TextChanged(object sender, EventArgs e)
        {
            if (!TextBoxChecking(textBox_Screen_Y.Text))
                textBox_Screen_Y.Text = string.Empty;

        }

        private void textBox_Screen_Width_TextChanged(object sender, EventArgs e)
        {
            if (!TextBoxChecking(textBox_Screen_Width.Text))
                textBox_Screen_Width.Text = string.Empty;

        }

        private void textBox_Screen_Hight_TextChanged(object sender, EventArgs e)
        {
            if (!TextBoxChecking(textBox_Screen_Hight.Text))
                textBox_Screen_Hight.Text = string.Empty;

        }

    }
}
