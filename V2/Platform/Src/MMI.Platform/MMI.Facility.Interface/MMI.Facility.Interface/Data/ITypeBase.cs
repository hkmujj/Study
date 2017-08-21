using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.IndexDescription;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 
    /// </summary>
    public enum CmdType
    {
        /// <summary>
        /// �˳�ϵͳ
        /// </summary>
        Exit,

        /// <summary>
        /// ��ҳ
        /// </summary>
        ChangePage,

        /// <summary>
        /// ����boolֵ
        /// </summary>
        SetBoolValue,

        /// <summary>
        /// ���������bool ֵ ��������ֻ������editģʽ, ����ģʽ�����ԡ�
        /// </summary>
        SetInBoolValue,

        /// <summary>
        /// ����floatֵ
        /// </summary>
        SetFloatValue,

        /// <summary>
        /// ���������Float ֵ ��������ֻ������editģʽ������ģʽ�����ԡ�
        /// </summary>
        SetInFloatValue,

        /// <summary>
        /// ����CIR��ӡ����Ҫ��ʾ������
        /// </summary>
        SetCirPrintString,

        /// <summary>
        /// ������չϵͳ��ֵ
        /// </summary>
        SetExValue,

        /// <summary>
        /// ��������id
        /// </summary>
        SetSoundId,
    }

    /// <summary>
    /// 
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface ITypeBase
    {
        /// <summary>
        /// 
        /// </summary>
        string ProjectName { set; get; }

        /// <summary>
        /// ������
        /// </summary>
        string MainIndex { get; }

        /// <summary>
        /// ��������ͼ���
        /// </summary>
        int ViewIndex { get; set; }

        /// <summary>
        /// ͨ������
        /// </summary>
        ICommunicationDataService CommunicationDataService { get; }

        /// <summary>
        /// ����
        /// </summary>
        IConfig IConfig { get; }

        /// <summary>
        /// ������� �ṩ���Ҷ���
        /// </summary>
        IObjectService ObjectService { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<int, bool> BoolList { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<int, float> FloatList { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<int, bool> OutBoolList { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<int, float> OutFloatList { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<int, bool> BoolOldList { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<int, float> FloatOldList { get; }

        /// <summary>
        /// 
        /// </summary>
        List<int> ViewList { get; }

        /// <summary>
        /// 
        /// </summary>
        string RecPath { set; get; }

        /// <summary>
        /// 
        /// </summary>
        AppPaths AppPaths { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IAppConfig AppConfig { set; get; }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        string GetTypeName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetInfo();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paraType"></param>
        /// <param name="paraIndex"></param>
        /// <returns></returns>
        string GetParaInfo(ParaType paraType, int paraIndex);

        /// <summary>
        /// �����ʼ��
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        bool init(ref int nErrorObjectIndex);

        /// <summary>
        /// ��ͼԪ�Ƿ�����ʹ��
        /// </summary>
        bool Enable { get; set; }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        event TypeBase.postCmd onPostCmd;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nCT"></param>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        void append_postCmd(CmdType nCT, int nParaA, int nParaB, float nParaC);

        /// <summary>
        /// 
        /// </summary>
        event TypeBase.postCmdA onPostCmdA;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nCT"></param>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        void append_postCmdA(int nIndex, CmdType nCT, int nParaA, int nParaB, float nParaC);

        /// <summary>
        /// 
        /// </summary>
        event TypeBase.postCmdB onPostCmdB;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nCT"></param>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        /// <param name="nStr"></param>
        void append_postCmd(CmdType nCT, int nParaA, int nParaB, float nParaC, string nStr);

        /// <summary>
        /// 
        /// </summary>
        void computValue();

    }

    /// <summary>
    /// Ĭ��ITypeBase��ʵ��
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "RedundantAssignment")]
    public class TypeBase : ITypeBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public virtual string MainIndex { get; private set; }

        /// <summary>
        /// ��������ͼ���
        /// </summary>
        public int ViewIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyDictionary<int, float> OutFloatList { get { return CommunicationDataService.WriteService.ReadOnlyFloatDictionary; } }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyDictionary<int, bool> BoolOldList
        {
            get
            {
                return CommunicationDataService.ReadService.ReadOnlyBoolOldDictionary;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyDictionary<int, float> FloatOldList
        {
            get
            {
                return CommunicationDataService.ReadService.ReadOnlyFloatOldDictionary;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RecPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AppPaths AppPaths { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IAppConfig AppConfig { get; set; }

        /// <summary>
        /// ��ͼԪ�Ƿ�����ʹ��
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// ͨ������
        /// </summary>
        public ICommunicationDataService CommunicationDataService { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public IConfig IConfig { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IIndexInterpreter IndexInterpreter { set; get; }

        /// <summary>
        /// ������� �ṩ���Ҷ���
        /// </summary>
        public IObjectService ObjectService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDataPackage DataPackage { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyDictionary<int, bool> BoolList
        {
            get { return CommunicationDataService.ReadService.ReadOnlyBoolDictionary; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyDictionary<int, float> FloatList
        {
            get { return CommunicationDataService.ReadService.ReadOnlyFloatDictionary; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyDictionary<int, bool> OutBoolList { get { return CommunicationDataService.WriteService.ReadOnlyBoolDictionary; } }


        /// <summary>
        /// 
        /// </summary>
        public virtual List<int> ViewList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TypeBase()
        {
            Enable = true;
            RecPath = @"c:\";
            ViewIndex = 0;
            MainIndex = "0";
        }

        //�¼�
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nCT"></param>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public delegate void postCmd(CmdType nCT, int nParaA, int nParaB, float nParaC);

        /// <summary>
        /// 
        /// </summary>
        public event postCmd onPostCmd;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nCT"></param>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public void append_postCmd(CmdType nCT, int nParaA, int nParaB, float nParaC)
        {
            if (onPostCmd != null)
            {

            }
            if (onPostCmdA != null)
            {
                if (nCT != CmdType.SetCirPrintString)
                {
                    onPostCmdA(ViewIndex, nCT, nParaA, nParaB, nParaC);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nCT"></param>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public delegate void postCmdA(int nIndex, CmdType nCT, int nParaA, int nParaB, float nParaC);

        /// <summary>
        /// 
        /// </summary>
        public event postCmdA onPostCmdA;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nCT"></param>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public void append_postCmdA(int nIndex, CmdType nCT, int nParaA, int nParaB, float nParaC)
        {
            if (onPostCmdA != null)
            {
                onPostCmdA(nIndex, nCT, nParaA, nParaB, nParaC);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nCT"></param>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        /// <param name="nStr"></param>
        public delegate void postCmdB(int nIndex, CmdType nCT, int nParaA, int nParaB, float nParaC, string nStr);

        /// <summary>
        /// 
        /// </summary>
        public event postCmdB onPostCmdB;

        /// <summary>
        /// ΪCIR��ӡ�����
        /// </summary>
        /// <param name="nCT"></param>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        /// <param name="nStr"></param>
        public void append_postCmd(CmdType nCT, int nParaA, int nParaB, float nParaC, string nStr)
        {
            if (onPostCmdB != null)
            {
                if (nCT != CmdType.SetCirPrintString)
                {
                    nStr = string.Empty;
                    append_postCmdA(ViewIndex, nCT, nParaA, nParaB, nParaC);
                }
                else
                {
                    onPostCmdB(ViewIndex, nCT, nParaA, nParaB, nParaC, nStr);
                }
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public virtual void computValue()
        {
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public string GetTypeName()
        {
            return GetType().Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetInfo()
        {
            return "������Ϣ";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paraType"></param>
        /// <param name="paraIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string GetParaInfo(ParaType paraType, int paraIndex)
        {
            ReadOnlyCollection<IIndexDescriptionProvider<int>> target = null;
            switch (paraType)
            {
                case ParaType.inBool:
                    target = IndexInterpreter.InBoolIndexDescriptionCollection;
                    break;
                case ParaType.outBool:
                    target = IndexInterpreter.OutBoolIndexDescriptionCollection;
                    break;
                case ParaType.inFloat:
                    target = IndexInterpreter.InFloatIndexDescriptionCollection;
                    break;
                case ParaType.outFloat:
                    target = IndexInterpreter.OutFloatIndexDescriptionCollection;
                    break;
                case ParaType.view:
                    break;
                case ParaType.uiPara:
                    break;
                case ParaType.uiIndex:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("paraType");
            }

            if (target != null)
            {
                var targetModel = target.FirstOrDefault(f => f.Index == paraIndex);
                if (targetModel!= null)
                {
                    return targetModel.Description;
                }
            }
            return "������Ϣ";
        }

        /// <summary>
        /// �����ʼ��
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public virtual bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = 0;
            return false;
        }

        /// <summary>
        /// ���ر�ʾ��ǰ <see cref="T:System.Object"/> �� <see cref="T:System.String"/>��
        /// </summary>
        /// <returns>
        /// <see cref="T:System.String"/>����ʾ��ǰ�� <see cref="T:System.Object"/>��
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return GetTypeName() + " " + MainIndex;
        }
    }
}
