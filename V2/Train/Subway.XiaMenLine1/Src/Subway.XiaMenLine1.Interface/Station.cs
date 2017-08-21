namespace Subway.XiaMenLine1.Interface
{
    public class Station : IStation
    {
        public Station(int logicNum, int number, string stationName)
        {
            LogicNum = logicNum;
            Number = number;
            StationName = stationName;
        }

        public int LogicNum { get; private set; }
        public int Number { get; private set; }
        public string StationName { get; private set; }

    }
}