using System;
using System.Windows.Forms;
using CommonControls;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS
{
    public partial class NoBodyVigilantView : Form
    {
        private Int32 _time = 20;
        private bool _isMode20 = false;
        //private ICommunicationDataService _dataService;

        private volatile bool m_IsStartted;

        public NoBodyVigilantView(Form form)
        {
            InitializeComponent();

            //_dataService = dataService;
            m_IsStartted = false;
            GlobalTimer.Instance.TimersElapsed1000MS += _timer_Elapsed;

            this.Owner = form;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!m_IsStartted)
            {
                return;
            }

            if (_time > 0)
            {
                _time--;
            }
            defLabel2.DefText = _time.ToString();
        }

        private Action<NoBodyVigilantView> _CenterToParentAction = (t) => t.CenterToParentNew();

        public void CenterToParentNew()
        {
            CenterToParent();
        }

        public void ShowNew()
        {
            try
            {
                this.InvokeIfNeed(_CenterToParentAction,this);
                this.InvokeShow();;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private Action<NoBodyVigilantView> _start20Action = t => t.Start20();

        public void Start20New()
        {
            this.InvokeIfNeed(_start20Action, this);
        }

        public void Start20()
        {
            try
            {
            //_isMode20 = true;
            _time = 20;
            defLabel2.DefText = _time.ToString();
            m_IsStartted = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private Action<NoBodyVigilantView> _start10Action = t => t.Start10();

        public void Start10New()
        {
            this.InvokeIfNeed(_start10Action, this);
        }

        public void Start10()
        {
            try
            {
            //_isMode20 = false;
            _time = 10;
            defLabel2.DefText = _time.ToString();
            m_IsStartted = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Stop()
        {
            m_IsStartted = false;
        }

        private void NoBodyVigilantView_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        protected override void OnClosed(EventArgs e)
        {
            //GlobalTimer.Instance.TimersElapsed1000MS -= _timer_Elapsed;
        }

        public void Dispose()
        {
        }
    }
}
