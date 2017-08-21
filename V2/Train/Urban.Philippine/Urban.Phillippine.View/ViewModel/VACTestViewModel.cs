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
    [Export(typeof(IVACTestViewModel))]
    public class VACTestViewModel : IVACTestViewModel
    {
        private readonly Dictionary<int, int> m_LogicIndex;
        private int m_Index = -1;
        private readonly Timer m_Timer;
        public VACTestViewModel()
        {
            m_Timer = new Timer((state) =>
              {
                  if (m_Index != -1 && m_LogicIndex.ContainsKey(m_Index))
                  {
                      GlobalParam.InitParam.CommunicationDataService.WriteService.ChangeBool(m_LogicIndex[m_Index], false);
                  }
              });
            m_LogicIndex = new Dictionary<int, int>
            {
                {1, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBVAC25Cool]},
                {2, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBVAC50Cool]},
                {3, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBVAC75Cool]},
                {4, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBVAC100Cool]},
                {5, IndexConfigure.OutBoolIndex[OutBoolKeys.OutBVACTestStop]}
            };
            TestCommand = new DelegateCommand<string>((args) =>
            {
                if (string.IsNullOrEmpty(args))
                {
                    return;
                }
                m_Index = args.ConvertToInt();
                if (m_LogicIndex.ContainsKey(m_Index))
                {
                    GlobalParam.InitParam.CommunicationDataService.WriteService.ChangeBool(m_LogicIndex[m_Index], true);
                    m_Timer.Change(500, int.MaxValue);
                }
            });
        }

        public ICommand TestCommand { get; set; }//数据输出响应命令
    }
}