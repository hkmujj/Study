using MMI.Facility.Interface;

namespace MMI.Facility.DataType.Extension
{
    public static class ProjectTypeHelper
    {

        /// <summary>
        ///  是否为有效的 ProjectType
        /// </summary>
        /// <param name="projectType"></param>
        /// <returns></returns>
        public static bool IsValidateProjectType(this ProjectType projectType)
        {
            return projectType <= GetMaxProjectType() && projectType >= GetMinProjectType();
        }

        /// <summary>
        /// 最小值
        /// </summary>
        /// <returns></returns>
        public static ProjectType GetMinProjectType()
        {
            return ProjectType.Signal;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        /// <returns></returns>
        public static ProjectType GetMaxProjectType()
        {
            return ProjectType.DynamicMap;
        }
    }
}
