using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Appraise;
using CRH2MMI.BreakLocked;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Global;
using CRH2MMI.Config.ConfigModel;
using CRH2MMI.Resource;
using log4net;
using Mmi.Common.DateTimeInterpreter;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.Common
{
    internal class CRH2BaseClass : MMIBaseClass, INavigateAware
    {
        public ScreenId ScreenId { private set; get; }


        internal DateTime CurrentTime
        {
            get
            {
                if (DataPackage.Config.SystemConfig.StartModel == StartModel.Edit)
                {
                    return DateTime.Now;
                }

                return
                    m_DateTimeInterpreter.Interpreter(
                        FloatList[IndexDescriptionConfig.InFloatDescriptionDictionary[InFloatKeys.显示日期]],
                        FloatList[IndexDescriptionConfig.InFloatDescriptionDictionary[InFloatKeys.显示时间]]);
            }
        }

        private IDateTimeInterpreter m_DateTimeInterpreter;
        private IScreensavers m_Screensavers;

        private IScreensavers Screensavers
        {
            get
            {
                return m_Screensavers ??
                       (m_Screensavers = DataPackage.ObjectService.GetObject<Screensavers>(ProjectName).FirstOrDefault());
            }
        }

        /// <summary>
        /// 通过 xls 的名字列 初始化 UIObj
        /// </summary>
        protected void InitUIObj(IEnumerable<CRH2CommunicationPortModel> models)
        {
            InitInBoolIndexList(models.SelectMany(s => s.InBoolColoumNames));
            InitInFloatIndexList(models.SelectMany(s => s.InFloatColoumNames));
            InitOutBoolIndexList(models.SelectMany(s => s.OutBoolColoumNames));
            InitOutFloatIndexList(models.SelectMany(s => s.OutFloatColoumNames));

            LogMgr.Debug(string.Format("Class {0}'s InBoolList = {1}.", GetType(), string.Join(",", UIObj.InBoolList.Select(s => s.ToString(CultureInfo.InvariantCulture)).ToArray())));
            LogMgr.Debug(string.Format("Class {0}'s InFloatList = {1}.", GetType(), string.Join(",", UIObj.InFloatList.Select(s => s.ToString(CultureInfo.InvariantCulture)).ToArray())));
            LogMgr.Debug(string.Format("Class {0}'s OutBoolList = {1}.", GetType(), string.Join(",", UIObj.OutBoolList.Select(s => s.ToString(CultureInfo.InvariantCulture)).ToArray())));
            LogMgr.Debug(string.Format("Class {0}'s OutFloatList = {1}.", GetType(), string.Join(",", UIObj.OutFloatList.Select(s => s.ToString(CultureInfo.InvariantCulture)).ToArray())));
        }

        /// <summary>
        /// 通过 xls 的名字列 初始化 UIObj.InBoolList
        /// </summary>
        /// <param name="names"></param>
        protected void InitInBoolIndexList(IEnumerable<string> names)
        {
            UIObj.InBoolList.Clear();
            UIObj.InBoolList.AddRange(names.Select(GetInBoolIndex));
        }

        /// <summary>
        /// 通过 xls 的名字列 初始化 UIObj.InFloatList
        /// </summary>
        /// <param name="names"></param>
        protected void InitInFloatIndexList(IEnumerable<string> names)
        {
            UIObj.InFloatList.Clear();
            UIObj.InFloatList.AddRange(names.Select(GetInFloatIndex));
        }

        /// <summary>
        /// 通过 xls 的名字列 初始化 UIObj.OutBoolList
        /// </summary>
        /// <param name="names"></param>
        protected void InitOutBoolIndexList(IEnumerable<string> names)
        {
            UIObj.OutBoolList.Clear();
            UIObj.OutBoolList.AddRange(names.Select(GetOutBoolIndex));
        }

        /// <summary>
        /// 通过 xls 的名字列 初始化 UIObj.OutFloatList
        /// </summary>
        /// <param name="names"></param>
        protected void InitOutFloatIndexList(IEnumerable<string> names)
        {
            UIObj.OutFloatList.Clear();
            UIObj.OutFloatList.AddRange(names.Select(GetOutFloatIndex));
        }

        public sealed override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;
            m_DateTimeInterpreter = DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime);
            ObjectManager.Instance.RegistObject(this);
            ScreenId = ProjectName == "CRH2" ? ScreenId.CRH2 : ScreenId.CRH2_2;
            var initRlt = Init();
            using (NDC.Push("MMI baseClass Init"))
            {
                LogMgr.Debug(!initRlt
                    ? string.Format("Init class {0} fail.", GetType())
                    : string.Format("Init class {0} Sucess.", GetType()));
            }
            return initRlt;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (Screensavers != null)
            {
                Screensavers.CheckToScreensavers();
            }

            switch (nParaA)
            {
                case ParaADefine.InCurent:
                    NavigateInCurrent((ViewConfig) nParaB);
                    break;
                case ParaADefine.SwitchFromOhter:
                    NavigateFrom((ViewConfig) nParaC);
                    GlobalViewNavigate.Instance.OnNavigeteFrom((ViewConfig) nParaC);
                    NavigateTo((ViewConfig) nParaB);
                    GlobalViewNavigate.Instance.OnNavigeteTo((ViewConfig) nParaB);
                    break;
            }

            base.setRunValue(nParaA, nParaB, nParaC);
        }

        public sealed override bool mouseDown(Point point)
        {
            if (Screensavers != null)
            {
                Screensavers.FeedWatcher();
            }

            return OnMouseDown(point);
        }

        protected virtual bool OnMouseDown(Point point)
        {
            return true;
        }

        public sealed override bool mouseUp(Point point)
        {
            return OnMouseUp(point);
        }

        protected virtual bool OnMouseUp(Point point)
        {
            return true;
        }

        public virtual void NavigateInCurrent(ViewConfig current)
        {

        }

        public virtual void NavigateTo(ViewConfig toThis)
        {

        }

        public virtual void NavigateFrom(ViewConfig fromOhter)
        {

        }

        public virtual bool Init()
        {
            return true;
        }
    }
}
