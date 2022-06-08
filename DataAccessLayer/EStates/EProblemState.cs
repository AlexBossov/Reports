namespace DataAccessLayer.EStates
{
    public enum EProblemState : byte
    {
        OpenTask = 1, // Task has been created, but it has not been started yet
        ActiveTask = 2, // Task is in progress
        ResolvedTask = 3 // Task done
    }
}