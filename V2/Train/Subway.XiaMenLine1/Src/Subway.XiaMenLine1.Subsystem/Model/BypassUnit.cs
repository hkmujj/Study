using System.Diagnostics;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    [ExcelLocation("旁路界面内容和接口.xls", "Sheet1")]
    [DebuggerDisplay("Content={Content}")]
    public class BypassUnit : NotificationObject, ISetValueProvider
    {
        private BypassState m_Car6BypassState;
        private BypassState m_Car1BypassState;

        [ExcelField("Content")]
        public string Content { set; get; }

        [ExcelField("车1接口_合")]
        public string Car1SwithOn { set; get; }

        [ExcelField("车1接口_分")]
        public string Car1SwithOff { set; get; }

        [ExcelField("车1接口_故障")]
        public string Car1Fault { set; get; }

        [ExcelField("车1接口_未知")]
        public string Car1Unkown { set; get; }

        [ExcelField("车6接口_合")]
        public string Car6SwithOn { set; get; }

        [ExcelField("车6接口_分")]
        public string Car6SwithOff { set; get; }

        [ExcelField("车6接口_故障")]
        public string Car6Fault { set; get; }

        [ExcelField("车6接口_未知")]
        public string Car6Unkown { set; get; }


        public BypassState Car1BypassState
        {
            set
            {
                if (value == m_Car1BypassState)
                {
                    return;
                }
                m_Car1BypassState = value;
                RaisePropertyChanged(() => Car1BypassState);
            }
            get { return m_Car1BypassState; }
        }

        public BypassState Car6BypassState
        {
            set
            {
                if (value == m_Car6BypassState)
                {
                    return;
                }
                m_Car6BypassState = value;
                RaisePropertyChanged(() => Car6BypassState);
            }
            get { return m_Car6BypassState; }
        }


        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Content":
                    Content = value;
                    break;
                case "Car1SwithOn":
                    Car1SwithOn = value;
                    break;
                case "Car1SwithOff":
                    Car1SwithOff = value;
                    break;
                case "Car1Fault":
                    Car1Fault = value;
                    break;
                case "Car1Unkown":
                    Car1Unkown = value;
                    break;

                case "Car6SwithOn":
                    Car6SwithOn = value;
                    break;
                case "Car6SwithOff":
                    Car6SwithOff = value;
                    break;
                case "Car6Fault":
                    Car6Fault = value;
                    break;
                case "Car6Unkown":
                    Car6Unkown = value;
                    break;
            }
        }
    }
}