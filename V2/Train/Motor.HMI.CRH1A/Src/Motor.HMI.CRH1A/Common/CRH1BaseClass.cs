using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using Mmi.Common.DateTimeInterpreter;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;
using Motor.HMI.CRH1A.Config.ConfigModel;
using Motor.HMI.CRH1A.Resource;

namespace Motor.HMI.CRH1A.Common
{
    /// <summary>
    /// CRH1 型车的基类
    /// </summary>
    public class CRH1BaseClass : baseClass, IDisposable
    {
        /// <summary>
        /// 能否响应用户
        /// </summary>
        protected static bool CanResponseUser { set; get; }

        protected IProjectIndexDescriptionConfig IndexDescription
        {
            get
            {
                return
                    DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                        new CommunicationDataKey(AppConfig));
            }
        }

        protected DateTime CurrenTime
        {
            get
            {
                return
                    DateTimeInterpreter.Interpreter(
                        FloatList[IndexDescription.InFloatDescriptionDictionary[InFloatKeys.InF显示日期]],
                        FloatList[IndexDescription.InFloatDescriptionDictionary[InFloatKeys.InF显示时间]]);
            }
        }

        private static readonly IDateTimeInterpreter DateTimeInterpreter =
            DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime);

        protected List<PasswordConfig> PasswordConfigs
        {
            get { return GlobalInfo.Instance.CRH1ADetailConfig.PasswordConfigs; }
        }

        ///// 获取当前工程的名字
        ///// </summary>
        //public string ProjectName { get; private set; }

        public void OnPost(CmdType type, int aPara, int bPara, float cPara)
        {
            switch (type)
            {

                case CmdType.SetBoolValue:
                    if (aPara < OutBoolList.Keys.Min())
                    {
                        append_postCmd(type, aPara + OutBoolList.Keys.Min(), bPara, cPara);
                    }
                    else
                    {
                        append_postCmd(type, aPara, bPara, cPara);
                    }
                    break;
                case CmdType.SetFloatValue:
                    if (aPara < OutFloatList.Keys.Min())
                    {
                        append_postCmd(type, aPara + OutFloatList.Keys.Min(), bPara, cPara);
                    }
                    else
                    {
                        append_postCmd(type, aPara, bPara, cPara);
                    }
                    break;
                default:
                    append_postCmd(type, aPara, bPara, cPara);
                    break;
            }
        }

        public override sealed bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;

            CanResponseUser = true;

            ProjectName = AppConfig.AppName;

            return Initialize();
        }

        public virtual bool Initialize()
        {
            return true;
        }

        public override sealed bool mouseDown(Point point)
        {
            if (CanResponseUser)
            {
                return OnMouseDown(point);
            }
            return false;
        }

        protected virtual bool OnMouseDown(Point point)
        {
            return false;
        }

        public override sealed bool mouseUp(Point point)
        {
            if (CanResponseUser)
            {
                return OnMouseUp(point);
            }

            return false;
        }

        protected virtual bool OnMouseUp(Point point)
        {
            return false;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (ParaADefine.InCurrent == nParaA)
            {
                NavigateInCurrent((ViewConfig)nParaB);
            }
            else if (ParaADefine.SwitchFromOhter == nParaA)
            {
                NavigateFrom((ViewConfig)nParaC);
                GlobalViewNavigate.Instance.OnNavigeteFrom((ViewConfig)nParaC);
                NavigateTo((ViewConfig)nParaB);
                GlobalViewNavigate.Instance.OnNavigeteTo((ViewConfig)nParaB);
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

        public virtual void Dispose()
        {

        }
    }
}
