using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.WuHanLine6.GIS.Config
{
    [ExcelLocation("站点名称表.xls", "Sheet1")]
    public class StationName : NotificationObject, ISetValueProvider
    {
        private bool m_IsNext;
        private bool m_IsPast;
        private bool m_IsTransfer;
        private bool m_IsSkip;

        /// <summary>
        /// 站点编号
        /// </summary>
        [ExcelField("编号", true)]
        public int Index { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        [ExcelField("中文名称")]
        public string ChineseName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [ExcelField("英文名称")]
        public string EnglishName { get; set; }

        /// <summary>
        /// 站点是否已经经过
        /// </summary>
        public bool IsPast
        {
            get { return m_IsPast; }
            set
            {
                if (value == m_IsPast)
                {
                    return;
                }
                m_IsPast = value;
                RaisePropertyChanged(() => IsPast);
            }
        }

        /// <summary>
        /// 是下一站
        /// </summary>
        public bool IsNext
        {
            get { return m_IsNext; }
            set
            {
                if (value == m_IsNext)
                {
                    return;
                }
                m_IsNext = value;
                RaisePropertyChanged(() => IsNext);
            }
        }

        /// <summary>
        /// 是否是中转站
        /// </summary>
        [ExcelField("是否是中转站")]
        public bool IsTransfer
        {
            get { return m_IsTransfer; }
            set
            {
                if (value == m_IsTransfer)
                {
                    return;
                }
                m_IsTransfer = value;
                RaisePropertyChanged(() => IsTransfer);
            }
        }

        /// <summary>
        ///中转站线路号
        /// </summary>
        [ExcelField("中转站线路号")]
        public int TransferLineIndex { get; set; }

        /// <summary>
        /// 站点是否不经过
        /// </summary>
        public bool IsSkip
        {
            get { return m_IsSkip; }
            set
            {
                if (value == m_IsSkip)
                {
                    return;
                }
                m_IsSkip = value;
                RaisePropertyChanged(() => IsSkip);
            }
        }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}