using Core.Interfaces;
using Core.ViewModel;
using Databases.Data;
using Databases.Models.Databases.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DatabaseDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<TBLPrimaryDto>> GetAllAsync()
        {

            return await _context.Primaries
  .AsNoTracking()
  .Include(p => p.Sections)
      .ThenInclude(s => s.Fields)
  .Select(p => MapToDto(p))
  .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
           
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();

        private static TBLPrimaryDto MapToDto(TBL_Primary p) => new()
        {
            Ref_Id = p.Ref_Id,
            OrgenaztionId = p.OrgenaztionId,
            RegionName = p.RegionName,
            RoleName = p.RoleName,
            CreateBy = p.CreateBy,
            CreateDate = p.CreateDate,
            IsApproval = p.IsApproval,
            Sections = p.Sections?.Select(MapSectionToDto).ToList()
        };

        private static SectionDto MapSectionToDto(Section s) => new()
        {
            Ref_ID = s.Ref_ID,
            Id = s.Id,
            Title = s.Title,
            IsOpen = s.IsOpen,
            IsComplete = s.IsComplete,
            Fields = s.Fields?.Select(MapFieldToDto).ToList()
        };

        private static FieldDto MapFieldToDto(Fields f) => new()
        {
            Ref_ID = f.Ref_ID,
            Label = f.Label,
            Unit = f.Unit,
            Value = f.Value,
            BaselineValue = f.BaselineValue,
            BaselineDate = f.BaselineDate,
            TargetValue = f.TargetValue,
            TargetDate = f.TargetDate,
            ChangePercentage = f.ChangePercentage
        };
    }
}

