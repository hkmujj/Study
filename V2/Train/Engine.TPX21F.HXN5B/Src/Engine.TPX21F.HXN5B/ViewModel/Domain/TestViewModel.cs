using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Controller.Domain.Test;
using Engine.TPX21F.HXN5B.Controller.Domain.Test.Detail;
using Engine.TPX21F.HXN5B.Model.Domain.Test;
using Engine.TPX21F.HXN5B.Model.Domain.Test.Detail;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.ViewModel.Domain
{
    [Export]
    public class TestViewModel : NotificationObject
    {
        [ImportingConstructor]
        public TestViewModel(TestModel model, TestController controller, TestSelfViewModel testSelfViewModel)
        {
            Model = model;
            Controller = controller;
            TestSelfViewModel = testSelfViewModel;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public TestController Controller { private set; get; }

        public TestModel Model { private set; get; }

        public TestSelfViewModel TestSelfViewModel { get; private set; }
    }

    [Export]
    public class TestSelfViewModel : NotificationObject
    {
        [ImportingConstructor]
        public TestSelfViewModel(TestSelfItemsModel model, TestSelfItemsController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public TestSelfItemsController Controller { get; private set; }

        public TestSelfItemsModel Model { get; private set; }   
    }
}