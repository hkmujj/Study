using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using MMI.Facility.DataType.View.Control;
using MMI.Facility.DataType.View.EventArg;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Running;

namespace MMI.Facility.View.Views.Common
{
    public partial class ChildAttributeControl : UserControl, IChildAttributeControl
    {
        protected ChildAttributeControl()
        {
            InitializeComponent();
        }

        public ChildAttributeControl(IDataPackage dataPackage, string appName)
            : this()
        {
            Initalize(dataPackage, appName);
        }


        private bool m_IsDgvBoolDown = false;
        private bool m_IsDgvFloatDown = false;


        public IDataPackage DataPackage { get; private set; }

        /// <summary>
        /// 保存实例化对象，key位顺序编号
        /// m_DataPackage.AddInLoader.ProjetAddinInstanceDic[m_DataPackage.Config.MainAppConfig.AppName];
        /// </summary>
        public IDictionary<string, IObjectBase> PAddinObjectsDic { private set; get; }


        public IConfig Config { private set; get; }

        public IAppConfig AppConfig { get; private set; }

        /// <summary>
        /// 视图信息列表
        /// m_DataPackage.RunningParam.RunningViewParamDic[m_DataPackage.Config.MainAppConfig.AppName].ViewUnitParamDic;
        /// </summary>
        public IDictionary<int, IRunningViewUnitParam> PViewsInfoDic { private set; get; }

        public string AppName { get; private set; }

        public event EventHandler<AttributeValueDoubleClickEventArg> BoolObjectAttribeFormDoubelClick;

        public event EventHandler<AttributeValueDoubleClickEventArg> FloatObjectattributeFormDoubleClick;


        private IObjectBase m_CurrentShowObj;

        private void Initalize(IDataPackage dataPackage, string appName)
        {
            AppName = appName;
            DataPackage = dataPackage;

            // TODO extension
            PAddinObjectsDic = DataPackage.AddInLoader.ProjetAddinInstanceDic[AppName];
            PViewsInfoDic =
                DataPackage.RunningParam.AppRunningParamDictionary[AppName].RunningViewParam.ViewUnitParamDic;
            Config = DataPackage.Config;
            AppConfig = Config.AppConfigs.FirstOrDefault(f => f.AppName == appName);
        }

        public void Select(IUIObject obj)
        {
            if (obj == null)
            {
                return;
            }

            var objectIndex = obj.ObjectKey;
            if (PAddinObjectsDic == null)
            {
                return;
            }

            m_CurrentShowObj = null;
            if (PAddinObjectsDic.TryGetValue(objectIndex, out m_CurrentShowObj))
            {
                Focus();

                Text = string.Format(" {0} --- {1} : {2}", AppName, m_CurrentShowObj.GetTypeName(), obj.MainIndex);

                #region ::::::: 基础表填充 :::::::

                RefreshBoolDataGridView(m_CurrentShowObj);

                RefreshFloatDataGridView(m_CurrentShowObj);

                RefreshViewDataGridView(m_CurrentShowObj);

                RefreshParaDataGridView(m_CurrentShowObj);

                #endregion
            }
        }

        private void RefreshBoolDataGridView(IObjectBase tmpIob)
        {
            //填充输入bool表
            dataGridView_inBool.Rows.Clear();
            List<int> tmpIntList = tmpIob.UIObj.InBoolList;
            //获取预定的行数
            for (var i = 0; i < tmpIob.UIObj.InBoolList.Count; i++)
            {
                if (i < tmpIntList.Count)
                {
                    dataGridView_inBool.Rows.Add(new object[]
                    {
                        i.ToString(),
                        tmpIntList[i].ToString(),
                        tmpIob.GetParaInfo(ParaType.inBool, tmpIob.UIObj.InBoolList[i])
                    });
                }
                else
                {
                    dataGridView_inBool.Rows.Add(new object[]
                    {
                        i.ToString(),
                        "0",
                        tmpIob.GetParaInfo(ParaType.inBool, tmpIob.UIObj.InBoolList[i])
                    });
                }
            }
        }

        private void RefreshViewDataGridView(IObjectBase tmpIob)
        {
            //填充视图表
            dataGridView_other.Rows.Clear();

            if (PViewsInfoDic != null)
            {
                foreach (var viewObj in PViewsInfoDic.Values)
                {
                    dataGridView_other.Rows.Add(new object[]
                    {
                        viewObj.ViewConfig.Index,
                        viewObj.Objects.Contains(tmpIob)
                    });
                }
            }


        }

        private void RefreshParaDataGridView(IObjectBase tmpIob)
        {
            //填充配置信息表
            dataGridView_para.Rows.Clear();
            var tmpStringList = tmpIob.UIObj.ParaList;
            //获取预订的行数
            for (var i = 0; i < tmpIob.UIObj.ParaList.Count; i++)
            {
                if (i < tmpStringList.Count)
                {
                    dataGridView_para.Rows.Add(new object[]
                    {
                        i.ToString(),
                        tmpStringList[i],
                        tmpIob.GetParaInfo(ParaType.uiPara, i)
                    });
                }
                else
                {
                    dataGridView_para.Rows.Add(new object[]
                    {
                        i.ToString(),
                        "",
                        tmpIob.GetParaInfo(ParaType.uiPara, i)
                    });
                }
            }
        }

