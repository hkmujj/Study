using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common.Global;

namespace CRH2MMI.Tow2
{
    internal class Tow2Resource
    {
        private TowInfo2 m_TowInfo2;

        private readonly TowInfo2Config m_TowInfo2Config;

        private Dictionary<string, List<int>> m_CarNoBoolIdxDic;

        public Tow2Resource(TowInfo2 towInfo2)
        {
            m_TowInfo2 = towInfo2;
            m_TowInfo2Config = GlobalInfo.CurrentCRH2Config.TowInfo2Config;
            Tow2CarNamse = m_TowInfo2Config.Cars.Select(s => s.CarName).ToList();
            m_CarNoBoolIdxDic = new Dictionary<string, List<int>>();
        }

        public List<string> Tow2CarNamse { private set; get; }

        public static readonly List<Tow2NameInfo> Tow2NameInfoList = new List<Tow2NameInfo>()
        {
            new Tow2NameInfo() {Name = " 恒速", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = string.Empty, ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 制动", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 动力", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 后向", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 前向", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = string.Empty, ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 空挡", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 接触器K闭合", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 充电不良", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 再生制动过大", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 再生制动失效", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 复位", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 空转", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " OVTh误动作", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 直流过电压3", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 变流器箱内温度高", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 直流低电压2", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 二次侧过电流1", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 二次侧过电流2", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 三次侧低电压", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 三次侧过电压", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 接触网无电", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " CI故障", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 主电路元件异常", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 冷却器温度高", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 控制电源低电压", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 牵引电机电流不平衡", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 牵引电机过流1", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 牵引电机过电流2", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " PG故障", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 通风机工作", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 主变压器异常", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 牵引不动作", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 主电路工作牵引", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 主电路工作再生", ActiveColor = Color.FromArgb(0, 255, 0)},
            new Tow2NameInfo() {Name = " 门极电源故障", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 直流100V异常", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 二次侧接地1", ActiveColor = Color.Red},
            new Tow2NameInfo() {Name = " 二次侧接地2", ActiveColor = Color.Red},
        };

        public static readonly ReadOnlyCollection<Tow2NameInfo> Tow2FinalNameInfoList = Tow2NameInfoList.FindAll(f => !string.IsNullOrEmpty(f.Name)).AsReadOnly();

        /// <summary>
        ///  ,,  
        /// </summary>
        /// <param name="carIdx"></param>
        /// <param name="infoIdx">信息编号</param>
        /// <param name="trainName"></param>
        /// <returns></returns>
        public bool GetStateOf(string trainName,int carIdx, int infoIdx)
        {
            //return m_TowInfo2.BoolList[m_TowInfo2.UIObj.InBoolList[m_TowInfo2Config.Cars[trainNo].InBoolOffset + infoIdx]];
            if (!m_CarNoBoolIdxDic.ContainsKey(trainName))
            {
                m_CarNoBoolIdxDic.Add(trainName, GlobalResource.Instance.GetInBoolIndexs(
                    m_TowInfo2Config.Cars[carIdx].InBoolColoumNames));
            }

            return m_TowInfo2.BoolList[m_CarNoBoolIdxDic[trainName][infoIdx]];
        }
    }
}
