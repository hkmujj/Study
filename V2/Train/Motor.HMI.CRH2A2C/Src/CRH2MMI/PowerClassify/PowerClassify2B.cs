using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Model;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View.Train;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    internal class PowerClassify2B : PowerClassify16CarBase
    {
        public PowerClassify2B(PowerTpe powerTpe) : base(powerTpe)
        {
        }

        //public override void Init()
        //{
        //}
        protected override void InitPowerLines16()
        {
            base.InitPowerLines16();

            var units = m_PowerTpe.m_TrainView.GetTrainUnitRectangles();

            var y0 = ResetLine(PowerLineType.AC4001);
            var y1 = ResetLine(PowerLineType.AC4002);
            var y2 = ResetLine(PowerLineType.DC100);
            var y3 = ResetLine(PowerLineType.AC220);
            var y4 = ResetLine(PowerLineType.AC1001);
            var y5 = ResetLine(PowerLineType.AC1002);

            const int dim = 4;

            foreach (var ur in units)
            {
                m_PowerLines[PowerLineType.AC4001].Wires.Add(new StraightWire()
                {
                    StartPoint = new Point(ur.Left + dim, y0),
                    EndPoint = new Point(ur.Right - dim, y0)
                });
                m_PowerLines[PowerLineType.AC4002].Wires.Add(new StraightWire()
                {
                    StartPoint = new Point(ur.Left + dim, y1),
                    EndPoint = new Point(ur.Right - dim, y1)
                });
                m_PowerLines[PowerLineType.AC1001].Wires.Add(new StraightWire()
                {
                    StartPoint = new Point(ur.Left + dim, y4),
                    EndPoint = new Point(ur.Right - dim, y4)
                });

                m_PowerLines[PowerLineType.AC1002].Wires.Add(new StraightWire()
                {
                    StartPoint = new Point(ur.Left + dim, y5),
                    EndPoint = new Point(ur.Right - dim, y5)
                });

                m_PowerLines[PowerLineType.AC220].Wires.Add(new StraightWire()
                {
                    StartPoint = new Point(ur.Left + dim, y3),
                    EndPoint = new Point(ur.Right - dim, y3)
                });
            }


            m_PowerLines[PowerLineType.DC100].Wires.Add(new StraightWire()
            {
                StartPoint = new Point(units.First().Left + dim, y2),
                EndPoint = new Point(units.Last().Right - dim, y2)
            });

            InitalizeSetButon();

        }

        protected override void InitApu16()
        {
            var fcar = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.TrainViewLocation.Rectangle.Left;
            var apuY = m_ACK2s[0].ActureOutLine.Bottom - 6;
            var units = m_PowerTpe.m_TrainView.GetTrainUnitRectangles();



            m_Apus = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.APUInBools
                .Select((s, i) => new Apu()
                {
                    Location =
                        new Point(
                            i%2 != 0 ? units[s.UnitNo].Right - Apu.DefaultSize.Width - 3 : units[s.UnitNo].Left + 8,
                            apuY),
                    CarNo = s.CarNo,
                    UnitNo = s.UnitNo,Size = Car.DefaultSize
                }).ToList();
        }

        protected override void InitAtr16()
        {
            var apuY = m_Apus[0].Location.Y;
            var atrY = apuY - ATr.DefaultWireLeght;
            const int atrApuInterval = 3;

            m_ATrs = m_Apus.Select((s, i) => new ATr()
            {
                TextDirection = i%2 == 0 ? Direction.Right : Direction.Left,
                Location =
                    i%2 == 0
                        ? new Point(m_Apus[i].ActureOutLine.Right + atrApuInterval, atrY)
                        : new Point(s.ActureOutLine.Left - s.ActureOutLine.Width - atrApuInterval, atrY),
                CarNo = s.CarNo,
                UnitNo = s.UnitNo,
            }).ToList();

        }

        protected override void Init3PhMk16()
        {

            var phmkY = m_Apus[0].ActureOutLine.Bottom + 2;

            m_3PhMks = m_Apus.Select((s, i) => new ThirdPhMk()
            {
                TextDirection = i%2 == 0 ? Direction.Right : Direction.Left,
                Location =
                    i%2 == 0
                        ? new Point(s.ActureOutLine.X + s.ActureOutLine.Width/3*2 + 3, phmkY)
                        : new Point(s.ActureOutLine.X - s.ActureOutLine.Width/2*3 + 5, phmkY),
                CarNo = s.CarNo,
                UnitNo = s.UnitNo,
            }).ToList();
        }

        protected override void SetPowerUnitInput()
        {
            base.SetPowerUnitInput();


            for (int i = 0; i < m_ApuInputs.Count; i++)
            {
                var apu = m_ApuInputs[i];
                apu.InputPowerUnits = new List<IPowerUnit>() {m_Ack1s[i]};
            }
        }

        protected override void CorrectWires()
        {
            base.CorrectWires();


            var y1 = m_PowerLines[PowerLineType.AC4001].Wires.First().StartPoint.Y;
            var y2 = m_PowerLines[PowerLineType.AC4002].Wires.First().StartPoint.Y;
            var y3 = m_PowerLines[PowerLineType.DC100].Wires.First().StartPoint.Y;
            var y4 = m_PowerLines[PowerLineType.AC220].Wires.First().StartPoint.Y;
            var y5 = m_PowerLines[PowerLineType.AC1001].Wires.First().StartPoint.Y;
            var y6 = m_PowerLines[PowerLineType.AC1002].Wires.First().StartPoint.Y;

            for (int i = 0; i < m_Apus.Count; i++)
            {
                var apu1 = m_Apus[i];
                if (!apu1.StraightWires.Any())
                {
                    if (i%2 == 0)
                    {
                        InitApusWiresType1(apu1);
                    }
                    else
                    {
                        InitApusWiresType2(apu1);
                    }
                }
                else
                {
                    apu1.StraightWires.ForEach(e => e.Refresh());
                }
            }
           
           
        }



        protected override PowerFrom GetPowerFrom(int carNo)
        {
            switch (carNo)
            {
                case 1:
                    return PowerFrom.MTr1;
                case 5:
                    return PowerFrom.MTr2;
                case 9:
                    return PowerFrom.MTr3;
                case 13:
                    return PowerFrom.MTr4;
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}
