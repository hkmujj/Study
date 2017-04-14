using MMI.Communacation.Interface.AppLayer;
using MMI.Communacation.Interface.ProtocolLayer;
using MMI.Facility.DataType.Log;

namespace MMI.Facility.Control.Command
{
    public class CommandInterpreterFactory
    {
        public static ICommandContentInterpreter GetContentInterpreter(MMI.YDCommunicationWrapper.Command cmd)
        {
            ICommandContentInterpreter interpreter = null;
            var cmdTpye = (CommandType) cmd.cmd;
            switch (cmdTpye)
            {
                case CommandType.HeartbeatPacketRequest :
                    break;
                case CommandType.CourseStart :
                    var subtype = (CourseStartSubType) cmd.subType;

                    SysLog.Info("Course start sub type = {0}", cmd.subType);

                    switch (subtype)
                    {
                        case CourseStartSubType.None :
                            interpreter = new NoneStartProjectCommandContentInterpreter();
                            break;
                        case CourseStartSubType.SetStartProject :
                            interpreter = new SetStartProjectCommandContentInterpreter();
                            break;
                            case  CourseStartSubType.XmlData:
                            interpreter = new XmlDataCommandContentInterpreter();
                            break;
                        default :
                            interpreter = new NoneStartProjectCommandContentInterpreter();
                            SysLog.Info(string.Format("The sub start course type {0} is not been valialbe ! ignore the sub type.", subtype));
                            break;
                    }
                    break;
                case CommandType.CourseOver :
                    break;
                case CommandType.CirCmdRecv :
                    break;
                default :
                    SysLog.Error(string.Format("Can not parse command type {0}", cmdTpye));
                    break;
            }
            return interpreter;
        }
    }
}