#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：维护-No.3-软件版本
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Maintenance
{
    /// <summary>
    /// 功能描述：维护-No.3-软件版本
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V803MaintenanceSoftWareVersion : baseClass
    {
        #region 私有变量

        private readonly Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private readonly Font m_ChineseFont = new Font("宋体", 10);
        private int m_I = 0;
        private Rectangle m_FrameRects;
        private Rectangle m_FillRect;
        private ListBox<SoftwareVersionInfo> m_VersionInfos;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-软件版本";
        }

        private Button[] m_PageBtns;
        private string m_PageStr;
        private readonly Rectangle m_PageRect = new Rectangle(720, 245, 54, 20);

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_FrameRects = new Rectangle(51, 126, 644, 310);
            m_FillRect = new Rectangle(52, 127, 643, 309);

            m_VersionInfos = new ListBox<SoftwareVersionInfo>(new RectangleF(82, 127, 630, 309), new List<SoftwareVersionInfo>(),
               new ListBoxHeader() { Text = "", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14, FontStyle.Regular), BackgroundBrush = Brushs.BlackBrush, Height = 28, Width = 90, Property = "SoftwareName", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc },
               new ListBoxHeader() { Text = "C1", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("Arial", 14), DataFont = new Font("Arial", 14f), BackgroundBrush = Brushs.BlackBrush, Height = 28, Width = 88, Property = "Version1", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc },
               new ListBoxHeader() { Text = "C2", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("Arial", 14), DataFont = new Font("Arial", 14f), BackgroundBrush = Brushs.BlackBrush, Height = 28, Width = 88, Property = "Version2", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc },
               new ListBoxHeader() { Text = "C3", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("Arial", 14), DataFont = new Font("Arial", 14f), BackgroundBrush = Brushs.BlackBrush, Height = 28, Width = 88, Property = "Version3", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc },
               new ListBoxHeader() { Text = "C4", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("Arial", 14), DataFont = new Font("Arial", 14f), BackgroundBrush = Brushs.BlackBrush, Height = 28, Width = 88, Property = "Version4", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc },
               new ListBoxHeader() { Text = "C5", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("Arial", 14), DataFont = new Font("Arial", 14f), BackgroundBrush = Brushs.BlackBrush, Height = 28, Width = 80, Property = "Version5", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc },
               new ListBoxHeader() { Text = "C6", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("Arial", 14), DataFont = new Font("Arial", 14f), BackgroundBrush = Brushs.BlackBrush, Height = 28, Width = 80, Property = "Version6", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc }
               );
            m_VersionInfos.RowCount = 8;

    

            var image = UIObj.ParaList.Select(s =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, s), FileMode.Open))
                {
                    return Image.FromStream(fs);
                }

            }).ToArray();

            var bsUp = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = image[0], DownImage = image[0] };
            var bsDown = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = image[1], DownImage = image[1] };

            m_PageBtns = new Button[2];
            m_PageBtns[0] = new Button("", new Rectangle(715, 270, 64, 45), (int)ViewState.HsUpBtn, bsUp, false);
            m_PageBtns[0].ClickEvent += (sender, args) =>
            {

                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    m_VersionInfos.LastPage();
                    ChangPageInfo();
                }

            };
            m_PageBtns[1] = new Button("", new Rectangle(715, 320, 64, 45), (int)ViewState.HsDownBtn, bsDown, false);
            m_PageBtns[1].ClickEvent += (sender, args) =>
            {

                if (CurrentPage < AllPage)
                {
                    CurrentPage++;
                    m_VersionInfos.NextPage();
                    ChangPageInfo();
                }

            };
            var all =
                File.ReadAllLines(Path.Combine(AppPaths.ConfigDirectory, "软件版本.txt"), Encoding.Default)
                    .Where(w => !w.StartsWith(";;;;")).Select(s => s.Split(';')).Where(w => w.Length == 7).Select(s => new SoftwareVersionInfo()
                    {
                        SoftwareName = s[0],
                        Version1 = s[1],
                        Version2 = s[2],
                        Version3 = s[3],
                        Version4 = s[4],
                        Version5 = s[5],
                        Version6 = s[6]
                    });
            m_VersionInfos.DataList.AddRange(all);

            ChangPageInfo();

            return true;
        }

        private int CurrentPage { get; set; } = 1;
        private int AllPage { get; set; }
        private void ChangPageInfo()
        {
            AllPage = (m_VersionInfos.DataList.Count + m_VersionInfos.RowCount - 1) / m_VersionInfos.RowCount;

            m_PageStr = $"页{CurrentPage}-{AllPage}";
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(m_WhiteLinePen, m_FrameRects);
            dcGs.FillRectangle(Brushs.BlackBrush, m_FillRect);
            m_VersionInfos.Paint(dcGs);
            dcGs.DrawString(m_PageStr, m_ChineseFont, Brushs.BlackBrush, m_PageRect, FontInfo.SfCc);

            for (m_I = 0; m_I < m_PageBtns.Length; m_I++)
            {
                m_PageBtns[m_I].Paint(dcGs);
            }
            base.paint(dcGs);
        }
        #endregion

        /// <summary>
        /// 鼠标单点下
        /// </summary>
        /// <param name="point"/>
        /// <returns/>
        public override bool mouseDown(Point point)
        {
            m_PageBtns.FirstOrDefault(f => f.Rect.Contains(point))?.MouseDown(point);
            return base.mouseDown(point);
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="point"/>
        /// <returns/>
        public override bool mouseUp(Point point)
        {
            m_PageBtns.FirstOrDefault(f => f.Rect.Contains(point))?.MouseUp(point);
            return base.mouseUp(point);
        }
    }
}
