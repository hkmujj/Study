using System.Diagnostics;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.XiaMenLine1.Subsystem.Extension;

namespace Subway.XiaMenLine1.Subsystem.Model
{

    [ExcelLocation("牵引封锁界面内容和接口.xls", "Sheet1")]
    [DebuggerDisplay("IsLocked={IsLocked},UnitName={UnitName},IndexName={IndexName}")]
    public class TractionLockUnit : NotificationObject, ISetValueProvider
    {
        public static readonly TractionLockUnit Empty = new TractionLockUnit() { IndexName = string.Empty, UnitName = null };

        private bool m_IsLocked;

        [ExcelField("Name")]
        public string UnitName { set; get; }
        [ExcelField("Index")]
        public string IndexName { set; get; }
        [ExcelField("RowSpan")]
        public int IsRowSpan { get; set; }

        public bool IsLocked
        {
            set
            {
                if (value == m_IsLocked)
                {
                    return;
                }
                m_IsLocked = value;
                RaisePropertyChanged(() => IsLocked);
            }
            get { return m_IsLocked; }
        }

        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "UnitName":
                    UnitName = value;
                    break;
                case "IndexName":
                    IndexName = value;
                    break;
                case "IsRowSpan":
                    IsRowSpan = value.ToInt();
                    break;

            }
        }
    }
}