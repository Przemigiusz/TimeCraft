namespace TimeCraft_Console_App.Messages
{
    //CalendarDaysTable - 1XXX
    //Switch - 2XXX
    //Calendar - 3XXX
    //What To Do Menu In Calendar - 4XXX
    //Plans Section - 5XXX
    //What To Do Menu in DaySchedule - 6XXX
    //DaySchedule - 7XXX
    //ExitMenu - 8XXX
    //StartingMenu - 9XXX

    internal static class Codes
    {
        public const int Exit = 0000;

        public const int CTDContinueNavigation = 1000;
        public const int CTDPreviousElement = 1001;
        public const int CTDNextElement = 1002;
        public const int CDTDisplayExitMenu = 1003;

        public const int SChosenOption = 2000;

        public const int CContinueNavigation = 3000;
        public const int CLogout = 3001;

        public const int WTDMenuCTDAddPlans = 4000;
        public const int WTDMenuCTDShowPlans = 4001;
        public const int WTDMenuCTDCancel = 4002;

        public const int PSDisplayWTDMenu = 5000;
        public const int PSExitSection = 5001;

        public const int WTDMenuDSDetails = 6000;
        public const int WTDMenuDSEdit = 6001;
        public const int WTDMenuDSDelete = 6002;
        public const int WTDMenuDSCancel = 6003;

        public const int DSDisplayDetails = 7000;
        public const int DSEdit = 7001;
        public const int DSDelete = 7002;
        public const int DSxit = 7003;

        public const int EBack = 8000;
        public const int ELogout = 8001;

        public const int SMContinue = 9000;
        public const int SMExit = 9001;

        public const int LoRLogin = 10000;
        public const int LoRRegistration = 10001;
    }
}
