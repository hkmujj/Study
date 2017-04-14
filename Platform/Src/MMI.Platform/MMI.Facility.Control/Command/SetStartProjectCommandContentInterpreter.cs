using System.IO;
using System.Text;
using CommonUtil.Util;
using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.DataType.Course;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Course;

namespace MMI.Facility.Control.Command
{
    public class XmlDataCommandContentInterpreter : ICommandContentInterpreter<ICourseStartParameter>
    {
        public ICourseStartParameter Interpreter(MMI.YDCommunicationWrapper.Command data)
        {
            var content = Encoding.Default.GetString(data.cParam, 0, data.GetParamLen());
            SysLog.Debug("Start couser row data = {0}", content);

            return DataSerialization.DeserializeFromStream<CourseStartParameter>(new MemoryStream(data.cParam, 0, data.GetParamLen()));
        }

        object ICommandContentInterpreter.Interpreter(MMI.YDCommunicationWrapper.Command data)
        {
            return Interpreter(data);
        }
    }

    public class SetStartProjectCommandContentInterpreter : ICommandContentInterpreter<string>
    {
        public string Interpreter(MMI.YDCommunicationWrapper.Command data)
        {
            return Encoding.Default.GetString(data.cParam, 0, data.GetParamLen());
        }

        object ICommandContentInterpreter.Interpreter(MMI.YDCommunicationWrapper.Command data)
        {
            return Interpreter(data);
        }
    }

    public class NoneStartProjectCommandContentInterpreter : ICommandContentInterpreter<string>
    {
        public string Interpreter(YDCommunicationWrapper.Command data)
        {
            return string.Empty;
        }

        object ICommandContentInterpreter.Interpreter(YDCommunicationWrapper.Command data)
        {
            return Interpreter(data);
        }
    }
}