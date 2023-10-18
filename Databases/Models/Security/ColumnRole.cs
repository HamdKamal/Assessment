using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Models.Security
{
    public class ColumnRole : IdentityRole
    {
        public required string NameAr { get; set; }
    }
}
