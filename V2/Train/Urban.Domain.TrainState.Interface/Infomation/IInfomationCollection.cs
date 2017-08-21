using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime;

namespace Urban.Domain.TrainState.Interface.Infomation
{
    public interface IInfomationCollection : INotifyCollectionChanged
    {

        // 摘要: 
        //     获取包含在 System.Collections.ObjectModel.ReadOnlyCollection<T> 实例中的元素数。
        //
        // 返回结果: 
        //     包含在 System.Collections.ObjectModel.ReadOnlyCollection<T> 实例中的元素数。
        int Count { get; }

        // 摘要: 
        //     获取指定索引处的元素。
        //
        // 参数: 
        //   index:
        //     要获取的元素的从零开始的索引。
        //
        // 返回结果: 
        //     指定索引处的元素。
        //
        // 异常: 
        //   System.ArgumentOutOfRangeException:
        //     index 小于零。- 或 -index 等于或大于 System.Collections.ObjectModel.ReadOnlyCollection<T>.Count。
        IInfomationItem this[int index] { get; }

        // 摘要: 
        //     确定某元素是否在 System.Collections.ObjectModel.ReadOnlyCollection<T> 中。
        //
        // 参数: 
        //   value:
        //     要在 System.Collections.ObjectModel.ReadOnlyCollection<T> 中定位的对象。对于引用类型，该值可以为
        //     null。
        //
        // 返回结果: 
        //     如果在 System.Collections.ObjectModel.ReadOnlyCollection<T> 中找到 value，则为 true；否则为
        //     false。
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        bool Contains(IInfomationItem value);
        //
        // 摘要: 
        //     从目标数组的指定索引处开始将整个 System.Collections.ObjectModel.ReadOnlyCollection<T> 复制到兼容的一维
        //     System.Array。
        //
        // 参数: 
        //   array:
        //     作为从 System.Collections.ObjectModel.ReadOnlyCollection<T> 复制的元素的目标位置的一维 System.Array。System.Array
        //     必须具有从零开始的索引。
        //
        //   index:
        //     array 中从零开始的索引，将在此处开始复制。
        //
        // 异常: 
        //   System.ArgumentNullException:
        //     array 为 null。
        //
        //   System.ArgumentOutOfRangeException:
        //     index 小于零。
        //
        //   System.ArgumentException:
        //     源 System.Collections.ObjectModel.ReadOnlyCollection<T> 中的元素数目大于从 index 到目标
        //     array 末尾之间的可用空间。
        void CopyTo(IInfomationItem[] array, int index);
        
        // 摘要: 
        //     返回循环访问 System.Collections.ObjectModel.ReadOnlyCollection<T> 的枚举器。
        //
        // 返回结果: 
        //     用于 System.Collections.ObjectModel.ReadOnlyCollection<T> 的 System.Collections.Generic.IEnumerator<T>。
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        IEnumerator<IInfomationItem> GetEnumerator();
        //
        // 摘要: 
        //     搜索指定的对象，并返回整个 System.Collections.ObjectModel.ReadOnlyCollection<T> 中第一个匹配项的从零开始的索引。
        //
        // 参数: 
        //   value:
        //     要在 System.Collections.Generic.List<T> 中定位的对象。对于引用类型，该值可以为 null。
        //
        // 返回结果: 
        //     如果在整个 System.Collections.ObjectModel.ReadOnlyCollection<T> 中找到 item 的第一个匹配项，则为该项的从零开始的索引；否则为
        //     -1。
        int IndexOf(IInfomationItem value);
    }
}