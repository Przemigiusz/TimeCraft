using Spectre.Console;
using TimeCraft_Console_App.Messages;
using TimeCraft_Console_App.Models;

namespace TimeCraft_Console_App.Interface_Elements.PlansTable
{
    internal class TasksSection : PlansSection
    {
        private List<TaskItem> rows;
        public TasksSection(string columnHeader, List<Models.Task> tasks) : base(columnHeader) {
            this.rows = new List<TaskItem>();
            this.initializeRows(tasks);
            this.itemsPerPage = 3;
            this.currentPage = 1;
        }
        private void initializeRows(List<Models.Task> tasks) {
            for (int i = 0; i < tasks.Count; ++i) {
                this.rows.Add(new TaskItem(tasks[i]));
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
                            return new DSNavigationResult(Codes.PSDisplayWTDMenu, this.rows[this.currentRowId].Task.TaskId, this.currentRowId, this.rows[this.currentRowId].Task.GetType().Name);
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
        public override void render(bool areWeUpdating = false)
        {
            if (areWeUpdating == false)
            {
                this.setCurrentXPos(Console.CursorLeft);
                this.setCurrentYPos(Console.CursorTop);
            }
            else
            {
                Console.SetCursorPosition(this.getCurrentXPos(), this.getCurrentYPos());
            }
            this.setCurrentXPos(Console.CursorLeft);
            this.setCurrentYPos(Console.CursorTop);
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
                        Models.Task task = this.rows[i + startIndex].Task;
                        string taskNum = $"{i + 1}. ";
                        string taskName = $"{task.TaskName}";
                        AnsiConsole.Markup($"[bold yellow]{taskNum}[/]");
                        this.rows[i + startIndex].setCurrentXPos(Console.CursorLeft);
                        this.rows[i + startIndex].setCurrentYPos(Console.CursorTop);
                        if (this.rows[i + startIndex].Task.TaskPriority == "Low")
                        {
                            AnsiConsole.Markup($"[bold white]{taskName}[/][bold lime] ■[/]");
                        }
                        else if (this.rows[i + startIndex].Task.TaskPriority == "Normal")
                        {
                            AnsiConsole.Markup($"[bold white]{taskName}[/][bold orange1] ■[/]");
                        }
                        else
                        {

                            AnsiConsole.Markup($"[bold white]{taskName}[/][bold red] ■[/]");
                        }
                        if (this.rows[i + startIndex].Task.IsCompleted)
                        {
                            AnsiConsole.MarkupLine("[bold green1] :check_mark_button:[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold red] :cross_mark:[/]");
                        }

                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[bold white]{new string(' ', this.columnWidth)}[/]");
                    }
                }
            }
            else
            {
                AnsiConsole.MarkupLine($"[bold white]There are no tasks scheduled on this day[/]");
            }
            int cursorTop = Console.CursorTop;
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
                this.currentRowId = endIndex - 1;
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
