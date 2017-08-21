using System.Collections.Generic;

namespace Engine._6A.EventArgs
{
    public class ColumnEventArgs
    {
        public ColumnEventArgs() { }
        public int NewIndex { get; set; }

        public int OldIndex { get; set; }
        public int PageIndex { get; set; }
        public IEnumerable<object> StateCollection { get; set; }
        public string Region { get; set; }
    }
}