using System.Drawing;

namespace CRH2MMI.Common.View.Train
{
    static class CarFactory
    {
        public static Car CreateCar(CarConfig carConfig, Point location)
        {
            if (carConfig.IsHeadCar)
            {
                return new HeadCar()
                {
                    CarConfig = carConfig,
                    Direction = carConfig.HeadDirection,
                    Location = location
                };
            }
            return new Car()
            {
                Location = location,
                CarConfig = carConfig,
            };
        }
    }
}
