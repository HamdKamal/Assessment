using Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPrimaryRepository
    {
        Task AddAsync(TBLPrimaryDto model);
        Task EditAsync(int id, TBLPrimaryDto model);
    }
}
