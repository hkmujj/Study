using System;
using System.Collections.Generic;
using System.Linq;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Control
{
    /// <summary>
    /// 司机输入数据的解释器
    /// </summary>
    public class DriverInputDataInterpreter : IDriverInputInterpreter
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<UserActionType, string> m_InterpreteredDictionary;

        // ReSharper disable once NotAccessedField.Local
        private static readonly Dictionary<UserActionType, List<string>> m_CharDictionary;

        private UserActionType m_LastActionType;

        private DateTime m_LastActionTime;

        private string m_LastString;

        public TimeSpan CharSpan { set; get; }

        static DriverInputDataInterpreter()
        {
            m_InterpreteredDictionary = new Dictionary<UserActionType, string>
            {
                {UserActionType.B1, "1"},
                {UserActionType.B2, "2"},
                {UserActionType.B3, "3"},
                {UserActionType.B4, "4"},
                {UserActionType.B5, "5"},
                {UserActionType.B6, "6"},
                {UserActionType.B7, "7"},
                {UserActionType.B8, "8"},
                {UserActionType.B9, "9"},
                {UserActionType.B10, "0"}
            };
            m_CharDictionary = new Dictionary<UserActionType, List<string>>()
            {
                {UserActionType.B1, new List<string> {""}},
                {UserActionType.B2, new List<string> {"A", "B", "C"}},
                {UserActionType.B3, new List<string> {"D", "E", "F"}},
                {UserActionType.B4, new List<string> {"G", "H", "I"}},
                {UserActionType.B5, new List<string> {"J", "K", "L"}},
                {UserActionType.B6, new List<string> {"M", "N", "O"}},
                {UserActionType.B7, new List<string> {"P", "Q", "R", "S"}},
                {UserActionType.B8, new List<string> {"T", "U", "V"}},
                {UserActionType.B9, new List<string> {"W", "X", "Y", "Z"}},
                {UserActionType.B10, new List<string> {""}}
            };

        }

        public DriverInputDataInterpreter(bool canInputChar = true)
        {
            m_LastActionTime = DateTime.Now;
            CanInputChar = canInputChar;

            Reset();
        }

        /// <summary>
        /// 是否能输入字符
        /// </summary>
        public bool CanInputChar { private set; get; }

        public DriverInputState InputState { get; private set; }

        /// <summary>
        /// 重置状态
        /// </summary>
        public void Reset()
        {
            InputState = DriverInputState.Number;
            CharSpan = new TimeSpan(0, 0, 3);
        }

        public DriverInputInterpreterResult Interpreter(UserActionType actionType)
        {
            if (!actionType.IsInputData())
            {
                return DriverInputInterpreterResult.InvalidateResult;
            }

            if (actionType == UserActionType.B11)
            {
                if (InputState == DriverInputState.Number && CanInputChar)
                {
                    InputState = DriverInputState.Character;
                }
                else
                {
                    InputState = DriverInputState.Number;
                }
                return DriverInputInterpreterResult.ControlResult;
            }

            switch (InputState)
            {
                case DriverInputState.Number:
                    if (m_InterpreteredDictionary.ContainsKey(actionType))
                    {
                        return new DriverInputInterpreterResult(m_InterpreteredDictionary[actionType]);
                    }
                    break;
                case DriverInputState.Character:
                    return InterpreterChar(actionType);

                case DriverInputState.Other:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            return DriverInputInterpreterResult.InvalidateResult;
        }

        private DriverInputInterpreterResult InterpreterChar(UserActionType actionType)
        {
            var last = m_LastActionType;
            var lastTime = m_LastActionTime;
            m_LastActionType = actionType;
            m_LastActionTime = DateTime.Now;
            if (actionType == last)
            {
                if (DateTime.Now - lastTime <= CharSpan)
                {
                    var idx = (m_CharDictionary[actionType].IndexOf(m_LastString) + 1)%
                              m_CharDictionary[actionType].Count;
                    m_LastString = m_CharDictionary[actionType][idx];
                    return new DriverInputInterpreterResult(m_LastString, DriverInputInterpreterResult.InputType.Replace);
                }
            }
            m_LastString = m_CharDictionary[actionType].First();
            return new DriverInputInterpreterResult(m_LastString);
        }
    }
}