namespace ES.Facility.PublicModule.Thread
{
    /// <summary>
    /// 后台计算类
    /// </summary>
    public class BgComputeBase : BgWorkerBase
    {
        //****************************************************************************************
        //**    该类为后台线程处理计算问题的一个基类，不能直接实例化
        //**    支持完成进度和完成通知
        //****************************************************************************************
        #region ******************************  variable   ***************************************

        #endregion

        #region ******************************  attribute  ***************************************

        #endregion

        #region ******************************  structure  ***************************************
        public BgComputeBase() { }

        protected override void initBgWorker()
        {
            base.initBgWorker();
        }
        #endregion

        #region ******************************    event    ***************************************

        #endregion

        #region ******************************   method    ***************************************      

        #endregion

    }
}