        private void RefreshFloatDataGridView(IObjectBase tmpIob)
        {
            List<int> tmpIntList = tmpIob.UIObj.InFloatList;
            //填充输入float表
            dataGridView_inFloat.Rows.Clear();
            //获取预订的行数
            for (var i = 0; i < tmpIob.UIObj.InFloatList.Count; i++)
            {
                if (i < tmpIntList.Count)
                {
                    dataGridView_inFloat.Rows.Add(new object[]
                    {
                        i.ToString(),
                        tmpIntList[i].ToString(),
                        tmpIob.GetParaInfo(ParaType.inFloat, tmpIob.UIObj.InFloatList[i])
                    });
                }
                else
                {
                    dataGridView_inFloat.Rows.Add(new object[]
                    {
                        i.ToString(),
                        "0",
                        tmpIob.GetParaInfo(ParaType.inFloat, tmpIob.UIObj.InFloatList[i])
                    });
                }
            }

            //TODO replace ,, unpractical
        }

        private void dataGridView_inBool_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 || m_CurrentShowObj == null)
            {
                return;
            }

            m_IsDgvBoolDown = true;
            SetBoolValue(e.RowIndex, 1, m_IsDgvBoolDown);
        }

        private void dataGridView_inBool_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || m_CurrentShowObj == null)
            {
                return;
            }

            var value = dataGridView_inBool.Rows[e.RowIndex].Cells[1].Value.ToString();
            var index = Convert.ToInt32(value);

            HandleUtil.OnHandle(BoolObjectAttribeFormDoubelClick, this,
                new AttributeValueDoubleClickEventArg(AppName, index));

        }


        private void dataGridView_inFloat_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || m_CurrentShowObj == null)
            {
                return;
            }

            var value = dataGridView_inFloat.Rows[e.RowIndex].Cells[1].Value.ToString();
            var index = Convert.ToInt32(value);

            HandleUtil.OnHandle(FloatObjectattributeFormDoubleClick, this,
                new AttributeValueDoubleClickEventArg(AppName, index));
        }

        private void dataGridView_inFloat_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 0 || m_CurrentShowObj == null)
            {
                return;
            }

            m_IsDgvFloatDown = true;

            SetFloatValue(e.RowIndex, 1, m_IsDgvFloatDown);
        }

        private void dataGridView_inBool_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!m_IsDgvBoolDown || m_CurrentShowObj == null)
            {
                return;
            }

            m_IsDgvBoolDown = false;

            SetBoolValue(e.RowIndex, 1, m_IsDgvBoolDown);

        }

        private void dataGridView_inFloat_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!m_IsDgvFloatDown || m_CurrentShowObj == null)
            {
                return;
            }

            m_IsDgvFloatDown = false;

            SetFloatValue(e.RowIndex, 1, m_IsDgvFloatDown);

        }

        private void dataGridView_other_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView_other.EndEdit();
        }

        private void SetFloatValue(int nRowIndex, int nCellIndex, bool isDown)
        {
            var tmpStr = dataGridView_inFloat.Rows[nRowIndex].Cells[nCellIndex].Value.ToString();
            var tmpInt = 0;
            if (int.TryParse(tmpStr, out tmpInt))
            {
                if (m_CurrentShowObj != null)
                {
                    if (isDown)
                    {
                        m_CurrentShowObj.append_postCmd(CmdType.SetInFloatValue, tmpInt, 0,
                            m_CurrentShowObj.FloatList[tmpInt] + 1);
                    }
                    else
                    {
                        m_CurrentShowObj.append_postCmd(CmdType.SetInFloatValue, tmpInt, 0,
                            m_CurrentShowObj.FloatList[tmpInt] - 1);
                    }
                }
            }
        }

        private void SetBoolValue(int nRowIndex, int nCellIndex, bool isDown)
        {
            var tmpStr = dataGridView_inBool.Rows[nRowIndex].Cells[nCellIndex].Value.ToString();
            var tmpInt = 0;
            if (int.TryParse(tmpStr, out tmpInt))
            {

                if (m_CurrentShowObj != null)
                {
                    if (tmpInt < 0)
                        tmpInt = tmpInt*-1;
                    if (isDown)
                    {
                        m_CurrentShowObj.append_postCmd(CmdType.SetInBoolValue, tmpInt,
                            m_CurrentShowObj.BoolList[tmpInt] ? 0 : 1, 0f);
                    }
                    else
                    {
                        m_CurrentShowObj.append_postCmd(CmdType.SetInBoolValue, tmpInt,
                            m_CurrentShowObj.BoolList[tmpInt] ? 0 : 1, 0f);
                    }
                }
            }
        }
    }
}
