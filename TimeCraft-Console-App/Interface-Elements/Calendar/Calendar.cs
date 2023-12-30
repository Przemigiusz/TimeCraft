using Spectre.Console;
using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;
using TimeCraft_Console_App.Messages;
using SharedLibrary.Models;

namespace TimeCraft_Console_App.Interface_Elements.Calendar
{
    internal class Calendar
    {
        private int currentElementId;
        private List<Element> elements;
        private int rowsAmount = 6;
        private int columnsAmount = 7;
        private int leftMargin;
        private DateTime chosenDate;
        private List<Meeting> meetings;
        private List<SharedLibrary.Models.Task> tasks;
        private Interface_Elements.Calendar.ExitMenuC exitMenu;
        private int endYPos;

        private string calendarAsciiArt = "  ___        _                _           \r\n / __| __ _ | | ___  _ _   __| | __ _  _ _ \r\n| (__ / _` || |/ -_)| ' \\ / _` |/ _` || '_|\r\n \\___|\\__,_||_|\\___||_||_|\\__,_|\\__,_||_|  ";
        
        private List<string> dayAbbreviations;
        public Calendar(List<string> months, List<string> dayAbbreviations, List<Meeting> meetings, List<SharedLibrary.Models.Task> tasks) {
            this.currentElementId = 0;
            this.elements = new List<Element>();
            this.meetings = meetings;
            this.tasks = tasks;

            this.dayAbbreviations = dayAbbreviations;

            MonthsSwitch monthsSwitch = new MonthsSwitch();
            monthsSwitch.initializeSwitchOptions(months);

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            List<DateTime> datesForCurrentMonth = this.generateCalendarDaysForSpecificMonth(currentYear, currentMonth);
            CalendarDaysTable daysTable = new CalendarDaysTable(rowsAmount, columnsAmount);
            daysTable.initializeTableCells(datesForCurrentMonth, meetings, tasks);

            this.elements.Add(monthsSwitch);
            this.elements.Add(daysTable);

            List<SelectOption> exitMenuOptions = new List<SelectOption>
            {
                new SelectOption("Yes, I am sure"),
                new SelectOption("No, I want to go back"),
            };
            string exitMenuHeader = "Are You sure You want to exit the app?";
            this.exitMenu = new Interface_Elements.Calendar.ExitMenuC(exitMenuHeader, exitMenuOptions);
        }

