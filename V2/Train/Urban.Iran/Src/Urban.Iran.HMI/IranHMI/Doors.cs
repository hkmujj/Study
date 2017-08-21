using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Domain.TrainState.Interface;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Config.ConfigModel;
using Urban.Iran.HMI.Domain;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Doors : HMIBase
    {
        //private  Bitmap[] bmpArr;

        private Bitmap[] m_DoorStateImageCollection;

        private FjButtonEx m_LegendBtn;
        private List<DoorBase> m_Doors;
        private List<GDIRectText> m_RectCollection;

        private RectangleImage m_TrainImage;

        private List<GDIRectText> m_CarNames;

        public override string GetInfo()
        {
            return "车门状态";
        }

        protected override bool Initalize()
        {
            m_LegendBtn = new FjButtonEx(1, GlobleParam.ResManager.GetString("String32"), GlobleRect.m_LegendbtnRect);
            m_LegendBtn.MouseDown += (sender, pt) => ChangedPage(IranViewIndex.DoorsLegend);
            m_RectCollection = new List<GDIRectText>();

            m_TrainImage = new RectangleImage()
            {
                Image = new Bitmap(Path.Combine(RecPath, "frame\\Train.jpg")),
                OutLineRectangle = new Rectangle(81, 179, 639, 81)
            };
            //35,28
            m_CarNames = CarNameConstant.CarNameCollection.Select((s, i) => new GDIRectText()
            {
                OutLineRectangle = new Rectangle(126 + 124*i, 207, 56, 28),
                Text = s,
                TextColor = GdiCommon.DarkBlueBrush.Color,
                BkColor = Color.FromArgb(82, 90, 109),
            }).ToList();

            m_DoorStateImageCollection = new[]
            {
                new Bitmap(RecPath + "\\frame/drClose.jpg"),
                new Bitmap(RecPath + "\\frame/drOpen.jpg"),
                new Bitmap(RecPath + "\\frame/drCutout.jpg"),
                new Bitmap(RecPath + "\\frame/drFaulty.jpg"),
                new Bitmap(RecPath + "\\frame/drEmergencyUnlock.jpg"),
                new Bitmap(RecPath + "\\frame/peaActive.jpg"),
                new Bitmap(RecPath + "\\frame/peaFaulty.jpg"),
                new Bitmap(RecPath + "\\frame/peaSpeeking.jpg"),
            };


            InitalizePeaStates();

            InitalizeDoorStates();

            InitalizeUiObject();

            return true;
        }

        private void InitalizePeaStates()
        {
            var x1 = 96;
            const int yUp1 = 190;
            const int yDown1 = 227;
            const int width1 = 25;
            const int height1 = 20;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0)
                    {
                        var tmp = new GDIRectText
                        {
                            Tag = new string[]
                            {
                                "0101" + (i + 1) + " Right " + (j + 1) + " peaActive",
                                "0101" + (i + 1) + " Right " + (j + 1) + " peaFaulty",
                                "0101" + (i + 1) + " Right " + (j + 1) + " peaSpeeking"
                            },
                            NeedDarwOutline = false,
                            BackColorVisible = false,
                            OutLineRectangle = new Rectangle(x1, yUp1, width1, height1),
                            RefreshAction = o => RefreshImage((GDIRectText) o)
                        };
                        m_RectCollection.Add(tmp);
                    }
                    if (j == 3)
                    {
                        var tmp1 = new GDIRectText
                        {
                            Tag = new string[]
                            {
                                "0101" + (i + 1) + " Left " + (j + 1) + " peaActive",
                                "0101" + (i + 1) + " Left " + (j + 1) + " peaFaulty",
                                "0101" + (i + 1) + " Left " + (j + 1) + " peaSpeeking"
                            },
                            NeedDarwOutline = false,
                            BackColorVisible = false,
                            OutLineRectangle = new Rectangle(x1, yDown1, width1, height1),
                            RefreshAction = o => RefreshImage((GDIRectText) o)
                        };
                        m_RectCollection.Add(tmp1);
                    }
                    x1 += 28;
                }
                x1 += 12;
            }
        }

        private void InitalizeDoorStates()
        {
            m_Doors = new List<DoorBase>();

            foreach (
                var result in
                    IranTrainState.Instance.Train.Cars.SelectMany(s => s.Doors)
                        .SelectMany(s => s.ListeningModelCollection)
                        .Cast<DoorListening>())
            {
                if (m_Doors.Find(f => f.CarDoorName == result.CarName + result.DoorName) == null)
                {
                    var tmp = new DoorBase(this, result)
                    {
                        OutRectangleFalse = ParseToRectangle(result.OutRectangleTrainsideFalse),
                        OutRectangleTure = ParseToRectangle(result.OutRectangleTrainsideTrue)
                    };
                    tmp.IndexDictionary.Add(0, GlobleParam.Instance.InBoolDictionary[result.Name]);
                    tmp.RefreshAction += r =>
                    {
                        DoorBase dr = (DoorBase) r;
                        if (BoolList[dr.IndexDictionary[3]])
                        {
                            dr.DoorState = DoorState.EmergencyUnlock;
                        }
                        else if (BoolList[dr.IndexDictionary[1]])
                        {
                            dr.DoorState = DoorState.CutOut;
                        }
                        else if (BoolList[dr.IndexDictionary[2]])
                        {
                            dr.DoorState = DoorState.Fault;
                        }
                        else if (BoolList[dr.IndexDictionary[0]])
                        {
                            dr.DoorState = DoorState.Close;
                        }
                        else
                        {
                            dr.DoorState = DoorState.Open;
                        }
                    };
                    m_Doors.Add(tmp);
                }
                else
                {
                    int index = m_Doors.FindIndex(f => f.CarDoorName == result.CarName + result.DoorName);
                    m_Doors[index].IndexDictionary.Add(FindMaxKey(m_Doors[index].IndexDictionary) + 1,
                        GlobleParam.Instance.InBoolDictionary[result.Name]);
                }
            }
        }

        private void InitalizeUiObject()
        {
            UpdateUiobject(CommunicationIndexType.InBool, m_RectCollection.SelectMany(s => (string[]) s.Tag));

            UIObj.InBoolList.AddRange(m_Doors.SelectMany(s => s.IndexDictionary.Values));
        }

        private static int FindMaxKey(Dictionary<int, int> dPara)
        {
            return dPara.Keys.Concat(new[] {int.MinValue}).Max();
        }

        private static Rectangle ParseToRectangle(string sPara)
        {
            var tmp = sPara.Split(',');
            return tmp.Length == 4
                ? new Rectangle(int.Parse(tmp[0]), int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]))
                : new Rectangle();
        }

        /// <summary>
        /// 刷新PEA的状态
        /// </summary>
        /// <param name="item">当前的对象</param>
        private void RefreshImage(GDIRectText item)
        {
            var names = item.Tag as string[];
            if (names.Length < 3)
            {
                LogMgr.Info("The tag length <3! ");
            }
            if (BoolList[GlobleParam.Instance.FindInBoolIndex(names[1])])
            {
                item.BKImage = m_DoorStateImageCollection[6];
            }
            else if (BoolList[GlobleParam.Instance.FindInBoolIndex(names[2])])
            {
                item.BKImage = m_DoorStateImageCollection[7];
            }
            else if (BoolList[GlobleParam.Instance.FindInBoolIndex(names[0])])
            {
                item.BKImage = m_DoorStateImageCollection[5];
            }
            else
            {
                item.BKImage = null;
            }
        }

        public override void paint(Graphics g)
        {
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex) 3;

            try
            {
                m_TrainImage.OnDraw(g);

                m_CarNames.ForEach(e => e.OnDraw(g));

                m_Doors.ForEach(e => e.OnPaint(g));

                m_LegendBtn.OnDraw(g);

                m_RectCollection.ForEach(e => e.OnPaint(g));

            }
            catch (Exception e)
            {
                LogMgr.Fatal(string.Format(
                    "Occuse some exception in {0} of public override void paint(Graphics g),{1}", GetType(), e));
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
    }
}