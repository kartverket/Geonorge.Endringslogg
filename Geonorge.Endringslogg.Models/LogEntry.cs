﻿using System;

namespace Geonorge.Endringslogg.Models
{
    public class LogEntry
    {
        /// <summary>
        ///     Unique identifier for this entry
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Date and time for when the event happened.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        ///     The application where the event happened.
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        ///     The username of the user that made the change.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        ///     Description of what happened. E.g Adjusted extent in metadata.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The identificator of the element that has changed, e.g. the metadata uuid
        /// </summary>
        public string ElementId { get; set; }
    }
}
