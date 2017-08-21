using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;


namespace NJ_MMI
{

    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    class ShowText : baseClass 
    {
        private string[] line;
        private List <string > id =new List <string >();
        private List<string> level = new List<string>();
        private List<string> textup = new List<string>();
        private List<string> textdown = new List<string>();
        private List<int> level1 = new List<int>();
        private int temp = 0;     
        public override string GetInfo()
        {
            return "显示内容";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            System.Text.Encoding.GetEncoding("gb2312");
            line = File.ReadAllLines(@"1D\config\显示内容配置.txt", System.Text.Encoding.Default);
            for (int i = 0; i < line.Length; i++)
            {
                int index0 = line[i].IndexOf("[");
                id.Add(line[i].Substring(0, index0));
                int index1 = line[i].IndexOf("]");
                int index2 = line[i].IndexOf("_");
                level.Add(line[i].Substring(index0 + 1, index1 - 3));
                if (index2 > -1)
                {
                    textup.Add(line[i].Substring(index1 + 1, index2 - 5));
                    textdown.Add(line[i].Substring(index2 + 1));
                }
                else
                {
                    textup.Add(line[i].Substring(index1 + 1));
                    textdown.Add(" ");
                }
            }
            return true;
        }
        public override void paint(Graphics g)
        {
            if (!BoolList[UIObj.InBoolList[2]])
            {
            for (int i = 0; i < id.Count; i++)
            {
                int ID = Convert.ToInt32(id[i]);
                if (BoolList[ID])
                {
                    if(level1.Count>4)
                    {
                        level1.Clear();
                    }
                    level1.Add(Convert.ToInt32(level[i]));
                }
                if (!BoolList[ID])
                {
                    level1.Remove(Convert.ToInt32(level[i]));
                }
            }
            for (int j = 0; j < level1.Count; j++)
            {
                int num = Convert.ToInt32(level1[j]);
                if (num > temp)
                {
                    temp = num;
                }
            }
            for (int j = 0; j < level.Count; j++)
            {
                if (Convert.ToInt32(level[j]) == temp)
                {
                    g.DrawString(textup [j], Global.Font_Arial_11_B, Global.brush1, new Rectangle(20, 470, 400, 20), Global.SF_NC);
                    g.DrawString(textdown [j], Global.Font_Arial_11_B, Global.brush1, new Rectangle(20, 490, 400, 20), Global.SF_NC);
                    temp = 0;
                }      
                if (level1 .Count==0)
                {
                    append_postCmd(CmdType.ChangePage, 104, 0, 0);
                }
            }}
             
            if (BoolList[UIObj.InBoolList [0]])
            {
                append_postCmd(CmdType.ChangePage, 104, 0, 0);
            }
            if (!BoolList[UIObj.InBoolList[1]])
            {
                append_postCmd(CmdType.ChangePage, 103, 0, 0);
            }
            if (BoolList[UIObj.InBoolList[2]])
            {
                append_postCmd(CmdType.ChangePage, 104, 0, 0);
            }
            base.paint(g);
        }
    }
}
