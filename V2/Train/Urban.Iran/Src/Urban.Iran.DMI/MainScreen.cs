using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.DMI.Controls;
using Urban.Iran.DMI.Index;
using Urban.Iran.DMI.Index.IndexKeys;
using Urban.Iran.DMI.Model;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class MainScreen : baseClass
    {
        /// <summary>
        /// 数据和 spec
        /// </summary>
        private List<FjButtonEx> m_BtnArr;

        private List<ControlModelButton> m_ControlModelButtons;

        private DoorStateControl m_DoorStateControl;

        private DoorStateControl m_DoorStateControl1;

        private RectangleImage m_SkipStopIndicator;

        private DWellControl m_DWellControl;

        private BrakeControl m_BrakeControl;

        private RectangleImage m_ATOErrorControl;

        private GDIRectText m_DriverIDControl;

        private GDIRectText m_TrainIDControl;

        private GDIRectText ZeroFlag;

        private void btn_onMouseDown(FjButtonEx btnSender, Point pt)
        {
            switch (btnSender.m_BtnIndex)
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 53, 0, 0);
                    break;
                case 1:
                    append_postCmd(CmdType.ChangePage, 56, 0, 0);
                    break;
            }
        }

        public override string GetInfo()
        {
            return "MainScreen";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitalizeControlModelBtns();

            InitalizeDoorStateControl();

            InitalizeSkipStopControl();

            InitalizeDwellControl();

            InitalizeBrakeControl();

            InitalizeATOFaultControl();

            InitalizeIdControl();

            InitalizeDataAndSpecBtn();

            InitalizeZeroFlag();

            return true;
        }
        private void InitalizeZeroFlag()
        {
            ZeroFlag = new GDIRectText
            {
                NeedDarwOutline = false,
                OutLineRectangle = new Rectangle(255, 325, 45, 10),
                Visible = false,
                BackColorVisible = true,
                BKBrush = GdiCommon.MediumGreyBrush,
                Tag = IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.零速标志],
                RefreshAction = (o) =>
                {

                    ((GDIRectText)o).Visible = BoolList[Convert.ToInt32(((GDIRectText)o).Tag)];
                }
            };

        }
        private void InitalizeIdControl()
        {
            var didx = IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.司机号];
            var tidx = IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.车次号];
            m_DriverIDControl = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(350, 40, 150, 30),
                DrawFont = GdiCommon.Txt12Font,
                TextBrush = GdiCommon.MediumGreyBrush,
                BackColorVisible = false,
                RefreshAction = o =>
                {
                    var txt = (GDIRectText)o;
                    txt.Text = string.Format("Driver no.  {0}", FloatList[didx].ToString("000000"));
                }
            };
            m_TrainIDControl = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(350, 70, 150, 30),
                DrawFont = GdiCommon.Txt12Font,
                TextBrush = GdiCommon.MediumGreyBrush,
                BackColorVisible = false,
                RefreshAction = o =>
                {
                    var txt = (GDIRectText)o;
                    txt.Text = string.Format("Driver no.  {0}", FloatList[tidx].ToString("000000"));
                }
            };
            UIObj.InFloatList.Add(didx);
            UIObj.InFloatList.Add(tidx);
        }

        private void InitalizeIDControl()
        {
            var didx = IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.司机号];
            var tidx = IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.车次号];
            m_DriverIDControl = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(350, 40, 150, 30),
                DrawFont = GdiCommon.Txt12Font,
                TextBrush = GdiCommon.MediumGreyBrush,
                RefreshAction = o =>
                {
                    var txt = (GDIRectText)o;
                    txt.Text = string.Format("Driver no.  {0}", FloatList[didx].ToString("000000"));
                }
            };
            m_TrainIDControl = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(350, 70, 150, 30),
                DrawFont = GdiCommon.Txt12Font,
                TextBrush = GdiCommon.MediumGreyBrush,
                RefreshAction = o =>
                {
                    var txt = (GDIRectText)o;
                    txt.Text = string.Format("Driver no.  {0}", FloatList[tidx].ToString("000000"));
                }
            };
            UIObj.InFloatList.Add(didx);
            UIObj.InFloatList.Add(tidx);
        }

        private void InitalizeATOFaultControl()
        {
            var index = IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ATOFault];
            m_ATOErrorControl = new RectangleImage()
            {
                OutLineRectangle = new Rectangle(175, 265, 45, 45),
                Image = ImageResourceFacade.AtoError,
                RefreshAction =
                    o =>
                    {
                        var im = (RectangleImage)o;
                        im.Visible = BoolList[index];
                    }
            };
            UIObj.InBoolList.Add(index);
        }

        private void InitalizeBrakeControl()
        {
            m_BrakeControl = new BrakeControl(this)
            {
                OutLineRectangle = new Rectangle(5, 330, 60, 30),
                BrakeTypeIndexCollection =
                    new ReadOnlyCollection<Tuple<BrakeType, int, int>>(new List<Tuple<BrakeType, int, int>>()
                    {
                        new Tuple<BrakeType, int,int>(BrakeType.Emergent,
                            IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.触发紧急制动],-1),
                        new Tuple<BrakeType, int,int>(BrakeType.Ordinary,
                            IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.常用制动],-1),
                        new Tuple<BrakeType, int,int>(BrakeType.OrdinaryFlicker,
                            IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.常用制动闪烁],IndexParam.Instance.OutBoolKeyIndexDictionary[OutBoolKeys.常用制动闪烁按钮指令]),
                    })
            };
            UIObj.InBoolList.AddRange(m_BrakeControl.BrakeTypeIndexCollection.Select(s => s.Item2));
        }

        private void InitalizeDwellControl()
        {
            m_DWellControl = new DWellControl(this)
            {
                OutLineRectangle = new Rectangle(230, 320, 105, 18),
                StateIndexCollection =
                    new ReadOnlyCollection<Tuple<DWellState, int>>(new List<Tuple<DWellState, int>>()
                    {
                        new Tuple<DWellState, int>(DWellState.TimeIndicator,
                            IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.离站倒计时显示]),
                        new Tuple<DWellState, int>(DWellState.TrainHold,
                            IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.扣车]),
                    }),
                TimeIndex = IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.离站倒计时],
                DrawFont = GdiCommon.Txt10Font,
            };

            UIObj.InBoolList.AddRange(m_DWellControl.StateIndexCollection.Select(s => s.Item2));
            UIObj.InFloatList.Add(m_DWellControl.TimeIndex);
        }

        private void InitalizeSkipStopControl()
        {
            var index = IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.跳停];

            m_SkipStopIndicator = new RectangleImage()
            {
                OutLineRectangle = new Rectangle(345, 365, 45, 45),
                Image = ImageResourceFacade.skipStop,
                RefreshAction =
                    o =>
                    {
                        var control = (RectangleImage)o;
                        control.Visible = BoolList[index];
                    }
            };
            UIObj.InBoolList.Add(index);
        }

        private void InitalizeDoorStateControl()
        {
            m_DoorStateControl1 = new DoorStateControl(this)
            {
                OutLineRectangle = new Rectangle(390, 315, 45, 45),
                StateImageDictionary = new ReadOnlyDictionary<DoorState, Image>(new Dictionary<DoorState, Image>()
                {
                    {DoorState.Unkown, null},
                    {DoorState.LeftPermmit, ImageResourceFacade.openLeft},
                    {DoorState.RightPermmit, ImageResourceFacade.openRight},
                    {DoorState.AllPermmit, ImageResourceFacade.openAll},
                    {DoorState.AllClosed, ImageResourceFacade.closeAll},
                    {DoorState.AnyOpened, ImageResourceFacade.drOpen},
                    {DoorState.AnyOpenedOnRunning, ImageResourceFacade.drError},
                }),
                DoorStateIndexTuples = (new List<Tuple<int, DoorState>>()
                {
                    new Tuple<int, DoorState>(IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.所有车门关闭],
                        DoorState.AllClosed),
                    new Tuple<int, DoorState>(IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.有车门打开],
                        DoorState.AnyOpened),
                    new Tuple<int, DoorState>(IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.运行时车门打开],
                        DoorState.AnyOpenedOnRunning),
                }).AsReadOnly(),

            };
            m_DoorStateControl = new DoorStateControl(this)
            {
                OutLineRectangle = new Rectangle(345, 315, 45, 45),
                StateImageDictionary = new ReadOnlyDictionary<DoorState, Image>(new Dictionary<DoorState, Image>()
                {
                    {DoorState.Unkown, null},
                    {DoorState.LeftPermmit, ImageResourceFacade.openLeft},
                    {DoorState.RightPermmit, ImageResourceFacade.openRight},
                    {DoorState.AllPermmit, ImageResourceFacade.openAll},
                    {DoorState.AllClosed, ImageResourceFacade.closeAll},
                    {DoorState.AnyOpened, ImageResourceFacade.drOpen},
                    {DoorState.AnyOpenedOnRunning, ImageResourceFacade.drError},
                }),
                DoorStateIndexTuples = (new List<Tuple<int, DoorState>>()
                {
                    new Tuple<int, DoorState>(IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.左侧门允许],
                        DoorState.LeftPermmit),
                    new Tuple<int, DoorState>(IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.右侧门允许],
                        DoorState.RightPermmit),
                    new Tuple<int, DoorState>(IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.两侧门允许],
                        DoorState.AllPermmit),

                }).AsReadOnly(),
            };

            UIObj.InBoolList.AddRange(m_DoorStateControl.DoorStateIndexTuples.Select(s => s.Item1));
        }

        private void InitalizeDataAndSpecBtn()
        {
            m_BtnArr = new List<FjButtonEx>()
            {
                new FjButtonEx(0, new Rectangle(580, 362, 57, 48), ImageResourceFacade.data),
                new FjButtonEx(1, new Rectangle(580, 412, 57, 48), ImageResourceFacade.spec),
            };

            foreach (var btn in m_BtnArr)
            {
                btn.onMouseDown += btn_onMouseDown;
            }
        }

        private void InitalizeControlModelBtns()
        {
            var btnRectArr = new Rectangle[4];
            btnRectArr[0] = new Rectangle(580, 12, 57, 48);
            btnRectArr[1] = new Rectangle(580, 62, 57, 48);
            btnRectArr[2] = new Rectangle(580, 112, 57, 48);
            btnRectArr[3] = new Rectangle(580, 162, 57, 48);

            m_ControlModelButtons = btnRectArr.Select(s => new ControlModelButton(this) { OutLineRectangle = s }).ToList();
            m_ControlModelButtons[0].StateImageDictionary = new Dictionary<ControlModelState, Image>()
            {
                {ControlModelState.Normal, ImageResourceFacade.ATBNormal},
                {ControlModelState.Request, ImageResourceFacade.ATBNormal},
                {ControlModelState.Permmit, ImageResourceFacade.ATBApply},
                {ControlModelState.Actived, ImageResourceFacade.ATBSel},
                {ControlModelState.UnKnow, ImageResourceFacade.ATBApply1},
            }.AsReadOnlyDictionary();
            m_ControlModelButtons[1].StateImageDictionary = new Dictionary<ControlModelState, Image>()
            {
                {ControlModelState.Normal, ImageResourceFacade.AUTONormal},
                {ControlModelState.Request, ImageResourceFacade.AUTONormal},
                {ControlModelState.Permmit, ImageResourceFacade.AUTOApply},
                {ControlModelState.Actived, ImageResourceFacade.AUTOSel},
                 {ControlModelState.UnKnow, ImageResourceFacade.AUTOApply1},
            }.AsReadOnlyDictionary();
            m_ControlModelButtons[2].StateImageDictionary = new Dictionary<ControlModelState, Image>()
            {
                {ControlModelState.Normal, ImageResourceFacade.ATPNormal},
                {ControlModelState.Request, ImageResourceFacade.ATPNormal},
                {ControlModelState.Permmit, ImageResourceFacade.ATPApply},
                {ControlModelState.Actived, ImageResourceFacade.ATPSel},
                {ControlModelState.UnKnow, ImageResourceFacade.ATPApply1},
            }.AsReadOnlyDictionary();
            m_ControlModelButtons[3].StateImageDictionary = new Dictionary<ControlModelState, Image>()
            {
                {ControlModelState.Normal, ImageResourceFacade.RMNormal},
                {ControlModelState.Request, ImageResourceFacade.RMNormal},
                {ControlModelState.Permmit, ImageResourceFacade.RMApply},
                {ControlModelState.Actived, ImageResourceFacade.RMSel},
                {ControlModelState.UnKnow, ImageResourceFacade.RMApply1},
            }.AsReadOnlyDictionary();

            m_ControlModelButtons[0].StateDictionary =
                new ReadOnlyDictionary<int, ControlModelState>(new Dictionary<int, ControlModelState>()
                {
                    {IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ATB允许], ControlModelState.Permmit},
                    {IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ATB模式], ControlModelState.Actived},
                });
            m_ControlModelButtons[1].StateDictionary =
             new ReadOnlyDictionary<int, ControlModelState>(new Dictionary<int, ControlModelState>()
                {
                    {IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ATO允许], ControlModelState.Permmit},
                    {IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ATO模式], ControlModelState.Actived},
                });

            m_ControlModelButtons[2].StateDictionary =
             new ReadOnlyDictionary<int, ControlModelState>(new Dictionary<int, ControlModelState>()
                {
                    {IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ATP允许], ControlModelState.Permmit},
                    {IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ATP模式], ControlModelState.Actived},
                });

            m_ControlModelButtons[3].StateDictionary =
             new ReadOnlyDictionary<int, ControlModelState>(new Dictionary<int, ControlModelState>()
                {
                    {IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.RM允许], ControlModelState.Permmit},
                    {IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.RM模式], ControlModelState.Actived},
                    {IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.退行模式],ControlModelState.Actived},
                });

            m_ControlModelButtons[0].ButtonDownEvent =
                (sender, args) => SendBool(IndexParam.Instance.OutBoolKeyIndexDictionary[OutBoolKeys.ATB按钮指令], 1);
            m_ControlModelButtons[0].ButtonUpEvent =
                (sender, args) => SendBool(IndexParam.Instance.OutBoolKeyIndexDictionary[OutBoolKeys.ATB按钮指令], 0);

            m_ControlModelButtons[1].ButtonDownEvent =
             (sender, args) => SendBool(IndexParam.Instance.OutBoolKeyIndexDictionary[OutBoolKeys.ATO按钮指令], 1);
            m_ControlModelButtons[1].ButtonUpEvent =
                (sender, args) => SendBool(IndexParam.Instance.OutBoolKeyIndexDictionary[OutBoolKeys.ATO按钮指令], 0);

            m_ControlModelButtons[2].ButtonDownEvent =
             (sender, args) => SendBool(IndexParam.Instance.OutBoolKeyIndexDictionary[OutBoolKeys.ATP按钮指令], 1);
            m_ControlModelButtons[2].ButtonUpEvent =
                (sender, args) => SendBool(IndexParam.Instance.OutBoolKeyIndexDictionary[OutBoolKeys.ATP按钮指令], 0);

            m_ControlModelButtons[3].ButtonDownEvent =
             (sender, args) => SendBool(IndexParam.Instance.OutBoolKeyIndexDictionary[OutBoolKeys.RM按钮指令], 1);
            m_ControlModelButtons[3].ButtonUpEvent =
                (sender, args) => SendBool(IndexParam.Instance.OutBoolKeyIndexDictionary[OutBoolKeys.RM按钮指令], 0);

            UIObj.InBoolList.AddRange(m_ControlModelButtons.Select(s => s.StateDictionary).SelectMany(s => s.Keys));

        }

        private void SendBool(int index, int value)
        {
            append_postCmd(CmdType.SetBoolValue, index, value, 0);
        }


        public override void paint(Graphics g)
        {
            g.FillRectangle(GdiCommon.DarkBlueBrush, GlobleRect.BKRect);

            m_DriverIDControl.OnPaint(g);

            m_TrainIDControl.OnPaint(g);

            //ATO ERROR
            m_ATOErrorControl.OnPaint(g);

            //Dewll time
            m_DWellControl.OnPaint(g);

            //ATP Brake
            m_BrakeControl.OnPaint(g);


            m_ControlModelButtons.ForEach(e => e.OnPaint(g));

            //车门状态
            m_DoorStateControl.OnPaint(g);
            m_DoorStateControl1.OnPaint(g);

            //skipstop
            m_SkipStopIndicator.OnPaint(g);

            foreach (var btn in m_BtnArr)
            {
                btn.OnDraw(g);
            }
            ZeroFlag.OnPaint(g);
            base.paint(g);
        }

        public override bool mouseDown(Point point)
        {
            foreach (var btn in m_BtnArr)
            {
                if (btn.IsVisible(point))
                {
                    btn.OnMouseDown(point);
                    break;
                }
            }
            m_BrakeControl.OnMouseDown(point);
            return m_ControlModelButtons.Any(a => a.OnMouseDown(point));
        }

        public override bool mouseUp(Point point)
        {
            m_BrakeControl.OnMouseUp(point);
            return m_ControlModelButtons.Any(a => a.OnMouseUp(point));
        }
    }
}