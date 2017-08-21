using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common.Global;
using CRH2MMI.Resource.Images;

namespace CRH2MMI.Menu
{
    internal class MenuResource
    {
        private CRH2menu m_Menu;

        private Image[] m_Images;

        ///// <summary>
        ///// 司机页面的按键配置
        ///// </summary>
        //public static readonly string DriverMenuConfigFile = "DriverMenuViewConfig.xml";

        public List<DriverMenuContent> DriverMenuContents { private set; get; }


        public static MenuResource Instance { private set; get; }

        public MenuResource(CRH2menu menu)
        {
            m_Menu = menu;
        }

        public void Init()
        {
            Instance = this;

            if (DriverMenuContents == null || !DriverMenuContents.Any())
            {
                DriverMenuContents = GlobalInfo.CurrentCRH2Config.MenuConfig.DriverMenus;
            }

            m_Images = new Image[] { ImageResource.isele, ImageResource.menu1, ImageResource.menu2, ImageResource.menu3 };
        }

        public Image GetBkImage(MenuBase control)
        {
            if (control == null)
            {
                return m_Images[0];
            }

            if (control is DriverMenu)
            {
                return m_Images[1];
            }

            if (control is TrainManagerMenu)
            {
                return m_Images[2];
            }

            if (control is RecorderMenu)
            {
                return m_Images[3];
            }

            return null;
        }

    }
}
