namespace ES.Facility.PublicModule.IO
{
    public static class IniSerialize
    {
        public static T Deserialize<T>(string file) where T : new ()
        {
            var infos = typeof (T).GetCustomAttributes(true);

            return new T();
        }
    }
}
