using System.Text;

namespace Motor.ATP._300T.共用
{
    public class KeyBoard
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nKeyWords">要注册的键值</param>
        /// <param name="nTrickTime">按键最大滞留时间</param>
        public KeyBoard(string nKeyWords, int nTrickTime)
        {
            m_TimeOut = false;
            m_TrickTime = 5;
            InitData();
            m_DataBuffer.Append(nKeyWords);
            m_TrickTime = nTrickTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nTrickTime">按键最大滞留时间</param>
        public KeyBoard(int nTrickTime)
        {
            m_TimeOut = false;
            m_TrickTime = 5;
            InitData();
            m_TrickTime = nTrickTime;
        }

        /// <summary>
        /// 
        /// </summary>
        public KeyBoard()
        {


            InitData();
        }

        /// <summary>
        /// 注册键值
        /// </summary>
        /// <param name="nKeyWords">注册组数值</param>
        /// <param name="nKeyCode">当前值序号</param>
        public void RegisterKeyWords(string nKeyWords, int nKeyCode)
        {
            m_DataBuffer.Remove(0, m_DataBuffer.Length);
            m_DataBuffer.Append(nKeyWords);
            LastKey = nKeyCode;
            Timer = 0;
            DataId = 0;
            m_TimeOut = false;
        }

        /// <summary>
        /// 再次按下同一个按钮
        /// </summary>
        public void TurnTheSameBtn()
        {
            DataId = ++DataId % m_DataBuffer.Length;
            Timer = 0;
        }

        /// <summary>
        /// 获取当前输入值
        /// </summary>
        /// <param name="nClear">计时器是否清零</param>
        /// <returns></returns>
        public string GetKeyValue(bool nClear)
        {
            var key = string.Empty;
            if (DataId >= 0 && DataId < m_DataBuffer.Length)
            {
                key = m_DataBuffer.ToString().Substring(DataId, 1);
            }
            if (nClear)
            {
                m_TimeOut = false;
                LastKey = -1;
                DataId = 0;
                Timer = 0;
            }
            return key;
        }

        //初始化
        private void InitData()
        {            
            m_DataBuffer = new StringBuilder();
            Timer = 0;
            DataId = 0;
            LastKey = -1;
        }


        //按键滞留最大时间
        private readonly int m_TrickTime;

        //计时器
        /// <summary>
        /// 计时器
        /// </summary>
        public int Timer { get; private set; }

        //计时器超时
        private bool m_TimeOut;
        /// <summary>
        /// 计时器是否超时
        /// </summary>
        public bool TimeOut 
        {
            get
            {
                if (LastKey > -1)
                {
                    Timer++;
                    if (Timer > m_TrickTime)
                    {
                        Timer = 0;
                        m_TimeOut = true;
                    }
                    else
                    {
                        m_TimeOut = false;
                    }
                }
                return m_TimeOut; 
            } 
        }

        //序号
        /// <summary>
        /// 当前值序号
        /// </summary>
        public int DataId { get; private set; }

        //上次按键
        /// <summary>
        /// 返回上次按键编号
        /// </summary>
        public int LastKey { get; private set; }

        /// <summary>
        /// 键值容器
        /// </summary>
        private StringBuilder m_DataBuffer;
    }
}
