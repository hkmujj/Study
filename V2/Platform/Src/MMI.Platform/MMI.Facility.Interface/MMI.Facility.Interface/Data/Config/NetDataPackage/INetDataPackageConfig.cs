namespace MMI.Facility.Interface.Data.Config.NetDataPackage
{
    /// <summary>
    /// 
    /// </summary>
    public interface INetDataPackageConfig
    {
        /// <summary>
        /// ���ݰ���
        /// </summary>
        int PackageCount { get; }

        /// <summary>
        /// floatֵ����ʼbyteλ��
        /// </summary>
        int FloatStartByte { get; }

        /// <summary>
        /// floatֵ��byte����
        /// </summary>
        int FloatCountOfByte { get; }

        /// <summary>
        /// Boolֵ����ʼbyteλ��
        /// </summary>
        int BoolStartByte { get; }

        /// <summary>
        /// Boolֵ��byte����
        /// </summary>
        int BoolCountOfByte { get; }

        /// <summary>
        /// floatֵ��ӳ����ʼ��ַ
        /// </summary>
        int FloatMappedStartIndex { get; }

        /// <summary>
        /// floatֵ��ӳ����ʼ��ַ
        /// </summary>
        int BoolMappedStartIndex { get; }

        /// <summary>
        /// ���float���ܸ���
        /// </summary>
        /// <returns></returns>
        int GetTotalFloatCount();

        /// <summary>
        /// ��� bool ���ܸ���
        /// </summary>
        /// <returns></returns>
        int GetTotalBoolCount();
    }
}