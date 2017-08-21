using System.ComponentModel.Composition;
using MMI.Tester.BatchDataSender.Controller;
using MMI.Tester.BatchDataSender.Model;

namespace MMI.Tester.BatchDataSender.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TestDataViewModel
    {
        [ImportingConstructor]
        public TestDataViewModel(TestDataController controller, TestDataModel model)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
        }

        public TestDataController Controller { private set; get; }

        public TestDataModel Model { private set; get; }
    }
}