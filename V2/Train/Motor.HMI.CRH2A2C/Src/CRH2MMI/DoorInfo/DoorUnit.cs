using CommonUtil.Controls;

namespace CRH2MMI.DoorInfo
{
    internal abstract class DoorUnit : CommonInnerControlBase
    {
        protected DoorUnit(DoorLocation doorNo, int trainNo)
        {
            TrainNo = trainNo;
            DoorNo = doorNo;
        }

        protected DoorUnit(DoorInBoolModel model)
        {
            TrainNo = model.CarNo;
            DoorNo = model.DoorLocation;
            DoorInBoolKeyModel = new DoorInBoolKeyModel()
            {
                CarNo = model.CarNo,
                Location = model.DoorLocation,
                OperType = model.Type
            };

            RefreshAction = o =>
            {
                var door = (DoorUnit)o;
                door.State =
                    door.Resource.GetDoorStates(door.DoorInBoolKeyModel);
            };
        }

        public int TrainNo { private set; get; }

        /// <summary>
        /// 门的状态
        /// </summary>
        public DoorState State { set; get; }

        /// <summary>
        /// 门的位数
        /// </summary>
        public DoorLocation DoorNo { private set; get; }


        public DoorInBoolKeyModel DoorInBoolKeyModel { private set; get; }

        public DoorResource Resource { set; get; }

    }
}
