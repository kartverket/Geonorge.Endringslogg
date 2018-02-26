﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Endringslogg.Web.Data;
using Geonorge.Endringslogg.Web.Models;
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
                .Where(l => l.ElementId == elementId)
                .OrderByDescending(l => l.DateTime)
                .Take(limitNumberOfEntries)
                .ToListAsync();
        }
    }
}