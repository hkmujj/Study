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

        //�ͳ���Ӧ����������
        public string Passenger { get; set; }
        //������Ӧ����������
        public string Goods { get; set; }

    }
}