using System;

namespace ES.Facility.PublicModule.BaseData
{
    public class DataDefind
    {
        static private readonly int snLength = 17;
        /// <summary>
        /// 数据标记号长度
        /// </summary>
        static public int SnLength { get { return snLength; } }

        static private readonly int defindDataLength = 500;
        /// <summary>
        /// 默认的数据载体长度
        /// </summary>
        static public int DefindDataLength { get { return defindDataLength; } }

    }

    public enum DataUseType : int
    {
        /// <summary> 无状态</summary>
        None = 0,

        /// <summary>
        /// 默认存储方式
        /// </summary>
        Defind = 1,

        /// <summary>
        /// 扩展类型(装箱模式)存储方式
        /// </summary>
        ExtObject = 2,
    }

    public enum DataFormType : int
    {
        /// <summary>
        /// 扩展类型
        /// </summary>
        Ex = 0,

        Int = 11,

        Float = 12,

        Double = 13,

        String = 20,
    }

    /// <summary>
    /// 非托管数据传递的主要载体
    /// </summary>
    /// <remarks>
    /// 长度参照 CDataDefind类
    /// </remarks>
    [CLSCompliant(false)]
    unsafe public struct SInteraction
    {
        /// <summary>
        /// 数据标记号
        /// </summary>
        public fixed char SN[17];
        
        /// <summary>
        /// 数据类型
        /// 自定义类型需大于1000
        /// </summary>
        public Int32 DataType;

        /// <summary>
        /// 默认的数据载体
        /// </summary>
        /// <remarks>
        /// 跨对象传递时采用该结构进行数据传递
        /// </remarks>
        public fixed byte DefindData[500];
    }
    
    [CLSCompliant(false)]
    public class CInteraction : IInteraction
    {
        #region ::::::::::::::::::::::::::::::::  attrible  ::::::::::::::::::::::::::::::::
        
        protected SInteraction _sInteractionData;
        /// <summary>非托管数据传递的主要载体</summary>
        public SInteraction SInteractionData
        {
            get { return _sInteractionData; }
            set { _sInteractionData = value; }
        }

        protected DataFormType _objectDataType = 0;
        /// <summary>
        /// 数据类型
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
        /// 方便使用而建立的字符类型
        /// </summary>
        public string ObjectAExString
        {
            get { return _objectAExString; }
            set { _objectAExString = value; }
        }

        int _cmdId = -1;
        /// <summary>
        /// 命令编号
        /// </summary>
        public int CmdId { get { return _cmdId; } set { _cmdId = value; } }


        int _selfId = -1;
        public int Id { get { return _selfId; } set { _selfId = value; } }

        int _objectAExIntA = 0;
        /// <summary>
        /// 方便使用而建立的整数类型
        /// </summary>
        public int ObjectAExIntA
        {
            get { return _objectAExIntA; }
            set { _objectAExIntA = value; }
        }

        int _objectAExIntB = 0;
        /// <summary>
        /// 方便使用而建立的整数类型
        /// </summary>
        public int ObjectAExIntB
        {
            get { return _objectAExIntB; }
            set { _objectAExIntB = value; }
        }

        float _objectAExFloatA = 0f;
        /// <summary>
        /// 方便使用而建立的浮点类型
        /// </summary>
        public float ObjectAExFloatA
        {
            get { return _objectAExFloatA; }
            set { _objectAExFloatA = value; }
        }

        float _objectAExFloatB = 0f;
        /// <summary>
        /// 方便使用而建立的浮点类型
        /// </summary>
        public float ObjectAExFloatB
        {
            get { return _objectAExFloatB; }
            set { _objectAExFloatB = value; }
        }

        float _objectAExFloatC = 0f;
        /// <summary>
        /// 方便使用而建立的浮点类型
        /// </summary>
        public float ObjectAExFloatC
        {
            get { return _objectAExFloatC; }
            set { _objectAExFloatC = value; }
        }

        float _objectAExFloatD = 0f;
        /// <summary>
        /// 方便使用而建立的浮点类型
        /// </summary>
        public float ObjectAExFloatD
        {
            get { return _objectAExFloatD; }
            set { _objectAExFloatD = value; }
        }

        float _objectAExFloatE = 0f;
        /// <summary>
        /// 方便使用而建立的浮点类型
        /// </summary>
        public float ObjectAExFloatE
        {
            get { return _objectAExFloatE; }
            set { _objectAExFloatE = value; }
        }

        protected object _objectData = null;
        /// <summary>扩展类型的数据载体</summary>
        /// <remarks>使用该对象时，由于装卸箱的操作，会造成效率低下</remarks>
        public object ObjectData
        {
            get { return _objectData; }
            set { _objectData = value; }
        }
        
        protected DataUseType _dataMode = DataUseType.None;
        /// <summary>数据存储方式</summary>
        public DataUseType DataMode
        {
            get { return _dataMode; }
            set { _dataMode = value; }
        }

        #endregion

        #region ::::::::::::::::::::::::::::::::  method    ::::::::::::::::::::::::::::::::
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="nDUT"></param>
        /// <param name="nDFT"></param>
        /// <param name="nDFET"></param>
        public void initInteraction(DataUseType nDUT, DataFormType nDFT, int nobjectDataExType)
        {
            _dataMode = nDUT;
            _objectDataType = nDFT;
            _objectDataExType = nobjectDataExType;

            _objectData = null;         //将对象置为空
        }
                
        #endregion

    }
}
