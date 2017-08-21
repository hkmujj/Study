using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Engine.Turkmenistan.LKJ.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.Turkmenistan.LKJ.公共组件
{

    [GksDataType(DataType.isMMIObjectClass)]
    public class VcC1ViewChangeButton : baseClass
    {
        #region 私有变量

        private readonly List<Image> m_ResourceImage = new List<Image>();    //图片资源
        private readonly List<Button> m_BtnsDownTabView = new List<Button>();//按钮列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共试图-视图切换按钮";
        }

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath , a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            String[] strsBtnTabView = new String[5] { "警惕", "缓解","调车", "开车", "解锁" };
            ButtonStyle bs = new ButtonStyle() { FontStyle = FontStyles.FsSong11CcB, Background = null, DownImage = null };
            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button(
                    null,
                    new Rectangle(46 + i * 72, 455, 50, 53),
                    i,
                    bs,
                    false
                    );

                btn.ClickEvent += Btn_ClickEvent;
                btn.DownEvent += Btn_DownEvent;
                m_BtnsDownTabView.Add(btn);
            }

            return true;
        }


        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            m_BtnsDownTabView.ToList().ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            m_BtnsDownTabView.ToList().ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {


            switch (e.Message)
            {
                case 0:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                    break;
                case 1:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                    break;
                case 2:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 0, 0);
                    break;
                case 3:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[3], 0, 0);
                    break;
                case 4:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[4], 0, 0);
                    break;
                default:
                   
                    break;
            }
        }
        void Btn_DownEvent(object sender, ClickEventArgs<int> e)
        {


            switch (e.Message)
            {
                case 0:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                    break;
                case 1:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                    break;
                case 2:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 1, 0);
                    break;
                case 3:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[3], 1, 0);
                    break;
                case 4:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[4], 1, 0);
                    break;
                default:

                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            //for (int i = 0; i < 5; i++)
            //{
            //    if (this._btns_Down_TabView[i].IsReplication)
            //    {
            //        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + i, 0, 0);
            //    }
            //}

                m_BtnsDownTabView.ToList().ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }
        #endregion

    }
}
