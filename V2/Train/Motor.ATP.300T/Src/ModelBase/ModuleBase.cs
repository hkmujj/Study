using System.Drawing;

namespace Motor.ATP._300T.ModelBase
{
    public abstract class ModuleBase
    {
        protected ModuleBase(PointF topPoint, float scaling)
        {
            TopPoint = topPoint;
            Scaling = scaling;
        }

        public virtual void Draw(Graphics gs)
        {
            
        }

        /// <summary>
        /// 顶点坐标
        /// </summary>
        protected PointF TopPoint;

        /// <summary>
        /// 缩放比例
        /// </summary>
        protected float Scaling;
    }
}
