using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Alarm
{
    /// <summary>
    /// 警报汇总菜单 将当前的活动事件按单元分类 点击各个按钮显示该单元的细分故障 
    /// 各个单元编号按从左至右 重上至下的顺序编号
    /// </summary>  
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_AlarmOverView : CRH1BaseClass
    {
        #region 私有字段
        public GT_MenuTitle Title = new GT_MenuTitle("警报汇总");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 165, 70, 40);
        public Rectangle Rect;//菜单栏的大矩形框
        public SolidBrush Brush = new SolidBrush(Color.FromArgb(119, 136, 153));//菜单栏背景刷
        public Pen Pen = new Pen(Color.FromArgb(217, 223, 229), 3);
        public CRH1AButton[] GButton = new CRH1AButton[14];

        public string[] Buttontext = new string[14] { "高 压", "牵 引", "外门", "控制和通讯", "辅助供电", "制 动", "空调", "前 端", "电池供电", "供 风",     
            "污 物 箱","轴报","烟雾警报器","信息系统" };//按钮的标题名称
        public int Weight = 100;
        public int Height = 50;
        //public static bool[] selected_Unit = new bool[15] { false, false, false, false, false, false, false, false, false, false, false, false, false, false,true };//被选中的单元号
        public static bool[] IsActive = new bool[14];//该数组的相应位为一表示该单元存在活动事件
        // public Dictionary<int,ExceptionData> activeEvent=new Dictionary<int,ExceptionData>();
        #endregion

        #region 静态字段
        public static int SelectedUnitView = -1;
        #endregion

        #region 重载方法
        public override string GetInfo()
        {
            return "警报汇总";
        }

        public override bool Initialize()
        {
            //3
            InitData();

            return true;
        }
        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point.X, point.Y);
            return true;
        }
        protected override bool OnMouseUp(Point point)
        {
            OnButtonUp(point.X, point.Y);
            return true;
        }
        #endregion

        #region 私有方法
        private void GetValue()
        {

            for (int j = 0; j < 14; j++)
            {
                if (!GT_GalobalFaultManage.Instance.HasSuredActiveFault())
                {
                    IsActive[j] = false;
                }
                else
                {
                    var allFault = GT_GalobalFaultManage.Instance.AllActiveFaults;
                    foreach (ExceptionData exData in allFault)
                    {
                        if (exData.ExUnit - 1 == j)
                        {
                            IsActive[j] = true;
                            break;
                        }
                        else
                        {
                            IsActive[j] = false;
                        }
                    }

                }
            }

            for (int i = 0; i < 14; i++)
            {
                if (IsActive[i])
                {
                    GButton[i].SetButtonColor(255, 255, 0);//该单元有活动事件时按钮呈现为黄色 
                }
                else
                {
                    GButton[i].SetButtonColor(192, 192, 192);//该单元没有活动事件时按钮呈现为灰色
                }
            }
        }

        private void InitData()
        {
            Rect = new Rectangle(Recposition.X + 3, Recposition.Y, 785, 285);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i * 4 + j < 14)
                    {
                        GButton[i * 4 + j] = new CRH1AButton();
                        GButton[i * 4 + j].SetButtonRect(Rect.X + j * (Weight + 60) + 100, Rect.Y + i * (Height + 20) + 10, Weight, Height);
                        GButton[i * 4 + j].SetButtonText(Buttontext[i * 4 + j]);
                    }
                }
            }
            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                foreach (var button in GButton)
                {
                    button.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
            };
          
        }

        /// <summary>
        /// 绘制方法
        /// </summary>
        /// <param name="g"></param>
        public void DrawOn(Graphics g)
        {
            //绘制菜单标题
            Title.OnDraw(g);
            g.FillRectangle(Brush, Rect);
            g.DrawRectangle(Pen, Rect);
            for (int i = 0; i < 14; i++)
            {
                GButton[i].OnDraw(g);
            }
        }

        /// <summary>
        /// 响应按钮按下事件
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void OnButtonDown(int x, int y)
        {

            for (int i = 0; i < 14; i++)
            {
                if (GButton[i].OutLineRectangle.Contains(x, y) && GButton[i].IsEnable)
                {
                    if (IsActive[i])
                    {
                        GButton[i].OnButtonDown();
                    }
                }
            }
        }

        /// <summary>
        /// 响应按钮弹起事件
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 14; i++)
            {
                if (GButton[i].OutLineRectangle.Contains(x, y) && GButton[i].IsEnable)
                {
                    if (IsActive[i])
                    {
                        SelectedUnitView = i + 1;
                        OnPost(CmdType.ChangePage, 23, 0, 0);
                        GButton[i].OnButtonUp();
                    }
                }
            }
        }
        #endregion
    }
}