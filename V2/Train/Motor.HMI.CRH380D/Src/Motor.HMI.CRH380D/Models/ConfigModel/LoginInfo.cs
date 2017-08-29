using System.Diagnostics;
using Excel.Interface;

namespace Motor.HMI.CRH380D.Models.ConfigModel
{
    [ExcelLocation("登陆密码.xls", "Login")]
    [DebuggerDisplay("账号={id}, 密码={PassWord}")]
    public class LoginInfo : ISetValueProvider
    {
        [ExcelField("账号", true)]
        public int id { set; get; }

        [ExcelField("密码")]
        public string PassWord { set; get; }
        

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}