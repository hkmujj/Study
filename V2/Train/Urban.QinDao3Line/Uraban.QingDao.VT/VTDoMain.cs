using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using Excel.Interface;

using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Urban.QingDao.VT
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class VTDoMain : baseClass
    {
        private Dictionary<int, VTControl> m_VTControlsCellction;
        private List<VTControl> m_VTControls;
        private List<Tuple<SpeedPointer, PageType>> m_Pointer;
        public override bool init(ref int nErrorObjectIndex)
        {
            var excel = Path.Combine(RecPath, @"..\config\VT元件.xls");
            var config = new ExcelReaderConfig()
            {
                File = excel,
                Coloumns = new List<ColoumnConfig>() { new ColoumnConfig() { Name = "*", IsPrimaryKey = false } },
                SheetNames = new List<string>() { "元件", "指针" }
            };
            var dt = config.Adapter();
            var list = dt.Tables[0].Rows.Cast<DataRow>().ToList().Select(row =>
            {
                var tmp = new VTControlStruct
                {
                    ControlID = row[0].ToInt(),
                    Type = row[1].ToControlType(),
                    DefaultStatus = row[2].ToInt(),
                    ControlStatus = row[3].ToInt(),
                    OutKey = row[8].ToInt(),
                    InKey = row[9].ToInt(),
                    Text = row[10].ToString(),
                    Rect = row[11].ToRetangle(),
                    CurrentIamge = row[12].ToImage(this),
                    DefaultImage = row[13].ToImage(this),
                    PageType = row[14].ToPageType()

                };
                return tmp;
            }).GroupBy(g => g.ControlID);
            m_VTControlsCellction = new Dictionary<int, VTControl>();
            m_Pointer = new List<Tuple<SpeedPointer, PageType>>();
            foreach (var ls in list)
            {
                var tmp = ls.ToList();
                if (tmp.Count > 1)
                {
                    for (int i = 0; i < tmp.Count; i++)
                    {
                        if (i == 0)
                        {
                            m_VTControlsCellction.Add(tmp[i].ControlID, tmp[i].ToVTControl(this));

                        }
                        else
                        {
                            m_VTControlsCellction[tmp[i].ControlID].Images.Add(tmp[i].ControlStatus, new Tuple<Image, int>(tmp[i].CurrentIamge, tmp[i].OutKey));
                        }
                    }
                }
                else
                {
                    m_VTControlsCellction.Add(tmp[0].ControlID, tmp[0].ToVTControl(this));
                }
            }
            m_VTControls = m_VTControlsCellction.Values.ToList();
            m_VTControls.ForEach(f => f.Loaded());
            foreach (DataRow row in dt.Tables[1].Rows)
            {
                var rec = row[10].ToRetangle();
                var recf = new RectangleF(rec.Location, rec.Size);
                var pom = new SpeedPointer(row[5].ToImage(this), row[7].ToInt(), row[8].ToInt(), row[9].ToInt(), recf, row[4].ToInt(), this);
                m_Pointer.Add(new Tuple<SpeedPointer, PageType>(pom, row[6].ToPageType()));
            }
            DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += (sender, args) =>
            {
                switch (args.CourseService.CurrentCourseState)
                {
                    case CourseState.Unknown:
                        break;
                    case CourseState.Started:
                        m_VTControls.ForEach(f => f.Loaded());
                        break;
                    case CourseState.Stoped:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
            return true;
        }

        public override bool mouseDown(Point point)
        {

            var tmp = m_VTControls.Where(w => w.PageType == m_PageType);
            if (tmp.Any(a => a.Type == ControlType.Button))
            {
                foreach (var va in tmp.Where(w => w.LeftHot.Contains(point) && w.Type == ControlType.Button))
                {
                    m_PageType = va.InKey.ToPageType();
                    break;
                }
            }
            foreach (var control in tmp)
            {
                if (control.LeftHot.Contains(point))
                {
                    control.Last();
                    break;
                }
                if (control.RightHot.Contains(point))
                {
                    control.Next();
                    break;
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            if (ViewType != ViewType.Start)
            {
                return true;
            }
            foreach (var col in m_VTControls.Where(w => (w.LeftHot.Contains(point) || w.RightHot.Contains(point)) && w.PageType == m_PageType))
            {
                if (col.Type != ControlType.Knob && col.Type != ControlType.CircuitBreaker)
                {
                    if (col.Type == ControlType.AutoKnob)
                    {
                        col.AutoControlReset();
                    }
                    else
                    {
                        col.ClearSendData();
                    }
                }

            }
            return true;
        }
        private ViewType ViewType = ViewType.Stop;
        private PageType m_PageType = PageType.One;
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            ViewType = (ViewType)nParaB;
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                m_VTControls.ForEach(f => f.Reset());
            }
            base.setRunValue(nParaA, nParaB, nParaC);
        }

        public override void paint(Graphics g)
        {
            base.paint(g);
            var tmp = m_VTControlsCellction.Values.Where(w => w.PageType == m_PageType).ToList();
            tmp.ForEach(f => f.DrawBack(g));
            tmp.ForEach(f =>
            {

                f.OnDraw(g);
                f.Refresh();
            });
            m_Pointer.Where(w => w.Item2 == m_PageType).ToList().ForEach(f => f.Item1.PaintPointer(g));
        }
    }
}