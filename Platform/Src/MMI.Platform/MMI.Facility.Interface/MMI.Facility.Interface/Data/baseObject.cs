using System;
using MMI.Facility.Interface.Data.Config;

// ReSharper disable All

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// ͼԪ����������Ϣ
    /// �ö������ڴ洢ԭʼ����
    /// </summary>
    [Serializable]
    public class baseObjectInfo : IAppBaseObjectInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public baseObjectInfo()
        {
            OutputBoolIndexs = string.Empty;
            InputBoolIndexs = string.Empty;
            MainIndex = 0;
            ParaIndexs = string.Empty;
            InputFloatIndexs = string.Empty;
            OutputFloatIndexs = string.Empty;
            ViewInfo = string.Empty;
            ClassName = string.Empty;
        }

        /// <summary>
        /// ���õ�����
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// ������ͼ��Ϣ
        /// </summary>
        public string ViewInfo { get; set; }

        /// <summary>
        /// ����bool��Ӧ��Ϣ
        /// </summary>
        public string InputBoolIndexs { get; set; }

        /// <summary>
        /// ���bool��Ӧ��Ϣ
        /// </summary>
        public string OutputBoolIndexs { get; set; }

        /// <summary>
        /// ����float��Ӧ��Ϣ
        /// </summary>
        public string InputFloatIndexs { get; set; }

        /// <summary>
        /// ���float��Ӧ��Ϣ
        /// </summary>
        public string OutputFloatIndexs { get; set; }

        /// <summary>
        /// ������Ӧ��Ϣ
        /// </summary>
        public string ParaIndexs { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int MainIndex { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ObjectKeyWord
    {
        /// <summary>
        /// UI����
        /// </summary>
        objectName,

        /// <summary>
        /// ��ͼ
        /// </summary>
        form,

        /// <summary>
        /// ����bool
        /// </summary>
        inBool,

        /// <summary>
        /// ����float
        /// </summary>
        inFloat,

        /// <summary>
        /// ���bool
        /// </summary>
        outBool,

        /// <summary>
        /// ���float
        /// </summary>
        outFloat,

        /// <summary>
        /// ��������
        /// </summary>
        para,
        
        /// <summary>
        /// ���
        /// </summary>
        index,
    }
   
}
