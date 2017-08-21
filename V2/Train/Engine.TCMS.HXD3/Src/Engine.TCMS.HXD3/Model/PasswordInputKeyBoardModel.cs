using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Controller;

namespace Engine.TCMS.HXD3.Model
{
    [Export(typeof(PasswordInputKeyBoardModel))]
    public class PasswordInputKeyBoardModel
    {
        [ImportingConstructor]
        public PasswordInputKeyBoardModel(PasswordIntputKeyBorderController controller)
        {
            Controller = controller;
        }

        public PasswordIntputKeyBorderController Controller { get; private set; }   
    }
}