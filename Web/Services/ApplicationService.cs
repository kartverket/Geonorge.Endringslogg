using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Endringslogg.Web.Data;
using Geonorge.Endringslogg.Models;
using Microsoft.EntityFrameworkCore;

namespace Geonorge.Endringslogg.Web.Services
{
    public class ApplicationService: IApplicationService
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetApplicationFromApiKey(string apiKey)
        {
            return await _dbContext.Applications
                .Where(l => l.ApiKey == apiKey)
                .Select(a => a.ApplicationName)
                .FirstOrDefaultAsync();
        }
    }
}
