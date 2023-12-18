using Spectre.Console;
using TimeCraft_Console_App.Interface_Elements.Forms.FormFields.TimeSelect;

namespace TimeCraft_Console_App.Interface_Elements.Forms.FormFields
{
    internal class TimeField : FormField
    {
        private int currentElementId;
        private List<TimeSwitch> elements;
        private int endYPos;
        private string answer;

        public TimeField(string fieldName, int currentHoursOptionId = 12, int currentMinutesOptionId = 0) : base(fieldName) {
            this.elements = new List<TimeSwitch>();
            HoursSwitch hoursSwitch = new HoursSwitch(currentHoursOptionId);
            MinutesSwitch minutesSwitch = new MinutesSwitch(currentMinutesOptionId);
            this.elements.Add(hoursSwitch);
            this.elements.Add(minutesSwitch);
            this.answer = "";
        }

        public override void render()
        {
            AnsiConsole.Markup($"[bold yellow]{this.fieldName} [/]");
            this.elements[0].setCurrentXPos(Console.CursorLeft);
            this.elements[0].setCurrentYPos(Console.CursorTop);
            Console.Write($"{this.elements[0].getCurrentTimeValue()}:");
            this.elements[1].setCurrentXPos(Console.CursorLeft);
            this.elements[1].setCurrentYPos(Console.CursorTop);
            Console.WriteLine(this.elements[1].getCurrentTimeValue());
            this.endYPos = Console.CursorTop;
            this.navigate();
            Console.SetCursorPosition(0, this.endYPos);
            Console.WriteLine();
        }

        public void navigate()
        {
            ConsoleKeyInfo keyInfo;
            this.elements[this.currentElementId].startBeingFocused();
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
                        this.elements[this.currentElementId].navigate();
                        break;
                    case ConsoleKey.Escape:
                        this.answer = $"{this.elements[0].getCurrentTimeValue()}:{this.elements[1].getCurrentTimeValue()}";
                        this.elements[this.currentElementId].stopBeingFocused();
                        return;
                }
            }
        }
        private void moveRight()
        {
            int previousElementId = this.currentElementId;
            if (this.currentElementId < this.elements.Count - 1)
            {
                ++this.currentElementId;
            }
            else
            {
                this.currentElementId = 0;
            }
            this.elements[previousElementId].stopBeingFocused();
            this.elements[this.currentElementId].startBeingFocused();
        }
        private void moveLeft()
        {
            int previousElementId = this.currentElementId;
            if (this.currentElementId > 0)
            {
                --this.currentElementId;
            }
            else
            {
                this.currentElementId = this.elements.Count - 1;
            }
            this.elements[previousElementId].stopBeingFocused();
            this.elements[this.currentElementId].startBeingFocused();
        }
        public override string getAnswer()
        {
            return this.answer;
        }
    }
}
