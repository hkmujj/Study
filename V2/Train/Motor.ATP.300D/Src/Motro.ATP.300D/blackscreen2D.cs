using System;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._300D.Common;

namespace Motor.ATP._300D
{
    //[GksDataType(DataType.isMMIObjectClass)]
    internal class Blackscreen2D : ATPBase
    {
        private ViewType m_LastViewIndex;

        private ViewType m_CurrentViewIndex;

        public Predicate<Blackscreen2D> IsBlackScreen;

        private readonly baseClass m_ObjectClass;

        public Blackscreen2D(baseClass objectClass)
        {
            m_ObjectClass = objectClass;
        }

        public override void paint(Graphics g)
        {
            if (IsBlackScreen != null && IsBlackScreen(this))
            {
                m_ObjectClass.append_postCmd(CmdType.ChangePage, 44, 0, 0);
            }
            else
            {
                if (m_CurrentViewIndex != ViewType.BlackScreen && m_CurrentViewIndex != ViewType.BlackScreen44)
                {
                    return;
                }

                // TODO 有用?
                //for (int j = 0; j < 250; j++)
                //{
                //    m_ObjectClass.append_postCmd(CmdType.SetFloatValue, j, 0, 0);
                //}
                if (m_LastViewIndex < ViewType.Main)
                {
                    m_LastViewIndex = ViewType.DriverID;
                }
                Background2D.bfirstshuju = false;
                StrDrivData2D.breal = false;
                DMIMainMenu2D.Popuptxt = " ";
                Background2D.bfirstcheci = false;
                Background2D.bfirstsijihao = false;
                m_ObjectClass.append_postCmd(CmdType.ChangePage, 101, 0, 0);
            }
        }

        public override bool Initalize()
        {
            if (m_ObjectClass.UIObj.ParaList.Count < 0)
            {
                return false;
            }
            
            if (UIObj.InBoolList.Count != 2)
            {
                LogMgr.Warn(string.Format("in bool cout ({0}) must = 2 of {1}", UIObj.InBoolList.Count, GetType()));
                return false;
            }
           
            return true;
        }

        public override string GetInfo()
        {
            return "黑屏";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            m_CurrentViewIndex = (ViewType)nParaB;

            if (nParaA == 2)
            {
                switch ((ViewType)nParaB)
                {
                    case (ViewType)44:
                        m_LastViewIndex = (ViewType)nParaC;
                        break;
                }
            }
        }
    }
}