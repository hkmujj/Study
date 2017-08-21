using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.HMI.CRH380BG.Model.Domain.Brake
{
    [Export]
    public class BrakeFunctionStatusModel : NotificationObject
    {
        private Efficence m_IsAirCompressorEfficence;
        private Efficence m_IsTrianPipeFillWithWindEfficence;
        private Efficence m_IsSandDryEfficence;

        public Efficence IsSandDryEfficence
        {
            get { return m_IsSandDryEfficence; }
            set
            {
                if (value == m_IsSandDryEfficence)
                    return;
                m_IsSandDryEfficence = value;
                RaisePropertyChanged(() => IsSandDryEfficence);
            }
        }

        public Efficence IsTrianPipeFillWithWindEfficence
        {
            get { return m_IsTrianPipeFillWithWindEfficence; }
            set
            {
                if (value == m_IsTrianPipeFillWithWindEfficence)
                    return;
                m_IsTrianPipeFillWithWindEfficence = value;
                RaisePropertyChanged(() => IsTrianPipeFillWithWindEfficence);
            }
        }

        public Efficence IsAirCompressorEfficence
        {
            get { return m_IsAirCompressorEfficence; }
            set
            {
                if (value == m_IsAirCompressorEfficence)
                    return;
                m_IsAirCompressorEfficence = value;
                RaisePropertyChanged(() => IsAirCompressorEfficence);
            }
        }
    }
}

