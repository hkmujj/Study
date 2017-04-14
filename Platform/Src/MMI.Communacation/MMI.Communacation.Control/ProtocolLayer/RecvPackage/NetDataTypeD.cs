using System.Runtime.InteropServices;
using MMI.Facility.Interface;

namespace MMI.Communacation.Control.ProtocolLayer.RecvPackage
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct NetDataTypeD
    {
        [FieldOffset(0)]
        public int Type;

        private const int Division = 1000;

        public void SetProjctType(ProjectType projectType)
        {
            Type = Type % Division + (int)projectType*Division;
        }

        public void SetDataPackageIndex(int index)
        {
            Type = (int)GetProjectType()*Division + index;
        }

        public ProjectType GetProjectType()
        {
            return (ProjectType) (Type/Division);
        }

        public int GetDataPackageIndex()
        {
            return (byte) (Type%Division);
        }
    }
}