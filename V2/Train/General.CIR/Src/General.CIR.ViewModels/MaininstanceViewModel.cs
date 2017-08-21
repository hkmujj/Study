using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using General.CIR.Interfaces;
using General.CIR.Models;
using General.CIR.Models.Units;

namespace General.CIR.ViewModels
{
    [Export, Export(typeof(ICIRStatusChanged))]
    public class MaininstanceViewModel : ViewModelBase
    {
        //[CompilerGenerated]
        //[Serializable]
        //private sealed class <>c
        //{
        //	public static readonly MaininstanceViewModel.<>c <>9 = new MaininstanceViewModel.<>c();

        //	public static Func<MaininsatnceItem, bool> <>9__2_0;

        //	internal bool <Initaliation>b__2_0(MaininsatnceItem w)
        //	{
        //		return w.Page == 1;
        //	}
        //}

        private ObservableCollection<MaininsatnceItem> m_DisplayItems;

        private MaininsatnceItem m_SelectItem;

        public ObservableCollection<MaininsatnceItem> DisplayItems
        {
            get
            {
                return m_DisplayItems;
            }
            set
            {
                bool flag = Equals(value, m_DisplayItems);
                if (!flag)
                {
                    m_DisplayItems = value;
                    SelectItem = value.FirstOrDefault<MaininsatnceItem>();
                    RaisePropertyChanged<ObservableCollection<MaininsatnceItem>>(() => DisplayItems);
                }
            }
        }

        public MaininsatnceItem SelectItem
        {
            get
            {
                return m_SelectItem;
            }
            set
            {
                bool flag = Equals(value, m_SelectItem);
                if (!flag)
                {
                    m_SelectItem = value;
                    RaisePropertyChanged<MaininsatnceItem>(() => SelectItem);
                }
            }
        }

        public override void Initaliation()
        {

            IEnumerable<MaininsatnceItem> collection = GlobalParams.Instance.MainInsatnce.AllItems.Where(w => w.Page == 1);
            DisplayItems = new ObservableCollection<MaininsatnceItem>(collection);
        }
    }
}
