using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Events;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class AcitveEvents : HMIBase
    {
        private  DataGrid m_DataGrid;

        private Func<EventLife, bool> m_Filter;

        public static AcitveEvents Instance { private set; get; }

        private List<EventLife> m_ShowingEventsBuffer;

        public override string GetInfo()
        {
            return "AcitveEvents";
        }

        protected override bool Initalize()
        {
            Instance = this;
            SetFilter();
            var dataSource = new DataTable();
            dataSource.Columns.Add(GlobleParam.ResManager.GetString("String120"), typeof(string));
            dataSource.Columns.Add(GlobleParam.ResManager.GetString("String121"), typeof(string));
            dataSource.Columns.Add(GlobleParam.ResManager.GetString("String122"), typeof(string));
            dataSource.Columns.Add(GlobleParam.ResManager.GetString("String123"), typeof(string));
            dataSource.Columns.Add(GlobleParam.ResManager.GetString("String124"), typeof(int));

            m_DataGrid = new DataGrid(dataSource)
            {
                RowHeight = 30,
                RowHeadWidth = 31,
                IsRowHeadVisiable = true,
                IsShowRowHeadNum = true,
                Location = new Point(0, 135)
            };
            m_DataGrid.ColumnWidth.Add(60);
            m_DataGrid.ColumnWidth.Add(75);
            m_DataGrid.ColumnWidth.Add(320);
            m_DataGrid.ColumnWidth.Add(180);
            m_DataGrid.ColumnWidth.Add(130);
            m_DataGrid.SelectedRowChanged += DataGridOnSelectedRowChanged;

            m_DataGrid.Init();

            EventManager.Instance.EventAdded += life => RefreshEvents();
            EventManager.Instance.EventRemoved += life => RefreshEvents();
            EventManager.Instance.HistoryEventList.CollectionChanged += (sender, args) => RefreshEvents();

            return true;
        }

        private void DataGridOnSelectedRowChanged(DataGrid dataGrid)
        {
            if (dataGrid.SelectedRow != null)
            {
                var el = m_ShowingEventsBuffer[dataGrid.DataSource.Rows.IndexOf(dataGrid.SelectedRow)];
                AlarmPage.Instance.Event = el;
                ChangedPage(IranViewIndex.ExtendedEventInfomation);
            }   
        }

        public void GotoFirstPage()
        {
            m_DataGrid.GotoFirstPage();
        }

        public void SetFilter(Func<EventLife, bool> filter = null)
        {
            if (filter == null)
            {
                m_Filter = e => true;
            }
            else
            {
                m_Filter = filter;
            }
        }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView, IranViewIndex lastView)
        {

            if (naviType == NavigateType.SwitchFromOther)
            {
                BottomButton.Instance.NavigateLeft = () => m_DataGrid.GotoPreviousPage();
                BottomButton.Instance.NavigateRight = () => m_DataGrid.GotoNextPage();

                RefreshEvents();
            }
        }

        public void RefreshEvents()
        {
            IEnumerable<EventLife> target;
            switch (GlobleParam.Instance.CurrentIranViewIndex)
            {
                case IranViewIndex.EventHistory:
                    SetFilter();
                    target = EventManager.Instance.HistoryEventList;
                    break;
                case IranViewIndex.ActiveEvents:
                    target = EventManager.Instance.ActivedEventCollection.Values;
                    break;
                default:
                    return;
            }

            m_ShowingEventsBuffer = target.Where(m_Filter).OrderByDescending(o => o.StartTime).ToList();
            if (GlobleParam.Instance.CurrentIranViewIndex == IranViewIndex.ActiveEvents)
            {
                Train.Instance.ShowingEvents.Clear();
                Train.Instance.ShowingEvents.AddRange(m_ShowingEventsBuffer);
            }
            m_DataGrid.DataSource.Rows.Clear();
            foreach (var el in m_ShowingEventsBuffer)
            {
                AddDataGridRow(el);
            }
            m_DataGrid.Init();
        }


        public override bool mouseDown(Point point)
        {
            return m_DataGrid.OnMouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            return m_DataGrid.OnMouseUp(point);
        }

        public override void paint(Graphics g)
        {
            m_DataGrid.OnDraw(g);
        }

        private void AddDataGridRow(EventLife elife)
        {
            var info = elife.EventInfo;
            var dr = m_DataGrid.DataSource.NewRow();
            dr[0] = info.CarName;
            dr[1] = EnumUtil.GetDescription(info.Priority)[0];
            dr[2] = info.Description;
            dr[3] = elife.StartTime.ToString(CultureInfo.InvariantCulture);
            dr[4] = info.EventCode;
            m_DataGrid.DataSource.Rows.Add(dr);
        }
    }
}