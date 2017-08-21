using System;
using System.Drawing;
using ATP200C.Resource;
using Mmi.Common.DateTimeInterpreter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;

namespace ATP200C.MainView
{
    public class ATPBaseClass: baseClass
    {
        /// <summary>
        /// 图元初始化过程执行过程 (只在初始化过程执行一次)
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public sealed override bool init(ref int nErrorObjectIndex)
        {
            IndexDescription = DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                new CommunicationDataKey(AppConfig));
            nErrorObjectIndex = -1;

            var rlt = Initalize();

            if (!rlt)
            {
                // TODO Initalize fail
            }
            return true;
        }

        protected IProjectIndexDescriptionConfig IndexDescription { get; private set; }

        private static readonly IDateTimeInterpreter DateTimeInterpreter =
            DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime);

        internal DateTime CurrentTime
        {
            get
            {
                var fd = FloatList[IndexDescription.InFloatDescriptionDictionary[InFloatKeys.显示日期]];
                var ft = FloatList[IndexDescription.InFloatDescriptionDictionary[InFloatKeys.显示时间]];
                if (Math.Abs(fd) > float.Epsilon || Math.Abs(ft) > float.Epsilon)
                {
                    return DateTimeInterpreter.Interpreter(fd, ft);
                }
                return DateTime.Now;
            }
        }


        public virtual bool Initalize()
        {
            return true;
        }
    }
}
