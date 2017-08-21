using System;
using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Controller;

namespace LightRail.HMI.SZLHLF.Model.MaintainModel
{
    [Export]
    public class InputKeyInfoModel : ModelBase
    {
        private string m_InputNumbers;

        [ImportingConstructor]
        InputKeyInfoModel(InputKeyController inputKeyController)
        {
            InputKeyController = inputKeyController;
            InputKeyController.ViewModel = new Lazy<InputKeyInfoModel>(()=>this);
            InputKeyController.Initalize();
        }
        
        /// <summary>
        /// 控制
        /// </summary>
        public InputKeyController InputKeyController { get; set; }

        /// <summary>
        /// 输入字符串
        /// </summary>
        public string InputNumbers
        {
            get { return m_InputNumbers; }
            set
            {
                if (value == m_InputNumbers)
                {
                    return;
                }
                m_InputNumbers = value;
                RaisePropertyChanged(() => InputNumbers);
            }
        }
    }
}
