using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Model;
using CommonUtil.Util;using CommonUtil.Util.Extension;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;


namespace CRH2MMI.DoorInfo
{
    /// <summary>
    /// 车门信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Door : CRH2BaseClass
    {
        private List<DoorUnit> m_PressReleaseDoors;

        private List<DoorUnit> m_OpenColseDoors;

        private float m_OpenColseDoorMidX;

        private float m_PressReleaseDoorMidX;

        private DoorResource m_DoorResource;

        private Arrows m_DirectionArrows;

        private TrainView m_TrainView;

        private DoorInfoBack m_DoorInfoBackView;

        public override void paint(Graphics g)
        {
            OnDraw(g);
        }

        public override bool Init()
        {
            m_DoorResource = new DoorResource(this);

            m_TrainView = TrainView.Instance;

            m_DoorInfoBackView = new DoorInfoBack()
                                 {
                                     Location = new Point(0, 145)
                                 };

            InitUIObj(GlobalInfo.CurrentCRH2Config.DoorConfig.DoorInBoolModels.Cast<CRH2CommunicationPortModel>());

            InitDoors();

            m_DirectionArrows = new Arrows()
            {
                OutLineRectangle = new Rectangle(new Point(180, 335), new Size(20, 30)),
                Direction = Direction.Left,
                RefreshAction = o => RefreshDriverRoom(),
            };

            GlobalEvent.Instance.ReversalChanged += OnReversalChanged;

            return true;
        }

        [Obsolete]
        private void OnReversalChanged(object sender, EventArgs eventArgs)
        {

            var mat = MatrixHelper.CreateTurnMatrix(m_OpenColseDoorMidX, TurnOrientation.Horizontal);
            m_OpenColseDoors.ForEach(e =>
            {
                var p = new[]
                {
                    e.Location
                };

                mat.TransformPoints(p);
                e.Location = p[0];
            });

            mat.Reset();
            mat = MatrixHelper.CreateTurnMatrix(m_PressReleaseDoorMidX, TurnOrientation.Horizontal);
            m_PressReleaseDoors.ForEach(e =>
            {
                var p = new[]
                {
                    e.Location
                };

                mat.TransformPoints(p);
                e.Location = p[0];
            });

            RefreshDriverRoom();

        }

        private void RefreshDriverRoom()
        {
            var tvDir = m_TrainView.GetDriverDirection();
            if (m_DirectionArrows.Direction != tvDir)
            {
                var mat = MatrixHelper.CreateTurnMatrix(m_OpenColseDoorMidX, TurnOrientation.Horizontal);
                var dap = new[] { m_DirectionArrows.Location };
                mat.TransformPoints(dap);
                m_DirectionArrows.Direction = tvDir;
                m_DirectionArrows.Location = dap[0];
            }
        }

        private void InitDoors()
        {

            InitPressRelaseDoors();

            InitOpenCloseDoors();

        }

        private void InitOpenCloseDoors()
        {
            var size = new Size(30, 40);
            var offset = (DoorInfoBack.DefaultBkRegionSizeUnit.Width - size.Width) / 2 + 1;

            m_OpenColseDoors =
                GlobalInfo.CurrentCRH2Config.DoorConfig.DoorInBoolModels.FindAll(
                    f => f.Type == DoorOperType.OpenClose).Select(s => new OpenColseDoor(s)
                    {
                        Resource = m_DoorResource,
                        OutLineRectangle = new Rectangle(m_DoorInfoBackView.GetLocationOf(s).Translate(offset, 0), size)
                    }).Cast<DoorUnit>().ToList();

            m_OpenColseDoorMidX =
                (float) (m_OpenColseDoors.Max(m => m.Location.X) + m_OpenColseDoors.Min(m => m.Location.X))/2;

        }

        private void InitPressRelaseDoors()
        {
            var size = new Size(42, 24);
            m_PressReleaseDoors =
                GlobalInfo.CurrentCRH2Config.DoorConfig.DoorInBoolModels.FindAll(
                    f => f.Type == DoorOperType.PressRelase).Select(s => new PressReleaseDoor(s)
                    {
                        Resource = m_DoorResource,
                        OutLineRectangle = new Rectangle(m_DoorInfoBackView.GetLocationOf(s), size)
                    }).Cast<DoorUnit>().ToList();

            m_PressReleaseDoorMidX =
                (float)(m_PressReleaseDoors.Max(m => m.Location.X) + m_PressReleaseDoors.Min(m => m.Location.X)) / 2;
        }

        public override string GetInfo()
        {
            return "车门信息";
        }

        public void OnDraw(Graphics g)
        {
            m_DoorInfoBackView.RefreshDriverRoom(m_TrainView.GetDriverDirection());

            m_DoorInfoBackView.OnDraw(g);

            m_OpenColseDoors.ForEach(e => e.OnPaint(g));

            m_PressReleaseDoors.ForEach(e => e.OnPaint(g));
        }
    }
}