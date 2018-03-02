using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Endringslogg.Web.Services
{
    public interface IApplicationService
    {
        Task<string> GetApplicationFromApiKey(string apiKey);
    }
}
