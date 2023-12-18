using Spectre.Console;
using TimeCraft_Console_App.Messages;

namespace TimeCraft_Console_App.Interface_Elements.PlansTable
{
    internal abstract class PlansSection : Element
    {
        protected string columnHeader;
        protected int currentRowId;
        protected int? previouslySelectedRowId;
        protected int columnWidth = 47;
        protected int endYPos;
        protected int itemsPerPage;
        protected int currentPage;

        public int ItemsPerPage { get { return this.itemsPerPage; } }
        public int CurrentPage {  set { this.currentPage = value; } }
        public int EndYPos { get { return this.endYPos; } }

        public PlansSection(string columnHeader)
        {
            this.columnHeader = columnHeader;
            this.currentRowId = 0;
        }
        public abstract void render(bool areWeUpdating = false);
        public abstract DSNavigationResult navigate(int rowToSelectId = -1);
        protected abstract void moveUp();
        protected abstract void moveDown();
        public override void stopBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold mediumspringgreen]{this.columnHeader}[/]");
        }
        public override void startBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold black on mediumspringgreen]{this.columnHeader}[/]");
        }
        public void clearPreviousRender()
        {
            Console.SetCursorPosition(0, this.getCurrentYPos());
            int width = Console.WindowWidth;
            string clearLine = new string(' ', width);
            int startIndex = this.getCurrentYPos();
            int endIndex = this.endYPos;
            for (int i = startIndex; i <= endIndex; ++i)
            {
                Console.SetCursorPosition(0, i);
                if (i != endIndex - startIndex - 1)
                {
                    Console.WriteLine(clearLine);
                }
                else
                {
                    Console.Write(clearLine);
                }
            }   
        }
    }
}
