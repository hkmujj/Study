using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Model;
using CommonUtil.Util;using CommonUtil.Util.Extension;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Global;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;


namespace CRH2MMI.Common.View.Train
{
    /// <summary>
    /// 公共车
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class TrainView : CRH2BaseClass
    {
        private List<Car> m_Cars;

        /// <summary>
        /// 
        /// </summary>
        private bool m_CarWidthChanged;

        private bool m_NeedDrawCarName;

        /// <summary>
        /// 单例
        /// </summary>
        public static TrainView Instance { private set; get; }

        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<int, TrainView> m_TrainViewDic = new Dictionary<int, TrainView>();

        /// <summary>
        /// 是否需要绘制车名
        /// </summary>
        public bool NeedDrawCarName
        {
            set
            {
                m_NeedDrawCarName = value;
                m_Cars.ForEach(e => e.NeedDrawCarName = value);
            }
            get { return m_NeedDrawCarName; }
        }

        /// <summary>
        /// 所有的CAR
        /// </summary>
        public ReadOnlyCollection<Car> Cars{ get { return m_Cars.AsReadOnly(); }}

        /// <summary>
        /// 车厢状态是否自动变化 
        /// </summary>
        public bool IsCarStateAutoChangable
        {
            set
            {
                m_IsCarStateAutoChangable = value;
                if (!value)
                {
                    SetCarStateAlwaryNomarl();
                }
                else
                {
                    ResetCarState();
                }
            }
            get { return m_IsCarStateAutoChangable; }
        }

        /// <summary>
        /// 单元状态是否可见
        /// </summary>
        public bool IsUnitStateVisible
        {
            set
            {
                if (m_TrainUnit==null)
                {
                    return;
                }
                m_TrainUnit.IsUnitStateVisible = value;
            }
            get
            {
                if (m_TrainUnit==null)
                {
                    return false;
                }
                return m_TrainUnit.IsUnitStateVisible;
            }
        }

        /// <summary>
        /// 单元名字是否可见
        /// </summary>
        public bool IsUnitNameVisible
        {
            set
            {
                if (m_TrainUnit == null)
                {
                    return;
                }
                m_TrainUnit.IsUnitNameVisible = value;
            }
            get
            {
                if (m_TrainUnit == null)
                {
                    return false;
                }
                return m_TrainUnit.IsUnitStateVisible;
            }
        }

        public bool IsDriverRoomVisible
        {
            set
            {
                m_IsDriverRoomVisible = value;
                var headCars = m_Cars.Where(w => w is HeadCar).Cast<HeadCar>().OrderBy(o => o.CarNo);
                headCars.First().IsDriverRoomVisible = value;
                headCars.Last().IsDriverRoomVisible = value;
            }
            get { return m_IsDriverRoomVisible; }
        }

        // ReSharper disable once InconsistentNaming
        private EventHandler LocationChanged;
        private Point m_Location;

        /// <summary>
        /// 轮子和单元状态的间隔
        /// </summary>
        private const int WheelUnitStateInterval = 10;

        private TrainUnit m_TrainUnit;
        private bool m_IsDriverRoomVisible;
        private bool m_IsCarStateAutoChangable;


        public Point Location
        {
            set
            {
                m_Location = value;
                HandleUtil.OnHandle(LocationChanged, null, null);
            }
            get { return m_Location; }
        }

        /// <summary>
        /// 获取单元的位置
        /// </summary>
        /// <returns></returns>
        public List<Rectangle> GetTrainUnitRectangles()
        {
            return m_TrainUnit.TrainUnitRect.Select(s => new Rectangle(s.Location, s.Size)).ToList();
        }

        private void OnLocationChanged(object sender, EventArgs eventArgs)
        {
            var mat = new Matrix();
            var fcar = m_Cars.OrderBy(o => o.Location.X).First();

            mat.Translate(Location.X - fcar.Location.X, Location.Y - fcar.Location.Y);

            foreach (var car in m_Cars)
            {
                car.Location = mat.TransformPoint(car.Location);
            }

            RefreshTrainUnitLocation(fcar);
        }


        public override void paint(Graphics g)
        {
            m_Cars.ForEach(e => e.OnPaint(g));

            m_TrainUnit.OnDraw(g);
        }

        public override bool Init()
        {

            if (!m_TrainViewDic.ContainsKey(UIObj.MainIndex))
            {
                m_TrainViewDic.Add(UIObj.MainIndex, this);
            }

            Instance = this;

            InitUIObj();

            InitalizeComponet();

            return true;
        }

        public void InitalizeComponet(bool needUpdateRes=true)
        {
            if (needUpdateRes)
            { // ReSharper disable once UnusedVariable
                var res = new TrainResource(this);
            }

            m_CarWidthChanged = false;

            LocationChanged = OnLocationChanged;

            InitCar();

            InitAcceptEleArc();

            InitTrainUnit();

            GlobalEvent.Instance.ReversalChanged += OnReversalChanged;
        }

        private void InitUIObj()
        {
            var tvConfig = GlobalInfo.CurrentCRH2Config.TrainConfig;
            var models = tvConfig.CarConfigs.Cast<CRH2CommunicationPortModel>().ToList();
            models.AddRange(tvConfig.AcceptEleCarConfigs.Cast<CRH2CommunicationPortModel>());
            models.AddRange(tvConfig.TrainUnits.Cast<CRH2CommunicationPortModel>());
            InitUIObj(models);
        }

        private void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            m_TrainUnit.Reversal();
            for (int i = 0; i < GlobalParam.Instance.CarCount / 2; i++)
            {
                var resIdx = GlobalParam.Instance.CarCount - 1 - i;
                var rect = m_Cars[i].OutLineRectangle;
                m_Cars[i].Reverse();
                m_Cars[resIdx].Reverse();
                m_Cars[i].OutLineRectangle = m_Cars[resIdx].OutLineRectangle;
                m_Cars[resIdx].OutLineRectangle = rect;
            }
        }

