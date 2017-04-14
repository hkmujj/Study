using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Interface.Project
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProjectFormBase : Form, IViewForm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="dataPackage"></param>
        public ProjectFormBase(string appName, IDataPackage dataPackage)
            : this()
        {
            DataPackage = dataPackage;
            AppName = appName;

            AppConfig = dataPackage.Config.AppConfigs.FirstOrDefault(f => f.AppName == appName);
            InitializeComponent();
        }

    
        /// <summary>
        /// 
        /// </summary>
        protected ProjectFormBase()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);

            InitializeComponent();

        }

        /// <summary>
        /// 
        /// </summary>
        public string ProjectName
        {
            get { return AppName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IAppConfig AppConfig { protected set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string AppName { get; protected set; }

        /// <summary>
        /// 是否显示调试信息
        /// </summary>
        public bool DebugInfoVisible { get; set; }

        /// <summary>
        /// 数据包
        /// </summary>
        public IDataPackage DataPackage { get; protected set; }


        /// <summary>
        /// 激活窗口
        /// </summary>
        public virtual void Active()
        {
            if (!Visible)
            {
                Show();

                SetFormLocation();
            }

        }

        /// <summary>
        /// SetFormLocation
        /// </summary>
        protected void SetFormLocation()
        {
            var config = DataPackage.Config.AppConfigs.FirstOrDefault(f => f.AppName == AppName);
            if (config == null)
            {
                return;
            }

            StartPosition = FormStartPosition.Manual;
            Location = Point.Ceiling(config.ActureFormConfig.Rectangle.Location);
            Size = Size.Ceiling(config.ActureFormConfig.Rectangle.Size);

        }
    }
}
