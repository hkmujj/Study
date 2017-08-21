using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Interface.Statues;
using Urban.Domain.TrainState.Model;
using Urban.NanJing.NingTian.DDU.Resource.Internal;

namespace Urban.NanJing.NingTian.DDU.Flash.Model.Train.Car
{
    public class NingTianCar : CarBase, ITypeProvider<CarType>
    {
        public new CarType Type { get; private set; }

        object ITypeProvider.Type
        {
            get { return Type; }
        }

        public NingTianCar(CarType type)
        {
            Type = type;
            CarName = type.ToString();
            switch (type)
            {
                case CarType.A1:
                    CarNumber = 0;
                    break;
                case CarType.B1:
                    CarNumber = 1;
                    break;
                case CarType.B2:
                    CarNumber = 2;
                    break;
                case CarType.A2:
                    CarNumber = 3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }

            InitalizePower();

            InitalizeDoors();

            InitalizePansengers();
        }

        private void InitalizePansengers()
        {
            const string fomart = "{0}车乘客信息{1}{2}";
            PassengerCollection = new ReadOnlyCollection<Passenger>(new List<Passenger>()
            {
                new Passenger()
                {
                    ListeningModelCollection = new List<ListeningModel>()
                    {
                        new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, "1", "正常"))),
                        new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, "1", "故障"))),
                        new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, "1", "未知"))),

                        //new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, "2", "正常"))),
                        //new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, "2", "故障"))),
                        //new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, "2", "未知"))),
                    }
                }
            });
            foreach (var passenger in PassengerCollection)
            {
                passenger.Updating += PassengerOnUpdating;
            }
        }

        private void PassengerOnUpdating(Passenger target, IUpdateParam updateParam)
        {
            target.PassengerState = PassengerState.Normal;
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[0].Name))
            {
                target.PassengerState = PassengerState.Normal;
            }
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[1].Name))
            {
                target.PassengerState = PassengerState.Fault;
            }
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[2].Name))
            {
                target.PassengerState = PassengerState.Unkown;
            }
        }

        private void InitalizeDoors()
        {
            Doors = Enumerable.Range(1, 8).Select(s => new Door()
            {
                Name = s.ToString(),
                Identity = s,
            }).ToList().AsReadOnly();
            foreach (var dr in Doors)
            {
                const string fomart = "{0}车客室门{1}{2}";

                dr.ListeningModelCollection = new List<ListeningModel>()
                {
                    new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, dr.Name, "关闭状态"))),
                    new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, dr.Name, "隔离状态"))),
                    new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, dr.Name, "打开状态"))),
                    new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, dr.Name, "故障状态"))),
                    new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, dr.Name, "障碍物"))),
                    new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, dr.Name, "解锁状态"))),
                    new ListeningModel(InBoolKeys.ResourceManager.GetString(string.Format(fomart, Type, dr.Name, "未知状态"))),
                };

                dr.Updating += DoorOnUpdating;
            }
        }

        private void DoorOnUpdating(Door target, IUpdateParam updateParam)
        {
            target.DoorState = DoorState.Close;
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[0].Name))
            {
                target.DoorState = DoorState.Close;
            }
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[1].Name))
            {
                target.DoorState = DoorState.CutOut;
            }
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[2].Name))
            {
                target.DoorState = DoorState.Open;
            }
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[3].Name))
            {
                target.DoorState = DoorState.Fault;
            }
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[4].Name))
            {
                target.DoorState = DoorState.Obstacle;
            }
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[5].Name))
            {
                target.DoorState = DoorState.Unlock;
            }
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[6].Name))
            {
                target.DoorState = DoorState.Unkown;
            }
        }


        private void InitalizePower()
        {
            switch (Type)
            {
                case CarType.A1:
                    Power.PowerItems =
                        new ReadOnlyCollection<CarPowerItem>(new List<CarPowerItem>()
                        {
                            new CarPowerItem(CarPowerType.AC)
                            {
                                ListeningModelCollection =
                                    new List<ListeningModel>()
                                    {
                                        new ListeningModel(InBoolKeys.A1车电源AC无故障),
                                        new ListeningModel(InBoolKeys.A1车电源AC未知),
                                        new ListeningModel(InBoolKeys.A1车电源AC故障),
                                    }
                            },
                            new CarPowerItem(CarPowerType.DC)
                            {
                                ListeningModelCollection =
                                    new List<ListeningModel>()
                                    {
                                        new ListeningModel(InBoolKeys.A1车电源DC无故障),
                                        new ListeningModel(InBoolKeys.A1车电源DC未知),
                                        new ListeningModel(InBoolKeys.A1车电源DC故障),
                                    }
                            }
                        });
                    break;
                case CarType.B1:
                    break;
                case CarType.B2:
                    break;
                case CarType.A2:
                    Power.PowerItems =
                        new ReadOnlyCollection<CarPowerItem>(new List<CarPowerItem>()
                        {
                            new CarPowerItem(CarPowerType.AC)
                            {
                                ListeningModelCollection =
                                    new List<ListeningModel>()
                                    {
                                        new ListeningModel(InBoolKeys.A2车电源AC无故障),
                                        new ListeningModel(InBoolKeys.A2车电源AC未知),
                                        new ListeningModel(InBoolKeys.A2车电源AC故障),
                                    }
                            },
                            new CarPowerItem(CarPowerType.DC)
                            {
                                ListeningModelCollection =
                                    new List<ListeningModel>()
                                    {
                                        new ListeningModel(InBoolKeys.A2车电源DC无故障),
                                        new ListeningModel(InBoolKeys.A2车电源DC未知),
                                        new ListeningModel(InBoolKeys.A2车电源DC故障),
                                    }
                            }
                        });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (var it in Power.PowerItems)
            {
                it.Updating += PowerItemOnUpdating;
            }
        }

        private void PowerItemOnUpdating(CarPowerItem target, IUpdateParam updateParam)
        {
            target.State = CarPowerState.Normal;
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[0].Name))
            {
                target.State = CarPowerState.Normal;
            }
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[1].Name))
            {
                target.State = CarPowerState.Unkown;
            }
            if (updateParam.GetInBoolAt(target.ListeningModelCollection[2].Name))
            {
                target.State = CarPowerState.Fault;
            }
        }
    }
}