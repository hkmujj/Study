using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.DataType.Data
{
    public sealed class CommunicationChangedDataBuffer
    {
        private readonly Queue<CommunicationDataChangedArgs<bool>> m_ChangedBoolBuffer;

        private readonly Queue<CommunicationDataChangedArgs<float>> m_ChangedFloatBuffer;

        private const int BufferSize = 100;

        public CommunicationChangedDataBuffer(int boolCapacity, int floatCapacity)
        {
            m_ChangedBoolBuffer =
                new Queue<CommunicationDataChangedArgs<bool>>(Enumerable.Range(0, BufferSize)
                    .Select(
                        s =>
                            new CommunicationDataChangedArgs<bool>(new Dictionary<int, bool>(boolCapacity),
                                new Dictionary<int, bool>(boolCapacity))));



            m_ChangedFloatBuffer =
                new Queue<CommunicationDataChangedArgs<float>>(Enumerable.Range(0, BufferSize)
                    .Select(
                        s =>
                            new CommunicationDataChangedArgs<float>(new Dictionary<int, float>(floatCapacity),
                                new Dictionary<int, float>(floatCapacity))));

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public CommunicationDataChangedArgs<bool> GetCleanedBool(RaiseCommunicationDataChangedType raiseCommunicationDataChangedType = RaiseCommunicationDataChangedType.ByNetDataRevc)
        {
            var current = m_ChangedBoolBuffer.Dequeue();
            m_ChangedBoolBuffer.Enqueue(current);
            current.NewValue.Clear();
            current.OldValue.Clear();
            current.RaiseDataChangedType = raiseCommunicationDataChangedType;
            return current;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public CommunicationDataChangedArgs<float> GetCleanedFloat(RaiseCommunicationDataChangedType raiseCommunicationDataChangedType = RaiseCommunicationDataChangedType.ByNetDataRevc)
        {
            var current = m_ChangedFloatBuffer.Dequeue();
            m_ChangedFloatBuffer.Enqueue(current);
            current.NewValue.Clear();
            current.OldValue.Clear();
            current.RaiseDataChangedType = raiseCommunicationDataChangedType;
            return current;
        }
    }
}