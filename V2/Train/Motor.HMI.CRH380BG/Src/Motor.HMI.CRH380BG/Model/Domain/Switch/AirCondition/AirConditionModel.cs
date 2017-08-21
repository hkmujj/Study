using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Model.Domain.Switch.AirCondition
{
    [Export]
    public class AirConditionModel : NotificationObject
    {
        private AirConditionState m_TrainAirConditionState;
        private AirConditionState m_EMU1AirConditionState;
        private AirConditionState m_OnecarAirCondition1State;
        private AirConditionState m_OnecarAirCondition2State;
        private AirConditionState m_OnecarAirCondition3State;
        private AirConditionState m_OnecarAirCondition4State;
        private AirConditionState m_OnecarAirCondition5State;
        private AirConditionState m_OnecarAirCondition6State;
        private AirConditionState m_OnecarAirCondition7State;
        private AirConditionState m_OnecarAirCondition8State;
        private AirConditionTemperatureState m_TemPeratureState11;
        private AirConditionTemperatureState m_TemPeratureState12;
        private AirConditionTemperatureState m_TemPeratureState21;
        private AirConditionTemperatureState m_TemPeratureState22;
        private AirConditionTemperatureState m_TemPeratureState31;
        private AirConditionTemperatureState m_TemPeratureState32;
        private AirConditionTemperatureState m_TemPeratureState41;
        private AirConditionTemperatureState m_TemPeratureState42;
        private AirConditionTemperatureState m_TemPeratureState51;
        private AirConditionTemperatureState m_TemPeratureState52;
        private AirConditionTemperatureState m_TemPeratureState61;
        private AirConditionTemperatureState m_TemPeratureState62;
        private AirConditionTemperatureState m_TemPeratureState71;
        private AirConditionTemperatureState m_TemPeratureState72;
        private AirConditionTemperatureState m_TemPeratureState81;
        private AirConditionTemperatureState m_TemPeratureState82;
        private bool m_AllTrainAirIsSelect;
        private bool m_TrainAirIsSelect;
        private bool m_EMU1AirIsSelect;
        private bool m_OneCarAir1IsSelect;
        private bool m_OneCarAir2IsSelect;
        private bool m_OneCarAir3IsSelect;
        private bool m_OneCarAir4IsSelect;
        private bool m_OneCarAir5IsSelect;
        private bool m_OneCarAir6IsSelect;
        private bool m_OneCarAir7IsSelect;
        private bool m_OneCarAir8IsSelect;
        private int m_SelectNum;

        public AirConditionModel()
        {
            IsSelect();
            SelectNum = 0;
        }

        public int SelectNum
        {
            get { return m_SelectNum; }
            set
            {
                if (value == m_SelectNum)
                {
                    return;
                }
                m_SelectNum = value;
                IsSelect();
                RaisePropertyChanged(() => SelectNum);
            }
        }

        public AirConditionState TrainAirConditionState
        {
            set
            {
                if (value == m_TrainAirConditionState)
                {
                    return;
                }

                m_TrainAirConditionState = value;
                RaisePropertyChanged(() => TrainAirConditionState);
            }
            get { return m_TrainAirConditionState; }
        }
        public AirConditionState EMU1AirConditionState
        {
            set
            {
                if (value == m_EMU1AirConditionState)
                {
                    return;
                }

                m_EMU1AirConditionState = value;
                RaisePropertyChanged(() => EMU1AirConditionState);
            }
            get { return m_EMU1AirConditionState; }
        }
        public AirConditionState OnecarAirCondition1State
        {
            set
            {
                if (value == m_OnecarAirCondition1State)
                {
                    return;
                }

                m_OnecarAirCondition1State = value;
                RaisePropertyChanged(() => OnecarAirCondition1State);
            }
            get { return m_OnecarAirCondition1State; }
        }
        public AirConditionState OnecarAirCondition2State
        {
            set
            {
                if (value == m_OnecarAirCondition2State)
                {
                    return;
                }

                m_OnecarAirCondition2State = value;
                RaisePropertyChanged(() => OnecarAirCondition2State);
            }
            get { return m_OnecarAirCondition2State; }
        }
        public AirConditionState OnecarAirCondition3State
        {
            set
            {
                if (value == m_OnecarAirCondition3State)
                {
                    return;
                }

                m_OnecarAirCondition3State = value;
                RaisePropertyChanged(() => OnecarAirCondition3State);
            }
            get { return m_OnecarAirCondition3State; }
        }
        public AirConditionState OnecarAirCondition4State
        {
            set
            {
                if (value == m_OnecarAirCondition4State)
                {
                    return;
                }

                m_OnecarAirCondition4State = value;
                RaisePropertyChanged(() => OnecarAirCondition4State);
            }
            get { return m_OnecarAirCondition4State; }
        }
        public AirConditionState OnecarAirCondition5State
        {
            set
            {
                if (value == m_OnecarAirCondition5State)
                {
                    return;
                }

                m_OnecarAirCondition5State = value;
                RaisePropertyChanged(() => OnecarAirCondition5State);
            }
            get { return m_OnecarAirCondition5State; }
        }
        public AirConditionState OnecarAirCondition6State
        {
            set
            {
                if (value == m_OnecarAirCondition6State)
                {
                    return;
                }

                m_OnecarAirCondition6State = value;
                RaisePropertyChanged(() => OnecarAirCondition6State);
            }
            get { return m_OnecarAirCondition6State; }
        }
        public AirConditionState OnecarAirCondition7State
        {
            set
            {
                if (value == m_OnecarAirCondition7State)
                {
                    return;
                }

                m_OnecarAirCondition7State = value;
                RaisePropertyChanged(() => OnecarAirCondition7State);
            }
            get { return m_OnecarAirCondition7State; }
        }
        public AirConditionState OnecarAirCondition8State
        {
            set
            {
                if (value == m_OnecarAirCondition8State)
                {
                    return;
                }

                m_OnecarAirCondition8State = value;
                RaisePropertyChanged(() => OnecarAirCondition8State);
            }
            get { return m_OnecarAirCondition8State; }
        }

        public AirConditionTemperatureState TemPeratureState11
        {
            set
            {
                if (value == m_TemPeratureState11)
                {
                    return;
                }

                m_TemPeratureState11 = value;
                RaisePropertyChanged(() => TemPeratureState11);
            }
            get { return m_TemPeratureState11; }
        }

        public AirConditionTemperatureState TemPeratureState12
        {
            set
            {
                if (value == m_TemPeratureState12)
                {
                    return;
                }

                m_TemPeratureState12 = value;
                RaisePropertyChanged(() => TemPeratureState12);
            }
            get { return m_TemPeratureState12; }
        }

        public AirConditionTemperatureState TemPeratureState21
        {
            set
            {
                if (value == m_TemPeratureState21)
                {
                    return;
                }

                m_TemPeratureState21 = value;
                RaisePropertyChanged(() => TemPeratureState21);
            }
            get { return m_TemPeratureState21; }
        }
        public AirConditionTemperatureState TemPeratureState22
        {
            set
            {
                if (value == m_TemPeratureState22)
                {
                    return;
                }

                m_TemPeratureState22 = value;
                RaisePropertyChanged(() => TemPeratureState22);
            }
            get { return m_TemPeratureState22; }
        }
        public AirConditionTemperatureState TemPeratureState31
        {
            set
            {
                if (value == m_TemPeratureState31)
                {
                    return;
                }

                m_TemPeratureState31 = value;
                RaisePropertyChanged(() => TemPeratureState31);
            }
            get { return m_TemPeratureState31; }
        }
        public AirConditionTemperatureState TemPeratureState32
        {
            set
            {
                if (value == m_TemPeratureState32)
                {
                    return;
                }

                m_TemPeratureState32 = value;
                RaisePropertyChanged(() => TemPeratureState32);
            }
            get { return m_TemPeratureState32; }
        }
        public AirConditionTemperatureState TemPeratureState41
        {
            set
            {
                if (value == m_TemPeratureState41)
                {
                    return;
                }

                m_TemPeratureState41 = value;
                RaisePropertyChanged(() => TemPeratureState41);
            }
            get { return m_TemPeratureState41; }
        }
        public AirConditionTemperatureState TemPeratureState42
        {
            set
            {
                if (value == m_TemPeratureState42)
                {
                    return;
                }

                m_TemPeratureState42 = value;
                RaisePropertyChanged(() => TemPeratureState42);
            }
            get { return m_TemPeratureState42; }
        }

        public AirConditionTemperatureState TemPeratureState51
        {
            set
            {
                if (value == m_TemPeratureState51)
                {
                    return;
                }

                m_TemPeratureState51 = value;
                RaisePropertyChanged(() => TemPeratureState51);
            }
            get { return m_TemPeratureState51; }
        }

        public AirConditionTemperatureState TemPeratureState52
        {
            set
            {
                if (value == m_TemPeratureState52)
                {
                    return;
                }

                m_TemPeratureState52 = value;
                RaisePropertyChanged(() => TemPeratureState52);
            }
            get { return m_TemPeratureState52; }
        }

        public AirConditionTemperatureState TemPeratureState61
        {
            set
            {
                if (value == m_TemPeratureState61)
                {
                    return;
                }

                m_TemPeratureState61 = value;
                RaisePropertyChanged(() => TemPeratureState61);
            }
            get { return m_TemPeratureState61; }
        }
        public AirConditionTemperatureState TemPeratureState62
        {
            set
            {
                if (value == m_TemPeratureState62)
                {
                    return;
                }

                m_TemPeratureState62 = value;
                RaisePropertyChanged(() => TemPeratureState62);
            }
            get { return m_TemPeratureState62; }
        }
        public AirConditionTemperatureState TemPeratureState71
        {
            set
            {
                if (value == m_TemPeratureState71)
                {
                    return;
                }

                m_TemPeratureState71 = value;
                RaisePropertyChanged(() => TemPeratureState71);
            }
            get { return m_TemPeratureState71; }
        }
        public AirConditionTemperatureState TemPeratureState72
        {
            set
            {
                if (value == m_TemPeratureState72)
                {
                    return;
                }

                m_TemPeratureState72 = value;
                RaisePropertyChanged(() => TemPeratureState72);
            }
            get { return m_TemPeratureState72; }
        }
        public AirConditionTemperatureState TemPeratureState81
        {
            set
            {
                if (value == m_TemPeratureState81)
                {
                    return;
                }

                m_TemPeratureState81 = value;
                RaisePropertyChanged(() => TemPeratureState81);
            }
            get { return m_TemPeratureState81; }
        }
        public AirConditionTemperatureState TemPeratureState82
        {
            set
            {
                if (value == m_TemPeratureState82)
                {
                    return;
                }

                m_TemPeratureState82 = value;
                RaisePropertyChanged(() => TemPeratureState82);
            }
            get { return m_TemPeratureState82; }
        }

        public bool AllTrainAirIsSelect
        {
            get { return m_AllTrainAirIsSelect; }
            set
            {
                if (value == m_AllTrainAirIsSelect)
                {
                    return;
                }

                m_AllTrainAirIsSelect = value;
                RaisePropertyChanged(() => AllTrainAirIsSelect);
            }
        }

        public bool TrainAirIsSelect
        {
            get { return m_TrainAirIsSelect; }
            set
            {
                if (value == m_TrainAirIsSelect)
                {
                    return;
                }

                m_TrainAirIsSelect = value;
                RaisePropertyChanged(() => TrainAirIsSelect);
            }
        }

        public bool EMU1AirIsSelect
        {
            get { return m_EMU1AirIsSelect; }
            set
            {
                if (value == m_EMU1AirIsSelect)
                {
                    return;
                }

                m_EMU1AirIsSelect = value;
                RaisePropertyChanged(() => EMU1AirIsSelect);
            }
        }

        public bool OneCarAir1IsSelect
        {
            get { return m_OneCarAir1IsSelect; }
            set
            {
                if (value == m_OneCarAir1IsSelect)
                {
                    return;
                }

                m_OneCarAir1IsSelect = value;
                RaisePropertyChanged(() => OneCarAir1IsSelect);
            }
        }

        public bool OneCarAir2IsSelect
        {
            get { return m_OneCarAir2IsSelect; }
            set
            {
                if (value == m_OneCarAir2IsSelect)
                {
                    return;
                }

                m_OneCarAir2IsSelect = value;
                RaisePropertyChanged(() => OneCarAir2IsSelect);
            }
        }

        public bool OneCarAir3IsSelect
        {
            get { return m_OneCarAir3IsSelect; }
            set
            {
                if (value == m_OneCarAir3IsSelect)
                {
                    return;
                }

                m_OneCarAir3IsSelect = value;
                RaisePropertyChanged(() => OneCarAir3IsSelect);
            }
        }

        public bool OneCarAir4IsSelect
        {
            get { return m_OneCarAir4IsSelect; }
            set
            {
                if (value == m_OneCarAir4IsSelect)
                {
                    return;
                }

                m_OneCarAir4IsSelect = value;
                RaisePropertyChanged(() => OneCarAir4IsSelect);
            }
        }

        public bool OneCarAir5IsSelect
        {
            get { return m_OneCarAir5IsSelect; }
            set
            {
                if (value == m_OneCarAir5IsSelect)
                {
                    return;
                }

                m_OneCarAir5IsSelect = value;
                RaisePropertyChanged(() => OneCarAir5IsSelect);
            }
        }

        public bool OneCarAir6IsSelect
        {
            get { return m_OneCarAir6IsSelect; }
            set
            {
                if (value == m_OneCarAir6IsSelect)
                {
                    return;
                }

                m_OneCarAir6IsSelect = value;
                RaisePropertyChanged(() => OneCarAir6IsSelect);
            }
        }

        public bool OneCarAir7IsSelect
        {
            get { return m_OneCarAir7IsSelect; }
            set
            {
                if (value == m_OneCarAir7IsSelect)
                {
                    return;
                }

                m_OneCarAir7IsSelect = value;
                RaisePropertyChanged(() => OneCarAir7IsSelect);
            }
        }

        public bool OneCarAir8IsSelect
        {
            get { return m_OneCarAir8IsSelect; }
            set
            {
                if (value == m_OneCarAir8IsSelect)
                {
                    return;
                }

                m_OneCarAir8IsSelect = value;
                RaisePropertyChanged(() => OneCarAir8IsSelect);
            }
        }

        private void IsSelect()
        {
            if (SelectNum == 0)
            {
                AllTrainAirIsSelect = true;
            }
            else
            {
                AllTrainAirIsSelect = false;
            }
            if (SelectNum == 1)
            {
                TrainAirIsSelect = true;
            }
            else
            {
                TrainAirIsSelect = false;
            }
            if (SelectNum == 2)
            {
                EMU1AirIsSelect = true;
            }
            else
            {
                EMU1AirIsSelect = false;
            }
            if (SelectNum == 3)
            {
                OneCarAir8IsSelect = true;
            }
            else
            {
                OneCarAir8IsSelect = false;
            }
            if (SelectNum == 4)
            {
                OneCarAir7IsSelect = true;
            }
            else
            {
                OneCarAir7IsSelect = false;
            }
            if (SelectNum == 5)
            {
                OneCarAir6IsSelect = true;
            }
            else
            {
                OneCarAir6IsSelect = false;
            }
            if (SelectNum == 6)
            {
                OneCarAir5IsSelect = true;
            }
            else
            {
                OneCarAir5IsSelect = false;
            }
            if (SelectNum == 7)
            {
                OneCarAir4IsSelect = true;
            }
            else
            {
                OneCarAir4IsSelect = false;
            }
            if (SelectNum == 8)
            {
                OneCarAir3IsSelect = true;
            }
            else
            {
                OneCarAir3IsSelect = false;
            }
            if (SelectNum == 9)
            {
                OneCarAir2IsSelect = true;
            }
            else
            {
                OneCarAir2IsSelect = false;
            }
            if (SelectNum == 10)
            {
                OneCarAir1IsSelect = true;
            }
            else
            {
                OneCarAir1IsSelect = false;
            }
        }
    }
}
