namespace Excel.Interface
{
    /// <summary>
    /// Ϊ excel ���� ����ֵ��
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