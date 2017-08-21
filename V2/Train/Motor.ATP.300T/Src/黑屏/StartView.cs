using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Motor.ATP._300T.黑屏
{
    class StartView
    {
        // ReSharper disable once InconsistentNaming
        private const string m_Title = "CTCS3-300T 列控车载设备";

        // ReSharper disable once InconsistentNaming
        private static readonly Font m_TitleFont = new Font(SystemFonts.DefaultFont.FontFamily, 40, FontStyle.Bold);

        // ReSharper disable once InconsistentNaming
        private static readonly Point m_TitleLocation = new Point(90, 100);

        // ReSharper disable once InconsistentNaming
        private static readonly Rectangle m_ProgressImageRectangle = new Rectangle(100, 270, 600, 90);

        // ReSharper disable once InconsistentNaming
        private static readonly Rectangle m_ProgressValueRectangle = new Rectangle(120, 295, 560, 46);

        public StartState StartState { private set; get; }

        public event Action StartCompleted;

        // ReSharper disable once InconsistentNaming
        private static readonly List<ProgressValueModel> m_ProgressValueModels = new List<ProgressValueModel>()
                                                                                 {
                                                                                     new ProgressValueModel(3,0.2f),
                                                                                     new ProgressValueModel(20,0.5f),
                                                                                     new ProgressValueModel(40,0.7f),
                                                                                     new ProgressValueModel(50,0.8f),
                                                                                     new ProgressValueModel(55,1)
                                                                                 };

        private ProgressValueModel m_CurrentProgressValueModel;

        private int m_Period;

        /// <summary>
        /// 进度条图片
        /// </summary>
        public Image ProgressImage { get; set; }

        public float ProgressValuePecent {  get { return m_CurrentProgressValueModel.Progress; } }

        public StartView()
        {
            Restart();
        }

        public void Restart()
        {
            m_Period = 0;
            StartState = StartState.Starting;
            m_CurrentProgressValueModel = m_ProgressValueModels.First();
        }

        public void SkipProgress()
        {
            m_CurrentProgressValueModel = m_ProgressValueModels.Last();
            m_Period = m_CurrentProgressValueModel.Period + 1;
        }

        public void OnPaint(Graphics g)
        {
            g.FillRectangle(Brushes.White, 0, 0, 800, 600);

            g.DrawString(m_Title, m_TitleFont, Brushes.Black, m_TitleLocation);

            g.DrawImage(ProgressImage, m_ProgressImageRectangle);

            ++m_Period;
            if (m_Period < m_CurrentProgressValueModel.Period)
            {

            }
            else if (m_ProgressValueModels.IndexOf(m_CurrentProgressValueModel) < m_ProgressValueModels.Count-1)
            {
                m_CurrentProgressValueModel = m_ProgressValueModels[m_ProgressValueModels.IndexOf(m_CurrentProgressValueModel)+1];
            }
            else
            {
                OnStartCompleted(); 
            }

            g.FillRectangle(Brushes.DodgerBlue,
                m_ProgressValueRectangle.X,
                m_ProgressValueRectangle.Y,
                m_ProgressValueRectangle.Width * ProgressValuePecent,
                m_ProgressValueRectangle.Height);
        }

        private void OnStartCompleted()
        {
            StartState = StartState.Completed;
            if (StartCompleted != null)
            {
                StartCompleted();
            }
        }

        class ProgressValueModel
        {
            public ProgressValueModel(int period, float progress)
            {
                Progress = progress;
                Period = period;
            }

            public int Period { set; get; }

            public float Progress { set; get; }
        }
    }
}
