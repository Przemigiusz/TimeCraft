using SharedLibrary.Models;
using Spectre.Console;
using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;

namespace TimeCraft_Console_App.Interface_Elements.Forms
{
    internal class RegistrationForm
    {
        private List<FormField>? formFields;

        public RegisteredUser render()
        {
            formFields = new List<FormField>
                {
                    new TextField("Enter Your First Name"),
                    new TextField("Enter Your Last Name"),
                    new TextField("Enter Your Email"),
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
            string repeatedPassword = AnsiConsole.Prompt(new TextPrompt<string>("[yellow]Re-enter password[/]:").PromptStyle("white").Secret());

            Console.WriteLine();

            RegisteredUser user = new RegisteredUser(this.formFields[0].getAnswer(), this.formFields[1].getAnswer(), this.formFields[2].getAnswer(), password, repeatedPassword);

            return user;
        }
    }
}

