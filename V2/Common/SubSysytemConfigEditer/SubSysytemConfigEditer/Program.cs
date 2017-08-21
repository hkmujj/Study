using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtil.Util;
using MMI.Facility.DataType.Config;
using MMI.Facility.Interface.Data.Config;

namespace SubSysytemConfigEditer
{
    class Program
    {
        private static IChangeSubSystem SubSystem;
        static void Main(string[] args)
        {
            AppStartModel model;
            WorkModel Work;
            LogMgr.Error(args[0]);
            LogMgr.Error(args[1]);
            LogMgr.Error(args[2]);
            if (Enum.TryParse(args[0], true, out model))
            {
                switch (model)
                {
                    case AppStartModel.Normal:
                        
                        break;
                    case AppStartModel.Display:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            if (Enum.TryParse(args[1],true,out Work))
            {
                switch (Work)
                {
                    case WorkModel.Merge:
                        SubSystem=new CopySubSystem(args[2].Split(';'));
                        break;
                    case WorkModel.Add:
                        SubSystem=new AddSubSystem(args[2].Split(';'));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            SubSystem.Changed();
        }
    }
}
