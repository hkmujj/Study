namespace Urban.GuiYang.DDU.Extension
{
    public static class FloatExtention
    {
        public static float ToFloat(this float value)
        {
            if (value.ToString().Contains("."))
            {
                return value;
            }
            return (int)value;
        }
    }
}
