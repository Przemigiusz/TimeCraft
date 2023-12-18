using Spectre.Console;

namespace TimeCraft_Console_App.Interface_Elements.DaySchedule
{
    internal class PlansSwitch : Switch
    {
        public PlansSwitch(List<string> switchOptions) : base()
        {
            this.width = 47;
            this.initializeSwitchOptions(switchOptions);
        }
        public override void initializeSwitchOptions(List<string> switchOptions)
        {
            for (int i = 0; i < switchOptions.Count; ++i)
            {
                this.switchOptions.Add(switchOptions[i]);
            }
        }
        public int navigate()
        {
            if (this.switchOptions.Count > 0)
            {
                ConsoleKeyInfo keyInfo;
                this.renderUpdated(false);
                while (true)
                {
                    keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            this.moveLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            this.moveRight();
                            break;
                        case ConsoleKey.Enter:
                            this.renderUpdated(true);
                            return currentOptionId+1;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Brak elementów w kolekcji 'rows' - PlansSwitch");
            }
            
        }
        public override void render(int passedWidth) {}
        public void render()
        {
                string combined = $"[ {this.switchOptions[this.currentOptionId]} ]";
                int width = combined.Length;
                int leftMargin = (this.width - width) / 2;

                Console.SetCursorPosition(leftMargin, Console.CursorTop);

                AnsiConsole.Markup($"[bold aqua][[ [/]");
                this.setCurrentXPos(Console.CursorLeft);
                this.setCurrentYPos(Console.CursorTop);
                AnsiConsole.Markup($"[bold white]{this.switchOptions[this.currentOptionId]}[/]");
                AnsiConsole.MarkupLine($"[bold aqua] ]][/]");

        }
        protected override void renderUpdated(bool isFocusLost)
        {
            clearPreviousRender();
            string combined;
            if (isFocusLost)
            {
                combined = $"[ {this.switchOptions[this.currentOptionId]} ]";
            }
            else
            {
                combined = $"[ <{this.switchOptions[this.currentOptionId]}> ]";
            }
            int width = combined.Length;
            int leftMargin = (this.width - width) / 2;


            Console.SetCursorPosition(leftMargin, Console.CursorTop);

            AnsiConsole.Markup($"[bold aqua][[ [/]");
            this.setCurrentXPos(Console.CursorLeft);
            this.setCurrentYPos(Console.CursorTop);
            if (isFocusLost)
            {
                AnsiConsole.Markup($"[bold black on white]{this.switchOptions[this.currentOptionId]}[/]");
            }
            else
            {
                AnsiConsole.Markup($"[bold black on white]<{this.switchOptions[this.currentOptionId]}>[/]");
            }
            AnsiConsole.MarkupLine($"[bold aqua] ]][/]");
        }
    }
}
