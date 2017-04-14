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
        /// 退出系统
        /// </summary>
        Exit,

        /// <summary>
        /// 换页
        /// </summary>
        ChangePage,

        /// <summary>
        /// 设置bool值
        /// </summary>
        SetBoolValue,

        /// <summary>
        /// 设置输入的bool 值 ，此类型只适用于edit模式, 其它模式被忽略。
        /// </summary>
        SetInBoolValue,

        /// <summary>
        /// 设置float值
        /// </summary>
        SetFloatValue,

        /// <summary>
        /// 设置输入的Float 值 ，此类型只适用于edit模式，其它模式被忽略。
        /// </summary>
        SetInFloatValue,

        /// <summary>
        /// 设置CIR打印机需要显示的内容
        /// </summary>
        SetCirPrintString,

        /// <summary>
        /// 设置扩展系统的值
        /// </summary>
        SetExValue,

        /// <summary>
        /// 设置声音id
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
        /// 主分类
        /// </summary>
        string MainIndex { get; }

        /// <summary>
        /// 从属的视图编号
        /// </summary>
        int ViewIndex { get; set; }

        /// <summary>
        /// 通信数据
        /// </summary>
        ICommunicationDataService CommunicationDataService { get; }

        /// <summary>
        /// 数据
        /// </summary>
        IConfig IConfig { get; }

        /// <summary>
        /// 对象服务， 提供查找对象
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
        /// 获取类型名称
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
        /// 对象初始化
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        bool init(ref int nErrorObjectIndex);

        /// <summary>
        /// 该图元是否允许使用
        /// </summary>
        bool Enable { get; set; }

        /// <summary>
        /// 发送信息处理
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
    /// 默认ITypeBase的实现
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
        /// 主分类
        /// </summary>
        public virtual string MainIndex { get; private set; }

        /// <summary>
        /// 从属的视图编号
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
        /// 该图元是否允许使用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 通信数据
        /// </summary>
        public ICommunicationDataService CommunicationDataService { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public IConfig IConfig { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IIndexInterpreter IndexInterpreter { set; get; }

        /// <summary>
        /// 对象服务， 提供查找对象
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

        //事件
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
        /// 为CIR打印而添加
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
        /// 计算对象
        /// </summary>
        public virtual void computValue()
        {
        }

        /// <summary>
        /// 获取类型名称
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
            return "帮助信息";
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
            return "参数信息";
        }

        /// <summary>
        /// 对象初始化
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public virtual bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = 0;
            return false;
        }

        /// <summary>
        /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return GetTypeName() + " " + MainIndex;
        }
    }
}
