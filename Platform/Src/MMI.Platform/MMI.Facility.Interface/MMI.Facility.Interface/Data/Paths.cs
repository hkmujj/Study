namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// ���̸���Ŀ¼�ļ�����
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
        /// APP����Ŀ¼
        /// </summary>
        public string RunPath { get; set; }


        /// <summary>
        /// �����ļ�Ŀ¼
        /// </summary>
        public string IniPath { get; set; }


        /// <summary>
        /// ���Ŀ¼
        /// </summary>
        public string AddinPath { get; set; }


        /// <summary>
        /// ��־Ŀ¼
        /// </summary>
        public string LogPath { get; set; }

    }
}