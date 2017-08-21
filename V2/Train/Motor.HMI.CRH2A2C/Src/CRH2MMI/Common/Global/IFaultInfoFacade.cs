using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Fault;

namespace CRH2MMI.Common.Global
{
    public interface IFaultInfoFacade
    {
        List<FaultNameInfo> GetFaultNameInfos();
    }

    class FaultInfoFacade : IFaultInfoFacade
    {
        private readonly GlobalResource m_Resource;

        public FaultInfoFacade(GlobalResource resource)
        {
            m_Resource = resource;
        }

        public List<FaultNameInfo> GetFaultNameInfos()
        {
            return (from DataRow row in m_Resource.FaultInfoDataTable.Rows
                select new FaultNameInfo()
                {
                    FaultLogicIndex = Convert.ToInt32(row["故障逻辑位"]),
                    FaultNo = Convert.ToInt32(row["故障代码"]),
                    FaultCarNos = row["故障车厢号"].ToString().Split(',').Select(s => Convert.ToInt32(s)).ToList(),
                    FaultName = Convert.ToString(row["故障名称"]),
                    ProtectedMachine = Convert.ToString(row["保护装置"]),
                    FaultProcessImageFile =
                        (Convert.ToString(row["处理措施图片"])).IsNullOrWhiteSpace()
                            ? Path.Combine(GlobalParam.Instance.ResourceDirectory,
                                string.Format("Fault_{0:000}.png", Convert.ToInt32(row["故障代码"])))
                            : Path.Combine(GlobalParam.Instance.ResourceDirectory, Convert.ToString(row["处理措施图片"])),
                }).ToList();
        }
    }
}
