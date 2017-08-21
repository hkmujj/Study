using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using Engine.TCMS.HXD3C.Config;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;

namespace Engine.TCMS.HXD3C.Utils
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class GlobalParam : baseClass
    {
        public PasswordConfig PasswordConfig { get; private set; }

        public static GlobalParam Instance { get; private set; }

        /// <summary>
        /// 当前视图ID
        /// </summary>
        public int CurrentViewID { private set; get; }

        public List<int> LastViewIDs{private set; get; }

        public bool NavigetByReture { set; get; }

        public override bool init(ref int nErrorObjectIndex)
        {
            Instance = this;

            LastViewIDs = new List<int>();

            InitData();

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                var viewID = (int) nParaC;
                if (!NavigetByReture)
                {
                    if (LastViewIDs.Contains(viewID) )
                    {
                        LastViewIDs.Add(viewID);
                        if (LastViewIDs.Count > 10)
                        {
                            LastViewIDs.Remove(LastViewIDs.First());
                        }
                    }
                    else
                    {
                        LastViewIDs.Remove(viewID);
                        LastViewIDs.Add(viewID);
                    }
                }
                NavigetByReture = false;
            }

            CurrentViewID = nParaB;
        }

        void InitData()
        {
            LoadPassWordFile();
        }
        private void LoadPassWordFile()
        {
            PasswordConfig =
                DataSerialization.DeserializeFromXmlFile<PasswordConfig>(Path.Combine(RecPath,
                    "..\\config\\PasswordConfig.xml"));
        }

    }
}