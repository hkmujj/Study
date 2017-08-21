using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.LCDM.HDX2.Entity.Model
{
    [Export]
    public class FooterModel : NotificationObject, IRaiseResourceChangedProvider
    {
        private string m_Title;
        private ObservableCollection<string> m_NewSettings;
        private ObservableCollection<string> m_CurrentSettings;

        public string Title
        {
            set
            {
                if (value == m_Title)
                {
                    return;
                }
                m_Title = value;
                RaisePropertyChanged(() => Title);
            }
            get { return m_Title; }
        }

        internal object[] SettingBuffer { set; get; }

        public ObservableCollection<string> NewSettings
        {
            set
            {
                if (Equals(value, m_NewSettings))
                {
                    return;
                }
                m_NewSettings = value;
                RaisePropertyChanged(() => NewSettings);
            }
            get { return m_NewSettings; }
        }

        public ObservableCollection<string> CurrentSettings
        {
            set
            {
                if (Equals(value, m_CurrentSettings))
                {
                    return;
                }
                m_CurrentSettings = value;
                RaisePropertyChanged(() => CurrentSettings);
            }
            get { return m_CurrentSettings; }
        }

        public void RaiseResourceChanged()
        {
            RaisePropertyChanged(() => Title);
            RaisePropertyChanged(() => NewSettings);
            RaisePropertyChanged(() => CurrentSettings);
        }
    }
}