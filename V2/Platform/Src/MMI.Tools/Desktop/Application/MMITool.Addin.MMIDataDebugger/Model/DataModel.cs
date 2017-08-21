using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMITool.Addin.MMIDataDebugger.Config.Model;

namespace MMITool.Addin.MMIDataDebugger.Model
{
    [Export]
    public class DataModel : NotificationObject
    {

        private DataCollection m_DataCollection;

        public DataCollection DataCollection
        {
            get { return m_DataCollection; }
            set
            {
                if (Equals(value, m_DataCollection))
                    return;

                m_DataCollection = value;
                RaisePropertyChanged(() => DataCollection);
            }
        }
    }
}