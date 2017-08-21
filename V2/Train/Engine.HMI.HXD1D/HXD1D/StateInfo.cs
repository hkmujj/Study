using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using HXD1D.Common;
using HXD1D.Titlte;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
   public class StateInfo : baseClass
    {
        /// <summary>
        /// 图片集
        /// </summary>
        private List<Image> _imgs;
        //<summary>
        //bool逻辑号
        // </summary>
        private List<int> BoolIds;
        //<summary>
        //float逻辑号
        // </summary>
        private List<int> FoolatIds;

        /// <summary>
        /// 坐标
        /// </summary>
        private List<Rectangle> _rectangles;

        /// <summary>
        /// 文字点的坐标
        /// </summary>
        private List<Rectangle> _list;
        public static int num = 0;//主断分的次数

        public bool CurrentisState
        {
            set
            {
                if (_currentisState == value) return;
                if (value)
                    num++;
                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0],0,num);
                _currentisState = value;
            }
        }
        //主断的状态
        private bool _currentisState;

        public override string GetInfo()
        {
            return "状态信息";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }
        /// <summary>
        /// 重载父类的画图方法
        /// </summary>
        /// <param name="dcGs">画图对象</param>

        public override void paint(Graphics dcGs)
        {

            base.paint(dcGs);
            DrawRects(dcGs);
            DrawChacter(dcGs);
            DrawImgs(dcGs);

        }
        /// <summary>
        /// 画矩形框
        /// </summary>
        /// <param name="e"></param>
        private void DrawRects(Graphics e)
        {
            for (int i = 0; i < 19; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rectangles[i]);
            }

        }
        /// <summary>
        /// 写文字
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawChacter(Graphics e)
        {   //各状态的名字
            for (int i = 0; i < _list.Count; i++)
            {
                e.DrawString(FormatStyle.str5[i], FormatStyle.Font12, FormatStyle.WhiteBrush,
                               _list[i],FormatStyle.MFormat);
            }
            //主断分的次数

            CurrentisState = BoolList[BoolIds[15]];

            //e.DrawString(num.ToString(CultureInfo.InvariantCulture), FormatStyle.Font12, FormatStyle.WhiteBrush,
            //                   _rectangles[5], FormatStyle.MFormat);
            e.DrawString(Title.OpenCloseCount.ToString(), FormatStyle.Font12, FormatStyle.WhiteBrush,
                               _rectangles[5], FormatStyle.MFormat);
            //MVB A线状态, MVB B线状态 , WTB A线状态, WTB B线状态 
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[BoolIds[17 + j+i*3]])
                    {
                        e.DrawString(FormatStyle.str6[j], FormatStyle.Font12, FormatStyle.WhiteBrush,
                            _rectangles[15 + i],FormatStyle.MFormat);
                    }                 
 
                }
              
            }

        }
        /// <summary>
        /// 画图片
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawImgs(Graphics e)
        {    
            //受电功的状态显示
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[BoolIds[0 + i * 2]])
                {
                    e.DrawImage(_imgs[0 + i], _rectangles[19]);

                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[BoolIds[1+ i * 2]])
                {
                    e.DrawImage(_imgs[0+ i], _rectangles[20]);

                }
            }
            //车顶隔离开关
            for (int i = 0; i< 2;i++ )
            {
                if (BoolList[BoolIds[9+i]])
                {
                    e.DrawImage(_imgs[10+i], _rectangles[21]);

                }
                if (BoolList[BoolIds[11+i]])
                {
                    e.DrawImage(_imgs[12 + i], _rectangles[22]);

                }

            }
            //主断的状态
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[BoolIds[13+i]])
                {
                    e.DrawImage(_imgs[6 + i], _rectangles[25]);

                }
            }
          //司机室占用的状态
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[BoolIds[29 + i]])
                {
                    e.DrawString(FormatStyle.str31[1 + i], FormatStyle.Font14, FormatStyle.WhiteBrush,
                                   _rectangles[26], FormatStyle.MFormat);
                }
                else if (!BoolList[BoolIds[29]] && !BoolList[BoolIds[30]])
                {  e.DrawString(FormatStyle.str31[0], FormatStyle.Font14, FormatStyle.WhiteBrush,
                                  _rectangles[26], FormatStyle.MFormat);
                } 
            }
            // Vcm1,vcm2
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[BoolIds[31 + i]])
                {
                    e.DrawString(FormatStyle.str32[i], FormatStyle.Font14, FormatStyle.WhiteBrush,
                        _rectangles[27] , FormatStyle.MFormat);
                }
                if (BoolList[BoolIds[34 + i]])
                {
                    e.DrawString(FormatStyle.str32[i], FormatStyle.Font14, FormatStyle.WhiteBrush,
                        _rectangles[28], FormatStyle.MFormat);
                }

            }
            //机车模式
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[BoolIds[37 + i]])
                {
                     e.DrawString(FormatStyle.str33[i], FormatStyle.Font14, FormatStyle.WhiteBrush,
                        _rectangles[30], FormatStyle.MFormat);
                }
                }
              //机车节点
                   e.DrawString(FloatList[FoolatIds[0]].ToString(CultureInfo.InvariantCulture), FormatStyle.Font14, FormatStyle.WhiteBrush,
                         _rectangles[29], FormatStyle.MFormat);
               //司机室

                   for (int i = 0; i < 2; i++)
                   {
                       if (BoolList[BoolIds[29+i]])
                       {
                         e.DrawString((1+1).ToString(CultureInfo.InvariantCulture), FormatStyle.Font14, FormatStyle.WhiteBrush,
                           _rectangles[31], FormatStyle.MFormat);
                   }
                //电空电流
                       e.DrawString(FloatList[FoolatIds[1]].ToString(CultureInfo.InvariantCulture), FormatStyle.Font14, FormatStyle.WhiteBrush,
                         _rectangles[32], FormatStyle.MFormat);
             }

               
        
          
        }
        /// <summary>
        /// 初始化和赋值
        /// </summary>
        private void InitData()
        {
            #region //初始化对象
            _imgs = new List<Image>();
            BoolIds = new List<int>();
            FoolatIds = new List<int>();
            _rectangles = new List<Rectangle>();
            _list = new List<Rectangle>();
            #endregion
            #region //从对象配置表里取数据
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                _imgs.Add(Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]));
            }
            //取出配置表Boolids编号

            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    BoolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
            //取出配置表Floatids编号

            for (int index = 0; index < UIObj.InFloatList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InFloatList[index + 1]; i++)
                {
                    FoolatIds.Add(UIObj.InFloatList[index] + i);
                }
            }

            #endregion
            #region //坐标的赋值
            //受电工，车顶隔离开关
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _rectangles.Add(new Rectangle(301 + j * 112, 62 + i * 53, 121, 51));
                }
            }
            //主断，主断分段次数
            for (int i = 0; i < 2; i++)
            {
                _rectangles.Add(new Rectangle(301 + i * 112, 170, 111, 43));
            }
            _rectangles.Add(new Rectangle(301, 215, 224, 37));
            //vcm1,vcm2
            for (int i = 0; i < 2; i++)
            {
                _rectangles.Add(new Rectangle(301 + i * 148, 254, 74, 37));
            }
            //机车模式
            _rectangles.Add(new Rectangle(301, 293, 224, 37));
            //机车节点
            for (int i = 0; i < 3; i++)
            {
                _rectangles.Add(new Rectangle(301 + i * 74, 331, 74, 37));
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _rectangles.Add(new Rectangle(301 + j * 112, 372 + 37 * i, 111, 37));
                }
            }
            //受电弓状态坐标
            for (int i = 0; i < 2; i++)
            {
                _rectangles.Add(new Rectangle(334 + i * 112, 65, 51, 47));
            }
            //隔离开关的状态
            for (int i = 0; i < 2;i++ )
            {
                _rectangles.Add(new Rectangle(331 + i * 114, 128, 62, 37));
            }
            //主断的状态
            for (int i = 0; i < 2; i++)
            {
                _rectangles.Add(new Rectangle(331 + i * 114, 138, 37, 22));
            }
            //
             _rectangles.Add(new Rectangle(342, 171, 40, 38));
            //司机室占用状态
             _rectangles.Add(new Rectangle(337, 221, 153, 24));
            //VCM1，VCM2状态
             for (int i = 0; i < 2;i++ )
             {
                 _rectangles.Add(new Rectangle(318+i*142, 265, 54, 27));

             }
            //机车节点
            _rectangles.Add(new Rectangle(312, 341, 52, 27));
            //机车模式
             _rectangles.Add(new Rectangle(339, 297, 129, 27));
            //司机室，电空电流
             for (int i = 0; i < 2; i++)
             {
                  _rectangles.Add(new Rectangle(324+i*116, 381, 54, 27));

             }
            //受电功1，受电功2文字坐标
            _list.Add(new Rectangle(235, 87, 67, 20));
            _list.Add(new Rectangle(535, 87, 67, 20));
            //车顶隔离开关
            _list.Add(new Rectangle(188, 145, 116, 20));
            _list.Add(new Rectangle(535, 145, 116, 20));
            //主断，主断分段次数
            _list.Add(new Rectangle(259, 195, 44, 20));
            _list.Add(new Rectangle(525, 195, 110, 20));
            //VCM1，VCM2
            _list.Add(new Rectangle(263, 267, 44, 20));
            _list.Add(new Rectangle(524, 267, 44, 20));
            //机车模式，机车节点
            _list.Add(new Rectangle(229, 308, 75, 20));
            _list.Add(new Rectangle(229, 349, 75, 20));
            //司机室，电空电流
            _list.Add(new Rectangle(235, 393, 70, 20));
            _list.Add(new Rectangle(524, 393, 78, 20));
            //MVB A线状态, MVB B线状态 , WTB A线状态, WTB B线状态 
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _list.Add(new Rectangle(207 + j * 318, 430 + i * 41, 92, 20));
                }
            }

            #endregion
          
        }
    }
}
