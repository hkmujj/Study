using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    [Export]
    public class TractionLockModel : NotificationObject
    {
        private List<TractionLockUnit> m_TractionLockUnitCollection;
        private ObservableCollection<TractionLockUnit> m_TractionLockUnitCollectionPartLeft;
        private ObservableCollection<TractionLockUnit> m_TractionLockUnitCollectionPartRight;

        public const int UnitCountPerPage = 12;

        public List<TractionLockUnit> TractionLockUnitCollection
        {
            set
            {
                if (Equals(value, m_TractionLockUnitCollection))
                {
                    return;
                }
                m_TractionLockUnitCollection = value;
                TractionLockUnitCollectionPartLeft =
                    new ObservableCollection<TractionLockUnit>(value.Take(UnitCountPerPage));
                TractionLockUnitCollectionPartRight =
                    new ObservableCollection<TractionLockUnit>(value.Skip(UnitCountPerPage).Take(UnitCountPerPage));
            }
            get { return m_TractionLockUnitCollection; }
        }

        public ObservableCollection<TractionLockUnit> TractionLockUnitCollectionPartLeft
        {
            set
            {
                if (Equals(value, m_TractionLockUnitCollectionPartLeft))
                {
                    return;
                }
                m_TractionLockUnitCollectionPartLeft = value;
                RaisePropertyChanged(() => TractionLockUnitCollectionPartLeft);
            }
            get { return m_TractionLockUnitCollectionPartLeft; }
        }

        public ObservableCollection<TractionLockUnit> TractionLockUnitCollectionPartRight
        {
            set
            {
                if (Equals(value, m_TractionLockUnitCollectionPartRight))
                {
                    return;
                }
                m_TractionLockUnitCollectionPartRight = value;
                if (TractionLockUnitCollectionPartRight.Count < UnitCountPerPage)
                {
                    TractionLockUnitCollectionPartRight.AddRange(Enumerable.Repeat(TractionLockUnit.Empty,
                        UnitCountPerPage - m_TractionLockUnitCollectionPartRight.Count));
                }
                RaisePropertyChanged(() => TractionLockUnitCollectionPartRight);
            }
            get { return m_TractionLockUnitCollectionPartRight; }
        }

    }
}