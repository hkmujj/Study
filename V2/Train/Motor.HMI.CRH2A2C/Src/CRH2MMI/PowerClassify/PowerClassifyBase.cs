using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    abstract class PowerClassifyBase
    {
        // ReSharper disable once InconsistentNaming
        protected PowerTpe m_PowerTpe;

        // ReSharper disable once InconsistentNaming
        protected List<ACK2> m_ACK2s;
        // ReSharper disable once InconsistentNaming
        protected List<MTr> m_MTrs;
        // ReSharper disable once InconsistentNaming
        protected List<Apu> m_Apus;
        // ReSharper disable once InconsistentNaming
        protected List<ACK1> m_Ack1s;
        // ReSharper disable once InconsistentNaming
        protected List<ATr> m_ATrs;
        // ReSharper disable once InconsistentNaming
        protected List<ThirdPhMk> m_3PhMks;
        // ReSharper disable once InconsistentNaming
        protected List<CircuitChangerUnit> m_BKKs;
        // ReSharper disable once InconsistentNaming
        protected List<CRH2Button> m_ConditionBtns;

        protected List<PowerUnitCollectoin<StraightWire>> m_ApuInputs;

        // ReSharper disable once InconsistentNaming
        protected PowerLines m_PowerLines;

        // ReSharper disable once InconsistentNaming
        protected CRH2Button m_SelectedConditionBtn;

        // ReSharper disable once InconsistentNaming
        protected List<PowerStateTree> m_PowerStateTrees;

        protected List<PowerStateData> m_AllPowerStateDatas;

        // ReSharper disable once InconsistentNaming
        protected List<IPowerUnit> m_AllPowerUnits;

        private CommonSetBtn m_CommonSetBtn;

        protected int YAC4001 { get { return m_PowerLines[PowerLineType.AC4001].Wires.First().StartPoint.Y; } }
        protected int YAC4002 { get { return m_PowerLines[PowerLineType.AC4002].Wires.First().StartPoint.Y; } }
        protected int YDC100 { get { return m_PowerLines[PowerLineType.DC100].Wires.First().StartPoint.Y; } }
        protected int YAC220 { get { return m_PowerLines[PowerLineType.AC220].Wires.First().StartPoint.Y; } }
        protected int YAC1001 { get { return m_PowerLines[PowerLineType.AC1001].Wires.First().StartPoint.Y; } }
        protected int YAC1002 { get { return m_PowerLines[PowerLineType.AC1002].Wires.First().StartPoint.Y; } }

        // ReSharper disable once InconsistentNaming
        protected int m_SelectBtnOutBoolIndex;

        protected PowerClassifyBase(PowerTpe powerTpe)
        {
            m_PowerTpe = powerTpe;
            m_ApuInputs = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.APUInBools.Select(s => new PowerUnitCollectoin<StraightWire>()).ToList();
            //m_ApuInputs = new List<PowerUnitCollectoin<StraightWire>>() { new PowerUnitCollectoin<StraightWire>(), new PowerUnitCollectoin<StraightWire>() };
        }

        public abstract void Init();

        protected virtual void InitalizeSetButon()
        {
            m_CommonSetBtn = new CommonSetBtn
            {
                SetButtonDown = OnSetButtonDown,
                SetButtonUp = OnSetButtonUp,
            };

            m_CommonSetBtn.SetBtnEntity.Location = m_CommonSetBtn.SetBtnEntity.Location.Translate(0, 30);
        }

        protected virtual void OnSetButtonDown(object sender, EventArgs e)
        {
            if (m_SelectedConditionBtn != null)
            {
                m_PowerTpe.PowerTypeResource.SetBkkState(m_SelectBtnOutBoolIndex, 1);
            }
        }

        protected virtual void OnSetButtonUp(object sender, EventArgs e)
        {
            if (m_SelectedConditionBtn != null)
            {
                m_PowerTpe.PowerTypeResource.SetBkkState(m_SelectBtnOutBoolIndex, 0);
                ReselectConditionBtn(null);
            }
        }

        public virtual void OnDraw(Graphics g)
        {
            m_ACK2s.ForEach(e => e.OnDraw(g));

            m_MTrs.ForEach(e => e.OnDraw(g));
            m_Ack1s.ForEach(e => e.OnDraw(g));

            m_Apus.ForEach(e => e.OnDraw(g));
            m_3PhMks.ForEach(e => e.OnDraw(g));
            m_ATrs.ForEach(e => e.OnDraw(g));
            m_BKKs.ForEach(e => e.OnDraw(g));

            m_PowerLines.OnDraw(g);

            m_ConditionBtns.ForEach(e => e.OnDraw(g));

            foreach (var apuInput in m_ApuInputs)
            {
                apuInput.OnDraw(g);
            }

            if (m_CommonSetBtn != null)
            {
                m_CommonSetBtn.OnDraw(g);
            }
        }

        public virtual void OnPaint(Graphics g)
        {
            Refresh();

            OnDraw(g);
        }

        public virtual bool OnMouseDown(Point point)
        {
            var b = m_ConditionBtns.Any(a => a.OnMouseDown(point));
            if (!b)
            {
                return m_CommonSetBtn != null && (m_CommonSetBtn.OnMouseDown(point));
            }
            return true;
        }

        public virtual bool OnMouseUp(Point point)
        {
            return m_CommonSetBtn != null && m_CommonSetBtn.OnMouseUp(point);
        }

        protected void RecordAllPowerUnit()
        {
            m_AllPowerUnits = new List<IPowerUnit>();
            m_AllPowerUnits.AddRange(m_MTrs.Cast<IPowerUnit>());
            m_AllPowerUnits.AddRange(m_Ack1s.Cast<IPowerUnit>());
            m_AllPowerUnits.AddRange(m_ACK2s.Cast<IPowerUnit>());
            m_AllPowerUnits.AddRange(m_BKKs.Cast<IPowerUnit>());
            m_AllPowerUnits.AddRange(m_3PhMks.Cast<IPowerUnit>());
            m_AllPowerUnits.AddRange(m_Apus.Cast<IPowerUnit>());
            m_AllPowerUnits.Add(m_PowerLines);
        }

        protected virtual void Refresh()
        {
            m_BKKs.ForEach(e => e.Reset());

            m_ApuInputs.ForEach(e => e.Reset());

            m_MTrs.ForEach(e => e.Refresh());

            m_Apus.ForEach(e => e.Refresh());

            m_ATrs.ForEach(e => e.Refresh());

            m_3PhMks.ForEach(e => e.Refresh());

            m_Ack1s.ForEach(e => e.Refresh());

            m_ACK2s.ForEach(e => e.Refresh());

            m_BKKs.ForEach(e => e.Refresh());

            m_ApuInputs.ForEach(e => e.Refresh());


        }

        protected virtual void InitPowerLines()
        {
            m_PowerLines = new PowerLines(new Point(0, m_Ack1s[0].OutputWire.EndPoint.Y + 10));
        }

        protected virtual void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            // mtr
            var mat =
                MatrixHelper.CreateTurnMatrix(LineHelper.GetMidPoint(m_MTrs[0].Location, m_MTrs[1].Location).X,
                    TurnOrientation.Horizontal);

            m_MTrs.ForEach(e => { e.Location = mat.TransformPoint(e.Location); });

            // ack1
            mat =
                MatrixHelper.CreateTurnMatrix(LineHelper.GetMidPoint(m_Ack1s[0].Location, m_Ack1s[1].Location).X,
                    TurnOrientation.Horizontal);
            m_Ack1s.ForEach(e => e.Location = mat.TransformPoint(e.Location));

            // apu
            mat =
                MatrixHelper.CreateTurnMatrix(LineHelper.GetMidPoint(m_Apus[0].Location, m_Apus[1].Location).X,
                    TurnOrientation.Horizontal);
            mat.Translate(-1, 0);
            m_Apus.ForEach(e =>
            {
                e.Location = mat.TransformPoint(e.Location);
                e.Reverse();
            });

            // atr
            mat =
                MatrixHelper.CreateTurnMatrix(LineHelper.GetMidPoint(m_ATrs[0].Location, m_ATrs[1].Location).X,
                    TurnOrientation.Horizontal);
            mat.Translate(-1, 0);
            m_ATrs.ForEach(e =>
            {
                e.Location = mat.TransformPoint(e.Location);
                e.Reverse();
            });

            // m_3PhMks
            mat =
                MatrixHelper.CreateTurnMatrix(LineHelper.GetMidPoint(m_3PhMks[0].Location, m_3PhMks[1].Location).X,
                    TurnOrientation.Horizontal);
            m_3PhMks.ForEach(e =>
            {
                e.Location = mat.TransformPoint(e.Location);
                e.Reverse();
            });

            m_PowerLines.Reverse();

            CorrectWires();
        }

        protected virtual void CorrectWires()
        {
            var y1 = m_PowerLines[PowerLineType.AC4001].LeftWire.StartPoint.Y;
            var y2 = m_PowerLines[PowerLineType.AC4002].LeftWire.StartPoint.Y;
            var y3 = m_PowerLines[PowerLineType.DC100].LeftWire.StartPoint.Y;
            var y4 = m_PowerLines[PowerLineType.AC220].LeftWire.StartPoint.Y;
            var y5 = m_PowerLines[PowerLineType.AC1001].LeftWire.StartPoint.Y;
            var y6 = m_PowerLines[PowerLineType.AC1002].LeftWire.StartPoint.Y;

            foreach (var ack1 in m_Ack1s)
            {
                //ack1.OutputWire.EndPoint.Y = ac4001Y;
                ack1.OutputWire.EndPoint = new Point(ack1.OutputWire.EndPoint.X, y1);
                ack1.OutputWire.IsDrawEndPoint = true;
            }

            var minApuX = m_Apus.Min(m => m.Location.X);
            var apu1 = m_Apus.Find(w => w.Location.X == minApuX);
            if (!apu1.StraightWires.Any())
            {
                apu1.StraightWires = new List<StraightWire>
                                     {
                                         //new StraightWire()
                                         //{
                                         //    StartPoint =
                                         //        new Point(apu1.ActureOutLine.X + apu1.ActureOutLine.Width/2, apu1.ActureOutLine.Top),
                                         //    EndPoint = new Point(apu1.ActureOutLine.X + apu1.ActureOutLine.Width/2, y1),
                                         //    IsDrawEndPoint = true,
                                         //    //IsPowerOn = false,
                                         //},
                                         new StraightWire()
                                         {
                                             StartPoint = new Point(apu1.ActureOutLine.X + 4, apu1.ActureOutLine.Bottom),
                                             EndPoint = new Point(apu1.ActureOutLine.X + 4, y5),
                                             IsDrawEndPoint = true,
                                             //IsPowerOn = false,
                                         },
                                         new StraightWire()
                                         {
                                             StartPoint = new Point(apu1.ActureOutLine.X + 8, apu1.ActureOutLine.Bottom),
                                             EndPoint = new Point(apu1.ActureOutLine.X + 8, y4),
                                             IsDrawEndPoint = true,
                                             //IsPowerOn = false,
                                         },
                                         new StraightWire()
                                         {
                                             StartPoint = new Point(apu1.ActureOutLine.X + 12, apu1.ActureOutLine.Bottom),
                                             EndPoint = new Point(apu1.ActureOutLine.X + 12, y3),
                                             IsDrawEndPoint = true,
                                             //IsPowerOn = false,
                                         },
                                         //m_PowerLines[PowerLineType.AC4001].LeftWire
                                     };
            }
            else
            {
                apu1.StraightWires.ForEach(e => e.Refresh());
            }


            var maxApuX = m_Apus.Max(m => m.Location.X);
            var apu2 = m_Apus.Find(w => w.Location.X == maxApuX);
            if (!apu2.StraightWires.Any())
            {
                apu2.StraightWires = new List<StraightWire>
                                     {
                                         //new StraightWire()
                                         //{
                                         //    StartPoint =
                                         //        new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width/2, apu2.ActureOutLine.Top),
                                         //    EndPoint = new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width/2, y1),
                                         //    IsDrawEndPoint = true,
                                         //    //IsPowerOn = false,
                                         //},
                                         new StraightWire()
                                         {
                                             StartPoint =
                                                 new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width / 2 + 4, apu2.ActureOutLine.Bottom),
                                             EndPoint = new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width / 2 + 4, y3),
                                             IsDrawEndPoint = true,
                                             //IsPowerOn = false,
                                         },
                                         new StraightWire()
                                         {
                                             StartPoint =
                                                 new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width / 2 + 8, apu2.ActureOutLine.Bottom),
                                             EndPoint = new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width / 2 + 8, y4),
                                             IsDrawEndPoint = true,
                                             //IsPowerOn = false,
                                         },
                                         new StraightWire()
                                         {
                                             StartPoint =
                                                 new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width / 2 + 12, apu2.ActureOutLine.Bottom),
                                             EndPoint = new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width / 2 + 12, y5),
                                             IsDrawEndPoint = true,
                                             //IsPowerOn = false,
                                         },
                                         //m_PowerLines[PowerLineType.AC4001].RightWire
                                     };
            }
            else
            {
                apu2.StraightWires.ForEach(e => e.Refresh());
            }



            foreach (var aTr in m_ATrs)
            {
                aTr.InputWire.EndPoint = new Point(aTr.InputWire.EndPoint.X, y1);
                aTr.InputWire.IsDrawEndPoint = true;
                aTr.OutPutWire.EndPoint = new Point(aTr.OutPutWire.EndPoint.X, y6);
                aTr.OutPutWire.IsDrawEndPoint = true;
            }


            {
                m_ApuInputs[0] = new PowerUnitCollectoin<StraightWire>()
                {
                    PowerUnits = new List<StraightWire>()
                                               {
                                                   new StraightWire()
                                                   {
                                                       StartPoint =
                                                           new Point(apu1.ActureOutLine.X + apu1.ActureOutLine.Width / 2, apu1.ActureOutLine.Top),
                                                       EndPoint = new Point(apu1.ActureOutLine.X + apu1.ActureOutLine.Width / 2, y1),
                                                       IsDrawEndPoint = true,
                                                       //IsPowerOn = false,
                                                   },
                                                   m_ATrs[0].InputWire,
                                                   m_PowerLines[PowerLineType.AC4001].LeftWire
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

            {
                m_ApuInputs[1] = new PowerUnitCollectoin<StraightWire>()
                {
                    RefreshAction = o =>
                    {
                        var pu = ((PowerUnitCollectoin<StraightWire>)o);
                        pu.IsPowerOn = true;
                        pu.RefreshState();
                    },
                    PowerUnits = new List<StraightWire>()
                                                  {
                                                      new StraightWire()
                                                      {
                                                          StartPoint =
                                                              new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width / 2, apu2.ActureOutLine.Top),
                                                          EndPoint = new Point(apu2.ActureOutLine.X + apu2.ActureOutLine.Width / 2, y1),
                                                          IsDrawEndPoint = true,
                                                          //IsPowerOn = false,
                                                      },
                                                      m_ATrs[1].InputWire,
                                                      m_PowerLines[PowerLineType.AC4001].RightWire
                                                  }
                };
            }

            foreach (var mk in m_3PhMks)
            {
                mk.StraightWire.IsDrawEndPoint = true;
            }
        }

        protected void InitBtns()
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


        public void ReselectConditionBtn(CRH2Button btn)
        {
            if (m_SelectedConditionBtn != null)
            {
                m_SelectedConditionBtn.CurrentMouseState = MouseState.MouseUp;
            }
            m_SelectedConditionBtn = btn;
            if (btn != null)
            {
                btn.CurrentMouseState = MouseState.MouseDown;
            }
        }


        /// <summary>
        /// 初始2,6车的MTR
        /// </summary>
        protected void InitMtrs()
        {
            var mtr0 =
                GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.MtrInBools.Find(
                    f => f.CarNo == 1);
            var mtr1 = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.MtrInBools.Find(
                    f => f.CarNo == 5);
            m_MTrs = new List<MTr>()
            {
                new MTr(PowerFrom.MTr1)
                {
                    Location = new Point(270, 220),
                    CarNo = 1,
                    RefreshAction = o => MtrRefreshAction(o,mtr0)
                },
                new MTr(PowerFrom.MTr2)
                {
                    Location = new Point(270 + 88*2, 220),
                    CarNo = 5,
                    RefreshAction = o => MtrRefreshAction(o, mtr1)
                }
            };
        }

        protected void MtrRefreshAction(object o, PowerClassifyUnitModel config)
        {
            var mtr = o as MTr;
            Debug.Assert(mtr != null, "mtr != null");

            mtr.IsPowerOn = m_PowerTpe.PowerTypeResource.GetState(config);

            if (mtr.IsPowerOn)
            {
                mtr.Color = PowerFromColorAdaptor.GetColor(mtr.PowerFrom);
            }
        }

        protected void ApuRefreshAction(object o, PowerClassifyUnitModel config)
        {
            var apu = o as IPowerUnit;
            Debug.Assert(apu != null, "apu != null");

            var pf = GetApuPowerFrom(config);
            apu.IsPowerOn = pf != PowerFrom.Null;

            apu.RefreshByState(pf);
        }
        protected void InitAck1s()
        {
            m_Ack1s = new List<ACK1>();
            foreach (var mTr in m_MTrs)
            {
                var ack1 =
                    GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.ACK1InBools.Find(
                        f => f.CarNo == mTr.CarNo);
                m_Ack1s.Add(new ACK1()
                {
                    Location =
                        new Point(mTr.OutputWire.EndPoint.X - ACK1.WireXOffset,
                        mTr.OutputWire.EndPoint.Y - 5),
                    CarNo = mTr.CarNo,
                    RefreshAction = o => OnPowerUnitRefreshAction(o as CircuitChangerUnit, ack1),

                });
            }
        }

        protected void OnPowerUnitRefreshAction(CircuitChangerUnit powerUnit, PowerClassifyUnitModel config)
        {
            powerUnit.IsPowerOn = m_PowerTpe.PowerTypeResource.GetState(config);
            powerUnit.RefreshState();
        }

        protected void InitAck2s()
        {
            var ack2 = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.ACK2InBools[0];
            m_ACK2s = new List<ACK2>()
            {
                new ACK2()
                {
                    Location = new Point(270 + 88, m_Ack1s[0].OutputWire.EndPoint.Y + 5),
                    CircuitInputDirection = Direction.Left,
                    RefreshAction = o => OnPowerUnitRefreshAction(o as CircuitChangerUnit, ack2)
                }
            };
        }

        protected int InitApu()
        {
            var apuY = m_ACK2s[0].ActureOutLine.Bottom - 5;
            var apuConfig = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.APUInBools;
            var apu0Config = apuConfig.Find(f => f.CarNo == 0);
            var apu7Config = apuConfig.Find(f => f.CarNo == 7);
            m_Apus = new List<Apu>()
            {
                new Apu()
                {
                    Location = new Point(m_MTrs[0].ActureOutLine.Left - Apu.DefaultSize.Width, apuY),
                    CarNo = 0,
                    RefreshAction = o => ApuRefreshAction(o, apu0Config)
                },
                new Apu()
                {
                    Location = new Point(m_Ack1s[1].ActureOutLine.Right + ATr.RectSize.Width, apuY),
                    CarNo = 7,
                    RefreshAction = o => ApuRefreshAction(o, apu7Config)
                },
            };
            return apuY;
        }

        protected void InitBkk()
        {
            var bkkconfig = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.BKKInBools[0];
            m_BKKs = new List<CircuitChangerUnit>()
            {
                new BKK()
                {
                    Location =
                        new Point(m_ACK2s[0].ActureOutLine.X, m_3PhMks[0].StraightWire.EndPoint.Y - BKK.WireYOffset),
                    CircuitInputDirection = Direction.Left,
                    CarNo = 3,
                    RefreshAction = o =>OnPowerUnitRefreshAction(o as CircuitChangerUnit, bkkconfig)
                }
            };
        }

        protected void Init3PhMk()
        {
            var phmkY = m_Apus[0].ActureOutLine.Bottom + 2;
            var apuConfig = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.APUInBools;
            m_3PhMks = new List<ThirdPhMk>()
            {
                new ThirdPhMk()
                {
                    TextDirection = Direction.Right,
                    Location = new Point(m_Apus[0].ActureOutLine.X + m_Apus[0].ActureOutLine.Width/3*2, phmkY),
                    CarNo = 0,
                    RefreshAction = o => ApuRefreshAction(o, apuConfig.Find(f => f.CarNo == 0)),
                }
            };
            m_3PhMks.Add(new ThirdPhMk()
            {
                TextDirection = Direction.Left,
                Location =
                    new Point(
                        m_Apus[1].ActureOutLine.X + m_Apus[1].ActureOutLine.Width / 2 - m_3PhMks[0].ActureOutLine.Width -
                        3,
                        phmkY),
                CarNo = 7,
                RefreshAction = o => ApuRefreshAction(o, apuConfig.Find(f => f.CarNo == 7))
            });
        }

        protected void InitAtr(int apuY)
        {
            var atrY = apuY - ATr.DefaultWireLeght;
            const int atrApuInterval = 3;
            var apuConfig = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.APUInBools;
            m_ATrs = new List<ATr>()
            {
                new ATr()
                {
                    TextDirection = Direction.Right,
                    Location = new Point(m_Apus[0].ActureOutLine.Right + atrApuInterval, atrY),
                    CarNo = 0,
                    RefreshAction = o => ApuRefreshAction(o, apuConfig.Find(f => f.CarNo == 0))
                }
            };
            m_ATrs.Add(new ATr()
            {
                TextDirection = Direction.Left,
                Location =
                    new Point(m_Apus[1].ActureOutLine.Left - m_ATrs[0].ActureOutLine.Width - atrApuInterval, atrY),
                CarNo = 7,
                RefreshAction = o => ApuRefreshAction(o, apuConfig.Find(f => f.CarNo == 7))
            });
        }


        protected virtual PowerFrom GetApuPowerFrom(PowerClassifyUnitModel config)
        {
            return PowerFrom.Null;
        }

        protected int ResetLine(PowerLineType type)
        {
            var y = m_PowerLines[type].Wires.First().StartPoint.Y;
            m_PowerLines[type].Wires.Clear();
            return y;
        }


    }
}
