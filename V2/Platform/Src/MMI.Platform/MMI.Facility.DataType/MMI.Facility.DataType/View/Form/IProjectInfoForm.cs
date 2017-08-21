using MMI.Facility.DataType.View.Control;

// ReSharper disable once CheckNamespace


namespace MMI.Facility.PublicUI.Interface
{
    public interface IProjectInfoControl : IHelpControl
    {
        string AppName { get; }

        void InitPorjectList();

        void InitViewsList();

        void InitClassList();

        void InitObjectList();
    }
}
