using System;
using ATP200C.PublicComponents;

namespace ATP200C.MainView.FunctionButton
{
    public partial class FunctionButtonView
    {
        #region :::::::::::::: 所有条件

        /// <summary>
        /// 默认不成立的条件
        /// </summary>
        /// <returns></returns>
        public bool ButtonConditional_0()
        {
            bool cd = false;
            return cd;
        }

        /// <summary>
        /// 默认成立的条件
        /// </summary>
        /// <returns></returns>
        public bool ButtonConditional_1()
        {
            bool cd = true;
            return cd;
        }

        #region :::::::::::: 1字打头的判断条件，主要判断按钮状态 ::::::::::::::::::

        /// <summary>
        /// 数据按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_101()
        {
            if (ViewJumped.CurrentViewId == 2)
            {
                return true;
            }
            if (OutBoolList[UIObj.OutBoolList[7]])
            //if (BoolList[UIObj.OutBoolList[7]])
            {
                return true; //按完载频
            }

            return false;
        }

        /// <summary>
        /// 模式按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_102()
        {
            if (ViewJumped.CurrentViewId == 2) return true;
            return false;
        }

        /// <summary>
        /// 载频按钮判断
        /// </summary>
        /// <returns>true 为可跳转</returns>
        public bool ButtonConditional_103()
        {
            //if (ViewJumped.CurrentViewId == 2) return true;
            //return false;
            return BoolList[65];
        }

        /// <summary>
        /// 等级按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_104()
        {
            if (ViewJumped.CurrentViewId == 2) return true;
            return false;
        }

        /// <summary>
        /// 音量亮度按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_105()
        {
            //if (ViewJumped.CurrentViewId == 2) return true;
            //if (OutBoolList[UIObj.OutBoolList[7]])
            //    //if (BoolList[UIObj.OutBoolList[7]])
            //{
            //    return true; //按完载频
            //}
            return false;
        }

        /// <summary>
        /// 启动按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_106()
        {
            if (ViewJumped.CurrentViewId == 2) return true;
            if (ATPMain.Instance.ControlModel != ControModelEnum.待机 && ATPMain.Instance.ControlModel != ControModelEnum.冒后) return true;
            return false;
        }

        /// <summary>
        /// 缓解按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_107()
        {
            if (ViewJumped.CurrentViewId == 2) return true;
            if (Convert.ToInt32(FloatList[UIObj.InFloatList[0]]) == 5) return false;
            return true;
        }

        /// <summary>
        /// 警惕按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_108()
        {
            if (ViewJumped.CurrentViewId == 2) return true;
            if (ATPMain.Instance.ControlModel == ControModelEnum.目视) return false;
            if (ATPMain.Instance.ControlModel == ControModelEnum.冒进) return false;
            if (ATPMain.Instance.ControlModel == ControModelEnum.引导) return false;
            return true;
        }

        /// <summary>
        /// 目视按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_121()
        {
            return !BoolList[UIObj.InBoolList[0]]; //目视允许
        }

        /// <summary>
        /// 调车按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_122()
        {
            if (ATPMain.Instance.ControlModel == ControModelEnum.调车) return true;
            return false;
        }

        /// <summary>
        /// 任务结束按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_123()
        {
            switch (ATPMain.Instance.ControlModel)
            {
                case ControModelEnum.待机:
                case ControModelEnum.调车:
                case ControModelEnum.冒后:
                case ControModelEnum.冒进:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 退出调车按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_124()
        {
            return ATPMain.Instance.ControlModel != ControModelEnum.调车;
        }

        /// <summary>
        /// 部分按钮判断
        /// </summary>
        /// <returns>true为按钮锁定灰色</returns>
        public bool ButtonConditional_125()
        {
            if (ATPMain.Instance.ControlModel == ControModelEnum.待机)
            {
                if (ATPMain.Instance.CTCSLevel == CTCSLevel.CTCS0 ||
                    ATPMain.Instance.CTCSLevel == CTCSLevel.CTCS2)
                    return false;
            }
            return true;
        }

        #endregion

        #region :::::::::::: 2字打头的判断条件，主要判断菜单跳转状态 ::::::::::::::::::

        /// <summary>
        /// 等级按钮按下后菜单跳转判断条件
        /// </summary>
        /// <returns></returns>
        public bool ButtonConditional_204()
        {
            if (ATPMain.Instance.ControlModel != ControModelEnum.待机) return false;
            return true;
        }

        #endregion

        #region :::::::::::: 3字打头的判断条件，主要判断发送数据状态 ::::::::::::::::::

        /// <summary>
        /// 启动按钮按下数据发送判断
        /// </summary>
        /// <returns>true表示可以发送数据</returns>
        public bool ButtonConditional_306()
        {
            if (OutBoolList[UIObj.OutBoolList[7]]) return true;

            return ATPMain.Instance.ControlModel == ControModelEnum.冒后;
        }

        #endregion

        #endregion

        /// <summary>
        /// 初始化所有条件判断
        /// </summary>
        private void InitAllConditionals()
        {
            AllConditionals.Add(0, ButtonConditional_0);
            AllConditionals.Add(1, ButtonConditional_1);
            AllConditionals.Add(101, ButtonConditional_101);
            AllConditionals.Add(102, ButtonConditional_102);
            AllConditionals.Add(103, ButtonConditional_103);
            AllConditionals.Add(104, ButtonConditional_104);
            AllConditionals.Add(105, ButtonConditional_105);
            AllConditionals.Add(106, ButtonConditional_106);
            AllConditionals.Add(107, ButtonConditional_107);
            AllConditionals.Add(108, ButtonConditional_108);
            AllConditionals.Add(121, ButtonConditional_121);
            AllConditionals.Add(122, ButtonConditional_122);
            AllConditionals.Add(123, ButtonConditional_123);
            AllConditionals.Add(124, ButtonConditional_124);
            AllConditionals.Add(125, ButtonConditional_125);
            AllConditionals.Add(204, ButtonConditional_204);
            AllConditionals.Add(306, ButtonConditional_306);
        }

        private void InitAllRectangles()
        {

        }
    }
}
