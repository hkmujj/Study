using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Tester.BatchDataSender.Controller;
using MMI.Tester.BatchDataSender.Model;

namespace MMI.Tester.BatchDataSender.ViewModel
{
    [Export]
    public class RootViewModel : NotificationObject
    {
        [ImportingConstructor]
        public RootViewModel(RootModel model, RootController controller, TestDataViewModel boolDataViewModel, TestDataViewModel floatDataViewModel, SelectTemplateDataViewModel selectTemplateDataViewModel)
        {
            Model = model;
            Controller = controller;
            BoolDataViewModel = boolDataViewModel;
            boolDataViewModel.Model.SendDataType = SendDataType.InBool;
            FloatDataViewModel = floatDataViewModel;
            SelectTemplateDataViewModel = selectTemplateDataViewModel;
            floatDataViewModel.Model.SendDataType = SendDataType.InFloat;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public RootController Controller { private set; get; }

        public RootModel Model { private set; get; }

        public SelectTemplateDataViewModel SelectTemplateDataViewModel { get; private set; }

        public TestDataViewModel BoolDataViewModel { private set; get; }

        public TestDataViewModel FloatDataViewModel { private set; get; }

    }
}