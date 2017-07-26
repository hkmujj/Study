using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Model.Monitor.ViewControl;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.ViewModel;

namespace Motor.ATP.Infrasturcture.Controller.Monitor
{
    public class ViewControlController : ControllerBase<MonitorViewModel>
    {
        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            try
            {
                var ress = DriverInterfaceKeys.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

                List<ViewSelector> list = new List<ViewSelector>();
                foreach (DictionaryEntry re in ress)
                {
                    var vs = new ViewSelector((string) re.Value);
                    vs.PropertyChanged += VsOnPropertyChanged;
                    list.Add(vs);
                }

                ViewModel.Model.ViewControlModel.ViewSelectors = list.AsReadOnly();
            }
            catch (Exception e)
            {
                LogMgr.Error("Can not get DriverInterfaceKeys.ResourceManager'resourceset, error={0}", e);
            }
        }

        private void VsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var vs = sender as ViewSelector;
            if (vs != null)
            {
                try
                {
                    ViewModel.Domain.UpdateDriverInterface(vs.Name);
                }
                catch (Exception)
                {
                }

            }
        }
    }
}