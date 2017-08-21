using System;
using System.Drawing;
using ATP200C.Common;
using ATP200C.PublicComponents;
using MMI.Facility.Interface.Attribute;

namespace ATP200C.MainView
{
    /// <summary>
    /// 功能描述 MainAreoA类 
    ///     信号屏主界面 A区显示信息 包括预警信息 目标距离信息
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-13
    /// </summary>
   [GksDataType(DataType.isMMIObjectClass)]
    public class MainAreoA : ATPBaseClass
   {
       #region 私有字段
       private  GtRectText A_text;//A 区 警 示 信 息

       /// <summary>
       /// 制 动 预 警
       /// </summary>
       private  RectWarn rectwarn = new RectWarn();
       private  bool Warn = false;
       #endregion

       #region 接口数据
       /// <summary>
       /// 制动预警时间
       /// </summary>
       private int _zhiDongYuJinTime = 0;

       /// <summary>
       /// 目标距离
       /// </summary>
       private int _targetDistance = 0;

       /// <summary>
       /// 是否处于TSM模式（true 表示处于该模式）
       /// </summary>
       private bool _isTSM = false;

       /// <summary>
       /// 是否处于CSM模式
       /// </summary>
       private bool _isCSM = false;

       /// <summary>
       /// 列车当前速度
       /// </summary>
       private int _currentSpeed;

       /// <summary>
       /// 列车允许速度
       /// </summary>
       private int _allowSpeed;

       /// <summary>
       /// 预警速度
       /// </summary>
       private int _yuJinSpeed;
       #endregion

       #region  重载方法
       /// <summary>
       /// 获取图元对象名称
       /// </summary>
       /// <returns></returns>
       public override string GetInfo()
       {
           return "信号屏主界面A区信息显示区域";
       }

       public override bool Initalize()
       {
           #region A 区 警 示 信 息 坐 标 信 息
           A_text = new GtRectText();
           A_text.SetBkColor(1, 2, 3);
           A_text.SetTextColor(255, 255, 255);
           A_text.SetTextStyle(14, FormatStyle.Center, true, "宋体");
           A_text.SetTextRect(ATP200CCommon.RectA[1].X + 5, ATP200CCommon.RectA[1].Y + 3, ATP200CCommon.RectA[1].Width - 10, 30);
           //A_text.isdrawrectfrm = true;
           #endregion
           return true;
       }

       /// <summary>
       /// 绘制方法(该方法会被定时器按一定的时间间隔重复调用) 
       /// </summary>
       /// <param name="dcGs">参数 GDI+ 绘图对象</param>
       public override void paint(Graphics dcGs)
       {
           GetValue();
           refreshData();
           onDraw(dcGs);
       }
       #endregion

       /// <summary>
       /// 获取接口数据
       /// </summary>
       private void GetValue()
       { 
           _zhiDongYuJinTime=Convert.ToInt32(FloatList[UIObj.InFloatList[0]]);//制动预警时间

           if (_zhiDongYuJinTime<0)
           {
               _zhiDongYuJinTime = 0;
           }

           _targetDistance = Convert.ToInt32(FloatList[UIObj.InFloatList[1]]);//目标距离

           if (_targetDistance<0) 
           {
               _targetDistance = 0;
           }

           _currentSpeed = Convert.ToInt32(FloatList[UIObj.InFloatList[2]]);
           _allowSpeed = Convert.ToInt32(FloatList[UIObj.InFloatList[3]]);
           _yuJinSpeed = Convert.ToInt32(FloatList[UIObj.InFloatList[4]]);

           _isTSM = BoolList[UIObj.InBoolList[0]];//TSM状态
           _isCSM = BoolList[UIObj.InBoolList[1]];//CSM模式
       }

       /// <summary>
       /// 刷新数据方法
       /// </summary>
       private void refreshData()
       {
           A_text.SetText(_targetDistance.ToString());

             Warn = false;
             if (_isCSM)//CSM模式
             {
                 //制动预警
                 //大小
                 if (_zhiDongYuJinTime == 0)
                 {
                     rectwarn.SetRect(54);
                     Warn = true;
                 }
                 else if (_zhiDongYuJinTime <= 4)
                 {
                     rectwarn.SetRect(40);
                     Warn = true;
                 }
                 else if (_zhiDongYuJinTime < 8)
                 {
                     rectwarn.SetRect(25);
                     Warn = true;
                 }
                 else if (_zhiDongYuJinTime == 8)
                 {
                     rectwarn.SetRect(5);
                     Warn = true;
                 }
                 else
                 {
                     Warn = false;
                 }
                 //颜色
                 if (_currentSpeed <= _allowSpeed)
                 {
                     rectwarn.SetBKColor(195, 195, 195);//灰色
                     //isudu = 0;
                 }
                 else if (_currentSpeed > _allowSpeed && _currentSpeed <= _yuJinSpeed)
                 {
                     rectwarn.SetBKColor(234, 145, 0);//橙色
                     // isudu = 1;
                 }
                 else if (_currentSpeed > _yuJinSpeed)
                 {
                     rectwarn.SetBKColor(191, 0, 2);//红色
                     // isudu = 2;
                 }
             }

            #region TSM 模式	
            else if (_isTSM)//TSM模式
            {
                //制动预警
                //大小
                if (_zhiDongYuJinTime == 0)
                {
                    rectwarn.SetRect(54);
                    Warn = true;
                }
                else if (_zhiDongYuJinTime <= 4)
                {
                    rectwarn.SetRect(40);
                    Warn = true;
                }
                else if (_zhiDongYuJinTime < 8)
                {
                    rectwarn.SetRect(25);
                    Warn = true;
                }
                else
                {
                    rectwarn.SetRect(5);
                    Warn = true;
                }

                //颜色
                if (_currentSpeed <= _allowSpeed)
                {
                    rectwarn.SetBKColor(223, 223, 0);//黄色
                    // isudu = 3;
                }
                else if (_currentSpeed > _allowSpeed && _currentSpeed <= _yuJinSpeed)
                {
                    rectwarn.SetBKColor(234, 145, 0);//橙色
                    //isudu = 4;
                }
                else if (_currentSpeed > _yuJinSpeed)
                {
                    rectwarn.SetBKColor(191, 0, 2);//红色
                    // isudu = 5;
                }
            }
                #endregion

             if (ATPMain.Instance.ControlModel != ControModelEnum.完全)
             {
                 Warn = false;
             }
       }

