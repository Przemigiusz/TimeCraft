namespace TimeCraft_Console_App.Interface_Elements
{
    internal abstract class Element
    {
        protected int currentXPos;
        protected int currentYPos;
        public int getCurrentXPos()
        {
            return currentXPos;
        }
        public int getCurrentYPos()
        {
            return currentYPos;
        }
        public void setCurrentXPos(int currentXPos)
        {
            this.currentXPos = currentXPos;
        }
        public void setCurrentYPos(int currentYPos)
        {
            this.currentYPos = currentYPos;
        }
        public abstract void stopBeingFocused();
        public abstract void startBeingFocused();
    }
}
