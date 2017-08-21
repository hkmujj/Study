using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HXD1D.Common;
using HXD1D.Titlte;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D.故障界面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class FaultInfo : baseClass
    {
        /// <summary> 
        /// 图片集
        /// </summary>
        private List<Image> _imgs;

        //<summary>
        //bool逻辑号
        // </summary>
        private List<int> _boolIds;

        //<summary>
        //float逻辑号
        // </summary>
        private List<int> _foolatIds;

        /// <summary>
        /// 视图16矩形框
        /// </summary>
        private List<Rectangle> _rectangles;
        /// <summary>
        /// 视图15矩形框
        /// </summary>
        private List<Rectangle> _rects;
        /// <summary>
        /// 当前页码
        /// </summary>
        private int currentpage = 0;
        //按键列表
        private List<Region> _regions;
        /// <summary>
        /// 故障页码
        /// </summary>
        private int _allDef;
        /// <summary>
        /// 当前故障个数
        /// </summary>
        private int _noOverDefNumb;
        /// <summary>
        /// 历史故障个数
        /// </summary>
        private int _allDefNumb;
        /// <summary>
        /// 行号
        /// </summary>
        private int _rowindex = -1;
        /// <summary>
        /// 故障下标
        /// </summary>
        private int _faultId = 0;
        /// <summary>
        /// 所有故障下标
        /// </summary>
        private int _allfaultId = 0;
        /// <summary>
        /// 向下按钮
        /// </summary>
        private bool _upIspress
        {
            set
            {
                if (_up == value) return;
                if (value && _rowindex > 0)
                {
                    _rowindex--;
                }

                _up = value;
            }

        }
        /// <summary>
        /// 向上按钮
        /// </summary>
        private bool _downIspress
        {
            set
            {
                if (_down == value)
                    return;
                if (Title.buttonIsDown[8] && (Title.CurentView == 15 || Title.CurentView == 16))
                {
                    if (value && _rowindex < Title.MsgInfList.AllMsgsList.Count - 1)
                    {
                        _rowindex++;
                    }
                }
                else
                {
                    if (value && _rowindex < Title.MsgInfList.CurrentMsgList.Count - 1)
                        _rowindex++;
                }
                _down = value;
            }

        }

        /// <summary>
        /// 处理按钮的状态
        /// </summary>
        private bool _handle
        {
            set
            {
                if (_inihandle == value) return;
                if (value)
                {
                    _logicId.Add(_faultId);
                }
                _inihandle = value;
            }
        }

        /// <summary>
        /// 处理按钮的初始的状态
        /// </summary>
        private bool _inihandle;
        /// <summary>
        /// 向下按钮的初始状态
        /// </summary>
        private bool _up;
        /// <summary>
        /// 向上按钮的初始状态
        /// </summary>
        private bool _down;


        /// <summary>
        /// 故障所对应的逻辑号
        /// </summary>
        private List<int> _logicId;

        #region 重载的方法
        public override string GetInfo()
        {
            return "故障信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override bool mouseDown(Point nPoint)
        {
            for (int index = 0; index < _regions.Count; index++)
            {
                if (_regions[index].IsVisible(nPoint))
                {
                    switch (index)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                    }
                }
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            for (int index = 0; index < _regions.Count; index++)
            {
                if (_regions[index].IsVisible(nPoint))
                {
                    switch (index)
                    {
                        case 0:
                            if (currentpage > 0)
                            {
                                currentpage--;
                            }
                            break;
                        case 1:

                            if (currentpage < _allDef)
                            {
                                currentpage++;
                                _rowindex = 0;
                            }
                            break;
                    }
                }
            }
            return base.mouseUp(nPoint);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            ChangeView(dcGs, Title.CurentView);
            base.paint(dcGs);


        }
        #endregion

        #region 视图15的方法
        /// <summary>
        /// 填充矩形框
        /// </summary>
        /// <param name="e"></param>
        private void Fillrects(Graphics e)
        {
            e.FillRectangle(FormatStyle.GreyBrus, _rects[0]);
            for (int i = 0; i < 17; i++)
            {
                e.FillRectangle(FormatStyle.YellowBrush, _rects[6 + i]);
                e.FillRectangle(FormatStyle.GreyBrush, _rects[23 + i]);
                e.FillRectangle(FormatStyle.BlueBrush, _rects[40 + i]);
                e.FillRectangle(FormatStyle.YellowBrush, _rects[57 + i]);
            }

        }
        /// <summary>
        /// 当前是那个页面，0 当前故障，1 历史故障
        /// </summary>
        public static int MenuId;
        /// <summary>
        /// 画文字
        /// </summary>
        /// <param name="e"></param>

        private void DrawChacter(Graphics e)
        {
            if (!BoolList[_boolIds[1]] && BoolOldList[_boolIds[1]])
            {
                MenuId = 1;
            }
            else if (!BoolList[806] && BoolOldList[806])
            {
                MenuId = 0;
            }
            if (MenuId == 1)
            {
                e.DrawString("故障发生时间", FormatStyle.Font14, FormatStyle.BlackBrush, _rects[110], FormatStyle.MFormat);
                e.DrawString("故障结束时间", FormatStyle.Font14, FormatStyle.BlackBrush, _rects[111], FormatStyle.MFormat);
            }
            else
            {
                e.DrawString("故障发生时间", FormatStyle.Font14, FormatStyle.BlackBrush, _rects[5], FormatStyle.MFormat);
            }
            e.DrawString("类别", FormatStyle.Font14, FormatStyle.RedBrush, _rects[1], FormatStyle.MFormat);
            e.DrawString("车号", FormatStyle.Font14, FormatStyle.BlackBrush, _rects[2], FormatStyle.MFormat);
            e.DrawString("代码", FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[3], FormatStyle.MFormat);
            e.DrawString("故障内容", FormatStyle.Font14, FormatStyle.BlackBrush, _rects[4], FormatStyle.MFormat);
        }
        /// <summary>
        ///画图片 
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawImg(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.DrawImage(_imgs[0 + i], _rects[108 + i]);
            }

        }
        /// <summary>
        /// 判断是画当前故障还是历史故障
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawOn(Graphics e)
        {
            _noOverDefNumb = Title.MsgInfList.CurrentMsgList.Count;//故障当前代码的个数
            _allDefNumb = Title.MsgInfList.AllMsgsList.Count;//故障历史代码的个数

            if (Title.buttonIsDown[8] && Title.CurentView == 15)
            {
                _allDef = Title.MsgInfList.AllMsgsList.Count / 17;
                DrawAllMsg(e);
            }
            else
            {
                _allDef = Title.MsgInfList.CurrentMsgList.Count / 17;
                DrawMsglist(e);
            }


        }
        /// <summary>
        /// 填充选中的
        /// </summary>
        /// <param name="e">画图对象</param>
        private void FillChoose(Graphics e)
        {
            if (!BoolList[_boolIds[2]] && BoolOldList[_boolIds[2]])
            {
                _upIspress = true;
            }
            else
            {
                _upIspress = false;
            }
            if (!BoolList[_boolIds[3]] && BoolOldList[_boolIds[3]])
            {
                _downIspress = true;
            }
            else
            {
                _downIspress = false;
            }
            _faultId = Title.MsgInfList.CurrentMsgList.Count - (currentpage * 17 + _rowindex % 17) - 1;
            _allfaultId = Title.MsgInfList.AllMsgsList.Count - (currentpage * 17 + _rowindex % 17) - 1;

            if (_noOverDefNumb > 0)
            {

                e.FillRectangle(FormatStyle.BlueBrush, _rects[6 + _rowindex]);
                e.FillRectangle(FormatStyle.BlackBrush, _rects[23 + _rowindex]);
                e.FillRectangle(FormatStyle.BlackBrush, _rects[40 + _rowindex]);
                e.FillRectangle(FormatStyle.BlueBrush, _rects[57 + _rowindex]);


            }
        }

        /// <summary>
        /// 画出故障发生时的各个状态
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawMsglist(Graphics e)
        {
            if (_noOverDefNumb <= 0)
                return;
            for (int i = _noOverDefNumb - 1, j = 0; i >= 0; i--, j++)
            {
                if ((_noOverDefNumb - 1 - i) >= (currentpage * 17) && (_noOverDefNumb - 1 - i) < (currentpage + 1) * 17)
                {

                    int rowid = j % 17;
                    e.DrawString(Title.MsgInfList.CurrentMsgList[i].Level,
                      FormatStyle.Font14, FormatStyle.RedBrush, _rects[6 + rowid], FormatStyle.MFormat);
                    e.DrawString(Title.MsgInfList.CurrentMsgList[i].MsgID,
                         FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[23 + rowid], FormatStyle.MFormat);
                    e.DrawString(Title.MsgInfList.CurrentMsgList[i].Code,
                         FormatStyle.Font14, FormatStyle.RedBrush, _rects[40 + rowid], FormatStyle.MFormat);
                    e.DrawString(Title.MsgInfList.CurrentMsgList[i].MsgContent,
                         FormatStyle.Font14, FormatStyle.BlackBrush, _rects[74 + rowid], FormatStyle.LeftFormat);
                    e.DrawString(Title.MsgInfList.CurrentMsgList[i].MsgReceiveTime.ToString("MM-dd hh:mm:ss"),
                            FormatStyle.Font12, FormatStyle.BlackBrush, _rects[112 + rowid], FormatStyle.LeftFormat);

                }

            }

        }

        /// <summary>
        /// 画整个故障
        /// </summary>
        /// <param name="e">画图的对象</param>
        private void DrawAllMsg(Graphics e)
        {
            if (_allDefNumb <= 0)
                return;
            for (int i = _allDefNumb - 1, j = 0; i >= 0; i--, j++)
            {
                if ((_allDefNumb - 1 - i) >= (currentpage * 17) && (_allDefNumb - 1 - i) < (currentpage + 1) * 17)
                {

                    int rowid = j % 17;
                    e.DrawString(Title.MsgInfList.AllMsgsList[i].Level,
                        FormatStyle.Font14, FormatStyle.RedBrush, _rects[6 + rowid], FormatStyle.MFormat);
                    e.DrawString(Title.MsgInfList.AllMsgsList[i].MsgID,
                        FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[23 + rowid], FormatStyle.MFormat);
                    e.DrawString(Title.MsgInfList.AllMsgsList[i].Code,
                        FormatStyle.Font14, FormatStyle.RedBrush, _rects[40 + rowid], FormatStyle.MFormat);
                    e.DrawString(Title.MsgInfList.AllMsgsList[i].MsgContent,
                        FormatStyle.Font14, FormatStyle.BlackBrush, _rects[74 + rowid], FormatStyle.LeftFormat);
                    e.DrawString(Title.MsgInfList.AllMsgsList[i].MsgReceiveTime.ToString("MM-dd hh:mm:ss"),
                        FormatStyle.Font12, FormatStyle.BlackBrush, _rects[91 + rowid], FormatStyle.LeftFormat);
                    e.DrawString(Title.MsgInfList.AllMsgsList[i].MsgOverTime.ToString("MM-dd hh:mm:ss"), FormatStyle.Font12,
                        FormatStyle.BlackBrush, _rects[112 + rowid], FormatStyle.LeftFormat);
                }

            }
        }
        /// <summary>
        /// 选中行字的变色
        /// </summary>
        /// <param name="e">画图对象</param>
        private void Drawselect(Graphics e)
        {
            _rowindex = _rowindex % 17;

            if ((BoolList[_boolIds[2]] || BoolList[_boolIds[3]])&&_rowindex!=-1)
            {
                e.DrawString(
                    Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].Level : Title.MsgInfList.CurrentMsgList[_faultId].Level,
                    FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[6 + _rowindex], FormatStyle.MFormat);
                e.DrawString(
                    Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].MsgID : Title.MsgInfList.CurrentMsgList[_faultId].MsgID,
                   FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[23 + _rowindex], FormatStyle.MFormat);
                e.DrawString(
                    Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].Code : Title.MsgInfList.CurrentMsgList[_faultId].Code,
                    FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[40 + _rowindex], FormatStyle.MFormat);
                e.DrawString(Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].MsgContent : Title.MsgInfList.CurrentMsgList[_faultId].MsgContent,
                    FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[74 + _rowindex], FormatStyle.LeftFormat);
                if (Title.buttonIsDown[8])
                {
                    e.DrawString(Title.MsgInfList.AllMsgsList[_allfaultId].MsgReceiveTime.ToString("MM-dd hh:mm:ss"),
                        FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[91 + _rowindex], FormatStyle.LeftFormat);

                    e.DrawString(Title.MsgInfList.AllMsgsList[_allfaultId].MsgOverTime.ToString("MM-dd hh:mm:ss"),
                        FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[112 + _rowindex], FormatStyle.LeftFormat);
                }
                else
                {
                    e.DrawString(Title.MsgInfList.CurrentMsgList[_faultId].MsgReceiveTime.ToString("MM-dd hh:mm:ss"),
                         FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[112 + _rowindex], FormatStyle.LeftFormat);
                }
            }

        }

        #endregion

        #region 视图16的方法
        /// <summary>
        /// 赋值和计算当前故障号和历史故障号
        /// </summary>
        private void Msg()
        {
            if (!BoolList[_boolIds[6]] && BoolOldList[_boolIds[6]])
            {
                _upIspress = true;
            }
            else
            {
                _upIspress = false;
            }
            if (!BoolList[_boolIds[7]] && BoolOldList[_boolIds[7]])
            {
                _downIspress = true;
            }
            else
            {
                _downIspress = false;
            }

            try
            {
                _faultId = Title.MsgInfList.CurrentMsgList.Count - (currentpage * 17 + _rowindex % 17) - 1;
                _allfaultId = Title.MsgInfList.AllMsgsList.Count - (currentpage * 17 + _rowindex % 17) - 1;
            }
            catch (Exception e)
            { 
            }
        }
        /// <summary>
        /// 画出处理提示界面的标题懒和处理信息
        /// </summary>
        /// <param name="e"></param>
        private void Drawrects(Graphics e)
        {
            //标题栏的背景颜色
            e.FillRectangle(FormatStyle.GreyBrush, _rectangles[0]);
            e.FillRectangle(FormatStyle.GreyBrush, _rectangles[1]);
            e.FillRectangle(FormatStyle.GreyBrush, _rectangles[2]);
            e.DrawRectangle(FormatStyle.WhitePen, _rectangles[0]);
            e.DrawRectangle(FormatStyle.WhitePen, _rectangles[1]);
            e.DrawRectangle(FormatStyle.WhitePen, _rectangles[2]);
            //标题栏变化的内容

            try
            {
                if (Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList.Count() > _allfaultId : Title.MsgInfList.CurrentMsgList.Count() > _faultId)
                {
                    e.DrawString(Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].Level : Title.MsgInfList.CurrentMsgList[_faultId].Level, FormatStyle.Font14,
                        FormatStyle.WhiteBrush, _rectangles[0], FormatStyle.MFormat);
                    e.DrawString(Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].MsgContent : Title.MsgInfList.CurrentMsgList[_faultId].MsgContent, FormatStyle.Font14,
                        FormatStyle.WhiteBrush, _rectangles[1], FormatStyle.MFormat);
                    e.DrawString(Title.ButtonName, FormatStyle.Font14,
                        FormatStyle.WhiteBrush, _rectangles[2], FormatStyle.MFormat);
                    ////处理信息的提示
                    //if (Title.buttonIsDown[2])
                    //{
                    //    e.DrawString(Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].V0 : Title.MsgInfList.CurrentMsgList[_faultId].V0, FormatStyle.Font14,
                    //        FormatStyle.WhiteBrush, _rectangles[3], FormatStyle.LeftFormat);
                    //}
                    //else if (Title.buttonIsDown[3])
                    //{
                    //    e.DrawString(Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].V1 : Title.MsgInfList.CurrentMsgList[_faultId].V1, FormatStyle.Font14,
                    //        FormatStyle.WhiteBrush, _rectangles[3], FormatStyle.LeftFormat);
                    //}
                    //else if (Title.buttonIsDown[4])
                    //{
                    //    e.DrawString(Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].FaultSolutionStr : Title.MsgInfList.CurrentMsgList[_faultId].FaultSolutionStr,
                    //        FormatStyle.Font14, FormatStyle.WhiteBrush, _rectangles[3], FormatStyle.LeftFormat);
                    //}
                }
            }
            catch(Exception exception)
            {

            }


        }
        /// <summary>
        ///填充 消息提示的背景颜色
        /// </summary>
        /// <param name="e">画图对象</param>
        private void Fillrect(Graphics e)
        {
            e.FillRectangle(FormatStyle.BlueBrush, _rectangles[3]);
        }

        #endregion
        /// <summary>
        /// 判断视图
        /// </summary>
        /// <param name="e">画图对象</param>
        /// <param name="view">视图</param>    
        private void ChangeView(Graphics e, int view)
        {
            switch (view)
            {
                case 15:
                    Fillrects(e);
                    FillChoose(e);
                    DrawChacter(e);
                    DrawOn(e);
                    DrawImg(e);
                    Drawselect(e);
                    break;
                case 16:
                    //Fillrect(e);
                    Drawrects(e);
                    Msg();
                    paint_16_Why(e);
                    paint_16_JieGuo(e);
                    paint_16_Operation(e);
                    break;
            }
        }

        private void paint_16_Why(Graphics dcGs)
        {
            dcGs.DrawRectangle(Pens.White, new Rectangle(0, 63, 800, 464));

            dcGs.DrawString(
                "可能产生的原因",
                FormatStyle.Font12B,
                Brushes.White,
                new RectangleF(0,63,420,50),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
            dcGs.FillRectangle(Brushes.Blue, new Rectangle(2,113,420,182));

            try
            {
                string str = Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].Reason : Title.MsgInfList.CurrentMsgList[_faultId].Reason;
                if (str.Contains("#"))
                {
                    string str_ = "";
                    string[] strs = str.Split(new Char[] { '#' });
                    for (int i = 0; i < strs.Length; i++)
                    {
                        if (i > 0) str_ += "\n";
                        str_ += strs[i];
                    }
                    str = str_;
                }

                dcGs.DrawString(
                    str,
                    FormatStyle.Font14,
                    FormatStyle.WhiteBrush,
                    new Rectangle(2, 115, 420, 182),
                    FormatStyle.LeftFormat
                    );
            }
            catch (Exception e)
            { 
            }
        }

        private void paint_16_JieGuo(Graphics dcGs)
        {
            dcGs.DrawString(
                 "导致的结果",
                 FormatStyle.Font12B,
                 Brushes.White,
                 new RectangleF(0, 295, 420, 50),
                 new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                 );
            dcGs.FillRectangle(Brushes.Blue, new Rectangle(2, 345, 420, 182));

            try
            {
                string str = Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].Result : Title.MsgInfList.CurrentMsgList[_faultId].Result;
                if (str.Contains("#"))
                {
                    string str_ = "";
                    string[] strs = str.Split(new Char[] { '#' });
                    for (int i = 0; i < strs.Length; i++)
                    {
                        if (i > 0) str_ += "\n";
                        str_ += strs[i];
                    }
                    str = str_;
                }

                dcGs.DrawString(
                    str,
                    FormatStyle.Font14,
                    FormatStyle.WhiteBrush,
                    new Rectangle(2, 347, 420, 182),
                    FormatStyle.LeftFormat
                    );
            }
            catch (Exception e)
            { 
            }
        }

        private void paint_16_Operation(Graphics dcGs)
        {
            dcGs.DrawString(
                 "处理措施",
                 FormatStyle.Font12B,
                 Brushes.White,
                 new RectangleF(440, 63, 360, 50),
                 new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                 );
            dcGs.FillRectangle(Brushes.Blue, new Rectangle(440, 113, 360, 414));

            try
            {
                string str = Title.buttonIsDown[8] ? Title.MsgInfList.AllMsgsList[_allfaultId].FaultSolutionStr : Title.MsgInfList.CurrentMsgList[_faultId].FaultSolutionStr;
                if (str.Contains("#"))
                {
                    string str_ = "";
                    string[] strs = str.Split(new Char[] { '#' });
                    for (int i = 0; i < strs.Length; i++)
                    {
                        if (i > 0) str_ += "\n";
                        str_ += strs[i];
                    }
                    str = str_;
                }

                dcGs.DrawString(
                    str,
                    FormatStyle.Font14,
                    FormatStyle.WhiteBrush,
                    new Rectangle(440, 115, 360, 182),
                    FormatStyle.LeftFormat
                    );
            }
            catch (Exception e)
            { 
            }
        }

        private void InitData()
        {
            #region 列表的初始化

            _boolIds = new List<int>();
            _foolatIds = new List<int>();
            _imgs = new List<Image>();
            _rects = new List<Rectangle>();
            _rectangles = new List<Rectangle>();
            _regions = new List<Region>();
            _logicId = new List<int>();

            #endregion

            #region 从配置表取Img，Floatids，Boolids编号
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                _imgs.Add(Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]));
            }
            //取出配置表Boolids编号

            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    _boolIds.Add(UIObj.InBoolList[index] + i);
                }
            }

            //取出配置表Floatids编号
            for (int index = 0; index < UIObj.InFloatList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InFloatList[index + 1]; i++)
                {
                    _foolatIds.Add(UIObj.InFloatList[index] + i);
                }
            }
            #endregion

            #region 视图15的坐标赋值

            _rects.Add(new Rectangle(1, 35, 776, 34));
            _rects.Add(new Rectangle(2, 35, 48, 30));
            _rects.Add(new Rectangle(48, 35, 48, 30));
            _rects.Add(new Rectangle(94, 35, 48, 30));
            _rects.Add(new Rectangle(354, 35, 97, 30));
            _rects.Add(new Rectangle(627, 35, 126, 30));
            //显示类别的位置
            for (int i = 0; i < 17; i++)
            {
                _rects.Add(new Rectangle(2, 69 + i * 27, 30, 27));
            }
            //显示车号的位置
            for (int i = 0; i < 17; i++)
            {
                _rects.Add(new Rectangle(32, 69 + i * 27, 50, 27));

            }
            //显示代码的位置
            for (int i = 0; i < 17; i++)
            {
                _rects.Add(new Rectangle(82, 69 + i * 27, 50, 27));

            }
            //显示故障内容和故障时间的整个位置
            for (int i = 0; i < 17; i++)
            {
                _rects.Add(new Rectangle(132, 69 + i * 27, 644, 27));

            }
            //显示故障内容的位置
            for (int i = 0; i < 17; i++)
            {
                _rects.Add(new Rectangle(132, 69 + i * 27, 467, 27));
            }
            //显示故障时间的位置
            for (int i = 0; i < 17; i++)
            {
                _rects.Add(new Rectangle(539, 69 + i * 27, 127, 27));
            }
            //上下页
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(776, 70 + i * 414, 23, 45));
            }
            for (int i = 0; i < 2; i++)
            {
                _regions.Add(new Region(_rects[108 + i]));
            }
            //所有故障的开始时间和结束时间
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(528 + i * 120, 35, 126, 30));
            }
            //故障结束的时间
            for (int i = 0; i < 17; i++)
            {
                _rects.Add(new Rectangle(656, 69 + i * 27, 127, 27));
            }

            #endregion

            #region 视图16的赋值

            //处理界面的标题栏
            _rectangles.Add(new Rectangle(2, 35, 165, 27));
            _rectangles.Add(new Rectangle(167, 35, 429, 27));
            _rectangles.Add(new Rectangle(596, 35, 195, 27));
            //处理界面信息界面底图
            _rectangles.Add(new Rectangle(0, 63, 800, 464));

            #endregion



        }
    }
}
