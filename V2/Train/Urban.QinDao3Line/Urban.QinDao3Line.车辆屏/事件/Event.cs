using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;
using MsgReceiveMod;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class Event : NewQBaseclass
    {
        private List<string> m_String;
        private bool[] IsBtndown = new bool[4];
        /// <summary>
        /// 事件移动号
        /// </summary>
        //private readonly int m_EventId = 0;

        /// <summary>
        /// 事件的页数
        /// </summary>
        //private int m_EventPage = 0;

        /// <summary>
        /// 事件的总共页数
        /// </summary>
        private int m_EventSumPage = 0;

        private readonly Dictionary<int, string[]> m_EventDic = new Dictionary<int, string[]>();

        /// <summary>
        /// 所有发生过的事件
        /// </summary>
        public static MsgHandlerFault0<FaultEvent> MsgInfList
        {
            get { return m_MsgInfList; }
        }

        private static readonly MsgHandlerFault0<FaultEvent> m_MsgInfList = new MsgHandlerFault0<FaultEvent>();

        public static EventMsg<BaseEvent> EventList
        {
            get { return m_EvevntList; }
        }

        private static readonly EventMsg<BaseEvent> m_EvevntList = new EventMsg<BaseEvent>();

        /// <summary>
        /// 发生过的逻辑号
        /// </summary>
        private readonly List<int> m_FaultLogicIDList = new List<int>();

        public override void paint(Graphics g)
        {
            EventLoad();
            FillRects(g);
            DrawButtons(g);
            Init();
            DrawRects(g);
            DrawWords(g);
            base.paint(g);
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            m_String = new List<string>()
            {
                string.Empty,
                ResourceFacade.MPUEventList,                     //1
                ResourceFacade .MPUEventResourceReLoad,          //2
                ResourceFacade .MPUEventResourcePage,            //3
                ResourceFacade .MPUEventResourceEvents,          //4
                ResourceFacade .MPUEventDateAndTime,             //5
                ResourceFacade .MPUEventLocation,
                ResourceFacade .MPUEventStackName,
                ResourceFacade .MPUEventDescription,
            };
            EventData.InitData();
            Read read = new Read(Path.Combine(AppConfig.AppPaths.ConfigDirectory, "事件.txt"), m_EventDic);
            read.LoadConfigs();
            return base.init(ref nErrorObjectIndex);
        }

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (EventData.Regions[index].IsVisible(point))
                {
                    IsBtndown[index] = true;
                }
            }
            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (EventData.Regions[index].IsVisible(point))
                {
                    IsBtndown[index] = false;
                }
            }
            return base.mouseUp(point);
        }

        private void DrawButtons(Graphics e)
        {
            for (int i = 0; i < 3; i++)
            {
                if (IsBtndown[i])
                {
                    e.DrawImage(m_Imgs[1], EventData.Rects[1+i]);
                }
                else
                    e.DrawImage(m_Imgs[0], EventData.Rects[1+i]);
                if (i >= 1)
                    e.DrawImage(m_Imgs[1+i], EventData.Rects[4+i]);
            }
        }

        private void DrawRects(Graphics e)
        {
            for (int i = 0; i < 64; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, EventData.Rects[7 + i]);
            }
        }

        private void FillRects(Graphics e)
        {
            for (int i = 0; i < 64; i++)
            {
                e.FillRectangle(Common.m_WhiteBrush, EventData.Rects[7 + i]);
            }
        }

        private void DrawWords(Graphics e)
        {
            e.DrawString(m_String[1], Common.m_Font18, Common.m_BlackBrush,
                        EventData.Rects[0], Common.m_MFormat);
            e.DrawString(m_String[2], Common.m_Font12, Common.m_BlackBrush,
                        EventData.Rects[1], Common.m_MFormat);
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_String[3+i]+":", Common.m_Font10, Common.m_BlackBrush,
                                       EventData.Rects[7 + i], Common.m_CenterFormat);
            }
            for (int i = 0; i < 4; i++)
            {
                e.DrawString(m_String[5 + i], Common.m_Font10, Common.m_BlackBrush,
                                                      EventData.Rects[11 + 15 * i], Common.m_CenterFormat);
            }
            //int index = 0;
            //try
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        e.DrawString(m_EventDic[1 + i][1] + "  :", Common.m_Font12, Common.m_BlackBrush,
            //            EventData.Rects[59 + i], Common.m_MFormat);
            //    }
            //    for (int i = 0; i < 4; i++)
            //    {
            //        e.DrawString(m_EventDic[3 + i][1], Common.m_Font12, Common.m_BlackBrush,
            //            EventData.Rects[15 + index], Common.m_MFormat);
            //        index += 11;
            //    }
            //    if (EventList.EventLsit.Count > 0 && EventList.EventLsit.Count < 10)
            //    {
            //        for (int i = 0; i < EventList.EventLsit.Count; i++)
            //        {
            //            e.DrawString((1 + i + m_EventId*10).ToString(), Common.m_Font12, Common.m_BlackBrush,
            //                EventData.Rects[5 + i], Common.m_MFormat);
            //            //事件的内容
            //            e.DrawString(EventList.EventLsit[i + m_EventId * 10].EventContent, Common.m_Font12,
            //                Common.m_BlackBrush,
            //                EventData.Rects[27 + i], Common.m_MFormat);
            //            //事件的发生地方
            //            e.DrawString(EventList.EventLsit[i + m_EventId * 10].EventLocation, Common.m_Font12,
            //                Common.m_BlackBrush,
            //                EventData.Rects[38 + i], Common.m_MFormat);
            //            //事件的名字
            //            e.DrawString(EventList.EventLsit[i + m_EventId * 10].EventName, Common.m_Font12,
            //                Common.m_BlackBrush,
            //                EventData.Rects[49 + i], Common.m_MFormat);
            //            //事件发生的时间
            //            e.DrawString(
            //                EventList.EventLsit[i + m_EventId * 10].EventTime.ToString("dd/MM/yy hh:mm:ss"),
            //                Common.m_Font10, Common.m_BlackBrush,
            //                EventData.Rects[16 + i], Common.m_MFormat);

            //        }
            //    }
            //    else if (MsgInfList.CurrentMsgList.Count > 10)
            //    {
            //        for (int i = 0; i < 10; i++)
            //        {
            //            e.DrawString((1 + i + m_EventId*10).ToString(), Common.m_Font10, Common.m_BlackBrush,
            //                EventData.Rects[5 + i], Common.m_MFormat);
            //        }

            //    }

            //}
            //catch (Exception ex)
            //{

            //}

        }

        private void EventLoad()
        {
            foreach (var index in m_EventDic.Keys)
            {
                var eventinfo = new BaseEvent();
                if (BoolList[index] && index > 2 && !m_FaultLogicIDList.Contains(index))
                {

                    eventinfo.EventContent = m_EventDic[index][1];
                    eventinfo.EventLocation = m_EventDic[index][2];
                    eventinfo.EventName = m_EventDic[index][3];
                    eventinfo.EventTime = DateTime.Now;
                    eventinfo.EventLogicId = index;
                    m_EvevntList.AddEvent(eventinfo,DateTime.Now);
                    m_FaultLogicIDList.Add(index);

                }
                else if (m_FaultLogicIDList.Contains(index) && !BoolList[index])
                {

                    m_EvevntList.EventOver(index, DateTime.Now);
                    m_FaultLogicIDList.Remove(index);
                }
            }

        }
        private void Init()
        {
            m_EventSumPage = EventList.EventLsit.Count / 10;
        }  
    }
}
