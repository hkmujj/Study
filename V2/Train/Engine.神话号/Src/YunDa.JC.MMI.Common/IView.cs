namespace YunDa.JC.MMI.Common
{
    public interface IView
    {
        int ID { get; set; }
        bool IsShow { get; set; }
        ViewManager ViewManger { get; set; }
    }
}