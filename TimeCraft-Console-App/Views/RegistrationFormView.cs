using SharedLibrary.Models;
using Spectre.Console;
using TimeCraft_Console_App.Interface_Elements.Forms;

namespace TimeCraft_Console_App.Views
{
    internal class RegistrationFormView
    {
        public RegisteredUser displayRegistrationForm()
        {
            Console.Clear();
            RegistrationForm registrationForm = new RegistrationForm();
            RegisteredUser user = registrationForm.render();
            return user;
        }

        public void displayValidationErrors(List<string> errors)
        {
            foreach (var error in errors)
            {
                AnsiConsole.MarkupLine($"[bold red]{error}[/]");
            }
            Console.WriteLine();
            ConsoleKeyInfo keyInfo;
            AnsiConsole.MarkupLine("[bold white]Press[/] [bold mediumspringgreen]Enter[/] [bold white]to[/] [bold mediumspringgreen]Try Again[/]");
            while (true)
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    return;
                }
            }
        }

        public void displaySuccessMessage()
        {
            ConsoleKeyInfo keyInfo;
            AnsiConsole.MarkupLine("[bold mediumspringgreen]Registration completed successfully. Press Enter to Continue[/]");
            while (true)
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    return;
                }
            }
        }


    }
}
