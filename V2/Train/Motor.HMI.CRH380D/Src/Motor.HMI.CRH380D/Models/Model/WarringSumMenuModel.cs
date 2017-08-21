using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class WarringSumMenuModel : ModelBase
    {

        public ButtonColorState m_AllowCheck;

        [ImportingConstructor]
        public WarringSumMenuModel(DoorController doorController)
        {

        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public ButtonColorState AllowCheck
        {
            get { return m_AllowCheck; }
            set
            {
                if (value == m_AllowCheck)
                {
                     return;
                }
                m_AllowCheck = value;
                RaisePropertyChanged(()=>AllowCheck);
            }
        }
    }
}