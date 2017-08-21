using Engine.LCDM.HXD3.Config;
using Engine.LCDM.HXD3.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Engine.LCDM.HXD3.Controller;
using Engine.LCDM.HXD3.Extentions;
using Engine.LCDM.HXD3.Interfaces;
using Excel.Interface;
using MMI.Facility.Interface.Service;
using MMI.Facility.Interface.Data;
using System.Windows.Markup;
using Engine.LCDM.HXD3.Common;

namespace Engine.LCDM.HXD3.ViewModels
{
    [Export(typeof(BrakeSetting))]
    [Export(typeof(ICourceStatusChanged))]
    public class BrakeSetting : ViewModelBase, IViewModelExtention<LCDMViewModel>
    {
        [ImportingConstructor]
        public BrakeSetting(BrakeSetController controller)
        {
            var tmp = ExcelParser.Parser<InitialSet>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            InitialCollection = new ObservableCollection<InitialSet>(tmp);
            InitDicToDictionary = InitialCollection.ToDictionary(t => t.Name, t => t.Content);
            if (InitDicToDictionary.ContainsKey("定压值"))
            {
                CurPressure = int.Parse(InitDicToDictionary["定压值"]);
                NewPressure = int.Parse(InitDicToDictionary["定压值"]);
            }
            ButtonThree = Visibility.Visible;
            ButtonTwo = Visibility.Hidden;
            Controller = controller;
            controller.ViewModel = this;
        }
      
        public BrakeSetController Controller { get; private set; }

        public Visibility NewSetShow
        {
            get { return m_NewSetShow; }
            set
            {
                if (value == m_NewSetShow) return;
                m_NewSetShow = value;
                RaisePropertyChanged(() => NewSetShow);
            }
        }

        private Visibility m_NewSetShow = Visibility.Hidden;

        public Visibility CurrentSetShow
        {
            get { return m_CurrentSetShow; }
            set
            {
                if (value == m_CurrentSetShow) return;
                m_CurrentSetShow = value;
                RaisePropertyChanged(() => CurrentSetShow);
            }
        }
        private Visibility m_CurrentSetShow = Visibility.Visible;

        public float CurPressure
        {
            get { return m_CurPressure; }
            set
            {
                if (value.Equals(m_CurPressure))
                {
                    return;
                }
                m_CurPressure = value;
                RaisePropertyChanged(() => CurPressure);
            }
        }

        private float m_CurPressure;

        public float NewPressure
        {
            get { return m_NewPressure; }
            set
            {
                if (value.Equals(m_NewPressure))
                {
                    return;
                }
                m_NewPressure = value;
                RaisePropertyChanged(() => NewPressure);
            }
        }

        private float m_NewPressure;

        public bool CurOperation
        {
            get { return m_CurOperation; }
            set
            {
                if (value == m_CurOperation)
                {
                    return;
                }
                m_CurOperation = value;
                RaisePropertyChanged(() => CurOperation);
            }
        }

        private bool m_CurOperation;

        public bool NewOperation
        {
            get { return m_NewOperation; }
            set
            {
                if (value == m_NewOperation)
                {
                    return;
                }
                m_NewOperation = value;
                RaisePropertyChanged(() => NewOperation);
            }
        }

        private bool m_NewOperation;

        public bool CurInOff
        {
            get { return m_CurInOff; }
            set
            {
                if (value == m_CurInOff)
                {
                    return;
                }
                m_CurInOff = value;
                RaisePropertyChanged(() => CurInOff);
            }
        }

        private bool m_CurInOff;

        public bool NewInOff
        {
            get { return m_NewInOff; }
            set
            {
                if (value == m_NewInOff)
                {
                    return;
                }
                m_NewInOff = value;
                RaisePropertyChanged(() => NewInOff);
            }
        }

        private bool m_NewInOff;

        public bool CurSteadyInOff
        {
            get { return m_CurSteadyInOff; }
            set
            {
                if (value == m_CurSteadyInOff)
                    return;
                m_CurSteadyInOff = value;
                RaisePropertyChanged(() => CurSteadyInOff);
            }
        }
        private bool m_CurSteadyInOff;

