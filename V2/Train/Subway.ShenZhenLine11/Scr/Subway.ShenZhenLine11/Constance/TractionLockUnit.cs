using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.ShenZhenLine11.Constance
{
    [ExcelLocation("牵引封锁界面接口.xls", "Sheet1")]
    public class TractionLockUnit : NotificationObject, ISetValueProvider
    {
        private bool m_IsLock;

        /// <summary>
        /// 牵引封锁单元
        ///  1 左侧门 
        ///  2 右侧门 
        ///  3 停放制动未缓解  
        ///  4 紧急停车蘑菇拍下  
        ///  5 警惕按钮  
        ///  6 三个以上BCU 严重故障  
        ///  7 牵引制动同时存在
        ///  8 切除5个以上B09
        ///  9 两端同时占有
        /// 10 超速
        /// 11 方向矛盾
        /// 12 4个DCU严重故障
        /// 13 有制动未缓解
        /// </summary>
        [ExcelField("类型")]
        public int Type { get; set; }
        /// <summary>
        /// 所属门Index
        /// </summary>
        [ExcelField("编号")]
        public int Index { get; set; }
        /// <summary>
        /// 故障名称
        /// </summary>
        [ExcelField("逻辑号")]
        public string LogicName { get; set; }

        /// <summary>
        /// 是否牵引封锁   true  是  false 否
        /// </summary>
        public bool IsLock
        {
            get { return m_IsLock; }
            set
            {
                if (value == m_IsLock)
                {
                    return;
                }
                m_IsLock = value;
                RaisePropertyChanged(() => IsLock);
            }
        }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}