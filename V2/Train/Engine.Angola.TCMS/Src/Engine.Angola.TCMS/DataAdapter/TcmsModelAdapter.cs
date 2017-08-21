using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Windows.Input;
using Engine.Angola.TCMS.Events;
using Engine.Angola.TCMS.Extension;
using Engine.Angola.TCMS.Model;
using Engine.Angola.TCMS.Model.MainData;
using Engine.Angola.TCMS.Model.SWData;
using Engine.Angola.TCMS.Resource.Keys;
using Engine.Angola.TCMS.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;

namespace Engine.Angola.TCMS.DataAdapter
{
    [Export]
    public class TcmsModelAdapter : IDataListener
    {
        private readonly AngolaTCMSShellViewModel m_ShellViewModel;

        // ReSharper disable once NotAccessedField.Local
        private Timer m_UpdateTimeTimer;

        private AsyncAdapteDataEvent m_AsyncAdapteDataEvent;

        private AngolaTCMSShellModel Model
        {
            get { return m_ShellViewModel.Model; }
        }

        [ImportingConstructor]
        public TcmsModelAdapter(AngolaTCMSShellViewModel shellViewModel)
        {
            m_ShellViewModel = shellViewModel;
        }

        public void Initalize()
        {
            m_AsyncAdapteDataEvent = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<AsyncAdapteDataEvent>();

            m_AsyncAdapteDataEvent.Subscribe(AsyncUpdateData, ThreadOption.UIThread);

            GlobalParam.Instance.InitParam.RegistDataListener(this);

            m_UpdateTimeTimer = new Timer(UpdateTime, null, 1000, 1000);
        }

        private void AsyncUpdateData(AsyncAdapteDataEvent.Args args)
        {
            UpdateBtn(args.DataChangedArgs);
        }

        private void UpdateTime(object state)
        {
            Model.Other.NetTime = DateTime.Now;
        }

        private void ReadServiceOnDataChanged(object sender, CommunicationDataChangedArgs args)
        {
            UpdateMainContent(args);

            UpdateSWContent(args);

            m_AsyncAdapteDataEvent.Publish(new AsyncAdapteDataEvent.Args(args));
        }

