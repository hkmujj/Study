using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using Excel.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;

namespace Engine.LCDM.HDX2.DataAdapter.Message
{
    public class MessageManager
    {
        public IReadOnlyDictionary<int, MessageItem> MessageDictionary { private set; get; }

        public ObservableCollection<Entity.Model.Domain.Message> OccursedMessageCollection { private set; get; }

        public MessageManager()
        {
            OccursedMessageCollection = new ObservableCollection<Entity.Model.Domain.Message>();
        }

        public void Initalize(SubsystemInitParam initParam)
        {
            var msgs =
                ExcelParser.Parser<MessageItem>(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory);

            MessageDictionary = msgs.ToDictionary(kvp => kvp.LogicIndex, kvp => kvp).AsReadOnlyDictionary();

        }

        public void Update(CommunicationDataChangedArgs args)
        {
            foreach (var kvp in args.ChangedBools.NewValue)
            {
                if (MessageDictionary.ContainsKey(kvp.Key))
                {
                    var occ = OccursedMessageCollection.FirstOrDefault(f => f.Content.LogicIndex == kvp.Key);
                    if (kvp.Value)
                    {
                        if (occ == null)
                        {
                            OccursedMessageCollection.Add(new Entity.Model.Domain.Message(MessageDictionary[kvp.Key]) { StartTime = DateTime.Now });
                        }
                    }
                    else
                    {
                        if (occ != null)
                        {
                            OccursedMessageCollection.Remove(occ);
                        }
                    }
                }
            }
        }
    }
}