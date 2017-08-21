using System;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.Model.Domain.Constant;

namespace Subway.TCMS.LanZhou.Model.Domain.Car
{
    [DebuggerDisplay("GroupId={GroupId},CarId={CarId},Name={Name}")]
    public class CarModel : NotificationObject
    {
        [DebuggerStepThrough]
        public CarModel(int groupId, int carId, string name)
        {
            GroupId = groupId;
            CarId = carId;
            Name = name;
            TrainBodyViewData = new TrainBodyViewData();
            RunningViewData = new RunningViewData();
            TrainStatusDatas = new TrainStatusDatas();
            AirConditionControl = new AirConditionControl();
        }

        public string Name { get; private set; }

        public int CarId { get; private set; }

        /// <summary>
        /// 编组号
        /// </summary>
        public int GroupId { get; private set; }

        public DoorModel DoorModel { get; set; }

        public CarPilothouseStatus PilothouseStatus { get; set; }
   
        public CarBowStatus CarBowStatus { get; set; }

        public TrainBodyViewData TrainBodyViewData { get;  set; }

        public RunningViewData RunningViewData {  set; get; }

        public Lazy<LineInformationDatas> LineInformationDatas { set; get; }

        public TrainStatusDatas TrainStatusDatas { get;  set; }

        public AirConditionControl AirConditionControl { get; set; }

    }
}