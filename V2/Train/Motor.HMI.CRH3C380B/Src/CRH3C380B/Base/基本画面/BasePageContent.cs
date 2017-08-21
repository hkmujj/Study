using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;

namespace Motor.HMI.CRH3C380B.Base.基本画面
{
    public abstract class BasePageContent : CommonInnerControlBase
    {
        protected List<CommonInnerControlBase> m_ControlCollection;

        protected List<RectangleF> m_Rectangle;
        protected Dictionary<int, int> m_DynamoDictionary1;
        protected Dictionary<int, int> m_DynamoDictionary2;
        protected List<RectangleF> m_RectsList;

        protected List<string[]> m_BValueAllArrange;
        protected List<string> m_RawBValueArrange;
        protected List<string[]> m_FValueAllArrange;
        protected List<string> m_RawFValueAllArrange;

        protected bool[] m_BValue;
        protected float[] m_FValue;

        protected NeedChangeLength[] m_ChangingRect;

        public string[] BtnStr { protected set; get; }

        protected DMIBasePage SrcObj { get; private set; }

        protected BasePageContent(DMIBasePage srcObj)
        {
            SrcObj = srcObj;
        }



        protected void ResponseBtnEvent()
        {
            if (SrcObj.DMIButton.BtnUpList.Count != 0)
            {
                switch (SrcObj.DMIButton.BtnUpList[0])
                {
                    case 0:
                        break;
                    case 6: //维护
                        SrcObj.append_postCmd(CmdType.ChangePage, 110, 0, 0);
                        break;
                    case 7: //系统
                        SrcObj.append_postCmd(CmdType.ChangePage, 120, 0, 0);
                        break;
                    case 8: //门
                        SrcObj.append_postCmd(CmdType.ChangePage, 130, 0, 0);
                        break;
                    case 9: //ATP系统屏幕
                        SrcObj.append_postCmd(CmdType.ChangePage, 140, 0, 0);
                        break;
                    case 10: //紧急
                        SrcObj.append_postCmd(CmdType.ChangePage, 150, 0, 0);
                        break;
                    case 11: //准备/停放屏幕画面
                        SrcObj.append_postCmd(CmdType.ChangePage, 160, 0, 0);
                        break;
                    case 12: //牵引制动
                        SrcObj.append_postCmd(CmdType.ChangePage, 170, 0, 0);
                        break;
                    case 13: //自动速度控制
                        SrcObj.append_postCmd(CmdType.ChangePage, 180, 0, 0);
                        break;
                    case 14: //开关
                        SrcObj.append_postCmd(CmdType.ChangePage, 190, 0, 0);
                        break;
                }
            }
        }

    }
}