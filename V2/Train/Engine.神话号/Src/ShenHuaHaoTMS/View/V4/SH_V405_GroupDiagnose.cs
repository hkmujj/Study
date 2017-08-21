using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CommonControls;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;
using ShenHuaHaoTMS.Common;
using YunDa.JC.MMI.Common;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V405_GroupDiagnose : UserControl, IView, IDataListener
    {
        public class DiagnoseInfo
        {
            public Int32 BoolLogic { get; set; }

            public DefLabel FalutLabel { get; set; }

            public DefLabel PointOutLabel { get; set; }
        }

        public int ID { get; set; }

        private readonly ListenViewChangedBridge m_ListenViewChanged = new ListenViewChangedBridge();

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

                if (_isShow) //显示
                {
                    if (!ViewManger.Contains(this))
                    {
                        ViewManger.Add(this);
                        this.InvokeShow();
                        ;
                        GlobalParam.Instance.InitParam.RegistDataListener(m_ListenViewChanged);
                    }
                }
                else //隐藏
                {
                    if (ViewManger.Contains(this))
                    {
                        ViewManger.Remove(this);
                        this.InvokeHide();
                        ;
                        _viewBtns.ForEach(a => a.IsDown = false);
                        GlobalParam.Instance.InitParam.UnregistDataListener(m_ListenViewChanged);
                    }
                }
            }
        }

        private bool _isShow = false;

        public ViewManager ViewManger { get; set; }
        private ICommunicationDataService _dataService;

        private List<DefButton> _viewBtns = new List<DefButton>();
        private List<DiagnoseInfo> _diagnoseInfos = new List<DiagnoseInfo>();
        private DataSet _dataSet = null;

        public SH_V405_GroupDiagnose()
        {
            InitializeComponent();
        }

        public SH_V405_GroupDiagnose(Int32 id, ViewManager viewManager, ICommunicationDataService dataService,
            BlackView bv)
        {
            InitializeComponent();
            _dataService = dataService;
            m_ListenViewChanged.BoolChanged += onViewChanged;
            Dock = DockStyle.Fill;
            Margin = new Padding(0);

            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);
            GlobalParam.Instance.InitParam.RegistDataListener(this);

            _viewBtns = new List<DefButton>()
            {
                defButton5
            };

            _diagnoseInfos = new List<DiagnoseInfo>()
            {
                new DiagnoseInfo() {FalutLabel = defLabel5, PointOutLabel = defLabel6},
                new DiagnoseInfo() {FalutLabel = defLabel9, PointOutLabel = defLabel10},
                new DiagnoseInfo() {FalutLabel = defLabel24, PointOutLabel = defLabel23},
                new DiagnoseInfo() {FalutLabel = defLabel7, PointOutLabel = defLabel8},
                new DiagnoseInfo() {FalutLabel = defLabel11, PointOutLabel = defLabel12},
                new DiagnoseInfo() {FalutLabel = defLabel13, PointOutLabel = defLabel14},
                new DiagnoseInfo() {FalutLabel = defLabel15, PointOutLabel = defLabel16},
                new DiagnoseInfo() {FalutLabel = defLabel17, PointOutLabel = defLabel18},
                new DiagnoseInfo() {FalutLabel = defLabel19, PointOutLabel = defLabel20},
                new DiagnoseInfo() {FalutLabel = defLabel21, PointOutLabel = defLabel22}
            };
        }

        public void SetData(DataSet ds)
        {
            _dataSet = ds;
            SetTrainType(0);
        }

        public void SetTrainType(Int32 trainID)
        {
            String sheetName = "主车";
            if (trainID > 0)
            {
                sheetName = "从" + trainID;
            }
            DataRowCollection drc = _dataSet.Tables[sheetName].Rows;
            List<Int32> boolLogics = new List<int>();
            foreach (DataRow item in drc)
            {
                object o1 = item[0];
                boolLogics.Add(Convert.ToInt32(item[0]));
            }

            if (boolLogics.Count != _diagnoseInfos.Count)
            {
                MessageBox.Show("编组诊断提示信息屏配置文件格式错误！");
                return;
            }

            for (int i = 0; i < boolLogics.Count; i++)
            {
                _diagnoseInfos[i].BoolLogic = boolLogics[i];
                if (_dataService.ReadService.ReadOnlyBoolDictionary[boolLogics[i]])
                {
                    _diagnoseInfos[i].FalutLabel.BorderColor = Color.FromArgb(255, 200, 0);
                    _diagnoseInfos[i].FalutLabel.FontBrush = Color.Red;
                    _diagnoseInfos[i].PointOutLabel.BorderColor = Color.FromArgb(255, 200, 0);
                    _diagnoseInfos[i].PointOutLabel.FontBrush = Color.Red;
                }
                else
                {
                    _diagnoseInfos[i].FalutLabel.BorderColor = Color.White;
                    _diagnoseInfos[i].FalutLabel.FontBrush = Color.White;
                    _diagnoseInfos[i].PointOutLabel.BorderColor = Color.White;
                    _diagnoseInfos[i].PointOutLabel.FontBrush = Color.White;
                }
            }
        }

        void onViewChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
            {
                if (cmd.Key >= 800 && cmd.Key < 825) //按钮命令
                {
                    if (cmd.Value && _isShow)
                    {
                        DefButton btn = _viewBtns.Find(a => a.ID == cmd.Key);
                        if (btn != null)
                        {
                            btn.IsDown = true;
                            ViewManger.CurrentViewID = btn.ViewID;
                        }
                    }
                }
            }
        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
            {
                DiagnoseInfo di = _diagnoseInfos.Find(a => a.BoolLogic == cmd.Key);
                if (di != null)
                {
                    if (cmd.Value)
                    {
                        di.FalutLabel.BorderColor = Color.FromArgb(255, 200, 0);
                        di.FalutLabel.FontBrush = Color.Red;
                        di.PointOutLabel.BorderColor = Color.FromArgb(255, 200, 0);
                        di.PointOutLabel.FontBrush = Color.Red;
                    }
                    else
                    {
                        di.FalutLabel.BorderColor = Color.White;
                        di.FalutLabel.FontBrush = Color.White;
                        di.PointOutLabel.BorderColor = Color.White;
                        di.PointOutLabel.FontBrush = Color.White;
                    }
                }
            }
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
        }

        private void defButton5_DefClick(object sender, ButtonClickArgs e)
        {
            ViewManger.CurrentViewID = e.ViewID;
        }

        public void InvalidateNew()
        {
        }
    }
}
