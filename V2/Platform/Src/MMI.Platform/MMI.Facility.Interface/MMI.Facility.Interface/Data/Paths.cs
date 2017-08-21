namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 工程各个目录文件名称
    /// </summary>
    public class Paths
    {

        /// <summary>
        /// 
        /// </summary>
        public Paths()
        {
            LogPath = string.Empty;
            AddinPath = string.Empty;
            IniPath = string.Empty;
            RunPath = string.Empty;
            AppPath = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AppPath { get; set; }


        /// <summary>
        /// APP运行目录
        /// </summary>
        public string RunPath { get; set; }


        /// <summary>
        /// 配置文件目录
        /// </summary>
        public string IniPath { get; set; }


        /// <summary>
        /// 插件目录
        /// </summary>
        public string AddinPath { get; set; }


        /// <summary>
        /// 日志目录
        /// </summary>
        public string LogPath { get; set; }

    }
}