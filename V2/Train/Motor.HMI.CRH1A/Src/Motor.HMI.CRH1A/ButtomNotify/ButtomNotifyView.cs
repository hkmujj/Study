using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Resource;

namespace Motor.HMI.CRH1A.ButtomNotify
{
    /// <summary>
    /// 下部通知条通知界面， 用于显示  DSD 试验成功。
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ButtomNotifyView : CRH1BaseClass
    {
        private List<ButtomNotifyModelWrapper> m_ToNotifyModelCollection;

        private readonly List<ButtomNotifyModelWrapper> m_CurrentNotifyModel = new List<ButtomNotifyModelWrapper>();

        private ButtomNotifyModelWrapper ShowingNotifyModel
        {
            get { return m_CurrentNotifyModel.First(); }
        }

        private ButtomNotifyItemView m_ShowingItemView;

        public override string GetInfo()
        {
            return " 下部通知条通知界面， 用于显示  DSD 试验成功。";
        }


        public bool Visible
        {
            get { return m_CurrentNotifyModel != null && m_CurrentNotifyModel.Any(); }
        }

        public override bool Initialize()
        {
            m_ToNotifyModelCollection = new List<ButtomNotifyModelWrapper>()
            {
                new ButtomNotifyModelWrapper(new ButtomNotifyModel(InBoolKeys.InBDSD试验完成, "DSD试验成功！"))
            };

            m_ShowingItemView = new ButtomNotifyItemView() {OutLineRectangle = new Rectangle(0, 457, 705, 50)};

            m_ShowingItemView.Init();

            m_ShowingItemView.EnsureNotify += ShowingItemViewOnEnsureNotify;

            return true;
        }

        private void ShowingItemViewOnEnsureNotify(ButtomNotifyModelWrapper notifyModel)
        {
            notifyModel.Responsed = true;
            m_CurrentNotifyModel.Remove(notifyModel);
        }

        protected override bool OnMouseDown(Point point)
        {
            if (Visible)
            {
                return m_ShowingItemView.OnMouseDown(point);
            }

            return false;
        }

        protected override bool OnMouseUp(Point point)
        {
            if (Visible)
            {
                return m_ShowingItemView.OnMouseUp(point);
            }

            return false;
        }


        public override void paint(Graphics g)
        {
            UpdateState();

            DrawContent(g);
        }

        private void DrawContent(Graphics g)
        {
            if (!Visible)
            {
                return;
            }

            m_ShowingItemView.ShowingTarget = ShowingNotifyModel;
            m_ShowingItemView.OnDraw(g);

        }

        private void UpdateState()
        {
            UpdateCurrentModel();
        }

        private void UpdateCurrentModel()
        {
            // 清空
            foreach (
                var model in
                    m_ToNotifyModelCollection.Where(
                        w =>
                            w.Responsed &&
                            !BoolList[IndexDescription.InBoolDescriptionDictionary[w.NotifyModel.IndexKey]]))
            {
                model.Responsed = false;
            }

            foreach (
                var model in
                    m_ToNotifyModelCollection.Where(
                        w =>
                            BoolList[IndexDescription.InBoolDescriptionDictionary[w.NotifyModel.IndexKey]] &&
                            !w.Responsed && !m_CurrentNotifyModel.Contains(w)))
            {
                m_CurrentNotifyModel.Add(model);
            }
        }
    }
}