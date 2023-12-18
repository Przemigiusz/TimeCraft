using Spectre.Console;

namespace TimeCraft_Console_App.Interface_Elements
{
    internal abstract class Switch : Element
    {
        protected int currentOptionId;
        protected List<string> switchOptions;
        protected int width;
        public Switch()
        {
            this.currentOptionId = 0;
            this.switchOptions = new List<string>();
        }
        public abstract void initializeSwitchOptions(List<string> switchOptions);
        
        protected void moveRight()
        {
            if (this.currentOptionId < this.switchOptions.Count - 1)
            {
                ++this.currentOptionId;
            }
            else
            {
                this.currentOptionId = 0;
            }
            this.renderUpdated(false);
        }
        protected void moveLeft()
        {
            if (this.currentOptionId > 0)
            {
                --this.currentOptionId;
            }
            else
            {
                this.currentOptionId = this.switchOptions.Count - 1;
            }
            this.renderUpdated(false);
        }
        public abstract void render(int width);
        protected abstract void renderUpdated(bool isFocusLost);
        protected void clearPreviousRender()
        {
            Console.SetCursorPosition(0, this.getCurrentYPos());
            int width = Console.WindowWidth;
            string clearLine = new string(' ', width);
            Console.Write(clearLine);
        }
        public override void stopBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold white]{this.switchOptions[this.currentOptionId].ToUpper()}[/]");
        }
        public override void startBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold black on white]{this.switchOptions[this.currentOptionId].ToUpper()}[/]");
        }
    }
}
