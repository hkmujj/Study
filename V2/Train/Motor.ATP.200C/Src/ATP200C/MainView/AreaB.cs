using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ATP200C.Common;
using ATP200C.Common.Controls;
using ATP200C.Domain;
using ATP200C.PublicComponents;
using ATP200C.Resource;
using ATP200C.Resource.Images;
using ATPComControl.SpeedArc;
using MMI.Facility.Interface.Attribute;
using ATPComControl;
using CommonUtil.Controls;
using SpeedPointer = ATPComControl.SpeedPointer;

namespace ATP200C.MainView
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class AreaB : ATPBaseClass
    {
        private readonly List<RectangleF> m_RectFs = new List<RectangleF>();
        private SpeedPointer m_SpeedPointers;
        private Image m_ImagePointer;
        private SpeedCircle m_SpeedCircles;
        private readonly TrainSpeedInfo m_TrainSpeedInfo = new TrainSpeedInfo();

        private OrderType m_OrderType;

        private EBrakeIsolated m_EBrakeIsolated;

        /// <summary>
        /// 目标速度曲线
        /// </summary>
        private SpeedCircle m_TheVtargetCircle;

        /// <summary>
        /// 允许速度曲线
        /// </summary>
        private SpeedCircle m_TheVpermCircle;

        /// <summary>
        /// 警报制动速度曲线
        /// </summary>
        private SpeedCircle m_TheVintCircle;

        private SpeedCircle m_TheEmergCircel;

        private ISpeedConverter m_SpeedConverter;

        private ISpeedConverter m_SpeedArcConverter;

        private const float m_HookWidth = 5;

        private readonly Font m_SpeedTextFont = new Font("宋体", 20, FontStyle.Bold);

        public override string GetInfo()
        {
            return "速度信息区";
        }

        public override bool Initalize()
        {
            m_RectFs.Add(new RectangleF(80, 15, 318, 318)); //表盘
            m_RectFs.Add(new RectangleF(70, 5, 338, 338)); //圆弧

#if false
            // 占两格的
            m_EBrakeIsolated = new EBrakeIsolated()
            {
                OutLineRectangle = new Rectangle(ATP200CCommon.RectB[3].Location, new Size(ATP200CCommon.RectB[3].Width + ATP200CCommon.RectB[4].Width, 30)),
                BackColorVisible = false,
                TextFormat =
                    new StringFormat() {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center}
            };
#else
            // 占一格的
            m_EBrakeIsolated = new EBrakeIsolated()
            {
                OutLineRectangle =
                    new Rectangle(ATP200CCommon.RectB[4].Location, new Size(ATP200CCommon.RectB[4].Width, 30)),
                BackColorVisible = false,
                DrawFont = new Font("Arial", 6),
                TextFormat =
                    new StringFormat() {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center}
            };
#endif

            m_SpeedConverter = new Atp300CSpeedAngleConverter();
            m_SpeedArcConverter = new Atp300CSpeedAngleAcrConverter();

            m_ImagePointer = ImageResource.zhizhenbai;
            m_SpeedPointers = new SpeedPointer(225.0f,
                -45f,
                -45f,
                400,
                0,
                m_RectFs[0],
                new PointF(m_RectFs[0].X + m_RectFs[0].Width / 2, m_RectFs[0].Y + m_RectFs[0].Width / 2),
                m_ImagePointer,
                m_SpeedConverter);

            m_SpeedCircles = new SpeedCircle(45, -225f, 400.0f, 0.0f, m_RectFs[1], m_SpeedArcConverter);

            m_TheVtargetCircle = new SpeedCircle(45, -225f, 400.0f, 0.0f, m_RectFs[1], m_SpeedArcConverter);

            m_TheVpermCircle = new SpeedCircle(45, -225f, 400.0f, 0.0f, m_RectFs[1], m_SpeedArcConverter);

            m_TheVintCircle = new SpeedCircle(45, -225f, 400.0f, 0.0f, m_RectFs[1], m_SpeedArcConverter);

            m_TheEmergCircel = new SpeedCircle(45, -225f, 400.0f, 0.0f, m_RectFs[1], m_SpeedArcConverter);

            return true;
        }

        public override void paint(Graphics g)
        {
            UpdateStates();

            g.DrawImage(ImageResource.速度表盘, m_RectFs[0]);

            DrawSpeedPoint(g);

            DrawArc(g);

            DrawSpeedText(g);

            DrawOrderRegion(g);
        }

        private void DrawOrderRegion(Graphics g)
        {
            if (m_OrderType == OrderType.EmerBrakeIsolated)
            {
                m_EBrakeIsolated.OnDraw(g);
            }
        }

        private void UpdateStates()
        {
            m_OrderType =
                (OrderType) (int) FloatList[IndexDescription.InFloatDescriptionDictionary[InFloatKeys.B4区命令图标]];
        }

        private void DrawSpeedText(Graphics g)
        {
            g.DrawString(m_TrainSpeedInfo.Vtrain.ToString("0"), m_SpeedTextFont, Brushes.Black, m_RectFs[1], FontsItems.TheAlignment(FontRelated.居中));
        }

        private void DrawArc(Graphics g)
        {
            // TODO add 侧线发车判断
            if (ATPMain.Instance.ControlModel != ControModelEnum.完全 || ATPMain.Instance.CTCSLevel == CTCSLevel.CTCS0)
            {
                if (ATPMain.Instance.ControlModel == ControModelEnum.LKJ)
                {
                    return;
                }

                //if (m_TrainSpeedInfo.Vtrain <= m_TrainSpeedInfo.Vperm)
                {
                    m_TheVpermCircle.PaintCircleHook(g,
                        m_TrainSpeedInfo.Vperm,
                        m_HookWidth,
                        TrainInfoColor.Gray.GetBoldPen(),
                        TrainSpeedInfoExtension.GetBoldInside());
                    return;
                }
            }

            if (Math.Abs(m_TrainSpeedInfo.Vtrain) < float.Epsilon && ATPMain.Instance.ControlModel == ControModelEnum.完全)
            {
                m_TheVpermCircle.PaintCircle(g, 0, m_TrainSpeedInfo.Vperm, m_TrainSpeedInfo.VpermCircleColor.GetNormalPen());
                m_TheVpermCircle.PaintCircleHook(g,
                    m_TrainSpeedInfo.Vperm,
                    m_HookWidth,
                    m_TrainSpeedInfo.VpermCircleColor.GetBoldPen(),
                    TrainSpeedInfoExtension.GetBoldInside());
                return;
            }

            if (m_TrainSpeedInfo.Vtrain > m_TrainSpeedInfo.VEmergent)
            {
                DrawArcLagerthanEmergence(g);
            }
                // 当前速度 在 常用制动速度和紧急制动速度间
            else if (m_TrainSpeedInfo.Vtrain > m_TrainSpeedInfo.Vint)
            {
                DrawArcBetweenServiceBrakeAndEmergence(g);
            }
            else if (m_TrainSpeedInfo.Vtrain > m_TrainSpeedInfo.Vperm)
            {
                DrawArcBetweenPermAndServiceBrake(g);
            }
            else
            {
                m_TheVpermCircle.PaintCircle(g, m_TrainSpeedInfo.Vperm, m_TrainSpeedInfo.Vtarget, m_TrainSpeedInfo.VpermCircleColor.GetNormalPen());
                m_TheVtargetCircle.PaintCircle(g, m_TrainSpeedInfo.Vtarget, 0, m_TrainSpeedInfo.VtargetCircleColor.GetNormalPen());
                m_TheVpermCircle.PaintCircleHook(g,
                    m_TrainSpeedInfo.Vperm,
                    m_HookWidth,
                    m_TrainSpeedInfo.VpermCircleColor.GetBoldPen(),
                    TrainSpeedInfoExtension.GetBoldInside());
            }
        }

        private void DrawArcBetweenPermAndServiceBrake(Graphics g)
        {
            if (ATPMain.Instance.BrakeLevel.HasMaxServiceBrake())
            {
                m_TheVpermCircle.PaintCircle(g, m_TrainSpeedInfo.Vperm, m_TrainSpeedInfo.Vtarget, m_TrainSpeedInfo.VpermCircleColor.GetNormalPen());
                m_TheVtargetCircle.PaintCircle(g, m_TrainSpeedInfo.Vtarget, 0, m_TrainSpeedInfo.VtargetCircleColor.GetNormalPen());

                m_TheVpermCircle.PaintCircle(g, m_TrainSpeedInfo.Vtrain, m_TrainSpeedInfo.Vperm, m_TrainSpeedInfo.VEmergentColor.GetNormalPen());
                m_TheVpermCircle.PaintCircleHook(g,
                    Math.Max(m_TrainSpeedInfo.Vperm, m_TrainSpeedInfo.Vtrain),
                    m_HookWidth,
                    m_TrainSpeedInfo.VEmergentColor.GetBoldPen(),
                    TrainSpeedInfoExtension.GetBoldInside());
            }
            else
            {
                m_TheVpermCircle.PaintCircle(g, m_TrainSpeedInfo.Vperm, m_TrainSpeedInfo.Vtarget, m_TrainSpeedInfo.VpermCircleColor.GetNormalPen());
                m_TheVtargetCircle.PaintCircle(g, m_TrainSpeedInfo.Vtarget, 0, m_TrainSpeedInfo.VtargetCircleColor.GetNormalPen());

                m_TheVpermCircle.PaintCircle(g, m_TrainSpeedInfo.Vtrain, m_TrainSpeedInfo.Vperm, m_TrainSpeedInfo.VintCircleColor.GetNormalPen());
                m_TheVpermCircle.PaintCircleHook(g,
                    Math.Max(m_TrainSpeedInfo.Vperm, m_TrainSpeedInfo.Vtrain),
                    m_HookWidth,
                    m_TrainSpeedInfo.VintCircleColor.GetBoldPen(),
                    TrainSpeedInfoExtension.GetBoldInside());
            }
        }

        private void DrawArcBetweenServiceBrakeAndEmergence(Graphics g)
        {
            m_TheVintCircle.PaintCircle(g, m_TrainSpeedInfo.Vint, m_TrainSpeedInfo.Vperm, m_TrainSpeedInfo.VintCircleColor.GetNormalPen());
            m_TheVpermCircle.PaintCircle(g, m_TrainSpeedInfo.Vperm, m_TrainSpeedInfo.Vtarget, m_TrainSpeedInfo.VpermCircleColor.GetNormalPen());
            m_TheVtargetCircle.PaintCircle(g, m_TrainSpeedInfo.Vtarget, 0, m_TrainSpeedInfo.VtargetCircleColor.GetNormalPen());

            m_TheVintCircle.PaintCircle(g, m_TrainSpeedInfo.Vtrain, m_TrainSpeedInfo.Vint, m_TrainSpeedInfo.VintCircleColor.GetNormalPen());
            m_TheVintCircle.PaintCircleHook(g,
                Math.Max(m_TrainSpeedInfo.Vint, m_TrainSpeedInfo.Vtrain),
                m_HookWidth,
                m_TrainSpeedInfo.VEmergentColor.GetBoldPen(),
                TrainSpeedInfoExtension.GetBoldInside());
        }

        private void DrawArcLagerthanEmergence(Graphics g)
        {
            if (ATPMain.Instance.BrakeLevel.HasEmergenceBrake())
            {
                m_TheEmergCircel.PaintCircle(g, Math.Max(m_TrainSpeedInfo.VEmergent, m_TrainSpeedInfo.Vtrain), 0, m_TrainSpeedInfo.VEmergentColor.GetNormalPen());
                m_TheEmergCircel.PaintCircleHook(g,
                    Math.Max(m_TrainSpeedInfo.VEmergent, m_TrainSpeedInfo.Vtrain),
                    m_HookWidth,
                    m_TrainSpeedInfo.VEmergentColor.GetBoldPen(),
                    TrainSpeedInfoExtension.GetBoldInside());
            }
            else
            {
                m_TheEmergCircel.PaintCircle(g,
                    Math.Max(m_TrainSpeedInfo.VEmergent, m_TrainSpeedInfo.Vtrain),
                    m_TrainSpeedInfo.Vperm,
                    m_TrainSpeedInfo.VEmergentColor.GetNormalPen());
                m_TheVpermCircle.PaintCircle(g, m_TrainSpeedInfo.Vperm, m_TrainSpeedInfo.Vtarget, m_TrainSpeedInfo.VpermCircleColor.GetNormalPen());
                m_TheVtargetCircle.PaintCircle(g, m_TrainSpeedInfo.Vtarget, 0, m_TrainSpeedInfo.VtargetCircleColor.GetNormalPen());
                m_TheEmergCircel.PaintCircleHook(g,
                    Math.Max(m_TrainSpeedInfo.VEmergent, m_TrainSpeedInfo.Vtrain),
                    m_HookWidth,
                    m_TrainSpeedInfo.VEmergentColor.GetBoldPen(),
                    TrainSpeedInfoExtension.GetBoldInside());
            }
        }

        private void DrawSpeedPoint(Graphics g)
        {
            m_SpeedPointers.PaintPointerColor(g, m_ImagePointer, m_TrainSpeedInfo.Vtrain);

            m_TrainSpeedInfo.Vtrain = FloatList[UIObj.InFloatList[1]];
            m_TrainSpeedInfo.Vtarget = FloatList[UIObj.InFloatList[2]];
            m_TrainSpeedInfo.Vperm = FloatList[UIObj.InFloatList[3]];
            m_TrainSpeedInfo.Vint = FloatList[UIObj.InFloatList[4]];
            m_TrainSpeedInfo.VEmergent = FloatList[UIObj.InFloatList[5]];

            switch (m_TrainSpeedInfo.PointerColor)
            {
                case TrainInfoColor.Gray :
                    m_ImagePointer = ImageResource.zhizhenhui;
                    break;
                case TrainInfoColor.Orange :
                    m_ImagePointer = ImageResource.zhizhencheng;
                    break;
                case TrainInfoColor.Red :
                    m_ImagePointer = ImageResource.zhizhenhong;
                    break;
                case TrainInfoColor.Yellow :
                    m_ImagePointer = ImageResource.zhizhenhuang;
                    break;
                case TrainInfoColor.White :
                    m_ImagePointer = ImageResource.zhizhenbai;
                    break;
            }
        }
    }
}
