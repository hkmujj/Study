using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.IndexDescription;

namespace MMI.Facility.DataType.View.Control
{
    public interface IChildShareControl
    {
        AppIndexType Type {  get; }

        IDataPackage DataPackage {  get; }

        ProjectType ProjectType { get; }

        void Select(int index);
    }
}
