using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common.Global;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    internal class PowerClassify2A : PowerClassifyBase
    {
        public PowerClassify2A(PowerTpe powerTpe)
            : base(powerTpe)
        {
        }

        public override void Init()
        {
            InitMtrs();

            InitAck1s();

            InitAck2s();

            var apuY = InitApu();

            InitAtr(apuY);

            Init3PhMk();

            InitBkk();

            m_PowerLines = new PowerLines(new Point(0, m_Ack1s[0].OutputWire.EndPoint.Y + 10));

            CorrectWires();

            SetPowerUnitInput();

            InitBtns();

            RecordAllPowerUnit();

            InitalizeSetButon();

            GlobalEvent.Instance.ReversalChanged += OnReversalChanged;
        }

        private void SetPowerUnitInput()
        {
            for (int i = 0; i < m_Ack1s.Count; i++)
            {
                m_Ack1s[i].InputPowerUnits = new List<IPowerUnit>() { m_MTrs[i] };
            }

            m_ACK2s[0].InputPowerUnits = new List<IPowerUnit>() { m_Ack1s[0], m_Ack1s[1] };

            m_BKKs[0].InputPowerUnits = new List<IPowerUnit>() { m_Apus[0], m_Apus[1] };

            m_ApuInputs[0].InputPowerUnits = new List<IPowerUnit>() { m_Ack1s[0], m_ACK2s[0] };

            m_ApuInputs[1].InputPowerUnits = new List<IPowerUnit>() { m_ACK2s[0], m_Ack1s[1] };

        }

        protected override PowerFrom GetApuPowerFrom(PowerClassifyUnitModel config)
        {
            Debug.Assert(config.InBoolColoumNames.Count() == 2);

            if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[0])])
            {
                return PowerFrom.MTr1;
            }
            if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[1])])
            {
                return PowerFrom.MTr2;
            }
            return PowerFrom.Null;
        }

    }
}
