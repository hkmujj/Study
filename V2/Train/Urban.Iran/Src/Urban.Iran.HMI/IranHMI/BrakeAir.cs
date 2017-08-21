using System;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Statues;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Domain;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BrakeAir : HMIBase
    {
        private Rectangle[] m_RectArr;
        private Rectangle[] m_TxtRectArr;
        private Rectangle[] m_BarkeRectArr;
        private Rectangle[] m_AirRect;
        private Rectangle[] m_TxtRects;
        private Point[] m_LinePoints;
        private Bitmap[] m_BaImgArr;
        private FjButtonEx m_LegendBtn;
        private byte[] m_BreakStatus;
        private byte[] m_EmBreakStatus;
        private byte[] m_ParBreakeStatus;
        private byte[] m_AirCompressorStatus;
        private float[] m_FValue;
        private int[] m_FStart;
        private int[] m_BStart;

        private ITrain m_Train;

        private Image GetBogiesBrakingImage(IBraking braking)
        {
            switch (braking.BrakingState)
            {
                case BrakingState.Unkown:
                    return m_BaImgArr[4];
                    break;
                case BrakingState.Apply:
                    return m_BaImgArr[3];
                    break;
                case BrakingState.Relase:
                    return m_BaImgArr[2];
                    break;
                case BrakingState.Fault:
                    return m_BaImgArr[0];
                    break;
                case BrakingState.CutOut:
                    return m_BaImgArr[1];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return null;
        }

        private Image GetEmergencyBrakingImage(IBraking braking)
        {
            switch (braking.BrakingState)
            {
                case BrakingState.Unkown:
                    return m_BaImgArr[7];
                    break;
                case BrakingState.Apply:
                    return m_BaImgArr[5];
                    break;
                case BrakingState.Relase:
                    return m_BaImgArr[6];
                    break;
                case BrakingState.Fault:
                    return null;
                    break;
                case BrakingState.CutOut:
                    return null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return null;
        }

        private Image GetParkingBrakingImage(IBraking braking)
        {
            switch (braking.BrakingState)
            {
                case BrakingState.Unkown :
                    return null;
                    break;
                case BrakingState.Apply :
                    return m_BaImgArr[11];
                    break;
                case BrakingState.Relase :
                    return m_BaImgArr[13];
                    break;
                case BrakingState.Fault :
                    return null;
                    break;
                case BrakingState.CutOut :
                    return m_BaImgArr[12];
                    
                    break;
                default :
                    throw new ArgumentOutOfRangeException();
            }
            return null;
        }

        public override string GetInfo()
        {
            return "空气制动";
        }

        protected override bool Initalize()
        {
            #region

            m_RectArr = new[]
                      {
                          new Rectangle(141, 131, 38, 22),
                          new Rectangle(214, 131, 38, 22),
                          new Rectangle(259, 131, 38, 22),
                          new Rectangle(334, 131, 38, 22),
                          new Rectangle(377, 131, 38, 22),
                          new Rectangle(450, 131, 38, 22),
                          new Rectangle(495, 131, 38, 22),
                          new Rectangle(568, 131, 38, 22),
                          new Rectangle(613, 131, 38, 22),
                          new Rectangle(687, 131, 38, 22),
                      };

            m_TxtRectArr = new[]
                         {
                             new Rectangle(178, 165, 38, 22),
                             new Rectangle(296, 165, 38, 22),
                             new Rectangle(404, 165, 38, 22),
                             new Rectangle(531, 165, 38, 22),
                             new Rectangle(650, 165, 38, 22)
                         };

            m_BarkeRectArr = new[]
                           {
                               new Rectangle(169, 266, 48, 48),
                               new Rectangle(297, 266, 48, 48),
                               new Rectangle(415, 266, 48, 48),
                               new Rectangle(533, 266, 48, 48),
                               new Rectangle(651, 266, 48, 48),
                               new Rectangle(169, 325, 48, 48),
                               new Rectangle(297, 325, 48, 48),
                               new Rectangle(415, 325, 48, 48),
                               new Rectangle(533, 325, 48, 48),
                               new Rectangle(651, 325, 48, 48),
                           };

            m_AirRect = new[]
                      {
                          new Rectangle(108, 383, 48, 72),
                          new Rectangle(714, 383, 48, 72),
                          new Rectangle(171, 405, 80, 30),
                          new Rectangle(619, 405, 80, 30)
                      };

            m_LinePoints = new[]
                         {
                             new Point(156, 419),
                             new Point(171, 419),
                             new Point(251, 419),
                             new Point(619, 419),
                             new Point(699, 419),
                             new Point(714, 419)
                         };

            m_TxtRects = new[]
                       {
                           new Rectangle(180, 131, 35, 22),
                           new Rectangle(298, 131, 35, 22),
                           new Rectangle(415, 131, 35, 22),
                           new Rectangle(533, 131, 35, 22),
                           new Rectangle(651, 131, 35, 22),
                       };

            m_BaImgArr = new[]
                       {
                           new Bitmap(RecPath + "\\frame/brakeFaulty.jpg"),
                           new Bitmap(RecPath + "\\frame/brakeCutout.jpg"),
                           new Bitmap(RecPath + "\\frame/brakeOff.jpg"),
                           new Bitmap(RecPath + "\\frame/brakeOk.jpg"),
                           new Bitmap(RecPath + "\\frame/brakeUnknown.jpg"),
                           new Bitmap(RecPath + "\\frame/eBrakeApp.jpg"),
                           new Bitmap(RecPath + "\\frame/eBrakeReleased.jpg"),
                           new Bitmap(RecPath + "\\frame/eBrakeUnknown.jpg"),
                           new Bitmap(RecPath + "\\frame/airCompFaulty.jpg"),
                           new Bitmap(RecPath + "\\frame/airCompStandby.jpg"),
                           new Bitmap(RecPath + "\\frame/airCompRunning.jpg"),
                           new Bitmap(RecPath + "\\frame/pBrakeApp.jpg"),
                           new Bitmap(RecPath + "\\frame/pBrakeIsolated.jpg"),
                           new Bitmap(RecPath + "\\frame/pBrakeReleased.jpg")
                       };

            #endregion

            m_FStart = new int[3];
            m_BStart = new int[4];
            m_BreakStatus = new byte[10];
            m_EmBreakStatus = new byte[5];
            m_ParBreakeStatus = new byte[5];
            m_AirCompressorStatus = new byte[2];
            m_FValue = new[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
            m_LegendBtn = new FjButtonEx(1, GlobleParam.ResManager.GetString("String32"), GlobleRect.m_LegendbtnRect);
            m_LegendBtn.MouseDown += (sender, pt) => ChangedPage(IranViewIndex.BrakeAirLegend);

            if (UIObj.InFloatList.Count >= 3)
            {
                m_FStart[0] = UIObj.InFloatList[0];
                m_FStart[1] = UIObj.InFloatList[1];
                m_FStart[2] = UIObj.InFloatList[2];
            }

            if (UIObj.InBoolList.Count >= 4)
            {
                for (var index = 0; index < 4; ++index)
                {
                    m_BStart[index] = UIObj.InBoolList[index];
                }
            }

            m_Train = IranTrainState.Instance.Train;
            foreach (var brakingListening in m_Train.Cars.Select(s => s.CarBraking).SelectMany(s => s.ListeningModelCollection).GroupBy(g => g.Type))
            {
                UpdateUiobject(brakingListening.Key, brakingListening.Select(s => s.Name));
            }


            foreach (var bogiesListening in m_Train.Cars.Select(s => s.Bogies).SelectMany(s => s.ListeningModelCollection).GroupBy(g => g.Type))
            {
                UpdateUiobject(bogiesListening.Key, bogiesListening.Select(s => s.Name));
            }

            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            var temp = 5;
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex) 5;

            DrawCarBraking(g);

            foreach (var rec in m_TxtRectArr)
            {
                g.DrawRectangle(GdiCommon.MediumGreyPen, rec);
                g.DrawString(m_FValue[temp].ToString("F1"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, rec, GdiCommon.RightFormat);
                ++temp;
            }

            //g.DrawString("%", GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, new Point(750, 134));
            g.DrawString("Bar", GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, new Point(750, 169));

            DrawBogiesBraking(g);

            temp = 0;
            //foreach (var rect in m_TxtRects)
            //{
            //    g.DrawString(m_FValue[temp].ToString("F0"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, rect, GdiCommon.CenterFormat);
            //    ++temp;
            //}

            GdiCommon.MediumGreyPen.Width = 4;
            for (var index = 0; index < 3; ++index)
            {
                g.DrawLine(GdiCommon.MediumGreyPen, m_LinePoints[index * 2], m_LinePoints[index * 2 + 1]);
            }
            GdiCommon.MediumGreyPen.Width = 1;
            g.DrawImage(m_BaImgArr[8 + 2 - m_AirCompressorStatus[0]], m_AirRect[0]);
            g.DrawImage(m_BaImgArr[8 + 2 - m_AirCompressorStatus[1]], m_AirRect[1]);
            g.DrawRectangle(GdiCommon.GrayWhitePen, m_AirRect[2]);
            g.DrawString(m_FValue[10].ToString("F1") + " Bar", GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, m_AirRect[2], GdiCommon.CenterFormat);
            g.DrawRectangle(GdiCommon.GrayWhitePen, m_AirRect[3]);
            g.DrawString(m_FValue[11].ToString("F1") + " Bar", GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, m_AirRect[3], GdiCommon.CenterFormat);

            m_LegendBtn.OnDraw(g);
        }

        private void DrawBogiesBraking(Graphics g)
        {
            for (int i = 0; i < m_Train.Cars.Count; i++)
            {
                var axs = m_Train.Cars[i].Bogies.Axises;

                g.DrawImage(GetBogiesBrakingImage(axs[0].Braking), m_RectArr[i * 2]);

                g.DrawImage(GetBogiesBrakingImage(axs[1].Braking), m_RectArr[i * 2 + 1]);
            }
        }

        private void DrawCarBraking(Graphics g)
        {
            for (int i = 0; i < m_Train.Cars.Count; i++)
            {
                var carBrakings = m_Train.Cars[i].CarBraking.Brakings;

                g.DrawImage(GetParkingBrakingImage(carBrakings[0]), m_BarkeRectArr[i]);
                g.DrawImage(GetEmergencyBrakingImage(carBrakings[1]), m_BarkeRectArr[i + 5]);
            }
        }

        public override bool mouseDown(Point point)
        {
            if (m_LegendBtn.IsVisible(point))
            {
                m_LegendBtn.OnMouseDown(point);
            }
            return true;
        }

        private void GetValue()
        {
            for (var index = 0; index < 5; ++index)
            {
                m_FValue[index] = FloatList[m_FStart[0] + index];
                m_FValue[index + 5] = FloatList[m_FStart[1] + index];
            }
            m_FValue[10] = FloatList[m_FStart[2]];
            m_FValue[11] = FloatList[m_FStart[2] + 1];

            GetStatus(m_BreakStatus, 10, 0, 5);
            GetStatus(m_ParBreakeStatus, 5, 1, 3);
            GetStatus(m_EmBreakStatus, 5, 2, 3);
            GetStatus(m_AirCompressorStatus, 2, 3, 3);
        }

        private void GetStatus(byte[] byteArr, int len, int num, int typeNum)
        {
            var flag = false;
            for (var index = 0; index < len; ++index)
            {
                flag = false;
                for (byte i = 0; i < typeNum; ++i)
                {
                    if (BoolList[m_BStart[num] + index * typeNum + i])
                    {
                        flag = true;
                        byteArr[index] = i;
                        break;
                    }
                }
                if (!flag)
                    byteArr[index] = (byte) ( typeNum - 1 );
            }
        }
    }
}