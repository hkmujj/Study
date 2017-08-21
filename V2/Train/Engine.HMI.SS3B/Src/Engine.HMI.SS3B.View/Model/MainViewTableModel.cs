using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.HMI.SS3B.View.Model
{
    public class MainViewTableModel : NotificationObject
    {
        private ObservableCollection<MainViewTableItemModel> m_Items;

        public MainViewTableModel()
        {
            Items = new ObservableCollection<MainViewTableItemModel>()
            {
                new MainViewTableItemModel() {Name = "1", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "11", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "21", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "31", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "1", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "11", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "21", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "31", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "1", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "11", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "21", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "31", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "1", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "11", Info1 = "2", MineInfo = "3"},
                new MainViewTableItemModel() {Name = "21", Info1 = "2", MineInfo = "3"},
             
            };
        }
        public ObservableCollection<MainViewTableItemModel> Items
        {
            set
            {
                if (Equals(value, m_Items))
                {
                    return;
                }
                m_Items = value;
                RaisePropertyChanged(() => Items);
            }
            get { return m_Items; }
        }
    }
}