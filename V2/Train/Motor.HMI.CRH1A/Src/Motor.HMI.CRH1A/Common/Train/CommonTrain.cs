using System;
using System.Drawing;
using System.Linq;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using MMI.Facility.Interface.Attribute;

namespace Motor.HMI.CRH1A.Common.Train
{
    /// <summary>
    /// 最左上角的车, 
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class CommonTrain : CRH1BaseClass
    {
        private readonly Rectangle m_Recposition = new Rectangle(20, 100, 100, 100);

        public bool CanSelect { set; get; }

        /// <summary>
        /// 选中的变化事件
        /// </summary>
        public EventHandler SelectedChanged { set { m_TrainvView.SelectedChanged = value; } get { return m_TrainvView.SelectedChanged; } }

        /// <summary>
        /// 故障发生变化 
        /// </summary>
        public EventHandler FaultChanged { set { m_TrainvView.FaultChanged = value; } get { return m_TrainvView.FaultChanged; }}

        /// <summary>
        /// 单元实例
        /// </summary>
        public static CommonTrain Instance { private set; get; }

        public TrainvView TrainvView { get { return m_TrainvView; }}
        private TrainvView m_TrainvView;

        public CommonTrain()
        {
            CanSelect = false;

           
        }

        private void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            m_TrainvView.Reverse();
        }

        public override string GetInfo()
        {
            return "列车配置";
        }

        protected override void NavigateTo(ViewConfig to)
        {
            {
                Reset();
            }
        }

        /// <summary>
        /// 复位
        /// </summary>
        public void Reset()
        {
            CanSelect = false;
            SelectedChanged = null;
            FaultChanged = null;
            m_TrainvView.DeselectAll();
        }


        public override bool Initialize()
        {
            GlobalEvent.Instance.ReversalChanged += OnReversalChanged;

            //3
            InitData();

            if (Instance == null)
            {
                Instance = this;
            }

            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

        public void GetValue()
        {
            var inbool = UIObj.InBoolList.Select(s => BoolList[s]).ToArray();
            CommonTrainInBoolRes.Instance.Init(inbool);

        }

        private void InitData()
        {
            m_TrainvView = new TrainvView()
            {
                OutLineRectangle = m_Recposition,
            };
        }


        private void DrawOn(Graphics g)
        {
            m_TrainvView.OnPaint(g);
        }


        protected override bool OnMouseDown(Point point)
        {
            return OnButtonDown(point);
        }

        private bool OnButtonDown(Point point)
        {
            if (!CanSelect)
            {
                return false;
            }

            return m_TrainvView.OnMouseDown(point);
        }

    }


}
