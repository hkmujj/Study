using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Urban.Iran.DMI.Index;
using Urban.Iran.DMI.Index.IndexKeys;
using Urban.Iran.DMI.Model;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class StartScreen : baseClass
    {
        private Image[] m_ImgArr;
        private int[] m_MemIndex;
        private Rectangle m_TrackerRect;


        public override string GetInfo()
        {
            return "StartScreen";
        }

        public override bool init(ref int nErrorObjectIndex)
        {

            m_TrackerRect = new Rectangle(105, 286, 17, 15);

            IndexParam.Instance.Initalize(DataPackage.Config.SystemDicrectory.SystemConfigDirectory);

            DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += OnCourseStateChanged;

            m_MemIndex = new[]
            {
                IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.MMIÆÁÁÁÆÁ],
                IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.MMIÆÁÆô¶¯Ê§°Ü],
                IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.MMIÆÁÒ»Ö±Æô¶¯],
                IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.MMIÆÁÆô¶¯³É¹¦],
                IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ÐÅºÅÆÁ»½ÐÑ],
            };

            UIObj.InBoolList.AddRange(m_MemIndex);

            m_ImgArr = new Image[5];
            m_ImgArr[0] = ImageResourceFacade.inactive;
            m_ImgArr[1] = ImageResourceFacade.startup;
            m_ImgArr[2] = ImageResourceFacade.active;
            m_ImgArr[3] = ImageResourceFacade.tracker;
            m_ImgArr[4] = ImageResourceFacade.active2;

            SetValueWhenDebug();

            return true;
        }

        private void OnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            var service = courseStateChangedArgs.CourseService;
            if (service.CurrentCourseState == CourseState.Stoped)
            {
                append_postCmd(CmdType.ChangePage, 51, 0, 0);
            }
        }

        private void SetValueWhenDebug()
        {
            append_postCmd(CmdType.SetInBoolValue, m_MemIndex[0], 1, 0);
            append_postCmd(CmdType.SetInBoolValue, m_MemIndex[3], 1, 0);
            append_postCmd(CmdType.SetInBoolValue, IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ÏÔÊ¾¾àÀë], 1, 0);
        }

        public override void paint(Graphics g)
        {
            if (!BoolList[m_MemIndex[0]])
            {
                return;
            }

            if (BoolList[m_MemIndex[3]])
            {
                OnStarted(g);
            }
            else if (BoolList[m_MemIndex[2]])
            {
                OnStarting(g);
            }
            else if (BoolList[m_MemIndex[1]])
            {
                OnStartFail(g);
            }
        }

        private void OnStartFail(Graphics g)
        {
            g.DrawImage(m_ImgArr[0], GlobleRect.BKRect);
        }

        private void OnStarting(Graphics g)
        {
            g.DrawImage(m_ImgArr[1], GlobleRect.BKRect);
            if (m_TrackerRect.X >= 500)
            {
                m_TrackerRect.X = 105;
            }
            else
            {
                m_TrackerRect.X += 20;
            }
            g.DrawImage(m_ImgArr[3], m_TrackerRect);
        }

        private void OnStarted(Graphics g)
        {
            if (BoolList[m_MemIndex[4]])
            {
                append_postCmd(CmdType.ChangePage, 52, 0, 0);
                return;
            }
            g.DrawImage(m_ImgArr[2], GlobleRect.BKRect);
            if (m_TrackerRect.X >= 500)
            {
                m_TrackerRect.X = 105;
            }
            else
            {
                m_TrackerRect.X += 20;
            }
            g.DrawImage(m_ImgArr[3], m_TrackerRect);

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
    }
}