using Spectre.Console;
using TimeCraft_Console_App.Messages;
using SharedLibrary.Models;

namespace TimeCraft_Console_App.Interface_Elements.PlansTable
{
    internal class MeetingsSection : PlansSection
    {
        private List<MeetingItem> rows;
        public MeetingsSection(string columnHeader, List<Meeting> meetings) : base(columnHeader) {
            this.rows = new List<MeetingItem>();
            this.intializeRows(meetings);
            this.itemsPerPage = 3;
            this.currentPage = 1;
        }
        private void intializeRows(List<Meeting> meetings)
        {
            for (int i = 0; i < meetings.Count; ++i)
            {
                this.rows.Add(new MeetingItem(meetings[i]));
            }
        }
        public override DSNavigationResult navigate(int rowToSelectId = -1)
        {
            ConsoleKeyInfo keyInfo;
            if (rowToSelectId == -1)
            {
                this.currentRowId = (this.currentPage - 1) * this.itemsPerPage;
            }
            else
            {
                this.currentRowId = rowToSelectId;
            }
            if (this.rows.Count > 0)
            {
                this.rows[this.currentRowId].startBeingFocused();
            }
            while (true)
            {
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (this.rows.Count > 0)
                        {
                            this.moveDown();
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (this.rows.Count > 0)
                        {
                            this.moveUp();
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (this.rows.Count > 0)
                        {
                            return new DSNavigationResult(Codes.PSDisplayWTDMenu, this.rows[this.currentRowId].Meeting.MeetingId, this.currentRowId, this.rows[this.currentRowId].Meeting.GetType().Name);
                        }
                        break;
                    case ConsoleKey.Escape:
                        if (this.rows.Count > 0)
                        {
                            this.rows[this.currentRowId].stopBeingFocused();
                        }
                        return new DSNavigationResult(Codes.PSExitSection);
                }
            }
        }
        public override void render(bool areWeChangingPage = false)
        {
            if (areWeChangingPage == false)
            {
                this.setCurrentXPos(Console.CursorLeft);
                this.setCurrentYPos(Console.CursorTop);
            }
            else
            {
                Console.SetCursorPosition(this.getCurrentXPos(), this.getCurrentYPos());
            }
            AnsiConsole.MarkupLine($"[bold mediumspringgreen]{this.columnHeader}[/]");
            AnsiConsole.MarkupLine($"[bold white]{new string(('─'), this.columnWidth)}[/]");

            if (this.rows.Count > 0)
            {
                int startIndex = (currentPage - 1) * itemsPerPage;
                int endIndex = Math.Min(startIndex + itemsPerPage, this.rows.Count);
                for (int i = 0; i < this.itemsPerPage; ++i)
                {
                    if (i < endIndex - startIndex)
                    {
                        Meeting meeting = this.rows[i + startIndex].Meeting;
                        string meetingNum = $"{i + 1}. ";
                        string meetingName = $"{meeting.Topic}";
                        string meetingStartTime = $"    - Start Time: {meeting.MeetingStartTime}";
                        string meetingEndTime = $"    - End Time: {meeting.MeetingEndTime}";
                        AnsiConsole.Markup($"[bold yellow]{meetingNum}[/]");
                        this.rows[i + startIndex].setCurrentXPos(Console.CursorLeft);
                        this.rows[i + startIndex].setCurrentYPos(Console.CursorTop);
                        AnsiConsole.MarkupLine($"[bold yellow]{meetingName}[/]");
                        AnsiConsole.MarkupLine($"[bold white]{meetingStartTime}\r\n{meetingEndTime}[/]");
                    }
                    else
                    {
                        Console.WriteLine($"{new string(' ', this.columnWidth)}");
                        Console.WriteLine($"{new string(' ', this.columnWidth)}");
                        Console.WriteLine($"{new string(' ', this.columnWidth)}");
                    }
                    if (i != this.itemsPerPage - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
            else {
                AnsiConsole.MarkupLine($"[bold white]There are no appointments scheduled on this day[/]");
            }
            this.endYPos = Console.CursorTop;
            AnsiConsole.MarkupLine($"[bold white]{new string(('─'), this.columnWidth)}[/]");
        }
        protected override void moveUp()
        {
            int previousRowId = this.currentRowId;

            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, this.rows.Count);

            if (this.currentRowId > startIndex)
            {
                --this.currentRowId;
            }
            else
            {
                this.currentRowId = endIndex-1;
            }

            this.rows[previousRowId].stopBeingFocused();
            this.rows[this.currentRowId].startBeingFocused();
        }
        protected override void moveDown()
        {
            int previousRowId = this.currentRowId;

            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, this.rows.Count);

            if (this.currentRowId < endIndex - 1)
            {
                ++this.currentRowId;
            }
            else
            {
                this.currentRowId = (currentPage - 1) * itemsPerPage;
            }

            this.rows[previousRowId].stopBeingFocused();
            this.rows[this.currentRowId].startBeingFocused();
        }
    }
}
