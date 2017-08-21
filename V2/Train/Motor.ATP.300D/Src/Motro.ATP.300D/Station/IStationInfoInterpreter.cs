using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Motor.ATP._300D.Station
{
    public interface IStationInfoInterpreter
    {
        void Initalize(SortedList<int, string> resource);

        /// <summary>
        /// 将网络数据解释为车站信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Interpret(float value);
    }

    public abstract class StationInfoInterpreterBase : IStationInfoInterpreter
    {
        protected SortedList<int, string> Resource { private set; get; }

        public virtual void Initalize(SortedList<int, string> resource)
        {
            Resource = resource;
        }

        protected string GetResource(int index)
        {
            if (Resource!=null && Resource.ContainsKey(index))
            {
                return Resource[index];
            }
            return string.Empty;
        }

        /// <summary>
        /// 将网络数据解释为车站信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Interpret(float value)
        {
            return GetResource(InterpretedAsResourceIndex(value));
        }

        /// <summary>
        /// 解释为资源索引
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract int InterpretedAsResourceIndex(float value);

    }
}