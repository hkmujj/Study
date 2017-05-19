using System;
using System.Diagnostics;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    [ExcelLocation("主运行界面制动子界面内容和接口.xls", "Sheet1")]
    [DebuggerDisplay("CarIndex={CarIndex}, BrakeLocation={BrakeLocation}, BrakeStatus={BrakeStatus}")]
    public class BrakeUnit : NotificationObject, ISetValueProvider
    {
        private BrakeStatus m_BrakeStatus;

        public BrakeStatus BrakeStatus
        {
            set
            {
                if (value == m_BrakeStatus)
                {
                    return;
                }
                m_BrakeStatus = value;
                RaisePropertyChanged(() => BrakeStatus);
            }
            get { return m_BrakeStatus; }
        }

        [ExcelField("CarIndex")]
        public int CarIndex { private set; get; }

        [ExcelField("BrakeLocation")]
        public int BrakeLocation { private set; get; }

        [ExcelField("ParkBrakeExertInterface")]
        public string ParkBrakeExertInterface { private set; get; }

        [ExcelField("ParkBrakeCutInterface")]
        public string ParkBrakeCutInterface { private set; get; }

        [ExcelField("ParkBrakeRelieveInterface")]
        public string ParkBrakeRelieveInterface { private set; get; }

        [ExcelField("ParkBrakeUnkownInterface")]
        public string ParkBrakeUnkownInterface { private set; get; }

        [ExcelField("NormalBrakeCutInterface")]
        public string NormalBrakeCutInterface { private set; get; }

        [ExcelField("NormalBrakeExertInterface")]
        public string NormalBrakeExertInterface { private set; get; }

        [ExcelField("NormalBrakeRelieveInterface")]
        public string NormalBrakeRelieveInterface { private set; get; }

        [ExcelField("NormalBrakeUnkownInterface")]
        public string NormalBrakeUnkownInterface { private set; get; }

        public string GetIndexName(BrakeStatus brake)
        {
            switch (brake)
            {
                case BrakeStatus.ParkBrakeExert:
                    return ParkBrakeExertInterface;
                case BrakeStatus.ParkBrakeCut:
                    return ParkBrakeCutInterface;
                case BrakeStatus.ParkBrakeRelieve:
                    return ParkBrakeRelieveInterface;
                case BrakeStatus.ParkBrakeUnkown:
                    return ParkBrakeUnkownInterface;
                case BrakeStatus.NormalBrakeCut:
                    return NormalBrakeCutInterface;
                case BrakeStatus.NormalBrakeExert:
                    return NormalBrakeExertInterface;
                case BrakeStatus.NormalBrakeRelieve:
                    return NormalBrakeRelieveInterface;
                case BrakeStatus.NormalBrakeUnkown:
                    return NormalBrakeUnkownInterface;
                default:
                    return String.Empty;
            }
        }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "CarIndex":
                    CarIndex = Convert.ToInt32(value);
                    break;
                case "BrakeLocation":
                    BrakeLocation = Convert.ToInt32(value);
                    break;
                case "ParkBrakeExertInterface":
                    ParkBrakeExertInterface = value;
                    break;
                case "ParkBrakeCutInterface":
                    ParkBrakeCutInterface = value;
                    break;
                case "ParkBrakeRelieveInterface":
                    ParkBrakeRelieveInterface = value;
                    break;
                case "ParkBrakeUnkownInterface":
                    ParkBrakeUnkownInterface = value;
                    break;
                case "NormalBrakeCutInterface":
                    NormalBrakeCutInterface = value;
                    break;
                case "NormalBrakeExertInterface":
                    NormalBrakeExertInterface = value;
                    break;
                case "NormalBrakeRelieveInterface":
                    NormalBrakeRelieveInterface = value;
                    break;
                case "NormalBrakeUnkownInterface":
                    NormalBrakeUnkownInterface = value;
                    break;
            }
        }
    }
}