using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.View;

namespace Engine.TAX2.SS7C.Model.Domain.SecondLevel
{
    [Export]
    public class InputPasswordModel : NotificationObject, ICaretTextModel
    {
        private string m_InputtedPasswords;
        private string m_SelectedKeybordValue;
        private int m_CaretIndex;

        public string Text
        {
            get { return m_InputtedPasswords; }
            set
            {
                if (value == m_InputtedPasswords)
                {
                    return;
                }

                m_InputtedPasswords = value;
                RaisePropertyChanged(() => Text);
            }
        }

        public int BindableCaretIndex
        {
            get { return m_CaretIndex; }
            set
            {
                if (value == m_CaretIndex)
                {
                    return;
                }

                m_CaretIndex = value;
                RaisePropertyChanged(() => BindableCaretIndex);
            }
        }

        public string SelectedKeybordValue
        {
            get { return m_SelectedKeybordValue; }
            set
            {
                if (value == m_SelectedKeybordValue)
                {
                    return;
                }

                m_SelectedKeybordValue = value;
                RaisePropertyChanged(() => SelectedKeybordValue);
            }
        }
        
        //public int BindableCaretIndex { get; set; }

        //public string Text { get; set; }
    }
}