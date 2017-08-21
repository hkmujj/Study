using System;

using Tram.CBTC.DataAdapter.ConcreateAdapter.CASCO;
using Tram.CBTC.DataAdapter.ConcreateAdapter.NRIET;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.DataAdapter
{
    public class DataAdapterFactory : IDataAdapterFactory
    {
        public static readonly DataAdapterFactory Instance = new DataAdapterFactory();

        private DataAdapterFactory()
        {

        }

        public DataAdapterBase CreateDataAdapter(global::Tram.CBTC.Infrasturcture.Model.CBTC cbtc)
        {
            switch (cbtc.Type)
            {
                
                case SignalType.CASCO:
                    return new CASCODataAdapter(cbtc);
                case SignalType.NRIET:
                    return new NRIETDataAdapter(cbtc);

                default:
                    throw new ArgumentOutOfRangeException(String.Format("CBTC.Type 为未知的类型！"));
            }
        }
    }
}