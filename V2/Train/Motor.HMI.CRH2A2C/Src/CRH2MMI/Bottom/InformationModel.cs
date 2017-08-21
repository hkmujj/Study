using System.Diagnostics;

namespace CRH2MMI.Bottom
{
    class InformationModel
    {
        [DebuggerStepThrough]
        public InformationModel()
        {
            ClearType = InformationClearType.Manual;
            InformationType = InformationType.Info;
            Location = InformationLocation.Down;
        }

        [DebuggerStepThrough]
        public InformationModel(string information)
            : this()
        {
            Information = information;
        }

        public InformationType InformationType { set; get; }

        public InformationClearType ClearType { set; get; }

        public InformationLocation Location { set; get; }

        public string Information { set; get; }
    }
}
