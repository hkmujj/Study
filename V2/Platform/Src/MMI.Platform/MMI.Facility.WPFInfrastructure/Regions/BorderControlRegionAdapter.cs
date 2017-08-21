using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace MMI.Facility.WPFInfrastructure.Regions
{
    /// <summary>
    /// borader 的region 
    /// </summary>
    [Export(typeof(BorderControlRegionAdapter))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class BorderControlRegionAdapter : RegionAdapterBase<Border>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ContentControlRegionAdapter"/>.
        /// </summary>
        /// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
        [ImportingConstructor]
        public BorderControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        /// <summary>
        /// Adapts a <see cref="ContentControl"/> to an <see cref="IRegion"/>.
        /// </summary>
        /// <param name="region">The new region being used.</param>
        /// <param name="regionTarget">The object to adapt.</param>
        protected override void Adapt(IRegion region, Border regionTarget)
        {
            if (regionTarget == null) throw new ArgumentNullException("regionTarget");
            bool contentIsSet = regionTarget.Child != null;

            if (contentIsSet)
            {
                throw new InvalidOperationException();
            }

            region.ActiveViews.CollectionChanged += delegate
            {
                var fod = region.ActiveViews.FirstOrDefault() as UIElement;
                if (!Equals(regionTarget.Child, fod))
                {
                    regionTarget.Child = region.ActiveViews.FirstOrDefault() as UIElement;
                }
            };

            region.Views.CollectionChanged +=
                (sender, e) =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Add && region.ActiveViews.Count() == 0)
                    {
                        region.Activate(e.NewItems[0]);
                    }
                };
        }

        /// <summary>
        /// Creates a new instance of <see cref="SingleActiveRegion"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="SingleActiveRegion"/>.</returns>
        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnImportsSatisfied()
        {
            
        }
    }
}