namespace Urban.NanJing.NingTian.DDU.ͨѶ״̬
{
    /// <summary>
    /// ����������ͨѶ��Ϣģ��info
    /// �����ˣ�����
    /// ����ʱ�䣺2014-07-24
    /// </summary>
    public class MessageModeInfo
    {
        /// <summary>
        /// ��ȡ�����ñ������
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ��ȡ��������������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public MessageModeInfo(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}