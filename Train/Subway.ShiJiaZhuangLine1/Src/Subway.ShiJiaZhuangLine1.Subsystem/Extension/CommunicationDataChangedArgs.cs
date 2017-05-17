using System;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Extension
{
    public static class CommunicationDataChangedArgsExtension
    {
        public static bool GetValutAt(this IReadOnlyDictionary<int, bool> data, string indexName,
            IndexNameType nameType = IndexNameType.In)
        {
            switch (nameType)
            {
                case IndexNameType.In:
                    if (SubsysParams.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary.ContainsKey(indexName))
                    {
                        return data[SubsysParams.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary[indexName]];
                    }
                    
                    break;
                case IndexNameType.Out:
                    if (SubsysParams.Instance.IndexDescriptionConfig.OutBoolDescriptionDictionary.ContainsKey(indexName))
                    {
                        return
                            data[SubsysParams.Instance.IndexDescriptionConfig.OutBoolDescriptionDictionary[indexName]];
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("nameType", nameType, null);
            }

            var msg = String.Format("Can not found index where indexName={0}, type={1}", indexName, nameType);
            AppLog.Error(msg);
            throw new ArgumentException(msg);
        }

        public static float GetValutAt(this IReadOnlyDictionary<int, float> data, string indexName,
           IndexNameType nameType = IndexNameType.In)
        {
            switch (nameType)
            {
                case IndexNameType.In:
                    if (SubsysParams.Instance.IndexDescriptionConfig.InFloatDescriptionDictionary.ContainsKey(indexName))
                    {
                        return data[SubsysParams.Instance.IndexDescriptionConfig.InFloatDescriptionDictionary[indexName]];
                    }

                    break;
                case IndexNameType.Out:
                    if (SubsysParams.Instance.IndexDescriptionConfig.OutFloatDescriptionDictionary.ContainsKey(indexName))
                    {
                        return
                            data[SubsysParams.Instance.IndexDescriptionConfig.OutFloatDescriptionDictionary[indexName]];
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("nameType", nameType, null);
            }

            var msg = String.Format("Can not found index where indexName={0}, type={1}", indexName, nameType);
            AppLog.Error(msg);
            throw new ArgumentException(msg);
        }

        public static bool ContainsKey(this CommunicationDataChangedArgs<bool> data, IndexNameType nameType, params string[] keyNames)
        {
            return keyNames.Any(a => data.ContainsKey(a, nameType));
        }

        public static bool ContainsKey(this CommunicationDataChangedArgs<float> data, IndexNameType nameType, params string[] keyNames)
        {
            return keyNames.Any(a => data.ContainsKey(a, nameType));
        }

        public static bool ContainsKey(this CommunicationDataChangedArgs<bool> data, string keyName, IndexNameType nameType = IndexNameType.In)
        {
            switch (nameType)
            {
                case IndexNameType.In:
                    return
                        data.NewValue.ContainsKey(
                            SubsysParams.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary[keyName]);
                case IndexNameType.Out:
                    return
                        data.NewValue.ContainsKey(
                            SubsysParams.Instance.IndexDescriptionConfig.OutBoolDescriptionDictionary[keyName]);
                default:
                    throw new ArgumentOutOfRangeException("nameType", nameType, null);
            }
        }

        public static bool ContainsKey(this CommunicationDataChangedArgs<float> data, string keyName, IndexNameType nameType = IndexNameType.In)
        {
            switch (nameType)
            {
                case IndexNameType.In:
                    return
                        data.NewValue.ContainsKey(
                            SubsysParams.Instance.IndexDescriptionConfig.InFloatDescriptionDictionary[keyName]);
                case IndexNameType.Out:
                    return
                        data.NewValue.ContainsKey(
                            SubsysParams.Instance.IndexDescriptionConfig.OutFloatDescriptionDictionary[keyName]);
                default:
                    throw new ArgumentOutOfRangeException("nameType", nameType, null);
            }
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<bool> data, string indexName,
           Action updateActionWhenTrue, Action updateActionWhenFalse = null, IndexNameType nameType = IndexNameType.In)
        {
            switch (nameType)
            {
                case IndexNameType.In:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary[indexName],
                        b =>
                        {
                            if (b)
                            {
                                if (updateActionWhenTrue != null)
                                {
                                    updateActionWhenTrue();
                                }
                            }
                            else
                            {
                                if (updateActionWhenFalse != null)
                                {
                                    updateActionWhenFalse();
                                }
                            }
                        });
                    break;
                case IndexNameType.Out:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.OutBoolDescriptionDictionary[indexName],
                         b =>
                        {
                            if (b)
                            {
                                if (updateActionWhenTrue != null)
                                {
                                    updateActionWhenTrue();
                                }
                            }
                            else
                            {
                                if (updateActionWhenFalse != null)
                                {
                                    updateActionWhenFalse();
                                }
                            }
                        });
                    break;
                default:
                    AppLog.Error("Can not found type {0} when UpdateIfContains", nameType);
                    throw new ArgumentOutOfRangeException("nameType", nameType, null);
            }
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<bool> data, string indexName,
            Action<bool> updateAction, IndexNameType nameType = IndexNameType.In)
        {
            switch (nameType)
            {
                case IndexNameType.In:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary[indexName],
                        updateAction);
                    break;
                case IndexNameType.Out:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.OutBoolDescriptionDictionary[indexName],
                        updateAction);
                    break;
                default:
                    AppLog.Error("Can not found type {0} when UpdateIfContains", nameType);
                    throw new ArgumentOutOfRangeException("nameType", nameType, null);
            }
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<bool> data, string indexName,
            Action<string> updateActionWhenTrue,Action<string> updateActionWhenFalse, IndexNameType nameType = IndexNameType.In)
        {
            switch (nameType)
            {
                case IndexNameType.In:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary[indexName],
                        b =>
                        {
                            if (b)
                            {
                                if (updateActionWhenTrue != null)
                                {
                                    updateActionWhenTrue(indexName);
                                }
                            }
                            else
                            {
                                if (updateActionWhenFalse != null)
                                {
                                    updateActionWhenFalse(indexName);
                                }
                            }
                        });
                    break;
                case IndexNameType.Out:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.OutBoolDescriptionDictionary[indexName],
                        b =>
                        {
                            if (b)
                            {
                                if (updateActionWhenTrue != null)
                                {
                                    updateActionWhenTrue(indexName);
                                }
                            }
                            else
                            {
                                if (updateActionWhenFalse != null)
                                {
                                    updateActionWhenFalse(indexName);
                                }
                            }
                        });
                    break;
                default:
                    AppLog.Error("Can not found type {0} when UpdateIfContains", nameType);
                    throw new ArgumentOutOfRangeException("nameType", nameType, null);
            }
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<bool> data, string indexName,
            Action<string, bool> updateAction, IndexNameType nameType = IndexNameType.In)
        {
            switch (nameType)
            {
                case IndexNameType.In:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary[indexName],
                        b => updateAction(indexName, b));
                    break;
                case IndexNameType.Out:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.OutBoolDescriptionDictionary[indexName],
                        b => updateAction(indexName, b));
                    break;
                default:
                    AppLog.Error("Can not found type {0} when UpdateIfContains", nameType);
                    throw new ArgumentOutOfRangeException("nameType", nameType, null);
            }
        }


        public static void UpdateIfContains(this CommunicationDataChangedArgs<float> data, string indexName,
            Action<float> updateAction, IndexNameType nameType = IndexNameType.In)
        {
            switch (nameType)
            {
                case IndexNameType.In:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.InFloatDescriptionDictionary[indexName],
                        updateAction);
                    break;
                case IndexNameType.Out:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.OutFloatDescriptionDictionary[indexName],
                        updateAction);
                    break;
                default:
                    AppLog.Error("Can not found type {0} when UpdateIfContains", nameType);
                    throw new ArgumentOutOfRangeException("nameType", nameType, null);
            }
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<float> data, string indexName,
            Action<string, int, float> updateAction, IndexNameType nameType = IndexNameType.In)
        {
            switch (nameType)
            {
                case IndexNameType.In:
                    var index = SubsysParams.Instance.IndexDescriptionConfig.InFloatDescriptionDictionary[indexName];
                    data.UpdateIfContains(index, f => updateAction(indexName, index, f));
                    break;
                case IndexNameType.Out:
                    var index1 = SubsysParams.Instance.IndexDescriptionConfig.OutFloatDescriptionDictionary[indexName];
                    data.UpdateIfContains(index1, f => updateAction(indexName, index1, f));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("nameType", nameType, null);
            }
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<float> data, string indexName,
            Action<string, float> updateAction, IndexNameType nameType = IndexNameType.In)
        {
            switch (nameType)
            {
                case IndexNameType.In:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.InFloatDescriptionDictionary[indexName],
                        b => updateAction(indexName, b));
                    break;
                case IndexNameType.Out:
                    data.UpdateIfContains(
                        SubsysParams.Instance.IndexDescriptionConfig.OutFloatDescriptionDictionary[indexName],
                        b => updateAction(indexName, b));
                    break;
                default:
                    AppLog.Error("Can not found type {0} when UpdateIfContains", nameType);
                    throw new ArgumentOutOfRangeException("nameType", nameType, null);
            }
        }
    }

    public enum IndexNameType
    {
        In,
        Out,
    }
}