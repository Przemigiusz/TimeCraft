using SharedLibrary.Models;
using Spectre.Console;

namespace TimeCraft_Console_App.Interface_Elements.Forms.FormFields
{
    internal class LoginForm
    {
        private List<FormField>? formFields;

        public Login render()
        {
            formFields = new List<FormField>
                {
                    new TextField("Email"),
                };

            foreach (FormField field in formFields)
            {
                if (field is SelectField)
                {
                    ((SelectField)field).render(-1);
                }
                else
                {
                    field.render();
                }
            }
            string password = AnsiConsole.Prompt(new TextPrompt<string>("[yellow]Enter password[/]:").PromptStyle("white").Secret());

            Console.WriteLine();


            Login login = new Login(this.formFields[0].getAnswer(), password);
            return login;
        }
    }
}
