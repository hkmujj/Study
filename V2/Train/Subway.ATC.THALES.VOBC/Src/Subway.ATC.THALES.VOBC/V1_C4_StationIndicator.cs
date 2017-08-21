using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Service;

namespace Subway.ATC.THALES.VOBC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_C4_StationIndicator : baseClass
    {
        private string[] doorStates;
        private string[] stationStates;
        //private List<String> _stationList = new List<String>();
        private string _nextStation = string.Empty;
        private Dictionary<int, string> _stations = new Dictionary<int, string>();

        private IStationNameProviderService m_StationNameProviderService;

        public override string GetInfo()
        {
            return "车站指示";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            doorStates = new string[2] { "< ", " >" };
            stationStates = new string[4] { "StationHold", "", "StationSkip", "" };

            m_StationNameProviderService = DataPackage.ServiceManager.GetService<IStationNameProviderService>();

            string[] strs = File.ReadAllLines(Path.Combine(RecPath, "..\\config\\车站信息.txt"), Encoding.Default);
            for (int i = 0; i < strs.Length; i++)
            {
                string[] strs_ = strs[i].Split(':');
                _stations.Add(Convert.ToInt32(strs_[0]), strs_[1]);
            }

            return true;
        }

        private int ParserStationIndex(float value)
        {
            var stationId =
                BitConverter.ToInt32(
                    BitConverter.GetBytes(value), 0);

            return stationId;
        }

        private string GetStationName(float value)
        {
            var idx = ParserStationIndex(value);

            if (m_StationNameProviderService.StationDictionary != null && m_StationNameProviderService.StationDictionary.ContainsKey(idx))
            {
                return m_StationNameProviderService.StationDictionary[idx].Name;
            }

            if (_stations.ContainsKey(idx))
            {
                return _stations[idx];
            }

            // 兼容老的方式
            if (_stations.ContainsKey((int)value))
            {
                return _stations[(int) value];
            }

            return string.Empty;

        }

        public override void paint(Graphics dcGs)
        {
            Font f = new Font("Arial", 15, FontStyle.Bold);

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    Brush b = new SolidBrush(Color.Red);
                    dcGs.DrawString(stationStates[i], f, b, new PointF(518, 390));
                    break;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    Brush b = new SolidBrush(Color.Red);
                    dcGs.DrawString(stationStates[i + 2], f, b, new PointF(663, 390));
                    break;
                }
            }

            _nextStation = GetStationName(FloatList[UIObj.InFloatList[0]]);

            if (string.IsNullOrWhiteSpace(_nextStation))
            {
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    if (i == 0)
                    {
                        _nextStation = "< " + _nextStation;
                    }
                    else
                    {
                        _nextStation += " >";
                    }
                    break;
                }
            }


            dcGs.DrawString(_nextStation, f, new SolidBrush(Color.FromArgb(0, 204, 255)),
                new RectangleF(506, 345, 140, 29),
                new StringFormat() {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center}); //
            dcGs.DrawString(GetStationName(FloatList[UIObj.InFloatList[1]]), f, new SolidBrush(Color.FromArgb(0, 204, 255)),
                new RectangleF(651, 345, 140, 29),
                new StringFormat() {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center});
        }
    }
}
