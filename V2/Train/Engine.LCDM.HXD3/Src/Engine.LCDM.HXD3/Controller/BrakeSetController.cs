using System.ComponentModel.Composition;
using System.Windows.Documents;
using System.Windows.Input;
using Engine.LCDM.HXD3.ViewModels;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using Engine.LCDM.HXD3.Enums;
using Engine.LCDM.HXD3.Extentions;
using Engine.LCDM.HXD3.Resource;
using Engine.LCDM.HXD3.Views.BottomButton;
using Engine.LCDM.HXD3.Config;
using MMI.Facility.Interface.Service;
using System;
using Excel.Interface;
using System.Collections.ObjectModel;
using Engine.LCDM.HXD3.Common;

namespace Engine.LCDM.HXD3.Controller
{
    [Export(typeof(BrakeSetController))]
    public class BrakeSetController : ControllerBase<BrakeSetting>
    {
        public BrakeSetController()
        {
            AffirmOperation = new DelegateCommand(AffirmAction);
            ExcuteOperation = new DelegateCommand(ExcuteAction);
            CancelOperation = new DelegateCommand(CancelAction);
            OtherChoiceCommand = new DelegateCommand<string>(OtherChoiceAction);
            OperationCommand = new DelegateCommand<string>(OperatAction);
            CutOffPutIn = new DelegateCommand<string>(CutOffPutInAction);
            TrainStyleCommand=new DelegateCommand(TrainStyleAction);
            MakeWindCommand=new DelegateCommand(MakeWindAction);
            AddPressure = new DelegateCommand(AddPressureAction);
            DeCreasePressure=new DelegateCommand(DecreasePressureAction);
            SteadyPressure=new DelegateCommand(SteadyPressureAction);
            ReleaseCommand = new DelegateCommand(ReleaseAction);
            SteadyInOffCommand = new DelegateCommand(SteadyInOffAction);
        }
        
        private void VisibilityAction(string dexInfo)
        {
            if (dexInfo.Length < 8)
                return;
            ViewModel.ButtonOne = dexInfo[0] == '1' ? Visibility.Visible : Visibility.Hidden;
            ViewModel.ButtonTwo = dexInfo[1] == '1' ? Visibility.Visible : Visibility.Hidden;
            ViewModel.ButtonThree = dexInfo[2] == '1' ? Visibility.Visible : Visibility.Hidden;
            ViewModel.ButtonFour = dexInfo[3] == '1' ? Visibility.Visible : Visibility.Hidden;
            ViewModel.ButtonFive = dexInfo[4] == '1' ? Visibility.Visible : Visibility.Hidden;
            ViewModel.ButtonSix = dexInfo[5] == '1' ? Visibility.Visible : Visibility.Hidden;
            ViewModel.ButtonSeven = dexInfo[6] == '1' ? Visibility.Visible : Visibility.Hidden;
            ViewModel.ButtonEight = dexInfo[7] == '1' ? Visibility.Visible : Visibility.Hidden;
        }
        private void AffirmAction()
        {
            ViewModel.ButtonOne = Visibility.Hidden;
        }
        private void ExcuteAction()
        {
            if (ViewModel.IsExecute)
            {
                ViewModel.ViewModel.Controller.Navigator.Execute(typeof(MainBottomButton).FullName);
                ViewModel.CurOperation = ViewModel.NewOperation;
                ViewModel.CurInOff = ViewModel.NewInOff;
                ViewModel.CurTrainStyle = ViewModel.NewTrainStyle;
                ViewModel.CurWind = ViewModel.NewWind;
                ViewModel.CurPressure = ViewModel.NewPressure;
                ViewModel.CurSteadyInOff = ViewModel.NewSteadyInOff;
                ViewModel.CurReleaseStyle = ViewModel.NewReleaseStyle;
                ViewModel.NewSetShow = Visibility.Hidden;
                VisibilityAction("00111111");
                ExitButtonShow();

                ViewModel.CurOperation.SendData(OutBoolKeys.非操纵端);
                (!ViewModel.CurOperation).SendData(OutBoolKeys.操纵端);
                ViewModel.CurInOff.SendData(OutBoolKeys.切除);
                (!ViewModel.CurInOff).SendData(OutBoolKeys.投入);
                ViewModel.CurTrainStyle.SendData(OutBoolKeys.客车);
                (!ViewModel.CurTrainStyle).SendData(OutBoolKeys.货车);
                ViewModel.CurPressure.SendData(OutFloatKeys.列车管压力);
                (!ViewModel.CurSteadyInOff).SendData(OutBoolKeys.平稳投入);
                ViewModel.CurSteadyInOff.SendData(OutBoolKeys.平稳切除);
                ViewModel.CurReleaseStyle.SendData(OutBoolKeys.阶段缓解);
                (!ViewModel.CurReleaseStyle).SendData(OutBoolKeys.直接缓解);
                ViewModel.CurWind.SendData(OutBoolKeys.补风);
                (!ViewModel.CurWind).SendData(OutBoolKeys.不补风);
                ViewModel.IsExecute = false;
            }
            else
                ViewModel.IsExecute = true;
        }
        private void CancelAction()
        {
            ViewModel.ViewModel.Controller.Navigator.Execute(typeof(MainBottomButton).FullName);
            ViewModel.NewOperation=ViewModel.CurOperation;
            ViewModel.NewInOff=ViewModel.CurInOff;
            ViewModel.NewTrainStyle=ViewModel.CurTrainStyle;
            ViewModel.NewWind=ViewModel.CurWind;
            ViewModel.NewPressure = ViewModel.CurPressure;
            ViewModel.NewSteadyInOff = ViewModel.CurSteadyInOff;
            ViewModel.NewReleaseStyle = ViewModel.CurReleaseStyle;
            ViewModel.NewSetShow = Visibility.Hidden;
            ViewModel.IsExecute = false;
            VisibilityAction("00111111");
            ExitButtonShow();
        }
        private void ExitButtonShow()
        {
            ViewModel.IsExecute = false;

            if (ViewModel.CurInOff)
            {
                ViewModel.ButtonSix = Visibility.Hidden;
            }
            else
            {
                ViewModel.ButtonSix = Visibility.Visible;
            }
            if (ViewModel.CurOperation)
            {
                ViewModel.ButtonThree = Visibility.Hidden;
                ViewModel.ButtonFive = Visibility.Hidden;
            }
            else
            {
                ViewModel.ButtonThree = Visibility.Visible;
                ViewModel.ButtonFive = Visibility.Visible;
            }
        }

