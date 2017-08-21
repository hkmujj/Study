using System.ComponentModel;

namespace Engine.TPX21F.HXN5B.Model.Interface
{
    public interface IPassableItem : INotifyPropertyChanged
    {
        IPassableItemConfig ItemConfig { get; }

        bool Passed { set; get; }
    }

    public interface IPassableItemConfig
    {
        string Content { get; }

        string IndexBool { get; }
    }
}