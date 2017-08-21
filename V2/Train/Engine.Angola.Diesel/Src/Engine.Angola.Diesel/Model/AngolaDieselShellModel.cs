using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.Angola.Diesel.Model
{
    [Export]
    public class AngolaDieselShellModel : NotificationObject
    {
        [ImportingConstructor]
        public AngolaDieselShellModel(DialModel dialModel, IndicatorModel indicatorModel)
        {
            DialModel = dialModel;
            IndicatorModel = indicatorModel;
        }

        public DialModel DialModel { private set; get; }

        public IndicatorModel IndicatorModel { private set; get; }

    }
}