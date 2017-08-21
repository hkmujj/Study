using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Model.Train.CarPart;

namespace Urban.GuiYang.DDU.Model.Train
{
    public class AirConditionModel : NotificationObject
    {
        private AirConditionPage1 m_CurrentAirConditionPage1;
        public Lazy<List<AirConditionPage1>> AirConditionPage1Collection { get; set; }

        public AirConditionPage1 CurrentAirConditionPage1
        {
            get { return m_CurrentAirConditionPage1; }
            set
            {
                if (Equals(value, m_CurrentAirConditionPage1))
                {
                    return;
                }
                m_CurrentAirConditionPage1 = value;
                RaisePropertyChanged(() => CurrentAirConditionPage1);
            }
        }

        public Lazy<AirConditionPage2> AirConditionPage2 { get; set; }
       
    }
}
