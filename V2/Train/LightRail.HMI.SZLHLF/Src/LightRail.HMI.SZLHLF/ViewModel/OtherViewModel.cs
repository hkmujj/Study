using System;
using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Controller;
using MMI.Facility.Interface.Service;

namespace LightRail.HMI.SZLHLF.ViewModel
{
    [Export(typeof(OtherViewModel))]
    public class OtherViewModel : ViewModelBase, IDataListener
    {
        [ImportingConstructor]
        public OtherViewModel(OtherModel model, OtherController controller, InfoSetModel infoSetModel)
        {
            Model = model;
            InfoSetModel = infoSetModel;
            infoSetModel.ViewModel = this;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public OtherModel Model { private set; get; }


        public InfoSetModel InfoSetModel { private set; get; }

        public OtherController Controller { private set; get; }

        public void OnBoolChanged(object sender, MMI.Facility.Interface.Data.CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            throw new NotImplementedException();
        }

        public void OnDataChanged(object sender, MMI.Facility.Interface.Data.CommunicationDataChangedArgs dataChangedArgs)
        {
            throw new NotImplementedException();
        }

        public void OnFloatChanged(object sender, MMI.Facility.Interface.Data.CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            throw new NotImplementedException();
        }
    }
}
