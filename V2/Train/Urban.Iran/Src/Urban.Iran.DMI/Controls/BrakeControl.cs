using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using CommonUtil.Controls;
using CommonUtil.Model;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using Urban.Iran.DMI.Model;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI.Controls
{
    public class BrakeControl : RectangleImage
    {
        public BrakeType BrakeType { set; get; }

        private readonly CommonUtil.Model.IReadOnlyDictionary<BrakeType, Image> m_BrakeTypeImageDictionary;

        public ReadOnlyCollection<Tuple<BrakeType, int, int>> BrakeTypeIndexCollection { set; get; }

        private readonly baseClass m_SrcObj;

        public BrakeControl(baseClass srcObj)
        {
            m_SrcObj = srcObj;

            m_BrakeTypeImageDictionary = new ReadOnlyDictionary<BrakeType, Image>(new Dictionary<BrakeType, Image>()
            {
                {BrakeType.None, null},
                {BrakeType.Ordinary, ImageResourceFacade.iAtpB},
                {BrakeType.Emergent, ImageResourceFacade.iAtpA},
                { BrakeType.OrdinaryFlicker, ImageResourceFacade.iAtpB }
            });
        }

        public override void Refresh()
        {
            BrakeType = BrakeType.None;
            var t = BrakeTypeIndexCollection.FirstOrDefault(f => m_SrcObj.BoolList[f.Item2]);
            if (t != null)
            {
                BrakeType = t.Item1;
            }

            Image = m_BrakeTypeImageDictionary[BrakeType];

            base.Refresh();
        }



        /// <summary/>
        /// <param name="g"/>
        public override void OnDraw(Graphics g)
        {
            if (BrakeType == BrakeType.OrdinaryFlicker)
            {
                if (DateTime.Now.Second % 2 == 0)
                {
                    g.DrawImage(Image, OutLineRectangle);
                }
            }
            else
            {
                if (Image != null)
                {
                    g.DrawImage(Image, OutLineRectangle);
                }

            }

            //base.OnDraw(g);
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"/>
        public override bool OnMouseDown(Point point)
        {
            if (OutLineRectangle.Contains(point) && BrakeType == BrakeType.OrdinaryFlicker)
            {
                var temp = BrakeTypeIndexCollection.FirstOrDefault(f => f.Item1 == BrakeType.OrdinaryFlicker);

                if (temp == null)
                {
                    return false;
                }
                var index = temp.Item3;

                if (index != null)
                {
                    m_SrcObj.append_postCmd(CmdType.SetBoolValue, (int)index, 1, 0);
                }

            }
            return true;
        }

        /// <summary>
        /// 鼠标按下后弹起
        /// </summary>
        /// <param name="point"/>
        public override bool OnMouseUp(Point point)
        {
            if (OutLineRectangle.Contains(point))
            {

                var temp = BrakeTypeIndexCollection.FirstOrDefault(f => f.Item1 == BrakeType.OrdinaryFlicker);

                if (temp == null)
                {
                    return false;
                }
                var index = temp.Item3;

                if (index != null)
                {
                    m_SrcObj.append_postCmd(CmdType.SetBoolValue, (int)index, 0, 0);
                }

            }
            return true;
        }
    }
}