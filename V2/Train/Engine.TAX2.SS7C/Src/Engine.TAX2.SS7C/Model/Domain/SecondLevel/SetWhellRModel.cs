using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.View;

namespace Engine.TAX2.SS7C.Model.Domain.SecondLevel
{
    [Export]
    public class SetWhellRModel : NotificationObject, ICaretTextModel
    {
        private float m_OldR;
        private int m_BindableCaretIndex;
        private string m_Text;

        public float OldR
        {
            get { return m_OldR; }
            set
            {
                if (value.Equals(m_OldR))
                {
                    return;
                }

                m_OldR = value;
                RaisePropertyChanged(() => OldR);
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

        /// <summary>显示文本</summary>
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