using System;
using System.Collections.Generic;

namespace MMI.Facility.WPFInfrastructure.Behaviors
{
    internal static class ViewRegionRegistrationExtension
    {
        public static List<ViewRegionRelevance> ParserViewRegions(this IViewRegionRegistration viewRegion)
        {
            return ParserViewRegions(viewRegion.ArrayDataType, viewRegion.ViewRegionCollection);
        }

        private static List<ViewRegionRelevance> ParserViewRegions(ViewRegionArrayDataType dataType, string[] viewRegions)
        {
            List<ViewRegionRelevance> viewRegionCollection = new List<ViewRegionRelevance>();
            switch (dataType)
            {
                case ViewRegionArrayDataType.Type1:
                    if (viewRegions.Length < 3)
                    {
                        throw new ArgumentException("viewRegions.length must > 3");
                    }
                    if (viewRegions.Length % 3 != 0)
                    {
                        throw new ArgumentException("viewRegions.length % 3 != 0");
                    }
                    for (int i = 0; i < viewRegions.Length; i += 3)
                    {
                        viewRegionCollection.Add(new ViewRegionRelevance()
                        {
                            RegionName = viewRegions[i],
                            IsDefaultView = Convert.ToBoolean(viewRegions[i + 1]),
                            Priority = Convert.ToInt32(viewRegions[i + 2])
                        });
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("dataType", dataType, null);
            }

            return viewRegionCollection;
        }
    }
}