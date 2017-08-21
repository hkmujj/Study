using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.DataAdapter.Model;

namespace Motor.ATP._300T.Subsys.Control.UserAction.StateProvider
{
    public class F2CTCS2StateProvider : DriverSelectableItemStateProviderBase
    {
        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            UpdateEnable();
            UpdateOutlineVisible();
            ATP.RegionFStateProvier.CTCS2StateProvider.PropertyChanged += CTCS2StateProviderOnPropertyChanged;
        }

        private void CTCS2StateProviderOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateEnable();
            UpdateOutlineVisible();
        }

        private void UpdateEnable()
        {
            Enabled = ATP.RegionFStateProvier.CTCS2StateProvider.Enabled;
        }

        private void UpdateOutlineVisible()
        {
            if(ATP.CTCS.CurrentCTCSType == CTCSType.CTCS2)
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
