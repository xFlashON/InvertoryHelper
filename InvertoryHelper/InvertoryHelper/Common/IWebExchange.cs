using System.Threading.Tasks;

namespace InvertoryHelper.Common
{
    public interface IWebExchange
    {
        Task<ExchangeResult> GetData();

        Task<ExchangeResult> SendData();
    }

    public struct ExchangeResult
    {
        public bool Sucsess;
        public string Message;
    }
}