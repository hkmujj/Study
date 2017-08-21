using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Model.Domain.Switch.Illumination
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IlluminationModel : NotificationObject
    {
        private IlluminationState m_TrainIlluminationState;
        private IlluminationState m_EMU1IlluminationState;
        private IlluminationState m_OnecarIllumination1State;
        private IlluminationState m_OnecarIllumination2State;
        private IlluminationState m_OnecarIllumination3State;
        private IlluminationState m_OnecarIllumination4State;
        private IlluminationState m_OnecarIllumination5State;
        private IlluminationState m_OnecarIllumination6State;
        private IlluminationState m_OnecarIllumination7State;
        private IlluminationState m_OnecarIllumination8State;
        private bool m_AllTrainIlluminationIsSelect;
        private bool m_TrainIlluminationIsSelect;
        private bool m_EMU1IlluminationIsSelect;
        private bool m_OneCarIllumination1IsSelect;
        private bool m_OneCarIllumination2IsSelect;
        private bool m_OneCarIllumination3IsSelect;
        private bool m_OneCarIllumination4IsSelect;
        private bool m_OneCarIllumination5IsSelect;
        private bool m_OneCarIllumination6IsSelect;
        private bool m_OneCarIllumination7IsSelect;
        private bool m_OneCarIllumination8IsSelect;
        private int m_SelectNum;

         public IlluminationModel()
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

        public IlluminationState TrainIlluminationState
        {
            set
            {
                if (value == m_TrainIlluminationState)
                {
                    return;
                }

                m_TrainIlluminationState = value;
                RaisePropertyChanged(() => TrainIlluminationState);
            }
            get { return m_TrainIlluminationState; }
        }

        public IlluminationState EMU1IlluminationState
        {
            set
            {
                if (value == m_EMU1IlluminationState)
                {
                    return;
                }

                m_EMU1IlluminationState = value;
                RaisePropertyChanged(() => EMU1IlluminationState);
            }
            get { return m_EMU1IlluminationState; }
        }

        public IlluminationState OnecarIllumination1State
        {
            set
            {
                if (value == m_OnecarIllumination1State)
                {
                    return;
                }

                m_OnecarIllumination1State = value;
                RaisePropertyChanged(() => OnecarIllumination1State);
            }
            get { return m_OnecarIllumination1State; }
        }

        public IlluminationState OnecarIllumination2State
        {
            set
            {
                if (value == m_OnecarIllumination2State)
                {
                    return;
                }

                m_OnecarIllumination2State = value;
                RaisePropertyChanged(() => OnecarIllumination2State);
            }
            get { return m_OnecarIllumination2State; }
        }

        public IlluminationState OnecarIllumination3State
        {
            set
            {
                if (value == m_OnecarIllumination3State)
                {
                    return;
                }

                m_OnecarIllumination3State = value;
                RaisePropertyChanged(() => OnecarIllumination3State);
            }
            get { return m_OnecarIllumination3State; }
        }

        public IlluminationState OnecarIllumination4State
        {
            set
            {
                if (value == m_OnecarIllumination4State)
                {
                    return;
                }

                m_OnecarIllumination4State = value;
                RaisePropertyChanged(() => OnecarIllumination4State);
            }
            get { return m_OnecarIllumination4State; }
        }

        public IlluminationState OnecarIllumination5State
        {
            set
            {
                if (value == m_OnecarIllumination5State)
                {
                    return;
                }

                m_OnecarIllumination5State = value;
                RaisePropertyChanged(() => OnecarIllumination5State);
            }
            get { return m_OnecarIllumination5State; }
        }

        public IlluminationState OnecarIllumination6State
        {
            set
            {
                if (value == m_OnecarIllumination6State)
                {
                    return;
                }

                m_OnecarIllumination6State = value;
                RaisePropertyChanged(() => OnecarIllumination6State);
            }
            get { return m_OnecarIllumination6State; }
        }

        public IlluminationState OnecarIllumination7State
        {
            set
            {
                if (value == m_OnecarIllumination7State)
                {
                    return;
                }

                m_OnecarIllumination7State = value;
                RaisePropertyChanged(() => OnecarIllumination7State);
            }
            get { return m_OnecarIllumination7State; }
        }

        public IlluminationState OnecarIllumination8State
        {
            set
            {
                if (value == m_OnecarIllumination8State)
                {
                    return;
                }

                m_OnecarIllumination8State = value;
                RaisePropertyChanged(() => OnecarIllumination8State);
            }
            get { return m_OnecarIllumination8State; }
        }
        public bool AllTrainIlluminationIsSelect
        {
            get { return m_AllTrainIlluminationIsSelect; }
            set
            {
                if (value == m_AllTrainIlluminationIsSelect)
                {
                    return;
                }

                m_AllTrainIlluminationIsSelect = value;
                RaisePropertyChanged(() => AllTrainIlluminationIsSelect);
            }
        }

        public bool TrainIlluminationIsSelect
        {
            get { return m_TrainIlluminationIsSelect; }
            set
            {
                if (value == m_TrainIlluminationIsSelect)
                {
                    return;
                }

                m_TrainIlluminationIsSelect = value;
                RaisePropertyChanged(() => TrainIlluminationIsSelect);
            }
        }

        public bool EMU1IlluminationIsSelect
        {
            get { return m_EMU1IlluminationIsSelect; }
            set
            {
                if (value == m_EMU1IlluminationIsSelect)
                {
                    return;
                }

                m_EMU1IlluminationIsSelect = value;
                RaisePropertyChanged(() => EMU1IlluminationIsSelect);
            }
        }

        public bool OneCarIllumination1IsSelect
        {
            get { return m_OneCarIllumination1IsSelect; }
            set
            {
                if (value == m_OneCarIllumination1IsSelect)
                {
                    return;
                }

                m_OneCarIllumination1IsSelect = value;
                RaisePropertyChanged(() => OneCarIllumination1IsSelect);
            }
        }

        public bool OneCarIllumination2IsSelect
        {
            get { return m_OneCarIllumination2IsSelect; }
            set
            {
                if (value == m_OneCarIllumination2IsSelect)
                {
                    return;
                }

                m_OneCarIllumination2IsSelect = value;
                RaisePropertyChanged(() => OneCarIllumination2IsSelect);
            }
        }

        public bool OneCarIllumination3IsSelect
        {
            get { return m_OneCarIllumination3IsSelect; }
            set
            {
                if (value == m_OneCarIllumination3IsSelect)
                {
                    return;
                }

                m_OneCarIllumination3IsSelect = value;
                RaisePropertyChanged(() => OneCarIllumination3IsSelect);
            }
        }

        public bool OneCarIllumination4IsSelect
        {
            get { return m_OneCarIllumination4IsSelect; }
            set
            {
                if (value == m_OneCarIllumination4IsSelect)
                {
                    return;
                }

                m_OneCarIllumination4IsSelect = value;
                RaisePropertyChanged(() => OneCarIllumination4IsSelect);
            }
        }

        public bool OneCarIllumination5IsSelect
        {
            get { return m_OneCarIllumination5IsSelect; }
            set
            {
                if (value == m_OneCarIllumination5IsSelect)
                {
                    return;
                }

                m_OneCarIllumination5IsSelect = value;
                RaisePropertyChanged(() => OneCarIllumination5IsSelect);
            }
        }

        public bool OneCarIllumination6IsSelect
        {
            get { return m_OneCarIllumination6IsSelect; }
            set
            {
                if (value == m_OneCarIllumination6IsSelect)
                {
                    return;
                }

                m_OneCarIllumination6IsSelect = value;
                RaisePropertyChanged(() => OneCarIllumination6IsSelect);
            }
        }

        public bool OneCarIllumination7IsSelect
        {
            get { return m_OneCarIllumination7IsSelect; }
            set
            {
                if (value == m_OneCarIllumination7IsSelect)
                {
                    return;
                }

                m_OneCarIllumination7IsSelect = value;
                RaisePropertyChanged(() => OneCarIllumination7IsSelect);
            }
        }

        public bool OneCarIllumination8IsSelect
        {
            get { return m_OneCarIllumination8IsSelect; }
            set
            {
                if (value == m_OneCarIllumination8IsSelect)
                {
                    return;
                }

                m_OneCarIllumination8IsSelect = value;
                RaisePropertyChanged(() => OneCarIllumination8IsSelect);
            }
        }

        private void IsSelect()
        {
            if (SelectNum == 0)
            {
                AllTrainIlluminationIsSelect = true;
            }
            else
            {
                AllTrainIlluminationIsSelect = false;
            }
            if (SelectNum == 1)
            {
                TrainIlluminationIsSelect = true;
            }
            else
            {
                TrainIlluminationIsSelect = false;
            }
            if (SelectNum == 2)
            {
                EMU1IlluminationIsSelect = true;
            }
            else
            {
                EMU1IlluminationIsSelect = false;
            }
            if (SelectNum == 3)
            {
                OneCarIllumination8IsSelect = true;
            }
            else
            {
                OneCarIllumination8IsSelect = false;
            }
            if (SelectNum == 4)
            {
                OneCarIllumination7IsSelect = true;
            }
            else
            {
                OneCarIllumination7IsSelect = false;
            }
            if (SelectNum == 5)
            {
                OneCarIllumination6IsSelect = true;
            }
            else
            {
                OneCarIllumination6IsSelect = false;
            }
            if (SelectNum == 6)
            {
                OneCarIllumination5IsSelect = true;
            }
            else
            {
                OneCarIllumination5IsSelect = false;
            }
            if (SelectNum == 7)
            {
                OneCarIllumination4IsSelect = true;
            }
            else
            {
                OneCarIllumination4IsSelect = false;
            }
            if (SelectNum == 8)
            {
                OneCarIllumination3IsSelect = true;
            }
            else
            {
                OneCarIllumination3IsSelect = false;
            }
            if (SelectNum == 9)
            {
                OneCarIllumination2IsSelect = true;
            }
            else
            {
                OneCarIllumination2IsSelect = false;
            }
            if (SelectNum == 10)
            {
                OneCarIllumination1IsSelect = true;
            }
            else
            {
                OneCarIllumination1IsSelect = false;
            }
        }
    }
}
