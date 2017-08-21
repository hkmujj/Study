using System.ComponentModel.Composition;
using Other.ContactLine.JW4G.Controller;
using Other.ContactLine.JW4G.Model;

namespace Other.ContactLine.JW4G.ViewModel
{
    [Export]
    public class ContactLineViewModel
    {
        [ImportingConstructor]
        public ContactLineViewModel(ContactLineController controller, ContactLineModel model)
        {
            controller.ViewModel = this;
            Controller = controller;
            Model = model;
            //ServiceLocator.Current.GetInstance<IEventAggregator>()
            //    .GetEvent<DataChangedEvent>()
            //    .Subscribe(AllDataChanged);
        }

        //private void AllDataChanged(CommunicationDataChangedArgs obj)
        //{
        //    DataChanged(null, obj.ChangedFloats);
        //}

        /// <summary>
        /// 接触网屏数据模型
        /// </summary>
        public ContactLineModel Model { get; private set; }
        /// <summary>
        /// 接触网控制类
        /// </summary>
        public ContactLineController Controller { get; private set; }
        //public void DataChanged(object sender, CommunicationDataChangedArgs<float> args)
        //{
        //}
    }
}