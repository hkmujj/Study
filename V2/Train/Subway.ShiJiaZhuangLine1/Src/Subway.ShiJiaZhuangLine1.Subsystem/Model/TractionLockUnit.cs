using System.Diagnostics;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{

    [ExcelLocation("牵引封锁界面内容和接口.xls", "Sheet1")]
    [DebuggerDisplay("IsLocked={IsLocked},UnitName={UnitName},IndexName={IndexName},Number={Number}")]
    public class TractionLockUnit : NotificationObject, ISetValueProvider
    {
        public static readonly TractionLockUnit Empty = new TractionLockUnit() { IndexName = string.Empty, UnitName = null };

        private bool m_IsLocked;

        [ExcelField("Name")]
        public string UnitName { set; get; }

        [ExcelField("Index")]
        public string IndexName { set; get; }
        [ExcelField("Number")]
        public string Number { get; set; }

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
            }
        }
    }
}