        private void displayPadding(int howManyLine)
        {
            for (int i = 0; i < howManyLine; ++i)
            {
                Console.WriteLine("");
            }
        }
        public void render() {
            string[] lines = this.calendarAsciiArt.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            int maxWidth = lines.Max(line => line.Length);

            foreach (var line in lines)
            {
                AnsiConsole.MarkupLine($"[bold mediumspringgreen]{line}[/]");
            }

            this.displayPadding(2);

            MonthsSwitch monthsSwitch = (MonthsSwitch)this.elements[0];
            monthsSwitch.render(maxWidth);

            this.displayPadding(1);

            string dayAbbreviationsString = "";
            for (int i = 0; i < this.dayAbbreviations.Count; ++i)
            {
                if (i != 0)
                {
                    dayAbbreviationsString += "   ";
                }
                dayAbbreviationsString += this.dayAbbreviations[i];
            }
            int leftMargin = (maxWidth - dayAbbreviationsString.Length) / 2;
            Console.SetCursorPosition(leftMargin, Console.CursorTop);
            AnsiConsole.MarkupLine($"[bold aqua]{dayAbbreviationsString}[/]");

            CalendarDaysTable daysTable = (CalendarDaysTable)this.elements[1];
            this.endYPos = daysTable.render(leftMargin);
            this.leftMargin = leftMargin;
        }
        public CNavigationResult navigate()
        {
            if (this.elements.Count > 0)
            {
                MonthsSwitch monthsSwitch = (MonthsSwitch)this.elements[0];
                monthsSwitch.startBeingFocused();
                int result = int.MaxValue;
                ConsoleKeyInfo keyInfo;
                while (true)
                {
                    keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.DownArrow:
                            result = this.moveVertically(keyInfo.Key);
                            break;
                        case ConsoleKey.Enter:
                            if (this.elements[this.currentElementId] is MonthsSwitch)
                            {
                                result = this.select();
                            }
                            break;
                        case ConsoleKey.Escape:
                            this.exitMenu.render(this.endYPos);
                            int navigationResult = this.exitMenu.navigate();
                            if (navigationResult == Codes.EBack)
                            {
                                this.exitMenu.hidePanel();
                                break;
                            }
                            else
                            {
                                return new CNavigationResult(Codes.Exit);
                            }
                    }
                    switch (result)
                    {
                        case Codes.Exit:
                            return new CNavigationResult(Codes.Exit);
                        case Codes.WTDMenuCTDAddPlans:
                        case Codes.WTDMenuCTDShowPlans:
                            return new CNavigationResult(result, chosenDate: this.chosenDate);
                        case Codes.SChosenOption:
                            break;
                    }

                }
            }
            else
            {
                throw new InvalidOperationException("Brak elementów w kolekcji 'elements' - Calendar");
            }
        }
        private int moveVertically(ConsoleKey key)
        {
            MonthsSwitch monthsSwitch = (MonthsSwitch)this.elements[0];
            CalendarDaysTable daysTable = (CalendarDaysTable)this.elements[1];
            if (key == ConsoleKey.DownArrow)
            {
                if (this.currentElementId < this.elements.Count - 1)
                {
                    ++this.currentElementId;
                }
                else
                {
                    this.currentElementId = 0;
                }
            }
            else
            {
                if (this.currentElementId > 0)
                {
                    --this.currentElementId;
                }
                else
                {
                    this.currentElementId = this.elements.Count - 1;
                }
            }
            if (this.elements[this.currentElementId] is CalendarDaysTable)
            {
                monthsSwitch.stopBeingFocused();
                CNavigationResult result;
                bool daysTableNavNeeded = false;
                int previousElementId = 0;
                while (true)
                {
                    if (daysTableNavNeeded)
                    {
                        result = daysTable.navigate(previousElementId);
                    }
                    else
                    {
                        result = daysTable.navigate();
                    }
                    daysTableNavNeeded = false;
                    if (result.Code == Codes.CTDPreviousElement || result.Code == Codes.CTDNextElement)
                    {
                        this.moveVertically(ConsoleKey.UpArrow);
                        return Codes.CContinueNavigation;
                    }
                    else if (result.Code == Codes.CDTDisplayExitMenu)
                    {
                        daysTableNavNeeded = true;
                        previousElementId = result.PreviousElementId;
                        this.exitMenu.render(this.endYPos);
                        int navigationResult = this.exitMenu.navigate();
                        if (navigationResult == Codes.EBack)
                        {
                            this.exitMenu.hidePanel();
                        }
                        else
                        {
                            return Codes.Exit;
                        }
                    }
                    else // result.Code() == Codes.TMAddTask/TMScheduleMeeting/TMShowPlans
                    {
                        this.chosenDate = result.ChosenDate;
                        return result.Code;
                    }
                }
            }
            else
            {
                monthsSwitch.startBeingFocused();
                return Codes.CContinueNavigation;
            }
        }
        private int select()
        {
            MonthsSwitch monthsSwitch = (MonthsSwitch)this.elements[0];
            CNavigationResult result = monthsSwitch.navigate();
            int month = result.Content;
            monthsSwitch.startBeingFocused();
            List<DateTime> daysOfMonth = this.generateCalendarDaysForSpecificMonth(DateTime.Now.Year, month);
            CalendarDaysTable daysTable = (CalendarDaysTable)this.elements[1];
            daysTable.clearOutTheTable();
            daysTable.initializeTableCells(daysOfMonth, this.meetings, this.tasks);
            daysTable.renderUpdated(this.leftMargin);
            return result.Code;
        }
        private List<DateTime> generateCalendarDaysForSpecificMonth(int year, int month)
        {
            List<DateTime> calendarDays = new List<DateTime>();

            DateTime firstDayOfMonth = new DateTime(year, month, 1);

            int daysFromPreviousMonth = ((int)firstDayOfMonth.DayOfWeek + 6) % 7;

            for (int i = daysFromPreviousMonth; i > 0; i--)
            {
                calendarDays.Add(firstDayOfMonth.AddDays(-i));
            }

            for (int i = 0; i < DateTime.DaysInMonth(year, month); i++)
            {
                calendarDays.Add(firstDayOfMonth.AddDays(i));
            }

            int remainingDays = 42 - calendarDays.Count;

            for (int i = 1; i <= remainingDays; i++)
            {
                calendarDays.Add(firstDayOfMonth.AddMonths(1).AddDays(i - 1));
            }

            return calendarDays;
        }
    }
}
