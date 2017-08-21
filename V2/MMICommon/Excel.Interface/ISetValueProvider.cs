namespace Excel.Interface
{
    /// <summary>
    /// 为 excel 调用 设置值。
    /// </summary>
    public interface ISetValueProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        void SetValue(string propertyOrFieldName, string value);
    }
}