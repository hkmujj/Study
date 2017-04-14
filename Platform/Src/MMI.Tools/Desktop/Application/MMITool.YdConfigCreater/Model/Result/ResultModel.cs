using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMITool.Addin.YdConfigCreater.Model.Result.Detail;

namespace MMITool.Addin.YdConfigCreater.Model.Result
{
    [Export]
    public class ResultModel : NotificationObject
    {
        private ResultItem m_SelectedResultItem;

        public ObservableCollection<ResultItem> ResultItemCollection { set; get; }

        public ResultItem SelectedResultItem
        {
            set
            {
                if (Equals(value, m_SelectedResultItem))
                {
                    return;
                }

                m_SelectedResultItem = value;
                RaisePropertyChanged(() => SelectedResultItem);
            }
            get { return m_SelectedResultItem; }
        }
    }
}