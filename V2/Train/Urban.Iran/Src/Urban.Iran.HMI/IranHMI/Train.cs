using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using Urban.Domain.TrainState.Interface;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Domain;
using Urban.Iran.HMI.Events;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Train : HMIBase
    {
        private Rectangle[] m_TrainRect;
        private Point[] m_TrainNum;
        private Bitmap[] m_BmpArr;
        private Point m_Location;

        private Bitmap[] m_PantographImg;

        public static Train Instance { private set; get; }

        public List<EventLife> ShowingEvents { private set; get; }

        public override string GetInfo()
        {
            return "Train";
        }

        protected override bool Initalize()
        {
            Instance = this;
            ShowingEvents = new List<EventLife>();

            m_Location = new Point(127, 63);
            m_TrainRect = new[]
            {
                new Rectangle(m_Location.X, m_Location.Y + 18, 614, 43),
                new Rectangle(m_Location.X, m_Location.Y + 18, 130, 43),
                new Rectangle(m_Location.X + 484, m_Location.Y + 18, 130, 43),
                new Rectangle(m_Location.X + 136, m_Location.Y + 4, 23, 15),
                new Rectangle(m_Location.X + 454, m_Location.Y + 4, 23, 15),
                new Rectangle(m_Location.X - 16, m_Location.Y + 20, 10, 38), //方向箭头
                new Rectangle(m_Location.X, m_Location.Y + 18, 130, 43),
                new Rectangle(m_Location.X + 130, m_Location.Y + 18, 118, 43),
                new Rectangle(m_Location.X + 248, m_Location.Y + 18, 118, 43),
                new Rectangle(m_Location.X + 366, m_Location.Y + 18, 118, 43),
                new Rectangle(m_Location.X + 485, m_Location.Y + 18, 130, 43)
            };
            m_TrainNum = new[]
            {
                new Point(m_Location.X + 48, m_Location.Y),
                new Point(m_Location.X + 175, m_Location.Y),
                new Point(m_Location.X + 292, m_Location.Y),
                new Point(m_Location.X + 400, m_Location.Y),
                new Point(m_Location.X + 527, m_Location.Y)
            };
            m_BmpArr = new[]
            {
                new Bitmap(RecPath + "\\frame/rArrow.jpg"),
                new Bitmap(RecPath + "\\frame/lArrow.jpg"),
                new Bitmap(RecPath + "\\frame/rTrain.jpg"),
                new Bitmap(RecPath + "\\frame/rPanto.jpg"),
                new Bitmap(RecPath + "\\frame/lPanto.jpg"),
                new Bitmap(RecPath + "\\frame/rRTrainLeft.jpg"),
                new Bitmap(RecPath + "\\frame/rYTrainLeft.jpg"),
                new Bitmap(RecPath + "\\frame/rWTrainLeft.jpg"),
                new Bitmap(RecPath + "\\frame/rTrainLeft.jpg"),
                new Bitmap(RecPath + "\\frame/rRTrainMiddle.jpg"),
                new Bitmap(RecPath + "\\frame/rYTrainMiddle.jpg"),
                new Bitmap(RecPath + "\\frame/rWTrainMiddle.jpg"),
                new Bitmap(RecPath + "\\frame/rTrainMiddle.jpg"),
                new Bitmap(RecPath + "\\frame/rRTrainRight.jpg"),
                new Bitmap(RecPath + "\\frame/rYTrainRight.jpg"),
                new Bitmap(RecPath + "\\frame/rWTrainRight.jpg"),
                new Bitmap(RecPath + "\\frame/rTrainRight.jpg")
            };

            m_PantographImg = new[]
            {
                new Bitmap(RecPath + "\\frame/lPanto_on.jpg"),
                new Bitmap(RecPath + "\\frame/lPanto_off.jpg"),
                new Bitmap(RecPath + "\\frame/rPanto_on.jpg"),
                new Bitmap(RecPath + "\\frame/rPanto_off.jpg"),
            };

            var listenPantographModels =
                IranTrainState.Instance.Train.Cars.SelectMany(s => s.Pantographs)
                    .SelectMany(s => s.ListeningModelCollection)
                    .ToList();

            UpdateUiobject(CommunicationIndexType.InBool,
                listenPantographModels.Where(w => w.Type == CommunicationIndexType.InBool).Select(s => s.Name));
            UpdateUiobject(CommunicationIndexType.InFloat,
                listenPantographModels.Where(w => w.Type == CommunicationIndexType.InFloat).Select(s => s.Name));
            UpdateUiobject(CommunicationIndexType.OutBool,
                listenPantographModels.Where(w => w.Type == CommunicationIndexType.OutBool).Select(s => s.Name));
            UpdateUiobject(CommunicationIndexType.OutFloat,
                listenPantographModels.Where(w => w.Type == CommunicationIndexType.OutFloat).Select(s => s.Name));

            return true;
        }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView, IranViewIndex lastView)
        {
            if (naviType == NavigateType.SwitchFromOther)
            {
                ShowingEvents.Clear();

                var flag = UpdateLocation(currentView, lastView);

                if (flag)
                {
                    UpdateTrainRegoin();
                }
            }
        }

        private void UpdateTrainRegoin()
        {

            m_TrainRect = new[]
            {
                new Rectangle(m_Location.X, m_Location.Y + 18, 614, 43),
                new Rectangle(m_Location.X, m_Location.Y + 18, 130, 43),
                new Rectangle(m_Location.X + 484, m_Location.Y + 18, 130, 43),
                new Rectangle(m_Location.X + 136, m_Location.Y + 4, 23, 15),
                new Rectangle(m_Location.X + 454, m_Location.Y + 4, 23, 15),
                new Rectangle(m_Location.X - 16, m_Location.Y + 20, 10, 38), //方向箭头
                new Rectangle(m_Location.X, m_Location.Y + 18, 130, 43),
                new Rectangle(m_Location.X + 130, m_Location.Y + 18, 118, 43),
                new Rectangle(m_Location.X + 248, m_Location.Y + 18, 118, 43),
                new Rectangle(m_Location.X + 366, m_Location.Y + 18, 118, 43),
                new Rectangle(m_Location.X + 485, m_Location.Y + 18, 130, 43)
             };
            m_TrainNum = new[]
            {
                new Point(m_Location.X + 48, m_Location.Y),
                new Point(m_Location.X + 175, m_Location.Y),
                new Point(m_Location.X + 292, m_Location.Y),
                new Point(m_Location.X + 400, m_Location.Y),
                new Point(m_Location.X + 527, m_Location.Y)
            };



        }

        private bool UpdateLocation(IranViewIndex nParaB, IranViewIndex nParaC)
        {
            var flag = false;

            switch (nParaB)
            {
                case (IranViewIndex)40:
                case (IranViewIndex)39:
                    flag = true;
                    m_Location = new Point(127, 20);
                    break;
                case (IranViewIndex)6:
                    flag = true;
                    m_Location = new Point(95, 63);
                    break;
                case (IranViewIndex)8:
                    flag = true;
                    m_Location = new Point(122, 63);
                    break;
                default:
                    if ((nParaC == (IranViewIndex)39 || nParaC == (IranViewIndex)1 || nParaC == (IranViewIndex)3 || nParaC == (IranViewIndex)40 || nParaC == (IranViewIndex)6 || nParaC == (IranViewIndex)8) &&
                        (nParaB != (IranViewIndex)39 || nParaB == (IranViewIndex)2 || nParaB == (IranViewIndex)2 || nParaC != (IranViewIndex)40 || nParaB != (IranViewIndex)6 || nParaB != (IranViewIndex)8))
                    {
                        flag = true;
                        m_Location = new Point(127, 63);
                    }
                    break;
            }
            return flag;
        }

        public override void paint(Graphics g)
        {
            if (true) //可能需要换端
            {
                DrawCarNames(g);

                g.DrawImage(m_BmpArr[0], m_TrainRect[5]);
                g.DrawImage(m_BmpArr[2], m_TrainRect[0]);

                DrawPantograph(g);

                DrawEventCarIfNeed(g);
            }
        }

        private void DrawEventCarIfNeed(Graphics g)
        {
            foreach (var gp in ShowingEvents.GroupBy(p => p.EventInfo.CarName))
            {
                var currentEventColor = gp.Min(m => m.EventInfo.Color);
                switch (gp.Key)
                {
                    case CarNameConstant.Car01011:
                        g.DrawImage(m_BmpArr[(int)(4 + currentEventColor)], m_TrainRect[6]);
                        break;
                    case CarNameConstant.Car01012:
                        g.DrawImage(m_BmpArr[(int)(8 + currentEventColor)], m_TrainRect[7]);
                        break;
                    case CarNameConstant.Car01013:
                        g.DrawImage(m_BmpArr[(int)(8 + currentEventColor)], m_TrainRect[8]);
                        break;
                    case CarNameConstant.Car01014:
                        g.DrawImage(m_BmpArr[(int)(8 + currentEventColor)], m_TrainRect[9]);
                        break;
                    case CarNameConstant.Car01015:
                        g.DrawImage(m_BmpArr[(int)(12 + currentEventColor)], m_TrainRect[10]);
                        break;
                }
            }
        }

        private void DrawCarNames(Graphics g)
        {
            var temp = 1011;
            foreach (var pt in m_TrainNum)
            {
                g.DrawString(temp.ToString("00000"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, pt);
                ++temp;
            }
        }

        private void DrawPantograph(Graphics g)
        {
            g.DrawImage(
                IranTrainState.Instance.Train.Cars[1].Pantographs[0].State == PantographState.Up
                    ? m_PantographImg[2]
                    : m_PantographImg[3], m_TrainRect[3]);

            g.DrawImage(
                IranTrainState.Instance.Train.Cars[3].Pantographs[0].State == PantographState.Up
                    ? m_PantographImg[0]
                    : m_PantographImg[1], m_TrainRect[4]);
        }
    }
}