using System.Collections.Generic;
using Urban.Phillippine.View.Interface;

namespace Urban.Phillippine.View.Constant
{
    public static class EList
    {
        public static void Changed<T>(this IList<T> para, IDictionary<int, bool> args, int? iPara = null)
            where T : IStatusChanged
        {
            foreach (var s in para)
            {
                s.Changed(args, iPara);
            }
        }

        public static void Changed<T>(this IList<T> para, IDictionary<int, float> args, int? iPara = null)
            where T : IStatusChanged
        {
            foreach (var s in para)
            {
                s.Changed(args, iPara);
            }
        }
    }
}