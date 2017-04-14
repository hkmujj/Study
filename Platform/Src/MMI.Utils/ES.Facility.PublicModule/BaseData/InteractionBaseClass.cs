using System;

namespace ES.Facility.PublicModule.BaseData
{
    /// <summary>
    /// 域内交互的接口
    /// </summary>
    public interface IInteractionBaseClass
    {
        event InteractionBaseClass.postData OnGetPostData;
        void append_postData(int nDataType, Object nObject);

        event InteractionBaseClass.postCmds OnGetPostCmd;
        void append_PostCmds(int nDataType, int nParaIntA, int nParaIntB, float nParaFloatA, float nParaFloatB);
    }

    /// <summary>
    /// 域内交互的基础实现类
    /// </summary>
    public class InteractionBaseClass : VerHelper,  IInteractionBaseClass
    {
        public delegate void postData(int nDataType, Object nObject);
        public event postData OnGetPostData;
        public void append_postData(int nDataType, Object nObject)
        {
            if (OnGetPostData != null) OnGetPostData((int)nDataType, (Object)nObject);
        }

        public delegate void postCmds(int nDataType, int nParaIntA, int nParaIntB, float nParaFloatA, float nParaFloatB);
        public event postCmds OnGetPostCmd;
        public void append_PostCmds(int nDataType, int nParaIntA, int nParaIntB, float nParaFloatA, float nParaFloatB)
        {
            if (OnGetPostCmd != null) OnGetPostCmd(nDataType, nParaIntA, nParaIntB, nParaFloatA, nParaFloatB);
        }
    }
}
