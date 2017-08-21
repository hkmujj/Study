using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Model;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View.Train;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    internal abstract class PowerClassify16CarBase : PowerClassifyBase
    {
        protected PowerClassify16CarBase(PowerTpe powerTpe)
            : base(powerTpe)
        {
        }

        public override void Init()
        {
            InitPowerLines16();

            InitMtrs16();

            InitAck116();

            InitAck216();

            InitBkk16();

            InitApu16();

            InitAtr16();

            Init3PhMk16();

            CorrectWires();

            SetPowerUnitInput();

            InitConditionBtns();

        }

        protected virtual void InitMtrs16()
        {
            var car0Location = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.TrainViewLocation.Rectangle.Location;

            m_MTrs =
                GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.MtrInBools.Select(
                    s => new MTr(GetPowerFrom(s.CarNo))
                    {
                        Location = new Point(car0Location.X + s.CarNo*Car.DefaultSize.Width, 220),
                        Size = Car.DefaultSize,
                        CarNo = s.CarNo,
                        RefreshAction = o => MtrRefreshAction(o, s)
                    }).ToList();
        }

        protected virtual void InitAck116()
        {
            InitAck1s();
        }

        protected abstract PowerFrom GetPowerFrom(int carNo);


        protected virtual void InitPowerLines16()
        {
            m_PowerLines = new PowerLines(new Point(0, 310));
        }

        protected virtual void InitAck216()
        {
            // 通过单元计算出来的
            var wires = m_PowerLines[PowerLineType.AC4001].Wires;

            var y = wires.First().StartPoint.Y;

            m_ACK2s =
                GlobalInfo.CurrentCRH2Config.PowerClassifyConfig
                    .ACK2InBools.Select(s => new ACK2()
                    {
                        Location = new Point(wires[s.UnitNo].EndPoint.X - 20, y - 12),
                        UnitNo = s.UnitNo,
                        CircuitInputDirection = Direction.Left,
                        RefreshAction = o => OnPowerUnitRefreshAction(o as CircuitChangerUnit, s)
                    }).ToList();
        }

        protected virtual void InitBkk16()
        {
            // 通过单元计算出来的
            var wires = m_PowerLines[PowerLineType.AC4002].Wires;

            var y = wires.First().StartPoint.Y;

            m_BKKs =
                GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.BKKInBools.Select(s => new BKK()
                {
                    Location = new Point(wires[s.UnitNo].EndPoint.X - 20, y - 27),
                    UnitNo = s.UnitNo,
                    Direction = s.UnitNo == 1 ? Direction.Up : Direction.Left,
                    RefreshAction = o => OnPowerUnitRefreshAction(o as CircuitChangerUnit, s)
                })
                    .Cast<CircuitChangerUnit>()
                    .ToList();
        }

        protected virtual void InitApu16()
        {
            m_Apus = new List<Apu>();
        }

        protected virtual void InitAtr16()
        {
            m_ATrs = new List<ATr>();
        }

        protected virtual void Init3PhMk16()
        {
            m_3PhMks = new List<ThirdPhMk>();


        }

        protected virtual void SetPowerUnitInput()
        {
            for (int i = 0; i < m_Ack1s.Count; i++)
            {
                m_Ack1s[i].InputPowerUnits.Add(m_MTrs[i]);
            }
        }

        protected override void CorrectWires()
        {
            var y1 = YAC4001;

            var y6 = YAC1001;

            foreach (var ack1 in m_Ack1s)
            {
                //ack1.OutputWire.EndPoint.Y = ac4001Y;
                ack1.OutputWire.EndPoint = new Point(ack1.OutputWire.EndPoint.X, y1);
                ack1.OutputWire.IsDrawEndPoint = true;
            }

            foreach (var mk in m_3PhMks)
            {
                mk.StraightWire.IsDrawEndPoint = true;
            }


            foreach (var aTr in m_ATrs)
            {
                aTr.InputWire.EndPoint = new Point(aTr.InputWire.EndPoint.X, y1);
                aTr.InputWire.IsDrawEndPoint = true;
                aTr.OutPutWire.EndPoint = new Point(aTr.OutPutWire.EndPoint.X, y6);
                aTr.OutPutWire.IsDrawEndPoint = true;
            }

            for (int i = 0; i < m_Apus.Count; i++)
            {
                var apu = m_Apus[i];
                m_ApuInputs[i] = new PowerUnitCollectoin<StraightWire>()
                {
                    PowerUnits = new List<StraightWire>()
                    {
                        new StraightWire()
                        {
                            StartPoint =
                                new Point(apu.ActureOutLine.X + apu.ActureOutLine.Width/2, apu.ActureOutLine.Top),
                            EndPoint = new Point(apu.ActureOutLine.X + apu.ActureOutLine.Width/2, y1),
                            IsDrawEndPoint = true,
                            //IsPowerOn = false,
                        },
                        m_ATrs[i].InputWire,
                        m_PowerLines[PowerLineType.AC4001].Wires[i]
                    },
                    IsPowerOn = true,
                    RefreshAction = o =>
                    {
                        var pu = ((PowerUnitCollectoin<StraightWire>) o);
                        pu.IsPowerOn = true;
                        pu.RefreshState();
                    },
                };
            }
        }

        /// <summary>
        /// 右边长的类型
        /// </summary>
        /// <param name="apu2"></param>
        protected void InitApusWiresType2(Apu apu2)
        {
            apu2.StraightWires = new List<StraightWire>
            {
                new StraightWire()
                {
                    StartPoint =
                        new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width/2 + 4, apu2.ActureOutLine.Bottom),
                    EndPoint = new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width/2 + 4, YDC100),
                    IsDrawEndPoint = true,
                },
                new StraightWire()
                {
                    StartPoint =
                        new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width/2 + 8, apu2.ActureOutLine.Bottom),
                    EndPoint = new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width/2 + 8, YAC220),
                    IsDrawEndPoint = true,
                },
                new StraightWire()
                {
                    StartPoint =
                        new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width/2 + 12, apu2.ActureOutLine.Bottom),
                    EndPoint = new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width/2 + 12, YAC1001),
                    IsDrawEndPoint = true,
                },
            };
        }

        /// <summary>
        /// 左边长的类型
        /// </summary>
        /// <param name="apu1"></param>
        protected void InitApusWiresType1(Apu apu1)
        {
            apu1.StraightWires = new List<StraightWire>
            {
                new StraightWire()
                {
                    StartPoint = new Point(apu1.ActureOutLine.X + 8, apu1.ActureOutLine.Bottom),
                    EndPoint = new Point(apu1.ActureOutLine.X + 8, YAC1001),
                    IsDrawEndPoint = true,
                },
                new StraightWire()
                {
                    StartPoint = new Point(apu1.ActureOutLine.X + 12, apu1.ActureOutLine.Bottom),
                    EndPoint = new Point(apu1.ActureOutLine.X + 12, YAC220),
                    IsDrawEndPoint = true,
                },
                new StraightWire()
                {
                    StartPoint = new Point(apu1.ActureOutLine.X + 16, apu1.ActureOutLine.Bottom),
                    EndPoint = new Point(apu1.ActureOutLine.X + 16, YDC100),
                    IsDrawEndPoint = true,
                },
            };
        }

        protected virtual void InitConditionBtns()
        {
            m_ConditionBtns =
                GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.BKKOutBools.Select(
                    s => new CRH2Button()
                    {
                        OutLineRectangle = s.Rectangle,
                        DownImage = GlobalResource.Instance.BtnDownImage,
                        UpImage = GlobalResource.Instance.BtnUpImage,
                        TextColor = Color.White,
                        Text = s.Text,
                        ButtonDownEvent = (o, args) => OnConditionButtonDownEvent(o, args, s),
                    }).ToList();
        }

        private void OnConditionButtonDownEvent(object sender, EventArgs eventArgs, PowerClassifyButtonModel btnModel)
        {
            ReselectConditionBtn(sender as CRH2Button);
            m_SelectBtnOutBoolIndex =
                m_PowerTpe.GetOutBoolIndex(btnModel.OutBoolColoumNames.First());
        }
    }
}
