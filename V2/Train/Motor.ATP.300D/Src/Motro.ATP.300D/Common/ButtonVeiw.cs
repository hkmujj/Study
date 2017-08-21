using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;
using MMI.Facility.Interface.Attribute;

namespace Motor.ATP._300D.Common
{
    [GksDataType(DataType.isMMIObjectClass)]
    class ButtonVeiw : ATPBase
    {
        public static ButtonVeiw Instance { private set; get; }

        private List<CommonInnerControlBase> m_Collection;

        private List<IButtonResponser> m_ButtonResponsers;

        public void AddResponser(IButtonResponser responser)
        {
            m_ButtonResponsers.Add(responser);
        }

        public override bool Initalize()
        {
            Instance = this;
            m_Collection = new List<CommonInnerControlBase>();
            m_ButtonResponsers = new List<IButtonResponser>();
            var names =
                Enum.GetValues(typeof(ButtonType)).Cast<int>().Select(s => EnumUtil.GetDescription((ButtonType)s)).ToList().Take(20);
            var region = GetRectangle().GetEnumerator();
            m_Collection.AddRange(names.Select(s =>
            {
                region.MoveNext();
                return new GDIRectText()
                {
                    Text = s[0],
                    OutLineRectangle = region.Current,
                    NeedDarwOutline = true,
                    BackColorVisible = false,
                    TextFormat = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    }
                };
            }));

            return base.Initalize();
        }

        private IEnumerable<Rectangle> GetRectangle()
        {
            for (int i = 0; i < 11; i++)
            {
                yield return new Rectangle(10 + i * 72, 605, 60, 60);
            }
            for (int i = 0; i < 9; i++)
            {
                yield return new Rectangle(805, 5 + i * 75, 65, 65);
            }

        }

        public override void paint(Graphics g)
        {
            m_Collection.ForEach(f => f.OnDraw(g));
        }

        public override bool mouseDown(Point point)
        {
            var index = m_Collection.TakeWhile(controlBase => !controlBase.OutLineRectangle.Contains(point)).Count();
            if (index<m_Collection.Count)
            {
                //Background2D.ChangePressedButtonType((ButtonType)index);
                m_ButtonResponsers.ForEach(a => a.ResponseDown((ButtonType) index));
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            var index = m_Collection.TakeWhile(controlBase => !controlBase.OutLineRectangle.Contains(point)).Count();
            if (index < m_Collection.Count)
            {
                //Background2D.ChangePoppedButtonType((ButtonType)index);
                m_ButtonResponsers.ForEach(a => a.ResponseUp((ButtonType)index));
            }
            return true;
        }
    }
}