using System;
using System.Diagnostics;
using MMI.Facility.Interface;

namespace MMI.Communacation.Interface.AppLayer
{
    /// <summary>
    /// 网络数据
    /// </summary>
    public class NetDatas
    {
        [DebuggerStepThrough]
        public NetDatas(ReceiveDataModel<bool> receivedBools, ReceiveDataModel<float> receivedFloats, ProjectType projectType = ProjectType.Unkown)
        {
            ReceivedBools = receivedBools;
            ReceivedFloats = receivedFloats;
            ProjectType = projectType;
            if (!Enum.IsDefined(typeof(ProjectType), projectType))
            {
                ProjectType = ProjectType.Unkown;
            }
        }

        public ProjectType ProjectType { get; private set; }

        public ReceiveDataModel<bool> ReceivedBools { private set; get; }

        public ReceiveDataModel<float> ReceivedFloats { private set; get; }

    }
}