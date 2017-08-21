using System.Diagnostics;
using Engine.TAX2.SS7C.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.View;

namespace Engine.TAX2.SS7C.Model.Domain.SecondLevel.Details
{
    [DebuggerDisplay("Content={ItemConfig.Content}")]
    public class AccDataItem : NotificationObject, ICaretTextModel
    {
        private float m_Value;
        private string m_Text;
        private int m_BindableCaretIndex;

        [DebuggerStepThrough]
        public AccDataItem(SetAccDataItemConfig itemConfig)
        {
            ItemConfig = itemConfig;
        }

        public SetAccDataItemConfig ItemConfig { get; private set; }

        public float Value
        {
            get { return m_Value; }
            set
            {
                if (value.Equals(m_Value))
                {
                    return;
                }

                m_Value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        /// <summary>
        /// </summary>
        public int BindableCaretIndex
        {
            get { return m_BindableCaretIndex; }
            set
            {
                if (value == m_BindableCaretIndex)
                {
                    return;
                }

                m_BindableCaretIndex = value;
                RaisePropertyChanged(() => BindableCaretIndex);
            }
        }

        /// <summary>отй╬нд╠╬</summary>
        public string Text
        {
            get { return m_Text; }
            set
            {
                if (value == m_Text)
                {
                    return;
                }

                m_Text = value;
                RaisePropertyChanged(() => Text);
            }
        }
    }
}