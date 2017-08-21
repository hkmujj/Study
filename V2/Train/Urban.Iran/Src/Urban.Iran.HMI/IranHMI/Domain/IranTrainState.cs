using System;
using System.Collections.Generic;
using System.Drawing;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Model;
using Urban.Domain.TrainState.Model.Common;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI.Domain
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class IranTrainState : HMIBase, ITrainState, IStateUpdatable
    {
        private UpdateParam m_UpdateParam;

        public static ITrainState Instance { private set; get; }

        public IranTrainState()
        {
            Train = new IranTrain();
        }

        object IIdentityProvide.Identity
        {
            get { return Identity; }
        }

        public ScreenIdentity Identity { get; set; }

        public TrainBase Train { set; get; }

        ITrain ITrainState.Train
        {
            get { return Train; }
        }

        protected override bool Initalize()
        {
            Instance = this;

            m_UpdateParam = new UpdateParam();
            m_UpdateParam.DataService = CommunicationDataService;
            m_UpdateParam.IndexFacade = GlobleParam.Instance.CommunicationIndexFacade;

            foreach (var listeningModel in Train.ListeningModelCollection)
            {
                List<int> target;

                switch (listeningModel.Type)
                {
                    case CommunicationIndexType.InBool :
                        target = UIObj.InBoolList;
                        break;
                    case CommunicationIndexType.InFloat :
                        target = UIObj.InFloatList;
                        break;
                    case CommunicationIndexType.OutBool :
                        target = UIObj.OutBoolList;
                        break;
                    case CommunicationIndexType.OutFloat :
                        target = UIObj.OutFloatList;
                        break;
                    default :
                        throw new ArgumentOutOfRangeException();
                }

                target.Add(GlobleParam.Instance.CommunicationIndexFacade.FindIndex(listeningModel.Type, listeningModel.Name));
            }

            return true;
        }

        public override void paint(Graphics g)
        {
            m_UpdateParam.BaseClass = this;
            Update(m_UpdateParam);
        }

        public void Update(IUpdateParam updateParam)
        {
            Train.Update(updateParam);
        }
    }
}
