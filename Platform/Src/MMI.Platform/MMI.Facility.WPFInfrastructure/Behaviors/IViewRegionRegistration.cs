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

namespace MMI.Facility.WPFInfrastructure.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IViewRegionRegistration  : IViewRegionRelevance
    {
        /// <summary>
        /// RegionNameCollection 和 RegionName 只能有一种情况 ， 如果  RegionName 有值 ， 将忽略 RegionNameCollection
        /// </summary>
        string[] ViewRegionCollection { get; }

        /// <summary>
        /// ViewRegionCollection 的数据类型
        /// </summary>
        ViewRegionArrayDataType ArrayDataType { get; }

    }
}
