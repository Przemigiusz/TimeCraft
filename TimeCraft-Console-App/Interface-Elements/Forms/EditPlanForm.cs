using Spectre.Console;
using System.Globalization;
using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;
using TimeCraft_Console_App.Models;

namespace TimeCraft_Console_App.Interface_Elements.Forms
{
    internal class EditPlanForm
    {
        private List<FormField>? formFields;
        private int columnWidth = 47;

        public IPlan? render(IPlan? plan)
        {
            int i = 1;
            if (plan != null) {
                if (plan is Meeting)
                {
                    string[] startingParts = ((Meeting)plan).MeetingStartTime.Split(':');
                    string[] endingParts = ((Meeting)plan).MeetingEndTime.Split(':');
                    int meetingStartTimeHours;
                    int meetingStartTimeMinutes;
                    int meetingEndTimeHours;
                    int meetingEndTimeMinutes;
                    int.TryParse(startingParts[0], out meetingStartTimeHours);
                    int.TryParse(startingParts[1], out meetingStartTimeMinutes);
                    int.TryParse(endingParts[0], out meetingEndTimeHours);
                    int.TryParse(endingParts[1], out meetingEndTimeMinutes);
                    
                    this.formFields = new List<FormField>
                    {
                        new SelectField("What kind of meeting is it?", new List<SelectOption> { new SelectOption("Conference"), new SelectOption("Team Meeting"), new SelectOption("Client Meeting") }),
                        new TextField("What is the topic of this meeting?"),
                        new TimeField("What time the meeting starts? [[Press Escape to confirm]]", meetingStartTimeHours, meetingStartTimeMinutes),
                        new TimeField("What time the meeting ends? [[Press Escape to confirm]]", meetingEndTimeHours, meetingEndTimeMinutes)
                    };
                    AnsiConsole.MarkupLine($"[bold mediumspringgreen]Edit Meeting[/]");
                    AnsiConsole.MarkupLine($"[bold white]{new string(('─'), this.columnWidth)}[/]");
                }
                else
                {
                    this.formFields = new List<FormField>
                {
                    new SelectField("Priority", new List<SelectOption> { new SelectOption("Low"), new SelectOption("Normal"), new SelectOption("High") }),
                    new TextField("Title"),
                    new TextAreaField("Description"),
                    new SelectField("Status", new List<SelectOption> { new SelectOption("Done"), new SelectOption("Not Done")}),
                };
                    AnsiConsole.MarkupLine($"[bold mediumspringgreen]Edit Task[/]");
                    AnsiConsole.MarkupLine($"[bold white]{new string(('─'), this.columnWidth)}[/]");
                }

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
                Console.WriteLine();
                AnsiConsole.MarkupLine($"[bold mediumspringgreen]Enter new values(leave blank to keep existing values - except select fields):[/]");
                Console.WriteLine();
                foreach (FormField field in formFields)
                {
                    if (field is SelectField)
                    {
                        string fieldName = field.FieldName;

                        string selectedOptionName = GetSelectedOptionName(plan, fieldName);

                        int selectedOptionId = ((SelectField)field).Options.FindIndex(option => option.getOptionName() == selectedOptionName);

                        ((SelectField)field).render(selectedOptionId);
                    }
                    else
                    {
                        field.render();
                    }
                    
                }
                Console.WriteLine();
                ConsoleKeyInfo keyInfo;
                if (plan is Meeting)
                {
                    string kindOfMeeting = this.formFields[0].getAnswer();  

                    string topic = string.IsNullOrEmpty(this.formFields[1].getAnswer())
                        ? ((Meeting)plan).Topic
                        : this.formFields[1].getAnswer();

                    string meetingStartTime = this.formFields[2].getAnswer();

                    string meetingEndTime = this.formFields[3].getAnswer();

                    Meeting meeting = new Meeting(kindOfMeeting, topic, ((Meeting)plan).MeetingDate, meetingStartTime, meetingEndTime);
                    meeting.MeetingId = ((Meeting)plan).MeetingId;
                    AnsiConsole.MarkupLine("[bold white]Meeting updated[/] [mediumspringgreen]successfully![/]\n[bold white]Press[/] [bold mediumspringgreen]Enter[/] [bold white]to[/] [bold mediumspringgreen]Continue[/]");
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
                    string taskName = string.IsNullOrEmpty(this.formFields[1].getAnswer())
                        ? ((Models.Task)plan).TaskName
                        : this.formFields[1].getAnswer();

                    string taskDescription = string.IsNullOrEmpty(this.formFields[2].getAnswer())
                        ? ((Models.Task)plan).TaskDescription
                        : this.formFields[2].getAnswer();

                    string taskPriority = string.IsNullOrEmpty(this.formFields[0].getAnswer())
                        ? ((Models.Task)plan).TaskPriority
                        : this.formFields[0].getAnswer();

                    bool isCompleted = this.formFields[3].getAnswer() == "Done" ? true : false;


                    Models.Task task = new Models.Task(taskName, taskDescription, ((Models.Task)plan).TaskDate, taskPriority, isCompleted);
                    task.TaskId = ((Models.Task)plan).TaskId;
                    AnsiConsole.MarkupLine("[bold white]Task updated[/] [bold mediumspringgreen]successfully![/]\n[bold white]Press[/] [bold mediumspringgreen]Enter[/] [bold white]to[/] [bold mediumspringgreen]Continue[/]");
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
            return null;
        }
        private string GetSelectedOptionName(IPlan? plan, string fieldName)
        {
            string selectedOptionName = "";
            if (plan != null)
            {
                switch (fieldName)
                {
                    case "Priority":
                        selectedOptionName = ((Models.Task)plan)?.TaskPriority ?? "";
                        break;
                    case "Status":
                        selectedOptionName = ((Models.Task)plan)?.IsCompleted == true ? "Done" : "Not Done";
                        break;
                    case "What kind of meeting is it?":
                        selectedOptionName = ((Meeting)plan)?.KindOfMeeting ?? "";
                        break;
                }
            }
            return selectedOptionName;
        }
    }

}
