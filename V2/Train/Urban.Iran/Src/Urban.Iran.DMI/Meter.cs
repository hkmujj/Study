using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.define;
using Urban.Iran.DMI.Controls;
using Urban.Iran.DMI.Index;
using Urban.Iran.DMI.Index.IndexKeys;
using Urban.Iran.DMI.Model;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Meter : baseClass, IDisposable
    {
        private Image[] m_ImgArr;
        private int[] m_MemIndex;
        private Rectangle m_Rect;
        private Rectangle m_ArrowRect;
        private Point[] m_MarkPts;
        private SolidBrush m_Brush;
        public static List<int> m_KeyArr;
        public static int CurPage { get; private set; }
        public static Dictionary<int, ErrorData> ErrorMap { get; private set; }
        public static Dictionary<int, EventData> CurEvent { get; private set; }
        private List<FjButtonEx> m_BtnArr;
        private Rectangle m_OverSpeed;
        private ControlModelTypeControl m_ControlModelTypeControl;

        private SpeedIndicatorControl m_SpeedIndicatorControl;

        private Pen m_TargetDegreeShortPen;
        private Pen m_TargetDegreeLongPen;

        private GDIRectText m_TimeControl;
        private GDIRectText m_TargetSpeedControl;

        private void btn_onMouseDown(FjButtonEx btnSender, Point pt)
        {
            switch (btnSender.m_BtnIndex)
            {
                case 0:
                    if ((CurPage + 1) * 4 < m_KeyArr.Count)
                        ++CurPage;
                    break;
                case 1:
                    if (CurPage > 0)
                        --CurPage;
                    break;
                default:
                    break;
            }
        }


        public override string GetInfo()
        {
            return "Meter";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_TargetDegreeLongPen = new Pen(GdiCommon.MediumGreyPen.Color, 3);
            m_TargetDegreeShortPen = new Pen(GdiCommon.MediumGreyPen.Color, 1);

            InitalizeControlTypeControl();

            InitalizeSpeedIndicator();

            m_TimeControl = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(500, 440, 75, 30),
                DrawFont = GdiCommon.Txt12Font,
                TextBrush = GdiCommon.MediumGreyBrush,
                BackColorVisible = false,
                RefreshAction = o =>
                {
                    var control = (GDIRectText)o;
                    control.Text = DateTime.Now.ToLongTimeString();
                }
            };

            CurPage = 0;
            m_MemIndex = new[]
           {
                IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.MMI屏亮屏],
                IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.MMI屏启动失败],
                IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.MMI屏一直启动],
                IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.MMI屏启动成功],
                IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.信号屏唤醒],
            };

            UIObj.InBoolList.AddRange(m_MemIndex);

            m_ImgArr = new Image[5];
            m_ImgArr[0] = ImageResourceFacade.inactive;
            m_ImgArr[1] = ImageResourceFacade.startup;
            m_ImgArr[2] = ImageResourceFacade.active;
            m_ImgArr[3] = ImageResourceFacade.tracker;
            m_ImgArr[4] = ImageResourceFacade.active2;
            m_KeyArr = new List<int>();
            ErrorMap = new Dictionary<int, ErrorData>();
            CurEvent = new Dictionary<int, EventData>();
            m_Rect = new Rectangle(35, 0, 10, 0);
            m_ArrowRect = new Rectangle(35, 48, 10, 21);
            m_OverSpeed = new Rectangle(5, 20, 50, 50);
            m_MarkPts = new Point[25]
            {
                new Point(18, 100),
                new Point(32, 100),
                new Point(18, 190),
                new Point(32, 190),
                new Point(18, 280),
                new Point(32, 280),
                new Point(20, 118),
                new Point(30, 118),
                new Point(20, 136),
                new Point(30, 136),
                new Point(20, 154),
                new Point(30, 154),
                new Point(20, 172),
                new Point(30, 172),
                new Point(20, 208),
                new Point(30, 208),
                new Point(20, 226),
                new Point(30, 226),
                new Point(20, 244),
                new Point(30, 244),
                new Point(20, 262),
                new Point(30, 262),
                new Point(8, 76),
                new Point(193, 163),
                new Point(181, 153)
            };

            m_Brush = new SolidBrush(Color.FromArgb(Miscellaneous.m_BriAlpha, 0, 16, 32));
            m_BtnArr = new List<FjButtonEx>
            {
                new FjButtonEx(0, new Rectangle(315, 380, 20, 36), ImageResourceFacade.upArrow),
                new FjButtonEx(1, new Rectangle(315, 416, 20, 36), ImageResourceFacade.downArrow),
            };
            m_BtnArr.ForEach(e => e.onMouseDown += btn_onMouseDown);

            LoadFaults();

            return true;
        }

        private void InitalizeSpeedIndicator()
        {
            m_SpeedIndicatorControl = new SpeedIndicatorControl(this)
            {
                OutLineRectangle = new Rectangle(64, 34, 258, 258)
            };
            m_SpeedIndicatorControl.Init();

            UIObj.InFloatList.AddRange(m_SpeedIndicatorControl.InFloatIndexs);

            m_TargetSpeedControl = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(145, 320, 85, 45),
                TextBrush = GdiCommon.PinkBrush,
                DrawFont = GdiCommon.Txt28Font,
                BackColorVisible = false,
                TextFormat =
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far },
                RefreshAction =
                    o =>
                    {
                        var control = (GDIRectText)o;
                        control.Text =
                            FloatList[IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.目标点速度]].ToString("0");
                        control.Visible = !BoolList[IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.目标速度显示]];
                    }
            };
        }

        private void InitalizeControlTypeControl()
        {
            m_ControlModelTypeControl = new ControlModelTypeControl(this)
            {
                OutLineRectangle = new Rectangle(290, 265, 45, 45)
            };
            UIObj.InBoolList.AddRange(m_ControlModelTypeControl.TypeIndexTuples.Select(s => s.Item2));
        }

        private void LoadFaults()
        {
            var file =
                Path.Combine(
                    DataPackage.Config.AppConfigs.First(f => f.AppName == ProjectName).AppPaths.ConfigDirectory,
                    "故障信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);

            foreach (var s in all)
            {
                var str = s.Split('\t');
                if (str.Length >= 3)
                {
                    var data = new ErrorData { m_Type = Convert.ToInt32(str[1]), m_Message = str[2] ,};
                    var key = Convert.ToInt32(str[0]);
                    if (!ErrorMap.ContainsKey(key))
                    {
                        ErrorMap[key] = data;
                    }
                }
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
            }
        }

        public override void paint(Graphics g)
        {
            //黑屏处理
            //if (!BoolList[IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.MMI屏亮屏]])
            //{
            //    append_postCmd(CmdType.ChangePage, 51, 0, 0);
            //}
            if ((BoolList[m_MemIndex[0]] && BoolList[m_MemIndex[1]]) || !BoolList[m_MemIndex[0]])
            {
                append_postCmd(CmdType.ChangePage, 51, 0, 0);
            }
            m_SpeedIndicatorControl.OnPaint(g);

            if (!BoolList[IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.目标距离显示]])
            {
                DrawTargetDistance(g);
            }
            if (BoolList[IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.超速制动标志]])
            {
                g.FillRectangle(GdiCommon.RedBrush, m_OverSpeed);
            }

            m_TargetSpeedControl.OnPaint(g);

            m_TimeControl.OnPaint(g);

            m_ControlModelTypeControl.OnPaint(g);

            m_Brush.Color = Color.FromArgb(Miscellaneous.m_BriAlpha, 0, 16, 32);

            g.FillRectangle(m_Brush, GlobleRect.BKRect);

            m_BtnArr.ForEach(e => e.OnDraw(g));

            DrawErrorMsg(g);
        }

        public override bool mouseDown(Point point)
        {
            foreach (var btn in m_BtnArr.Where(btn => btn.IsVisible(point)))
            {
                btn.OnMouseDown(point);
                return true;
            }
            return false;
        }

        private void DrawTargetDistance(Graphics g)
        {
            if (!BoolList[IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.显示距离]])
            {
                return;
            }

            for (var index = 0; index < 3; ++index)
            {
                g.DrawLine(m_TargetDegreeLongPen, m_MarkPts[index * 2], m_MarkPts[index * 2 + 1]);
            }

            for (var index = 3; index < 11; ++index)
            {
                g.DrawLine(m_TargetDegreeShortPen, m_MarkPts[index * 2], m_MarkPts[index * 2 + 1]);
            }


            var target = FloatList[IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.到目标点的距离]];

            if (target > 1000)
            {
                m_Rect.Y = 100;
                m_Rect.Height = 180;
                g.FillRectangle(GdiCommon.YellowBrush, m_Rect);
                g.DrawImage(ImageResourceFacade.arrow, m_ArrowRect);
            }
            else
            {
                var temp = (target > 1000) ? 1000 : target;
                m_Rect.Height = (int)((180 * temp) / 1000);
                m_Rect.Y = 280 - m_Rect.Height;
                g.FillRectangle(GdiCommon.YellowBrush, m_Rect);
            }
            g.DrawString(target.ToString("F0") + "m", GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, m_MarkPts[22]);
        }

        private void DrawErrorMsg(Graphics g)
        {
            var list = new List<Tuple<DateTime, string, ErrorData>>();

            for (var index = Meter.CurPage * 4; index < Meter.m_KeyArr.Count; ++index)
            {
                var errData = Meter.CurEvent[Meter.m_KeyArr[index]].m_Data;
                var str = Meter.CurEvent[Meter.m_KeyArr[index]].m_Dtime.ToShortTimeString() + "  :" + errData.m_Message;
                list.Add(new Tuple<DateTime, string, ErrorData>(Meter.CurEvent[Meter.m_KeyArr[index]].m_Dtime, str, errData));

                if ((index + 1) % 4 == 0)
                {
                    break;
                }
            }
            var tmp = list.OrderByDescending(o => o.Item1).ToList();
            for (int i = 0; i < tmp.Count; ++i)
            {
                var yTemp = i * 30 + 360;
                g.DrawString(tmp[i].Item2, GdiCommon.Txt12Font,
                  (tmp[i].Item3.m_Type == 1) ? GdiCommon.PinkBrush : GdiCommon.WhiteBrush, new Point(80, yTemp));
            }
        }

        public void Dispose()
        {
            m_TargetDegreeShortPen.Dispose();
            m_TargetDegreeLongPen.Dispose();
        }
    }
}