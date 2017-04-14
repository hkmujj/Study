/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System;
using System.Linq;
using Xceed.Wpf.AvalonDock.Layout;

namespace MMITool.Platform.View.Layout
{
    [Obsolete]
    class LayoutInitializer : ILayoutUpdateStrategy
    {
        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer)
        {
            //AD wants to add the anchorable into destinationContainer
            //just for test provide a new anchorablepane 
            //if the pane is floating let the manager go ahead
            var destPane = destinationContainer as LayoutAnchorablePane;
            if (destinationContainer != null &&
                destinationContainer.FindParent<LayoutFloatingWindow>() != null)
                return false;

            //var toolsPane = layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault(d => d.Name == "ToolPane");
            var panel = layout.Descendents().OfType<LayoutPanel>().First();

            var group = panel.Children.OfType<LayoutAnchorablePaneGroup>().First();

            var toolsPane = group.Children.OfType<LayoutAnchorablePane>().FirstOrDefault(d => d.Name == "ToolPane");
            //Children.OfType<LayoutAnchorablePane>().FirstOrDefault(d => d.Name == "ToolPane");

            if (toolsPane != null)
            {
                toolsPane.Children.Add(new LayoutAnchorable(){ Content = new LayoutAnchorablePane(anchorableToShow)});
                return true;
            }

            return false;

        }


        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {
        }


        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
        {
            return false;
        }

        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {

        }
    }
}
