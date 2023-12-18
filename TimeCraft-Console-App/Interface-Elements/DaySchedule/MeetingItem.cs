using Spectre.Console;
using TimeCraft_Console_App.Models;

namespace TimeCraft_Console_App.Interface_Elements.PlansTable
{
    internal class MeetingItem : Element
    {
        private Meeting meeting;
        public Meeting Meeting { get { return meeting; } }
        public MeetingItem(Meeting meeting)
        {
            this.meeting = meeting;
        }
        public override void stopBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold yellow]{this.meeting.Topic}[/]");
        }
        public override void startBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold black on yellow]{this.meeting.Topic}[/]");
        }
    }
}
