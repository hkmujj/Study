namespace CRH2MMI.Common.Models
{
    /// <summary>
    /// 字典的key class 需要继承, 实现 ToString 方法
    /// </summary>
    public abstract class DictionaryKeyModel
    {
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }


    }
}
