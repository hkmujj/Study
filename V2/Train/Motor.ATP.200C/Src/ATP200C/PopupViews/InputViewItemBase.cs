using System;
using System.Drawing;
using System.Threading;
using ATP200C.MainView;
using ATP200C.PublicComponents;
using CommonUtil.Controls;

namespace ATP200C.PopupViews
{
    public class InputViewItemBase : PopupViewItem
    {
        private int m_CurrentInputIndex;

        private const int CheckInterval = 3000;

        private UserActionData m_LastUseData;

        private bool m_NeedGotoNext;

        /// <summary>
        /// 重置当前输入状态
        /// </summary>
        private Timer m_ResetTimer;

        private readonly object m_LockObj;

        /// <summary>
        /// 输入字母
        /// </summary>
        private bool m_IsInputChar;

        private int m_SeriesPressCount;
        
        protected readonly GDIRectText m_ConsText;

        protected GDIRectText m_InputContent;

        protected char[] m_InputContentArray;

        protected int SeriesPressCount
        {
            get { return m_SeriesPressCount; }
            set
            {
                lock (m_LockObj)
                {
                    m_SeriesPressCount = value;
                }
            }
        }

        protected int CurrentInputIndex
        {
            set
            {
                m_CurrentInputIndex = value;
                if (m_CurrentInputIndex < 0)
                {
                    m_CurrentInputIndex = 0;
                }
                else if (m_CurrentInputIndex > m_InputContentArray.Length - 1)
                {
                    m_CurrentInputIndex = m_InputContentArray.Length - 1;
                }
            }
            get { return m_CurrentInputIndex; }
        }

        private const char Unkown = '─';
        private const char Inputin = ' ';

        public InputViewItemBase()
        {
            m_LockObj = new object();
            m_IsInputChar = false;
            m_SeriesPressCount = -1;

            m_ConsText = new GDIRectText()
                         {
                             OutLineRectangle = new Rectangle(m_OutLineRectangle.Left + 36, m_OutLineRectangle.Top + 76, 200, 25),
                             Text = "司机号:",
                             TextColor = Color.White
                         };

            m_InputContent = new GDIRectText()
                             {
                                 OutLineRectangle = new Rectangle(m_ConsText.OutLineRectangle.Left, m_ConsText.OutLineRectangle.Bottom + 4, 200, 25),
                                 Text ="-------",
                                 TextColor = Color.White,
                                 DrawFont = FontsItems.Font18_You_B
                             };

            m_InputContentArray = m_InputContent.Text.ToCharArray();

            CurrentInputIndex = 0;
            m_ResetTimer = new Timer(state =>
            {
                SeriesPressCount = -1;
                lock (m_LockObj)
                {
                    CurrentInputIndex++;
                }
                m_ResetTimer.Change(Timeout.Infinite, CheckInterval);
            },
                null,
                -1,
                CheckInterval);
        }

        public override void Reset()
        {
            this.CurrentInputIndex = 0;
            m_InputContent.Text = "-------";
            m_InputContentArray = m_InputContent.Text.ToCharArray();
        }

        public override void OnDraw(Graphics g)
        {
            m_ConsText.OnDraw(g);

            m_InputContent.OnDraw(g);
        }

        public override void ActionCommand(ATPBaseClass baseClass, UserActionCommand cmd)
        {
            switch (cmd)
            {
                case UserActionCommand.Unknown :
                    break;
                case UserActionCommand.Up :
                    break;
                case UserActionCommand.Down :
                    break;
                case UserActionCommand.Left :
                    CurrentInputIndex = (CurrentInputIndex + m_InputContentArray.Length - 1) % m_InputContentArray.Length;
                    break;
                case UserActionCommand.Right :
                    CurrentInputIndex = ( CurrentInputIndex + 1 ) % m_InputContentArray.Length;
                    break;
                case UserActionCommand.Delete :
                    m_InputContentArray[CurrentInputIndex] = '-';
                    break;
                case UserActionCommand.Ok :
                    break;
                case UserActionCommand.Cancel :
                    break;
                default :
                    throw new ArgumentOutOfRangeException("cmd");
            }

            UpdateTextView();
        }

        public override void ActionData(UserActionData data)
        {
            m_InputContentArray[CurrentInputIndex] = GetUserData(data);
            UpdateTextView();
            if (m_NeedGotoNext)
            {
                CurrentInputIndex++;
            }
        }

        private void UpdateTextView()
        {
            m_InputContent.Text = new string(m_InputContentArray);
        }

        private char GetUserData(UserActionData data)
        {
            return m_IsInputChar ? GetInputChar(data) : GetInputNum(data);
        }

        private char GetInputNum(UserActionData data)
        {
            if (data < UserActionData.Num0)
            {
                m_NeedGotoNext = true;
                return (char)((int)data - UserActionData.Num1 + '1');
            }
            if (data == UserActionData.Num0)
            {
                m_NeedGotoNext = true;
                return '0';
            }
            m_IsInputChar = true;
            m_NeedGotoNext = false;
            return m_InputContentArray[CurrentInputIndex];
        }

        private char GetInputChar(UserActionData data)
        {
            if (data == UserActionData.Switch)
            {
                m_IsInputChar = false;
                m_ResetTimer.Change(Timeout.Infinite, CheckInterval);
                m_NeedGotoNext = false;
                return m_InputContentArray[CurrentInputIndex];
            }

            if (m_SeriesPressCount == -1 || m_LastUseData != data)
            {
                m_SeriesPressCount = 0;
            }

            var c = data.GetCharOfIndex(m_SeriesPressCount);

            m_SeriesPressCount++;

            m_LastUseData = data;
            m_ResetTimer.Change(CheckInterval, CheckInterval);
            if (c == ' ')
            {
                m_NeedGotoNext = false;
                return m_InputContentArray[CurrentInputIndex];
            }
            m_NeedGotoNext = false;
            var s=c.ToString().ToUpper();

            return s.ToCharArray()[0];
        }
    }
}

