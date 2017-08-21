using System;
using System.Collections.Generic;
using System.IO;
using Excel.Interface;
using Mmi.Common.CommunicationIndexWrapper;
using MMI.Facility.DataType;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Log;

namespace MMI.Facility.Control.Extension
{
    public static class AppCommunicationInterfaceModelExtension
    {
        /// <summary>
        /// 加载通信接口文件
        /// </summary>
        /// <param name="interfaceModel"></param>
        /// <param name="relativeRootPath"></param>
        public static void LoadInterfaceFile(this AppCommunicationInterfaceModel interfaceModel, string relativeRootPath)
        {
            try
            {
                interfaceModel.AbsolutFilePath =
                    Path.GetFullPath(Path.Combine(relativeRootPath, interfaceModel.RelativeFilePath));
            }
            catch (Exception e)
            {
                SysLog.Error("Can not combin path 【{0}】,【{1}】,error = {2}", relativeRootPath, interfaceModel.RelativeFilePath, e);
            }

            const string nameCol = "Name";
            const string indexCol = "Index";
            var config = new ExcelReaderConfig()
            {
                File = interfaceModel.AbsolutFilePath,
                SheetNames = new List<string>() {"Sheet1"},
                Coloumns = new List<ColoumnConfig>()
                {
                    new ColoumnConfig(){ IsPrimaryKey = true, Name = nameCol},
                    new ColoumnConfig(){ IsPrimaryKey = false, Name = indexCol},
                }
            };

            var ds = config.Adapter();

            if (ds.Tables.Count != 0)
            {
                var indexWrapper = new IndexWrapper(ds.Tables[0], nameCol, indexCol);
                interfaceModel.NameIndexDictionary = new ReadOnlyDictionary<string, int>(indexWrapper.ConcreatNameIndexDictionary);
            }

        }
    }
}