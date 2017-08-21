using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.Model;

namespace Subway.WuHanLine6.Controller
{
    /// <summary>
    /// 牵引辅助切除 控制类
    /// </summary>
    [Export]
    public class TractionAssistCutController : ControllerBase<TractionAssistCutModel>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TractionAssistCutController()
        {
            CutResetCommand = new DelegateCommand<string>(args =>
            {
                args.SendBoolData(true, true);
            });

        }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            ViewModel.DisplayStr = string.Format("电制动切除状态   MP1:{0} M1: {1} M2: {2} MP2: {3}",
                GetChar(ViewModel.Brake[0].IsCheck), GetChar(ViewModel.Brake[1].IsCheck),
                GetChar(ViewModel.Brake[2].IsCheck), GetChar(ViewModel.Brake[3].IsCheck));
            ViewModel.Brake.ForEach(f => f.IsCheckChanged += (b) =>
            {
                ViewModel.DisplayStr = string.Format("电制动切除状态   MP1:{0} M1: {1} M2: {2} MP2: {3}",
                    GetChar(ViewModel.Brake[0].IsCheck), GetChar(ViewModel.Brake[1].IsCheck),
                    GetChar(ViewModel.Brake[2].IsCheck), GetChar(ViewModel.Brake[3].IsCheck));
            });
        }

        private string GetChar(bool bl)
        {
            return bl ? "1" : "0";
        }
        /// <summary>
        /// 
        /// </summary>
        public void IsCheckedChanged()
        {
            if (ViewModel.IsChecked)
            {
                Cut();
            }
            else
            {
                Reste();
            }
        }
        private void Reste()
        {
            ViewModel.Brake.ForEach(f => f.IsCheck = false);
        }

        private void Cut()
        {
            ViewModel.Brake.ForEach(f => f.IsCheck = true);
        }

        /// <summary>
        /// 切除复位命令
        /// </summary>
        public ICommand CutResetCommand { get; private set; }



    }
}