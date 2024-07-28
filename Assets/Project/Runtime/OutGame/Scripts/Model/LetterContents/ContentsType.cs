namespace Project.Runtime.OutGame.Model
{
    public enum ContentsParentType
    {
        Login,
        Question
    }

    public enum ContentsType
    {
        LoginCheck,
        LoginGreeting,

        Question1Save,
        Question1Open,
        Question2Save,
        Question2Open,
        Question3Save,
        Question3Open,
        Question4Save,
        Question4Open,

        QuestionClearSave,
        QuestionClearOpenSuccess,
        QuestionClearOpenFailed
    }

    public enum ContentsSfbType
    {
        Save,
        Open
    }
}