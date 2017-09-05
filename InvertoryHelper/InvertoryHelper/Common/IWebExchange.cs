using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertoryHelper.Common
{
    public interface IWebExchange
    {
        ExchangeResult GetData();

        ExchangeResult SendData();

    }

    public struct ExchangeResult
    {
        public bool Sucsess;
        public string Message;

    }
}
