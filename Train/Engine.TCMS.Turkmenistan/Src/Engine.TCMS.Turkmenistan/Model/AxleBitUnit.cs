using Engine.TCMS.Turkmenistan.Extension;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.Turkmenistan.Model
{
    [ExcelLocation("轴位信息表.xls", "Sheet1")]
    public class AxleBitUnit : NotificationObject, ISetValueProvider
    {
        private double m_AxleBitValue;
        private bool m_IsFault;
        private bool m_IsShortCircuit;
        private bool m_IsOpne;

        [ExcelField("是否本车")]
        public bool IsCurrent { get; set; }
        [ExcelField("轴")]
        public int Axle { get; set; }
        [ExcelField("位")]
        public int Bit { get; set; }
        [ExcelField("开路接口")]
        public string OpenRoadName { get; set; }
        [ExcelField("短路接口")]
        public string ShortCircuit { get; set; }
        [ExcelField("故障接口")]
        public string Fault { get; set; }
        [ExcelField("值接口")]
        public string FloatName { get; set; }

        public bool IsOpne
        {
            get { return m_IsOpne; }
            set
            {
                if (value == m_IsOpne)
                    return;
                m_IsOpne = value;
                RaisePropertyChanged(() => IsOpne);
            }
        }

        public bool IsShortCircuit
        {
            get { return m_IsShortCircuit; }
            set
            {
                if (value == m_IsShortCircuit)
                    return;
                m_IsShortCircuit = value;
                RaisePropertyChanged(() => IsShortCircuit);
            }
        }

        public bool IsFault
        {
            get { return m_IsFault; }
            set
            {
                if (value == m_IsFault)
                    return;
                m_IsFault = value;
                RaisePropertyChanged(() => IsFault);
            }
        }

        public double AxleBitValue
        {
            get { return m_AxleBitValue; }
            set
            {
                if (value.Equals(m_AxleBitValue))
                    return;
                m_AxleBitValue = value;
                RaisePropertyChanged(() => AxleBitValue);
            }
        }

        public void BoolDataChanged(CommunicationDataChangedArgs<bool> args)
        {
            args.UpdateIfContains(OpenRoadName, b => IsOpne = b);
            args.UpdateIfContains(ShortCircuit, b => IsShortCircuit = b);
            args.UpdateIfContains(Fault, b => IsFault = b);
        }

        public void FloatChanged(CommunicationDataChangedArgs<float> args)
        {
            args.UpdateIfContains(FloatName, f => AxleBitValue = f);
        }
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
