using System;
using System.Collections;
using CBTC.DataAdapter.ConcreateAdapter.BOMBARDIER;
using CBTC.DataAdapter.ConcreateAdapter.CASCO;
using CBTC.DataAdapter.ConcreateAdapter.QuanLuTong;
using CBTC.DataAdapter.ConcreateAdapter.SIEMENS;
using CBTC.DataAdapter.ConcreateAdapter.TCT;
using CBTC.DataAdapter.ConcreateAdapter.THALES;
using CBTC.Infrasturcture.Model.Constant;

namespace CBTC.DataAdapter
{
    public class DataAdapterFactory : IDataAdapterFactory
    {
        public static readonly DataAdapterFactory Instance = new DataAdapterFactory();

        private DataAdapterFactory()
        {

        }

        public DataAdapterBase CreateDataAdapter(CBTC.Infrasturcture.Model.CBTC cbtc)
        {
            switch (cbtc.Type)
            {
                case SignalType.BOMBARDIER:
                    return new BOMBARDIERDataAdapter(cbtc);
                case SignalType.CASCO:
                    return new CASCODataAdapter(cbtc);
                case SignalType.SIEMENS:
                    return new SIEMENSDataAdapter(cbtc);
                case SignalType.TCT:
                    return new TCTDataAdapter(cbtc);
                case SignalType.THALES:
                    return new THALESDataAdapter(cbtc);
                case SignalType.QuanLuTong:
                    return new QuanLuTongDataAdapter(cbtc);
                default:
                    throw new ArgumentOutOfRangeException(String.Format("CBTC.Type 为未知的类型！"));
            }
        }
    }
}