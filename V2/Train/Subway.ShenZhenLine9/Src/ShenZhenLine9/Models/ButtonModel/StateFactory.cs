using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Subway.ShenZhenLine9.Config;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Models.ButtonModel
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IStateFactory))]
    public class StateFactory : IStateFactory
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public StateFactory()
        {
            m_StateDictionary = new Dictionary<IStateKey, IState>();
        }
        private readonly Dictionary<IStateKey, IState> m_StateDictionary;
        /// <summary>
        /// 获取State
        /// </summary>
        /// <param name="key">StateKey</param>
        /// <returns></returns>
        public IState GetOrCreateState(IStateKey key)
        {
            if (m_StateDictionary.ContainsKey(key))
            {
                return m_StateDictionary[key];
            }
            return null;
        }

        /// <summary>
        /// 获取State
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IState GetOrCreateState(string key)
        {
            if (IsContain(key))
            {
                return GetState(key);
            }

            CrateAllState();

            return GetState(key);

        }

        /// <summary>
        /// 获取所有State
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IState> GetAllState()
        {
            return m_StateDictionary.Values.ToList();
        }

        private IState GetState(string key)
        {
            return (from state in m_StateDictionary where state.Key.Key == key select state.Value).FirstOrDefault();
        }

        private bool IsContain(string key)
        {
            return m_StateDictionary.Any(state => state.Key.Key == key);
        }

        private void CrateAllState()
        {
            foreach (StateConfig config in GlobalParam.Instance.StateConfigs.AllSatteConfig)
            {
                CreateState(config.Key);
            }
        }
        private void CreateState(string key)
        {
            var s = GlobalParam.Instance.StateConfigs.AllSatteConfig.FirstOrDefault(f => f.Key == key);
            if (s != null)
            {
                var satetKey = new StateKey(s.Key, s.View);

                var state = new State(satetKey)
                {
                    TitleName = s.TitleName,
                    Btn01 = CreateBtn(s.Btn01),
                    Btn02 = CreateBtn(s.Btn02),
                    Btn03 = CreateBtn(s.Btn03),
                    Btn04 = CreateBtn(s.Btn04),
                    Btn05 = CreateBtn(s.Btn05),
                    Btn06 = CreateBtn(s.Btn06),
                    Btn07 = CreateBtn(s.Btn07),
                    Btn08 = CreateBtn(s.Btn08),
                    Btn09 = CreateBtn(s.Btn09),
                    Btn10 = CreateBtn(s.Btn10)
                };
                m_StateDictionary.Add(satetKey, state);
            }
        }

        private static IBtnItem CreateBtn(BtnItemConfig config)
        {
            return new BtnItem(config.Response) { Text = config.Text };
        }
    }
}