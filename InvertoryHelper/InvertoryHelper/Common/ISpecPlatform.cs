namespace InvertoryHelper.Common
{
    public interface ISpecPlatform
    {
        string GetDatabasePath();

        bool IsPortreitScreenOreientation();
    }
}