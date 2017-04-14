using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using Microsoft.Practices.Prism.ViewModel;

namespace MMI.Tester.BatchDataSender.Model
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TestDataModel : NotificationObject
    {
        private DataTable m_DataTable;
        private int m_CurrentColumnIndex;
        private SendDataType m_SendDataType;

        public List<int> IndexTable { get; private set; }

        public SendDataType SendDataType
        {
            set
            {
                if (value == m_SendDataType)
                {
                    return;
                }
                m_SendDataType = value;
                RaisePropertyChanged(() => SendDataType);
            }
            get { return m_SendDataType; }
        }

        public DataTable DataTable
        {
            set
            {
                if (Equals(value, m_DataTable))
                {
                    return;
                }
                m_DataTable = value;
                UpdateIndexTable();
                RaisePropertyChanged(() => DataTable);
            }
            get { return m_DataTable; }
        }

        private void UpdateIndexTable()
        {
            IndexTable = new List<int>();
            foreach (DataRow row in DataTable.Rows)
            {
                IndexTable.Add(Convert.ToInt32(row["Index"]));
            }
        }

        public int CurrentColumnIndex
        {
            set
            {
                if (value == m_CurrentColumnIndex)
                {
                    return;
                }
                m_CurrentColumnIndex = value;
                RaisePropertyChanged(() => CurrentColumnIndex);
                RaisePropertyChanged(() => CurrentColumnName);
                RaisePropertyChanged(() => SendDisplayInfo);
                RaisePropertyChanged(() => SendToolTip);
            }
            get { return m_CurrentColumnIndex; }
        }

        public string CurrentColumnName
        {
            get { return CurrentColumnIndex < 0 ? string.Empty : DataTable.Columns[CurrentColumnIndex].ColumnName; }
        }

        public string SendDisplayInfo
        {
            get
            {
                return CurrentColumnIndex < 2
                    ? "请选择可发送列来发送"
                    : string.Format("发送列:{0} {1}", CurrentColumnIndex, CurrentColumnName);
            }
        }

        public string SendToolTip
        {
            get
            {
                if (!BatchSenderParam.Instance.Param.SubsystemParam.DataPackage.Config.SystemConfig.IsDebugModel)
                {
                    return "非调试模式下不能进行发送数据操作";
                }
                return SendDisplayInfo;
            }
        }
    }
}