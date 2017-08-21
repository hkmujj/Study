using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.Facility.PublicModule.Extension;
using ES.Facility.PublicModule.IO;
using MMI.Facility.DataType;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Extension;

namespace MMI.Facility.View.Views.Common
{
    public partial class LogicControl : UserControl
    {
        public IAppLogicConfigCollection AppLogicConfigCollection { private set; get; }

        public LogicControl(IAppLogicConfigCollection appLogicConfigCollection)
            : this()
        {
            AppLogicConfigCollection = appLogicConfigCollection;
        }

        public LogicControl()
        {
            InitializeComponent();

        }

        private void LogicControl_Load(object sender, EventArgs e)
        {
            foreach (var appLogicConfig in AppLogicConfigCollection.AppLogicConfigDic.Values.OrderBy(o => o.Index))
            {
                dataGridView_logic.Rows.Add(new object[]
                                            {
                                                appLogicConfig.Index.ToString(CultureInfo.InvariantCulture),
                                                appLogicConfig.Enable.ToString(), 
                                                string.Join(" ", appLogicConfig.LeftDataList),
                                                appLogicConfig.LogicRunType.ToString(),
                                                string.Join(" ", appLogicConfig.RightDataList),
                                                appLogicConfig.Memo
                                            });
            }
        }

        public void Apply()
        {

            //逻辑生效

            int logicIndex = 0, logicNum = 0;

            AppLogicConfigCollection.AppLogicConfigDic.Clear();

            while (logicIndex < dataGridView_logic.Rows.Count)
            {
                var tmpDgvr = dataGridView_logic.Rows[logicIndex];
                tmpDgvr.Cells[0].Value = logicIndex.ToString(CultureInfo.InvariantCulture);

                var tmpLogicString = LogicObjectExtension.GetParaStringByType(1, (tmpDgvr.Cells[1].Value.Equals("True")) ? "1" : "0");
                tmpLogicString += LogicObjectExtension.GetParaStringByType(2, tmpDgvr.Cells[2].Value.ToString().Replace("(空)", ""));
                tmpLogicString += LogicObjectExtension.GetParaStringByType(3, tmpDgvr.Cells[3].Value.ToString().Replace("(空)", ""));
                tmpLogicString += LogicObjectExtension.GetParaStringByType(4, tmpDgvr.Cells[4].Value.ToString().Replace("(空)", ""));
                tmpLogicString += LogicObjectExtension.GetParaStringByType(5, tmpDgvr.Cells[5].Value.ToString().Replace("(空)", ""));
                tmpLogicString += LogicObjectExtension.GetParaStringByType(0, tmpDgvr.Cells[0].Value.ToString());

                tmpDgvr.Cells[0].Value = logicIndex.ToString(CultureInfo.InvariantCulture);

                var tmpLo = new LogicObject();
                if (tmpLo.InitParaFromString(tmpLogicString))
                {
                    var tmpLeftString = string.Join(" ", tmpLo.LeftDataList).Trim();
                    var tmpRightString = string.Join(" ", tmpLo.RightDataList).Trim();

                    tmpDgvr.Cells[2].Value = tmpLeftString;

                    tmpDgvr.Cells[4].Value = tmpRightString;

                    if (tmpLeftString.IsNullOrWhiteSpace() || tmpRightString.IsNullOrWhiteSpace())
                    {
                        tmpDgvr.Cells[1].Value = false.ToString();
                        tmpLo.Enable = false;
                    }

                    AppLogicConfigCollection.AppLogicConfigDic.Add(logicNum, tmpLo);

                    logicNum++;
                }
                else
                {
                    tmpDgvr.Cells[1].Value = false.ToString();
                }



                logicIndex++;
            }
        }

