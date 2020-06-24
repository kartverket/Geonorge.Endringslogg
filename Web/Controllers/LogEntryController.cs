using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geonorge.Endringslogg.Models;
using Geonorge.Endringslogg.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Geonorge.Endringslogg.Web.ActionFilters;
using Serilog;

namespace Geonorge.Endringslogg.Web.Controllers
{
    [ServiceFilter(typeof(LogHandler))]
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
                entry.Application = RouteData.Values["application"].ToString();
                await _logEntryService.AddAsync(entry);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
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
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(entries);
        }

        [Route("list-latest")]
        public async Task<IActionResult> GetEntries(int limitNumberOfEntries = 50, string operation = null, bool limitCurrentApplication = false)
        {
            List<LogEntry> entries;
            try
            {
                string limitApplication = "";
                if(limitCurrentApplication)
                    limitApplication = RouteData.Values["application"].ToString();

                entries = await _logEntryService.EntriesAsync(limitNumberOfEntries, operation, limitApplication);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(entries);
        }
    }
}