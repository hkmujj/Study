namespace Motor.ATP._300D.Common
{
    public interface IButtonResponser
    {
        bool ResponseDown(ButtonType btnType);

        bool ResponseUp(ButtonType btnType);
    }
}