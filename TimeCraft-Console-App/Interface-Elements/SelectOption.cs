using Spectre.Console;

namespace TimeCraft_Console_App.Interface_Elements.Forms.FormFields
{
    internal class SelectOption : Element
    {
        private string optionName;
        public SelectOption(string optionName)
        {
            this.optionName = optionName;
        }

        public string getOptionName()
        {
            return this.optionName;
        }
        public override void stopBeingFocused()
        {
            this.clearPreviousRender();
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold white]{this.optionName}[/]");
        }
        public override void startBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold black on white]>{this.optionName}[/]");
        }
        public void leftSelected()
        {
            this.clearPreviousRender();
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold white]>{this.optionName}[/]");
        }
        private void clearPreviousRender()
        {
            Console.SetCursorPosition(0, this.getCurrentYPos());
            int width = Console.WindowWidth;
            string clearLine = new string(' ', width);
            Console.Write(clearLine);
        }
    }
}
