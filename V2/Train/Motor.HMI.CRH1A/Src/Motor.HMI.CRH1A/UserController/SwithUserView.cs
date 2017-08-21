using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Config.ConfigModel;
using Motor.HMI.CRH1A.Resource;

namespace Motor.HMI.CRH1A.UserController
{
    [GksDataType(DataType.isMMIObjectClass)]
    class SwithUserView : CRH1BaseClass
    {
        // ReSharper disable once InconsistentNaming
        protected LoginKeyboard m_LoginKeyboard;

        public override string GetInfo()
        {
            return "切换用户";
        }

        public override bool Initialize()
        {
            //3
            m_LoginKeyboard = new LoginKeyboard()
            {
                Location = new Point(0, 170),
                InputCompletedEventHandler = (sender, args) =>
                {
                    var pass = PasswordConfigs.Find(f => f.Password == args.Inputstring);
                    if (pass != null)
                    {
                        OnPost(CmdType.SetBoolValue,
                            IndexDescription.OutBoolDescriptionDictionary[OutBooKeys.OB用户输入密码_机械师],
                            pass.UserType == UserType.Mechanician ? 1 : 0, 0);
                        OnPost(CmdType.ChangePage, 1, 0, 0);
                    }
                },
            };

            return true;
        }

        public override void paint(Graphics g)
        {
            m_LoginKeyboard.OnDraw(g);
        }

        protected override bool OnMouseDown(Point point)
        {
            return m_LoginKeyboard.OnMouseDown(point);

        }

        protected override bool OnMouseUp(Point point)
        {
            return m_LoginKeyboard.OnMouseUp(point);
        }
    }
}
