using General.CIR.ViewModels;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace General.CIR.Controller
{
	public class ControllerBase<T> where T : ViewModelBase
	{
		protected IEventAggregator EventAggregator
		{
			get;
			private set;
		}

		public T ViewModel
		{
			protected get;
			set;
		}

		public ControllerBase()
		{
			EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
		}

		public virtual void Initialize()
		{
		}
	}
}
