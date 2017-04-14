using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using Excel.Interface;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMI.Tester.BatchDataSender.Model;
using MMI.Tester.BatchDataSender.ViewModel;

namespace MMI.Tester.BatchDataSender.Controller
{
    [Export]
    public class RootController : ControllerBase<RootViewModel>
    {
        [ImportingConstructor]
        public RootController(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<CompositePresentationEvent<TempldateDataFileItemViewModel>>().Subscribe(UpdateViewData);
        }

        public void Initalize()
        {
            UpdateViewData(ViewModel.SelectTemplateDataViewModel.BoolViewModel);
            UpdateViewData(ViewModel.SelectTemplateDataViewModel.FloatViewModel);
        }

        private void UpdateViewData(TempldateDataFileItemViewModel model)
        {
            var dt = new DataTable();
            if (!string.IsNullOrEmpty(model.File))
            {
                var excelConfig = new ExcelReaderConfig()
                {
                    File = model.File,
                    SheetNames = new List<string>() {"Sheet1"},
                    Coloumns = new List<ColoumnConfig>()
                    {
                        new ColoumnConfig() {Name = "*"}
                    }
                };
                var ds = excelConfig.Adapter();
                if (ds.Tables.Count != 0)
                {
                    dt = ds.Tables[0];
                }
            }
            switch (model.DataType)
            {
                case SendDataType.InBool:
                    ViewModel.BoolDataViewModel.Model.DataTable = dt;
                    break;
                case SendDataType.InFloat:
                    ViewModel.FloatDataViewModel.Model.DataTable = dt;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}