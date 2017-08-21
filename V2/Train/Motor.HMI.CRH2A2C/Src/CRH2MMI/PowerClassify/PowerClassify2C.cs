using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using CRH2MMI.Common.Global;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    class PowerClassify2C : PowerClassifyBase
    {

        public PowerClassify2C(PowerTpe powerTpe) : base(powerTpe)
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

            AddMtr2(); 
            AddAck1();
            AddAPU();
            AddBKK2();

            m_MTrs.Sort((m1, m2) => m1.Location.X.CompareTo(m2.Location.X));
            m_Ack1s.Sort((m1, m2) => m1.Location.X.CompareTo(m2.Location.X));
            m_BKKs.Sort((m1, m2) => m1.Location.X.CompareTo(m2.Location.X));
            m_Apus.Sort((m1, m2) => m1.Location.X.CompareTo(m2.Location.X));
            m_3PhMks.Sort((m1, m2) => m1.Location.X.CompareTo(m2.Location.X));
            m_ATrs.Sort((m1, m2) => m1.Location.X.CompareTo(m2.Location.X));

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
                m_Ack1s[i].InputPowerUnits.Add(m_MTrs[i]);
            }

            m_ACK2s[0].InputPowerUnits = new List<IPowerUnit>() { m_Ack1s[0], m_Ack1s[2] };

            m_BKKs[0].InputPowerUnits = new List<IPowerUnit>() { m_Apus[0], m_BKKs[1], m_Apus[2] };

            m_BKKs[1].InputPowerUnits = new List<IPowerUnit>() { m_BKKs[0], m_Apus[1], m_Apus[2] };

            m_ApuInputs[0].InputPowerUnits = new List<IPowerUnit>() { m_Ack1s[0], m_ACK2s[0] };

            m_ApuInputs[1].InputPowerUnits = new List<IPowerUnit>() { m_ACK2s[0], m_Ack1s[2] };
        }

        protected override void CorrectWires()
        {
            const int u2Idx = 1;
            var ack1 = m_Ack1s[u2Idx];
            m_Ack1s.Remove(ack1);
            base.CorrectWires();
            m_Ack1s.Insert(u2Idx, ack1);
        }

        private void AddBKK2()
        {
            var bkk2x = m_Apus[2].StraightWires[1].EndPoint.X - ACK1.WireXOffset;
            var bkk2y = m_Apus[2].StraightWires[1].EndPoint.Y;

            var bkk2Config =
                GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.BKK2InBools.First();
            var bkk2 = new BKK2()
            {
                Location = new Point(bkk2x, bkk2y),
                CarNo = m_Apus[2].CarNo,
                RefreshAction = o => OnPowerUnitRefreshAction(o as CircuitChangerUnit, bkk2Config)
            };

            bkk2.InputWire.EndPoint = new Point(bkk2.InputWire.EndPoint.X, bkk2.InputWire.EndPoint.Y - 5);
            bkk2.OutputWire.IsDrawEndPoint = true;

            m_BKKs.Add(bkk2);

        }


        private void AddAPU()
        {
            var apux = m_Ack1s[2].ActureOutLine.Right - 35;
            var apuy = m_Apus[0].Location.Y - 16;
            var apu4Config = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.APUInBools.Find(f => f.CarNo == 4);
            m_Apus.Add(new Apu()
            {
                Location = new Point(apux, apuy),
                CarNo = m_Ack1s[2].CarNo,
                StraightWires = new List<StraightWire>()
                {
                    new StraightWire()
                    {
                        Color = Color.White,
                        StartPoint = new Point(apux + Apu.DefaultSize.Width / 2,
                            apuy),
                        EndPoint =
                            new Point(apux + Apu.DefaultSize.Width / 2, m_Ack1s[2].OutputWire.EndPoint.Y)
                    },
                },
                RefreshAction = o => ApuRefreshAction(o, apu4Config)
            });

            // 控件下面的竖线
            var swd = new StraightWire()
            {
                Color = Color.White,
                StartPoint = new Point(apux + Apu.DefaultSize.Width / 2,
                    m_Apus[2].OutLineRectangle.Bottom),
                EndPoint = new Point(apux + Apu.DefaultSize.Width / 2,
                    m_Apus[2].OutLineRectangle.Bottom + 10),
            };

            // 控件下面的横线
            var swh = new StraightWire()
            {
                Color = Color.White,
                StartPoint = new Point(swd.EndPoint.X - 60, swd.EndPoint.Y),
                EndPoint = new Point(swd.EndPoint.X + 20, swd.EndPoint.Y),
            };

            // 控件上面的横线 
            var swu = new StraightWire()
            {
                StartPoint = new Point(m_Ack1s[2].OutputWire.EndPoint.X, m_Ack1s[2].OutputWire.EndPoint.Y),
                EndPoint = new Point(apux + Apu.DefaultSize.Width / 2, m_Ack1s[2].OutputWire.EndPoint.Y),
            };

            m_ApuInputs.Add(new PowerUnitCollectoin<StraightWire>()
            {
                InputPowerUnits = new List<IPowerUnit>() { m_Ack1s.Find(f => f.CarNo == 3) },
                PowerUnits = new List<StraightWire>()
                {
                    swu,
                    new StraightWire()
                    {
                        Color = Color.White,
                        StartPoint = new Point(m_Ack1s[2].ActureOutLine.Right - 35 + Apu.DefaultSize.Width / 2,
                            apuy),
                        EndPoint =
                            new Point(m_Ack1s[2].ActureOutLine.Right - 35 + Apu.DefaultSize.Width / 2, m_Ack1s[2].OutputWire.EndPoint.Y)
                    },
                },
                IsPowerOn = true,
                RefreshAction = o =>
                {
                    var pu = ( (PowerUnitCollectoin<StraightWire>) o );
                    pu.IsPowerOn = true;
                    pu.RefreshState();
                },
            });


            m_Apus[2].StraightWires.Add(swd);
            m_Apus[2].StraightWires.Add(swh);
            //m_Apus[2].StraightWires.Add(swu);

        }


        /// <summary>
        /// 增加2单元的ACK1
        /// </summary>
        private void AddAck1()
        {
            var mtr = m_MTrs[2];
            var ack1 =
                GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.ACK1InBools.Find(
                    f => f.CarNo == mtr.CarNo);
            m_Ack1s.Add(new ACK1()
            {
                Location =
                    new Point(mtr.OutputWire.EndPoint.X - ACK1.WireXOffset,
                        mtr.OutputWire.EndPoint.Y - 5),
                CarNo = mtr.CarNo,
                RefreshAction = o => OnPowerUnitRefreshAction(o as CircuitChangerUnit, ack1)
            });
        }

        /// <summary>
        /// 增加2单元的MTR
        /// </summary>
        private void AddMtr2()
        {
            var mtr =
                GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.MtrInBools.Find(
                    f => f.CarNo == 3);
            m_MTrs.Add(new MTr(PowerFrom.MTr2)
            {
                Location = new Point(270 + 88 , 220),
                CarNo = 3,
                RefreshAction = o => MtrRefreshAction(o, mtr)
            });
            m_MTrs[1].SetMtrType(PowerFrom.MTr3);
        }

        protected override void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            // mtr
            var mat =
                MatrixHelper.CreateTurnMatrix(((float)m_MTrs.Min(m => m.Location.X) +  m_MTrs.Max(m => m.Location.X))/2,
                    TurnOrientation.Horizontal);

            m_MTrs.ForEach(e => { e.Location = mat.TransformPoint(e.Location); });

            // ack1
            mat =
                MatrixHelper.CreateTurnMatrix(((float)m_Ack1s.Min(m => m.Location.X) + m_Ack1s.Max(m => m.Location.X)) / 2,
                    TurnOrientation.Horizontal);
            m_Ack1s.ForEach(e => e.Location = mat.TransformPoint(e.Location));

            // apu
            mat =
                MatrixHelper.CreateTurnMatrix(((float)m_Apus.Min(m => m.Location.X) + m_Apus.Max(m => m.Location.X)) / 2,
                    TurnOrientation.Horizontal);
            var apu = m_Apus[0];
            apu.Location = mat.TransformPoint(apu.Location);
            apu.Reverse();
            apu = m_Apus[2];
            apu.Location = mat.TransformPoint(apu.Location);
            apu.Reverse();

            // atr
            mat =
                MatrixHelper.CreateTurnMatrix(((float)m_ATrs.Min(m => m.Location.X) + m_ATrs.Max(m => m.Location.X)) / 2,
                    TurnOrientation.Horizontal);
            m_ATrs.ForEach(e =>
            {
                e.Location = mat.TransformPoint(e.Location);
                e.Reverse();
            });

            // m_3PhMks
            mat =
                MatrixHelper.CreateTurnMatrix(((float)m_3PhMks.Min(m => m.Location.X) + m_3PhMks.Max(m => m.Location.X)) / 2,
                    TurnOrientation.Horizontal);
            m_3PhMks.ForEach(e =>
            {
                e.Location = mat.TransformPoint(e.Location);
                e.Reverse();
            });

            m_PowerLines.Reverse();

            ReverseApu();

            CorrectWires();
        }

        private void ReverseApu()
        {
            const int u2Idx = 1;
            var apu = m_Apus[u2Idx];
            var mat = MatrixHelper.CreateTurnMatrix(m_Ack1s[u2Idx].OutputWire.EndPoint.X, TurnOrientation.Horizontal);
            mat.Translate(Apu.DefaultSize.Width, 0);
            apu.Location = mat.TransformPoint(apu.Location);
            apu.Reverse();

            var bkk2 = (BKK2)m_BKKs[u2Idx];
            mat.Reset();
            mat.Translate(apu.StraightWires.Last().EndPoint.X - bkk2.InputWire.StartPoint.X, 0);
            bkk2.Location = mat.TransformPoint(bkk2.Location);
            bkk2.Reverse();
        }

        protected override PowerFrom GetApuPowerFrom(PowerClassifyUnitModel config)
        {
            if (config.CarNo != 4)
            {
                if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[0])])
                {
                    return PowerFrom.MTr1;
                }
                if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[1])])
                {
                    return PowerFrom.MTr3;
                }
            }
            else
            {
                if (m_PowerTpe.BoolList[m_PowerTpe.GetInBoolIndex(config.InBoolColoumNames[0])])
                {
                    return PowerFrom.MTr2;
                }
            }
            return PowerFrom.Null;
        }

        protected override void Refresh()
        {
            base.Refresh();

            m_BKKs[1].Refresh();

            m_BKKs[0].Refresh();
        }
    }
}