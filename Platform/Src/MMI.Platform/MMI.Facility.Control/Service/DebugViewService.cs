using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Forms;
using MMI.Facility.Control.Data;
using MMI.Facility.DataType.Extension;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    public class DebugViewService : IDebugViewService
    {
        private Form m_MdiParent;
        public ObservableCollection<Form> DebugFormCollection { get; private set; }

        public Form MdiParent
        {
            set
            {
                if (m_MdiParent != value)
                {
                    m_MdiParent = value;
                    UpdateMidParent();
                }
            }
            get { return m_MdiParent; }
        }

        public DebugViewService()
        {
            DebugFormCollection = new ObservableCollection<Form>();
            DebugFormCollection.CollectionChanged += DebugFormCollectionOnCollectionChanged;
        }

        private void DebugFormCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            UpdateMidParent();

            UpdateBounds();
        }

        private void UpdateBounds()
        {
            foreach (var form in DebugFormCollection)
            {
                form.Bounds = ConfigManager.Instance.Config.DebugWindowConfig.FirstOrDefaultRectangle(form.GetType());
            }
        }

        private void UpdateMidParent()
        {
            foreach (var debugForm in DebugFormCollection)
            {
                SetMdiParent(debugForm);
            }
        }

        private void SetMdiParent(Form debugForm)
        {
            debugForm.MdiParent = MdiParent.IsMdiContainer ? MdiParent : null;
        }
    }
}