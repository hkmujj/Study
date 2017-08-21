using System.ComponentModel.Composition;
using System.Linq;
using Engine.TAX2.SS7C.Model.Interface;
using Engine.TAX2.SS7C.Model.ViewSource;
using Engine.TAX2.SS7C.ViewModel.Domain.SecondLevel;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller.Domain.SecondLevel
{
    [Export]
    [Export(typeof(IResetSupport))]
    public class InputPasswordController :ControllerBase<InputPasswordViewModel>, IResetSupport
    {
        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            Reset();
        }

        public bool VerifyPassword()
        {
            return ViewModel.Model.Text == "000";
        }

        public void AddPasswordChar(string c)
        {
            if (!string.IsNullOrWhiteSpace(c))
            {
                ViewModel.Model.Text = ViewModel.Model.Text.Insert(ViewModel.Model.BindableCaretIndex, c);
                ViewModel.Model.BindableCaretIndex += 1;
            }
        }

        public void ModifyPassword()
        {
            ViewModel.Model.Text = string.Empty;
            ViewModel.Model.BindableCaretIndex = 0;
        }

        public void Reset()
        {
            ViewModel.Model.Text = string.Empty;
            ViewModel.Model.BindableCaretIndex = 0;
            ViewModel.Model.SelectedKeybordValue = KeybordStrings.Instance.Numbers.First();
        }
    }
}