using System;

namespace CRH2MMI.TrainLineNo
{
    class ContentPressedEventArgs<T> : EventArgs
    {
        public ContentPressedEventArgs(T content)
        {
            Content = content;
        }

        public T Content { private set; get; }
    }
}
