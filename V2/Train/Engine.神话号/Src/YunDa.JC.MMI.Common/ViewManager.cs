using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YunDa.JC.MMI.Common.Extensions;


namespace YunDa.JC.MMI.Common
{
    public interface IView
    {
        Int32 ID { get; set; }
        bool IsShow { get; set; }
        ViewManager ViewManger { get; set; }

        void InvalidateNew();
    }

    public class ViewManager
    {
        private Dictionary<int, IView> _views = new Dictionary<int, IView>();

        public delegate void EventHandle_Reset();
        public event EventHandle_Reset ResetEvent
        {
            add { _resetEvent += value; }
            remove { if (_resetEvent != null) _resetEvent -= value; }
        }
        private EventHandle_Reset _resetEvent = null;

        private Control ViewParent { get; set; }

        public bool Contains(Control view)
        {
            return ViewParent.Controls.Contains(view);
        }

        public void Add(Control view)
        {
            ViewParent.InvokeAddChild(view);
        }

        public void Remove(Control view)
        {
            ViewParent.InvokeRemoveChild(view);
        }

        public Int32 CurrentViewID
        {
            get { return _currentViewID; }
            set
            {
                if (_currentViewID == value) return;

                //隐藏
                if (_views.ContainsKey(_currentViewID))
                {
                    IView view = _views[_currentViewID];
                    view.IsShow = false;
                }
                _currentViewID = value;

                //显示
                if (_views.ContainsKey(value))
                {
                    IView view = _views[value];
                    view.IsShow = true;
                }
            }
        }

        public void OnPait()
        {
            _views[_currentViewID].InvalidateNew();
            //if (ViewParent.Controls != null && ViewParent.Controls.Count != 0)
            //{
            //    for (int i = 0; i < ViewParent.Controls.Count; i++)
            //    {
            //        ViewParent.Controls[i].InvokeIfNeed(new Action(ViewParent.Controls[i].Invalidate));
            //    }
            //}
        }

        void view_MouseLeave(object sender, EventArgs e)
        {
            
        }

        void view_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private Int32 _currentViewID = -1;

        public bool IsReset
        {
            set
            {
                if (_resetEvent != null)
                    _resetEvent();
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewManager(Control viewParent)
        {
            ViewParent = viewParent;
        }

        public void Register(IView view)
        {
            _views.Add(view.ID, view);
        }
    }
}
