//using Databases.Models.Databases.Models;

//public async Task EditFromJsonAsync(TBL_Primary primaryData)
//{
//    if (primaryData == null) return;

//    var existingPrimary = await _primaryRepo.GetByIdAsync(primaryData.Ref_Id);
//    if (existingPrimary == null)
//        throw new Exception("Primary data not found.");

//    existingPrimary.OrgenaztionId = primaryData.OrgenaztionId;
//    existingPrimary.RegionName = primaryData.RegionName;
//    existingPrimary.RoleName = primaryData.RoleName;
//    existingPrimary.CreateBy = primaryData.CreateBy;
//    existingPrimary.CreateDate = DateTime.Now;
//    existingPrimary.IsApproval = primaryData.IsApproval;

//    // Handle Sections
//    existingPrimary.Sections = primaryData.Sections?.Select(s => new Section
//    {
//        Id = s.Id,
//        Title = s.Title,
//        IsComplete = s.IsComplete,
//        IsOpen = s.IsOpen,
//        Base64 = s.Base64,
//        Filetype = s.Filetype,
//        FileName = s.FileName,
//        Fields = s.Fields?.Select(f => new Fields
//        {
//            Id = f.Id,
//            Label = f.Label,
//            Unit = f.Unit,
//            Value = f.Value,
//            BaselineValue = f.BaselineValue,
//            BaselineDate = f.BaselineDate,
//            TargetValue = f.TargetValue,
//            TargetDate = f.TargetDate,
//            ChangePercentage = f.ChangePercentage
//        }).ToList()
//    }).ToList();

//    _primaryRepo.Update(existingPrimary);
//    await _primaryRepo.SaveAsync();
//}
