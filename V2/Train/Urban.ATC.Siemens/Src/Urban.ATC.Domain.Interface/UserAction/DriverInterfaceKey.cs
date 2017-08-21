using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Motor.ATP.Domain.Interface.UserAction
{
    public class DriverInterfaceKey
    {
        protected bool Equals(DriverInterfaceKey other)
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
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((DriverInterfaceKey)obj);
        }

        /// <summary>
        /// Root结点， 初始化状态
        /// </summary>
        public static readonly DriverInterfaceKey Root = new DriverInterfaceKey(new List<string>() { "Root" });

        public static readonly ReadOnlyCollection<string> EmptyPaths = Enumerable.Empty<string>().ToList().AsReadOnly();

        /// <summary>
        /// 路径集合
        /// </summary>
        public ReadOnlyCollection<string> Paths { private set; get; }

        public DriverInterfaceKey(IList<string> paths = null)
        {
            Paths = paths != null ? new ReadOnlyCollection<string>(paths) : EmptyPaths;
        }

        [DebuggerStepThrough]
        public static DriverInterfaceKey Parser(string content)
        {
            return String.IsNullOrWhiteSpace(content)
                ? new DriverInterfaceKey()
                : new DriverInterfaceKey(content.Split(new char[1] { '-' }).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Paths != null)
            {
                return String.Join("-", Paths);
            }
            return String.Empty;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        //
        public static bool operator ==(DriverInterfaceKey a, DriverInterfaceKey b)
        {
            if (ReferenceEquals(null, a))
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(DriverInterfaceKey a, DriverInterfaceKey b)
        {
            return !(a == b);
        }
    }

    public class DriverInterfaceKeyEqualityComparer : IEqualityComparer<DriverInterfaceKey>
    {
        public bool Equals(DriverInterfaceKey x, DriverInterfaceKey y)
        {
            return x.ToString() == y.ToString();
        }

        public int GetHashCode(DriverInterfaceKey obj)
        {
            return obj.GetHashCode();
        }
    }
}