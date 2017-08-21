using System;
using System.Drawing;
using CRH2MMI.Common;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Resource.Images;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI
{
    /// <summary>
    /// 列车员信息界面
    /// 分两页显示
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class TrainAttendant : CRH2BaseClass
    {
        /// <summary>
        /// 分类信息
        /// </summary>
        public String[] trainInfo = new string[]
        {
            "车门",
            "火灾报警",
            "非常报警",
            "厕所报警",
            "缺水",
            "洋式1",
            "洋式2",
            "广播",
            "室内照明",
            "车厢指南"
        };

        /// <summary>
        /// 车厢数量 默认为8车
        /// </summary>
        public int trainNum = 8;

        public RectTextInfo[] doorState = new RectTextInfo[16]; //车门状态
        public RectTextInfo[] fireAlarm = new RectTextInfo[16]; //火灾
        public RectTextInfo[] extremAlarm = new RectTextInfo[16]; //非常
        public RectTextInfo[] wcAlarm = new RectTextInfo[8]; //厕所报警
        public RectTextInfo[] waterShort = new RectTextInfo[8]; //缺水
        public RectTextInfo[] style1 = new RectTextInfo[8]; //洋式1
        public RectTextInfo[] style2 = new RectTextInfo[8]; //洋式2
        public RectTextInfo[] boardcast = new RectTextInfo[2]; //广播
        public RectTextInfo[] light = new RectTextInfo[2]; //室内照明
        public RectTextInfo[] guide = new RectTextInfo[16]; //指南

        public RectangleF recPosition = new RectangleF(200, 200, 400, 300); //起始位置
        public int RecTextWidth = 40; //矩形框宽度
        public int RecTextHight = 20; //矩形框高度
        public int rowGap = 10; //行间距

        /// <summary>
        /// 分页显示
        /// 共2页
        /// </summary>
        public int pageNum = 0;

        private TrainView m_TrainView;

        /// <summary>
        /// 存储信息
        /// 1-16为车门各车厢状态,true为打开,false为关闭
        /// 17-32为火灾警报
        /// 33-48为非常警报
        /// 49-56为厕所警报      true为发生,false不显示
        /// 56-64为缺水          true为缺水,false不显示
        /// 64-72为洋式1      
        /// 72-80为洋式2         true为异常,false不显示
        /// 80-82为广播      
        /// 83-84为室内照明    
        /// 85-100为车厢指南
        /// 101 为车类型   true 为16车,false为8车
        /// </summary>
        public override string GetInfo()
        {
            return "列车员信息";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_TrainView.Location = new Point(180, 145);
            }
        }

        public override bool Init()
        {
            m_TrainView = TrainView.Instance;

            InitView();

            return true;
        }

        public override void paint(Graphics g)
        {
            OnDraw(g);
        }

        /// <summary>
        /// 初始化视图图元位置信息
        /// 
        /// </summary>
        public void InitView()
        {
            //矩形框数组初始化
            for (int i = 0; i < trainNum; i++)
            {
                doorState[i] = new RectTextInfo();
                fireAlarm[i] = new RectTextInfo();
                extremAlarm[i] = new RectTextInfo();
                if (i%2 == 0)
                {
                    wcAlarm[i] = new RectTextInfo();
                    waterShort[i] = new RectTextInfo();
                    style1[i] = new RectTextInfo();
                    style2[i] = new RectTextInfo();
                }
                guide[i] = new RectTextInfo();
                if (i%8 == 0)
                {
                    boardcast[i] = new RectTextInfo();
                    light[i] = new RectTextInfo();
                }
            }
            //位置初始化
            for (int i = 0; i < trainNum; i++)
            {
                doorState[i].SetRectTextInfo(recPosition.X + i*RecTextWidth, recPosition.Y, RecTextWidth, RecTextHight,
                    0, "关", 1, 3, 0, 12);
                fireAlarm[i].SetRectTextInfo(recPosition.X + i*RecTextWidth, recPosition.Y + 30, RecTextWidth,
                    RecTextHight, 0, "", 1, 3, 0, 12);
                extremAlarm[i].SetRectTextInfo(recPosition.X + i*RecTextWidth, recPosition.Y + 60, RecTextWidth,
                    RecTextHight, 0, "", 1, 3, 0, 12);
                if (i%2 == 0)
                {
                    wcAlarm[i].SetRectTextInfo(recPosition.X + i*RecTextWidth, recPosition.Y + 90, RecTextWidth,
                        RecTextHight, 0, "", 1, 3, 0, 12);
                    waterShort[i].SetRectTextInfo(recPosition.X + i*RecTextWidth, recPosition.Y + 120, RecTextWidth,
                        RecTextHight, 0, "", 1, 3, 0, 12);
                    style1[i].SetRectTextInfo(recPosition.X + i*RecTextWidth, recPosition.Y + 150, RecTextWidth,
                        RecTextHight, 0, "", 1, 3, 0, 12);
                    style2[i].SetRectTextInfo(recPosition.X + i*RecTextWidth, recPosition.Y + 180, RecTextWidth,
                        RecTextHight, 0, "", 1, 3, 0, 12);
                }
                if (i%8 == 0)
                {
                    boardcast[i].SetRectTextInfo(recPosition.X + i*RecTextWidth, recPosition.Y, RecTextWidth*8,
                        RecTextHight, 0, "全车厢人", 1, 2, 0, 12);
                    light[i].SetRectTextInfo(recPosition.X + i*RecTextWidth, recPosition.Y + 50, RecTextWidth*8,
                        RecTextHight, 0, "全车厢人", 1, 2, 0, 12);

                }
                guide[i].SetRectTextInfo(recPosition.X + i*RecTextWidth, recPosition.Y + 100, RecTextWidth, RecTextHight,
                    0, Convert.ToString(i + 1), 1, 3, 0, 12);
            }
        }

        public void OnDraw(Graphics g)
        {

            if (pageNum == 0)
            {
//显示第一页


                //绘制分类标题
                for (int i = 0; i < 7; i++)
                {
                    g.DrawString(trainInfo[i], CRH2Resource.Font12, CRH2Resource.WhiteBrush,
                        new RectangleF(5, recPosition.Y + i*30, 100, 50));
                }
                //绘制各信息状态矩形框
                for (int i = 0; i < trainNum; i++)
                {
                    doorState[i].OnDraw(g);
                    fireAlarm[i].OnDraw(g);
                    extremAlarm[i].OnDraw(g);
                    if (i%2 == 0)
                    {
                        wcAlarm[i].OnDraw(g);
                        waterShort[i].OnDraw(g);
                        style1[i].OnDraw(g);
                        style2[i].OnDraw(g);
                    }

                }

                g.DrawImage(ImageResource.blueb, 429, 450, ImageResource.blueb.Width, ImageResource.blueb.Height);
                g.DrawString("下一页面", CRH2Resource.Font12, CRH2Resource.WhiteBrush,
                    new Rectangle(429, 450, ImageResource.blueb.Width, ImageResource.blueb.Height),
                    CRH2Resource.DrawFormat);


            }
            else
            {
                //显示第二页
                //绘制分类标题
                for (int i = 7; i < 10; i++)
                {
                    g.DrawString(trainInfo[i], CRH2Resource.Font12, CRH2Resource.WhiteBrush,
                        new RectangleF(5, recPosition.Y + (i - 7)*50, 100, 50));
                }
                //绘制各信息状态矩形框
                for (int i = 0; i < trainNum; i++)
                {
                    guide[i].OnDraw(g);
                    if (i%8 == 0)
                    {
                        boardcast[i].OnDraw(g);
                        light[i].OnDraw(g);
                    }

                }

                g.DrawImage(ImageResource.blueb, 429, 450, ImageResource.blueb.Width, ImageResource.blueb.Height);
                g.DrawString("上一页面", CRH2Resource.Font12, CRH2Resource.WhiteBrush,
                    new Rectangle(429, 450, ImageResource.blueb.Width, ImageResource.blueb.Height),
                    CRH2Resource.DrawFormat);

            }

        }

        protected override bool OnMouseDown(Point nPoint)
        {
            return OnButtonClick(nPoint.X, nPoint.Y);
        }

        /// <summary>
        /// 页面切换点击处理
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public bool OnButtonClick(int x, int y)
        {
            if (429 <= x && x <= 429 + 117 && y >= 450 && y <= 450 + 43)
            {
                pageNum = pageNum == 0 ? 1 : 0;

                return true;
            }

            return false;
        }
    }
}
