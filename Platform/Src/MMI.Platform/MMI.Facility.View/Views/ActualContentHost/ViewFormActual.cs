using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Running;

namespace MMI.Facility.View.Views.ActualContentHost
{
    public partial class ViewFormActual : ProjectFormBase
    {
        /// <summary>
        /// 由主配置决定
        /// </summary>
        public bool IsDebugModel
        {
            get { return Config.SystemConfig.IsDebugModel; }
        }

        /// <summary>
        /// TODO why when the name is m__RefreshViewTimer , derived type can not see the design view ?
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static readonly Timer _RefreshViewTimer = new Timer();

        protected SizeF m_ScalSize = new SizeF(1, 1);

        public IRunningViewParam RunningViewParam { private set; get; }

        public IConfig Config { protected set; get; }

        public override void Active()
        {
            if (!this.Visible)
            {
                SetFormLocation();
                this.Show();
            }
        }

        protected void SetFormLocation()
        {
            var config = DataPackage.Config.AppConfigs.FirstOrDefault(f => f.AppName == AppName);
            if (config == null)
            {
                LogMgr.Error(string.Format("There is no app config which appname is {0}", AppName));
                return;
            }

            StartPosition = FormStartPosition.Manual;
            Location = Point.Ceiling(config.ActureFormConfig.Rectangle.Location);
            Size = Size.Ceiling(config.ActureFormConfig.Rectangle.Size);

        }

        public ViewFormActual(string appName, IDataPackage dataPackage)
            : this()
        {
            DataPackage = dataPackage;
            AppName = appName;
            Config = dataPackage.Config;

            AppConfig = Config.AppConfigs.FirstOrDefault(f => f.AppName == AppName);

            if (AppConfig == null)
            {
                SysLog.Fatal(string.Format("Can not find AppConfig where AppName is {0}", appName));
            }


            RunningViewParam = dataPackage.RunningParam.AppRunningParamDictionary[appName].RunningViewParam;

            _RefreshViewTimer.Interval = AppConfig.ActureFormViewConfig.ViewRfreshInterval;
            _RefreshViewTimer.Tick += (o, args) => InvalidateControls();

            contentViewControl1.InitalizeDatas(appName, dataPackage);

            ShowInTaskbar = true;


            if (Config.SystemConfig.IsDebugModel)
            {
                TopMost = false;
                FormBorderStyle = FormBorderStyle.Sizable;
                if (Config.SystemConfig.StartModel == StartModel.Edit || Config.SystemConfig.StartModel == StartModel.Normal)
                {
                    contentViewControl1.DebugInfoVisible = true;
                }
            }
            else
            {
                FormBorderStyle = FormBorderStyle.None;
            }
            
            Text = string.Format("Actual form : {0}", appName);

            contentViewControl1.MouseEnter += (sender, args) => OnMouseEnter(args);
            contentViewControl1.MouseLeave += (sender, args) => OnMouseLeave(args);
        }

        public ViewFormActual()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected void InvalidateControls()
        {
            Invalidate();

            contentViewControl1.Invalidate();
        }

        private void MMIActualForm_Load(object sender, EventArgs e)
        {
            if (_RefreshViewTimer != null)
            {
                _RefreshViewTimer.Start();
            }

            SetFormLocation();

            SetDesktopLocation(AppConfig.ActureFormConfig.Rectangle.X, AppConfig.ActureFormConfig.Rectangle.Y);
        }
    }
}