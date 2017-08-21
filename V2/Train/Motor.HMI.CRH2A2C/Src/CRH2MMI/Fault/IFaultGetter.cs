using System.Collections.ObjectModel;

namespace CRH2MMI.Fault
{
    partial class FaultMgr
    {
        public interface IViewFaultGetter: IFaultGetter
        {
            /// <summary>
            /// 是否有故障需要被激活
            /// </summary>
            bool HasFaultToBeActived { get; }

            /// <summary>
            /// 浏览结束
            /// </summary>
            void ViewFinishied();

            
        }

        public interface ICanDeleteFaultGetter : IFaultGetter
        {
            void DeleteFaultItem(FaultItemInfo faultItemInfo);

            /// <summary>
            /// 迭代路径
            /// </summary>
            ReadOnlyCollection<FaultItemInfo> IterPath { get; }

            /// <summary>
            /// 清除迭代路径
            /// </summary>
            void ClearIterPath();
        }

        public interface IFaultGetter
        {
            /// <summary>
            /// 当前迭代得到的故障信息
            /// </summary>
            FaultItemInfo CurrentShowFaultItemInfo { get; }

            /// <summary>
            /// 当前迭代位置
            /// </summary>
            int CurrentShowFaultIndex { set; get; }

            /// <summary>
            ///  是否存在故障
            /// </summary>
            bool HasFault { get; }

            /// <summary>
            /// 是否有上一个故障
            /// </summary>
            bool HasPreviousFault { get; }

            /// <summary>
            /// 是否有下一个故障
            /// </summary>
            bool HasNextFault { get; }

            /// <summary>
            /// 页面信息
            /// </summary>
            string PageInfo { get; }

            /// <summary>
            /// 重置当前显示的索引
            /// </summary>
            void ResetCurrentShowFaultIndex();

            /// <summary>
            /// 移动到上一个故障
            /// </summary>
            void GotoPreviousFault();

            /// <summary>
            /// 移动下下一个故障
            /// </summary>
            void GotoNextFault();

            /// <summary>
            /// fault mgr 变化 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="faultChangedArgs"></param>
            void OnFaultChangedEvent(object sender, FaultChangedArgs faultChangedArgs);

        }
    }
}
