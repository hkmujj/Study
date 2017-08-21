using System;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [ExcelLocation("HXD3.TCMS动作次数内容.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class ActionNumUnit : NotificationObject, ISetValueProvider
    {
        private static readonly Random Rd = new Random(DateTime.Now.Millisecond);

        public ActionNumUnit()
        {
            Number = Rd.Next(110, 54472);
        }

        private double m_Number;

        /// <summary>
        /// 显示的动作次数
        /// </summary>
        public double Number
        {
            get { return m_Number; }
            set
            {
                if (value.Equals(m_Number))
                {
                    return;
                }

                m_Number = value;
                RaisePropertyChanged(() => Number);
            }
        }

        [ExcelField("显示值")]
        public string Key { get; private set; }

        [ExcelField("逻辑号")]
        public string Logic { get; private set; }

        [ExcelField("位置")]
        public int Index { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}