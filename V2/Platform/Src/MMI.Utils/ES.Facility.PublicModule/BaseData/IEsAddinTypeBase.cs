using System;
using System.Collections.Generic;

namespace ES.Facility.PublicModule.BaseData
{
    #region ���ͽӿ�����
    /// <summary>
    /// ���ͽӿ�����
    /// </summary>
    [CLSCompliant(false)]
    public interface IEsAddinTypeBase : IVerHelper, IMemoHelper, IInteractionBaseClass
    {
        /// <summary>
        /// �Ƿ���������
        /// </summary>
        bool Enable { get; set; }

        /// <summary>
        /// Ψһ����
        /// �ؼ�����Ϣ��ͨ������Ϣ�ҵ�ʵ������
        /// </summary>
        /// <returns></returns>
        string getClassKeyName();

        /// <summary>
        /// ES�Ĺ�������
        /// </summary>
        EsModeType EsType { get; set;}

        /// <summary>
        /// �����ʼ��
        /// </summary>
        /// <returns></returns>
        bool init(ref List<string> cParaList, ref int nErrorObjectIndex);

        /// <summary>
        /// �����xֵ
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        bool initValue(string nParaString, ref int nErrorObjectIndex);

        /// <summary>
        /// ������Ϣ
        /// </summary>
        Dictionary<string, string> Config { get; set;}

        bool callFunc(AttributeType nAT, CallFunType nCFT, int nParaIntA, int nParaIntB, object nObject);
        
    }
    #endregion
    
    #region ���ͽӿ�ʵ����
    /// <summary>
    /// ���ͽӿ�ʵ����
    /// </summary>
    [CLSCompliant(false)]
    public class EsAddinTypeBase : InteractionBaseClass, IEsAddinTypeBase
    {
        #region ::: IEsAddinTypeBase �ӿ�

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
