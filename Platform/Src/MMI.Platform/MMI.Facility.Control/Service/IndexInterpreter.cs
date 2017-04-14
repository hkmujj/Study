using System;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.DataType.Data;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.IndexDescription;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    internal class IndexInterpreter : IIndexInterpreter
    {
        public IndexInterpreter(ICommunicationDataService dataService, CommunicationDataKey key)
        {
            Key = key;

            InBoolIndexDescriptionCollection = dataService
                .WritableReadService.BoolList.Cast<IIndexDescriptionProvider<int>>().ToList().AsReadOnly();

            InFloatIndexDescriptionCollection = dataService
                .WritableReadService.FloatList.Cast<IIndexDescriptionProvider<int>>().ToList().AsReadOnly();

            OutBoolIndexDescriptionCollection =
                ((CommunicatonDataServiceBase)
                    dataService.WriteService)
                    .BoolList.Cast<IIndexDescriptionProvider<int>>().ToList().AsReadOnly();

            OutFloatIndexDescriptionCollection =
                ((CommunicatonDataServiceBase)
                    dataService.WriteService)
                    .FloatList.Cast<IIndexDescriptionProvider<int>>().ToList().AsReadOnly();
        }

        public ReadOnlyCollection<IIndexDescriptionProvider<int>> InBoolIndexDescriptionCollection { get; private set; }

        public ReadOnlyCollection<IIndexDescriptionProvider<int>> OutBoolIndexDescriptionCollection { get; private set;
        }

        public ReadOnlyCollection<IIndexDescriptionProvider<int>> InFloatIndexDescriptionCollection { get; private set;
        }

        public ReadOnlyCollection<IIndexDescriptionProvider<int>> OutFloatIndexDescriptionCollection { get; private set;
        }

        public ICommunicationDataKey Key { get; private set; }

        public event Action<AppIndexType, ReadOnlyCollection<IIndexDescriptionProvider<int>>> IndexMeanChanged;

        public ReadOnlyCollection<IIndexDescriptionProvider<int>> GetIndexDescriptionCollection(AppIndexType type)
        {
            ReadOnlyCollection<IIndexDescriptionProvider<int>> target;
            switch (type)
            {
                case AppIndexType.InBool:
                    target = InBoolIndexDescriptionCollection;
                    break;
                case AppIndexType.OutBool:
                    target = OutBoolIndexDescriptionCollection;
                    break;
                case AppIndexType.InFloat:
                    target = InFloatIndexDescriptionCollection;
                    break;
                case AppIndexType.OutFloat:
                    target = OutFloatIndexDescriptionCollection;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
            return target;
        }

        public void RegistIndexMeaning(AppIndexType type, int index, string meaning)
        {
            var target = GetIndexDescriptionCollection(type);

            if (target != null)
            {
                var model = target.FirstOrDefault(f => f.Index == index);
                if (model != null)
                {
                    model.Description = meaning;
                }
                else
                {
                    SysLog.Debug(string.Format("Can not find model of index = {0}, type = {1}", index, type));
                }
            }
            else
            {
                SysLog.Debug(string.Format("Can not find model of index = {0}, type = {1}", index, type));
            }
        }

        public void NotifyMeanChanged(AppIndexType type)
        {
            if (IndexMeanChanged == null)
            {
                return;
            }

            var target = GetIndexDescriptionCollection(type);

            IndexMeanChanged(type, target);
        }
    }
}
