using MMI.Facility.WPFInfrastructure.View;
using System.ComponentModel.Composition;

namespace LightRail.HMI.SZLHLF.Model
{
    [Export]
    public class InputTrainNumModel : ModelBase, ICaretTextModel
    {
        private string m_InputtedTrainNum;
        private int m_CaretIndex;
        private string m_TrainNum;

        public string TrainNum
        {
            get { return m_TrainNum; }
            set
            {
                if (value == m_TrainNum)
                {
                    return;
                }

                m_TrainNum = value;
                RaisePropertyChanged(() => TrainNum);
            }
        }


        public string Text
        {
            get { return m_InputtedTrainNum; }
            set
            {
                if (value == m_InputtedTrainNum)
                {
                    return;
                }

                m_InputtedTrainNum = value;
                RaisePropertyChanged(() => Text);
            }
        }

        public int BindableCaretIndex
        {
            get { return m_CaretIndex; }
            set
            {
                if (value == m_CaretIndex)
                {
                    return;
                }

                m_CaretIndex = value;
                RaisePropertyChanged(() => BindableCaretIndex);
            }
        }


        //public int BindableCaretIndex { get; set; }

        //public string Text { get; set; }
    }
}
