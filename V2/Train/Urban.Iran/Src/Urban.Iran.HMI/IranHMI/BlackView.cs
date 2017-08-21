using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BlackView : HMIBase
    {
        private ICourseService m_CourseService;

        //private bool m_Lightted;

        private ViewState m_CurrentViewState;

        public override string GetInfo()
        {
            return "BlackView";
        }

        protected override bool Initalize()
        {
            m_CurrentViewState = ViewState.Black;
            m_CourseService = DataPackage.ServiceManager.GetService<ICourseService>();
            m_CourseService.CourseStateChanged += CourseServiceOnCourseStateChanged;
            append_postCmd(CmdType.SetInBoolValue, GlobleParam.Instance.InBoolDictionary["启动"], 1, 0);
            UIObj.InBoolList.Add(GlobleParam.Instance.InBoolDictionary["启动"]);
            UIObj.InBoolList.Add(GlobleParam.Instance.InBoolDictionary["亮屏"]);
            return true;
        }

        private void CourseServiceOnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            if (courseStateChangedArgs.CourseService.CurrentCourseState == CourseState.Stoped)
            {
                ChangedPage(IranViewIndex.Black);
                m_CurrentViewState = ViewState.Black;
            }
        }

        public override bool mouseDown(Point point)
        {
            if (GlobleParam.Instance.CurrentIranViewIndex == IranViewIndex.Black ||
                GlobleParam.Instance.CurrentIranViewIndex == IranViewIndex.StartView)
            {
                append_postCmd(CmdType.SetInBoolValue, GlobleParam.Instance.InBoolDictionary["亮屏"], 1, 0);
            }

            return true;
        }


        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView,
            IranViewIndex lastView)
        {
            if (m_CurrentViewState == ViewState.Black && BoolList[GlobleParam.Instance.InBoolDictionary["启动"]] &&
                m_CourseService.CurrentCourseState != CourseState.Stoped)
            {
                m_CurrentViewState = ViewState.Starting;
                PIS.ResetPis();

                ChangedPage(IranViewIndex.StartView);
                append_postCmd(CmdType.SetBoolValue, (int) SendOutboolType.AutoBroadcastFlg, 1, 0);
            }
            if (m_CurrentViewState != ViewState.Lighting && BoolList[GlobleParam.Instance.InBoolDictionary["亮屏"]])
            {
                m_CurrentViewState = ViewState.Lighting;
                ChangedPage(IranViewIndex.Standby);
            }

            if (m_CurrentViewState == ViewState.Lighting && !BoolList[GlobleParam.Instance.InBoolDictionary["亮屏"]])
            {
                m_CurrentViewState = ViewState.Black;
                ChangedPage(IranViewIndex.Black);
            }
            if (PIS.isWake && BoolList[GlobleParam.Instance.InBoolDictionary["亮屏"]])
            {
                append_postCmd(CmdType.SetFloatValue, (int)SendFloatType.StartStation, 0, 1); 
                append_postCmd(CmdType.SetFloatValue, (int)SendFloatType.NextStation, 0, 1);
                append_postCmd(CmdType.SetFloatValue, (int)SendFloatType.EndStation, 0, 0);
            }
            if (m_CurrentViewState == ViewState.Black)
            {
                PIS.isWake = true;
            }
        }

        private enum ViewState
        {
            Starting,

            Lighting,

            Black,
        }
    }
}
