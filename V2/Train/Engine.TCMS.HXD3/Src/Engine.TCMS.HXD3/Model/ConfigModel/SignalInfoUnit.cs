using System;
using System.Diagnostics.CodeAnalysis;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [ExcelLocation("XHD3.TCMS信号信息界面内容.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class SignalInfoUnit : NotificationObject, ISetValueProvider
    {
        private SignalInfoState m_State;
        private SignalType m_SignalType;

        public SignalInfoState State
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

        [ExcelField("信号类型")]
        public SignalType SignalType
        {
            get { return m_SignalType; }
            set { m_SignalType = value; }
        }

        [ExcelField("信号内容")]
        public string SignalText { get; private set; }

        [ExcelField("信号开放")]
        public string SignalOpen { get; private set; }

        [ExcelField("信号闪烁")]
        public string SingalFliker { get; private set; }

        [ExcelField("页")]
        public int Page { get; private set; }

        [ExcelField("行")]
        public int Row { get; private set; }

        [ExcelField("列")]
        public int Column { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "SignalType":
                    Enum.TryParse(value, true, out m_SignalType);
                    break;
            }
        }
    }
}