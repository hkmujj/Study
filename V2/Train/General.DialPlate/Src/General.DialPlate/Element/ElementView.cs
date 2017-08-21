using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Controls;
using MMI.Facility.Interface;

namespace General.DialPlate.Element
{
    public class ElementView : CommonInnerControlBase
    {
        public ElementModelWrapper ElementModelWrapper { private set; get; }

        private readonly Matrix m_TranslateMatrix;

        private readonly baseClass m_SrcObj;

        private readonly PointF m_ImageCenterPointF;

        private readonly bool m_NeedProtectedValue;

        private readonly Bitmap m_BufferImage;

        private float m_LastValue;

        private readonly Graphics m_Graphics;

        private float Value { get { return m_SrcObj.FloatList[ElementModelWrapper.ElementModel.LogicIndex]; } }

        public ElementView(baseClass srcObj, ElementModelWrapper elementModelWrapper, bool needProtectedValue = true)
        {
            Contract.Requires(srcObj != null);
            Contract.Requires(elementModelWrapper != null);
            m_LastValue = float.NaN;
            m_NeedProtectedValue = needProtectedValue;
            m_SrcObj = srcObj;
            ElementModelWrapper = elementModelWrapper;
            OutLineRectangle = Rectangle.Ceiling(elementModelWrapper.ElementModel.Region);

            m_BufferImage = (Bitmap)elementModelWrapper.Image.Clone();

            m_Graphics = Graphics.FromImage(m_BufferImage);

            m_ImageCenterPointF = new PointF((float)elementModelWrapper.Image.Width / 2, (float)elementModelWrapper.Image.Height / 2);

            if (elementModelWrapper.ElementModel.NeedRefresh())
            {
                RefreshAction = RefreshAngle;
            }
            m_TranslateMatrix = new Matrix();
        }

        private void RefreshAngle(object o)
        {
            Visible = true;
            if (Value >= ElementModelWrapper.ElementModel.MaxValue && !ElementModelWrapper.ElementModel.VisibleWhenLargerThanMax)
            {
                Visible = false;
                return;
            }
            if (Value < ElementModelWrapper.ElementModel.MinValue && !ElementModelWrapper.ElementModel.VisibleWhenLessThanMin)
            {
                Visible = false;
                return;
            }

            var model = ElementModelWrapper.ElementModel;
            var value = Value;

            if (Math.Abs(m_LastValue - value) < float.Epsilon)
            {
                return;
            }

            if (m_NeedProtectedValue)
            {
                value = Math.Max(Math.Min(model.MaxValue, value),
                    model.MinValue);
            }

            m_LastValue = value;

            m_TranslateMatrix.Reset();

            var angle =
                model.ConvertValueToDrawAngle(
                   value);
            m_TranslateMatrix.RotateAt(angle, m_ImageCenterPointF);

            m_Graphics.Clear(Color.Transparent);

            var g = m_Graphics;

            g.Clear(Color.Transparent);

            g.Transform = m_TranslateMatrix;

            g.DrawImage(ElementModelWrapper.Image, 0, 0, ElementModelWrapper.Image.Width, ElementModelWrapper.Image.Height);

        }

        public override void OnDraw(Graphics g)
        {
            g.DrawImage(m_BufferImage, ElementModelWrapper.ElementModel.Region);
        }
    }
}