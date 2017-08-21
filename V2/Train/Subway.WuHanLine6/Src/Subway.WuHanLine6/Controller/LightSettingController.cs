using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using DevExpress.Mvvm;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.WuHanLine6.Models.Model;

namespace Subway.WuHanLine6.Controller
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class LightSettingController : ControllerBase<Lazy<LightSettingViewModel>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewmodel"></param>
        [ImportingConstructor]
        public LightSettingController(Lazy<LightSettingViewModel> viewmodel) : base(viewmodel)
        {
            SubCommand = new DelegateCommand(() =>
            {
                if (viewmodel.Value.IsAuto)
                {
                    return;
                }
                if (viewmodel.Value.Light >= 10)
                {
                    viewmodel.Value.Light -= 10;
                }
            });
            PlusCommand = new DelegateCommand(() =>
              {
                  if (viewmodel.Value.IsAuto)
                  {
                      return;
                  }
                  if (viewmodel.Value.Light < 100)
                  {
                      viewmodel.Value.Light += 10;
                  }
              });
            AutoCommnad = new DelegateCommand(() =>
            {
                viewmodel.Value.IsAuto = true;
                viewmodel.Value.Light = 50;
            });
            HalfCommand = new DelegateCommand(() =>
              {
                  viewmodel.Value.IsAuto = false;
              });
        }
        /// <summary>
        /// 
        /// </summary>
        public ICommand SubCommand { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand PlusCommand { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand AutoCommnad { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand HalfCommand { get; private set; }
    }
}
