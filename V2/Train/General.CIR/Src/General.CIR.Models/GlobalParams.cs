using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using Excel.Interface;
using General.CIR.CIRData;
using General.CIR.Config;
using General.CIR.Events;
using General.CIR.Models.Units;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace General.CIR.Models
{
    public class GlobalParams
    {
        //[CompilerGenerated]
        //[Serializable]
        //private sealed class <>c
        //{
        //	public static readonly GlobalParams.<>c <>9 = new GlobalParams.<>c();

        //	public static Func<EngineType, bool> <>9__2_0;

        //	public static Func<EngineType, int> <>9__2_1;

        //	public static Func<EngineType, EngineType> <>9__2_2;

        //	internal bool <Initialize>b__2_0(EngineType w)
        //	{
        //		return w.Number != 0;
        //	}

        //	internal int <Initialize>b__2_1(EngineType t)
        //	{
        //		return t.Number;
        //	}

        //	internal EngineType <Initialize>b__2_2(EngineType t)
        //	{
        //		return t;
        //	}
        //}

        private SystemConfig m_SystemConfig;

        private CIRCommAgent.OnReceive m_Receive;

        private NetWorkDataEvent m_Event;

        public OutLib OutLib
        {
            get;
            private set;
        }

        public IDictionary<int, EngineType> AllEngineTypes
        {
            get;
            private set;
        }

        public static GlobalParams Instance
        {
            get;
            private set;
        }

        public MainInsatnce MainInsatnce
        {
            get;
            private set;
        }

        public SystemConfig SystemConfig
        {
            get
            {
                bool flag = m_SystemConfig == null;
                if (flag)
                {
                    string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
                    SystemConfig = DataSerialization.DeserializeFromXmlFile<SystemConfig>(Path.Combine(text, "SystemConfig.xml"));
                    m_SystemConfig.AppConfigPath = text;
                }
                return m_SystemConfig;
            }
            private set
            {
                m_SystemConfig = value;
            }
        }

        static GlobalParams()
        {
            Instance = new GlobalParams();
        }

        public void Initialize()
        {
            MainInsatnce = DataSerialization.DeserializeFromXmlFile<MainInsatnce>(Path.Combine(SystemConfig.AppConfigPath, "MainInstacne.xml"));
            OutLib = DataSerialization.DeserializeFromXmlFile<OutLib>(Path.Combine(SystemConfig.AppConfigPath, "OutLib.xml"));
            IEnumerable<EngineType> arg_73_0 = ExcelParser.Parser<EngineType>(SystemConfig.AppConfigPath);




            AllEngineTypes = ExcelParser.Parser<EngineType>(SystemConfig.AppConfigPath).ToDictionary(t => t.Number, t => t);

            m_Event = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NetWorkDataEvent>();
            CIRCommAgent.InitCIR("127.0.0.1", 20001, 20000);
            m_Receive = CallBackAction;
            CIRCommAgent.SetRecvHandler(m_Receive);
        }

        public void UnInit()
        {
            CIRCommAgent.UnInitCIR();
        }

        public void RequstResgisterEngineNumber()
        {
            CIRPacket cIRPacket = default(CIRPacket);
            cIRPacket.Init();
            cIRPacket.SetHeadInfo(3, 1, 3, 18);
            cIRPacket.SetData(CIRCommAgent.StructToBytes(new TrainIDRegistRequest
            {
                Regist = 1,
                TrainID = SystemConfig.EngineNumber
            }));
            Debug.WriteLine("请求注册机车号pack.GetPackLen()：{0}", new object[]
            {
                cIRPacket.GetPackLen()
            });
            CIRCommAgent.SendCIRData(CIRCommAgent.StructToBytes(cIRPacket), cIRPacket.GetDataLen(), false, 0);
        }

        public void ReuqestRegisterTrainNumber()
        {
            CIRPacket cIRPacket = default(CIRPacket);
            cIRPacket.Init();
            cIRPacket.SetHeadInfo(3, 1, 3, 17);
            cIRPacket.SetData(CIRCommAgent.StructToBytes(new TrainNumRegistRequest
            {
                Regist = 1,
                Order = 0,
                TrainNum = SystemConfig.TrainNumber
            }));
            Debug.WriteLine("请求注册车次号pack.GetPackLen()：{0}", new object[]
            {
                cIRPacket.GetPackLen()
            });
            CIRCommAgent.SendCIRData(CIRCommAgent.StructToBytes(cIRPacket), cIRPacket.GetDataLen(), false, 0);
        }

        private void CallBackAction(IntPtr bufferptr, int nlen)
        {
            CIRPacket cIRPacket = (CIRPacket)CIRCommAgent.PtrToStruct(bufferptr, nlen, typeof(CIRPacket));

            //StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "info.txt"), true);

            //sw.WriteLine($"Cbcmd:{cIRPacket.Cbcmd.ToString("X2")},Cbop:{cIRPacket.Cbop.ToString("X2")},Buff:{cIRPacket.Buff}");

            //sw.Close();
            //sw.Dispose();

            //Debug.WriteLine($"{cIRPacket.Cbcmd},{cIRPacket.Cbop}");
            //AppLog.Info($"{cIRPacket.Cbcmd},{cIRPacket.Cbop}");

            m_Event.Publish(new NetWorkEventArgs
            {
                BusinessType = (BusinessType)cIRPacket.Cbop,
                Data = cIRPacket
            });
        }
    }
}
