using System.Drawing;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultRectTextBehavierStrategy : IRectTextBehavierStrategy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public DefaultRectTextBehavierStrategy(GDIRectText control)
        {
            Control = control;
        }

        /// <summary>
        /// �����ؼ� 
        /// </summary>
        public GDIRectText Control { get; private set; }

        /// <summary>
        /// ��갴��
        /// </summary>
        /// <param name="point"></param>
        public virtual bool OnMouseDown(Point point)
        {
            // nothing
            return false;
        }

        /// <summary>
        /// ��갴�º���
        /// </summary>
        /// <param name="point"></param>
        public virtual bool OnMouseUp(Point point)
        {
            // nothing
            return false;
        }

        /// <summary>
        /// ��ͼ
        /// </summary>
        /// <param name="g"></param>
        public virtual void OnDraw(Graphics g)
        {
            if (!Control.Visible)
            {
                return;
            }

            // ����
            Control.DrawBk(g);

            // �ı�
            Control.DrawText(g);

            // ����
            Control.DrawOutline(g);
        }
    }
}