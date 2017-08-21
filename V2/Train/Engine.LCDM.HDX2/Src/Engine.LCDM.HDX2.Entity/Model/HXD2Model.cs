using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.LCDM.HDX2.Entity.Model
{
    [Export]
    public class HXD2Model : NotificationObject, IRaiseResourceChangedProvider
    {

        private IStateInterface m_StateInterface;
        private FooterModel m_FooterModel;

        public IStateInterface StateInterface
        {
            set
            {
                if (Equals(value, m_StateInterface))
                {
                    return;
                }
                m_StateInterface = value;
                RaisePropertyChanged(() => StateInterface);
            }
            get { return m_StateInterface; }
        }

        [Import]
        public FooterModel FooterModel
        {
            set
            {
                if (Equals(value, m_FooterModel))
                {
                    return;
                }
                m_FooterModel = value;
                RaisePropertyChanged(() => FooterModel);
            }
            get { return m_FooterModel; }
        }

        public void RaiseResourceChanged()
        {
            RaisePropertyChanged(() => StateInterface);
            if (StateInterface != null)
            {
                StateInterface.RaiseResourceChanged();
            }

            RaisePropertyChanged(() => FooterModel);
            FooterModel.RaiseResourceChanged();
        }
    }
}