using Microsoft.Practices.Prism.ViewModel;

namespace MMI.Facility.WPFInfrastructure.Interfaces
{
    /// <summary>
    /// ControllerBase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ControllerBase<T> : NotificationObject
    {
        /// <summary>
        /// 
        /// </summary>
        public ControllerBase()
        {
            
        }

        /// <summary>
        ///  ViewModel = viewModel;
        /// </summary>
        /// <param name="viewModel"></param>
        public ControllerBase(T viewModel)
        {
            ViewModel = viewModel;
        }

        /// <summary>
        /// ViewModel
        /// </summary>
        public virtual T ViewModel { set; protected get; }

        /// <summary>
        /// Initalize
        /// </summary>
        public virtual void Initalize()
        {
            
        }
    }
}
