
using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Win32.SafeHandles;

namespace Motor.ATP._300D
{
    public class UserActionDataInterpreter
    {
        private readonly Timer m_Timer;

        public ButtonType CurrentType { get; private set; }

        public ButtonType LastType { private set; get; }

        private static readonly Dictionary<ButtonType, char[]> m_BtnDataDictionary;

        private bool m_IsInputChar;

        private int m_CurrentDataIndex;
        private bool m_InputEnded;

        public bool InputEnded
        {
            private set
            {
                m_InputEnded = value;
                OnInputEndedChanged();
            }
            get { return m_InputEnded; }
        }

        public char LastInterpreterValue { get; private set; }

        public event Action InputEndedChanged;


        static UserActionDataInterpreter()
        {
            m_BtnDataDictionary = new Dictionary<ButtonType, char[]>()
                                  {
                                      { ButtonType.B1, new char[] { '1', char.MinValue } },
                                      { ButtonType.B2, new char[] { '2', 'A', 'B', 'C', } },
                                      { ButtonType.B3, new char[] { '3', 'D', 'E', 'F', } },
                                      { ButtonType.B4, new char[] { '4', 'G', 'H', 'I', } },
                                      { ButtonType.B5, new char[] { '5', 'J', 'K', 'L', } },
                                      { ButtonType.B6, new char[] { '6', 'M', 'N', 'O', } },
                                      { ButtonType.B7, new char[] { '7', 'P', 'Q', 'R', 'S' } },
                                      { ButtonType.B8, new char[] { '8', 'T', 'U', 'V',  } },
                                      { ButtonType.B9, new char[] { '9', 'W', 'X', 'Y', 'Z' } },
                                      { ButtonType.B0, new char[] { '0', char.MinValue } },
                                      { ButtonType.BSwitch, new char[] { char.MinValue } },
                                  };
        }

        public UserActionDataInterpreter()
        {
            m_Timer = new Timer(3000);
            m_Timer.Elapsed += TimerOnElapsed;
            m_CurrentDataIndex = 0;
            LastInterpreterValue = char.MinValue;
            InputEnded = false;
        }

        public void KnowInputEnded()
        {
            m_InputEnded = false;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            m_Timer.Stop();
            CurrentType = ButtonType.None;
            InputEnded = true;
            m_CurrentDataIndex = m_IsInputChar ? 1 : 0;
        }

        public char Interpreter(ButtonType btnType)
        {
            LastType = CurrentType;

            CurrentType = btnType;

            if (btnType > ButtonType.BSwitch )
            {
                return char.MinValue;
            }

            if (btnType == ButtonType.None)
            {
                return char.MinValue;
            }

            LastInterpreterValue = char.MinValue;
            InputEnded = false;

            if (btnType == ButtonType.BSwitch)
            {
                m_IsInputChar = !m_IsInputChar;
                m_CurrentDataIndex = 0;
            }
            else if (m_IsInputChar)
            {
                MoveToNext(btnType);
                m_Timer.Stop();
                m_Timer.Start();
            }
            else
            {
                m_CurrentDataIndex = 0;
                InputEnded = true;
            }

            if (!m_IsInputChar)
            {
                m_Timer.Stop();
            }

            LastInterpreterValue = m_BtnDataDictionary[btnType][m_CurrentDataIndex];
            return LastInterpreterValue;
        }

        private void MoveToNext(ButtonType btnType)
        {
            if (LastType == btnType)
            {
                m_CurrentDataIndex = (m_CurrentDataIndex + 1) % (m_BtnDataDictionary[btnType].Length);
                if (m_CurrentDataIndex == 0)
                {
                    m_CurrentDataIndex += 1;
                }
                InputEnded = false;
            }
            else
            {
                m_CurrentDataIndex = 1;
                InputEnded = true;
            }
        }

        public void Clear()
        {
            m_Timer.Stop();
            m_IsInputChar = false;
            CurrentType = ButtonType.None;
            InputEnded = true;
        }


        protected void OnInputEndedChanged()
        {
            Action handler = InputEndedChanged;
            if (handler != null)
            {
                handler();
            }
        }
    }
}