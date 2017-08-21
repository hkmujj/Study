using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace IPAddressTextBox
{
    [Description("IPAddressTextBox控件!用于输入IP地址")]
    public partial class IPAddressTextBox : UserControl
    {
        #region :::::::::::::::::::: Var ::::::::::::::::::::::::
        private const int DEFAULT_WIDTH_SIZE = 153;
        private const int DEFAULT_HEIGHT_SIZE = 21;

        //整个ip
        private string _text = string.Empty;

        private bool _isAllowWarn = true;
        #endregion

        #region :::::::::::::::::::: Func ::::::::::::::::::::::::
        /// <summary>
        /// 当在当前的textbox中输入数据后开始验证，然后下一个textbox获取鼠标焦点
        /// </summary>
        /// <param name="currentTextBox">当前的textbox</param>
        /// <param name="str">在textbox中数据的字符</param>
        private void Validate(TextBox currentTextBox, string str)
        {
            //如果在第一个firstTextBox中输入的数据超过233，那么为其设置默认字符为233，
            //其他textbox中一旦超过255，那么设置默认值为255
            int maxValue = currentTextBox.Name.Equals("firstTextBox") ? 233 : 255;

            TextBox nextTextBox = currentTextBox;
            switch (currentTextBox.Name)
            {
                case "firstTextBox":
                    nextTextBox = secondTextBox;
                    break;
                case "secondTextBox":
                    nextTextBox = thirdTextBox;
                    break;
                case "thirdTextBox":
                    nextTextBox = fourthTextBox;
                    break;
                case "fourthTextBox":
                    nextTextBox = fourthTextBox;
                    break;
                default:
                    break;
            }

            if (Int32.Parse(str) > maxValue)
            {
                currentTextBox.Text = maxValue.ToString();
                currentTextBox.Focus();

                if (_isAllowWarn)
                {
                    MessageBox.Show(str + " 不是一个有效值。请指定一个介于1和 " + maxValue +
                        " 之间的数值", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                currentTextBox.Text = str;
                if (!nextTextBox.Equals(currentTextBox))
                {
                    nextTextBox.Focus();
                    nextTextBox.SelectAll();
                }
                else
                    currentTextBox.SelectionStart = currentTextBox.Text.Length;
            }
        }

        /// <summary>
        /// 设置IP
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        private void MaskIpAddr(System.Windows.Forms.TextBox textBox, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
            {
                if (e.KeyChar != 8)
                {
                    if (textBox.Text.Length == 2 && e.KeyChar != '.')
                    {
                        Validate(textBox, textBox.Text + e.KeyChar);
                        e.Handled = true;
                    }
                    if (e.KeyChar == '.')
                    {
                        if (textBox.Text.Length == 0)  textBox.Text = string.Empty;
                        else FocusNext(textBox);

                        e.Handled = true;
                    }
                }
                else if (textBox.Text.Length == 0)
                {
                    FocusPreviouse(textBox);
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;//不考虑其他的键输入
            }
        }

        #region :::::::::::::::::: Fun :::::::::::::::::::::::::::
        /// <summary>
        /// 清空所有textBox的文本
        /// </summary>
        public void Clear()
        {
            firstTextBox.Clear();
            secondTextBox.Clear();
            thirdTextBox.Clear();
            fourthTextBox.Clear();
            firstTextBox.Focus();
        }

        /// <summary>
        /// 返回：当输入有误时，返回true;
        /// </summary>
        public bool IsAllowWarn
        {
            get { return _isAllowWarn; }
            set { _isAllowWarn = value; }
        }

        /// <summary>
        /// IP地址字符串
        /// </summary>
        [Browsable(true), DefaultValue("")]
        [Description("IP地址的值")]
        public override string Text
        {
            get
            {
                if (firstTextBox.Text.Length == 0 || secondTextBox.Text.Length == 0 ||
                    thirdTextBox.Text.Length == 0 || fourthTextBox.Text.Length == 0)
                {
                    _text = string.Empty;
                }
                else
                {
                    _text = firstTextBox.Text.Trim() + "." + secondTextBox.Text.Trim() + "." +
                        thirdTextBox.Text.Trim() + "." + fourthTextBox.Text.Trim();
                }
                return _text;
            }

            set
            {
                //在工具箱里拖出控件时，很多比较是不准确的
                _text = value;
                if (!string.IsNullOrEmpty(value))
                {
                    //MessageBox.Show("value的值："+value.ToString()+"。");
                    string[] nums = value.Split('.');
                    if (nums.Length < 4)
                    {
                        MessageBox.Show("指定了一个无效IP地址!", "提示");
                    }
                    else
                    {
                        SetTextBox(nums);
                    }
                }
                else
                {
                    _text = string.Empty;
                    Clear();
                }
            }
        }

        #endregion

        #endregion

        #region ::::::::::::::::::: Logic Func :::::::::::::::::::::
        /// <summary>
        /// 为IpTextBox赋值
        /// </summary>
        /// <param name="nums"></param>
        private void SetTextBox(string[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                string val = nums[i];
                if (Regex.IsMatch(val, @"\d"))
                {
                    int num = Int32.Parse(val);
                    switch (i)
                    {
                        case 0:
                            firstTextBox.Text = num.ToString();
                            break;
                        case 1:
                            secondTextBox.Text = num.ToString();
                            break;
                        case 2:
                            thirdTextBox.Text = num.ToString();
                            break;
                        case 3:
                            fourthTextBox.Text = num.ToString();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 设置上一个textBox的焦点
        /// </summary>
        /// <param name="textBox"></param>
        private void FocusPreviouse(TextBox textBox)
        {
            switch (textBox.Name)
            {
                case "secondTextBox":
                    firstTextBox.Focus();
                    firstTextBox.SelectionStart = firstTextBox.Text.Length;
                    break;
                case "thirdTextBox":
                    secondTextBox.Focus();
                    secondTextBox.SelectionStart = secondTextBox.Text.Length;
                    break;
                case "fourthTextBox":
                    thirdTextBox.Focus();
                    thirdTextBox.SelectionStart = thirdTextBox.Text.Length;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 设置：焦点移动下一个textBox，并处于选中状态
        /// </summary>
        /// <param name="textBox"></param>
        private void FocusNext(TextBox textBox)
        {
            switch (textBox.Name)
            {
                case "firstTextBox":
                    secondTextBox.Focus();
                    secondTextBox.SelectAll();
                    break;
                case "secondTextBox":
                    thirdTextBox.Focus();
                    thirdTextBox.SelectAll();
                    break;
                case "thirdTextBox":
                    fourthTextBox.Focus();
                    fourthTextBox.SelectAll();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 继承Focused
        /// </summary>
        public override bool Focused
        {
            get
            {
                if (firstTextBox.Focused || secondTextBox.Focused || thirdTextBox.Focused || fourthTextBox.Focused)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 默认焦点在firstText
        /// </summary>
        /// <returns></returns>
        public new bool Focus()
        {
            return firstTextBox.Focus();
        }
        #endregion

        #region ::::::::::::::::::: init ::::::::::::::::::::::
        public IPAddressTextBox()
        {
            InitializeComponent();
        }

        private void firstTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            MaskIpAddr(this.firstTextBox, e);
        }

        private void secondTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            MaskIpAddr(this.secondTextBox, e);
        }

        private void thirdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            MaskIpAddr(this.thirdTextBox, e);
        }

        private void fourthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            MaskIpAddr(this.fourthTextBox, e);
        }

        /// <summary>
        /// 禁止当窗体改变时控件也会开发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IPAddressControl_SizeChanged(object sender, EventArgs e)
        {
            this.Width = DEFAULT_WIDTH_SIZE;
            this.Height = DEFAULT_HEIGHT_SIZE;
        }

        /// <summary>
        /// firstTextBox焦点离开时判断输入值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void firstTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(firstTextBox.Text.Trim()) < 1) // 进行验证 
                {
                    MessageBox.Show(firstTextBox.Text.Trim() + " 不是一个有效项目。请指定一个介于 1 和 223 之间的数值。");
                    firstTextBox.Text = "";
                    firstTextBox.Focus();
                    return;
                }
            }
            catch
            {
                firstTextBox.Text = "";
                firstTextBox.Focus();
            }
        }
        #endregion

    }
}
