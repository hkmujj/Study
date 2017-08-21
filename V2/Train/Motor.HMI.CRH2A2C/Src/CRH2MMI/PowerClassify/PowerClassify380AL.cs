using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Model;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View.Train;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    class PowerClassify380AL : PowerClassify16CarBase
    {
        public PowerClassify380AL(PowerTpe powerTpe)
            : base(powerTpe)
        {
        }

        /// <summary>
        /// 文本地
        /// </summary>
        private List<Point> m_Ack2TextLocations;

        private List<Point> m_BKKTextLocations;

        private readonly Font m_TextFont = CRH2Resource.Font8;

        public override void Init()
        {
            base.Init();

            InitAck2Locations();

            base.InitBtns();

            InitBKKLocations();

            InitalizeSetButon();
        }


        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);

            m_Ack2TextLocations.ForEach(e => g.DrawString("ACK2", m_TextFont, CRH2Resource.WhiteBrush, e));
            m_BKKTextLocations.ForEach(e => g.DrawString("BKK", m_TextFont, CRH2Resource.WhiteBrush, e));
        }

        private void InitAck2Locations()
        {
            var wires = m_PowerLines[PowerLineType.AC4001].Wires;
            var y = wires.First().StartPoint.Y + 4;
            m_Ack2TextLocations = m_ACK2s.Skip(1).Select(s => new Point(wires[s.UnitNo].EndPoint.X+9, y)).ToList();
        }


        protected override void InitBkk16()
        {
            base.InitBkk16();

            m_BKKs.ForEach(e => e.TextVisible = false);
        }

        private void InitBKKLocations()
        {
            var wires = m_PowerLines[PowerLineType.AC4002].Wires;
            var y = wires.First().StartPoint.Y - m_TextFont.Height - 4;
            m_BKKTextLocations = m_BKKs.Skip(1).Select(s => new Point(wires[s.UnitNo].EndPoint.X + 9, y)).ToList();

            m_BKKTextLocations.Add(new Point(wires[0].EndPoint.X-30, y ));
        }


        protected override void SetPowerUnitInput()
        {
            base.SetPowerUnitInput();

            m_ACK2s[0].InputPowerUnits = new List<IPowerUnit> { m_Ack1s[0], m_Ack1s[1] };
            m_ACK2s[1].InputPowerUnits = new List<IPowerUnit> { m_Ack1s[2], m_Ack1s[3] };
            m_ACK2s[2].InputPowerUnits = new List<IPowerUnit> { m_Ack1s[4], m_Ack1s[5] };
            m_ACK2s[3].InputPowerUnits = new List<IPowerUnit> { m_Ack1s[5], m_Ack1s[6] };

            m_BKKs[0].InputPowerUnits = new List<IPowerUnit> { m_Apus[0], m_Apus[1] };
            m_BKKs[1].InputPowerUnits = new List<IPowerUnit> { m_Apus[2], m_Apus[3] };
            m_BKKs[2].InputPowerUnits = new List<IPowerUnit> { m_Apus[4], m_Apus[5] };
            m_BKKs[3].InputPowerUnits = new List<IPowerUnit> { m_Apus[5], m_Apus[6] };

            m_ApuInputs[0].InputPowerUnits = new List<IPowerUnit> { m_Ack1s[0], m_ACK2s[0] };
            m_ApuInputs[1].InputPowerUnits = new List<IPowerUnit> { m_Ack1s[1], m_ACK2s[0] };
            m_ApuInputs[2].InputPowerUnits = new List<IPowerUnit> { m_Ack1s[2], m_ACK2s[1] };
            m_ApuInputs[3].InputPowerUnits = new List<IPowerUnit> { m_Ack1s[3], m_ACK2s[1] };
            m_ApuInputs[4].InputPowerUnits = new List<IPowerUnit> { m_Ack1s[4], m_ACK2s[2] };
            m_ApuInputs[5].InputPowerUnits = new List<IPowerUnit> { m_Ack1s[5], m_ACK2s[2], m_ACK2s[3] };
            m_ApuInputs[6].InputPowerUnits = new List<IPowerUnit> { m_Ack1s[6], m_ACK2s[3] };
            
        }


        protected override void InitConditionBtns()
        {
            m_ConditionBtns = new List<CRH2Button>();
        }

        protected override void InitPowerLines16()
        {
            m_PowerLines = new PowerLines(new Point(0, 310));

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
                m_PowerLines[PowerLineType.AC4001].Wires.Add(new StraightWire
                {
                                                                 StartPoint = new Point(ur.Left + dim, y0),
                                                                 EndPoint = new Point(ur.Right - dim, y0)
                                                             });
                m_PowerLines[PowerLineType.AC4002].Wires.Add(new StraightWire
                {
                    StartPoint = new Point(ur.Left + dim, y1),
                    EndPoint = new Point(ur.Right - dim, y1)
                });
                m_PowerLines[PowerLineType.AC1002].Wires.Add(new StraightWire
                {
                    StartPoint = new Point(ur.Left + dim, y5),
                    EndPoint = new Point(ur.Right - dim, y5)
                });
            }

            for (int i = 0; i < 6; i += 2)
            {
                m_PowerLines[PowerLineType.AC1001].Wires.Add(new StraightWire
                {
                                                                 StartPoint = new Point(units[i].Left + dim, y4),
                                                                 EndPoint = new Point(units[i + 1].Right - dim, y4)
                                                             });
            }
            m_PowerLines[PowerLineType.AC1001].Wires.Add(new StraightWire
            {
                                                             StartPoint = new Point(units.Last().Left + dim, y4),
                                                             EndPoint = new Point(units.Last().Right - dim, y4)
                                                         });


            m_PowerLines[PowerLineType.DC100].Wires.Add(new StraightWire
            {
                                                            StartPoint = new Point(units.First().Left + dim, y2),
                                                            EndPoint = new Point(units.Last().Right - dim, y2)
                                                        });


            var fcar = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.TrainViewLocation.Rectangle.Left;
            var wires = m_PowerLines[PowerLineType.AC220].Wires;
            wires.Add(new StraightWire
            {
                          StartPoint = new Point(fcar + dim, y3),
                          EndPoint = new Point(fcar + Car.DefaultSize.Width *3- dim, y3)
                      });

            wires.Add(new StraightWire
            {
                StartPoint = new Point(fcar + Car.DefaultSize.Width * 6 + dim, y3),
                EndPoint = new Point(fcar + Car.DefaultSize.Width * 9 - Car.DefaultSize.Width/2 - dim, y3)
            });

            wires.Add(new StraightWire
            {
                StartPoint = new Point(fcar + Car.DefaultSize.Width * 9 - Car.DefaultSize.Width / 2 + dim, y3),
                EndPoint = new Point(fcar + Car.DefaultSize.Width * 11 - dim, y3)
            });



            wires.Add(new StraightWire
            {
                StartPoint = new Point(fcar + Car.DefaultSize.Width * 15+dim, y3),
                EndPoint = new Point(fcar + Car.DefaultSize.Width * 16 - dim, y3)
            });

        }


        protected override void InitAck216()
        {
            base.InitAck216();

            foreach (var ack2 in m_ACK2s.Skip(1))
            {
                ack2.TextVisible = false;
            }
        }

        protected override void InitApu16()
        {
            var fcar = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.TrainViewLocation.Rectangle.Left;
            var apuY = m_ACK2s[0].ActureOutLine.Bottom - 6;
            var units = m_PowerTpe.m_TrainView.GetTrainUnitRectangles();

            m_Apus = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.APUInBools
                .Skip(1).Select(s => new Apu
                {
                    Location =
                        new Point(units[s.UnitNo].Right - Apu.DefaultSize.Width - 3, apuY),
                    CarNo = s.CarNo,
                    UnitNo = s.UnitNo,
                    Tag =  s,
                    RefreshAction = o => ApuRefreshAction(o, s)
                }).ToList();
            m_Apus.Insert(0, new Apu
            {
                Location = new Point(fcar, apuY),
                Tag = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.APUInBools[0],
                CarNo = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.APUInBools[0].CarNo,
                UnitNo = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.APUInBools[0].UnitNo,
                RefreshAction = o => ApuRefreshAction(o, GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.APUInBools[0])
            });

        }

        protected override void Init3PhMk16()
        {
            var phmkY = m_Apus[0].ActureOutLine.Bottom + 2;
            m_3PhMks = new List<ThirdPhMk>
            {
                new ThirdPhMk
                {
                    TextDirection = Direction.Right,
                    Location = new Point(m_Apus[0].ActureOutLine.X + m_Apus[0].ActureOutLine.Width/3*2 + 3, phmkY),
                    CarNo = 0,
                    RefreshAction = o => ApuRefreshAction(o, m_Apus[0].Tag as PowerClassifyUnitModel),
                }
            };


            m_3PhMks.AddRange(m_Apus.Skip(1).Select((s,i) =>new ThirdPhMk
            {
                TextDirection = Direction.Left,
                Location =
                    new Point(
                        s.ActureOutLine.X + s.ActureOutLine.Width / 2 - m_3PhMks[0].ActureOutLine.Width -
                        5,
                        phmkY),
                CarNo = s.CarNo,
                UnitNo = s.UnitNo,
                RefreshAction = o => ApuRefreshAction(o, s.Tag as PowerClassifyUnitModel),
            }));

            foreach (var source in m_3PhMks.Skip(2).Take(4))
            {
                source.TextVisible = false;
            }
        }

        protected override void InitAtr16()
        {
            var apuY = m_Apus[0].Location.Y;
            var atrY = apuY - ATr.DefaultWireLeght;
            const int atrApuInterval = 3;
            m_ATrs = new List<ATr>
            {
                         new ATr
                         {
                             TextDirection = Direction.Right,
                             Location = new Point(m_Apus[0].ActureOutLine.Right + atrApuInterval, atrY),
                             CarNo = 0,
                             RefreshAction = o => ApuRefreshAction(o, m_Apus[0].Tag as PowerClassifyUnitModel)
                         }
                     };

            m_ATrs.AddRange(m_Apus.Skip(1).Select(s => new ATr
            {
                TextDirection = Direction.Left,
                Location =
                    new Point(s.ActureOutLine.Left - s.ActureOutLine.Width - atrApuInterval, atrY),
                CarNo = s.CarNo,
                UnitNo = s.UnitNo,
                RefreshAction = o => ApuRefreshAction(o, s.Tag as PowerClassifyUnitModel)
            }));

            foreach (var aTr in m_ATrs.Skip(1).Take(4))
            {
                aTr.TextVisible = false;
            }

        }

        protected override void CorrectWires()
        {

            var y1 = m_PowerLines[PowerLineType.AC4001].Wires.First().StartPoint.Y;
            var y2 = m_PowerLines[PowerLineType.AC4002].Wires.First().StartPoint.Y;
            var y3 = m_PowerLines[PowerLineType.DC100].Wires.First().StartPoint.Y;
            var y4 = m_PowerLines[PowerLineType.AC220].Wires.First().StartPoint.Y;
            var y5 = m_PowerLines[PowerLineType.AC1001].Wires.First().StartPoint.Y;
            var y6 = m_PowerLines[PowerLineType.AC1002].Wires.First().StartPoint.Y;

            foreach (var ack1 in m_Ack1s)
            {
                //ack1.OutputWire.EndPoint.Y = ac4001Y;
                ack1.OutputWire.EndPoint = new Point(ack1.OutputWire.EndPoint.X, y1);
                ack1.OutputWire.IsDrawEndPoint = true;
            }


            // apus from left to right
            var apus = m_Apus.OrderBy(o => o.CarNo).ToList();

            var apu1 = apus[0];

            if (!apu1.StraightWires.Any())
            {
                InitApusWiresType1(apu1);
            }
            else
            {
                apu1.StraightWires.ForEach(e => e.Refresh());
            }

            // loop for 2,4,6
            for (int i = 2; i <= 6; i+=2)
            {
                var apu2 = apus[i];
                if (!apu2.StraightWires.Any())
                {
                    InitApusWiresType2(apu2);
                }
                else
                {
                    apu2.StraightWires.ForEach(e => e.Refresh());
                }
            }

            foreach (var aTr in m_ATrs)
            {
                aTr.InputWire.EndPoint = new Point(aTr.InputWire.EndPoint.X, y1);
                aTr.InputWire.IsDrawEndPoint = true;
                aTr.OutPutWire.EndPoint = new Point(aTr.OutPutWire.EndPoint.X, y6);
                aTr.OutPutWire.IsDrawEndPoint = true;
            }


            for (int i = 0; i < apus.Count; i++)
            {
                var apu = apus[i];
                m_ApuInputs[i] = new PowerUnitCollectoin<StraightWire>
                {
                    PowerUnits = new List<StraightWire>
                    {
                                                      new StraightWire
                                                      {
                                                          StartPoint =
                                                              new Point(apu.ActureOutLine.X + apu.ActureOutLine.Width / 2, apu.ActureOutLine.Top),
                                                          EndPoint = new Point(apu.ActureOutLine.X + apu.ActureOutLine.Width / 2, y1),
                                                          IsDrawEndPoint = true,
                                                          //IsPowerOn = false,
                                                      },
                                                      m_ATrs[i].InputWire,
                                                      m_PowerLines[PowerLineType.AC4001].Wires[i]
                                                  },
                    IsPowerOn = true,
                    RefreshAction = o =>
                    {
                        var pu = ((PowerUnitCollectoin<StraightWire>)o);
                        pu.IsPowerOn = true;
                        pu.RefreshState();
                    },
                };
            }
            foreach (var mk in m_3PhMks)
            {
                mk.StraightWire.IsDrawEndPoint = true;
            }
        }

        protected override PowerFrom GetApuPowerFrom(PowerClassifyUnitModel config)
        {
            var carNo = config.CarNo;
            switch (carNo)
            {
                case 0:
                    return GetApuPowerFromU1(config);
                case 4:
                    return GetApuPowerFromU1(config);
                case 6:
                    return GetApuPowerFromU2(config);
                case 8:
                    return GetApuPowerFromU2(config);
                case 10:
                    return GetApuPowerFromU30(config);
                case 12:
                    return GetApuPowerFromU31(config);
                case 15:
                    return GetApuPowerFromU32(config);
            }
            return PowerFrom.Null;
        }

        private PowerFrom GetApuPowerFromU32(PowerClassifyUnitModel config)
        {
            if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[0])])
            {
                return PowerFrom.MTr6;
            }
            if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[1])])
            {
                return PowerFrom.MTr7;
            }
            return PowerFrom.Null;
        }

        private PowerFrom GetApuPowerFromU30(PowerClassifyUnitModel config)
        {
            if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[0])])
            {
                return PowerFrom.MTr5;
            }
            if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[1])])
            {
                return PowerFrom.MTr6;
            }
            return PowerFrom.Null;
        }

        private PowerFrom GetApuPowerFromU1(PowerClassifyUnitModel config)
        {
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

        private PowerFrom GetApuPowerFromU2(PowerClassifyUnitModel config)
        {
            if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[0])])
            {
                return PowerFrom.MTr3;
            }
            if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[1])])
            {
                return PowerFrom.MTr4;
            }
            return PowerFrom.Null;
        }

        private PowerFrom GetApuPowerFromU31(PowerClassifyUnitModel config)
        {
            if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[0])])
            {
                return PowerFrom.MTr5;
            }
            if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[1])])
            {
                return PowerFrom.MTr6;
            }
            if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[2])])
            {
                return PowerFrom.MTr7;
            }
            return PowerFrom.Null;
        }

        protected override PowerFrom GetPowerFrom(int carNo)
        {
            switch (carNo)
            {
                case 1:
                    return PowerFrom.MTr1;
                case 3:
                    return PowerFrom.MTr2;
                case 5:
                    return PowerFrom.MTr3;
                case 7:
                    return PowerFrom.MTr4;
                case 9:
                    return PowerFrom.MTr5;
                case 11:
                    return PowerFrom.MTr6;
                case 13:
                    return PowerFrom.MTr7;
            }
            throw new ArgumentException(string.Format("There has no mtr of car no = {0}", carNo));
        }
    }
}
