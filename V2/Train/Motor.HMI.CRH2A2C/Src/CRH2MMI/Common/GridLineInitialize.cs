using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Grid.Items;
using CommonUtil.Util;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Models;
using MMI.Facility.Interface;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid;
using CommonUtil.Controls.Grid.GridLine;
using MMICommon.Controls.Grid;

namespace CRH2MMI.Common
{
    /// <summary>
    /// 一般的 gridline 初始化器
    /// </summary>
    internal class GridLineInitialize
    {
        /// <summary>
        /// 默认为
        /// Alignment = StringAlignment.Near,
        /// LineAlignment = StringAlignment.Near,
        /// FormatFlags = StringFormatFlags.DirectionRightToLeft
        /// </summary>
        public StringFormat InnerTextFormat { set; get; }

        /// <summary>
        /// Text 内 float .tostring 的格式, 默认"F0"
        /// </summary>
        public string FloatFormat { set; get; }

        public baseClass ViewClass { set; get; }

        public GridLineInitialize()
        {
            FloatFormat = "F0";
            InnerTextFormat = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };
        }

        public List<GDIRectText> CreateTitles(GridLine gl, GridConfig glConfig, int textWidth = 90)
        {
            var rlt = new List<GDIRectText>();
            for (int i = 0; i < glConfig.Rows.Count; i++)
            {
                var row = glConfig.Rows[i];
                var point = gl.GetIntersectionLocation(i, 0);
                var cellSize = gl.CellSize;
                var text = new GDIRectText();
                text.SetBkColor(Color.Black);
                text.SetTextColor(new SolidBrush(Color.White));
                text.SetText(row.Name);
                text.SetTextStyle(10, FormatStyle.DirectionLeftToRight, false, "Arial");
                text.OutLineRectangle = new Rectangle(1, (int)(point.Y - cellSize.Height / 2), textWidth, (int)cellSize.Height);
                rlt.Add(text);
            }
            return rlt;
        }

        public void InitInnerContrl(GridLine gl, GridColumnConfig columnConfig, GridItemBase item)
        {
            switch (columnConfig.Parent.ItemType)
            {
                case GridItemType.Text:
                    InitInnerTextControl(gl, columnConfig.Parent.Parent, columnConfig.Parent, columnConfig, item as GridTextItem);
                    break;
                case GridItemType.Roudness:
                    InitInnerRoundnessControl(gl, columnConfig.Parent.Parent, columnConfig.Parent, columnConfig, item as GridRoundnessItem);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void InitInnerRoundnessControl(GridLine gl, GridConfig config, GridRowConfig rowConfig, GridColumnConfig columnConfig, GridRoundnessItem roundnessItem)
        {
            Debug.Assert(roundnessItem != null, "pei != null");
            roundnessItem.Roundness.Center = gl.GetIntersectionLocation(rowConfig.RowIndex, columnConfig.ColumIndex);
            roundnessItem.Roundness.R = config.RoundnessR;
            roundnessItem.Roundness.NeedDrawArc = false;
            roundnessItem.Roundness.NeedDrawContent = true;
            roundnessItem.Roundness.RefreshAction = o =>
            {
                roundnessItem.Roundness.ContentColor = GetInBoolValue(GlobalResource.Instance.GetInBoolIndexs(columnConfig.InBoolColoumNames)[0]) ? rowConfig.Colors[1] : rowConfig.Colors[0];
            };
        }

        private bool GetInBoolValue(int idx)
        {
            return ViewClass.BoolList[idx];
        }

        private float GetInFloatValue(int idx)
        {
            return ViewClass.FloatList[idx];
        }

        private void InitInnerTextControl(GridLine gl, GridConfig config, GridRowConfig rowConfig, GridColumnConfig columnConfig, GridTextItem textItem)
        {

            Debug.Assert(textItem != null, "dataItem != null");
            textItem.ContentSize = new Size(config.TextWidth, config.TextHeight);
            textItem.RectText.BkColor = Color.Black;
            textItem.RectText.TextColor = Color.White;
            textItem.RectText.NeedDarwOutline = true;
            textItem.RectText.TextFormat = InnerTextFormat;

            if (columnConfig.InBoolColoumNames.Any() && rowConfig.Colors.Any() && rowConfig.TextBkColors.Any() && rowConfig.Texts.Any())
            {
                textItem.RectText.RefreshAction = o =>
                {
                    var sen = (GDIRectText)o;
                    var inb = GetInBoolValue(GlobalResource.Instance.GetInBoolIndexs(columnConfig.InBoolColoumNames)[0]);
                    sen.Text = inb ? rowConfig.Texts[1] : rowConfig.Texts[0];
                    sen.TextColor = inb ? rowConfig.Colors[1] : rowConfig.Colors[0];
                    sen.BkColor = inb ? rowConfig.TextBkColors[1] : rowConfig.TextBkColors[0];
                };
            }
            else if (columnConfig.InFloatColoumNames.Any())
            {
                var format = rowConfig.FloatToStringFormat.IsNullOrWhiteSpace()
                    ? FloatFormat
                    : rowConfig.FloatToStringFormat;
                textItem.RectText.RefreshAction = o =>
                {
                    var sen = (GDIRectText) o;
                    sen.Text =
                        GetInFloatValue(
                            GlobalResource.Instance.GetInFloatIndexs(
                                columnConfig.InFloatColoumNames)[0]).ToString(format);
                };
            }
        }
    }
}
