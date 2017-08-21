/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System.Windows.Media;
using Microsoft.Practices.Prism.ViewModel;

namespace MMITool.Platform.ViewModel
{
    public class PaneViewModel : NotificationObject
    {
        public PaneViewModel()
        { }


        #region Title

        private string m_Title = null;
        public string Title
        {
            get { return m_Title; }
            set
            {
                if (m_Title != value)
                {
                    m_Title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        #endregion

        public ImageSource IconSource
        {
            get;
            protected set;
        }

        #region ContentId

        private string m_ContentId = null;
        public string ContentId
        {
            get { return m_ContentId; }
            set
            {
                if (m_ContentId != value)
                {
                    m_ContentId = value;
                    RaisePropertyChanged("ContentId");
                }
            }
        }

        #endregion

        #region IsSelected

        private bool m_IsSelected = false;
        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                if (m_IsSelected != value)
                {
                    m_IsSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        #endregion

        #region IsActive

        private bool m_IsActive = false;
        public bool IsActive
        {
            get { return m_IsActive; }
            set
            {
                if (m_IsActive != value)
                {
                    m_IsActive = value;
                    RaisePropertyChanged("IsActive");
                }
            }
        }

        #endregion


    }
}
