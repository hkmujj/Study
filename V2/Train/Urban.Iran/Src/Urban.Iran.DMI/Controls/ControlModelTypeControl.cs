using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Model;
using MMI.Facility.Interface;
using Urban.Iran.DMI.Index;
using Urban.Iran.DMI.Index.IndexKeys;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI.Controls
{
    public class ControlModelTypeControl : RectangleImage
    {
        public Model.ControlModelType ControlModelType { set; get; }

        private CommonUtil.Model.IReadOnlyDictionary<Model.ControlModelType, Image> m_TypeImageDictionary;

        public ReadOnlyCollection<Tuple<Model.ControlModelType, int>> TypeIndexTuples { private set; get; }

        private baseClass m_SrcObj;

        public ControlModelTypeControl(baseClass srcObj)
        {
            m_SrcObj = srcObj;
            m_TypeImageDictionary =
                new ReadOnlyDictionary<Model.ControlModelType, Image>(new Dictionary<Model.ControlModelType, Image>()
                {
                    {Model.ControlModelType.None, null},
                    {Model.ControlModelType.ATB, ImageResourceFacade.iAtb},
                    {Model.ControlModelType.AUTO, ImageResourceFacade.iAuto},
                    {Model.ControlModelType.ATP, ImageResourceFacade.iAtp},
                    {Model.ControlModelType.RM, ImageResourceFacade.iRM},
                    {Model.ControlModelType.Backward, ImageResourceFacade.iReverse},
                });

            TypeIndexTuples =
                new ReadOnlyCollection<Tuple<Model.ControlModelType, int>>(new List<Tuple<Model.ControlModelType, int>>()
                {
                    new Tuple<Model.ControlModelType, int>(Model.ControlModelType.ATB, IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ATB模式]),
                    new Tuple<Model.ControlModelType, int>(Model.ControlModelType.AUTO, IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ATO模式]),
                    new Tuple<Model.ControlModelType, int>(Model.ControlModelType.ATP, IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.ATP模式]),
                    new Tuple<Model.ControlModelType, int>(Model.ControlModelType.RM, IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.RM模式]),
                    new Tuple<Model.ControlModelType, int>(Model.ControlModelType.Backward, IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.退行模式]),
                });
        }

        public override void Refresh()
        {
            ControlModelType = Model.ControlModelType.None;
            var t = TypeIndexTuples.FirstOrDefault(f => m_SrcObj.BoolList[f.Item2]);
            if (t != null)
            {
                ControlModelType = t.Item1;
            }

            Image = m_TypeImageDictionary[ControlModelType];

            base.Refresh();
        }
    }
}