//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Regions;

namespace MMI.Facility.WPFInfrastructure.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(AutoPopulateExportedViewsBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AutoPopulateExportedViewsBehavior : RegionBehavior, IPartImportsSatisfiedNotification
    {
        /// <summary>
        /// Override this method to perform the logic after the behavior has been attached.
        /// </summary>
        protected override void OnAttach()
        {
            AddRegisteredViews();
        }

        /// <summary>
        /// 在满足部件的导入并可安全使用时调用。
        /// </summary>
        public void OnImportsSatisfied()
        {
            AddRegisteredViews();
        }

        private void AddRegisteredViews()
        {
            if (Region != null)
            {
                var allRele = (from w in RegisteredViews
                    where w.Metadata.ArrayDataType == ViewRegionArrayDataType.Invalidate
                    select new Tuple<Lazy<object, IViewRegionRegistration>, IViewRegionRelevance>(w, w.Metadata)).ToList();

                allRele.AddRange(
                    (from v in
                         RegisteredViews.Where(w => w.Metadata.ArrayDataType == ViewRegionArrayDataType.Type1)
                     let vrs = v.Metadata.ParserViewRegions()
                     from viewRegion in vrs
                     select new Tuple<Lazy<object, IViewRegionRegistration>, IViewRegionRelevance>(v, viewRegion)));

                object defalutView = null;

                var ve = allRele.FindAll(f => f.Item2.RegionName == Region.Name);
                foreach (var viewEntry in ve.OrderBy(o => o.Item2.Priority))
                {
                    var view = viewEntry.Item1.Value;
                    if (!Region.Views.Contains(view))
                    {
                        Region.Add(view);
                    }
                    if (viewEntry.Item2.IsDefaultView)
                    {
                        defalutView = view;
                    }
                }

                if (defalutView != null)
                {
                    Region.Activate(defalutView);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1819:PropertiesShouldNotReturnArrays", Justification = "MEF injected values"),
         ImportMany(AllowRecomposition = true)]
        public Lazy<object, IViewRegionRegistration>[] RegisteredViews { get; set; }
    }
}