        public bool NewSteadyInOff
        {
            get { return m_NewSteadyInOff; }
            set
            {
                if (value == m_NewSteadyInOff)
                    return;
                m_NewSteadyInOff = value;
                RaisePropertyChanged(() => NewSteadyInOff);
                ;
            }
        }
        private bool m_NewSteadyInOff;
        public bool CurReleaseStyle
        {
            get { return m_CurReleaseStyle; }
            set
            {
                if (value == m_CurReleaseStyle)
                    return;
                m_CurReleaseStyle = value;
                RaisePropertyChanged(() => CurReleaseStyle);
            }
        }
        private bool m_CurReleaseStyle;
        public bool NewReleaseStyle
        {
            get { return m_NewReleaseStyle; }
            set
            {
                if (value == m_NewReleaseStyle)
                    return;
                m_NewReleaseStyle = value;
                RaisePropertyChanged(() => NewReleaseStyle);
            }
        }
        private bool m_NewReleaseStyle;
        public bool CurTrainStyle
        {
            get { return m_CurTrainStyle; }
            set
            {
                if (value == m_CurTrainStyle)
                {
                    return;
                }
                m_CurTrainStyle = value;
                RaisePropertyChanged(() => CurTrainStyle);
            }
        }
        private bool m_CurTrainStyle;

        public bool NewTrainStyle
        {
            get { return m_NewTrainStyle; }
            set
            {
                if (value == m_NewTrainStyle)
                {
                    return;
                }
                m_NewTrainStyle = value;
                RaisePropertyChanged(() => NewTrainStyle);
            }
        }
        private bool m_NewTrainStyle;

        public bool CurWind
        {
            get { return m_CurWind; }
            set
            {
                if (value == m_CurWind)
                {
                    return;
                }
                m_CurWind = value;
                RaisePropertyChanged(() => CurWind);
            }
        }
        private bool m_CurWind;

        public bool NewWind
        {
            get { return m_NewWind; }
            set
            {
                if (value == m_NewWind)
                {
                    return;
                }
                m_NewWind = value;
                RaisePropertyChanged(() => NewWind);
            }
        }
        private bool m_NewWind;

        /// <summary>
        /// flow组件中显示的平稳投入或平稳切除
        /// </summary>
        public bool TextOfPutInStyle
        {
            get { return m_TextOfPutInStyle; }
            set
            {
                if (value == m_TextOfPutInStyle)
                {
                    return;
                }
                m_TextOfPutInStyle = value;
                RaisePropertyChanged(() => TextOfPutInStyle);
            }
        }
        private bool m_TextOfPutInStyle;

        /// <summary>
        /// flow组件中显示的直接缓解或阶段缓解
        /// </summary>
        public bool TextOfRealseStyle
        {
            get { return m_TextOfRealseStyle; }
            set
            {
                if (value == m_TextOfRealseStyle)
                {
                    return;
                }
                m_TextOfRealseStyle = value;
                RaisePropertyChanged(() => TextOfRealseStyle);
            }
        }
        private bool m_TextOfRealseStyle;
        /// <summary>
        /// flow组件中显示的补风或不补风
        /// </summary>
        public bool TextOfWindMake
        {
            get { return m_TextOfWindMake; }
            set
            {
                if (value == m_TextOfWindMake)
                {
                    return;
                }
                m_TextOfWindMake = value;
                RaisePropertyChanged(() => TextOfWindMake);
            }
        }

        private bool m_TextOfWindMake;
        public bool IsExecute
        {
            get { return m_IsExecute; }
            set
            {
                if (value == m_IsExecute)
                    return;
                m_IsExecute = value;
                RaisePropertyChanged(()=>IsExecute);
            }
        }
        private bool m_IsExecute = false;
        private Visibility m_ButtonExecute = Visibility.Hidden;
        public Visibility ButtonOne
        {
            get { return m_ButtonOne; }
            set
            {
                if (value == m_ButtonOne) return;
                m_ButtonOne = value;
                RaisePropertyChanged(() => ButtonOne);
            }
        }
        private Visibility m_ButtonOne = Visibility.Hidden;

        public Visibility ButtonTwo
        {
            get { return m_ButtonTwo; }
            set
            {
                if (value == m_ButtonTwo)
                {
                    return;
                }
                m_ButtonTwo = value;
                RaisePropertyChanged(() => ButtonTwo);
            }
        }
        private Visibility m_ButtonTwo = Visibility.Hidden;

