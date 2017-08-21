namespace CommonUtil.Controls.Grid
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGridItemFactory
    {
        /// <summary>
        /// 创建一个 Grid的元素
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        GridItemBase CreateItem(GridItemType itemType);
    }
}
