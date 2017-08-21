using System.Collections.Generic;
using System.Collections.ObjectModel;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Statues;
using Urban.Domain.TrainState.Model;

namespace Urban.Iran.HMI.Domain
{
    public class IranCar : CarBase
    {
        public IranCar()
        {
            CarBraking = new CarBraking()
                         {
                             Brakings = new ReadOnlyCollection<Braking>(new List<Braking>()
                                                                        {
                                                                            new Braking(BrakingState.Relase, BrakingLevel.Parking),
                                                                            new Braking(BrakingState.Relase, BrakingLevel.Emergency),
                                                                        })
                         };
        }
    }

    /// <summary>
    /// 带天线的
    /// </summary>
    public class IranCarWithPantograph : IranCar
    {
        public IranCarWithPantograph(int pantographCount = 1)
        {
            var patographs = new List<Pantograph>();
            for (int i = 0; i < pantographCount; i++)
            {
                patographs.Add(new Pantograph());
            }
            Pantographs = patographs.AsReadOnly();
        }
    }
}