        private void UpdateMainContent(CommunicationDataChangedArgs args)
        {
            var data = Model.TCMS.MainData;
            var f4data = Model.TCMS.F4Data;
            //InfMain_速度
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_速度, f => data.Speed = f);
            //InfMain_AltVolt
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_AltVolt, f => data.AltVolt = f);
            //InfMain_AltCorr
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_AltCorr, f => data.AltCorr = f);
            //InfMain_AuxilVolt
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_AuxilVolt, f => data.AuxilVolt = f);
            //InfMain_AuxilCorr
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_AuxilCorr, f => data.AuxilCorr = f);
            //InfMain_AguaTemp
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_AguaTemp, f => data.AguaTemp = f);
            //InfMain_BateVolt
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_BateVolt, f => data.BateVolt = f);
            //InfMain_OleoPres
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_OleoPres, f => data.OleoPres = f);
            //InfMain_CombPres
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_CombPres, f => data.CombPres = f);
            //InfMain_MotorVelo
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_MotorVelo, f => data.MotorVelo = f);
            //InfMain_AlterSaida
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_AlterSaida, f => data.AltSaida = f);
            //InfMain_Tm1Corr
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_Tm1Corr, f => data.Tm1Corr = f);
            //InfMain_Tm2Corr
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_Tm2Corr, f => data.Tm2Corr = f);
            //InfMain_Tm3Corr
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_Tm3Corr, f => data.Tm3Corr = f);
            //InfMain_Tm4Corr
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_Tm4Corr, f => data.Tm4Corr = f);
            //InfMain_Tm5Corr
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_Tm5Corr, f => data.Tm5Corr = f);
            //InfMain_Tm6Corr
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_Tm6Corr, f => data.Tm6Corr = f);
            //InfMain_FeedBack
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_PotenciaFeedBack, f => data.FeedBack = f);
            //InfMain_Ventil
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_DBVentil, f => data.Ventil = f);
            //InfMain_BateTemp1
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_BateTemp1, f => data.BateTemp1 = f);
            //InfMain_BateTemp2
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfMain_BateTemp2, f => data.BateTemp2 = f);
            //InfF4_Carga
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_Carga, f => f4data.Carga = f);
            //InfF4_MotorOleoPressao
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_MotorOleoPressao, f => f4data.MotorOleoPressao = f);
            //InfF4_BateriaVoltagem
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_BateriaVoltagem, f => f4data.BateriaVoltagem = f);
            //InfF4_PropulsaoPressao
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_PropulsaoPressao, f => f4data.PropulsaoPressao = f);
            //InfF4_MotorCoolantTemp
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_MotorCoolantTemp, f => f4data.MotorCoolantTemp = f);
            //InfF4_ConbustivelPressao
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_ConbustivelPressao, f => f4data.ConbustivelPressao = f);
            //InfF4_AlarneSaideEstado
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_AlarneSaideEstado, f => f4data.AlarneSaideEstado = f);
            //InfF4_MotorVelocidade
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_MotorVelocidade, f => f4data.MotorVelocidade = f);
            //InfF4_MotorHaoas
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_MotorHaoas, f => f4data.MotorHaoas = f);
            //InfF4_ConbustivelConsumo
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_ConbustivelConsumo, f => f4data.ConbustivelConsumo = f);
            //InfF4_AtmosfericaPressuao
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_AtmosfericaPressuao, f => f4data.AtmosfericaPressuao = f);
            //InfF4_DesejadaMotorVelo
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_DesejadaMotorVelo, f => f4data.DesejadaMotorVelo = f);
            //InfF4_OleoFiltroDifPres
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_OleoFiltroDifPres, f => f4data.OleoFiltroDifPres = f);
            //InfF4_CombFiltroDifPres
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_CombFiltroDifPres, f => f4data.CombFiltroDifPres = f);
            //InfF4_DepoisCoolerTemp
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_DepoisCoolerTemp, f => f4data.DepoisCoolerTemp = f);
            //InfF4_EsquerdoExaustorTemp
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_EsquerdoExaustorTemp, f => f4data.EsquerdoExaustorTemp = f);
            //InfF4_DireitoExaustorTemp
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_DireitoExaustorTemp, f => f4data.DireitoExaustorTemp = f);
            //InfF4_DireitoArFiltRestr
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_DireitoArFiltRestr, f => f4data.DireitoArFiltRestr = f);
            //InfF4_EsquerdoArFiltRestr
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_EsquerdoArFiltRestr, f => f4data.EsquerdoArFiltRestr = f);
            //InfF4_CarterPressao
            args.ChangedFloats.UpdateIfContains(InFloatKeys.InfF4_CarterPressao, f => f4data.CarterPressao = f);
        }

        private void UpdateBtn(CommunicationDataChangedArgs args)
        {
            var btn = m_ShellViewModel.RightBtnViewModel;
            args.ChangedBools.UpdateIfContains(Inb.InbButtonF1, b => ResponseCommand(b, btn.Model.F1Command));
            args.ChangedBools.UpdateIfContains(Inb.InbButtonF2, b => ResponseCommand(b, btn.Model.F2Command));
            args.ChangedBools.UpdateIfContains(Inb.InbButtonF3, b => ResponseCommand(b, btn.Model.F3Command));
            args.ChangedBools.UpdateIfContains(Inb.InbButtonF4, b => ResponseCommand(b, btn.Model.F4Command));
            args.ChangedBools.UpdateIfContains(Inb.InbButtonF5, b => ResponseCommand(b, btn.Model.F5Command));
        }

        private void UpdateSWContent(CommunicationDataChangedArgs args)
        {
            var swdata = m_ShellViewModel.Model.TCMS.SWData;
            var data = m_ShellViewModel.Model.TCMS.MainData;

            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_ThacaoCmd, b => swdata.ThacaoCmd = b ? ThacaoCmd.ON : ThacaoCmd.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_DynPreioCmd, b => swdata.DynPreioCmd = b ? DynPreioCmd.ON : DynPreioCmd.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_PropCargaCmd, b => swdata.PropCargaCmd = b ? PropCargaCmd.ON : PropCargaCmd.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_FieldWeakening, b => swdata.FieldWeakening = b ? FieldWeakening.ON : FieldWeakening.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_EfcNo, b => swdata.EfcNo = b ? EfcNo.ON : EfcNo.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_MotorFbkIn, b => swdata.MotorFbkIn = b ? MotorFbkIn.ON : MotorFbkIn.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_BrakeFbkIn, b => swdata.BrakeFbkIn = b ? BrakeFbkIn.ON : BrakeFbkIn.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_LocospeedCont, b => swdata.LocospeedCont = b ? LocospeedCont.ON : LocospeedCont.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_VigilanciaSwt, b => swdata.VigilanciaSwt = b ? VigilanciaSwt.ON : VigilanciaSwt.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_AirFbk, b => swdata.AirFbk = b ? AirFbk.ON : AirFbk.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_AirStartIn, b => swdata.AirStartIn = b ? AirStartIn.ON : AirStartIn.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_SW界面_AirStartDelay, b => swdata.AirStartDelay = b ? AirStartDelay.ON : AirStartDelay.OFF);
            args.ChangedBools.UpdateIfContains(Inb.主界面_Localiada_LenTa, b => data.Localiada = Localiada.LenTa);
            args.ChangedBools.UpdateIfContains(Inb.主界面_Localiada_Motor, b => data.Localiada = Localiada.Motor);
            args.ChangedBools.UpdateIfContains(Inb.主界面_Localiada_Travagen, b => data.Localiada = Localiada.Travagen);
            args.ChangedBools.UpdateIfContains(Inb.主界面_CMD0, b => { if (b) { data.Cmd = CMD.CMD0; } });
            args.ChangedBools.UpdateIfContains(Inb.主界面_CMD1, b => { if (b) { data.Cmd = CMD.CMD1; } });
            args.ChangedBools.UpdateIfContains(Inb.主界面_CMD2, b => { if (b) { data.Cmd = CMD.CMD2; } });
            args.ChangedBools.UpdateIfContains(Inb.主界面_CMD3, b => { if (b) { data.Cmd = CMD.CMD3; } });
            args.ChangedBools.UpdateIfContains(Inb.主界面_CMD4, b => { if (b) { data.Cmd = CMD.CMD4; } });
            args.ChangedBools.UpdateIfContains(Inb.主界面_CMD5, b => { if (b) { data.Cmd = CMD.CMD5; } });
            args.ChangedBools.UpdateIfContains(Inb.主界面_CMD6, b => { if (b) { data.Cmd = CMD.CMD6; } });
            args.ChangedBools.UpdateIfContains(Inb.主界面_CMD7, b => { if (b) { data.Cmd = CMD.CMD7; } });
            args.ChangedBools.UpdateIfContains(Inb.主界面_CMD8, b => { if (b) { data.Cmd = CMD.CMD8; } });


        }

        private void ResponseCommand(bool need, ICommand command)
        {
            if (command != null && need && command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
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
            ReadServiceOnDataChanged(sender, dataChangedArgs);
        }
    }
}