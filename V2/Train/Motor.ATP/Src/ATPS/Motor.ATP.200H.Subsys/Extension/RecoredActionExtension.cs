using System;
using Motor.ATP._200H.Subsys.Model;

namespace Motor.ATP._200H.Subsys.Extension
{
    public static class RecoredActionExtension
    {
        private static readonly TimeSpan InterSpan = TimeSpan.FromMilliseconds(1000);

        public static bool IsInDoubleClickSpan(this RecoredAction recoredAction)
        {
            return (DateTime.Now - recoredAction.DateTime).Duration() <= InterSpan;
        }

        public static bool IsInDoubleClickSpan(this RecoredAction recoredAction, RecoredAction other)
        {
            return (other.DateTime - recoredAction.DateTime).Duration() <= InterSpan;
        }
    }
}