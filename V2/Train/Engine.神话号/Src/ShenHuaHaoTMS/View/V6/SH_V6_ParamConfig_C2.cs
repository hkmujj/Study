using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using ShenHuaHaoTMS.Common;
using YunDa.JC.MMI.Common;
using ShenHuaHaoTMS.DefControls;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V6_ParamConfig_C2 : UserControl,IDisposable,IDataListener
    {

        private List<ILogic> _statesControls = new List<ILogic>();

        public SH_V6_ParamConfig_C2()
        {
            InitializeComponent();
        }

        public SH_V6_ParamConfig_C2(ICommunicationDataService dataService, SubsystemInitParam initParam)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Margin = new Padding(0);

            GlobalParam.Instance.InitParam.RegistDataListener(this);
            _statesControls = new List<ILogic>()
            {
                _defState_2,_defState_3,_defState_4,_defState_5,_defState_6,_defState_7,_defState_8,_defState_9,
                _defState_10,_defState_11,_defState_12,_defState_13,_defState_14,_defState_15,_defState_16,_defState_17,_defState_18,
                _defState_19,_defState_20,_defState_21,_defState_22,_defState_23,_defState_24,_defState_25,_defState_26,_defState_27,
                _defState_28,_defState_29,defState1,defState2,defState3,defState4,defState5,defState6,defState7,defState8,defState9,defState10,
                defState11,defState12,defState13,defState14,defState15,defState16,defState17,defState18,defState19,defState20,
                defState21,defState22,defState23,defState24,defState25,defState26,defState27,defState28,defState29,defState30,
                defState31,defState32
            };

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
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

        public void Dispose()
        {
            _statesControls.ForEach(a => ((DefState)a).Dispose());
        }
    }
}
