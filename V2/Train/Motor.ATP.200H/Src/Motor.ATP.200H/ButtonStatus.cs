using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 功能描述 BJ_BottomButtonStatus类 
    /// 底部硬件按钮状态 实时获取底部硬件按钮的最新状态
    /// 创建人：袁 凯    
    /// 创建时间：2013-10-29
    /// </summary>
    // ReSharper disable once InconsistentNaming
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class ButtonStatus : ShowTypeEffectBaseClass
    {
        /// <summary>
        /// 重启标志
        /// </summary>
        private bool m_IsReStarBlackScreen;

        public static bool IsBlackScreen { set; get; }
      
        /// <summary>
        /// 右侧F1至F8 按钮是否按下
        /// </summary>
        public static bool[] m_IsRightButtonDown = new bool[8];

        public static bool[] ManualIsRightButtonDown { private set; get; }

        /// <summary>
        /// 右侧F1至F8 按钮是否弹起
        /// </summary>
        public static bool[] m_IsRightButtonUp = new bool[8];

        public static bool[] ManualIsRightButtonUp { private set; get; }

        /// <summary>
        /// 底部数字按钮是否按下
        /// </summary>
        public static bool[] m_IsBottomButtonDown = new bool[11];

        public static bool[] ManualIsBottomButtonDown { private set; get; }

        /// <summary>
        /// 底部数字按钮是否弹起
        /// </summary>
        public static bool[] m_IsBottomButtonUp = new bool[11];

        public static bool[] ManualIsBottomButtonUp { private set; get; }

        public static void ClearManaulDown()
        {
            for (int i = 0; i < ManualIsBottomButtonDown.Length; i++)
            {
                ManualIsBottomButtonDown[i] = false;
            }
            for (int i = 0; i < ManualIsRightButtonDown.Length; i++)
            {
                ManualIsRightButtonDown[i] = false;
            }
        }

        public static void ClearManaulUp()
        {
            for (int i = 0; i < ManualIsBottomButtonUp.Length; i++)
            {
                ManualIsBottomButtonUp[i] = false;
            }
            for (int i = 0; i < ManualIsRightButtonUp.Length; i++)
            {
                ManualIsRightButtonUp[i] = false;
            }
        }

        static ButtonStatus()
        {
            ManualIsRightButtonDown = new bool[8];
            ManualIsRightButtonUp = new bool[8];
            ManualIsBottomButtonDown = new bool[11];
            ManualIsBottomButtonUp = new bool[11];
        }

        public override string GetInfo()
        {
            return "硬件按钮状态";
        }

        protected override void Initalize()
        {
            UIObj.InBoolList.Clear();
            UIObj.InBoolList.AddRange(GetInBoolIndexs());
        }

        IEnumerable<int> GetInBoolIndexs()
        {
            yield return this.GetInboolIndex(InBoolKeys.Inb按键F1);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键F2);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键F3);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键F4);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键F5);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键F6);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键F7);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键F8);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键调车1);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键目视2);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键机信3);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键启动4);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键缓解5);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键6);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键7);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键8);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键9);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键0);
            yield return this.GetInboolIndex(InBoolKeys.Inb按键警惕);
        }

        public override void paint(Graphics g)
        {
            RefreshShowType();

            GetValue();
        }

        /// <summary>
        /// 刷新按键信息
        /// </summary>
        private void GetValue()
        {
            //if (ShowType == ShowType.Normal)
            {
                for (int index = 0; index < 8; index++)
                {
                    //按下
                    if ((BoolList[UIObj.InBoolList[index]] && !BoolOldList[UIObj.InBoolList[index]])
                        || ManualIsRightButtonDown[index])
                    {
                        m_IsRightButtonDown[index] = true;
                    }
                    else
                    {
                        m_IsRightButtonDown[index] = false;
                    }

                    //弹起
                    if ((BoolOldList[UIObj.InBoolList[index]] && !BoolList[UIObj.InBoolList[index]])
                        || ManualIsRightButtonUp[index])
                    {
                        m_IsRightButtonUp[index] = true;
                    }
                    else
                    {
                        m_IsRightButtonUp[index] = false;
                    }
                }

                
            }
            for (int index = 0; index < 11; index++)
            {
                //按下
                if ((BoolList[UIObj.InBoolList[index + 8]] && !BoolOldList[UIObj.InBoolList[index + 8]])
                    || ManualIsBottomButtonDown[index])
                {
                    m_IsBottomButtonDown[index] = true;
                }
                else
                {
                    m_IsBottomButtonDown[index] = false;
                }

                //弹起
                if ((BoolOldList[UIObj.InBoolList[index + 8]] && !BoolList[UIObj.InBoolList[index + 8]])
                    || ManualIsBottomButtonUp[index])
                {
                    m_IsBottomButtonUp[index] = true;
                }
                else
                {
                    m_IsBottomButtonUp[index] = false;
                }
            }
            //更新右侧F1至F8按钮状态
            ClearManaulDown();

            ClearManaulUp();

            //读取黑屏标志
            m_IsReStarBlackScreen = BoolList[this.GetInboolIndex(InBoolKeys.InbATP屏重启标志)];
            IsBlackScreen = BoolList[this.GetInboolIndex(InBoolKeys.InbATP屏亮屏标志)];

            if (!m_IsReStarBlackScreen)
            {
                append_postCmd(CmdType.ChangePage, 10, 0, 0);
            }
        }
    }
}

