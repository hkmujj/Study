namespace Motor.ATP.Domain.Interface.UserAction
{
    public static class DrvierSelectableItemExtension
    {
        public static IATP GetATP(this IDriverSelectableItem obj)
        {
            return obj.Parent.Parent.ATP;
        }
    }
}