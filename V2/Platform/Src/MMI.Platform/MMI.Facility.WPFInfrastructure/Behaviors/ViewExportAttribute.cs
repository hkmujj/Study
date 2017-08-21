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

namespace MMI.Facility.WPFInfrastructure.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [MetadataAttribute]
    public sealed class ViewExportAttribute : ExportAttribute, IViewRegionRegistration
    {
        /// <summary>
        /// 
        /// </summary>
        public ViewExportAttribute()
            : base(typeof(object))
        {
        }


        /// <summary>
        ///  RegionName1, IsDefaultView1, Priority1, RegionName2, IsDefaultView2, Priority2...
        /// </summary>
        /// <param name="dataType">һ��view rgion ��������</param>
        /// <param name="viewRegions"></param>
        public ViewExportAttribute(ViewRegionArrayDataType dataType, string[] viewRegions)
            : base(typeof(object))
        {
            ArrayDataType = dataType;
            ViewRegionCollection = viewRegions;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        public ViewExportAttribute(string viewName)
            : base(viewName, typeof(object))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="dataType">һ��view rgion ��������</param>
        /// <param name="viewRegions"></param>
        public ViewExportAttribute(string viewName, ViewRegionArrayDataType dataType, string[] viewRegions)
            : base(viewName, typeof(object))
        {
            ArrayDataType = dataType;
            ViewRegionCollection = viewRegions;
        }


        /// <summary>
        /// ViewName
        /// </summary>
        public string ViewName
        {
            get { return base.ContractName; }
        }


        /// <summary>
        /// RegionNameCollection �� RegionName ֻ����һ����� �� ���  RegionName ��ֵ �� ������ RegionNameCollection
        /// </summary>
        public string[] ViewRegionCollection { get; private set; }

        public ViewRegionArrayDataType ArrayDataType { get; private set; }

        /// <summary>
        /// RegionNameCollection �� RegionName ֻ����һ����� �� ���  RegionName ��ֵ �� ������ RegionNameCollection
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDefaultView { set; get; }

        /// <summary>
        /// Priority �� ԽС����Խǰ
        /// </summary>
        public int Priority { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public enum ViewRegionArrayDataType
    {
        /// <summary>
        /// ��Ч��
        /// </summary>
        Invalidate,

        /// <summary>
        /// RegionName1, IsDefaultView1, Priority1, RegionName2, IsDefaultView2, Priority2...
        /// </summary>
        Type1,
    }

}
