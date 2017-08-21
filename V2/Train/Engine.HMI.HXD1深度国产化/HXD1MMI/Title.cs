using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 目录信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class Title : baseClass
    {
        private List<RectText> _upperRectTextList = new List<RectText>();
        //private string[] _statusArray = new string[] { "", "主界面", "主要数据", "维护", "参数配置", "牵引数据", "隔离解锁" };

        public static string CurrentView = "主界面";

        private int _statusValue;
        public override string GetInfo()
        {
            return "目录信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _upperRectTextList.Add(new RectText(new Rectangle(2, 2 + Common.InitialPosY, 40, 27), "A0", 13, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true, null, true));

            _upperRectTextList.Add(new RectText(new Rectangle(42, 2 + Common.InitialPosY, 180, 27), "", 13, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true));

            _upperRectTextList.Add(new RectText(new Rectangle(222, 2 + Common.InitialPosY, 200, 27), "", 13, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true));

            _upperRectTextList.Add(new RectText(new Rectangle(422, 2 + Common.InitialPosY, 183, 27), "HXD1 1001", 13, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true, null, true));

            _upperRectTextList.Add(new RectText(new Rectangle(605, 2 + Common.InitialPosY, 193, 27), "", 13, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true));

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            _upperRectTextList[1].Text = CurrentView;
            _upperRectTextList[4].Text = DateTime.Now.ToString();

            if (BoolList[1187])
            {
                _upperRectTextList[3].Text = "HXD1 1001A";
            }

            if (BoolList[1188])
            {
                _upperRectTextList[3].Text = "HXD1 1001B";
            }

            foreach (var v in _upperRectTextList)
            {
                v.OnDraw(dcGs);
            }
        }
    }
}