        private void OtherChoiceAction(string fullname)
        {
            ViewModel.IsExecute = false;

            ViewModel.ViewModel.Controller.Navigator.Execute(fullname);
        }

        private void OperatAction(string indexInfo)
        {
            ViewModel.IsExecute = true;

            ViewModel.NewSetShow = Visibility.Visible;
            VisibilityAction(indexInfo);
            if (ViewModel.NewOperation)
            {
                ViewModel.NewOperation = false;
                ViewModel.ButtonThree=Visibility.Visible;
                ViewModel.ButtonFive = Visibility.Visible;
                return;
            }
            ViewModel.ButtonThree = Visibility.Hidden;
            ViewModel.ButtonFive = Visibility.Hidden;
            ViewModel.NewOperation = true;
        }

        private void TrainStyleAction()
        {
            ViewModel.IsExecute = true;

            ViewModel.NewSetShow = Visibility.Visible;
            ViewModel.ButtonOne = Visibility.Visible;
            ViewModel.ButtonTwo = Visibility.Visible;
            if (ViewModel.NewTrainStyle)
            {
                ViewModel.NewTrainStyle = false;
                return;
            }
            ViewModel.NewTrainStyle = true;
        }

        private void CutOffPutInAction(string indexInfo)
        {
            ViewModel.IsExecute = true;

            ViewModel.NewSetShow = Visibility.Visible;
            VisibilityAction(indexInfo);
            if (ViewModel.NewInOff)
            {
                ViewModel.NewInOff = false;
                return;
            }
            ViewModel.NewInOff = true;
        }
        private void ReleaseAction()
        {
            ViewModel.IsExecute = true;

            ViewModel.NewSetShow = Visibility.Visible;
            ViewModel.ButtonOne = Visibility.Visible;
            ViewModel.ButtonTwo = Visibility.Visible;
            ViewModel.NewReleaseStyle = !ViewModel.NewReleaseStyle;
        }
        private void SteadyInOffAction()
        {
            ViewModel.IsExecute = false;

            ViewModel.NewSetShow = Visibility.Visible;
            ViewModel.ButtonOne = Visibility.Visible;
            ViewModel.ButtonTwo = Visibility.Visible;
            ViewModel.NewSteadyInOff = !ViewModel.NewSteadyInOff;
        }
        private void MakeWindAction()
        {
            ViewModel.IsExecute = false;
            
            ViewModel.NewSetShow = Visibility.Visible;
            ViewModel.ButtonOne = Visibility.Visible;
            ViewModel.ButtonTwo=Visibility.Visible;
            if (ViewModel.NewWind)
            {
                ViewModel.NewWind = false;
                return;
            }
            ViewModel.NewWind = true;
        }

        private void AddPressureAction()
        {
            ViewModel.IsExecute = false;
            
            ViewModel.NewSetShow=Visibility.Visible;
            ViewModel.ButtonOne = Visibility.Visible;
            ViewModel.ButtonTwo = Visibility.Visible;
            ViewModel.NewPressure += 10;
        }

        private void DecreasePressureAction()
        {
            ViewModel.IsExecute = false;
            
            ViewModel.NewSetShow = Visibility.Visible;
            ViewModel.ButtonOne = Visibility.Visible;
            ViewModel.ButtonTwo = Visibility.Visible;
            ViewModel.NewPressure -= 10;
        }

        private void SteadyPressureAction()
        {
            ViewModel.IsExecute = false;
            
            ViewModel.NewSetShow = Visibility.Visible;
            ViewModel.ButtonOne = Visibility.Visible;
            ViewModel.ButtonTwo = Visibility.Visible;
            if (GlobalParam.Instance.ViewSets.CanPressure500.Value)
            {
                m_SteadySet = Math.Abs(m_SteadySet - 500) < float.Epsilon ? 600 : 500;
            }
            else
            {
                m_SteadySet = 600;
            }
            ViewModel.NewPressure = m_SteadySet;
        }
        private float m_SteadySet = 500;

        public ICommand AffirmOperation { get; private set; }
        public ICommand ExcuteOperation { get; private set; }
        public ICommand CancelOperation { get; private set; }
        public ICommand OtherChoiceCommand { get; private set; }
        public ICommand OperationCommand { get; private set; }
        public ICommand CutOffPutIn { get; private set; }
        public ICommand TrainStyleCommand { get; private set; }
        public ICommand MakeWindCommand { get; private set; }
        public ICommand AddPressure { get; private set; }
        public ICommand DeCreasePressure { get; private set; }
        public ICommand SteadyPressure { get; private set; }
        public ICommand ReleaseCommand { get; private set; }
        public ICommand SteadyInOffCommand { get; private set; }
    }
}