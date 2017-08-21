namespace Motor.ATP.Infrasturcture.Model.Message
{
    public static class MessageExtension
    {
        public static void NavigateDown(this Message message)
        {
            message.CurrentFirstIndex++;
        }


        public static void NavigateUp(this Message message)
        {
            message.CurrentFirstIndex--;
        }
    }
}