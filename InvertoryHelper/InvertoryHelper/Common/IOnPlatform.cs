using System.Threading.Tasks;

namespace InvertoryHelper.Common
{
    public interface IOnPlatform
    {
        string GetDatabasePath();

        bool IsPortreitScreenOreientation();

        Task<string> ScanBarcode(string title);

        void PlaySound(string fileName);
    }
}