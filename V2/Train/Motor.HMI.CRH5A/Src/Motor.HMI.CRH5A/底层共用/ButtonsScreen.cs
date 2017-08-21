using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH5A.Resource;
using Motor.HMI.CRH5A.Resource.Images;

namespace Motor.HMI.CRH5A.底层共用
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ButtonsScreen : CRH5ABase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "边框按钮";

        }

        //6
        public override bool Initalize()
        {
            return Init();
        }
        #endregion

        public override void Paint(Graphics g)
        {
            GetValue();

            g.DrawImage(m_ImgBackground, m_RectsList[0]);

            for (int index = 0; index < m_TnIsDown.Length; index++)
            {
                if (m_TnIsDown[index])
                {
                    g.DrawRectangle(PenItems.GetThePen(SolidBrushsItems.WhiteBrush, 3f), Rectangle.Round(m_RectsList[index + 1]));
                }
            }
        }

        public override bool OnMouseDown(Point nPoint)
        {
            int btnIndex = 0;
            for (; btnIndex < m_TnIsDown.Length; btnIndex++)
            {
                if (!m_TnRegionArr[btnIndex].IsVisible(nPoint)) continue;
                if (!m_BtnList.Contains(btnIndex))
                {
                    m_TnIsDown[btnIndex] = true;
                    m_BtnList.Add(btnIndex);
                    BtnState = new BtnInfo(btnIndex);
                }
                break;
            }
            return true;
        }

        public override bool OnMouseUp(Point nPoint)
        {
            int btnIndex = 0;
            for (; btnIndex < m_TnIsDown.Length; btnIndex++)
            {
                if (!m_TnRegionArr[btnIndex].IsVisible(nPoint)) continue;
                if (m_BtnList.Contains(btnIndex))
                {
                    m_BtnList.RemoveAll(r => r == btnIndex);
                    m_TnIsDown[btnIndex] = false;
                    BtnState = null;
                }
            }
            return true;
        }

        //按钮状态获取
        private void GetValue()
        {
            for (int i = 0; i < 25; i++)
            {
                var value = ScreenIdentification == ScreenIdentification.ScreenTd
                    ? GetInBoolValue(InTDLogicName[i])
                    : GetInBoolValue(InTSLogicName[i]);
                //var values = BoolList;
                //var index = UIObj.InBoolList[i];
                if (value && !m_BtnList.Contains(i))//判断按钮按下
                {
                    m_BtnList.Add(i);
                    m_TnIsDown[i % 25] = true;
                    BtnState = new BtnInfo(i % 25);
                    break;
                }
                if (m_BtnList.Contains(i) && !value)//判断按钮弹起
                {
                    m_BtnList.RemoveAll(r => r == i);
                    m_TnIsDown[i % 25] = false;
                    BtnState = null;
                    break;
                }
            }
        }

        private bool Init()
        {
            m_ImgBackground = ImagesResouce.硬件按钮;

            m_RectsList = Coordinate.RectangleFLists(ViewIDName.ButtonsScreen);

            m_TnRegionArr = new Region[25];
            m_TnIsDown = new bool[25];


            for (int i = 0; i < m_TnRegionArr.Length; i++)
            {
                m_TnRegionArr[i] = new Region(m_RectsList[i + 1]);
            }
            return true;
        }

        private readonly string[] InTDLogicName = new[]
        {
            InBoolKeys.InBTD诊断信息键509,
            InBoolKeys.InBTD法文选择键510,
            InBoolKeys.InBTD德文选择键511,
            InBoolKeys.InBTD意大利语选择键512,
            InBoolKeys.InBTD软启键1513,
            InBoolKeys.InBTD软启键2514,
            InBoolKeys.InBTD维护控制键515,
            InBoolKeys.InBTD仪器键516,
            InBoolKeys.InBTDTIS键517,
            InBoolKeys.InBTD信息键518,
            InBoolKeys.InBTD取消键519,
            InBoolKeys.InBTD上翻键520,
            InBoolKeys.InBTD下翻键521,
            InBoolKeys.InBTD回车键522,
            InBoolKeys.InBTD全停键523,
            InBoolKeys.InBTD1524,
            InBoolKeys.InBTD2525,
            InBoolKeys.InBTD3526,
            InBoolKeys.InBTD4527,
            InBoolKeys.InBTD5528,
            InBoolKeys.InBTD6529,
            InBoolKeys.InBTD7530,
            InBoolKeys.InBTD8531,
            InBoolKeys.InBTD9532,
            InBoolKeys.InBTD0533,
        };

        private readonly string[] InTSLogicName = new string[]
        {
            InBoolKeys.InBTS诊断信息键484,
            InBoolKeys.InBTS法文选择键485,
            InBoolKeys.InBTS德文选择键486,
            InBoolKeys.InBTS意大利语选择键487,
            InBoolKeys.InBTS软启键1488,
            InBoolKeys.InBTS软启键2489,
            InBoolKeys.InBTS维护控制键490,
            InBoolKeys.InBTS仪器键491,
            InBoolKeys.InBTSTIS键492,
            InBoolKeys.InBTS信息键493,
            InBoolKeys.InBTS取消键494,
            InBoolKeys.InBTS上翻键495,
            InBoolKeys.InBTS下翻键496,
            InBoolKeys.InBTS回车键497,
            InBoolKeys.InBTS全停键498,
            InBoolKeys.InBTS1499,
            InBoolKeys.InBTS2500,
            InBoolKeys.InBTS3501,
            InBoolKeys.InBTS4502,
            InBoolKeys.InBTS5503,
            InBoolKeys.InBTS6504,
            InBoolKeys.InBTS7505,
            InBoolKeys.InBTS8506,
            InBoolKeys.InBTS9507,
            InBoolKeys.InBTS0508,
        };
        private Image m_ImgBackground;
        private List<RectangleF> m_RectsList;
        private Region[] m_TnRegionArr;
        private bool[] m_TnIsDown;
        public BtnInfo BtnState;

        /// <summary> 
        /// 存放所有按着的按钮编号
        /// </summary>
        private readonly List<int> m_BtnList = new List<int>();

    }
}
