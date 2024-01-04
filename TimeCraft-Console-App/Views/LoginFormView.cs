using SharedLibrary.Models;
using Spectre.Console;
using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;

namespace TimeCraft_Console_App.Views
{
    internal class LoginFormView
    {
        public Login displayLoginForm()
        {
            Console.Clear();
            LoginForm loginForm = new LoginForm();
            Login login = loginForm.render();
            return login;
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
    }
}
