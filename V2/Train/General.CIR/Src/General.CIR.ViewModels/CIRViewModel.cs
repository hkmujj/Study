using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using General.CIR.Controller;
using General.CIR.Interfaces;
using General.CIR.Models.Units;
using Microsoft.Practices.Prism.ViewModel;

namespace General.CIR.ViewModels
{
	[Export]
	public class CIRViewModel : NotificationObject
	{
	

		private bool m_IsStart;

		private IBtnItems m_CurrentView;

		public CIRController Controller
		{
			get; }

		[ImportMany]
		public List<Lazy<ICIRStatusChanged>> AllViewModel
		{
			get;
			private set;
		}

		public MainContentViewModel MainContentViewModel
		{
			get; }

		public SettingViewModel SettingViewModel
		{
			get; }

		[Import]
		public LineSelectViewModel LineSelectViewModel
		{
			get;
			private set;
		}

		[Import]
		public PhoneBookViewModel PhoneBookViewModel
		{
			get;
			private set;
		}

		[Import]
		public DispatchCommandViewModel DispatchCommandViewModel
		{
			get;
			private set;
		}

		[Import]
		public CalllViewModel CalllViewModel
		{
			get;
			private set;
		}

		[Import]
		public MaininstanceViewModel MaininstanceViewModel
		{
			get;
			private set;
		}

		public IBtnItems CurrentView
		{
			get
			{
				return m_CurrentView;
			}
			private set
			{
				bool flag = Equals(value, m_CurrentView);
				if (!flag)
				{
					m_CurrentView = value;
					m_CurrentView.Navigator();
					RaisePropertyChanged<IBtnItems>(() => CurrentView);
				}
			}
		}

		public bool IsStart
		{
			get
			{
				return m_IsStart;
			}
			set
			{
				bool flag = value == m_IsStart;
				if (!flag)
				{
					m_IsStart = value;
					Controller.UpdateState();
					RaisePropertyChanged<bool>(() => IsStart);
				}
			}
		}

		public string CurrentViewName
		{
			get;
			set;
		}

		[ImportingConstructor]
		public CIRViewModel(CIRController controller, SettingViewModel settingViewModel, MainContentViewModel mainContentViewModel)
		{
			controller.ViewModel = this;
			Controller = controller;
			SettingViewModel = settingViewModel;
			MainContentViewModel = mainContentViewModel;
			MainContentViewModel.TitleViewModel.TrainNumberIsRegisterChanged += delegate(bool args)
			{
				ObservableCollection<SettingItem> expr_0C = this.SettingViewModel.AllItems;
				SettingItem arg_37_0;
				if (expr_0C == null)
				{
					arg_37_0 = null;
				}
				else
				{
					
					arg_37_0 = expr_0C.FirstOrDefault();
				}
				SettingItem settingItem = arg_37_0;
				bool flag = settingItem != null;
				if (flag)
				{
					settingItem.Names = (args ? "1、车次功能号注销" : "1、车次功能号注册");
				}
			};
		}

		public void UpdateCurrentView(IBtnItems item)
		{
			Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<IBtnItems>(delegate(IBtnItems target)
			{
				this.CurrentView = target;
			}), item);
		}
	}
}
