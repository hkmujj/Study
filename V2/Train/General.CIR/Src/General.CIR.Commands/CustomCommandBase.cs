using General.CIR.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace General.CIR.Commands
{
	public abstract class CustomCommandBase
	{
		protected IEventAggregator EventAggregator
		{
			get;
			private set;
		}

		protected CIRViewModel ViewModel
		{
			get;
			private set;
		}

		public DelegateCommand Command
		{
			get;
			protected set;
		}

		protected CustomCommandBase()
		{
			ViewModel = ServiceLocator.Current.GetInstance<CIRViewModel>();
			Command = new DelegateCommand(CommandAction);
			EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
		}

		protected abstract void CommandAction();
	}
	public abstract class CustomCommandBase<T>
	{
		protected IEventAggregator EventAggregator
		{
			get;
			private set;
		}

		protected CIRViewModel ViewModel
		{
			get;
			private set;
		}

		public DelegateCommand<T> Command
		{
			get;
			protected set;
		}

		protected CustomCommandBase()
		{
			ViewModel = ServiceLocator.Current.GetInstance<CIRViewModel>();
			Command = new DelegateCommand<T>(CommandAction);
			EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
		}

		protected abstract void CommandAction(T args);
	}
}
