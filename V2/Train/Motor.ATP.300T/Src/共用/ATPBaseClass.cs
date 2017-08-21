using System;
using System.Linq;
using Mmi.Common.DateTimeInterpreter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using Motor.ATP._300T.Config;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.主屏;

namespace Motor.ATP._300T.共用
{
    public class ATPBaseClass : baseClass
    {
        protected ATPButtonScreen ATPButtonScreen { private set; get; }

        protected ATPMainScreen ATPMainScreen { private set; get; }

        protected ViewConfig CurrentView { private set; get; }

        private static readonly IDateTimeInterpreter DateTimeInterpreter =
            DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime);


        internal DateTime CurrentTime
        {
            get
            {
                if (DataPackage.Config.SystemConfig.StartModel != StartModel.Edit)
                {
                    return DateTimeInterpreter.Interpreter(
                        FloatList[IndexDescriptionConfig.InFloatDescriptionDictionary[InFloatKeys.显示日期]],
                        FloatList[IndexDescriptionConfig.InFloatDescriptionDictionary[InFloatKeys.显示时间]]);
                }
                return DateTime.Now;
            }
        }

        public sealed override bool init(ref int nErrorObjectIndex)
        {
            ATPButtonScreen = DataPackage.ObjectService.GetObject<ATPButtonScreen>(ProjectName).FirstOrDefault();

            ATPMainScreen = DataPackage.ObjectService.GetObject<ATPMainScreen>(ProjectName).FirstOrDefault();

            var rlt = Initalize();

            if (!rlt)
            {
                throw new ArgumentException(string.Format("{0}初始化失败", GetType()));
            }

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            CurrentView = (ViewConfig)nParaB;
        }

        public virtual bool Initalize()
        {
            return true;
        }
    }
}