        /// <summary>
        /// 司机室的车号
        /// </summary>
        /// <returns></returns>
        public int GetDriverCarNo()
        {
            var driver = m_Cars.Find(f => (f is HeadCar) && (((HeadCar) f).IsDriverRoom));
            if (driver !=null)
            {
                return driver.CarNo;
            }
            return 0;
        }

        public void SetCarState(List<CarStateModel> states)
        {
            foreach (var model in states)
            {
                var car = m_Cars.Find(f => f.CarNo == model.CarNo);
                if (car != null)
                {
                    car.CarStateProxy = new CarStateProxy() { UserSetState = model.State };
                }
                else
                {
                    LogMgr.Warn(string.Format("Can not set car state, CarNo({0}) can find in train.", model.CarNo));
                }
            }
        }

        public void SetCarStateAlwaryNomarl()
        {
            foreach (var car in m_Cars)
            {
                car.CarStateProxy = new CarStateProxy() { UserSetState = CarState.Normal };
            }
        }

        public void ResetCarState()
        {
            m_Cars.ForEach(e =>
            {
                e.CarState = CarState.Normal;
                e.CarStateProxy = null;
            });
        }

        /// <summary>
        /// 司机室的方向 
        /// </summary>
        /// <returns></returns>
        public Direction GetDriverDirection()
        {
            var headCars = m_Cars.FindAll(f => f is HeadCar).Cast<HeadCar>().ToList();
            headCars.ForEach(e => e.RefreshDirvierRoomState());
            var driver = headCars.Find(f => f.IsDriverRoom);
            if (driver != null)
            {
                return driver.Direction;
            }
            return Direction.Left; 
        }

        private void InitTrainUnit()
        {
            //m_TrainUnit = new TrainUnit(DefalutCarWidth*4, 2);
            m_TrainUnit =new TrainUnit( GlobalInfo.CurrentCRH2Config.TrainConfig.TrainUnits, m_Cars.First());
            RefreshTrainUnitLocation(m_Cars.First());
        }

