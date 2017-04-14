using System;
using System.ComponentModel;

namespace ES.Facility.PublicModule.Thread
{
    public class BgWorkerBase : IBgWorkerBase
    {
        #region ******************************  variable   ***************************************

        /// <summary>��̨�������</summary>
        [CLSCompliant(false)]
        protected BackgroundWorker _bgWork;

        #endregion

        #region ******************************  attribute  ***************************************

        protected int highestPercentageReached = 0;                           //��¼��ɰٷֱ�

        public BackgroundWorker BgWorker { get { return _bgWork; } set { _bgWork = value; } }

        #endregion

        #region ******************************  structure  ***************************************

        protected virtual void initBgWorker()
        {
            if (BgWorker == null)
            {
                BgWorker = new BackgroundWorker();    //��ʼ����̨�̶߳���

                //����״̬���ص�����¼�
                BgWorker.DoWork += _BgWorker_DoWork;
                BgWorker.RunWorkerCompleted += _BgWorker_RunWorkerCompleted;
                BgWorker.ProgressChanged += _BgWorker_ProgressChanged;
            }

            highestPercentageReached = 0;
        }

        #endregion

        #region ******************************    event    ***************************************
        public event Action<int, object> onProgressStaus;

        public event Action<int> onProgress;

        public event Action<bool> onEnd;

        protected void append_percentMessage(int percentNum) { if (onProgress != null) this.onProgress((int)percentNum); }

        protected void append_percentMessageObj(int percentNum, object ob) { if (onProgressStaus != null) this.onProgressStaus((int)percentNum, (object)ob); }

        protected void append_progressCompleted(bool isError) { if (onEnd != null) this.onEnd((bool)isError); }

        #endregion

        #region ******************************   method    ***************************************

        [CLSCompliant(false)]
        protected virtual void _BgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //������̨�߳�
            var worker = sender as System.ComponentModel.BackgroundWorker;

            //�������߳���Ҫ�������
            worker.WorkerReportsProgress = true;
        }

        [CLSCompliant(false)]
        protected virtual void _BgWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                append_progressCompleted(true);
            }
            else if (e.Cancelled)
            {
                append_progressCompleted(true);
            }
            else if ((bool)e.Result)
            {
                append_progressCompleted(false);    //��������
            }
            else
            {
                append_progressCompleted(true);
            }
        }

        [CLSCompliant(false)]
        protected virtual void _BgWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            append_percentMessage(e.ProgressPercentage);
            append_percentMessageObj(e.ProgressPercentage, e.UserState);
        }


        /// <summary>
        /// Ĭ��de�����̷߳���
        /// </summary>
        public virtual void go() { }

        #endregion
    }
}
