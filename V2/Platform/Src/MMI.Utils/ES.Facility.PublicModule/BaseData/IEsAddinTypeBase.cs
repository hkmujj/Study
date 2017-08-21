using System;
using System.Collections.Generic;

namespace ES.Facility.PublicModule.BaseData
{
    #region 类型接口类型
    /// <summary>
    /// 类型接口类型
    /// </summary>
    [CLSCompliant(false)]
    public interface IEsAddinTypeBase : IVerHelper, IMemoHelper, IInteractionBaseClass
    {
        /// <summary>
        /// 是否允许启用
        /// </summary>
        bool Enable { get; set; }

        /// <summary>
        /// 唯一类名
        /// 关键子信息，通过该信息找到实例对象
        /// </summary>
        /// <returns></returns>
        string getClassKeyName();

        /// <summary>
        /// ES的功能类型
        /// </summary>
        EsModeType EsType { get; set;}

        /// <summary>
        /// 对象初始化
        /// </summary>
        /// <returns></returns>
        bool init(ref List<string> cParaList, ref int nErrorObjectIndex);

        /// <summary>
        /// 參數賦值
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        bool initValue(string nParaString, ref int nErrorObjectIndex);

        /// <summary>
        /// 配置信息
        /// </summary>
        Dictionary<string, string> Config { get; set;}

        bool callFunc(AttributeType nAT, CallFunType nCFT, int nParaIntA, int nParaIntB, object nObject);
        
    }
    #endregion
    
    #region 类型接口实现类
    /// <summary>
    /// 类型接口实现类
    /// </summary>
    [CLSCompliant(false)]
    public class EsAddinTypeBase : InteractionBaseClass, IEsAddinTypeBase
    {
        #region ::: IEsAddinTypeBase 接口

        public EsAddinTypeBase()
        {
            Enable = false;
            Config = new Dictionary<string, string>();
            EsType = new EsModeType();
        }


        public bool Enable { get; set; }

        public virtual string getClassKeyName() { return "ObjectValueBase"; }

        public EsModeType EsType { get; set; }


        public virtual bool init(ref List<string> cParaList, ref int nErrorObjectIndex) { nErrorObjectIndex = 0; return false; }

        public virtual bool initValue(string nParaString, ref int nErrorObjectIndex) { nErrorObjectIndex = 0; return false; }


        public Dictionary<string, string> Config { get; set; }

        public virtual bool callFunc(AttributeType nAT, CallFunType nCFT, int nParaIntA, int nParaIntB, object nObject) { return false; }
        #endregion

        
    }
    #endregion
}
