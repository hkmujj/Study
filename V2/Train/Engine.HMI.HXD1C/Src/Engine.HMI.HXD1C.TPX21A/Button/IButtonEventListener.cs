namespace Engine.HMI.HXD1C.TPX21A.Button
{
    public interface IButtonEventListener
    {
        bool ResponseMouseDown(ButtonName btn);


        bool ResponseMouseUp(ButtonName btn);
    }
}