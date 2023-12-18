using Spectre.Console;
using System;

namespace TimeCraft_Console_App.Interface_Elements.Forms.FormFields.TimeSelect
{
    internal abstract class TimeSwitch : Element
    {
        protected int currentOptionId;
        protected List<string> switchOptions;

        protected int changeStep = 1;

        public TimeSwitch()
        {
            this.switchOptions = new List<string>();
            this.initializeSwitchOptions();
        }

        protected abstract void initializeSwitchOptions();

        public void navigate()
        {
            ConsoleKeyInfo keyInfo;
            while(true)
            {
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        handleKeyHold(() => moveLeft());
                        break;
                    case ConsoleKey.RightArrow:
                        handleKeyHold(() => moveRight());
                        break;
                    case ConsoleKey.Enter:
                        return;
                }
            }
        }

        protected void moveRight()
        {
            this.currentOptionId = (this.currentOptionId + changeStep) % this.switchOptions.Count;
            this.startBeingFocused();
        }

        protected void moveLeft()
        {
            this.currentOptionId = (this.currentOptionId - changeStep + this.switchOptions.Count) % this.switchOptions.Count;
            this.startBeingFocused();
        }

        private void handleKeyHold(Action action)
        {
            do
            {
                action();
            } while (Console.KeyAvailable);
        }

        public override void stopBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[white]{this.switchOptions[this.currentOptionId]}[/]");
        }

        public override void startBeingFocused()
        {
            Console.SetCursorPosition(this.currentXPos, this.currentYPos);
            AnsiConsole.Markup($"[bold black on white]{this.switchOptions[this.currentOptionId]}[/]");
        }

        public string getCurrentTimeValue()
        {
            return this.switchOptions[this.currentOptionId];
        }
    }
}
