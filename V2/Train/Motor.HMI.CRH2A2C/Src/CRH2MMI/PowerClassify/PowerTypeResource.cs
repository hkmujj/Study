using System.Linq;
using CRH2MMI.Common.Util;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.PowerClassify
{
    class PowerTypeResource
    {
        private readonly PowerTpe m_PowerTpe;

        public PowerTypeResource(PowerTpe powerTpe)
        {
            m_PowerTpe = powerTpe;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx">0: 投入  1:复位</param>
        /// <param name="setValue"> </param>
        public void SetBkkState(int idx, int setValue)
        {
            m_PowerTpe.append_postCmd(CmdType.SetBoolValue, idx, setValue, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx">0: 投入  1:复位</param>
        public void SetBkkState(int idx)
        {
            m_PowerTpe.append_postCmd(CmdType.SetBoolValue, m_PowerTpe.UIObj.OutBoolList[idx], 1, 0);
        }

        public bool GetState(PowerClassifyUnitModel config)
        {
            return
                m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames.First())];
        }


        public bool GetState(IUnit it)
        {
            var offset = 0;
            var utype = it.GetType();
            if (utype == typeof(ACK1))
            {
                if (it.CarNo == 1)
                {
                    offset = 2;
                }
                else if (it.CarNo == 5)
                {
                    offset = 3;
                }
                else if (it.CarNo == 3)
                {
                    offset = 12;
                }
            }

            if (utype == typeof(ACK2))
            {
                offset = 4;
            }

            if (utype == typeof(MTr))
            {
                if (it.CarNo == 1)
                {
                    offset = 0;
                }
                else if (it.CarNo == 5)
                {
                    offset = 1;
                }else if(it.CarNo == 3)
                {
                    offset = 11;
                }
            }

            //if (utype == typeof(Apu))
            //{
                
            //}

            if (utype == typeof(BKK))
            {
                if (it.CarNo == 3)
                {
                    offset = 5;    
                }
            }

            if (utype == typeof(BKK2))
            {
                offset = 13;
            }

            return GetState(offset);
        }

        public ApuPowerFrom GetApuPowerFrom(Apu apu)
        {
            int offset = apu.CarNo == 0 ? 6 : 10;
            //return GetState(offset) ? ApuPowerFrom.Car2 : ApuPowerFrom.Car6;

            if (apu.CarNo == 0)
            {
                if (GetState(6))
                {
                    return ApuPowerFrom.Car2;
                }
                if(GetState(7))
                {
                    return ApuPowerFrom.Car6;
                }
                return ApuPowerFrom.Unkown;
            }
            if (apu.CarNo == 7)
            {
                if (GetState(10))
                {
                    return ApuPowerFrom.Car2;
                }
                if (GetState(9))
                {
                    return ApuPowerFrom.Car6;
                }
                return ApuPowerFrom.Unkown;
            }
            return ApuPowerFrom.Unkown;
        }

        private bool GetState(int idx)
        {
            return m_PowerTpe.BoolList[m_PowerTpe.UIObj.InBoolList[idx]];
        }
    }

}
