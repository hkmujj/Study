using System.ComponentModel.Composition;
using Subway.WuHanLine6.Controller;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 烟火报警Model
    /// </summary>
    [Export]
    public class SmokeModel : ModelBase
    {
        private SmokeState m_SmokeF006;
        private SmokeState m_SmokeF005;
        private SmokeState m_SmokeF004;
        private SmokeState m_SmokeF003;
        private SmokeState m_SmokeF002;
        private SmokeState m_SmokeF001;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contrller"></param>
        [ImportingConstructor]
        public SmokeModel(SmokeContrller contrller)
        {
            Contrller = contrller;
            contrller.ViewModel = this;
        }

        /// <summary>
        /// Smoke控制类
        /// </summary>
        public SmokeContrller Contrller { get; private set; }

        /// <summary>
        /// 烟火状态1车
        /// </summary>
        public SmokeState SmokeF001
        {
            get { return m_SmokeF001; }
            set
            {
                if (value == m_SmokeF001)
                {
                    return;
                }
                m_SmokeF001 = value;
                RaisePropertyChanged(() => SmokeF001);
            }
        }

        /// <summary>
        /// 烟火状态2车
        /// </summary>
        public SmokeState SmokeF002
        {
            get { return m_SmokeF002; }
            set
            {
                if (value == m_SmokeF002)
                {
                    return;
                }
                m_SmokeF002 = value;
                RaisePropertyChanged(() => SmokeF002);
            }
        }

        /// <summary>
        /// 烟火状态3车
        /// </summary>
        public SmokeState SmokeF003
        {
            get { return m_SmokeF003; }
            set
            {
                if (value == m_SmokeF003)
                {
                    return;
                }
                m_SmokeF003 = value;
                RaisePropertyChanged(() => SmokeF003);
            }
        }

        /// <summary>
        /// 烟火状态4车
        /// </summary>
        public SmokeState SmokeF004
        {
            get { return m_SmokeF004; }
            set
            {
                if (value == m_SmokeF004)
                {
                    return;
                }
                m_SmokeF004 = value;
                RaisePropertyChanged(() => SmokeF004);
            }
        }

        /// <summary>
        /// 烟火状态5车
        /// </summary>
        public SmokeState SmokeF005
        {
            get { return m_SmokeF005; }
            set
            {
                if (value == m_SmokeF005)
                {
                    return;
                }
                m_SmokeF005 = value;
                RaisePropertyChanged(() => SmokeF005);
            }
        }

        /// <summary>
        /// 烟火状态6车
        /// </summary>
        public SmokeState SmokeF006
        {
            get { return m_SmokeF006; }
            set
            {
                if (value == m_SmokeF006)
                {
                    return;
                }
                m_SmokeF006 = value;
                RaisePropertyChanged(() => SmokeF006);
            }
        }
    }
}