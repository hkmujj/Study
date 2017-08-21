using System;
using System.Collections.Generic;
using Subway.TCMS.DataAdapter.ConcreateAdapter.Vietnam;
using Subway.TCMS.Infrasturcture.Model.Constance;

namespace Subway.TCMS.DataAdapter
{
    public class DataAdapterFactory
    {
        public static DataAdapterBase CreatDataAdapter(Infrasturcture.Model.TCMS tcms)
        {
            DataAdapterBase reuslt;
            switch (tcms.Type)
            {
                case TCMSType.Vietnam:
                    reuslt = new VietnamDataAdapter(tcms);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return reuslt;
        }
    }
}
