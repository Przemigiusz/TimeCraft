using Spectre.Console;
using TimeCraft_Console_App.Models;

namespace TimeCraft_Console_App.Views
{
    internal class PlanDetailsView
    {
        private int columnWidth = 47;
        public void displayPlanDetails(IPlan? plan)
        {
            Console.Clear();
            int i = 1;
            if (plan != null)
            {
                if (plan is Meeting)
                {
                    AnsiConsole.MarkupLine($"[bold mediumspringgreen]Meeting Details[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[bold mediumspringgreen]Task Details[/]");
                }
                AnsiConsole.MarkupLine($"[bold white]{new string(('─'), this.columnWidth)}[/]");
                var properties = plan.GetType().GetProperties();
                string key;

                foreach (var property in properties)
                {
                    switch (property.Name)
                    {
                        case "TaskName":
                            key = "Task Name";
                            break;
                        case "TaskDescription":
                            key = "Task Description";
                            break;
                        case "TaskDate":
                            key = "Task Date";
                            break;
                        case "TaskPriority":
                            key = "Task Priority";
                            break;
                        case "IsCompleted":
                            key = "Status";
                            break;
                        case "KindOfMeeting":
                            key = "Kind Of Meeting";
                            break;
                        case "Topic":
                            key = "Topic";
                            break;
                        case "MeetingDate":
                            key = "Meeting Date";
                            break;
                        case "MeetingStartTime":
                            key = "Meeting Start Time";
                            break;
                        case "MeetingEndTime":
                            key = "Meeting End Time";
                            break;
                        default:
                            key = "";
                            break;

                    }
                    object? value = property.GetValue(plan);

                    if (key != "")
                    {
                        if (value != null)
                        {
                            if (key == "Status")
                            {
                                string statusLabel = (bool)value ? "Done" : "Not done";
                                AnsiConsole.MarkupLine($"[bold yellow]{i}.[/] [bold white]{key}:[/] [bold aqua]{statusLabel}[/]");
                            }
                            else
                            {
                                AnsiConsole.MarkupLine($"[bold yellow]{i}.[/] [bold white]{key}:[/] [bold aqua]{value}[/]");
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine($"[bold yellow]{i}.[/] [bold white]{key}:[/] [bold aqua] null[/]");
                        }
                        ++i;
                    }  
                }
                ConsoleKeyInfo keyInfo;
                while (true)
                {
                    keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                }
            }
        }
    }
}
