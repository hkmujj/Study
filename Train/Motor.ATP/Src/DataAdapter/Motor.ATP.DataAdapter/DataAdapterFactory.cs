using System;
using Motor.ATP.DataAdapter.ConcreateAdapter.ATP200C;
using Motor.ATP.DataAdapter.ConcreateAdapter.ATP300H;
using Motor.ATP.DataAdapter.ConcreateAdapter.ATP300S;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.DataAdapter.ConcreateAdapter.ATP300T;

namespace Motor.ATP.DataAdapter
{
    public class DataAdapterFactory : IDataAdapterFactory
    {
        public DataAdapterBase CreateDataAdapter(ATPDomain atp)
        {
            switch (atp.ATPType)
            {
                case ATPType.ATP200H:
                    break;
                case ATPType.ATP200C:
                    return new ATP200CDataAdapter(atp);
                case ATPType.ATP300S:
                    return new ATP300SDataAdapter(atp);
                case ATPType.ATP300H:
                    return new ATP300HDataAdapter(atp);
                case ATPType.ATP300T:
                    return new ATP300TDataAdapter(atp);
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return null;
        }
    }
}