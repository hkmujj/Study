using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using ShenHuaHaoTMS.DefControls;
using System.Collections.Generic;
using System.Windows.Forms;
using MMI.Facility.Interface.Extension;
using YunDa.JC.MMI.Common;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View.V1
{
    public partial class SH_V1_Main_C2 : UserControl, IDataListener
    {
        private List<ILogic> _statesControls = new List<ILogic>();

        public SH_V1_Main_C2()
        {
            InitializeComponent();
        }

        public SH_V1_Main_C2(ICommunicationDataService dataService)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Margin = new Padding(0);

            GlobalParam.Instance.InitParam.RegistDataListener(this);
            _statesControls = new List<ILogic>()
            {
                _defState_2,defState1,defState2,_defState_1
            };
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            foreach (var cmd in dataChangedArgs.NewValue)
            {
                foreach (var item in _statesControls)
                {
                    if (item is DefState)
                    {
                        var temp = item as DefState;
                        temp.InvokeSetState(temp, cmd.Key, cmd.Value);
                    }
                }
            }
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
        }
    }
}
