namespace MMITool.Platform.ViewModel
{
    public class ToolViewModel : PaneViewModel
    {
        public ToolViewModel(string name)
        {
            Name = name;
            Title = name;
        }

        public string Name
        {
            get;
            private set;
        }


        private bool m_IsVisible = true;
        public bool IsVisible
        {
            get { return m_IsVisible; }
            set
            {
                if (m_IsVisible != value)
                {
                    m_IsVisible = value;
                    RaisePropertyChanged(() => IsVisible);
                }
            }
        }
    }
}
