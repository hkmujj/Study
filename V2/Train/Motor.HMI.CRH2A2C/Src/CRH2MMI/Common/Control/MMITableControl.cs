using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;

namespace CRH2MMI.Common.Control
{
    /// <summary>
    /// TableControl
    /// </summary>
    class MMITableControl : CommonInnerControlBase
    {
        public List<MMITableControlItem>  Items { set; get; }



        public override void OnDraw(Graphics g)
        {
            if (Items != null)
            {
                Items.ForEach(e => e.OnDraw(g));
            }
        }

     
    }
}
