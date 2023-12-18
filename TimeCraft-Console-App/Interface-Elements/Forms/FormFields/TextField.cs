using Spectre.Console;

namespace TimeCraft_Console_App.Interface_Elements.Forms.FormFields
{
    internal class TextField : FormField
    {
        private string answer;

        public TextField(string fieldName) : base(fieldName) {
            this.answer = "";
        }

        public override void render()
        {
            AnsiConsole.Markup($"[bold yellow]{this.fieldName}: [/]");
            this.answer = Console.ReadLine() ?? "";
            Console.WriteLine();
        }

        public override string getAnswer()
        {
            return this.answer;
        }
    }
}
