using System.Diagnostics;

namespace MMI.Facility.Interface.Args
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataChangedArgs<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldData"></param>
        /// <param name="newData"></param>
        [DebuggerStepThrough]
        public DataChangedArgs(T oldData, T newData)
        {
            NewData = newData;
            OldData = oldData;
        }

        /// <summary>
        /// 
        /// </summary>
        public T OldData { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public T NewData { private set; get; }
    }
}
