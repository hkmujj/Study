using System;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 表示与用户界面 (UI) 元素关联的空白或边距信息。
    /// </summary>
    public class Padding
    {
        /// <summary>
        /// PaddingChanged
        /// </summary>
        public event Action<Padding> PaddingChanged;



        /// 摘要:
        /// <summary>
        /// 提供没有空白的 System.Windows.Forms.Padding 对象。
        /// </summary>
        public static readonly Padding Empty = new Padding(0);

        private int m_Bottom;
        private int m_Left;
        private int m_Right;
        private int m_Top;

        /// <summary>
        ///         ///
        /// 摘要:
        ///     初始化 System.Windows.Forms.Padding 类的新实例，对所有边缘使用提供的空白大小。
        ///
        /// 参数:
        ///   all:
        ///     要用于所有边缘的空白的像素数目。
        /// </summary>
        /// <param name="all"></param>
        public Padding(int all)
        {
            Top = Bottom = Left = Right = all;
        }

        /// <summary>
        /// ///
        /// 摘要:
        ///     初始化 System.Windows.Forms.Padding 类的新实例，对每个边缘使用各自的空白大小。
        ///
        /// 参数:
        ///   left:
        ///     左边缘的空白大小（以像素为单位）。
        ///
        ///   top:
        ///     上边缘的空白大小（以像素为单位）。
        ///
        ///   right:
        ///     右边缘的空白大小（以像素为单位）。
        ///
        ///   bottom:
        ///     下边缘的空白大小（以像素为单位）。
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        public Padding(int left, int top, int right, int bottom)
        {
            Top = top;
            Left = left;
            Right = right;
            Bottom = bottom;
        }


        /// <summary>
        /// ///
        /// 摘要:
        ///     获取或设置下边缘的空白值。
        ///
        /// 返回结果:
        ///     下边缘的空白值（以像素为单位）。
        /// </summary>
        public int Bottom
        {
            get { return m_Bottom; }
            set
            {
                if (value == m_Bottom)
                {
                    return;
                }
                m_Bottom = value;
                OnPaddingChanged(this);
            }
        }

        /// <summary>
        /// ///
        /// 摘要:
        ///     获取左边缘和右边缘的组合空白。
        ///
        /// 返回结果:
        ///     获取 System.Windows.Forms.Padding.Left 和 System.Windows.Forms.Padding.Right
        ///     空白值的总和（以像素为单位）。
        /// </summary>
        public int Horizontal
        {
            get { return Left + Right; }
        }

        /// <summary>
        /// ///
        /// 摘要:
        ///     获取或设置左边缘的空白值。
        ///
        /// 返回结果:
        ///     左边缘的空白值（以像素为单位）。
        /// </summary>
        public int Left
        {
            get { return m_Left; }
            set
            {
                if (value == m_Left)
                {
                    return;
                }
                m_Left = value;
                OnPaddingChanged(this);
            }
        }

        /// <summary>
        /// ///
        /// 摘要:
        ///     获取或设置右边缘的空白值。
        ///
        /// 返回结果:
        ///     右边缘的空白值（以像素为单位）。
        /// </summary>
        public int Right
        {
            get { return m_Right; }
            set
            {
                if (value == m_Right)
                {
                    return;
                }
                m_Right = value;
                OnPaddingChanged(this);
            }
        }

        /// <summary>
        ///  ///
        /// 摘要:
        ///     获取或设置上边缘的空白值。
        ///
        /// 返回结果:
        ///     上边缘的空白值（以像素为单位）。
        /// </summary>
        public int Top
        {
            get { return m_Top; }
            set
            {
                if (value == m_Top)
                {
                    return;
                }
                m_Top = value;
                OnPaddingChanged(this);
            }
        }

        /// <summary>
        /// ///
        /// 摘要:
        ///     获取上边缘和下边缘的组合空白。
        ///
        /// 返回结果:
        ///     获取 System.Windows.Forms.Padding.Top 和 System.Windows.Forms.Padding.Bottom
        ///     空白值的总和（以像素为单位）。
        /// </summary>
        public int Vertical
        {
            get { return Top + Bottom; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void OnPaddingChanged(Padding obj)
        {
            Action<Padding> handler = PaddingChanged;
            if (handler != null)
            {
                handler(obj);
            }
        }

    }
}