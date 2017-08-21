using MMI.Facility.Interface.Service;
using System;
using System.Windows.Forms;
using ShenHuaHaoTMS.Common;
using YunDa.JC.MMI.Common;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V0_BackView : UserControl, IView
    {
        private Action<SH_V0_BackView> m_ViewControlAction = a => a.viewControl();


        public int ID { get; set; }

        public bool IsShow
        {
            get { return _isShow; }
            set
            {
                if (_isShow == value)
                {
                    return;
                }

                _isShow = value;

                this.InvokeIfNeed(m_ViewControlAction, this);
            }
        }

        private void viewControl()
        {
            if (_isShow) //显示
            {
                if (!ViewManger.Contains(this))
                {
                    ViewManger.Add(this);
                    this.InvokeShow();;
                }
            }
            else//隐藏
            {
                if (ViewManger.Contains(this))
                {
                    ViewManger.Remove(this);
                    this.InvokeHide();;
                }
            }
        }
        private bool _isShow = false;

        private ICommunicationDataService _dataService;

        public ViewManager ViewManger { get; set; }

        public SH_V0_BackView()
        {
            InitializeComponent();
        }

        public SH_V0_BackView(Int32 id, ViewManager viewManager, ICommunicationDataService dataService)
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
            Margin = new Padding(0);

            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);

            _dataService = dataService;
        }

        public void InvalidateNew()
        {
        }
    }
}
