using System;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using ES.Facility.PublicModule.IO;
using MMI.Facility.DataType.Config;
using MMI.Facility.Interface.Data;
using MMI.Facility.View.Views.Common;

namespace MMI.Facility.View.Views.DebugViews
{
    public partial class PorjectViewControl : ProjectInfoControl
    {
        public PorjectViewControl(string appName, IDataPackage dataPackage)
            : base(appName, dataPackage)
        {
            InitializeComponent();

            dataGridView_configInfo.Size = tabPage1.Size;
            dataGridView_class.Size = tabPage3.Size;
            dataGridView_object.Size = tabPage4.Size;
            dataGridView_views.Size = tabPage2.Size;

            RunningViewParam.CurrentRunningViewUnitParamChanged += args =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(InitObjectList));
                }
                else
                {
                    InitObjectList();
                }
            };

            // 设置图元对象为墨认
            tabControl1.SelectTab(3);
        }

        IniHelper _ih;
        string m_MemoFN = string.Empty;
        string m_MemoValue = string.Empty;

        /// <summary>
        /// 初始化工程信息队列
        /// </summary>
        public override void InitPorjectList()
        {
            var tmpDgv = dataGridView_configInfo;
            tmpDgv.Rows.Clear();

            AddInfoToDataGridView(tmpDgv, ConfigValueName.工程名称.ToString(), AppName);
            AddInfoToDataGridView(tmpDgv, ConfigValueName.调试模式.ToString(), Config.SystemConfig.IsDebugModel);
            AddInfoToDataGridView(tmpDgv, ConfigValueName.视图配置文件.ToString(), AppConfig.AppFileConfig.ViewConfigFile);
            AddInfoToDataGridView(tmpDgv, ConfigValueName.对象配置文件.ToString(), AppConfig.AppFileConfig.ObjectConfigFile);
            AddInfoToDataGridView(tmpDgv, ConfigValueName.视图的启动编号.ToString(), AppConfig.ActureFormViewConfig.CourseStartViewIndex);
        }

        /// <summary>
        /// 初始化视图信息队列
        /// </summary>
        public override void InitViewsList()
        {
            var tmpDgv = dataGridView_views;
            tmpDgv.Rows.Clear();

            foreach (var tmpVo in PViewsInfoDic.Select(runningViewUnitParam => runningViewUnitParam.Value.ViewConfig))
            {
                AddInfoToDataGridView(tmpDgv,
                    new object[] { tmpVo.Index, tmpVo.BgImageFile, string.Format("{0},{1},{2}", tmpVo.BgColor.R, tmpVo.BgColor.G, tmpVo.BgColor.B) });
            }

        }

        /// <summary>
        /// 初始化图元信息队列
        /// </summary>
        public override void InitClassList()
        {
            var tmpDgv = dataGridView_class;
            tmpDgv.Rows.Clear();

            foreach (var obj in PAddinObjectsLT.Values)
            {
                AddInfoToDataGridView(tmpDgv, new[] { obj.GetTypeName(), obj.GetInfo(), obj.UIObj.DllName });
            }
        }

        /// <summary>
        /// 初始化图元对象队列
        /// </summary>
        public override void InitObjectList()
        {
            var tmpDgv = dataGridView_object;
            tmpDgv.Rows.Clear();

            if (RunningViewParam.CurrentRunningViewUnitParam == null)
            {
                return;
            }

            foreach (var obj in PAddinObjectsLT.Values)
            {
                if (obj.ViewList.Contains(RunningViewParam.CurrentRunningViewUnitParam.ViewConfig.Index))
                {
                    var idx = AddInfoToDataGridView(tmpDgv, new object[] { obj.GetTypeName(), obj.MainIndex, obj.Enable });
                    if (idx != -1)
                    {
                        tmpDgv.Rows[idx].Tag = obj;
                    }
                }
            }
        }

        public int AddInfoToDataGridView(DataGridView pDGV, object objectA, object objectB)
        {
            return AddInfoToDataGridView(pDGV, new object[] { objectA.ToString(), objectB.ToString() });
        }

        public int AddInfoToDataGridView(DataGridView pDGV, object[] pObjects)
        {
            //必须在有列的状态下才添加数据
            if (pDGV.ColumnCount > 0)
            {
                return pDGV.Rows.Add(pObjects);
            }
            return -1;
        }

        public event Action<bool> InputObjectIsOn;

        /// <summary>
        /// 对象被选中,双击
        /// </summary>
        public event Action<IUIObject> ObjectSelectChanged;


        private void dataGridView_views_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var tmpIndex = dataGridView_views.SelectedRows[0].Cells[0].Value.ToString();
            var selectIndex = 0;
            if (int.TryParse(tmpIndex, out selectIndex))
            {
                //funViewsChanged(selectIndex);
                RunningViewParam.SelectCurrentRunningViewUnitParam(selectIndex);
            }
        }

        private void dataGridView_object_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_object.Rows.Count >= e.RowIndex && e.RowIndex >= 0)
            {
                var obj = (IObjectBase)dataGridView_object.Rows[e.RowIndex].Tag;
                obj.Enable = !obj.Enable;
            }
        }

        private void dataGridView_object_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView_object.EndEdit();
            if (e.RowIndex < 0)
            {
                return;
            }
            var obj = (IObjectBase)dataGridView_object.Rows[e.RowIndex].Tag;
            obj.Enable = !obj.Enable;
        }

        private void dataGridView_object_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView_object.Rows.Count >= e.RowIndex && e.RowIndex >= 0)
            {
                var obj = (IObjectBase)dataGridView_object.Rows[e.RowIndex].Tag;
                obj.Enable = !obj.Enable;
            }
        }

        private void dataGridView_object_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_object.SelectedRows.Count == 0)
            {
                return;
            }

            var obj = (IObjectBase)dataGridView_object.Rows[e.RowIndex].Tag;
            ActionUtil.OnAction(ObjectSelectChanged, obj.UIObj);
        }

        private void dataGridView_object_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
