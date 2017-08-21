using System;

namespace ES.Facility.PublicModule.IO
{
    /// <summary>
    /// �����ļ��Ĵ򿪡��رա���ȡ�ӿ�
    /// </summary>
    /// <remarks>�ļ������ӿ�</remarks>
    public interface IFileOperate
    {
        /// <summary>
        /// �ļ�ϵͳΪ·��
        /// ���ݿ�ϵͳ��Ϊ������
        /// </summary>
        String LinkPath { get; set;}

        /// <summary>
        /// ���ļ�������
        /// </summary>
        void Open();

        /// <summary>
        /// �ر��ļ�������
        /// </summary>
        void Close();
    }


    /// <summary>
    /// �������ݲ�����ʽ
    /// </summary>
    public interface IDataOperate
    {
        /// <summary>
        /// ��ѯ
        /// </summary>
        T Select<T>(string keyStr, string keyName, string defaultValue="NULL") where T : class;

        /// <summary>
        /// ����
        /// </summary>
        int Updata(string keyStr, string keyName, string value);

        /// <summary>
        /// ����
        /// </summary>
        int Inset(string keyStr, string keyName, string value);

        /// <summary>
        /// ɾ��
        /// </summary>
        int Delete(string keyStr, string keyName);
    }

    /// <summary>
    /// �ⲿ���ݲ���ģʽ
    /// </summary>
    public interface IIoHelper : IFileOperate, IDataOperate
    {

        #region ����

        /// <summary>
        /// �ļ�״̬�Ƿ�Ϊ����
        /// </summary>
        /// <value>
        /// true ����
        /// false �ر�
        /// </value>
        bool IsOpen { get;  set;  }
        
        #endregion
    }

}//end ns
