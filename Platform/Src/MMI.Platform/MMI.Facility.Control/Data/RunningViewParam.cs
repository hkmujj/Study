using System;
using System.Collections.Generic;
using System.Threading;
using CommonUtil.Util;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Args;
using MMI.Facility.Interface.Running;

namespace MMI.Facility.Control.Data
{
    public class RunningViewParam : IRunningViewParam
    {
        private readonly ReaderWriterLock m_CurrentRunningViewParamWriterLock;
        private IRunningViewUnitParam m_CurrentRunningViewUnitParam;

        public RunningViewParam(string projectName)
        {
            m_CurrentRunningViewParamWriterLock = new ReaderWriterLock();
            ProjectName = projectName;
            ViewUnitParamDic = new Dictionary<int, IRunningViewUnitParam>();
        }

        public string ProjectName { get; private set; }

        public event Action<DataChangedArgs<IRunningViewUnitParam>> CurrentRunningViewUnitParamChanged;

        public void SelectCurrentRunningViewUnitParam(int index)
        {
            if (ViewUnitParamDic.ContainsKey(index))
            {
                CurrentRunningViewUnitParam = ViewUnitParamDic[index];
            }
            else
            {
                SysLog.Warn(string.Format("Can not find view where index = {0}", index));
                CurrentRunningViewUnitParam = null;
            }
        }

        public Dictionary<int, IRunningViewUnitParam> ViewUnitParamDic { get; set; }

        public IRunningViewUnitParam CurrentRunningViewUnitParam
        {
            get { return m_CurrentRunningViewUnitParam; }
            set
            {
                if (m_CurrentRunningViewUnitParam == value)
                {
                    return;
                }

                try
                {
                    var oldValue = m_CurrentRunningViewUnitParam;
                    m_CurrentRunningViewParamWriterLock.AcquireWriterLock(TimeSpan.FromMilliseconds(1));
                    m_CurrentRunningViewUnitParam = value;
                    m_CurrentRunningViewParamWriterLock.ReleaseWriterLock();
                    ActionUtil.OnAction(CurrentRunningViewUnitParamChanged, new DataChangedArgs<IRunningViewUnitParam>(oldValue, m_CurrentRunningViewUnitParam));
                }
                catch (Exception)
                {
                    // TODO throw ?
                    SysLog.Warn(string.Format("Can not set CurrentRunningViewUnitParam of project[{0}], other thead is setting now!!! ", ProjectName));
                }
            }
        }

        
    }
}
