using Spectre.Console;

namespace TimeCraft_Console_App.Interface_Elements.PlansTable
{
    internal class TaskItem : Element
    {
        private SharedLibrary.Models.Task task;
        public SharedLibrary.Models.Task Task { get { return task; } }
        public TaskItem(SharedLibrary.Models.Task task)
        {
            this.task = task;
        }

        public override void stopBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[white]{this.task.TaskName}[/]");
        }
        public override void startBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold black on white]{this.task.TaskName}[/]");
        }
    }
}
