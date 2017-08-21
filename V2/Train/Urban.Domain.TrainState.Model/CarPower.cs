using System.Collections.ObjectModel;
using System.Linq;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Model
{
    public class CarPower : UpdatingProvider<CarPower> ,ICarPower
    {
        private ReadOnlyCollection<CarPowerItem> m_PowerItems;
        private ReadOnlyCollection<ICarPowerItem> m_ReadonlyPowerItems;

        public ReadOnlyCollection<CarPowerItem> PowerItems
        {
            get { return m_PowerItems; }
            set
            {
                m_PowerItems = value;
                m_ReadonlyPowerItems = m_PowerItems.Cast<ICarPowerItem>().ToList().AsReadOnly();
            }
        }

        ReadOnlyCollection<ICarPowerItem> ICarPower.PowerItems
        {
            get { return m_ReadonlyPowerItems; }
        }

        public CarPower()
        {
            PowerItems = new ReadOnlyCollection<CarPowerItem>(new CarPowerItem[0]);
        }

        protected override void OnUpdating(CarPower target, IUpdateParam updateParam)
        {
            foreach (var it in PowerItems)
            {
                it.Update(updateParam);
            }
        }
    }
}