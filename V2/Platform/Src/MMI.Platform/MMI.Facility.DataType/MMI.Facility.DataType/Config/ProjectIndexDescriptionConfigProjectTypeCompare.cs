using System.Collections.Generic;

namespace MMI.Facility.DataType.Config
{
    public class ProjectIndexDescriptionConfigProjectTypeCompare : IEqualityComparer<ProjectIndexDescriptionConfig>
    {
        public static readonly ProjectIndexDescriptionConfigProjectTypeCompare Instance = new ProjectIndexDescriptionConfigProjectTypeCompare();

        private ProjectIndexDescriptionConfigProjectTypeCompare()
        {
            
        }

        public bool Equals(ProjectIndexDescriptionConfig x, ProjectIndexDescriptionConfig y)
        {
            return x.ProjectType.Equals(y.ProjectType);
        }

        public int GetHashCode(ProjectIndexDescriptionConfig obj)
        {
            return obj.ProjectType.GetHashCode();
        }
    }
}