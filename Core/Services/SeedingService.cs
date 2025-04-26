using Core.Interfaces;
using Core.Repository;
using Core.ViewModel;
using Databases.Models.Databases.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class SeedingService
    {
        private readonly IRepository<TBL_Primary> _primaryRepo;

        public SeedingService(IRepository<TBL_Primary> primaryRepo)
        {
            _primaryRepo = primaryRepo;
        }

        public async Task SeedFromJsonAsync(TBL_Primary primaryData)
        {
            if (primaryData == null) return;

            var primary = new TBL_Primary
            {
                OrgenaztionId = primaryData.OrgenaztionId,
                RegionName = primaryData.RegionName,
                RoleName = primaryData.RoleName,
                CreateBy = primaryData.CreateBy,
                CreateDate =  DateTime.Now,
                IsApproval = primaryData.IsApproval,
                Sections = primaryData.Sections?.Select(s => new Section
                {
                    Id = s.Id,
                    Title = s.Title,
                    Fields = s.Fields?.Select(f => new Fields
                    {
                        Id = f.Id,
                        Label = f.Label,
                        Unit = f.Unit,
                        Value = f.Value,
                        BaselineValue = f.BaselineValue,
                        BaselineDate = f.BaselineDate,
                        TargetValue = f.TargetValue,
                        TargetDate = f.TargetDate,
                        ChangePercentage = f.ChangePercentage
                    }).ToList()
                }).ToList()
            };

            await _primaryRepo.AddAsync(primary);
            await _primaryRepo.SaveAsync();
        }
        public async Task<IEnumerable<TBLPrimaryDto>> GetAllAsync()
        {
            return await _primaryRepo.GetAllAsync();
        }

        public async Task<TBL_Primary?> GetByIdAsync(object id)
        {
            return await _primaryRepo.GetByIdAsync(id);
        }       

        public void Update(TBL_Primary entity)
        {
            _primaryRepo.Update(entity);
        }

        public void Delete(TBL_Primary entity)
        {
            _primaryRepo.Delete(entity);
        }
        public async Task SaveAsync()
        {
            await _primaryRepo.SaveAsync();
        }
    }
}
