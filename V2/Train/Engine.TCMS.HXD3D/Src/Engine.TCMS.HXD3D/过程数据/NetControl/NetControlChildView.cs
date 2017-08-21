using CommonUtil.Controls;

namespace Engine.TCMS.HXD3D.过程数据.NetControl
{
    public class NetControlChildView : CommonInnerControlBase
    {
        public NetControlChildView(ProcessNetControl parent, NetControlChildType childType)
        {
            ChildType = childType;
            Parent = parent;
        }

        public NetControlChildType ChildType { get; private set; }

        public ProcessNetControl Parent { get; private set; }

        public virtual void NavigateToThis()
        {

        }
    }
}