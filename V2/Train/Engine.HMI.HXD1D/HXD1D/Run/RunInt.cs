using System.Drawing;

namespace HXD1D.Run
{
    /// <summary>
    /// 变化矩形条
    /// </summary>
    public class NeedChangeLength
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rect">所要画的矩形条的最大状态</param>
        /// <param name="maxValue">到顶的最大显示值</param>
        /// <param name="theDirection">矩形条的增长方向</param>
        /// <param name="needIncrease">是否需要有递增的状态</param>
        public NeedChangeLength(Rectangle rect, float maxValue, Rect_Rise_Direction theDirection, bool needIncrease)
        {
            _theRectangleF = rect;
            _theMaxValue = maxValue;
            _theRectDirection = theDirection;
            isNeedIncrease = needIncrease;

            //内部初始化

        }
        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="gs">绘图对象参数</param>
        /// <param name="currentValue">当前收到的要画的值</param>
        /// <param name="brush">画笔颜色</param>
        public void DrawRectangleType(Graphics gs, ref float currentValue, SolidBrush brush)
        {
            _theBrush = brush;
            _theScale = _theRectangleF.Height / _theMaxValue;
            float height = isNeedIncrease ? (ReturnTheVariation(ref currentValue) * _theScale) : currentValue * _theScale;
            gs.FillRectangle(_theBrush,
                new RectangleF(_theRectangleF.X + 2, _theRectangleF.Y + _theRectangleF.Height - height,
                    _theRectangleF.Width - 3, height));
            
        }
        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="gs">绘图对象参数</param>
        /// <param name="currentValue">当前收到的要画的值</param>
        /// <param name="brush">画笔颜色</param>
        public void DrawRectangle(Graphics gs, ref float currentValue, SolidBrush brush)
        {
            _theBrush = brush;
            float height;
            switch (_theRectDirection)
            {
                case Rect_Rise_Direction.上:
                    _theScale = _theRectangleF.Height / _theMaxValue;
                    height = isNeedIncrease ? (ReturnTheVariation(ref currentValue) * _theScale) : currentValue * _theScale;
                    gs.FillRectangle(_theBrush,
                        new RectangleF(_theRectangleF.X+2, _theRectangleF.Y + _theRectangleF.Height - height,
                            _theRectangleF.Width-3, height));
                    break;
                case Rect_Rise_Direction.下:
                    _theScale = _theRectangleF.Height / _theMaxValue;
                    height = isNeedIncrease ? (ReturnTheVariation(ref currentValue) * _theScale) : currentValue * _theScale;
                    gs.FillRectangle(_theBrush,
                        new RectangleF(_theRectangleF.X, _theRectangleF.Y, _theRectangleF.Width, height));
                    break;
                case Rect_Rise_Direction.左:
                    _theScale = _theRectangleF.Width / _theMaxValue;
                    height = isNeedIncrease ? (ReturnTheVariation(ref currentValue) * _theScale) : currentValue * _theScale;
                    gs.FillRectangle(_theBrush,
                        new RectangleF(_theRectangleF.X + _theRectangleF.Width - height, _theRectangleF.Y, height,
                            _theRectangleF.Height));
                    break;
                case Rect_Rise_Direction.右:
                    _theScale = _theRectangleF.Width / _theMaxValue;
                    height = isNeedIncrease ? (ReturnTheVariation(ref currentValue) * _theScale) : currentValue * _theScale;
                    gs.FillRectangle(_theBrush,
                        new RectangleF(_theRectangleF.X, _theRectangleF.Y, height, _theRectangleF.Height));
                    break;
            }
        }

        private float ReturnTheVariation(ref float theValue)
        {
            if (ViewValue > theValue)
            {
                if (ViewValue - _tmpStrpLength >= theValue)
                    tmpNeedChangeLength = -TmpStrpLength;
                else
                    tmpNeedChangeLength = theValue - ViewValue;
            }
            else if (ViewValue < theValue)
            {
                if (ViewValue + _tmpStrpLength <= theValue)
                    tmpNeedChangeLength = _tmpStrpLength;
                else
                    tmpNeedChangeLength = theValue - ViewValue;
            }
            else
                tmpNeedChangeLength = 0;

            ViewValue += tmpNeedChangeLength;

            return ViewValue;
        }

        //当前要画的高度值
        private float ViewValue;

        //需要增加的高度量
        private float tmpNeedChangeLength;

        //递增量
        private float _tmpStrpLength = 5;

        /// <summary>
        /// 递增量
        /// </summary>
        public float TmpStrpLength
        {
            get { return _tmpStrpLength; }
            set { _tmpStrpLength = value; }
        }

        //画笔
        private SolidBrush _theBrush;

        //所要画的矩形条满格状态大小
        private RectangleF _theRectangleF;

        //所要画的矩形条满格状态所表示的值
        private float _theMaxValue;

        //是否需要递增状态
        private bool isNeedIncrease;

        //矩形条递增方向
        private Rect_Rise_Direction _theRectDirection;

        //比例尺
        private float _theScale;
       

    }
    public enum Rect_Rise_Direction
    {
        上=0,
        下,
        左,
        右,
    }
}