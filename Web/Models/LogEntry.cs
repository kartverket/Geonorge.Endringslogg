using System;

namespace Geonorge.Endringslogg.Web.Models
{
    public class LogEntry
    {
        public string Uuid { get; set; }
        public DateTime DateTime { get; set; }
        public string ApplicationId { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
        public string Operation { get; set; }
        public string ElementId { get; set; }
    }
}