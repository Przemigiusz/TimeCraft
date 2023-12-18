using Spectre.Console;

namespace TimeCraft_Console_App.Interface_Elements.Forms.FormFields
{
    internal class SelectField : FormField
    {
        private int currentElementId;
        private List<SelectOption> options;
        public List<SelectOption> Options { get { return this.options;  } }
        private string selectedOption;
        private int endYPos;

        public SelectField(string fieldName, List<SelectOption> options) : base(fieldName)
        {
            this.options = options;
            this.selectedOption = "";
        }
        public override void render() {}
        public void render(int rowToSelectId = -1)
        {
            AnsiConsole.MarkupLine($"[bold yellow]{this.fieldName}:[/]");
            foreach (SelectOption option in this.options)
            {
                option.setCurrentXPos(Console.CursorLeft);
                option.setCurrentYPos(Console.CursorTop);
                AnsiConsole.MarkupLine($"[bold white]{option.getOptionName()}[/]");
            }
            this.endYPos = Console.CursorTop;
            this.navigate(rowToSelectId);
            Console.SetCursorPosition(0, this.endYPos);
            Console.WriteLine();
        }

        public void navigate(int rowToSelectId = -1) {
            ConsoleKeyInfo keyInfo;
            if (rowToSelectId == -1)
            {
                this.currentElementId = 0;
            }
            else
            {
                this.currentElementId = rowToSelectId;
            }
            this.options[this.currentElementId].startBeingFocused();
            while (true) {
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        this.moveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        this.moveDown();
                        break;
                    case ConsoleKey.Enter:
                        this.selectedOption = this.options[this.currentElementId].getOptionName();
                        this.options[this.currentElementId].leftSelected();
                        return;
                        
                }
            }
        }

        private void moveUp()
        {
            int previousElementId = this.currentElementId;
            if (this.currentElementId > 0)
            {
                --this.currentElementId;
            }
            else
            {
                this.currentElementId = this.options.Count - 1;
            }
            this.options[previousElementId].stopBeingFocused();
            this.options[this.currentElementId].startBeingFocused();
        }
        private void moveDown()
        {
            int previousElementId = this.currentElementId;
            if (this.currentElementId < this.options.Count - 1)
            {
                ++this.currentElementId;
            }
            else
            {
                this.currentElementId = 0;
            }
            this.options[previousElementId].stopBeingFocused();
            this.options[this.currentElementId].startBeingFocused();
        }

        public override string getAnswer()
        {
            return this.selectedOption;
        }
    }
}
