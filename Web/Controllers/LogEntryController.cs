using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geonorge.Endringslogg.Models;
using Geonorge.Endringslogg.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Endringslogg.Web.Controllers
{
    [Route("api/logentry")]
    public class LogEntryController : Controller
    {
        private readonly LogEntryService _logEntryService;

        public LogEntryController(LogEntryService logEntryService)
        {
            _logEntryService = logEntryService;
        }

        [Route("add")]
        public async Task<IActionResult> Add([FromBody] LogEntry entry)
        {
            try
            {
                await _logEntryService.AddAsync(entry);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [Route("list")]
        public async Task<IActionResult> GetEntriesForElement([FromQuery] string elementId, int limitNumberOfEntries = 10)
        {
            List<LogEntry> entries;
            try
            {
                entries = await _logEntryService.EntriesForElementAsync(elementId, limitNumberOfEntries);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(entries);
        }
    }
}