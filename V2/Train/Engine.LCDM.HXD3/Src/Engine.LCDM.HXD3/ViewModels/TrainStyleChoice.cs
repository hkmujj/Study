namespace Engine.LCDM.HXD3.ViewModels
{
    public class TrainStyleChoice
    {
        public TrainStyleChoice(string one,string two)
        {
            Passenger = one;
            Goods = two;
        }

        public TrainStyleChoice()
        {
        }

        //客车对应的区域名称
        public string Passenger { get; set; }
        //货车对应的区域名称
        public string Goods { get; set; }

    }
}