        private void RefreshTrainUnitLocation(Car fCar)
        {
            m_TrainUnit.Location = new Point(fCar.Location.X - 5, fCar.OutLineRectangle.Bottom + WheelUnitStateInterval);
        }

        private void InitAcceptEleArc()
        {
            //var idxs = GlobalInfo.CurrentCRH2Config.TrainConfig.AcceptEleCarConfigs.Select(s => s.CarNo)
            //                     .ToList();

            foreach (var gp in GlobalInfo.CurrentCRH2Config.TrainConfig.AcceptEleCarConfigs.GroupBy(g => g.CarNo))
            {
                var car = m_Cars[gp.Key];
                car.AcceptEleArcs = gp.Select(s => new AcceptEleArc()
                {
                    AngleBracketLocation = s.Location, Direction = s.Direction,
                    CarNo = car.CarNo
                }).ToList();
            }


            //var car = m_Cars[idxs[0]];
            //car.AcceptEleArcs = new AcceptEleArc() {  Direction = Direction.Left };

            //car = m_Cars[idxs[1]];
            //car.AcceptEleArcs = new AcceptEleArc() {  Direction = Direction.Right };
        }

        private void InitCar()
        {
            var carConfigs = TrainResource.Instance.TrainConfig.CarConfigs;

            m_Cars = carConfigs.Select(s => CarFactory.CreateCar(s, new Point(Location.X + s.CarNo * Car.DefaultSize.Width, Location.Y))).ToList();

        }

        public override string GetInfo()
        {
            return "车";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                Instance = m_TrainViewDic[UIObj.MainIndex];
                Reset();
            }

            if (nParaA == ParaADefine.InCurent)
            {
                if (nParaB == (int)ViewConfig.DriveState)
                {
                    if (!IsCarStateAutoChangable)
                    {
                        IsCarStateAutoChangable = true;
                    }
                }
                else
                {
                    if (IsCarStateAutoChangable)
                    {
                        IsCarStateAutoChangable = false;
                    }
                }
            }
        }

        public void Reset()
        {
            NeedDrawCarName = false;
            IsUnitStateVisible = false;
            IsUnitNameVisible = false;
            IsDriverRoomVisible = false;
            
            SetCarVisible();
            ResetCarState();
            ResetHeadCarWidth();

            IsCarStateAutoChangable = false;
        }

        private void SetCarVisible()
        {
            m_Cars.ForEach(e => e.Visible = true);
        }

        /// <summary>
        /// 设置车头宽度
        /// </summary>
        /// <param name="widthModels"></param>
        public void SetHeadCarWidth(List<CarWidthModel> widthModels)
        {
            m_CarWidthChanged = true;

            var current = m_Cars.Select(s => new CarWidthModel() {CarNo = s.CarNo, Width = s.Size.Width}).ToList();

            widthModels.ForEach(e =>
                                {
                                    var car = current.Find(f => f.CarNo == e.CarNo);
                                    if (car != null)
                                    {
                                        car.Width = e.Width;
                                    }
                                });

            var location = m_Cars[0].Location;
            if (GlobalParam.Instance.IsReversalTrain)
            {
                current.Reverse();
                location = m_Cars.Last().Location;
            }


            foreach (var widthModel in current)
            {
                var car = m_Cars.Find(f => f.CarNo == widthModel.CarNo);
                // 高度被忽略 
                car.OutLineRectangle = new Rectangle(location, new Size(widthModel.Width, 0));

                location = new Point(location.X + widthModel.Width, location.Y);
            }

        }

        public void ResetHeadCarWidth()
        {
            if (m_CarWidthChanged)
            {
                SetHeadCarWidth(m_Cars.Select(s => new CarWidthModel(){ CarNo = s.CarNo, Width = Car.DefaultSize.Width}).ToList());
                m_CarWidthChanged = false;
            }
        }
    }
}
