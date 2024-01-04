using SharedLibrary.Models;
using Spectre.Console;
using System.Globalization;
using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;

namespace TimeCraft_Console_App.Interface_Elements
{
    internal class NewPlanForm
    {
        private List<FormField>? formFields;

        public IPlan render(DateTime chosenDate)
        {
            SelectField meetingOrTaskSelect = new SelectField("Do you want to add a meeting or a task to do?", new List<SelectOption> { new SelectOption("Meeting"), new SelectOption("Task") });
            meetingOrTaskSelect.render();
            string decision = meetingOrTaskSelect.getAnswer();

            if (decision == "Meeting")
            {
                formFields = new List<FormField>
                {
                    new SelectField("What kind of meeting is it?", new List<SelectOption> { new SelectOption("Conference"), new SelectOption("Team Meeting"), new SelectOption("Client Meeting") }),
                    new TextField("What is the topic of this meeting?"),
                    new TimeField("What time the meeting starts? [[Press Escape to confirm]]"),
                    new TimeField("What time the meeting ends? [[Press Escape to confirm]]"),
                };
            }
            else
            {
                formFields = new List<FormField>
                {
                    new SelectField("Priority", new List<SelectOption> { new SelectOption("Low"), new SelectOption("Normal"), new SelectOption("High") }),
                    new TextField("Title"),
                    new TextAreaField("Description")
                };
            }

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
            Console.WriteLine();
            ConsoleKeyInfo keyInfo;
            if (decision == "Meeting")
            {
                Meeting meeting = new Meeting(this.formFields[0].getAnswer(), this.formFields[1].getAnswer(), chosenDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), this.formFields[2].getAnswer(), this.formFields[3].getAnswer(), 0);
                AnsiConsole.MarkupLine("[bold white]Meeting added[/] [mediumspringgreen]successfully![/]\n[bold white]Press Enter to Continue[/]");
                while (true)
                {
                    keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        return meeting;
                    }
                }

            }
            else
            {
                SharedLibrary.Models.Task task = new SharedLibrary.Models.Task(this.formFields[1].getAnswer(), this.formFields[2].getAnswer(), chosenDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), this.formFields[0].getAnswer(), false, 0);
                AnsiConsole.MarkupLine("[bold white]Task added[/] [bold mediumspringgreen]successfully![/]\n[bold white]Press[/] [bold mediumspringgreen]Enter[/] [bold white]to[/] [bold mediumspringgreen]Continue[/]");
                while (true)
                {
                    keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        return task;
                    }
                }

            }
        }
    }
}
