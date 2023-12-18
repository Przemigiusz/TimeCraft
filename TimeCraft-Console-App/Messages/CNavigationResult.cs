namespace TimeCraft_Console_App.Messages
{
    internal class CNavigationResult
    {
        private int code;
        private int content;
        private int previousElementId;
        private DateTime chosenDate;
        public int Code { get { return this.code; } }
        public int Content { get { return this.content; } }
        public int PreviousElementId { get { return this.previousElementId; } }
        public DateTime ChosenDate { get { return this.chosenDate; } }
        public CNavigationResult(int code, int id = -1, int previousElementId = -1, DateTime? chosenDate = null)
        {
            this.code = code;
            this.content = id;
            this.previousElementId = previousElementId;
            this.chosenDate = chosenDate ?? DateTime.MinValue;
        }
    }
}
