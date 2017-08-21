using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Running;
using MMI.Facility.PublicUI.Interface;

namespace MMI.Facility.View.Views.Common
{
    public partial class ProjectInfoControl : UserControl, IProjectInfoControl
    {
        public ProjectInfoControl(string appName, IDataPackage dataPackage)
            : this()
        {
            AppName = appName;
            DataPackage = dataPackage;
            Config = dataPackage.Config;
            AppConfig = Config.AppConfigs.First(f => f.AppName == appName);
            PAddinObjectsLT = dataPackage.AddInLoader.ProjetAddinInstanceDic[appName];
            PAddinTypeDic = dataPackage.AddInLoader.AddinTypeDictionary;
            RunningViewParam = dataPackage.RunningParam.AppRunningParamDictionary[appName].RunningViewParam;
            PViewsInfoDic = RunningViewParam.ViewUnitParamDic;
        }

        private ProjectInfoControl()
        {
            InitializeComponent();
        }

        public string AppName { get; private set; }

        public virtual void InitPorjectList()
        {
            throw new NotImplementedException();
        }

        public virtual void InitViewsList()
        {
            throw new NotImplementedException();
        }

        public virtual void InitClassList()
        {
            throw new NotImplementedException();
        }

        public virtual void InitObjectList()
        {
            throw new NotImplementedException();
        }

        public IDataPackage DataPackage { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        public IConfig Config { private set; get; }

        public IAppConfig AppConfig { private set; get; }

        /// <summary>
        /// ����ʵ��������keyΪ˳����
        /// </summary>
        public Dictionary<string, IObjectBase> PAddinObjectsLT { private set; get; }

        /// <summary>
        /// �������������
        /// </summary>
        public IReadOnlyDictionary<string, Type> PAddinTypeDic { get; private set; }

        /// <summary>
        /// �洢����״̬ʹ��
        /// </summary>
        public IRunningViewParam RunningViewParam { private set; get; }

        /// <summary>
        /// ��ͼ��Ϣ�б�
        /// </summary>
        public IDictionary<int, IRunningViewUnitParam> PViewsInfoDic { get; private set; }


        public void ReastData() { }


        private void ClassList_Load(object sender, EventArgs e)
        {

        }
    }
}