using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using MMI.Facility.DataType.Log;
using MMI.Facility.DataType.View;
using MMI.Facility.Interface;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Running;

namespace MMI.Facility.View.Views.ActualContentHost
{
    public partial class ContentViewPanel : Panel
    {
        public ProjectFormBase ParentProjectForm { set; get; }

        /// <summary>
        /// 需要反复使用的视图对象
        /// </summary>
        private IRunningViewUnitParam m_LastViewObject;

        /// <summary>
        /// 多线程操作 RunningViewParam.CurrentRunningViewUnitParam
        /// </summary>
        private IRunningViewUnitParam m_CurrentViewObject;

        private readonly Font m_DebugFont = new Font("黑体", 14);

        private readonly PointF m_DebugInfoLocation = new PointF(10f, 10f);

        public Margins BoradMargins { set; get; }

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
        /// Paint timer 重绘， 有多线程操作
        /// </summary>
        private bool m_IsDrawing = false;

        /// <summary>
        /// 图元对象列表
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public Dictionary<string, IObjectBase> AddinObjectsLT { get; private set; }

        /// <summary>
        /// 逻辑配置表
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public Dictionary<int, IAppLogicConfig> LogicsInfoLT { get; private set; }

        public IRunningViewParam RunningViewParam { private set; get; }
        // ReSharper disable once InconsistentNaming
        protected Queue<MouseEvent> m_MouseEventQue = new Queue<MouseEvent>();


        public IReadOnlyDictionary<int, bool> BitList { get; private set; }

        public IReadOnlyDictionary<int, float> FloatList { get; private set; }

        //protected RectangleF m_ScalRectangleF = new RectangleF(0, 0, 1, 1);

        private Matrix m_ScalMatrix;

        private Matrix m_InvertScalMatrix;

        /// <summary>
        /// 记录视图变化
        /// </summary>
        private bool m_ViewIsChanged = false;

        private int m_LogicSpanTime = 0;

        private const int LogicSt = 5;

        /// <summary>
        /// 为多屏幕添加，区分屏幕ID
        /// </summary>
        public int Index = 0;

        private IRunningLogicCaculate m_RunningLogicCaculate;

        public IConfig Config { private set; get; }

