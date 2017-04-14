using System;

namespace ES.Facility.PublicModule.BaseData
{
    public class DataDefind
    {
        static private readonly int snLength = 17;
        /// <summary>
        /// ���ݱ�Ǻų���
        /// </summary>
        static public int SnLength { get { return snLength; } }

        static private readonly int defindDataLength = 500;
        /// <summary>
        /// Ĭ�ϵ��������峤��
        /// </summary>
        static public int DefindDataLength { get { return defindDataLength; } }

    }

    public enum DataUseType : int
    {
        /// <summary> ��״̬</summary>
        None = 0,

        /// <summary>
        /// Ĭ�ϴ洢��ʽ
        /// </summary>
        Defind = 1,

        /// <summary>
        /// ��չ����(װ��ģʽ)�洢��ʽ
        /// </summary>
        ExtObject = 2,
    }

    public enum DataFormType : int
    {
        /// <summary>
        /// ��չ����
        /// </summary>
        Ex = 0,

        Int = 11,

        Float = 12,

        Double = 13,

        String = 20,
    }

    /// <summary>
    /// ���й����ݴ��ݵ���Ҫ����
    /// </summary>
    /// <remarks>
    /// ���Ȳ��� CDataDefind��
    /// </remarks>
    [CLSCompliant(false)]
    unsafe public struct SInteraction
    {
        /// <summary>
        /// ���ݱ�Ǻ�
        /// </summary>
        public fixed char SN[17];
        
        /// <summary>
        /// ��������
        /// �Զ������������1000
        /// </summary>
        public Int32 DataType;

        /// <summary>
        /// Ĭ�ϵ���������
        /// </summary>
        /// <remarks>
        /// ����󴫵�ʱ���øýṹ�������ݴ���
        /// </remarks>
        public fixed byte DefindData[500];
    }
    
    [CLSCompliant(false)]
    public class CInteraction : IInteraction
    {
        #region ::::::::::::::::::::::::::::::::  attrible  ::::::::::::::::::::::::::::::::
        
        protected SInteraction _sInteractionData;
        /// <summary>���й����ݴ��ݵ���Ҫ����</summary>
        public SInteraction SInteractionData
        {
            get { return _sInteractionData; }
            set { _sInteractionData = value; }
        }

        protected DataFormType _objectDataType = 0;
        /// <summary>
        /// ��������
        /// </summary>
        public DataFormType ObjectDataType
        {
            get { return _objectDataType; }
            set { _objectDataType = value; }
        }

        protected int _objectDataExType = 0;

        public int ObjectDataExType
        {
            get { return _objectDataExType; }
            set { _objectDataExType = value; }
        }

        string _objectAExString = string.Empty;
        /// <summary>
        /// ����ʹ�ö��������ַ�����
        /// </summary>
        public string ObjectAExString
        {
            get { return _objectAExString; }
            set { _objectAExString = value; }
        }

        int _cmdId = -1;
        /// <summary>
        /// ������
        /// </summary>
        public int CmdId { get { return _cmdId; } set { _cmdId = value; } }


        int _selfId = -1;
        public int Id { get { return _selfId; } set { _selfId = value; } }

        int _objectAExIntA = 0;
        /// <summary>
        /// ����ʹ�ö���������������
        /// </summary>
        public int ObjectAExIntA
        {
            get { return _objectAExIntA; }
            set { _objectAExIntA = value; }
        }

        int _objectAExIntB = 0;
        /// <summary>
        /// ����ʹ�ö���������������
        /// </summary>
        public int ObjectAExIntB
        {
            get { return _objectAExIntB; }
            set { _objectAExIntB = value; }
        }

        float _objectAExFloatA = 0f;
        /// <summary>
        /// ����ʹ�ö������ĸ�������
        /// </summary>
        public float ObjectAExFloatA
        {
            get { return _objectAExFloatA; }
            set { _objectAExFloatA = value; }
        }

        float _objectAExFloatB = 0f;
        /// <summary>
        /// ����ʹ�ö������ĸ�������
        /// </summary>
        public float ObjectAExFloatB
        {
            get { return _objectAExFloatB; }
            set { _objectAExFloatB = value; }
        }

        float _objectAExFloatC = 0f;
        /// <summary>
        /// ����ʹ�ö������ĸ�������
        /// </summary>
        public float ObjectAExFloatC
        {
            get { return _objectAExFloatC; }
            set { _objectAExFloatC = value; }
        }

        float _objectAExFloatD = 0f;
        /// <summary>
        /// ����ʹ�ö������ĸ�������
        /// </summary>
        public float ObjectAExFloatD
        {
            get { return _objectAExFloatD; }
            set { _objectAExFloatD = value; }
        }

        float _objectAExFloatE = 0f;
        /// <summary>
        /// ����ʹ�ö������ĸ�������
        /// </summary>
        public float ObjectAExFloatE
        {
            get { return _objectAExFloatE; }
            set { _objectAExFloatE = value; }
        }

        protected object _objectData = null;
        /// <summary>��չ���͵���������</summary>
        /// <remarks>ʹ�øö���ʱ������װж��Ĳ����������Ч�ʵ���</remarks>
        public object ObjectData
        {
            get { return _objectData; }
            set { _objectData = value; }
        }
        
        protected DataUseType _dataMode = DataUseType.None;
        /// <summary>���ݴ洢��ʽ</summary>
        public DataUseType DataMode
        {
            get { return _dataMode; }
            set { _dataMode = value; }
        }

        #endregion

        #region ::::::::::::::::::::::::::::::::  method    ::::::::::::::::::::::::::::::::
        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="nDUT"></param>
        /// <param name="nDFT"></param>
        /// <param name="nDFET"></param>
        public void initInteraction(DataUseType nDUT, DataFormType nDFT, int nobjectDataExType)
        {
            _dataMode = nDUT;
            _objectDataType = nDFT;
            _objectDataExType = nobjectDataExType;

            _objectData = null;         //��������Ϊ��
        }
                
        #endregion

    }
}
