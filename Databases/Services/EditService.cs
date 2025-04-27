using Databases.Models.Databases.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Services
{
    public class EditService
    {
        public async Task EditPrimaryAsync(TBL_Primary newPrimaryData, string changedBy)
        {
            var existingPrimary = await _context.Set<TBL_Primary>()
                .Include(p => p.Sections)
                .ThenInclude(s => s.Fields)
                .FirstOrDefaultAsync(p => p.Ref_Id == newPrimaryData.Ref_Id);

            if (existingPrimary == null)
                throw new Exception("Entity not found.");

            // Log changes
            await _auditService.LogChangesAsync(existingPrimary, newPrimaryData, newPrimaryData.Ref_Id, changedBy);

            // Update the entity
            _context.Entry(existingPrimary).CurrentValues.SetValues(newPrimaryData);

            await _context.SaveChangesAsync();
        }

    }
}
