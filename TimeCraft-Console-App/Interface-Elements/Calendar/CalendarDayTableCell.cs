using Spectre.Console;

namespace TimeCraft_Console_App.Interface_Elements
{
    internal class CalendarDayTableCell : Element
    {
        private DateTime cellContent;
        private bool isScheduled;
        public bool IsScheduled {  get { return isScheduled; } }
        public CalendarDayTableCell(DateTime cellContent, bool isScheduled) {
            this.cellContent = cellContent;
            this.isScheduled = isScheduled;
        }
        public DateTime getCellContent() {
            return this.cellContent;
        }
        public override void stopBeingFocused() {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            if (this.isScheduled)
            {
                AnsiConsole.Markup($"[bold yellow]{this.getCellContent().ToString("dd")}[/]");
            }
            else
            {
                AnsiConsole.Markup($"[bold white]{this.getCellContent().ToString("dd")}[/]");
            }
        }
        public override void startBeingFocused() {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold black on white]{this.getCellContent().ToString("dd")}[/]");
        }
    }
}
