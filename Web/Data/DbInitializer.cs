using Geonorge.Endringslogg.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Endringslogg.Web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Applications.Any())
                return; // DB has been seeded

            if (!context.Applications.Any())
            {
                var utility = new Utilities();

                context.Applications.Add(new Models.Application { ApplicationName = "Metadataeditor", Id = Guid.NewGuid() , ApiKey = utility.GenerateApiKey() });
                context.Applications.Add(new Models.Application { ApplicationName = "Register", Id = Guid.NewGuid(), ApiKey = utility.GenerateApiKey() });
                context.Applications.Add(new Models.Application { ApplicationName = "Kartkatalog", Id = Guid.NewGuid(), ApiKey = utility.GenerateApiKey() });
                context.Applications.Add(new Models.Application { ApplicationName = "Kartografiregister", Id = Guid.NewGuid(), ApiKey = utility.GenerateApiKey() });
                context.Applications.Add(new Models.Application { ApplicationName = "Symbolregister", Id = Guid.NewGuid(), ApiKey = utility.GenerateApiKey() });
                context.Applications.Add(new Models.Application { ApplicationName = "Produktark", Id = Guid.NewGuid(), ApiKey = utility.GenerateApiKey() });

                context.SaveChanges();
            }

        }
    }
}
