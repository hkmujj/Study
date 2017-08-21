using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.ATP.Infrasturcture.Model.Monitor.ViewControl
{
    public class ViewControlModel : NotificationObject
    {
        private ReadOnlyCollection<ViewSelector> m_ViewSelectors;

        public ReadOnlyCollection<ViewSelector> ViewSelectors
        {
            set
            {
                if (Equals(value, m_ViewSelectors))
                    return;

                m_ViewSelectors = value;
                RaisePropertyChanged(() => ViewSelectors);
            }
            get { return m_ViewSelectors; }
        }
    }

    [DebuggerDisplay("Name={Name}")]
    public class ViewSelector : NotificationObject
    {
        private bool m_NavigateTo;

        [DebuggerStepThrough]
        public ViewSelector(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public bool NavigateTo
        {
            get { return m_NavigateTo; }
            set
            {
                if (value == m_NavigateTo)
                    return;

                m_NavigateTo = value;
                RaisePropertyChanged(() => NavigateTo);
            }
        }

        /// <summary>返回表示当前 <see cref="T:System.Object" /> 的 <see cref="T:System.String" />。</summary>
        /// <returns>
        /// <see cref="T:System.String" />，表示当前的 <see cref="T:System.Object" />。</returns>
        public override string ToString()
        {
            return string.Format("Name={0}, NavigateTo={1}", Name, NavigateTo);
        }
    }
}