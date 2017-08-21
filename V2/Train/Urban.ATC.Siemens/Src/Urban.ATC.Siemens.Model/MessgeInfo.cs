using Urban.ATC.Domain.Interface;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Siemens.Model
{
    public class MessgeInfo : IMessgeInfo
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sPara">内容</param>
        /// <param name="iPara">逻辑号</param>
        /// <param name="inPara">等级</param>
        public MessgeInfo(string sPara, int iPara, InfoLevl inPara)
        {
            Content = sPara;
            LogicID = iPara;
            Level = inPara;
            IsConfirm = false;
        }

        public MessgeInfo()
            : this("", -1, InfoLevl.Yellow)
        {
        }

        public string Content { get; private set; }
        public int LogicID { get; private set; }
        public InfoLevl Level { get; private set; }
        public bool IsConfirm { get; private set; }

        public void Confirm()
        {
            IsConfirm = true;
        }
    }
}