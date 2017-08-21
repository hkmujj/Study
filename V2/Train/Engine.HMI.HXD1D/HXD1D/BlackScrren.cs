using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using HXD1D.Titlte;
using HXD1D.控制设置;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;


namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BlackScrren : baseClass
    {
        private List<int> BoolIds = new List<int>();
        public override string GetInfo()
        {
            return "黑屏";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            InttData();

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            DrawView();
        }

        private void DrawView()
        {


            if (BoolList[BoolIds[0]])
            {
                Record.GoNextView(this, 1);
                //append_postCmd(CmdType.ChangePage, 1, 0, 0); 
            }
        }

        private void InttData()
        {
            //取出配置表Boolids编号

            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    BoolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
        }
    }
}
