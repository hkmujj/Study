using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Interfaces;

namespace Motor.HMI.CRH380D.Models.Units
{
    /// <summary>
    ///     火灾探测器单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "FireDeviceUnit")]
    public class FireDeviceUnit : NotificationObject, ISetValueProvider, IUnit
    {
        /// <summary>
        /// 构造
        /// </summary>
        public FireDeviceUnit()
        {
            
        }
        private FireDeviceState m_State;

        /// <summary>
        ///     火灾探测器状态
        /// </summary>
        public FireDeviceState State
        {
            get { return m_State; }
            set
            {
                if (value == m_State)
                {
                    return;
                }
                m_State = value;
                RaisePropertyChanged(() => State);
            }
        }

        /// <summary>
        /// 编号
        /// </summary>
        [ExcelField("编号")]
        public int Num { get; set; }

        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [ExcelField("位置")]
        public int Location { get; set; }

        /// <summary>
        ///  发生火灾
        /// </summary>
        [ExcelField("发生火灾")]
        public string Fire { get; set; }

        
        /// <summary />
        /// <param name="propertyOrFieldName" />
        /// <param name="value" />
        public void SetValue(string propertyOrFieldName, string value)
        {
            
        }

        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(Fire, b =>
            {
                State = b ? FireDeviceState.Fire : FireDeviceState.Normal;
            });
        }
    }

    /// <summary>
    ///     火灾探测器状态
    /// </summary>
    public enum FireDeviceState
    {
        /// <summary>
        ///　发生火灾
        /// </summary>
        [Description("发生火灾")]
        Fire,
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal,
    }
}
