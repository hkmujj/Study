using System.Drawing;

namespace CRH2MMI.DoorInfo
{
    internal class OpenColseDoor : DoorUnit
    {
        public OpenColseDoor(DoorInBoolModel model):base(model)
        {
            
        }

        public OpenColseDoor(DoorLocation doorNo, int trainNo) : base(doorNo, trainNo)
        {
        }

        public override void OnDraw(Graphics g)
        {
            var img  = DoorResource.Instance.GetDoorImage(State);
            g.DrawImage(img, OutLineRectangle);
        }
    }
}