        /// <summary>
        /// 存盘
        /// </summary>
        public void Save()
        {
            //保存数据
            if (MessageBox.Show("该操作将修改现有类配置文件(" + AppLogicConfigCollection.ParentConfig.AppPaths.LogicConfigFile + ")，该操作不可恢复，你确定继续吗？",
                "提示",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                var tmpStringList = new List<string> { "; " + DateTime.Now.ToLongTimeString() };

                //加入注释信息

                ReadOldData(tmpStringList);

                AddNewData(tmpStringList);

                //存储文件
                FileHelper.DeleteFile(
                    AppLogicConfigCollection.ParentConfig.AppPaths.LogicConfigFile,
                    Path.GetExtension(AppLogicConfigCollection.ParentConfig.AppPaths.LogicConfigFile));

                WriteToFile(tmpStringList);

                MessageBox.Show("操作成功");
            }
        }

        public void AddRow()
        {
            dataGridView_logic.Rows.Add(new object[] { AppLogicConfigCollection.AppLogicConfigDic.Count+1, true.ToString(), string.Empty, "AND", string.Empty, "(null)" });
        }

        public void DeleteRow()
        {
            if (dataGridView_logic.SelectedCells.Count <= 0)
            {
                return;
            }

            var tmpDgvc = dataGridView_logic.SelectedCells[0];

            if (tmpDgvc == null)
            {
                return;
            }

            if (MessageBox.Show(string.Format("你确定要删除第{0}行吗？", tmpDgvc.RowIndex), "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            var tmpIndex = 0;
            if (!int.TryParse(tmpDgvc.RowIndex.ToString(CultureInfo.InvariantCulture), out tmpIndex))
            {
                return;
            }

            IAppLogicConfig tmpLOB;
            if (AppLogicConfigCollection.AppLogicConfigDic.TryGetValue(tmpIndex, out tmpLOB))
            {
                AppLogicConfigCollection.AppLogicConfigDic.Remove(tmpIndex);

                dataGridView_logic.Rows.RemoveAt(tmpDgvc.RowIndex);
            }
        }

        public void UpMove()
        {
            //上移
            if (dataGridView_logic.SelectedCells.Count <= 0)
            {
                return;
            }

            var dgvr = dataGridView_logic.Rows[dataGridView_logic.SelectedCells[0].RowIndex];

            if (dgvr.Index <= 0)
            {
                return;
            }

            for (var i = 1; i <= 5; i++)
            {
                string tmpStrA = (string) dgvr.Cells[i].Value;
                if (tmpStrA != null && tmpStrA.Equals("(空)"))
                {
                    tmpStrA = string.Empty;
                }

                string tmpStrB = (string) dataGridView_logic.Rows[dgvr.Index - 1].Cells[i].Value;
                if (tmpStrB != null && tmpStrB.Equals("(空)"))
                {
                    tmpStrB = string.Empty;
                }

                dgvr.Cells[i].Value = tmpStrB;
                dataGridView_logic.Rows[dgvr.Index - 1].Cells[i].Value = tmpStrA;
            }

            dataGridView_logic.Rows[dgvr.Index - 1].Cells[0].Selected = true;
        }

        public void DownMove()
        {

            //下移
            if (dataGridView_logic.SelectedCells.Count <= 0)
            {
                return;
            }

            var dgvr = dataGridView_logic.Rows[dataGridView_logic.SelectedCells[0].RowIndex];

            if (dgvr.Index >= dataGridView_logic.Rows.Count - 1)
            {
                return;
            }

            for (var i = 1; i <= 5; i++)
            {
                string tmpStrA = (string) dgvr.Cells[i].Value;
                if (tmpStrA != null && tmpStrA.Equals("(空)"))
                {
                    tmpStrA = string.Empty;
                }

                string tmpStrB = (string) dataGridView_logic.Rows[dgvr.Index + 1].Cells[i].Value;
                if (tmpStrB != null && tmpStrB.Equals("(空)"))
                {
                    tmpStrB = string.Empty;
                }

                dgvr.Cells[i].Value = tmpStrB;
                dataGridView_logic.Rows[dgvr.Index + 1].Cells[i].Value = tmpStrA;
            }

            dataGridView_logic.Rows[dgvr.Index + 1].Cells[0].Selected = true;
        }

        private void WriteToFile(IEnumerable<string> tmpStringList)
        {
            foreach (var tmpObjectString in tmpStringList)
            {
                FileHelper.AddTextToCurFile(
                    AppLogicConfigCollection.ParentConfig.AppPaths.LogicConfigFile,
                    tmpObjectString);
            }
        }

        private void AddNewData(List<string> tmpStringList)
        {
            tmpStringList.AddRange(
                AppLogicConfigCollection.AppLogicConfigDic
                .Select(appLogicConfig => appLogicConfig.Value.ToStringPara())
                .Where(tmp => !string.IsNullOrEmpty(tmp)));
        }

        private void ReadOldData(List<string> tmpStringList)
        {
            //读取原始文件信息
            if (File.Exists(AppLogicConfigCollection.ParentConfig.AppPaths.LogicConfigFile))
            {
                //获取注解数据
                string[] tmpStrArry = File.ReadAllLines(AppLogicConfigCollection.ParentConfig.AppPaths.LogicConfigFile, Encoding.Default);
                tmpStringList.AddRange(
                    tmpStrArry.Select(t => t.Replace('\t', ' ').Trim())
                              .Where(tmpString => tmpString.Length > 0 && tmpString[0].Equals(';')));
            }
        }
    }
}
