using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.自动速度控制
{
    public class AutoSpeedControl
    {
        private readonly List<AutoSpeedControlModel> m_AutoSpeedControlModelCollection;

        public ReadOnlyCollection<AutoSpeedControlModel> AutoSpeedControlModelCollection { private set; get; }

        public AutoSpeedControlModel CurrentSeletedSpeed { private set; get; }

        public event Action<AutoSpeedControl> SeletChanged;



        public AutoSpeedControl()
        {
            m_AutoSpeedControlModelCollection = new List<AutoSpeedControlModel>
            {
                                                    new AutoSpeedControlModel(BottomBtnType.Btn01, "ASC\n关闭", "ASC(关闭)"),
                                                    new AutoSpeedControlModel(BottomBtnType.Btn02, "2km/h", "2km/h(联挂速度)"),
                                                    new AutoSpeedControlModel(BottomBtnType.Btn03, "5km/h", "5km/h"),
                                                    new AutoSpeedControlModel(BottomBtnType.Btn04, "10km/h", "10km/h"),
                                                    new AutoSpeedControlModel(BottomBtnType.Btn05, "25km/h", "25km/h"),
                                                    new AutoSpeedControlModel(BottomBtnType.Btn06, "最大\n速度", "最大速度"),
                                                };
            AutoSpeedControlModelCollection = new ReadOnlyCollection<AutoSpeedControlModel>(m_AutoSpeedControlModelCollection);
        }


        public void Select(AutoSpeedControlModel model)
        {
            if (m_AutoSpeedControlModelCollection.Contains(model))
            {
                if (model == CurrentSeletedSpeed || !model.CanSelect)
                {
                    return;
                }

                ResetSelectedState();

                if (model != m_AutoSpeedControlModelCollection[0])
                {
                    foreach (var source in m_AutoSpeedControlModelCollection.Skip(m_AutoSpeedControlModelCollection.IndexOf(model)))
                    {
                        source.CanSelect = false;
                    }
                }
                model.CanSelect = false;
                model.IsSelected = true;
                CurrentSeletedSpeed = model;

                OnSeletChanged(this);
            }
            else
            {
                Select(model.BtnType);
            }
        }

        public void Select(BottomBtnType btnType)
        {
            var model = m_AutoSpeedControlModelCollection.FirstOrDefault(f => f.BtnType == btnType);
            if (model != null)
            {
                Select(model);
            }
        }

        private void OnSeletChanged(AutoSpeedControl obj)
        {
            Action<AutoSpeedControl> handler = SeletChanged;
            if (handler != null)
            {
                handler(obj);
            }
        }

        private void ResetSelectedState()
        {
            m_AutoSpeedControlModelCollection.ForEach(e =>
            {
                e.IsSelected = false;
                e.CanSelect = true;
            });
        }
    }

    public class AutoSpeedControlModel
    {
        [DebuggerStepThrough]
        public AutoSpeedControlModel(BottomBtnType btnType, string btnContent = "", string viewContent = "", bool canSelect = true, bool isSelected = false)
        {
            BtnType = btnType;
            IsSelected = isSelected;
            CanSelect = canSelect;
            ViewContent = viewContent;
            BtnContent = btnContent;
        }

        public string BtnContent { private set; get; }

        public string ViewContent { private set; get; }

        public bool IsSelected { set; get; }

        public bool CanSelect { set; get; }

        public BottomBtnType BtnType { private set; get; }
    }
}