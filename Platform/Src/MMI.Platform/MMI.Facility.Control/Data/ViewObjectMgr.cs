using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.Control.Data
{
    /// <summary>
    /// 图元对象管理
    /// </summary>
    public class ViewObjectMgr : IViewObjectManager
    {
        private IMainBaseForm m_BaseForm;

        internal ViewObjectMgr(IMainBaseForm baseForm)
        {
            m_BaseForm = baseForm;
        }

    }
}
