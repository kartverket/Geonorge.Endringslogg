using System;

namespace Geonorge.Endringslogg.Web.Models
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
        public Application Application { get; set; }

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

    /// <summary>
    ///     Enum of applications where the entry originated.
    ///     Warning: The enum values must not be rearranged. Old entries must be preserved.
    ///     Refactoring name can be done without any consequences.
    /// </summary>
    public enum Application
    {
        Metadataeditor,
        Register,
        Kartkatalog,
        Kartografiregister,
        Symbolregister,
        Produktark
    }
}