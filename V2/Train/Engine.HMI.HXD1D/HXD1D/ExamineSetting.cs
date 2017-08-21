using System.Collections.Generic;
using System.Drawing;
using HXD1D.Common;
using HXD1D.Titlte;
using HXD1D.控制设置;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;


namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class ExamineSetting:baseClass
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
        /// 矩形框列表
        /// </summary>
        private List<Rectangle> _rects;
        #region 重载的方法
        public override string GetInfo()
        {
            return "检修设置";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            if (Title.CurentView != 39)
            {
                Title.ContentDictionary.Clear();
                ControlSeting._rowid = 0;
            }
            JudgeView(Title.CurentView,dcGs);
            base.paint(dcGs);

        }

        private void DrawView39Rects(Graphics e)
        {
            e.DrawRectangle(FormatStyle.WhitePen,_rects[0]);
            for (int i = 0; i < 6; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen1,_rects[3+i]);
            }
        }

        private void DrawView39Chacters(Graphics e)
        {
            //密码设置界面的提示
            e.DrawString(FormatStyle.str30[0],FormatStyle.Font14,FormatStyle.WhiteBrush,_rects[1],FormatStyle.MFormat);
            //密码输入提示
            e.DrawString(FormatStyle.str30[1], FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[2], FormatStyle.MFormat);
          
            foreach (var i in Title.PassWordDictionary.Keys)
            {
                e.DrawString(Title.PassWordDictionary[i], FormatStyle.Font10, FormatStyle.WhiteBrush,
                    _rects[3+i], FormatStyle.MFormat);
            }

        }

        private void Fillview39Rects(Graphics e)
        {
            if (ControlSeting._rowid>=0&&ControlSeting._rowid<6)
            {
                e.FillRectangle(FormatStyle.BlueBrush, _rects[ControlSeting._rowid + 3]);
            }
           
        }
          
        private void JudgeView(int view, Graphics e)
        {
            switch (view)
            {
                case 34:
                    break;
                case 35:
                    break;
                case 36:
                    break;
                case 37:
                    break;
                case 38:
                    break;
                case 39:
                    Fillview39Rects(e);
                    DrawView39Rects(e);
                    DrawView39Chacters(e);
                    break;
            }
        }

        #endregion

        private void InitData()
        {
            #region 列表的初始化
            _boolIds = new List<int>();
            _imgs = new List<Image>();
            _foolatIds = new List<int>();
            _rects = new List<Rectangle>();
            #endregion

            #region //从配置表取Img，Floatids，Boolids编号
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
            #region 视图39的坐标
            //外面大矩形框
            _rects.Add(new Rectangle(217,173,318,256));
            //提示内容
            _rects.Add(new Rectangle(235,131,287,28));
            //密码输入提示
            _rects.Add(new Rectangle(290,215,120,32));
            //里面密码输入的六个小矩形框
            for (int i = 0; i < 6; i++)
            {
                _rects.Add(new Rectangle(309+i*25,268,22,40));
                
            }
            #endregion

        }

    }
}
