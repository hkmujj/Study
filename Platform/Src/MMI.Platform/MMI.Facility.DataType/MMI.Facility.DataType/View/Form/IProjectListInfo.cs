using System.Collections.ObjectModel;

// ReSharper disable once CheckNamespace
namespace MMI.Facility.PublicUI.Interface
{
    public interface IProjectListInfoForm : IHelpForm
    {
        ReadOnlyCollection<IProjectInfoControl> ProjectInfoForms { get; }

        void InitalizeChildren();
    }
}
