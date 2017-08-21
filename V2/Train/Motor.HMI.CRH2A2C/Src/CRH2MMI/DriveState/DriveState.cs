using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Util;using CommonUtil.Util.Extension;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using CRH2MMI.CutState;
using CRH2MMI.TrainLineNo;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;

namespace CRH2MMI.DriveState
{
    /// <summary>
    /// //行驶状态
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class DriveState : CRH2BaseClass
    {
        private readonly RectTextInfo m_RecTrainLine = new RectTextInfo();

        private TrainView m_TrainView;

        private List<GDIRectText> m_ConstInfo;

        private Dictionary<CutValueFrom, List<GDIRectText>> m_CutInfos;

        private List<GDIRectText> m_DoorColseInfos;

        private RemovalStateQueue m_RemovalStateQueue;

        public override void paint(Graphics g)
        {
            RemovalStateMgr.Instance.RefreshStates();

            m_ConstInfo.ForEach(e => e.OnDraw(g));

            m_RecTrainLine.OnDraw(g);

            m_DoorColseInfos.ForEach(e => e.OnPaint(g));

            if (m_CutInfos.ContainsKey(CutValueFrom.Bool))
            {
                m_CutInfos[CutValueFrom.Bool].ForEach(e => e.OnPaint(g));
            }
            if (m_CutInfos.ContainsKey(CutValueFrom.Event))
            {
                m_CutInfos[CutValueFrom.Event].ForEach(e => e.OnDraw(g));
            }
        }
       
        public override bool Init()
        {

            m_TrainView = TrainView.Instance;

            var models =
                GlobalInfo.CurrentCRH2Config.DriveStateConfig.DoorConfigs
                    .Cast<CRH2CommunicationPortModel>().ToList();
            models.AddRange(
                GlobalInfo.CurrentCRH2Config.DriveStateConfig.CutInfoRegions.SelectMany(
                    s => s.DriveStateCutInfoList).Cast<CRH2CommunicationPortModel>());

            InitUIObj(models);

            InitDate();

            InitCutinfo();

            InitConstInfo();

            InitDoorColseInfo();

            GlobalEvent.Instance.ReversalChanged += OnReversalChanged;

            RemovalStateMgr.Instance.RemovalStateChanged += OnRemovalStateChanged;

            return true;
        }

        private void OnRemovalStateChanged(object sender, RemovalStateChangedArgs removalStateChangedArgs)
        {
            switch (removalStateChangedArgs.Type)
            {
                case RemovalStateChangedType.Add:
                    if (m_RemovalStateQueue != null)
                    {
                        m_RemovalStateQueue.Add(removalStateChangedArgs.OccuceRemovalState);
                    }
                    break;
                case RemovalStateChangedType.Removel:
                    if (m_RemovalStateQueue != null)
                    {
                        m_RemovalStateQueue.Delete(removalStateChangedArgs.OccuceRemovalState);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            UpdateCutStates();
        }

        private void UpdateCutStates()
        {
            if (m_CutInfos != null && m_CutInfos.ContainsKey(CutValueFrom.Event))
            {
                var infos = m_CutInfos[CutValueFrom.Event];
                for (int i = 0; i < infos.Count; i++)
                {
                    infos[i].Text = m_RemovalStateQueue[i];
                }
            }
        }

        private void InitCutinfo()
        {
            var cutInfoRegions = GlobalInfo.CurrentCRH2Config.DriveStateConfig.CutInfoRegions;

            m_CutInfos = new Dictionary<CutValueFrom, List<GDIRectText>>();

            foreach (var gp in cutInfoRegions.GroupBy(g => g.CutValueFrom))
            {
                if (gp.Key == CutValueFrom.Bool)
                {
                    m_CutInfos.Add(gp.Key, gp.Select(s => new GDIRectText()
                    {
                        BkColor = Color.Black,
                        OutLineRectangle = s.Rectangle,
                        TextColor = Color.Yellow,
                        Bold = true,

                        RefreshAction = o =>
                        {

                            var txt = (GDIRectText) o;
                            var show = s.DriveStateCutInfoList.FirstOrDefault(f => GetInBoolValue(f.InBoolColoumNames.First()));
                            txt.Visible = false;
                            if (show != null)
                            {
                                txt.Visible = true;
                                txt.Text = show.Text;
                            }
                        },
                    }).ToList());
                }
                else
                {
                    m_RemovalStateQueue = new RemovalStateQueue((uint) gp.Count());
                    m_CutInfos.Add(gp.Key, gp.Select(s => new GDIRectText()
                    {
                        BkColor = Color.Black,
                        OutLineRectangle = s.Rectangle,
                        TextColor = Color.Yellow,
                        Bold = true,
                    }).ToList());
                }
            }
        }

        private void InitDoorColseInfo()
        {
            var doors = GlobalInfo.CurrentCRH2Config.DriveStateConfig.DoorConfigs;

            m_DoorColseInfos = doors.Select(s => new GDIRectText()
            {
                OutLineRectangle = s.Rectangle,
                BkColor = Color.Yellow,
                RefreshAction = o =>
                {
                    var txt = (GDIRectText) o;
                    txt.Visible = GetInBoolValue(s.InBoolColoumNames.First());
                }
            }).ToList();

        }


        private void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            float minx = m_CutInfos.Values.SelectMany(s => s).Min(m => m.Location.X);
            float maxx = m_CutInfos.Values.SelectMany(s => s).Max(m => m.Location.X);

            var mat = MatrixHelper.CreateTurnMatrix((minx + maxx)/2, TurnOrientation.Horizontal);
            m_CutInfos.Values.SelectMany(s => s).ToList().ForEach(e => e.Location = mat.TransformPoint(e.Location));


            var doorMat = new Matrix();
            doorMat.Translate(1, 0);
            doorMat.Multiply(
                MatrixHelper.CreateTurnMatrix(
                    (m_DoorColseInfos.Min(m => m.Location.X) + m_DoorColseInfos.Max(m => m.Location.X))/2,
                    TurnOrientation.Horizontal));
            
            m_DoorColseInfos.ForEach(e => e.Location = doorMat.TransformPoint(e.Location));
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {

                switch (nParaB)
                {
                    case ParaADefine.SwitchFromOhter:
                        InitDate();
                        break;
                }
            }
        }

        public override string GetInfo()
        {
            return "行驶状态";
        }

        public void InitDate()
        {
            var location = GlobalInfo.CurrentCRH2Config.DriveStateConfig.TrainViewLocation.Rectangle.Location;
            m_TrainView.Location = location;
            m_TrainView.NeedDrawCarName = true;
            m_TrainView.IsUnitStateVisible = true;
            m_TrainView.IsDriverRoomVisible = true;
            m_TrainView.IsUnitNameVisible = true;

            m_RecTrainLine.SetRectTextInfo(location.X, 162, Car.DefaultSize.Width* GlobalParam.Instance.CarCount - 10, 48, 0, TNSET.TrainLine, 0, 3, 0, 12);

           
        }


        private void InitConstInfo()
        {
            var textSize = new Size(100, 20);

            m_ConstInfo = new List<GDIRectText>()
            {
                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle =
                        new Rectangle(new Point(1, 170),
                            new Size(textSize.Width, textSize.Height)),
                    TextColor = Color.White,
                    Text = "车   次",
                    NeedDarwOutline = false,
                },

                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle =
                        new Rectangle(new Point(1, 303),
                            new Size(textSize.Width, textSize.Height)),
                    TextColor = Color.White,
                    Text = "切   除",
                    NeedDarwOutline = false,
                },


                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle =
                        new Rectangle(new Point(1, 400),
                            new Size(textSize.Width, textSize.Height)),
                    TextColor = Color.White,
                    Text = "车厢门",
                    NeedDarwOutline = false,
                },


                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle =
                        new Rectangle(new Point(1, 490),
                            new Size(textSize.Width, textSize.Height)),
                    TextColor = Color.White,
                    Text = "单   元",
                    NeedDarwOutline = false,
                },

            };
        }
    }
}