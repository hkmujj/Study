namespace Engine.HMI.SS3B.Interface
{
    public interface IContentViewNavigater
    {
        void NavigateTo(string viewType);
        void NextPage();
        void LastPage();
        void GetCurren();
    }
}