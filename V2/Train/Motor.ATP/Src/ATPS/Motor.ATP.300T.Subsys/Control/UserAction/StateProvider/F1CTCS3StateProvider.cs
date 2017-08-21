using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP._300T.Subsys.Control.UserAction.StateProvider
{
    public class F1CTCS3StateProvider : DriverSelectableItemStateProviderBase
    {
        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            UpdateEnable();
            UpdateOutlineVisible();
            ATP.RegionFStateProvier.CTCS3StateProvider.PropertyChanged += CTCS3StateProviderOnPropertyChanged;
        }

        private void CTCS3StateProviderOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateEnable();
            UpdateOutlineVisible();
        }

        private void UpdateEnable()
        {
            Enabled = ATP.RegionFStateProvier.CTCS3StateProvider.Enabled;
        }

        private void UpdateOutlineVisible()
        {
            if (ATP.CTCS.CurrentCTCSType == CTCSType.CTCS3)
            {
                OutlineVisible = true;
            }
            else
            {
                OutlineVisible = false;
            }
        }

        public override void UpdateState()
        {
            UpdateEnable();
            UpdateOutlineVisible();
        }
    }
}
