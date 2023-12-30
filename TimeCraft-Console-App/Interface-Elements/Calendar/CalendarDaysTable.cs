using Spectre.Console;
using System.Globalization;
using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;
using TimeCraft_Console_App.Messages;
using SharedLibrary.Models;

namespace TimeCraft_Console_App.Interface_Elements
{
    internal class CalendarDaysTable : Element
    {
        int currentElementId;
        private List<CalendarDayTableCell> tableCells;
        private int rowsAmount;
        private int columnsAmount;

        private int endYPos;

        private WhatToDoMenuC whatToDoMenu;
        public CalendarDaysTable(int rowsAmount, int columnsAmount)
        {
            this.currentElementId = 0;
            this.tableCells = new List<CalendarDayTableCell>();
            this.rowsAmount = rowsAmount;
            this.columnsAmount = columnsAmount;
            List<SelectOption> whatToDoMenuOptions = new List<SelectOption>
            {
                new SelectOption("Add Plans"),
                new SelectOption("Show Plans"),
                new SelectOption("Cancel")
            };
            string whatToDoMenuMenuHeader = "What You want to do?";
            this.whatToDoMenu = new WhatToDoMenuC(whatToDoMenuMenuHeader, whatToDoMenuOptions);
        }
        public void initializeTableCells(List<DateTime> tableCells, List<Meeting> meetings, List<SharedLibrary.Models.Task> tasks) {
            bool isScheduled;
            for (int i = 0; i < tableCells.Count; ++i) {
                isScheduled = false;
                string formattedDate = tableCells[i].ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                foreach (Meeting meeting in meetings)
                {
                    if (meeting.MeetingDate == formattedDate)
                    {
                        isScheduled = true;
                        break;
                    }
                }
                if (isScheduled == false)
                {
                    foreach (SharedLibrary.Models.Task task in tasks)
                    {
                        if (task.TaskDate == formattedDate)
                        {
                            isScheduled = true;
                            break;
                        }
                    }
                }
                this.tableCells.Add(new CalendarDayTableCell(tableCells[i], isScheduled));
            }
        }
        public int render(int leftMargin) {
            Console.SetCursorPosition(leftMargin, Console.CursorTop);
            this.setCurrentXPos(Console.CursorLeft);
            this.setCurrentYPos(Console.CursorTop);
            int counter = 0;
            for (int i = 0; i < rowsAmount; ++i)
            {
                for (int j = 0; j < columnsAmount; ++j)
                {
                    if (counter != ((i * (columnsAmount - 1) + i)))
                    {
                        Console.Write("   ");
                    }
                    Console.Write(" ");
                    tableCells[counter].setCurrentXPos(Console.CursorLeft);
                    tableCells[counter].setCurrentYPos(Console.CursorTop);
                    if (tableCells[counter].IsScheduled)
                    {
                        AnsiConsole.Markup($"[bold yellow]{tableCells[counter].getCellContent().ToString("dd")}[/]");
                    }
                    else
                    {
                        AnsiConsole.Markup($"[bold white]{tableCells[counter].getCellContent().ToString("dd")}[/]");
                    }
                    ++counter;
                }
                Console.WriteLine("");
                Console.SetCursorPosition(leftMargin, Console.CursorTop);
            }
            this.setEndYPos();
            return this.endYPos;
        }
        public void renderUpdated(int leftMargin)
        {
            Console.SetCursorPosition(this.getCurrentXPos(), this.getCurrentYPos());
            int counter = 0;
            for (int i = 0; i < rowsAmount; ++i)
            {
                for (int j = 0; j < columnsAmount; ++j)
                {
                    if (counter != ((i * (columnsAmount - 1) + i)))
                    {
                        Console.Write("   ");
                    }
                    Console.Write(" ");
                    tableCells[counter].setCurrentXPos(Console.CursorLeft);
                    tableCells[counter].setCurrentYPos(Console.CursorTop);
                    if (tableCells[counter].IsScheduled)
                    {
                        AnsiConsole.Markup($"[bold yellow]{tableCells[counter].getCellContent().ToString("dd")}[/]");
                    }
                    else {
                        AnsiConsole.Markup($"[bold white]{tableCells[counter].getCellContent().ToString("dd")}[/]");
                    }
                    ++counter;
                }
                Console.WriteLine("");
                Console.SetCursorPosition(leftMargin, Console.CursorTop);
            }
        }
        public CNavigationResult navigate(int previousElementId = -1)
        {
            if (this.tableCells.Count > 0)
            {
                ConsoleKeyInfo keyInfo;
                int result = 0;
                if (previousElementId == -1)
                {
                    this.currentElementId = 0;
                }
                else
                {
                    this.currentElementId = previousElementId;
                }
                this.tableCells[this.currentElementId].startBeingFocused();
                while (true)
                {
                    keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.DownArrow:
                            result = this.moveVertically(keyInfo.Key);
                            break;
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:
                            result = this.moveHorizontally(keyInfo.Key);
                            break;
                        case ConsoleKey.Enter:
                            this.whatToDoMenu.render(this.endYPos);
                            CNavigationResult navigationResult = this.whatToDoMenu.navigate();
                            int navigationResultCode = navigationResult.Code;
                            if (navigationResultCode == Codes.WTDMenuCTDCancel)
                            {
                                this.whatToDoMenu.hidePanel();
                                break;
                            }
                            else
                            {
                                return new CNavigationResult(code: navigationResultCode, chosenDate: this.tableCells[this.currentElementId].getCellContent());
                            }
                        case ConsoleKey.Escape:
                            return new CNavigationResult(code: Codes.CDTDisplayExitMenu, previousElementId: this.currentElementId);
                    }
                    switch (result)
                    {
                        case Codes.CTDPreviousElement:
                        case Codes.CTDNextElement:
                            return new CNavigationResult(code: result);
                        case Codes.CTDContinueNavigation:
                            break;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Brak elementów w kolekcji 'tableCells' - CalendarDaysTable");
            }
        }
        private int moveVertically(ConsoleKey key)
        {
            bool needMove = false;
            int previousElementId = this.currentElementId;
            if (key == ConsoleKey.DownArrow)
            {
                if (this.currentElementId < ((this.rowsAmount - 1) * this.columnsAmount))
                {
                    this.currentElementId += this.columnsAmount;
                }
                else
                {
                    this.currentElementId -= this.columnsAmount * (this.rowsAmount - 1);
                    needMove = true;
                }
            }
            else
            {
                if (this.currentElementId > (this.columnsAmount - 1))
                {
                    this.currentElementId -= this.columnsAmount;
                }
                else
                {
                    this.currentElementId += this.columnsAmount * (this.rowsAmount - 1);
                    needMove = true;
                }
            }
            this.tableCells[this.currentElementId].startBeingFocused();
            this.tableCells[previousElementId].stopBeingFocused();
            if (needMove)
            {
                this.tableCells[this.currentElementId].stopBeingFocused();
                this.tableCells[previousElementId].stopBeingFocused();
                return key == ConsoleKey.DownArrow ? Codes.CTDNextElement : Codes.CTDPreviousElement;
            }
            else
            {
                this.tableCells[this.currentElementId].startBeingFocused();
                this.tableCells[previousElementId].stopBeingFocused();
                return Codes.CTDContinueNavigation;
            }
        }
        private int moveHorizontally(ConsoleKey key)
        {
            int currentRow = this.currentElementId / this.columnsAmount;
            int previousElementId = this.currentElementId;
            if (key == ConsoleKey.LeftArrow)
            {
                if (this.currentElementId > (currentRow * this.columnsAmount))
                {
                    --this.currentElementId;
                }
                else
                {
                    this.currentElementId += (this.columnsAmount - 1);
                }
            }
            else
            {
                if (this.currentElementId < ((currentRow + 1) * this.rowsAmount + currentRow))
                {
                    ++this.currentElementId;
                }
                else
                {
                    this.currentElementId -= (this.columnsAmount - 1);
                }
            }
            this.tableCells[this.currentElementId].startBeingFocused();
            this.tableCells[previousElementId].stopBeingFocused();
            return Codes.CTDContinueNavigation;

        }
        public void clearOutTheTable() {
            this.tableCells.Clear();
        }

        private void setEndYPos() {
            this.endYPos = Console.CursorTop;
        }
        public override void stopBeingFocused() {}
        public override void startBeingFocused() {}
    }
}
