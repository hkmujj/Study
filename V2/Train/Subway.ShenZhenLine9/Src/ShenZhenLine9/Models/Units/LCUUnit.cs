using System.Collections.Generic;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Models.Units
{
    [ExcelLocation("���ڵ���7������ϵͳ����ӿڱ�.xls", "LCUUnit")]
    public class LCUUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private LCUStatus m_Status;
        private string m_Content;

        [ExcelField("��")]
        public int Car { get; set; }
        [ExcelField("λ��")]
        public int Location { get; set; }
        [ExcelField("�Ͽ�")]
        public string Off { get; set; }
        [ExcelField("����")]
        public string Run { get; set; }
        [ExcelField("����")]
        public string Content
        {
            get { return m_Content; }
            set
            {
                if (value == m_Content)
                    return;

                m_Content = value;
                RaisePropertyChanged(() => Content);
            }
        }

        public LCUStatus Status
        {
            get { return m_Status; }
            set
            {
                if (value == m_Status)
                    return;

                m_Status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }

        /// <summary>
        /// ���ݱ任
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(Off, b =>
            {
                if (b)
                {
                    Status = LCUStatus.Off;
                }
            });
            args.UpdateIfContain(Run, b =>
            {
                if (b)
                {
                    Status = LCUStatus.Run;
                }
            });
        }
    }
}