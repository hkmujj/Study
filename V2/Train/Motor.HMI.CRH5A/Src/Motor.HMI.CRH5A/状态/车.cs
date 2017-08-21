using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using Motor.HMI.CRH5A.Resource;
using Motor.HMI.CRH5A.Resource.Images;
using Motor.HMI.CRH5A.底层共用;
using Motor.HMI.CRH5A.底层共用.RectView;

namespace Motor.HMI.CRH5A.Staus
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Car : CRH5ABase
    {
        private int m_CurrentIndex;
       
        private List<RectangleF> m_RectsList;
        private int m_ViewId;
        private const int MaxIndex = 16;
        //2
        /// <summary>
        /// 是否为8-16车
        /// </summary>
        public bool IsCar8To16 { get; set; }

        /// <summary>
        /// 当前选中的
        /// </summary>
        public int CurrentSelectedCarIndex { set; get; }

        public override string GetInfo()
        {
            return "车对象";
        }

        //6
        public override bool Initalize()
        {
            return Init();
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            m_ViewId = nParaB;
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                CurrentSelectedCarIndex = 1;
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();
            DrawCar(g);
        }

        private void GetValue()
        {
            if (ButtonsScreen.BtnState != null && !ButtonsScreen.BtnState.IsPress)
            {
            }
            if (TitleScreen.ChangeLength && !TitleScreen.OldChangeLength)
            {
                m_CurrentIndex = m_CurrentIndex / 2;
            }
            else if (!TitleScreen.ChangeLength && TitleScreen.OldChangeLength)
            {
                m_CurrentIndex = 0;
            }


            m_IsDraw16 = TitleScreen.ChangeLength && TitleScreen.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling;


        }
        /// <summary>
        /// 是否需要画>>2
        /// </summary>
        private bool m_IsDraw16;
        private void DrawCar(Graphics g)
        {
            //车
            g.DrawImage(CarImage(), m_RectsList[17]);
            //车辆使能
            if (GetInBoolValue(InBoolKeys.InB头尾车信号头车482) || GetInBoolValue(InBoolKeys.InB头尾车信号尾车483))
                g.DrawString("*", FontsItems.DefaultFont, SolidBrushsItems.GreenBrush1,
                    DriverLocation(GetInBoolValue(InBoolKeys.InB头尾车信号头车482), GetInBoolValue(InBoolKeys.InB头尾车信号尾车483)),
                    FontsItems.TheAlignment(FontRelated.靠左));
            g.DrawString("←",
               FontsItems.DefaultFont,
               SolidBrushsItems.GreenBrush1,
               m_RectsList[12],
               FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString("→",
             FontsItems.DefaultFont,
             SolidBrushsItems.GreenBrush1,
             m_RectsList[13],
             FontsItems.TheAlignment(FontRelated.居中));

            DrawTrainDirection(g);

            if (true)
            {
                g.DrawImage(ImagesResouce.三角, GetRectangle());
            }
            if (m_IsDraw16)
            {
                g.DrawString(">>", FontsItems.DefaultFont, SolidBrushsItems.YellowBrush2, m_RectsList[42]);
                g.DrawString(IsCar8To16 ? "1" : "2", FontsItems.DefaultFont, SolidBrushsItems.YellowBrush2, m_RectsList[43]);
            }
            int index;
            if (TitleScreen.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling || !TitleScreen.ChangeLength)
            {
                index = MaxIndex / 2;
            }
            else
            {
                index = MaxIndex;
            }


            g.DrawString(string.Format("{0}/{1}", CurrentSelectedCarIndex, index), FontsItems.DefaultFont, SolidBrushsItems.YellowBrush2, m_RectsList[16]);
        }

        private void DrawTrainDirection(Graphics g)
        {
            var td = TrainDirection.Zero;
            if (GetInBoolValue(InBoolKeys.InB向前))
            {
                td = TrainDirection.Forward;
            }
            if (GetInBoolValue(InBoolKeys.InB向后))
            {
                td = TrainDirection.Backward;
            }
            switch (td)
            {
                case TrainDirection.Zero:
                    break;
                case TrainDirection.Forward:
                    g.DrawImage(ImagesResouce.受电弓_左, GetBowRect());
                    break;
                case TrainDirection.Backward:
                    g.DrawImage(ImagesResouce.受电弓_右, GetBowRect());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private RectangleF DriverLocation(bool firstDue, bool lastDue)
        {
            if (firstDue)
            {
                if (TitleScreen.ChangeLength)
                {
                    return TitleScreen.TrainInside ? m_RectsList[37] : m_RectsList[36];
                }
                return TitleScreen.TrainInside ? m_RectsList[35] : m_RectsList[34];
            }
            if (lastDue)
            {
                if (TitleScreen.ChangeLength)
                {
                    return TitleScreen.TrainInside ? m_RectsList[36] : m_RectsList[37];
                }
                return TitleScreen.TrainInside ? m_RectsList[34] : m_RectsList[35];
            }
            return m_RectsList[35];
        }

        private Image CarImage()
        {
            if (TitleScreen.ChangeLength)
            {
                if (TitleScreen.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling)
                {
                    return TitleScreen.TrainInside ? ImagesResouce._8车换端 : ImagesResouce._8车;
                }
                return TitleScreen.TrainInside ? ImagesResouce._16车换端 : ImagesResouce._16车;
            }
            if (TitleScreen.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling)
            {
                return TitleScreen.TrainInside ? ImagesResouce._8车换端 : ImagesResouce._8车;
            }
            return TitleScreen.TrainInside ? ImagesResouce._8车双组显示换端 : ImagesResouce._8车双组显示;
        }

        private RectangleF GetBowRect()
        {
            if (TitleScreen.ChangeLength)
            {
                if (GetInBoolValue(InBoolKeys.InB3车受电弓绿558) || GetInBoolValue(InBoolKeys.InB6车受电弓绿561))
                    return TitleScreen.TrainInside ? m_RectsList[39] : m_RectsList[38];
                if (GetInBoolValue(InBoolKeys.InB11车受电弓绿) || GetInBoolValue(InBoolKeys.InB14车受电弓绿))
                    return TitleScreen.TrainInside ? m_RectsList[38] : m_RectsList[39];
            }
            return m_RectsList[40];
        }
        /// <summary>
        /// 获取车上三角位置
        /// </summary>
        /// <returns></returns>
        private RectangleF GetRectangle()
        {

            return TitleScreen.TrainInside ? m_RectsList[33 - m_CurrentIndex] : m_RectsList[18 + m_CurrentIndex];
        }

        private bool Init()
        {
            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.TitleScreen);
            return true;
        }

        public void MoveNext()
        {
            if (m_CurrentIndex + (TitleScreen.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling ? 2 : 1) >= MaxIndex)
            {
                return;
            }

            if (TitleScreen.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling)
            {

                if (TitleScreen.TrainInside)
                {
                    if (m_CurrentIndex != 0)
                    {
                        m_CurrentIndex -= 2;
                        return;
                    }
                }
                m_CurrentIndex += 2;
            }
            else
            {
                if (TitleScreen.TrainInside)
                {
                    if (m_CurrentIndex != 0)
                    {
                        m_CurrentIndex--;
                        return;
                    }

                }
                m_CurrentIndex++;
            }

        }

        public void MoveLast()
        {
            if (m_CurrentIndex == 0)
            {
                return;
            }
            if (TitleScreen.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling)
            {
                if (TitleScreen.TrainInside)
                {
                    if (m_CurrentIndex + (TitleScreen.ChangeLength ? 1 : 2) < MaxIndex)
                    {
                        m_CurrentIndex += 2;
                        return;
                    }
                }
                m_CurrentIndex -= 2;
            }
            else
            {
                if (TitleScreen.TrainInside)
                {
                    if (m_CurrentIndex + (TitleScreen.ChangeLength ? 1 : 2) < MaxIndex)
                    {
                        m_CurrentIndex++;
                        return;
                    }
                }
                m_CurrentIndex--;
            }
        }
    }
}
