namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 
    /// </summary>
    public static class DrvierSelectableItemExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IATP GetATP(this IDriverSelectableItem obj)
        {
            return obj.Parent.Parent.ATP;
        }
    }
}