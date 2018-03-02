using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Geonorge.Endringslogg.Models
{
    public class Application
    {
        public Guid Id { get; set; }
        public string ApplicationName { get; set; }
        public string ApiKey { get; set; }
    }
}