        public ContentViewPanel()
        {
            BoradMargins = new Margins(0, 0, 0, 0);

            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        public void InitalizeDatas(string appName, IDataPackage dataPackage)
        {
            DataPackage = dataPackage;
            AppName = appName;
            this.Config = dataPackage.Config;

            AppConfig = Config.AppConfigs.FirstOrDefault(f => f.AppName == AppName);

            if (AppConfig == null)
            {
                SysLog.Fatal(string.Format("Can not find AppConfig where AppName is {0}", appName));
            }


            BitList =
                dataPackage.RunningParam.CommunicationFacadeDataService.GetCommunicationDataService(AppConfig)
                    .ReadService.ReadOnlyBoolDictionary;
            FloatList =
                dataPackage.RunningParam.CommunicationFacadeDataService.GetCommunicationDataService(AppConfig)
                    .ReadService.ReadOnlyFloatDictionary;
            RunningViewParam = dataPackage.RunningParam.AppRunningParamDictionary[appName].RunningViewParam;
            LogicsInfoLT = AppConfig.AppLogicConfig.AppLogicConfigDic;
            AddinObjectsLT = dataPackage.AddInLoader.ProjetAddinInstanceDic[appName];

            m_RunningLogicCaculate = DataPackage.RunningParam.AppRunningParamDictionary[AppName].RunningLogicCaculate;
        }

        private void ContentViewControl_Paint(object sender, PaintEventArgs e)
        {
            // 当前没有视图可绘
            if (RunningViewParam == null || RunningViewParam.CurrentRunningViewUnitParam == null)
            {
                return;
            }

            if (m_IsDrawing)
            {
                return;
            }

            m_IsDrawing = true;

            if (LogicsInfoLT != null && LogicsInfoLT.Count > 0 && m_LogicSpanTime > LogicSt)
            {
                LogicsCmp();
                m_LogicSpanTime = 0;
            }

            if (AddinObjectsLT != null && AddinObjectsLT.Count > 0)
            {
                e.Graphics.MultiplyTransform(m_ScalMatrix);

                UiPaint(e.Graphics);
            }

            ((IChangedDataAdapter<int, bool>)
                this.DataPackage.CommunicationDataFacadeService.GetCommunicationDataService(AppConfig)
                    .ReadService.ReadOnlyBoolOldDictionary).RequestClearChanged(this.AppName);
            ((IChangedDataAdapter<int, float>)
                this.DataPackage.CommunicationDataFacadeService.GetCommunicationDataService(AppConfig)
                    .ReadService.ReadOnlyFloatOldDictionary).RequestClearChanged(this.AppName);

            m_IsDrawing = false;
            m_LogicSpanTime++;
        }


        protected virtual void UiPaint(Graphics g)
        {
            // 当前没有视图可绘
            if (RunningViewParam == null || RunningViewParam.CurrentRunningViewUnitParam == null)
            {
                return;
            }
            m_CurrentViewObject = RunningViewParam.CurrentRunningViewUnitParam;
            m_ViewIsChanged = m_LastViewObject != m_CurrentViewObject;

            RefreshBk();

            //读取一个未处理事件
            var tmpMe = GetNotResponceEvent();

            if (tmpMe != null)
            {
                var point = m_InvertScalMatrix.TransformPoint(new Point(tmpMe.X, tmpMe.Y));
                tmpMe.X = point.X;
                tmpMe.Y = point.Y;

            }

            foreach (var uiIob in m_CurrentViewObject.Objects.Where(uiIob => uiIob.Enable))
            {
                try
                {
                    //设置该对象隶属的窗体编号
                    uiIob.ViewIndex = Index;
                    SetUiObjectRunValue(uiIob);
                }
                catch (Exception e)
                {
                    LogMgr.Fatal(string.Format("Ui SetUiObjectRunValue occuse some error.\r\n {0}", e));
                }
                try
                {
                    uiIob.paint(g);
                    uiIob.computValue();
                }
                catch (OutOfMemoryException e)
                {
                    LogMgr.Fatal(string.Format("Ui paint occuse OutOfMemoryException.\r\n {0}", e));
                    GC.Collect();
                }
                catch (Exception e)
                {
                    LogMgr.Fatal(string.Format("Ui paint occuse some error.\r\n {0}", e));
                }
                try
                {
                    UiObjectRespondMouseEvent(tmpMe, uiIob);
                }
                catch (Exception e)
                {
                    LogMgr.Fatal(string.Format("Ui UiObjectRespondMouseEvent occuse some error.\r\n {0}", e));
                }
            }

            DrowDebugInfo(g);

            m_LastViewObject = m_CurrentViewObject;
        }

        private MouseEvent GetNotResponceEvent()
        {
            MouseEvent tmpMe = null;
            if (m_MouseEventQue.Any())
            {
                tmpMe = m_MouseEventQue.Dequeue();
            }
            return tmpMe;
        }


        /// <summary>
        /// 设置动态数据
        /// </summary>
        /// <param name="uiIob"></param>
        private void SetUiObjectRunValue(IObjectBase uiIob)
        {
            //设置动态数据
            //1表示 传当前的视图编号
            uiIob.setRunValue(SetRunValueDefine.ParaADefine.InCurrent, m_CurrentViewObject.ViewConfig.Index, 0f);

            //2表示 当视图变化后 传递上次视图 和当前视图编号
            if (m_ViewIsChanged && m_LastViewObject != null)
            {
                uiIob.setRunValue(SetRunValueDefine.ParaADefine.SwitchFromOther,
                    m_CurrentViewObject.ViewConfig.Index,
                    m_LastViewObject.ViewConfig.Index);
            }
        }

        /// <summary>
        /// 刷新背景
        /// </summary>
        private void RefreshBk()
        {
            if (!string.IsNullOrEmpty(m_CurrentViewObject.ViewConfig.BgImageFn))
            {
                BackgroundImage = Image.FromFile(m_CurrentViewObject.ViewConfig.BgImageFn);
            }
            else
            {
                BackgroundImage = null;
                BackColor = m_CurrentViewObject.ViewConfig.BgColor;
            }
        }

        /// <summary>
        /// 视图对象响应鼠标事件
        /// </summary>
        /// <param name="tmpMe"></param>
        /// <param name="uiIob"></param>
        private void UiObjectRespondMouseEvent(MouseEvent tmpMe, IObjectBase uiIob)
        {
            if (tmpMe != null)
            {
                if (tmpMe.Type == MouseEventType.MouseDown)
                {
                    uiIob.mouseDown(new Point(tmpMe.X, tmpMe.Y));
                }
                else if (tmpMe.Type == MouseEventType.MouseUp)
                {
                    uiIob.mouseUp(new Point(tmpMe.X, tmpMe.Y));
                }
            }
        }

        /// <summary>
        /// 绘制调试信息
        /// </summary>
        /// <param name="g"></param>
        private void DrowDebugInfo(Graphics g)
        {
            if (DebugInfoVisible)
            {
                g.DrawString(
                    AppConfig.ProjectType + " --  " + AppName + " 屏:" + Index + "  视图:" +
                    m_CurrentViewObject.ViewConfig.Index,
                    m_DebugFont,
                    Brushes.LawnGreen,
                    m_DebugInfoLocation);
            }
        }

        private void ContentViewControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var tmpMe = new MouseEvent { Type = MouseEventType.MouseDown, X = e.X, Y = e.Y };

                m_MouseEventQue.Enqueue(tmpMe);
            }
        }

