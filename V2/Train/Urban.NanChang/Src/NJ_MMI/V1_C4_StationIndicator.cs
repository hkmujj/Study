using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.PublicUI;
using System.IO;

namespace NJ_MMI
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_C4_StationIndicator : baseClass
    {
        private String[] doorStates;
        private String[] stationStates;
        //private List<String> _stationList = new List<String>();
        private String _nextStation = String.Empty;
        private Dictionary<Int32, String> _stations = new Dictionary<int, string>();

        public override string GetInfo()
        {
            return "车站指示";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            this.doorStates = new String[2] { "< ", " >" };
            this.stationStates = new String[4] { "StationHold", "", "StationSkip", "" };

            string[] strs = File.ReadAllLines(Path.Combine(RecPath, "..\\config\\车站信息.txt"), System.Text.Encoding.Default);
            for (int i = 0; i < strs.Length; i++)
            {
                String[] strs_ = strs[i].Split(':');
                _stations.Add(Convert.ToInt32(strs_[0]), strs_[1]);
            }

            return true;
        }

        //public override bool canSetStringList(int nPara)
        //{
        //    if (nPara == 1)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public override void addStringByLine(int nIndex, string cStr)
        //{
        //    if (nIndex == 0)
        //    {
        //        _stationList.Add(cStr);
        //    }
        //    _stationList.Add(cStr);
        //}

        public override void paint(Graphics dcGs)
        {
            Font f = new Font("Arial", 15, FontStyle.Bold);

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    Brush b = new SolidBrush(Color.Red);
                    dcGs.DrawString(this.stationStates[i], f, b, new PointF(518, 390));
                    break;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    Brush b = new SolidBrush(Color.Red);
                    dcGs.DrawString(this.stationStates[i+2], f, b, new PointF(663, 390));
                    break;
                }
            }

            if (this._stations != null && this._stations.Count != 0)
            {
                if ((Int32)FloatList[UIObj.InFloatList[0]]<0)
                    return;
                if (!_stations.ContainsKey((Int32)FloatList[UIObj.InFloatList[0]]))
                    return;
                this._nextStation = this._stations[(Int32)FloatList[UIObj.InFloatList[0]]];

                for (int i = 0; i < 2; i++)
                {
                    if (BoolList[UIObj.InBoolList[0] + i])
                    {
                        if (i == 0)
                            this._nextStation = "< " + this._nextStation;
                        else
                            this._nextStation += " >";
                        break;
                    }
                }


                dcGs.DrawString(this._nextStation, f, new SolidBrush(Color.FromArgb(0, 204, 255)), new RectangleF(506, 345, 140, 29), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });//
                dcGs.DrawString(this._stations[(Int32)FloatList[UIObj.InFloatList[1]]], f, new SolidBrush(Color.FromArgb(0, 204, 255)), new RectangleF(651, 345, 140, 29), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }

            base.paint(dcGs);
        }
    }
}
