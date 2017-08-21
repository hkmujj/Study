using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3C.Trial
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class TrialMenuView : baseClass, IButtonBtnEventResponser
    {
        private List<HXD3ButtonProxy> m_ContentBtns;

        public override bool init(ref int nErrorObjectIndex)
        {
            ButtomButtonView.Instance.AddResponser(this);

            var size = new Size(145, 75);

            var o = new Point(137, 212);

            var hi = 30;
            var vi = 25;
            m_ContentBtns = new List<HXD3ButtonProxy>(8);
            var conts = GetContents().GetEnumerator();
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    conts.MoveNext();
                    m_ContentBtns.Add(new HXD3ButtonProxy()
                    {
                        OutLineRectangle =
                            new Rectangle(o.X + size.Width*j + hi*j, o.Y + size.Height*i + vi*i, size.Width, size.Height)
                            ,
                          Text = conts.Current.Item1,
                          ButtonUpEvent = conts.Current.Item2
                    });
                }
            }

            m_ContentBtns.Remove(m_ContentBtns.Last());

            return true;
        }


        private IEnumerable<Tuple<string, EventHandler>> GetContents()
        {
            yield return new Tuple<string, EventHandler>("主司控器试验", (sender, args) => { });
            yield return new Tuple<string, EventHandler>("起动试验", (sender, args) => { });
            yield return new Tuple<string, EventHandler>("零级位试验", (sender, args) => { });
            yield return new Tuple<string, EventHandler>("辅助电源试验", (sender, args) => { });
            yield return new Tuple<string, EventHandler>("显示灯试验", (sender, args) => { });
            yield return new Tuple<string, EventHandler>("无人警惕试验", (sender, args) => { append_postCmd(CmdType.ChangePage, 217, 0, 0); });
            yield return new Tuple<string, EventHandler>("轮缘润滑试验", (sender, args) => { });
            yield return new Tuple<string, EventHandler>("顶轮试验", (sender, args) => { });
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>(Common.Str219);
            }
        }

        public override void paint(Graphics g)
        {
            m_ContentBtns.ForEach(e => e.OnDraw(g));
        }

        public override bool mouseDown(Point point)
        {
            return m_ContentBtns.Any(a => a.OnMouseDown(point));
        }

        public override bool mouseUp(Point point)
        {
            return m_ContentBtns.Any(a => a.OnMouseUp(point));
        }

        public bool Response(int btnIndex)
        {
            if (GlobalParam.Instance.CurrentViewID == 218)
            {
                switch (btnIndex)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;

                    case 8:
                        this.NavigateReturn();
                        break;
                }

                return true;
            }
            return false;
        }
    }
}