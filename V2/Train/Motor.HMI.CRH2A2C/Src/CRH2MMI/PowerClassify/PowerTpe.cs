using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Util.Extension;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.PowerClassify
{
    /// <summary>
    /// 供电分类
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class PowerTpe : CRH2BaseClass
    {
        // ReSharper disable once InconsistentNaming
        public PowerTypeResource PowerTypeResource { get; private set; }

        private PowerClassifyBase m_ClassifyBase;

        // ReSharper disable once InconsistentNaming
        internal TrainView m_TrainView;

        private Matrix m_TrasforMatrix;
        private Matrix m_RevmTrasforMatrix;

        public override void paint(Graphics g)
        {
            var old = g.Transform;
            var trans = g.Transform.Clone();
            trans.Multiply(m_TrasforMatrix);
            g.Transform = trans;

            m_ClassifyBase.OnPaint(g);
  
            g.Transform = old;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_TrainView.Location = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig.TrainViewLocation.Rectangle.Location;
                m_TrainView.IsUnitNameVisible = true;
                m_ClassifyBase.ReselectConditionBtn(null);
            }
        }

        public override bool Init()
        {
            m_TrasforMatrix = new Matrix();
            m_TrasforMatrix.Scale(1, 0.9f);
            m_TrasforMatrix.Translate(0, 20);
            m_RevmTrasforMatrix = m_TrasforMatrix.Clone();
            m_RevmTrasforMatrix.Invert();

            m_TrainView = TrainView.Instance;

            InitUIObj();

            PowerTypeResource = new PowerTypeResource(this);

            switch (GlobalInfo.CurrentCRH2Config.Type)
            {
                case CRH2Type.CRH2A:
                    m_ClassifyBase = new PowerClassify2A(this);
                    break;
                case CRH2Type.CRH2B:
                    m_ClassifyBase = new PowerClassify2B(this);
                    break;
                case CRH2Type.CRH2C:
                    m_ClassifyBase= new PowerClassify2C(this);
                    break;
                case CRH2Type.CRH380A:
                case CRH2Type.CRH380AUnion:
                    m_ClassifyBase = new PowerClassify380A(this);
                    break;
                case CRH2Type.CRH380AL:
                    m_ClassifyBase = new PowerClassify380AL(this);
                    break;
                case CRH2Type.CRH380AReconnection:
                    m_ClassifyBase = new PowerClassifyCRH380AReconnection(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            m_ClassifyBase.Init();

            return true;
        }

        private void InitUIObj()
        {
            var config = GlobalInfo.CurrentCRH2Config.PowerClassifyConfig;

            var models = config.MtrInBools.Cast<CRH2CommunicationPortModel>().ToList();
            models.AddRange(config.ACK1InBools);
            models.AddRange(config.ACK2InBools);
            models.AddRange(config.BKK2InBools);
            models.AddRange(config.BKKInBools);
            models.AddRange(config.BKKOutBools);
            models.AddRange(config.APUInBools);

            InitUIObj(models);
        }

        public override string GetInfo()
        {
            return "供电分类";
        }

        protected override bool OnMouseDown(Point nPoint)
        {
            var point = m_RevmTrasforMatrix.TransformPoint(nPoint);
            return m_ClassifyBase.OnMouseDown(point);
        }

        protected override bool OnMouseUp(Point point)
        {
            var nPoint = m_RevmTrasforMatrix.TransformPoint(point);
            return m_ClassifyBase.OnMouseUp(nPoint);
        }
    }
}