﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Endringslogg.Web.Data;
using Geonorge.Endringslogg.Models;
using Microsoft.EntityFrameworkCore;

namespace Geonorge.Endringslogg.Web.Services
{
    public class LogEntryService
    {
        private readonly ApplicationDbContext _dbContext;

        public LogEntryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(LogEntry entry)
        {
            entry.Id = Guid.NewGuid();
            entry.DateTime = DateTime.Now;
            await _dbContext.AddAsync(entry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LogEntry>> EntriesForElementAsync(string elementId, int limitNumberOfEntries = 10)
        {
            return await _dbContext.LogEntries
                .AsNoTracking()
                .Where(l => l.ElementId == elementId)
                .OrderByDescending(l => l.DateTime)
                .Take(limitNumberOfEntries)
                .ToListAsync();
        }

        public async Task<List<LogEntry>> EntriesAsync(int limitNumberOfEntries = 50, string operation = null, string application = "")
        {
            var query = from log in _dbContext.LogEntries
                        .AsNoTracking()
                        select log;
            if (!string.IsNullOrEmpty(operation))
                query = query.Where(l => l.Operation == operation);

            if (!string.IsNullOrEmpty(application))
                query = query.Where(l => l.Application == application);

            return await query.OrderByDescending(l => l.DateTime)
                .Take(limitNumberOfEntries)
                .ToListAsync();
        }

        public async Task<List<LogEntry>> GMLApplicationSchemasAsync(int limitNumberOfEntries, string elementId)
        {
            var query = from log in _dbContext.LogEntries.Where(l => l.Application == "Register" && l.Description.StartsWith("Ftp gml-skjema") ) 
            .AsNoTracking()
                        select log;
            if (!string.IsNullOrEmpty(elementId))
                query = query.Where(l => l.ElementId.EndsWith(elementId));

            return await query.OrderByDescending(l => l.DateTime)
                .Take(limitNumberOfEntries)
                .ToListAsync();
        }
    }
}