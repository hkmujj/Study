using System;

namespace ES.Facility.PublicModule
{
    /// <summary>
    /// 该类用于更新注释
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public sealed class MemoInfoAttribute : Attribute
    {
        private string _userName;
        private string _note;
        private string _post;


        /// <summary>
        /// 注释信息
        /// </summary>
        /// <param name="userName">建立人</param>
        public MemoInfoAttribute(string userName)
        {
            _userName = userName;
            Ver = 1.0;
            Log = "无信息";
            _note = "无记录";
            _post = "无说明";
        }

        /// <summary>
        /// 注释信息
        /// </summary>
        /// <param name="userName">建立人</param>
        /// <param name="pPost">回复者</param>
        /// <param name="pLog">日志</param>
        /// <param name="pNote">备注</param>
        public MemoInfoAttribute(string userName, string pPost, string pLog, string pNote)
        {
            _userName = userName;
            Ver = 1.0;
            Log = pLog;
            _note = pNote;
            _post = pPost;
        }

        /// <summary>
        /// 注释信息
        /// </summary>
        /// <param name="userName">建立人</param>
        /// <param name="pNote">备注</param>
        public MemoInfoAttribute(string userName, string pNote)
        {
            _userName = userName;
            Ver = 1.0;
            Log = "无信息";
            _note = pNote;
            _post = "无说明";
        }

        /// <summary>
        /// 版本信息
        /// </summary>
        public double Ver { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Log { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
