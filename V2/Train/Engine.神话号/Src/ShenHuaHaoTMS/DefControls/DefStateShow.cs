using System.Drawing;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControls;
using MMI.Facility.Interface.Extension;
using YunDa.JC.MMI.Common;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.DefControls
{
    public partial class DefStateShow : UserControl, IDataListener
    {
        private List<ILogic> _statesControls = new List<ILogic>();
        private BlackView _bv = null;
        private ICommunicationDataService _dataService;

        public DefStateShow()
        {
            InitializeComponent();
        }

        public DefStateShow(ICommunicationDataService dataService,BlackView bv)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Margin = new Padding(0);

            GlobalTimer.Instance.TimersElapsed500MS += _timer_Elapsed;

            GlobalParam.Instance.InitParam.RegistDataListener(this);

            _statesControls = new List<ILogic>()
            {
                defState1,defState2,defState3,defState4,defState5,defState6,defState7,defState8,defState9,defState10,
                defState11,defState12,defState13,defState14,defState15,defState16,defState17,defState18,defState19,defState20,
                defState21,defState22,defState23,defState24,defState25,defState26,defState27,defState28,defState29,defState30,
                defState31,defState32,defState33,defState34,defState35,defState36,defState37,defState38,defState39,defState40,
                defState41,defState42,defState43,defState44,defState45,defState46,defState47,defState48,defState49,defState50,
                defState51,defState52,defState53,defState54,defState55,defState56,defState57,defState58,defState59,defState60,
                defState61,defState62,defState63,defState64,defState65,defState66,defState67,defState68,defState69,defState70,
                defState71,defState72,defState73,defState74,defState75,defState76,defState77,defState78,defState79,defState80,
                defState81,defState82,defState83,defState84,defState85,defState86,defState87,defState88,defState89,defState90,
                defState91,defState92,defState93,defState94,defState95,defState96,defState97,defState98,defState99,defState100,
                defState101,defState102,defState103,defState104,defState105,defState106
            };
            _bv = bv;
            _dataService = dataService;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            defLabel81.DefText = ShenHuaHaoTMS.View.SH_V1_MainView.FalutPointOut;
            if (defLabel81.DefText != "")
            {
                defLabel81.BackColor = Color.Red;
            }
            else
            {
                defLabel81.BackColor = Color.Black;
            }
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            defLabel81.DefText = ShenHuaHaoTMS.View.SH_V1_MainView.FalutPointOut;
            if (defLabel81.DefText != "")
            {
                defLabel81.BackColor = Color.Red;
            }
            else
            {
                defLabel81.BackColor = Color.Black;
            }
        }

        void ReadService_FloatChanged(object sender, CommunicationDataChangedArgs<float> e)
        {
            foreach (var cmd in e.NewValue)
            {
                if (cmd.Key >= 50 && cmd.Key < 825)
                {
                    foreach (var item in _statesControls)
                    {
                        item.SetValue(cmd.Key, cmd.Value);
                    }
                }

                Int32 value = 0;
                if (_bv.LocalTrainInfo.ControlType == View.V4.ControlType.主控)
                {
                    value = Convert.ToInt32(_dataService.ReadService.ReadOnlyFloatDictionary[429]);
                        if (value == 0)
                        {
                            defLabel1.DefText = "分相距离_- -";
                        }
                        else
                        {
                            defLabel1.DefText = "分相距离_" + value + " m";
                        }
                }
                else if (_bv.LocalTrainInfo.Number == 1)
                {
                    value = Convert.ToInt32(_dataService.ReadService.ReadOnlyFloatDictionary[479]);
                    if (value == 0)
                    {
                        defLabel1.DefText = "分相距离_- -";
                    }
                    else
                    {
                        defLabel1.DefText = "分相距离_" + value + " m";
                    }
                }
                else if (_bv.LocalTrainInfo.Number == 2)
                {
                    value = Convert.ToInt32(_dataService.ReadService.ReadOnlyFloatDictionary[529]);
                    if (value == 0)
                    {
                        defLabel1.DefText = "分相距离_- -";
                    }
                    else
                    {
                        defLabel1.DefText = "分相距离_" + value + " m";
                    }
                }
                else if (_bv.LocalTrainInfo.Number == 3)
                {
                    value = Convert.ToInt32(_dataService.ReadService.ReadOnlyFloatDictionary[579]);
                    if (value == 0)
                    {
                        defLabel1.DefText = "分相距离_- -";
                    }
                    else
                    {
                        defLabel1.DefText = "分相距离_" + value + " m";
                    }
                }
            }
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

        public void InvalidateNew()
        {
            gridVisibleTableLayoutPanel7.InvokeInvalidate();
            gridVisibleTableLayoutPanel8.InvokeInvalidate();
        }

        public void Dispose()
        {
            GlobalTimer.Instance.TimersElapsed500MS -= _timer_Elapsed;

            _statesControls.ForEach(a => ((DefState) a).Dispose());
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            ReadService_FloatChanged(sender, dataChangedArgs);
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
        }
    }
}
