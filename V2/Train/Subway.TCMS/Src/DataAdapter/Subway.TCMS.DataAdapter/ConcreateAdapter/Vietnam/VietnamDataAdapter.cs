namespace Subway.TCMS.DataAdapter.ConcreateAdapter.Vietnam
{
    public class VietnamDataAdapter : DataAdapterBase
    {
        public VietnamDataAdapter(Infrasturcture.Model.TCMS tcms) : base(tcms)
        {

        }

        /// <summary>
        /// 课程结束时清除数据
        /// </summary>
        public override void ClearDataOnCourceStop()
        {
            ClearData();
        }
    }
}
