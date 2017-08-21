using System;
using CommonUtil.Controls.Grid.Items;
using MMICommon.Controls.Grid;

namespace CommonUtil.Controls.Grid
{
    class GridItemFactory : IGridItemFactory
    {
        private static readonly GridItemFactory m_Instance = new GridItemFactory();

        public static GridItemFactory Instance { get { return m_Instance; } }


        public GridItemBase CreateItem(GridItemType itemType)
        {
            switch (itemType)
            {
                case GridItemType.Text:
                    return new GridTextItem();
                case GridItemType.Roudness:
                    return new GridRoundnessItem();
                default:
                    throw new ArgumentOutOfRangeException("itemType");
            }
        }

    }
}
