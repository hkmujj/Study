using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Bottom;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Global;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.Title
{
    abstract class TitleShowStrategy
    {
        /// <summary>
        /// 获得menu 的显示字符
        /// </summary>
        /// <returns></returns>
        public string GetMenuText()
        {
            return EnumUtil.GetDescription(GetMenuType()).FirstOrDefault();
        }

        public abstract TitleMenuType GetMenuType();

        public abstract string GetTitleText(ViewConfig currentView);

        /// <summary>
        /// 返回界面
        /// </summary>
        public virtual void TurnBackView(Title title)
        {
            LogMgr.Info(string.Format("{0} is not imp TurnBackView method, so do nothing", GetType()));
        }

        public void GotoMenuView(Title title)
        {
            title.append_postCmd(CmdType.ChangePage, (int)ViewConfig.InitView, 1, 0);
        }

        /// <summary>
        /// 故障时的颜色是否显示 
        /// </summary>
        /// <returns></returns>
        public virtual bool FaultColorVisible()
        {
            return false;
        }
    }

    class DefaultTitleShowStrategy : TitleShowStrategy
    {
        public override TitleMenuType GetMenuType()
        {
            return TitleMenuType.Menu;
            //return m_Title.LastView != 0 ? TitleMenuType.Retrun : TitleMenuType.Menu;
        }

        public override string GetTitleText(ViewConfig currentView)
        {
            return EnumUtil.GetDescription(currentView).FirstOrDefault();
        }

        public override void TurnBackView(Title title)
        {
            title.append_postCmd(CmdType.ChangePage, (int)title.LastView, 1, 0);
        }
    }

    class TitleMenuShowStrategy : DefaultTitleShowStrategy
    {
        public TitleMenuType MenuType { set; get; }

        public override TitleMenuType GetMenuType()
        {
            return MenuType;
        }
    }

    internal class TeleCtrTitleMenuShowStrategy : TitleMenuShowStrategy
    {
        public override void TurnBackView(Title title)
        {
            if (ChangePagaManager.Instance.CanChangeTo(ViewConfig.Telecontr))
            {
                base.TurnBackView(title);
                TelentControl.Telecontr.SelfLocked = true;
            }
            else
            {
                title.GetSameProjectObjcect<SuggestiveInformation>()
                     .UpdateInformation(new InformationModel(ChangePagaManager.Instance.GetLastError())
                                        {
                                            InformationType = InformationType.Error,
                                            Location = InformationLocation.Up
                                        });
            }
        }
    }

    class TitleTextShowStrategy : DefaultTitleShowStrategy
    {
        public string TitleText { set; get; }

        public override string GetTitleText(ViewConfig currentView)
        {
            return TitleText;
        }
    }
}
