using System.Drawing;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Course;
using MMI.Facility.Interface.Event;
using MMI.Facility.Interface.Service;
using Motor.HMI.CRH3C380B.Config;
using Motor.HMI.CRH3C380B.Config.ConfigModel;

namespace Motor.HMI.CRH3C380B.Common
{
    public class GlobalParam
    {
        private ProjectConfig m_ProjectConfig;
        private DifferenceConfig m_TrainDiffConfig;
        private Rectangle m_FullScreenRectangle;

        public static GlobalParam Instance { private set; get; }

        public Rectangle FullScreenRectangle
        {
            get
            {
                if (m_FullScreenRectangle == Rectangle.Empty)
                {
                    m_FullScreenRectangle = new Rectangle(0, 0, 800, 600);
                }
                return m_FullScreenRectangle;
            }
        }

        public ICourseStartParameter CourseStartParameter { private set; get; }

        public ProjectType ProjectType
        {
            get { return ProjectConfig.ProjectType; }
        }

        public ProjectConfig ProjectConfig
        {
            get
            {
                if (m_ProjectConfig == null)
                {
                    LoadProjectConfig();
                }
                return m_ProjectConfig;
            }
        }

        public DifferenceConfig TrainDiffConfig
        {
            get
            {
                if (m_TrainDiffConfig == null)
                {
                    LoadTrainDiffConfig();
                }
                return m_TrainDiffConfig;
            }
        }

        private void LoadTrainDiffConfig()
        {
            m_TrainDiffConfig = DataSerialization.DeserializeFromXmlFile<DifferenceConfig>(DifferenceConfig.File);
        }

        static GlobalParam()
        {
            Instance = new GlobalParam();
            App.Current.ServiceManager.GetService<ICourseService>().CourseStateChanged += Instance.OnCourseStateChanged;
        }

        private void OnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            if (courseStateChangedArgs.CourseService.CurrentCourseState == CourseState.Started)
            {
                var ea = courseStateChangedArgs.Param as CourseStartEventArgs;
                if (ea != null)
                {
                    var data = ea.StartParam as ICourseStartParameter;
                    CourseStartParameter = data;
                }
            }
            else
            {
                CourseStartParameter = null;
            }
        }


        private void LoadProjectConfig()
        {
            m_ProjectConfig = DataSerialization.DeserializeFromXmlFile<ProjectConfig>(ProjectConfig.File);
        }
    }
}