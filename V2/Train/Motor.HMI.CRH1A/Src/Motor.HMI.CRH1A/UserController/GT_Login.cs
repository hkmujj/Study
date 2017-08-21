using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Config.ConfigModel;

namespace Motor.HMI.CRH1A.UserController
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class GT_Login : SwithUserView
    {
        readonly GT_MenuTitle m_Title = new GT_MenuTitle("登录");//菜单标题           

        //private LoginKeyboard m_LoginKeyboard;

        /// <summary>
        /// 密 码 给固定值
        /// </summary>
        private List<string> m_Passwords;

        public override string GetInfo()
        {
            return "登录";
        }

        public override bool Initialize()
        {
            m_Passwords =
                GlobalInfo.Instance.CRH1ADetailConfig.PasswordConfigs.FindAll(f => f.UserType == UserType.Driver)
                    .Select(s => s.Password)
                    .ToList();
            //3
            m_LoginKeyboard = new LoginKeyboard()
            {
                Location = new Point(0,150),
                InputCompletedEventHandler = (sender, args) =>
                {
                    if (m_Passwords.Contains(args.Inputstring))
                    {
                        OnPost(CmdType.ChangePage, 1, 0, 0);
                        GT_GalobalFaultManage.Instance.CanResponseFaultA = true;
                    }
                }
            };

            return true;
        }

        protected override void NavigateFrom(ViewConfig fromOhter)
        {
            if (fromOhter == ViewConfig.Start || fromOhter == ViewConfig.BlackScreen)
            {
                GlobalEvent.Instance.OnStartCompleted();
            }
        }

        public override void paint(Graphics g)
        {
            base.paint(g);

            //绘制菜单标题
            m_Title.OnDraw(g);

        }

    }
}
