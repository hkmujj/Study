using General.CIR.Interfaces;
using Microsoft.Practices.Prism.ViewModel;

namespace General.CIR.ViewModels
{
	public class ViewModelBase : NotificationObject, ICIRStatusChanged
	{
		public virtual void Clear()
		{
			Initaliation();
		}

		public virtual void Initaliation()
		{
		}
	}
}
