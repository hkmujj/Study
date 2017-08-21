using System;
using System.Diagnostics.CodeAnalysis;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [ExcelLocation("HXD3.TCMS传送信息内容.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class TransferInfoUnit : NotificationObject, ISetValueProvider
    {
        private static Random m_Rd = new Random(10);
        public TransferInfoUnit()
        {
            Value = m_Rd.Next(0, 255);
        }
        private string m_Values="00";
        private TransferState m_State;
        private double m_Value;

        public string Values
        {
            get { return m_Values; }
            set
            {
                if (value.Equals(m_Values))
                {
                    return;
                }
                m_Values = value;
                RaisePropertyChanged(() => Values);
            }
        }

        public double Value
        {
            get { return m_Value; }
            set
            {
                if (value.Equals(m_Value))
                {
                    return;
                }
                m_Value = value;
                Values = ((int)m_Value).ToString("X2");
                RaisePropertyChanged(() => Value);
            }
        }

        [ExcelField("类型")]
        public TransferState State { get { return m_State; } private set { m_State = value; } }

        [ExcelField("逻辑")]
        public string LogicName { get; private set; }

        [ExcelField("位置")]
        public int Location { get; private set; }

        [ExcelField("编号")]
        public int Number { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "State":
                    Enum.TryParse(value, true, out m_State);
                    break;
            }
        }
    }
}