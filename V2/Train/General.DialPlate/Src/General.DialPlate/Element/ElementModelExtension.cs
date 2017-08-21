namespace General.DialPlate.Element
{
    public static class ElementModelExtension
    {
        public static bool NeedRefresh(this IElementModel element)
        {
            return element.LogicIndex != int.MaxValue;
        }
    }
}