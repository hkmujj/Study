using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BlackScreen : baseClass
    {
        /// <summary>
        /// 坐标集
        /// </summary>
        public Rectangle m_Rects;

        public static SolidBrush m_Background = new SolidBrush(Color.FromArgb(100, 0, 0, 0));

        public override string GetInfo()
        {
            return "黑屏";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            //3
            nErrorObjectIndex = -1;

            m_Rects = new Rectangle(0, 0, 800, 600);

            SetWhenDebug();

            return true;
        }

        private void SetWhenDebug()
        {
            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 0);
        }

        public override void paint(Graphics g)
        {
            GetValue();

            DrawOn(g);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        private void GetValue()
        {
            if (BoolList[UIObj.InBoolList[0]])
            {
                append_postCmd(CmdType.ChangePage, 201, 0, 0);
            }
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            e.FillRectangle(m_Background, m_Rects);
        }
    }
}