        public Visibility ButtonThree
        {
            get { return m_ButtonThree; }
            set
            {
                if (value == m_ButtonThree)
                {
                    return;
                }
                m_ButtonThree = value;
                RaisePropertyChanged(() => ButtonThree);
            }
        }
        private Visibility m_ButtonThree = Visibility.Visible;

        public Visibility ButtonFour
        {
            get { return m_ButtonFour; }
            set
            {
                if (value == m_ButtonFour) return;
                m_ButtonFour = value;
                RaisePropertyChanged(() => ButtonFour);
            }
        }
        private Visibility m_ButtonFour = Visibility.Visible;
        public Visibility ButtonFive
        {
            get { return m_ButtonFive; }
            set
            {
                if (value == m_ButtonFive) return;
                m_ButtonFive = value;
                RaisePropertyChanged(() => ButtonFive);
            }
        }
        private Visibility m_ButtonFive = Visibility.Visible;
        public Visibility ButtonSix
        {
            get { return m_ButtonSix; }
            set
            {
                if (value == m_ButtonSix) return;
                m_ButtonSix = value;
                RaisePropertyChanged(() => ButtonSix);
            }
        }
        private Visibility m_ButtonSix = Visibility.Visible;
        public Visibility ButtonSeven
        {
            get { return m_ButtonSeven; }
            set
            {
                if (value == m_ButtonSeven) return;
                m_ButtonSeven = value;
                RaisePropertyChanged(() => ButtonSeven);
            }
        }
        private Visibility m_ButtonSeven = Visibility.Visible;

        public Visibility ButtonEight
        {
            get { return m_ButtonEight; }
            set
            {
                if (value == m_ButtonEight) return;
                m_ButtonEight = value;
                RaisePropertyChanged(() => ButtonEight);
            }
        }
        private Visibility m_ButtonEight = Visibility.Visible;

        public LCDMViewModel ViewModel { get; set; }

        public ObservableCollection<InitialSet> InitialCollection
        {
            get { return m_InitialCollection; }
            set
            {
                if (Equals(value, m_InitialCollection))
                {
                    return;
                }
                m_InitialCollection = value;
                RaisePropertyChanged(() => InitialCollection);
            }
        }
        private ObservableCollection<InitialSet> m_InitialCollection;
        public Dictionary<string, string> InitDicToDictionary;

        private readonly int[] m_BoolIndex = new int[]
        {
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.直接or阶段],                //0
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.平稳投入or平稳切除],        //
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.补风or不补风]
        };
        public override void DataChanged(object sender, CommunicationDataChangedArgs<bool> args)
        {
            if (args.NewValue.ContainsKey(m_BoolIndex[0]))
            {
                TextOfRealseStyle = args.NewValue[m_BoolIndex[0]];
            }
            if (args.NewValue.ContainsKey(m_BoolIndex[1]))
            {
                TextOfPutInStyle = args.NewValue[m_BoolIndex[1]];
            }
            if (args.NewValue.ContainsKey(m_BoolIndex[2]))
            {
                TextOfWindMake = args.NewValue[m_BoolIndex[2]];
            }
        }
        public override void Stop()
        {
            CurPressure = int.Parse(InitDicToDictionary["定压值"]);
            CurOperation=false;
            CurInOff=false;
            CurReleaseStyle = false; 
            CurWind = false; 
            TextOfPutInStyle = false; 
            TextOfRealseStyle = false; 
            TextOfWindMake = false;
            CurSteadyInOff = false;
        }
        public override void Start()
        {
            DataDefault();
        }
        private void DataDefault()
        {
            CurPressure.SendData(OutFloatKeys.列车管压力);
            false.SendData(OutBoolKeys.非操纵端);
            true.SendData(OutBoolKeys.操纵端);
            false.SendData(OutBoolKeys.切除);
            true.SendData(OutBoolKeys.投入);
            true.SendData(OutBoolKeys.客车);
            false.SendData(OutBoolKeys.货车);
            true.SendData(OutBoolKeys.平稳投入);
            false.SendData(OutBoolKeys.平稳切除);
            true.SendData(OutBoolKeys.直接缓解);
            false.SendData(OutBoolKeys.阶段缓解);
            false.SendData(OutBoolKeys.补风);
            true.SendData(OutBoolKeys.不补风);
        }
    }
}
