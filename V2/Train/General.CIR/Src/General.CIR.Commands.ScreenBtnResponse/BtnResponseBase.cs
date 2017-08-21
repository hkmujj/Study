using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Threading;
using General.CIR.Resource;
using General.CIR.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public abstract class BtnResponseBase
	{
	

		private static readonly List<string> JumpList;

		public static DateTime OperateTime;

		private static readonly DispatcherTimer Timer;

		protected static CIRViewModel ViewModel { get; }

		protected IEventAggregator EventAggregator
		{
			get;
			private set;
		}

		protected IRegionManager RegionManager
		{
			get;
			private set;
		}

		public ICommand DownCommand
		{
			get;
			private set;
		}

		public ICommand UpCommand
		{
			get;
			private set;
		}

		static BtnResponseBase()
		{
			JumpList = new List<string>
			{
				BtnItemKeys.主界面GSMR,
				BtnItemKeys.黑屏,
				BtnItemKeys.启动,
				BtnItemKeys.主界面信息区域中心信息,
				BtnItemKeys.主界面信息区域呼叫接通,
				BtnItemKeys.主界面信息区域正在呼叫,
				BtnItemKeys.主界面信息区域空白区域,
				BtnItemKeys.报警发送报警,
				BtnItemKeys.报警发送报警命令,
				BtnItemKeys.报警报警确认,
				BtnItemKeys.报警报警解除,
				BtnItemKeys.报警报警解除命令,
				BtnItemKeys.报警紧急确认,
				BtnItemKeys.报警紧急运行,
				BtnItemKeys.主界面电话号码输入
			};
			ViewModel = ServiceLocator.Current.GetInstance<CIRViewModel>();
			Timer = new DispatcherTimer();
			Timer.Tick += Timer_Tick;
			Timer.Interval = new TimeSpan(0, 0, 0, 1);
			Timer.Start();
		}

		private static void Timer_Tick(object sender, EventArgs e)
		{
			
			var flag = ViewModel?.CurrentView == null;
			if (!flag)
			{
				var flag2 = JumpList.TrueForAll(f=>!f.Equals(ViewModel.CurrentView.Keys));
				if (flag2)
				{
					var flag3 = (DateTime.Now - OperateTime).TotalSeconds > 5.0;
					if (flag3)
					{
						ViewModel.Controller.NavigatorToKey(BtnItemKeys.主界面GSMR);
					}
				}
			}
		}

		protected BtnResponseBase()
		{
			EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
			RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
			DownCommand = new DelegateCommand(ClickDown);
			UpCommand = new DelegateCommand(ClickUp);
		}

		public abstract void ClickUp();

		public abstract void ClickDown();

		protected static void Response(string key)
		{
			ViewModel.Controller.NavigatorToKey(key);
		}
	}
}
