using System;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Project;
using Motor.LKJ._2000.Entity.Controller;
using Motor.LKJ._2000.Entity.Model;

namespace Motor.LKJ._2000.Entity.ViewModel
{
    public class LKJ2000MainViewModel : NotificationObject
    {
        private LKJ2000MainModel m_Model;

        public LKJ2000MainModel Model
        {
            private set
            {
                if (Equals(value, m_Model))
                {
                    return;
                }
                m_Model = value;
                RaisePropertyChanged(() => Model);
            }
            get { return m_Model; }
        }

        public LKJ2000MainController Controller { private set; get; }

        public SubsystemInitParam SubsystemInitParam { private set; get; }

        public LKJ2000MainViewModel(SubsystemInitParam initParam)
        {
            SubsystemInitParam = initParam;
            Model = new LKJ2000MainModel();
            Controller = new LKJ2000MainController(this);
        }
    }
}