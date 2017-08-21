using HD_HXD2_TMS.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Drawing;
using System.Collections.Generic;

namespace HD_HXD2_TMS.VC公共组件
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_VC_Title : baseClass
    {
        private String _titleName = "维护";
        private Dictionary<ViewType, String> _viewNames = new Dictionary<ViewType, string>();
        public static String TrainName = "A";

        public override string GetInfo()
        {
            return "公共试图-标题信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _viewNames = new Dictionary<ViewType, string>()
            {
                {ViewType.Main, "维护"},
                {ViewType.Info, "机车概况"},
                {ViewType.ControlInsulateA, "隔离"},
                {ViewType.ControlInsulateB, "隔离"},
                {ViewType.ControlBowA, "受电弓"},
                {ViewType.ControlBowB, "受电弓"},
                {ViewType.ControlRunTest, "运行测试"},
                {ViewType.ControlDistance, "距离计数"},
                {ViewType.Breaking, "空气制动概况"},
                {ViewType.DataDriveA, "驱动概述"},
                {ViewType.DataDriveB, "驱动概述"},
                {ViewType.DataBreaking, "牵引/制动"},
                {ViewType.DataBreakerA, "断路器"},
                {ViewType.DataBreakerB, "断路器"},
                {ViewType.DataAuxiliaryA, "辅助"},
                {ViewType.DataAuxiliaryB, "辅助"},
                {ViewType.DataVersion, "软件版本"},
                {ViewType.DataOnlineState, "在线状态"},
                {ViewType.DataConverter, "主变流"},
                {ViewType.DataTrafficState, "信号状态"},
                {ViewType.DataTrafficStateRIOM21, "信号状态"},
                {ViewType.DataTrafficStateRIOM22, "信号状态"},
                {ViewType.FalutHistroyA,"历史故障查询"},
                {ViewType.FalutHistroyB,"历史故障查询"},
                {ViewType.FalutConveterA,"历史故障查询"},
                {ViewType.FalutConveterB,"历史故障查询"},
                {ViewType.FalutDownload,"故障下载"}
            };
            
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (_viewNames.ContainsKey((ViewType) nParaB))
                {
                    _titleName = _viewNames[(ViewType) nParaB];
                }
                else _titleName = "维护";
            }
        }

        private Pen _pen = new Pen(Color.FromArgb(200, 200, 200));
        public override void paint(Graphics dcGs)
        {
            //标题
            dcGs.DrawRectangle(_pen, new Rectangle(0, 87, 436, 30));
            dcGs.DrawString(
                _titleName,
                new Font("宋体", 11),
                Brushes.White,
                new RectangleF(0+5, 87+2, 431, 30),
                new StringFormat() {  LineAlignment= StringAlignment.Center, Alignment= StringAlignment.Near}
                );

            //日期
            dcGs.DrawRectangle(_pen, new Rectangle(436, 87, 121, 30));
            dcGs.DrawString(
                DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day,
                new Font("宋体", 11),
                Brushes.White,
                new RectangleF(436, 87, 121, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                );

            //时间
            dcGs.DrawRectangle(_pen, new Rectangle(557, 87, 121, 30));
            dcGs.DrawString(
                DateTime.Now.ToLongTimeString(),
                new Font("宋体", 11),
                Brushes.White,
                new RectangleF(557, 87, 121, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                );

            //机车号
            dcGs.DrawRectangle(_pen, new Rectangle(678, 87, 122, 30));
            String trainID = "HXD2" + FloatList[UIObj.InFloatList[BoolList[UIObj.InBoolList[0]] ? 0 : 1]] + TrainName;
            dcGs.DrawString(
                trainID,
                new Font("宋体", 11),
                Brushes.White,
                new RectangleF(678, 87, 122, 30),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                );

            //边框
            dcGs.DrawRectangle(Pens.White, new Rectangle(0, 117, 800, 453));

            base.paint(dcGs);
        }
    }
}
