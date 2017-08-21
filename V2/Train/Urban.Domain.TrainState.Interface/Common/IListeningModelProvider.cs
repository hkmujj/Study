using System.Collections.ObjectModel;

namespace Urban.Domain.TrainState.Interface.Common
{
    /// <summary>
    /// 收听通信数据模型
    /// </summary>
    public interface IListeningModelProvider
    {
        ReadOnlyCollection<IListeningModel> ListeningModelCollection { get; }

    }
}