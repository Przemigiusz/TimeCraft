using Spectre.Console;
using System.Globalization;
using TimeCraft_Console_App.Messages;

namespace TimeCraft_Console_App.Interface_Elements
{
    internal class MonthsSwitch : Switch
    {
        public MonthsSwitch() : base() {}
        public override void initializeSwitchOptions(List<string> switchOptions)
        {
            for (int i = 0; i < switchOptions.Count; ++i)
            {
                this.switchOptions.Add(switchOptions[i]);
            }

            string currentMonthName = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(DateTime.Now.Month);

            for (int i = 0; i < this.switchOptions.Count; ++i)
            {
                if (this.switchOptions[i] == currentMonthName)
                {
                    this.currentOptionId = i;
                    break;
                }
            }
        }
        public CNavigationResult navigate()
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
                            return new CNavigationResult(Codes.SChosenOption, currentOptionId + 1);
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Brak elementów w kolekcji 'switchOptions' - MonthsSwitch");
            }
        }
        public override void render(int passedWidth)
        {
            this.width = passedWidth;
            DateTime currentDate = DateTime.Now;
            string switchMonthPart = this.switchOptions[this.currentOptionId].ToUpper();
            string currentYearPart = $"{currentDate.Year}";
            string combined = $"{switchMonthPart}, {currentYearPart}";
            int width = combined.Length;

            int leftMargin = (passedWidth - width) / 2;

            this.setCurrentXPos(leftMargin);
            this.setCurrentYPos(Console.CursorTop);

            Console.SetCursorPosition(this.getCurrentXPos(), this.getCurrentYPos());

            AnsiConsole.MarkupLine($"[bold white]{combined}[/]");
        }
        protected override void renderUpdated(bool isFocusLost)
        {
            clearPreviousRender();
            string switchMonthPart;
            if (isFocusLost)
            {
                switchMonthPart = this.switchOptions[this.currentOptionId].ToUpper();
            }
            else
            {
                switchMonthPart = $"<{this.switchOptions[this.currentOptionId].ToUpper()}>";
            }

            string currentYearPart = $"{DateTime.Now.Year}";
            string combined = $"{switchMonthPart}, {currentYearPart}";
            int width = combined.Length;

            int leftMargin = (this.width - width) / 2;

            this.setCurrentXPos(leftMargin);
            this.setCurrentYPos(Console.CursorTop);

            Console.SetCursorPosition(this.getCurrentXPos(), this.getCurrentYPos());

            AnsiConsole.Markup($"[bold black on white]{switchMonthPart}[/], {currentYearPart}");
        }
    }
}
