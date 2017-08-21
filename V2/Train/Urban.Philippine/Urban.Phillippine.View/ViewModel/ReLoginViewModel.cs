using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IReLoginViewModel))]
    public class ReLoginViewModel : ViewModelBase, IReLoginViewModel
    {
        private Visibility m_Visibility;

        [ImportingConstructor]
        public ReLoginViewModel()
        {
            Confirm = new DelegateCommand(() => { RegionManager.RequestNavigate(RegionNames.MainShellRegion, ControlNames.LoginView); Visibility = Visibility.Hidden; });
            Cancel = new DelegateCommand(() => { Visibility = Visibility.Hidden; });
            ReLogin = new DelegateCommand(() => Visibility = Visibility.Visible);
            Visibility = Visibility.Hidden;
        }

        public ICommand Confirm { get; private set; }
        public ICommand Cancel { get; private set; }

        public Visibility Visibility
        {
            get { return m_Visibility; }
            set
            {
                if (value == m_Visibility) return;
                m_Visibility = value;
                RaisePropertyChanged(() => Visibility);
            }
        }

        public ICommand ReLogin { get; private set; }
    }
}
