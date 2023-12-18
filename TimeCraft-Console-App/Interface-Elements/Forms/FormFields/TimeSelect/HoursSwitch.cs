using Spectre.Console;

namespace TimeCraft_Console_App.Interface_Elements.Forms.FormFields.TimeSelect
{
    internal class HoursSwitch : TimeSwitch
    {

        public HoursSwitch(int currentOptionId) : base()
        {
            this.currentOptionId = currentOptionId;
            this.initializeSwitchOptions();
        }
        protected override void initializeSwitchOptions()
        {
            for (int hour = 0; hour < 24; ++hour)
            {
                string formattedHour = hour.ToString("D2");
                this.switchOptions.Add(formattedHour);
            }
        }        
    }
}
