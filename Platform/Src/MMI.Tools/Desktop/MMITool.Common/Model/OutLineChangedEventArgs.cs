using System;

namespace MMITool.Common.Model
{
    /// <summary>
    /// OutLineChangedEventArgs
    /// </summary>
    public class OutLineChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="oldRectangle"></param>
        /// <param name="newRectangle"></param>
        public OutLineChangedEventArgs(OutLineChangedType type, T oldRectangle, T newRectangle)
        {
            NewRectangle = newRectangle;
            OldRectangle = oldRectangle;
            Type = type;
        }

        /// <summary>
        /// 
        /// </summary>
        public OutLineChangedType Type { private set; get;}

        /// <summary>
        /// 
        /// </summary>
        public T OldRectangle { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public T NewRectangle { private set; get; }
    }

    /// <summary>
    /// OutLineChanged type
    /// </summary>
    [Flags]
    public enum OutLineChangedType
    {
        /// <summary>
        /// 
        /// </summary>
        Location,
        /// <summary>
        /// 
        /// </summary>
        Size,
        /// <summary>
        /// 
        /// </summary>
        Outline = Location | Size,
    }
}
