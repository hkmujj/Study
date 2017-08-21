using System;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using Mmi.Common.DateTimeInterpreter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Appraise;
using Motor.HMI.CRH3C380B.Base.停放制动;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Config;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Common
{
    public class CRH3C380BBase : MMIBaseClass
    {
        protected bool IsLeftScreen { get { return DMITitle.CurrentMMIName == MMIType.左侧MMI屏; } }

        protected bool IsRightScreen { get { return !IsLeftScreen; } }

        private static bool m_ProjectCorrected;

        internal DateTime CurrentTime
        {
            get
            {
                if (DataPackage.Config.SystemConfig.StartModel != StartModel.Edit)
                {
                    return m_DateTimeInterpreter.Interpreter(
                   FloatList[IndexDescriptionConfig.InFloatDescriptionDictionary[InFloatKeys.Inf显示日期]],
                   FloatList[IndexDescriptionConfig.InFloatDescriptionDictionary[InFloatKeys.Inf显示时间]]);
                }
                return DateTime.Now;
            }
        }
        private IDateTimeInterpreter m_DateTimeInterpreter;

        public sealed override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;

            m_DateTimeInterpreter = DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime);

            if (!m_ProjectCorrected)
            {
                GlobalParam.Instance.ProjectConfig.CorrectionProjectType(this);
                m_ProjectCorrected = true;
            }

            ObjectManager.Instance.Regist(this);

            LoadAttachedFiles();

            if (!Initalize())
            {
                AppLog.Error(string.Format("{0} Initalize fail. ", GetType()));
            }
            return true;
        }

        private void LoadAttachedFiles()
        {
            var appConfig = IConfig.AppConfigs.FirstOrDefault(f => f.AppName == ProjectName);
            if (appConfig == null)
            {
                AppLog.Error(string.Format("Can not found app config where AppName is {0}", ProjectName));
            }
        }

        public virtual bool Initalize()
        {
            return true;
        }

        public sealed override void paint(Graphics g)
        {
            Paint(g);
        }

        public sealed override bool mouseDown(Point point)
        {
            return OnMouseDown(point);
        }

        public sealed override bool mouseUp(Point point)
        {
            return OnMouseUp(point);
        }

        public virtual bool OnMouseDown(Point point)
        {
            return false;
        }

        public virtual bool OnMouseUp(Point point)
        {
            return false;
        }

        public virtual void Paint(Graphics g)
        {
        }

        public DMIButton DMIButton
        {
            get { return this.FindNeighbourObject<DMIButton>(); }
        }

        public DMITitle DMITitle
        {
            get { return this.FindNeighbourObject<DMITitle>(); }
        }

        public DMIParkingBrakes DmiParkingBrakes
        {
            get { return this.FindNeighbourObject<DMIParkingBrakes>(); }
        }
        /// <summary>
        /// 清空信息, 开课前, 结束课后.
        /// </summary>
        public virtual void Clear()
        {

        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            switch (nParaA)
            {
                case ParaADefine.InCurent:
                    NavigateInCurrent((ViewConfig)nParaB);
                    break;
                case ParaADefine.SwitchFromOhter:
                    NavigateFrom((ViewConfig)nParaC);
                    GlobalViewNavigate.Instance.OnNavigeteFrom((ViewConfig)nParaC);
                    NavigateTo((ViewConfig)nParaB);
                    GlobalViewNavigate.Instance.OnNavigeteTo((ViewConfig)nParaB);
                    break;
            }
        }

        protected virtual void NavigateInCurrent(ViewConfig current)
        {

        }

        protected virtual void NavigateTo(ViewConfig toThis)
        {

        }

        protected virtual void NavigateFrom(ViewConfig fromOhter)
        {

        }

        protected bool GetInBoolAt(string name)
        {
            return BoolList[IndexDescriptionConfig.InBoolDescriptionDictionary[name]];
        }
        protected float GetInFloatAt(string name)
        {
            return FloatList[IndexDescriptionConfig.InFloatDescriptionDictionary[name]];
        }

        protected void SetOutBool(string name, int value)
        {
            append_postCmd(CmdType.SetBoolValue,
                IndexDescriptionConfig.OutBoolDescriptionDictionary[name], value, 0);
        }
    }
}