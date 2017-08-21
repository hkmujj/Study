using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDebugViewService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        ObservableCollection<Form> DebugFormCollection { get; }
    }
}