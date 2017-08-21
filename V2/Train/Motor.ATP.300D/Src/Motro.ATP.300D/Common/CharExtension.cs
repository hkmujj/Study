namespace Motor.ATP._300D.Common
{
    public static class CharExtension
    {
        public static bool IsNumber(this char c)
        {
            return c >= '0' && c <= '9';
        }

        public static bool IsLower(this char c)
        {
            return c >= 'a' && c <= 'z';
        }

        public static bool IsUpper(this char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        public static bool IsCharacter(this char c)
        {
            return c.IsLower() || c.IsUpper();
        }
    }
}