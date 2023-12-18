namespace TimeCraft_Console_App.Messages
{
    internal class DSNavigationResult
    {
        private int code;
        private int content;
        private int previouslySelectedRowId;
        private string typeIndicator;
        public int Code { get { return this.code; } }
        public int Content { get { return this.content; } }
        public int PreviouslySelectedRowId { get { return this.previouslySelectedRowId; } }
        public string TypeIndicator { get { return this.typeIndicator; } }
        public DSNavigationResult(int code, int id = -1, int previouslySelectedRowId = -1,  string typeIndicator = "")
        {
            this.code = code;
            this.content = id;
            this.previouslySelectedRowId = previouslySelectedRowId;
            this.typeIndicator = typeIndicator;
        }
    }
}
