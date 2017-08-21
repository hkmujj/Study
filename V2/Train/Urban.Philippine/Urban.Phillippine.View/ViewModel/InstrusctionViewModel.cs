using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Config.KeyResouce;
using Urban.Phillippine.View.Extend;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IInstrusctionViewModel))]
    public class InstrusctionViewModel : ViewModelBase, IInstrusctionViewModel
    {
        private IDictionary<int, int> m_LogicIndex;
        private int m_Index = -1;
        private Timer m_Timer;
        public InstrusctionViewModel()
        {
            m_LogicIndex = new Dictionary<int, int>
            {
                {1, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBTrain1TCU1CutOut]},
                {2, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBTrain2TCU1CutOut]},
                {3, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBTrain3TCU1CutOut]},
                {4, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBTrain4TCU1CutOut]},
                {5, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBTrain1TCU2CutOut]},
                {6, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBTrain2TCU2CutOut]},
                {7, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBTrain3TCU2CutOut]},
                {8, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBTrain4TCU2CutOut]},
                {9, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBAllBatteryCharge]},
                {10, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBEBCutOut]}
            };
            m_Timer = new Timer((state) =>
              {
                  GlobalParam.InitParam.CommunicationDataService.WriteService.ChangeBool(
                       m_LogicIndex[m_Index], false);
              });
            Instruction = new DelegateCommand<string>((args) =>
            {
                m_Index = args.ConvertToInt();
                if (!string.IsNullOrEmpty(args) && m_LogicIndex.ContainsKey(m_Index))
                {
                    GlobalParam.InitParam.CommunicationDataService.WriteService.ChangeBool(
                        m_LogicIndex[m_Index], true);
                    m_Timer.Change(500, int.MaxValue);
                }
            });
        }
        public ICommand Instruction { get; private set; }
    }
}