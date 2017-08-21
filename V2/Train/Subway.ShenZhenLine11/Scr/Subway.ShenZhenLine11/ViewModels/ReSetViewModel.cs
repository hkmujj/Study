using System.ComponentModel.Composition;
using Subway.ShenZhenLine11.Controller;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export]
    public class ReSetViewModel : SubViewModelBase
    {
        [ImportingConstructor]
        public ReSetViewModel(RestViewController controller)
        {
            controller.ViewModel = this;
            Controller = controller;
        }

        public RestViewController Controller { get; private set; }
    }
}