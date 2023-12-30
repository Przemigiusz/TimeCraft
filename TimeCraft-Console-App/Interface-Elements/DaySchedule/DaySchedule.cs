using TimeCraft_Console_App.Interface_Elements.DaySchedule;
using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;
using TimeCraft_Console_App.Messages;
using SharedLibrary.Models;

namespace TimeCraft_Console_App.Interface_Elements.PlansTable
{
    internal class DaySchedule
    {
        private int currentElementId;
        private List<Element> elements;
        protected WhatToDoMenuDS whatToDoMenu;
        private int endYPos;

        public DaySchedule(List<Meeting> meetings, List<SharedLibrary.Models.Task> tasks)
        {
            this.elements = new List<Element>();
            PlansSection meetingsSection = new MeetingsSection("Meetings", meetings);
            PlansSection tasksSection = new TasksSection("Tasks", tasks);

            List<SelectOption> whatToDoMenuOptions = new List<SelectOption>
            {
                new SelectOption("Check The Details"),
                new SelectOption("Edit"),
                new SelectOption("Delete"),
                new SelectOption("Cancel")
            };
            string whatToDoMenuHeader = "What You want to do?";
            this.whatToDoMenu = new WhatToDoMenuDS(whatToDoMenuHeader, whatToDoMenuOptions);

            List<string> meetingsPages = new List<string>();
            List<string> tasksPages = new List<string>();
            int meetingPagesAmount = calculatePages(meetings.Count, meetingsSection.ItemsPerPage);
            int tasksPagesAmount = calculatePages(tasks.Count, tasksSection.ItemsPerPage);
            if (meetingPagesAmount > 0)
            {
                for (int i = 1; i <= meetingPagesAmount; ++i)
                {
                    meetingsPages.Add(i.ToString());
                }
            }
            else
            {
                meetingsPages.Add(1.ToString());
            }
            if (tasksPagesAmount > 0)
            {
                for (int i = 1; i <= tasksPagesAmount; ++i)
                {
                    tasksPages.Add(i.ToString());
                }
            }
            else
            {
                tasksPages.Add(1.ToString());
            }
            MeetingsSwitch meetingsSwitch = new MeetingsSwitch(meetingsPages);
            TasksSwitch tasksSwitch = new TasksSwitch(tasksPages);

            this.elements.Add(meetingsSection);
            this.elements.Add(meetingsSwitch);
            this.elements.Add(tasksSection);
            this.elements.Add(tasksSwitch);
        }

        public void render() {
            ((PlansSection)this.elements[0]).render();
            ((PlansSwitch)this.elements[1]).render();
            ((PlansSection)this.elements[2]).render();
            ((PlansSwitch)this.elements[3]).render();
            this.endYPos = Console.CursorTop;
        }
        public DSNavigationResult navigate()
        {
            if (this.elements.Count > 0)
            {
                ConsoleKeyInfo keyInfo;
                this.elements[this.currentElementId].startBeingFocused();
                while (true)
                {
                    keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            this.moveUp();
                            break;
                        case ConsoleKey.DownArrow:
                            this.moveDown();
                            break;
                        case ConsoleKey.Enter:
                            if (this.elements[this.currentElementId] is PlansSection)
                            {
                                DSNavigationResult sectionResult;
                                DSNavigationResult wtdResult;
                                int navigationResultCode;
                                int rowToSelectId = -1;
                                bool returnFromWTDMenu = false;
                                while (true)
                                {
                                    if (!returnFromWTDMenu)
                                    {
                                        sectionResult = ((PlansSection)this.elements[this.currentElementId]).navigate();
                                    }
                                    else
                                    {
                                        sectionResult = ((PlansSection)this.elements[this.currentElementId]).navigate(rowToSelectId);
                                        returnFromWTDMenu = false;
                                    }
                                    if (sectionResult.Code == Codes.PSDisplayWTDMenu)
                                    {
                                        this.whatToDoMenu.render(this.endYPos);
                                        wtdResult = this.whatToDoMenu.navigate();
                                        navigationResultCode = wtdResult.Code;
                                        if (navigationResultCode == Codes.WTDMenuDSDetails)
                                        {
                                            return new DSNavigationResult(Codes.DSDisplayDetails, sectionResult.Content, typeIndicator: sectionResult.TypeIndicator);
                                        }
                                        else if (navigationResultCode == Codes.WTDMenuDSEdit)
                                        {
                                            return new DSNavigationResult(Codes.DSEdit, sectionResult.Content, typeIndicator: sectionResult.TypeIndicator);
                                        }
                                        else if (navigationResultCode == Codes.WTDMenuDSDelete)
                                        {
                                            return new DSNavigationResult(Codes.DSDelete, sectionResult.Content, typeIndicator: sectionResult.TypeIndicator);
                                        }
                                        else
                                        {
                                            this.whatToDoMenu.hidePanel();
                                            rowToSelectId = sectionResult.PreviouslySelectedRowId;
                                            returnFromWTDMenu = true;
                                        }
                                    }
                                    else if (sectionResult.Code == Codes.PSExitSection)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                int switchResult;
                                switchResult = ((PlansSwitch)this.elements[this.currentElementId]).navigate();
                                if (this.elements[this.currentElementId] is MeetingsSwitch)
                                {
                                    ((PlansSection)this.elements[0]).CurrentPage = switchResult;
                                    ((PlansSection)this.elements[0]).clearPreviousRender();
                                    ((PlansSection)this.elements[0]).render(true);
                                }
                                else
                                {
                                    ((PlansSection)this.elements[2]).CurrentPage = switchResult;
                                    ((PlansSection)this.elements[2]).clearPreviousRender();
                                    ((PlansSection)this.elements[2]).render(true);
                                }
                                this.elements[this.currentElementId].startBeingFocused();
                            }
                            break;
                        case ConsoleKey.Escape:
                            return new DSNavigationResult(Codes.DSxit);
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Brak elementów w kolekcji 'elements' - DaySchedule");
            }
            
        }
        private void moveUp()
        {
            int previousElementId = this.currentElementId;
            if (this.currentElementId > 0)
            {
                --this.currentElementId;
            }
            else
            {
                this.currentElementId = this.elements.Count - 1;
            }
            this.elements[previousElementId].stopBeingFocused();
            this.elements[this.currentElementId].startBeingFocused();
        }

        private void moveDown()
        {
            int previousElementId = this.currentElementId;
            if (this.currentElementId < this.elements.Count - 1)
            {
                ++this.currentElementId;
            }
            else
            {
                this.currentElementId = 0;
            }
            this.elements[previousElementId].stopBeingFocused();
            this.elements[this.currentElementId].startBeingFocused();
        }
        private int calculatePages(int itemCount, int itemsPerPage)
        {
            int pagesAmount = itemCount / itemsPerPage;
            if (itemCount % itemsPerPage != 0)
            {
                pagesAmount++;
            }

            return pagesAmount;
        }
    }
}