       /// <summary>
       /// 绘制方法
       /// </summary>
       /// <param name="g"></param>
       private void onDraw(Graphics g)
       {
           //制动预警
           //if (GT_Main.Model == ControModelEnum.完全)//当前只有处于完全模式才显示预警时间
           //{
           //    Warn = true;
           //}
           //else
           //{
           //    Warn = false;
           //}

           if (Warn)
           {
               rectwarn.OnDraw(g);
           }
           #region A1区绘制预警标表示 制动前的预警时间
           //if (_isTSM)//TSM模式绘制 预警标
           //{
           //     if (_zhiDongYuJinTime > 8)
           //    {
           //        g.FillRectangle(GT_Common2D.gray_Brush, new RectangleF(GT_Common2D.RectA[0].Width / 2 - 2.5f, GT_Common2D.RectA[0].Y + 
           //            GT_Common2D.RectA[0].Height / 2 - 2.5f,
           //              5, 5));
           //    }
           //    else
           //    {
           //        g.FillRectangle(GT_Common2D.gray_Brush, new RectangleF(GT_Common2D.RectA[0].Width / 2 - 5 * (8 - _zhiDongYuJinTime) / 2, 
           //            GT_Common2D.RectA[0].Y + GT_Common2D.RectA[0].Height / 2 -
           //            5 * (8 - _zhiDongYuJinTime) / 2, 5 * (8 - _zhiDongYuJinTime), 5 * (8 - _zhiDongYuJinTime)));
           //    }
           //}

           //if (_isCSM)//当处于CSM模式时 绘制预警标
           //{
           //    if (_zhiDongYuJinTime <= 8)
           //    {
           //        g.FillRectangle(GT_Common2D.gray_Brush, new RectangleF(GT_Common2D.RectA[0].Width / 2 - 5 * (8 - _zhiDongYuJinTime) / 2, GT_Common2D.RectA[0].Y + GT_Common2D.RectA[0].Height / 2 -
           //        5 * (8 - _zhiDongYuJinTime) / 2, 5 * (8 - _zhiDongYuJinTime), 5 * (8 - _zhiDongYuJinTime)));
           //    }
           //}
           #endregion

           #region A2区 以柱状图显示 目标距离
           if (_isTSM && Warn)//当处于TSM模式时显示目标距离;  当前改为只有在 TSM以及 完全模式才显示目标距离
           {
               A_text.OnDraw(g);
               g.DrawLine(ATP200CCommon.WhitePen4, ATP200CCommon.RectA[1].X + 17, ATP200CCommon.RectA[1].Y + 250, ATP200CCommon.RectA[1].X + 37, ATP200CCommon.RectA[1].Y + 250);
               g.DrawLine(ATP200CCommon.WhitePen4, ATP200CCommon.RectA[1].X + 17, ATP200CCommon.RectA[1].Y + 50, ATP200CCommon.RectA[1].X + 37, ATP200CCommon.RectA[1].Y + 50);
               for (int i = 1; i < 7; i++)
               {
                   g.DrawLine(ATP200CCommon.WhitePen2, ATP200CCommon.RectA[1].X + 20, ATP200CCommon.RectA[1].Y + 250 - 20 * i, ATP200CCommon.RectA[1].X + 35, ATP200CCommon.RectA[1].Y + 250 - 20 * i);
               }
               g.DrawLine(ATP200CCommon.WhitePen4, ATP200CCommon.RectA[1].X + 17, ATP200CCommon.RectA[1].Y + 90, ATP200CCommon.RectA[1].X + 37, ATP200CCommon.RectA[1].Y + 90);

               for (int i = 1; i < 5; i++)
               {
                   g.DrawLine(ATP200CCommon.WhitePen2, ATP200CCommon.RectA[1].X + 25, ATP200CCommon.RectA[1].Y + 50 + 10 * i, ATP200CCommon.RectA[1].X + 37, ATP200CCommon.RectA[1].Y + 50 + 10 * i);
               }

               if (_targetDistance >= 0 && _targetDistance <= 1000)
               {
                   g.FillRectangle(ATP200CCommon.WhiteBrush, new Rectangle(ATP200CCommon.RectA[1].X + 40, ATP200CCommon.RectA[1].Y + 250 - Convert.ToInt32(_targetDistance / 5),
                       15, Convert.ToInt32(_targetDistance / 5)));
               }
               else
               {
                   g.FillRectangle(ATP200CCommon.WhiteBrush, new Rectangle(ATP200CCommon.RectA[1].X + 40, ATP200CCommon.RectA[1].Y +
                   250 - 200, 15, 200));
               }
           }
           #endregion
       }
   }
}
