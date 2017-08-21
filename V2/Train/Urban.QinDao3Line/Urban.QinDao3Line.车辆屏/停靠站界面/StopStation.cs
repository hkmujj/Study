using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI.停靠站界面
{
 [GksDataType(DataType.isMMIObjectClass)]
  public class StopStation:NewQBaseclass
 {
     public List<Rectangle> m_Rects=new List<Rectangle>();
     private readonly List<RectangleF> m_RectFs = new List<RectangleF>();
     private readonly List<Point> m_Points = new List<Point>();
     //越站位置列表
     public static Dictionary<int, int> m_BypasedIdDic = new Dictionary<int, int>();
     //越战移动下标
     public static List<int> m_BypasedIdList=new List<int>();
     //以停靠位置列表
     public static Dictionary<int, int> m_StopedIds = new Dictionary<int, int>();
     //以停靠下标
     public static int m_StopedId;
     //下一站位置列表
     public static int m_NextStaionIds;
     //下一站下标
     public static int m_NextStaionId ;
     //列车将要停靠列表
     public static Dictionary<int, int> m_StaionDic = new Dictionary<int, int>();
     //列车将要停靠下标
     public static List<int> m_Staionslist=new List<int>();
     /// <summary>
     /// 
     /// </summary>
     public static int m_MaxValue;
     //越战移动下标
     public static int m_BypassedId=0;
     /// <summary>
     /// 将要停靠站下标
     /// </summary>
     public static int m_StationId = 0;
     /// <summary>
     /// 四个按钮的状态
     /// </summary>
     public static bool[] m_DownId = new bool[4];
     //箭头指向编号
     public static int m_BtnId=-1;

     private string[] m_Str1 =
     {
         ResourceFacade.TrainLineResourceStartStation, ResourceFacade.TrainLineResourceDestition,
         ResourceFacade.TrainLineResourceDeparture, ResourceFacade.TrainLineResourceConfirm
     };

     /// <summary>
     /// 车站信息
     /// </summary>
     private readonly Dictionary<int, string> m_StationDic = new Dictionary<int, string>();
     public override bool init(ref int nErrorObjectIndex)
     {
         IntiData();
         ReadConfigcs read = new ReadConfigcs("车站信息",m_StationDic);
         read.ReaFile(Path.Combine(RecPath, "..\\Config"));
         Init();
         return true;
     }

     public override void paint(Graphics g)
     {
        
         DrawImg(g);
         DrawWords(g);
         DrawRects(g);
         IconMove(g);
         Fillrercts(g);
         base.paint(g);
     }

     private void DrawImg(Graphics e)
     {
         //按钮底图
         for (int i = 0; i < 4; i++)
         {
             e.DrawImage(m_Imgs[0], m_Rects[i]);
         }
   
         //按下的状态
         for (int i = 0; i < 4; i++)
         {
             if (m_DownId[i])
             {  e.DrawImage(m_Imgs[6], m_Rects[i]);
             }
           
         }
         
         //车站底图
         e.DrawImage(m_Imgs[5], m_Rects[4]);
         for (int i=0;i<4;i++)
         {
             if (m_DownId[i])
             {
                 //确认键
                 e.DrawImage(m_Imgs[0], m_Rects[5]);
                 //上下按钮键
                 for (int j = 0; j < 2; j++)
                 {
                     e.DrawImage(m_Imgs[j + 3], m_Rects[6 + j]);
                 }
                  
             }
             //始发站为 North railway
             if (BoolList[m_BoolIds[1]])
             {
                 e.DrawImage(m_Imgs[7], m_Rects[33]);
             }
             //始发站为 Qin Dao railway
             if (BoolList[m_BoolIds[0]])
             {
                 e.DrawImage(m_Imgs[8], m_Rects[32]);
             }
         }
       
     }
    

     /// <summary>
     /// 画文字
     /// </summary>
     /// <param name="e"></param>
     private void DrawWords(Graphics e)
     {
         int id = 0;
         for (int i = 0; i < 4; i++)
         {
             e.DrawString(m_Str1[i], Common.m_Font10B, Common.m_BlackBrush,
                          m_Rects[i], Common.m_MFormat);
         }
         //左侧按钮按下右侧按键显示的文字
         for (int i=0;i<4;i++)
         {
             if (m_DownId[i])
             {
                 e.DrawString(m_Str1[4], Common.m_Font10B, Common.m_BlackBrush,
                      m_Rects[5], Common.m_MFormat);
             }
             
         }
         
         //
         foreach (var k in m_StationDic.Keys)
         {
           
             e.DrawString(m_StationDic[k], Common.m_Font8B, Common.m_BlackBrush,
                           m_RectFs[id + 20], Common.m_LeftFormat);
             id++;

         }

     }

     private void Fillrercts(Graphics e)
     {
         int i = 0;
         int ksecond = 0;
         int kid = 0;
         //以停靠站
          foreach(var k in m_StationDic.Keys)
         {
             if (k==int.Parse(FloatList[m_FoolatIds[4]].ToString("0")))
             {
                
                 if (!m_StopedIds.ContainsKey(i)&&i>1)
                 {
                     m_StopedIds.Add(i,i + 10-2);
                 }
           
             }
             //if (!BoolList[_boolIds[i + 22]] && StopedIds.ContainsKey(i))
             //{
             //    StopedIds.Remove(671);
             //}
             //跳跃
              if (k==int.Parse(FloatList[m_FoolatIds[3]].ToString("0")))
             {
                 
                 if (!m_BypasedIdDic.ContainsKey(i)&&i>1)
                 {
                     m_BypasedIdDic.Add(i,i + 10-2);

                 }
                  
                 CompareMIn(m_BypasedIdDic,m_BypasedIdList);

             }
              //if (!BoolList[_boolIds[i+2]]&&BypasedIdDic.ContainsKey(i))
              //{
              //    BypasedIdList.Remove(BypasedIdDic[i]);
              //    BypasedIdDic.Remove(651);
              //}
             //下一站,以停靠站下一站为下一站
            if (m_StopedIds.Count>0)
            {
                if (k == int.Parse(FloatList[m_FoolatIds[1]].ToString("0")))
               {
                   m_NextStaionIds = m_StopedIds.Keys.First() + 1;
               }
               if (BoolList[m_BoolIds[0]])
               {
                   m_NextStaionIds = m_StopedIds.Keys.Last() + 1;
               }
           
                e.FillRectangle(Common.m_YellowBrush, m_RectFs[m_NextStaionIds]);
            }

             //列车将要停靠
            if (k == int.Parse(FloatList[m_FoolatIds[5]].ToString("0")))
             {
               
                 if (!m_StaionDic.ContainsKey(i)&&i>1)
                 {
                     m_StaionDic.Add(i,i + 10-2);
                 }
                 CompareMIn(m_StaionDic, m_Staionslist);
             }

              //if (!BoolList[_boolIds[i + 42]]&&StaionDic.ContainsKey(i))
              //{
              //    Staionslist.Remove(StaionDic[i]);
              //    StaionDic.Remove(i);
              //}
              //每次下标移动
             i++;
         }
         if (m_StationDic.Keys.First()==int.Parse(FloatList[m_FoolatIds[1]].ToString()))
         {
             //终点站
             e.FillEllipse(Common.m_GreenBrush, m_Rects[30]);
             //起点站
             e.FillEllipse(Common.m_WhiteBrush, m_Rects[31]);
         }
         foreach (var kd in m_StationDic.Keys)
         {
            if (ksecond==1)
            {
                kid = kd;
            }
             ksecond++;
         }
         if (kid== int.Parse(FloatList[m_FoolatIds[2]].ToString()))
         {
             //终点站
             e.FillEllipse(Common.m_WhiteBrush, m_Rects[30]);
             //起点站
             e.FillEllipse(Common.m_GreenBrush, m_Rects[31]);
         }
         //已停靠站被选中的
         foreach (var ks in m_StopedIds.Keys)
         {
             e.FillRectangle(Common.m_RedBrush, m_RectFs[ks - 2]);
         }
         //跳跃站台被选中
         foreach (var kb in m_BypasedIdDic.Keys)
         {
             e.FillRectangle(Common.m_GreyBrush, m_RectFs[kb-2]);
         }
         //列车将要停靠
         foreach (var kn in m_StaionDic.Keys)
         {
             e.FillRectangle(Common.m_GreenBrush, m_RectFs[kn - 2]);
         }

     }

     /// <summary>
     /// 终点站和起点站的园
     /// </summary>
     /// <param name="e"></param>
     private void DrawRects(Graphics e)
     {
         e.DrawEllipse(Common.m_BlackPen, m_Rects[30]);
         e.DrawEllipse(Common.m_BlackPen, m_Rects[31]);
     }
 
     /// <summary>
     /// 箭头的移动
     /// </summary>
     /// <param name="e"></param>
     private void IconMove(Graphics e)
     {
         if (BoolList[m_BoolIds[1]])
         {
             if (m_DownId[0])
             {
                 //箭头移动底图
                 e.DrawImage(m_Imgs[1], m_Rects[9]);
             }
             if (m_DownId[1])
             {
                 //箭头移动底图
                 e.DrawImage(m_Imgs[1], m_Rects[8]);
             }
         }
         if (BoolList[m_BoolIds[0]])
         {
             if (m_DownId[0])
             {
                 //箭头移动底图
                 e.DrawImage(m_Imgs[1], m_Rects[8]);
             }
             if (m_DownId[1])
             {
                 //箭头移动底图
                 e.DrawImage(m_Imgs[1], m_Rects[9]);
             }
         }
         if (m_BypasedIdDic.Count>0)
         {
             if (m_DownId[2])
             {
                 //箭头移动底图
                 e.DrawImage(m_Imgs[1], m_Rects[m_BypasedIdList[m_BypassedId]]);
                 
             }
         }
         if (m_StaionDic.Count> 0)
         {
             if (m_DownId[3])
             {
                 //箭头移动底图
                 e.DrawImage(m_Imgs[1], m_Rects[m_Staionslist[m_StationId]]);
             }
         }
     
     }
     public void CompareMIn(Dictionary<int,int> dictionary, List<int> list)
     {
         
         foreach (var v in dictionary)
         {
             if (!list.Contains(v.Value))
             {
                 list.Add(v.Value);
             }
             list.Sort();

         }
     }
     public void Init()
     {   
         //右侧四个按钮键的底图0-3
         for (int i = 0; i < 4; i++)
         {
             m_Rects.Add(new Rectangle(21, 118 + i * 92, 124, 52));
         }
         //车站底图 4
         m_Rects.Add(new Rectangle(373, 93, 29, 436));
         //确认键底图 5
         m_Rects.Add(new Rectangle(631,381,124,52));
         //上下键底图 6-7
         for (int i=0;i<2;i++)
         {
             m_Rects.Add(new Rectangle(688,198+i*63,60,63));
         }
         //终点站文字坐标 8
         m_Rects.Add(new Rectangle(272, 81, 103, 30));
         //起点站文字坐标 9
         m_Rects.Add(new Rectangle(272, 507, 103, 30));
         //10-29
         for (int i=0;i<20;i++)
         {
             m_Rects.Add(new Rectangle(272, 102 + i*20, 103, 30));

         }
         //终点站 30
         m_Rects.Add(new Rectangle(378, 78, 18, 18));
         //起点站 31
         m_Rects.Add(new Rectangle(378, 525, 18, 18));
         //行车方向 32- 33
         for (int i=0;i<2;i++)
         {
             m_Rects.Add(new Rectangle(334,110+i*363,30,35));
         }

         //车站状态显示区32-51
         for (int i = 0; i < 20; i++)
         {
             m_RectFs.Add(new RectangleF(381, 109 + i*20.6f, 13, 13));

         }
         //终点站 52
         m_RectFs.Add(new Rectangle(400, 90, 136, 19));
         //其他站 53-72
         for (int i = 0; i < 20; i++)
         {
             m_RectFs.Add(new RectangleF(400, 110 + i * 20.6f, 127, 16));
         }
         //起点站 73
         m_RectFs.Add(new Rectangle(400, 522, 127, 19));
         //
         m_Points.Add(new Point(388,104));
         //
         m_Points.Add(new Point(388,500));
     }
  }
}
