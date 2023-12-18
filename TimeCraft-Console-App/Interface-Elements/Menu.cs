using Spectre.Console;
using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;

namespace TimeCraft_Console_App.Interface_Elements
{
    internal abstract class Menu
    {
        protected List<SelectOption> options;
        protected string header;
        protected int currentOptionId;
        protected int startYPos;
        protected int endYPos;
        protected int topOffset;


        public Menu(string menuHeader, List<SelectOption> menuOptions)
        {
            this.header = menuHeader;
            this.options = menuOptions;
            this.currentOptionId = 0;
        }

        public void render(int topMargin)
        {
            this.startYPos = topMargin + topOffset;
            Console.SetCursorPosition(0, this.startYPos);

            AnsiConsole.MarkupLine($"[bold yellow]{this.header}[/]");
            Console.WriteLine();
            int howManyOptions = this.options.Count;
            for (int i = 0; i < howManyOptions; ++i)
            {
                this.options[i].setCurrentXPos(Console.CursorLeft);
                this.options[i].setCurrentYPos(Console.CursorTop);
                if (i != howManyOptions - 1)
                {
                    AnsiConsole.MarkupLine($"[bold white]{this.options[i].getOptionName()}[/]");
                }
                else
                {
                    AnsiConsole.Markup($"[bold white]{this.options[i].getOptionName()}[/]");
                }
                
            }
            this.endYPos = Console.CursorTop;
        }

        public void hidePanel()
        {
            for (int i = 0; i <= this.endYPos - this.startYPos; ++i)
            {
                Console.SetCursorPosition(0, this.startYPos + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        protected void moveDown()
        {
            int previousElementId = this.currentOptionId;
            if (this.currentOptionId < this.options.Count - 1)
            {
                ++this.currentOptionId;
            }
            else
            {
                this.currentOptionId = 0;
            }
            this.options[previousElementId].stopBeingFocused();
            this.options[this.currentOptionId].startBeingFocused();
        }

        protected void moveUp()
        {
            int previousElementId = this.currentOptionId;
            if (this.currentOptionId > 0)
            {
                --this.currentOptionId;
            }
            else
            {
                this.currentOptionId = this.options.Count - 1;
            }
            this.options[previousElementId].stopBeingFocused();
            this.options[this.currentOptionId].startBeingFocused();
        }
    }
}
