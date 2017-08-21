using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using HXD1D.Common;
using HXD1D.Titlte;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using System.Linq;

namespace HXD1D.运行条件
{
    [GksDataType(DataType.isMMIObjectClass)]
    class RunCondtion:baseClass
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
        /// 矩形框编号
        /// </summary>
        private List<Rectangle> _rects;
        private SortedDictionary<int, String> m_AllmsgDictionary = new SortedDictionary<int, string>();
        /// <summary>
        /// 主断列表
        /// </summary>
        private List<string> _allmsgList;
        public static bool[] BtnisDown=new bool[3];
        /// <summary>
        /// 牵引状态列表
        /// </summary>
        private static Dictionary<int,string> _qinyngDic=new Dictionary<int,string>();
        /// <summary>
        /// 受电弓状态
        /// </summary>
        private static Dictionary<int,string> _bowDic=new Dictionary<int,string>();
        /// <summary>
        /// 主断列表
        /// </summary>
        private static Dictionary<int,string> _zuduanDic=new Dictionary<int,string>();

      

        private ReadConfigcs read = new ReadConfigcs();
        #region 重载的方法
        public override string GetInfo()
        {
            return "运行条件";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

      
        public override void paint(Graphics dcGs)
        {   
            //ReciveMsg();
            Judge(Title.CurentView, dcGs);
            DrawrRects(dcGs);
            base.paint(dcGs);

        }
     


        #endregion

        private void DrawView26Chacter(Graphics e)
        {
            read.ReaFile("受电弓状态", _bowDic);
            if (BtnisDown[0] == false )
            {
                if (_bowDic.Count<=16)
                {
                    for (int i=0;i<_bowDic.Count;i++)
                    {
                        
                        //
                        if (BoolList[_bowDic.Keys.ToList()[i]])
                        {

                            e.FillRectangle(FormatStyle.WhiteBrush, _rects[32 + i].X + 1, _rects[32 + i].Y + 1,
                                             _rects[32 + i].Width - 2, _rects[32 + i].Height - 2);

                            e.DrawString(_bowDic[_bowDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.BlackBrush,
                                _rects[32 + i], FormatStyle.drawFormat);
                        }
                        else
                        {
                            e.DrawString(_bowDic[_bowDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.WhiteBrush,
                          _rects[32 + i], FormatStyle.drawFormat);
                        }
                    }
                }
                if (_bowDic.Count>16)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (BoolList[_bowDic.Keys.ToList()[i]])
                        {
                            e.FillRectangle(FormatStyle.WhiteBrush, _rects[32 + i].X+1,_rects[32+i].Y+1,
                                             _rects[32+ i].Width - 2, _rects[32 + i].Height-2);

                            e.DrawString(_bowDic[_bowDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.BlackBrush,
                                _rects[32 + i], FormatStyle.drawFormat);
                           
                        }
                        else
                        {
                            e.DrawString(_bowDic[_bowDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.WhiteBrush,
                          _rects[32 + i], FormatStyle.drawFormat); 
                             
                        }
                       

                    }
                }
               
                for (int i = 0; i < 16; i++)
                {
                    e.DrawRectangle(FormatStyle.WhitePen, _rects[i + 32]);
                }
            }

            if (BtnisDown[0] && _bowDic.Count>16&&_bowDic.Count < 32)
            {
                for (int i = 0; i < _bowDic.Count - 16; i++)
                {
                    if (BoolList[_bowDic.Keys.ToList()[i+16]])
                    {
                        e.FillRectangle(FormatStyle.WhiteBrush, _rects[48 + i].X + 1, _rects[48 + i].Y + 1,
                                             _rects[48 + i].Width - 2, _rects[48 + i].Height - 2);


                        e.DrawString(_bowDic[_bowDic.Keys.ToList()[i + 16]], FormatStyle.Font12, FormatStyle.BlackBrush,
                            _rects[48 + i], FormatStyle.drawFormat);

                           
                    }
                    else
                    {
                        e.DrawString(_bowDic[_bowDic.Keys.ToList()[i + 16]], FormatStyle.Font12, FormatStyle.WhiteBrush,
                      _rects[48 + i], FormatStyle.drawFormat);
                    }
                   
                }
              
                for (int i = 0; i < 16; i++)
                {

                    e.DrawRectangle(FormatStyle.WhitePen, _rects[i + 48]);

                }
               
            }


            DrawWords(e, _bowDic.Count, _bowDic.Keys.ToList()[0]);
         
             

        }
        private void DrawView27Chacter(Graphics e)
        {
          
            read.ReaFile("主断条件", _zuduanDic);
            try
            {
                if (BtnisDown[1] == false)
                {
                    if (_zuduanDic.Count <= 16)
                    {

                        for (int i = 0; i < _zuduanDic.Count; i++)
                        {
                            if (BoolList[_zuduanDic.Keys.ToList()[i]])
                            {

                                e.FillRectangle(FormatStyle.WhiteBrush, _rects[32 + i].X + 1, _rects[32 + i].Y + 1,
                                                 _rects[32 + i].Width - 2, _rects[32 + i].Height - 2);

                                e.DrawString(_zuduanDic[_zuduanDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.BlackBrush,
                                    _rects[32 + i], FormatStyle.drawFormat);
                            }
                            else
                            {
                                e.DrawString(_zuduanDic[_zuduanDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.WhiteBrush,
                              _rects[32 + i], FormatStyle.drawFormat);
                            }
                            
                        }
                    }
                    if (_zuduanDic.Count > 16)
                    {
                        for (int i=0;i<16;i++)
                        {
                            if (BoolList[_zuduanDic.Keys.ToList()[i]])
                            {

                                e.FillRectangle(FormatStyle.WhiteBrush, _rects[32 + i].X + 1, _rects[32 + i].Y + 1,
                                                 _rects[32 + i].Width - 2, _rects[32 + i].Height - 2);

                                e.DrawString(_zuduanDic[_zuduanDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.BlackBrush,
                                    _rects[32 + i], FormatStyle.drawFormat);
                            }
                            else
                            {
                                e.DrawString(_zuduanDic[_zuduanDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.WhiteBrush,
                              _rects[32 + i], FormatStyle.drawFormat);
                            }
                        }
                       
                    }

                    for (int i = 0; i < 16; i++)
                    {
                        e.DrawRectangle(FormatStyle.WhitePen, _rects[i + 32]);
                    }
                }

                if (BtnisDown[1] && _zuduanDic.Count > 16 && _zuduanDic.Count < 32)
                {
                    for (int i = 0; i < _bowDic.Count - 16; i++)
                    {
                        if (BoolList[_zuduanDic.Keys.ToList()[i+16]])
                        {
                            e.FillRectangle(FormatStyle.WhiteBrush, _rects[48 + i].X + 1, _rects[48 + i].Y + 1,
                                                 _rects[48 + i].Width - 2, _rects[48 + i].Height - 2);


                            e.DrawString(_zuduanDic[_zuduanDic.Keys.ToList()[i + 16]], FormatStyle.Font12, FormatStyle.BlackBrush,
                                _rects[48 + i], FormatStyle.drawFormat);


                        }
                        else
                        {
                            e.DrawString(_zuduanDic[_zuduanDic.Keys.ToList()[i + 16]], FormatStyle.Font12, FormatStyle.WhiteBrush,
                          _rects[48 + i], FormatStyle.drawFormat);
                        }

                    }
                    for (int i = 0; i < 16; i++)
                    {

                        e.DrawRectangle(FormatStyle.WhitePen, _rects[i + 48]);

                    }
                }
                DrawWords(e, _zuduanDic.Count, _zuduanDic.Keys.ToList()[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
           
           
        }
        private void DrawView28Chacter(Graphics e)
        {

            read.ReaFile("牵引条件", _qinyngDic);
            try
            {
                if (BtnisDown[2]==false)
                {
                    if (_qinyngDic.Count <= 16)
                    {
                        //foreach (var i in _qinyngDic.Keys)
                        //{
                        //    if (BoolList[i])
                        //    {
                        //        e.FillRectangle(FormatStyle.WhiteBrush, _rects[32 + i].X + 1, _rects[32 + i].Y + 1,
                        //                         _rects[32 + i].Width - 2, _rects[32 + i].Height - 2);

                        //        e.DrawString(_qinyngDic[i], FormatStyle.Font12, FormatStyle.BlackBrush,
                        //            _rects[32 + i], FormatStyle.drawFormat);
                        //    }
                        //}
                        for (int i = 0; i < _qinyngDic.Count; i++)
                        {
                            if (BoolList[_qinyngDic.Keys.ToList()[i]])
                            {
                                e.FillRectangle(FormatStyle.WhiteBrush, _rects[32 + i].X + 1, _rects[32 + i].Y + 1,
                                                 _rects[32 + i].Width - 2, _rects[32 + i].Height - 2);

                                e.DrawString(_qinyngDic[_qinyngDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.BlackBrush,
                                    _rects[32 + i], FormatStyle.drawFormat);
                            }
                            else
                            {
                                e.DrawString(_qinyngDic[_qinyngDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.WhiteBrush,
                              _rects[32 + i], FormatStyle.drawFormat);
                            }
                        }
                        //for (int i = 0; i < _qinyngDic.Count; i++)
                        //{
                        //    if (BoolList[_boolIds[i + 39]])
                        //    {

                        //        e.FillRectangle(FormatStyle.WhiteBrush, _rects[32 + i].X + 1, _rects[32 + i].Y + 1,
                        //                         _rects[32 + i].Width - 2, _rects[32 + i].Height - 2);

                        //        e.DrawString(_qinyngDic[i + 1060], FormatStyle.Font12, FormatStyle.BlackBrush,
                        //            _rects[32 + i], FormatStyle.drawFormat);
                        //    }
                        //    else
                        //    {
                        //        e.DrawString(_qinyngDic[i + 1060], FormatStyle.Font12, FormatStyle.WhiteBrush,
                        //      _rects[32 + i], FormatStyle.drawFormat);
                        //    }
                        //}
                    }
                    if (_qinyngDic.Count > 16)
                    {
                        for (int i = 0; i < 16; i++)
                        {

                            //if (BoolList[_boolIds[i + 39]])
                            if (BoolList[_qinyngDic.Keys.ToList()[i]])
                            {

                                e.FillRectangle(FormatStyle.WhiteBrush, _rects[32 + i].X + 1, _rects[32 + i].Y + 1,
                                                 _rects[32 + i].Width - 2, _rects[32 + i].Height - 2);

                                e.DrawString(_qinyngDic[_qinyngDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.BlackBrush,
                                    _rects[32 + i], FormatStyle.drawFormat);
                            }
                            else
                            {
                                e.DrawString(_qinyngDic[_qinyngDic.Keys.ToList()[i]], FormatStyle.Font12, FormatStyle.WhiteBrush,
                              _rects[32 + i], FormatStyle.drawFormat);
                            }

                        }
                    }

                    for (int i = 0; i < 16; i++)
                    {
                        e.DrawRectangle(FormatStyle.WhitePen, _rects[i + 32]);
                    }
                }

                if (BtnisDown[2] && _qinyngDic.Count > 16 && _qinyngDic.Count < 32)
                {
                    for (int i = 0; i < _qinyngDic.Count - 16; i++)
                    {
                        if (BoolList[_qinyngDic.Keys.ToList()[i+16]])
                        {
                            e.FillRectangle(FormatStyle.WhiteBrush, _rects[48 + i].X + 1, _rects[48 + i].Y + 1,
                                                 _rects[48 + i].Width - 2, _rects[48 + i].Height - 2);


                            e.DrawString(_qinyngDic[_qinyngDic.Keys.ToList()[i + 16]], FormatStyle.Font12, FormatStyle.BlackBrush,
                                _rects[48 + i], FormatStyle.drawFormat);

                        }
                       else
                        {
                            e.DrawString(_qinyngDic[_qinyngDic.Keys.ToList()[i + 16]], FormatStyle.Font12, FormatStyle.WhiteBrush,
                          _rects[48 + i], FormatStyle.drawFormat);
                        }
                       
                    }

                    for (int i = 0; i < 16; i++)
                    {

                        e.DrawRectangle(FormatStyle.WhitePen, _rects[i + 48]);

                    }
                }
                DrawWords(e, _qinyngDic.Count, _qinyngDic.Keys.ToList()[0]);
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
          
        }

        private void DrawrRects(Graphics e)
        {
            for (int i = 0; i < 32; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen,_rects[i]);
            }
        }

        private void DrawWords(Graphics e,int countId,int fristId)
        {
            for (int i = 0; i < 32; i++)
            {
                if (BoolList[fristId+i]&&i<countId)
                {
                  
                    e.FillRectangle(FormatStyle.WhiteBrush, _rects[0 + i].X + 1, _rects[0 + i].Y + 1,
                        _rects[0 + i].Width - 2, _rects[0 + i].Height - 2);
                    e.DrawString((i + 1).ToString(CultureInfo.InvariantCulture), FormatStyle.Font12, FormatStyle.BlackBrush, _rects[i],
                        FormatStyle.MFormat);
                }
                else
                {
                    e.DrawString((i + 1).ToString(CultureInfo.InvariantCulture), FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[i], FormatStyle.MFormat);
                }

            }
        }


        private void Judge(int View, Graphics e)
        {
            switch (View)
            {
                case 26:
                    DrawView26Chacter(e);
                    break;
                case 27:
                    DrawView27Chacter(e);
                    break;
                case 28:
                    DrawView28Chacter(e);
                    break;
            }
        }

        private void InitData()
        {
            _boolIds=new List<int>();
            _foolatIds=new List<int>();
            _imgs=new List<Image>();
            _rects=new List<Rectangle>( );
         
      

            #region 从配置表里取出数据
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
  
            for (int j = 0; j < 16; j++)
            {
              _rects.Add(new Rectangle(8, 76 + j* 26, 34, 26));   
               
            }
            for (int i = 0; i < 16; i++)
            {
                _rects.Add(new Rectangle(678, 86 + i* 26, 34, 26));   
            }
            for (int i = 0; i < 16; i++)
            {
                _rects.Add(new Rectangle(42, 76 + i * 26, 636, 26)); 
            }
            for (int i = 0; i < 16; i++)
            {
                _rects.Add(new Rectangle(42, 86 + i * 26, 636, 26));
            }
            //按钮下一页位置
              _rects.Add(new Rectangle(716, 230, 64, 50));
          
        }
    }
}
