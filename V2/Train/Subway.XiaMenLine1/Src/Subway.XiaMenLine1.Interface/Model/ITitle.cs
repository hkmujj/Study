namespace Subway.XiaMenLine1.Interface.Model
{
    public interface ITitle : IViewModel
    {
        string CurrenStation { get; set; }
        string NextStatuin { get; set; }
        string EndStation { get; set; }
        double NetPressValue { get; }
        double BatteryValue { get; }
        string TitleName { get; set; }
 
    }
}