using System.Collections.Generic;

namespace Engine.TCMS.HXD3D.Title.TopTitle
{
    public class TopTitleNameProvider 
    {
        private readonly List<ITopTitleNameFixder> m_NameFixder = new List<ITopTitleNameFixder>();

        private readonly Dictionary<int, string> m_ViewIdName;

        public TopTitleNameProvider()
        {
            m_ViewIdName = new Dictionary<int, string>
            {
                {1, "操作"},
                {21, "机车纵览"},
                {31, "隔离"},
                {32, "受电弓预选择"},
                {33, "距离计数器"},
                {41, "制动信息"},
                {42, "系统维护"},
                {43, "隔离阀状态"},
                {51, "列车供电"},
                {52, "驱动"},
                {53, "牵引/制动力"},
                {54, "开关状态"},
                {55, "过程数据-辅助"},
                {56, "过程数据-蓄电池"},
                {57, "网络控制"},
                {58, "过程数据-计数"},
                {60, "数据输入-请输入密码"},
                {61, "轮径"},
                {62, "轮缘润滑"},
                {63, "日期/时间设定"},
                {64, "其他设置"},
                {71, "主司控器试验"},
                {72, "启动试验"},
                {73, "零级位试验"},
                {74, "辅助电源试验"},
                {75, "显示灯试验"},
                {76, "无人警惕试验"},
                {77, "轮缘润滑试验"},
                {81, "无过滤"},
                {82, "激活事件"},
                {101, "司机诊断-司机需要确认"},
                {78, "顶轮试验"},
            };
        }

        /// <summary>
        /// 注册一个需要修改名字的
        /// </summary>
        /// <param name="nameFixder"></param>
        public void RegisteNameFixder(ITopTitleNameFixder nameFixder)
        {
            if (!m_NameFixder.Contains(nameFixder))
            {
                m_NameFixder.Add(nameFixder);

            }
        }

        /// <summary>
        /// 取消注册一个需要修改名字的
        /// </summary>
        /// <param name="nameFixder"></param>
        public void UnregisteNameFixder(ITopTitleNameFixder nameFixder)
        {
            m_NameFixder.Remove(nameFixder);
        }

        public string GetTitleName(int nParaA, int nParaB, float nParaC, string currentName = null)
        {
            if (m_ViewIdName.ContainsKey(nParaB))
            {
                currentName = m_ViewIdName[nParaB];
            }

            m_NameFixder.ForEach(e => currentName = e.UpdateTitleName(nParaA, nParaB, nParaC, currentName));

            return currentName;
        }
    }
}