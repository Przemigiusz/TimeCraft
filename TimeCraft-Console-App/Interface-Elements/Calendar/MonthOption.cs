using Spectre.Console;

namespace TimeCraft_Console_App.Interface_Elements
{
    internal class MonthOption : Element
    {
        private string optionName;
        public MonthOption(string optionName) {
            this.optionName = optionName;
        }
        public string getOptionName() {
            return this.optionName;
        }
        public override void stopBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.WriteLine(this.optionName);
        }
        public override void startBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.MarkupLine($"[bold black on white]{this.optionName}[/]");
        }
    }
}
