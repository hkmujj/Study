using System;
using System.ComponentModel.Composition;
using Subway.ShiJiaZhuangLine1.Subsystem.Controller;
using Subway.ShiJiaZhuangLine1.Subsystem.Model;

namespace Subway.ShiJiaZhuangLine1.Subsystem.ViewModels
{
    [Export]
    public class TractionLockViewModel : IDisposable
    {
        [ImportingConstructor]
        public TractionLockViewModel(TractionLockModel model, TractionLockController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public TractionLockController Controller { private set; get; }

        public TractionLockModel Model { private set; get; }

        public void Dispose()
        {
            
        }
    }
}