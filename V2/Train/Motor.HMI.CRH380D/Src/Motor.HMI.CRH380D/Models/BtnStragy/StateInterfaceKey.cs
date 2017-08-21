using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Motor.HMI.CRH380D.Models.BtnStragy
{
    public class StateInterfaceKey
    {
        protected bool Equals(StateInterfaceKey other)
        {
            return ToString() == other.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((StateInterfaceKey) obj);
        }

        /// <summary>
        /// Root结点， 初始化状态
        /// </summary>
        public static readonly StateInterfaceKey Root = new StateInterfaceKey(new List<string>() {"Root"});

        public static readonly ReadOnlyCollection<string> EmptyPaths = Enumerable.Empty<string>().ToList().AsReadOnly();

        /// <summary>
        /// 路径集合
        /// </summary>
        public ReadOnlyCollection<string> Paths { private set; get; }

        public StateInterfaceKey Parent
        {
            get { return Parser(string.Join("-", Paths.Take(Paths.Count - 1))); }
        }

        public StateInterfaceKey(IList<string> paths = null)
        {
            Paths = paths != null ? new ReadOnlyCollection<string>(paths) : EmptyPaths;
        }

        [DebuggerStepThrough]
        public static StateInterfaceKey Parser(string content)
        {
            return string.IsNullOrWhiteSpace(content)
                ? new StateInterfaceKey()
                : new StateInterfaceKey(content.Split('-').ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Paths != null)
            {
                return string.Join("-", Paths);
            }
            return string.Empty;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        //
        public static bool operator ==(StateInterfaceKey a, StateInterfaceKey b)
        {
            if (ReferenceEquals(null, a) && ReferenceEquals(null, b))
            {
                return true;
            }
            if (ReferenceEquals(null, a) || ReferenceEquals(null, b))
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(StateInterfaceKey a, StateInterfaceKey b)
        {
            return !(a == b);
        }
    }

    public class StateInterfaceKeyEqualityComparer : IEqualityComparer<StateInterfaceKey>
    {
        public bool Equals(StateInterfaceKey x, StateInterfaceKey y)
        {
            return x.ToString() == y.ToString();
        }

        public int GetHashCode(StateInterfaceKey obj)
        {
            return obj.GetHashCode();
        }
    }
}