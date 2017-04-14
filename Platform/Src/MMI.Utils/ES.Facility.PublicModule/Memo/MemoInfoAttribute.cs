using System;

namespace ES.Facility.PublicModule
{
    /// <summary>
    /// �������ڸ���ע��
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public sealed class MemoInfoAttribute : Attribute
    {
        private string _userName;
        private string _note;
        private string _post;


        /// <summary>
        /// ע����Ϣ
        /// </summary>
        /// <param name="userName">������</param>
        public MemoInfoAttribute(string userName)
        {
            _userName = userName;
            Ver = 1.0;
            Log = "����Ϣ";
            _note = "�޼�¼";
            _post = "��˵��";
        }

        /// <summary>
        /// ע����Ϣ
        /// </summary>
        /// <param name="userName">������</param>
        /// <param name="pPost">�ظ���</param>
        /// <param name="pLog">��־</param>
        /// <param name="pNote">��ע</param>
        public MemoInfoAttribute(string userName, string pPost, string pLog, string pNote)
        {
            _userName = userName;
            Ver = 1.0;
            Log = pLog;
            _note = pNote;
            _post = pPost;
        }

        /// <summary>
        /// ע����Ϣ
        /// </summary>
        /// <param name="userName">������</param>
        /// <param name="pNote">��ע</param>
        public MemoInfoAttribute(string userName, string pNote)
        {
            _userName = userName;
            Ver = 1.0;
            Log = "����Ϣ";
            _note = pNote;
            _post = "��˵��";
        }

        /// <summary>
        /// �汾��Ϣ
        /// </summary>
        public double Ver { get; set; }

        /// <summary>
        /// ��ע��Ϣ
        /// </summary>
        public string Log { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
