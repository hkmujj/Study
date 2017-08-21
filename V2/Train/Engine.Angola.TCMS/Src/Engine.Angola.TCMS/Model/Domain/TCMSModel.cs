using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.Angola.TCMS.Model.Domain
{
    [Export]
    public class TCMSModel : NotificationObject
    {
        [ImportingConstructor]
        public TCMSModel(MainData mainData,SWData swData,F4Data f4Data)
        {
            MainData = mainData;
            SWData = swData;
            F4Data = f4Data;
        }

        public MainData MainData { private set; get; }

        public SWData SWData { private set; get; }

        public F4Data F4Data { private set; get; }
    }
}