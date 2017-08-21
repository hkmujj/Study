namespace MMI.Tester.BatchDataSender.Model
{
    public class BatchSenderParam
    {
        public static readonly BatchSenderParam Instance = new BatchSenderParam();

        private BatchSenderParam()
        {

        }

        public void Initalize(BatchDataSenderInitParam param)
        {
            Param = param;
        }

        public BatchDataSenderInitParam Param { get; private set; }

    }
}