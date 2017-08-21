using System;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using Subway.TCMS.Infrasturcture.Model.Constance;

namespace Subway.TCMS.Infrasturcture.Service
{
    /// <summary>
    /// ��Ŀ�����ļ�����
    /// </summary>
    public class ProjectDecriptionService : IProjectDecriptionService
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="type">����������</param>
        public ProjectDecriptionService(TCMSType type)
        {
            Type = type;
        }

        /// <summary>
        /// ����������
        /// </summary>
        public TCMSType Type { get; private set; }

        /// <summary>
        /// ����Bool�ӿ������ֵ�
        /// </summary>
        public IReadOnlyDictionary<string, int> InBoolDictionary { get; private set; }

        /// <summary>
        /// ����Float�ӿ������ֵ�
        /// </summary>
        public IReadOnlyDictionary<string, int> InFloatDictionary { get; private set; }

        /// <summary>
        /// ���Bool�ӿ������ֵ�
        /// </summary>
        public IReadOnlyDictionary<string, int> OutBoolDictionary { get; private set; }

        /// <summary>
        /// ���Float�ӿ������ֵ�
        /// </summary>
        public IReadOnlyDictionary<string, int> OutFloatDictionary { get; private set; }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="config"></param>
        public void Initaliza(IProjectIndexDescriptionConfig config)
        {
            if (config == null)
            {
                AppLog.Error("{0}�����ļ���ϢΪ��,���������ļ���Ϣ��", Type);
                return;
            }

            InBoolDictionary = config.InFloatDescriptionDictionary;
            InFloatDictionary = config.InFloatDescriptionDictionary;
            OutBoolDictionary = config.OutBoolDescriptionDictionary;
            OutFloatDictionary = config.OutFloatDescriptionDictionary;
            AppLog.Info("{0}�����ļ�������ɣ�", this.Type);
        }
    }
}