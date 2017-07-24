using System.ComponentModel.Composition;
using MMITool.Addin.MMIDataDebugger.Controller;
using MMITool.Addin.MMIDataDebugger.Model;

namespace MMITool.Addin.MMIDataDebugger.ViewModel
{
    [Export]
    public class DataViewModel 
    {
        [ImportingConstructor]
        public DataViewModel(DataModel model, DataController controller, DataUpdateController dataUpdateController)
        {
            Model = model;
            controller.ViewModel = this;
            Controller = controller;
            m_DataUpdateController = dataUpdateController;
            dataUpdateController.ViewModel = this;
        }

        private readonly DataUpdateController m_DataUpdateController;

        public DataController Controller { get; private set; }

        public DataModel Model { get; private set; }

        public void Initalize()
        {
            Controller.Initalize();
            m_DataUpdateController.Initalize();
        }
    }
}