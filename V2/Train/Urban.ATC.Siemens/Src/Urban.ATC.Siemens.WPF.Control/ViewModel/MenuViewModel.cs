using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using Urban.ATC.Siemens.WPF.Control.Constant;
using Urban.ATC.Siemens.WPF.Control.Event;
using Urban.ATC.Siemens.WPF.Interface;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        public MenuViewModel(SiemensData data)
        {
            this.Parent = data;
            ButtonDown = new DelegateCommand<string>(ButtonDownMoedth);
            ChangPage = new DelegateCommand<string>(ChangePageMothed);
            ChangContent = new DelegateCommand<string>(ChangContentMothed);
            ChangedToInput = new DelegateCommand<string>(ChangedToInputAction);
            Menu = "菜单\rMENU";
            Type = MenuColorTyep.Active;
        }

        private void ChangedToInputAction(string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                Parent.InputScreen.DisplayNumber = string.Empty;
                Parent.InputKeyBoard.InputNumbers = string.Empty;
                Parent.InputScreen.InputType = obj;
                SetTitleName("");
                EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                {
                    Region = RegionNames.General,
                    Name = ControlNames.InputScreen,
                });
            }
        }

        private void ChangContentMothed(string obj)
        {
            EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
            {
                Region = RegionNames.General,
                Name = obj,
            });
            SetTitleName(obj);

        }

        private void SetTitleName(string name)
        {
            Parent.GeneralSrceen.ChinaTitle = PageNames.GetChinaTitle(name)[0];
            Parent.GeneralSrceen.EnglishTitle = PageNames.GetChinaTitle(name)[1];
        }

        private void ChangePageMothed(string obj)
        {

            if (obj == ControlNames.SettingScreen || obj == ControlNames.GeneralScreen)
            {
                EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                {
                    Region = RegionNames.Menu,
                    Name = ControlNames.Menu
                });
            }

            EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
            {
                Region = RegionNames.MainRoot,
                Name = obj
            });
        }

        private void ButtonDownMoedth(string name)
        {
            if (name == ControlNames.MenuButtonOpen && CanDownMenu)
            {
                return;
            }

            EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
            {
                Region = RegionNames.Menu,
                Name = name
            });
            //var type = typeof(MenuName);
            //foreach (var info in type.GetFields().Where(info => info.Name.Equals(name)))
            //{
            //    Parent.MainView.NavagateTo(info.GetValue(null).ToString());
            //}
        }

        public ICommand ChangContent { get; private set; }
        public ICommand ChangedToInput { get; private set; }
        public ICommand ChangPage { private set; get; }
        public ICommand ButtonDown { private set; get; }

        public string Menu
        {
            get { return m_Menu; }
            set
            {
                if (value == m_Menu)
                {
                    return;
                }
                m_Menu = value;
                RaisePropertyChanged(() => Menu);
            }
        }
        /// <summary>
        /// 是否可以点击菜单 true 不可以 false 可以
        /// </summary>
        public bool CanDownMenu { get; set; }
        public MenuColorTyep Type
        {
            get { return m_Type; }
            set
            {
                if (value == m_Type)
                {
                    return;
                }
                m_Type = value;
                RaisePropertyChanged(() => Type);
            }
        }

        private MenuColorTyep m_Type;
        private string m_Menu;
    }
}