        private void ContentViewControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var tmpMe = new MouseEvent { Type = MouseEventType.MouseUp, X = e.X, Y = e.Y };

                m_MouseEventQue.Enqueue(tmpMe);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            UpdateScal();
        }

        private void UpdateScal()
        {
            if (Width == 0 || Height == 0)
            {
                SysLog.Warn("Can not set scal matrix when the windown's width or height == 0");
                return;
            }

            var scalRectangleF = new RectangleF
            {
                X = 0,
                Y = 0,
                Width = (float)Size.Width / (800 + BoradMargins.Left + BoradMargins.Right),
                Height = (float)Size.Height / (600 + BoradMargins.Top + BoradMargins.Bottom)
            };

            if (AppConfig != null && AppConfig.ActureFormConfig != null)
            {
                scalRectangleF.X = AppConfig.ActureFormConfig.DesignX;
                scalRectangleF.Y = AppConfig.ActureFormConfig.DesignY;

                if (AppConfig.ActureFormConfig.DesignWidth.IsDifferentTo(0f))
                {
                    scalRectangleF.Width = Size.Width /
                                       (AppConfig.ActureFormConfig.DesignWidth + BoradMargins.Left + BoradMargins.Right);
                    scalRectangleF.Height = Size.Height /
                                        (AppConfig.ActureFormConfig.DesignHeight + BoradMargins.Top + BoradMargins.Bottom);
                }
            }

            m_ScalMatrix = new Matrix();
            m_ScalMatrix.Translate(-scalRectangleF.X, -scalRectangleF.Y);
            m_ScalMatrix.Scale(scalRectangleF.Width, scalRectangleF.Height, MatrixOrder.Append);

            m_InvertScalMatrix = m_ScalMatrix.Clone();
            m_InvertScalMatrix.Invert();

            SysLog.Debug("{0} form size changed , new scal vale={1},{2},{3},{4}", AppName, scalRectangleF.X,
                scalRectangleF.Y, scalRectangleF.Width, scalRectangleF.Height);
        }

        protected virtual void LogicsCmp()
        {
            m_RunningLogicCaculate.Caculate();
        }
    }
}
