namespace Engine.TCMS.HXD3D.Config
{
    public class GlobalParam
    {
        public static GlobalParam Instance = new GlobalParam();
        public ProjectConfig ProjectConfig { get; internal set; }
    }
}