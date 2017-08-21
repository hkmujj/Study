using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtil.Util;
using Mmi.Common.DateTimeInterpreter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;
using Motor.HMI.CRH5A.Appraise;
using Motor.HMI.CRH5A.Config;
using Motor.HMI.CRH5A.Config.ConfigModel;
using Motor.HMI.CRH5A.Resource;
using Motor.HMI.CRH5A.Staus;

namespace Motor.HMI.CRH5A.底层共用
{
    public class CRH5ABase : MMIBaseClass
    {

        protected DateTime CurrentTime
        {
            get
            {
                var fd = FloatList[IndexDescriptionConfig.InFloatDescriptionDictionary[InFloatKeys.InF显示日期]];
                var ft = FloatList[IndexDescriptionConfig.InFloatDescriptionDictionary[InFloatKeys.InF显示时间]];
                if (Math.Abs(fd) > float.Epsilon || Math.Abs(ft) > float.Epsilon)
                {
                    return DateTimeInterpreter.Interpreter(fd, ft);
                }
                return DateTime.Now;
            }
        }

        private static readonly IDateTimeInterpreter DateTimeInterpreter = DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime);

        public ScreenIdentification ScreenIdentification { private set; get; }

        public TitleScreen TitleScreen { get { return this.FindNeighbourObject<TitleScreen>(); } }

        public Car Car { get { return this.FindNeighbourObject<Car>(); } }

        public ButtonsScreen ButtonsScreen { get { return this.FindNeighbourObject<ButtonsScreen>(); } }

        protected IProjectIndexDescriptionConfig IndexDescriptionConfig { private set; get; }

        static CRH5ABase()
        {
            
        }

        public sealed override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;
            
            IndexDescriptionConfig =
                IConfig.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(new CommunicationDataKey(AppConfig));

            ScreenIdentification = GetProjectName().Contains("TS") ? ScreenIdentification.ScreenTs : ScreenIdentification.ScreenTd;

            ObjectManager.Instance.Regist(this);

            if (!Initalize())
            {
                LogMgr.Error(string.Format("{0} Initalize fail. ", GetType()));
            }
            return true;
        }

        protected void ReadFile(string file)
        {
            if (File.Exists(file))
            {
                var tmpStringLine = 0;
                string[] tmpFile3 = File.ReadAllLines(file, Encoding.Default);
                foreach (string str3 in tmpFile3)
                {
                    ParseLine(tmpStringLine++, str3);
                }
            }
        }

        protected virtual void ParseLine(int line, string content)
        {
            throw new NotImplementedException();
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

        public virtual void Paint(Graphics g) { }

        protected void UpdateUiObject(IEnumerable<CommunicationDataModel> dataModels)
        {
            foreach (var source in dataModels.GroupBy(o => o.DataType))
            {
                List<int> target;

                switch (source.Key)
                {
                    case CommunicationDataType.InBool:
                        target = UIObj.InBoolList;
                        break;
                    case CommunicationDataType.InFloat:
                        target = UIObj.InFloatList;
                        break;
                    case CommunicationDataType.OutBool:
                        target = UIObj.OutBoolList;
                        break;
                    case CommunicationDataType.OutFloat:
                        target = UIObj.OutFloatList;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                target.AddRange(source.Select(s => s.FindIndex(this)));
            }
        }
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (ParaADefine.InCurent == nParaA)
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
    }
}