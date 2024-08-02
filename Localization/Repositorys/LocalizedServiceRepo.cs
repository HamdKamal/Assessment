using Localization.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localization.Repositorys
{
    public class LocalizedServiceRepo : ILocalizedService
    {
        private readonly IStringLocalizer<LocalizedServiceRepo> _localizer;

        public LocalizedServiceRepo(IStringLocalizer<LocalizedServiceRepo> localizer)
        {
            _localizer = localizer;
        }

        public string GetLocalizedMessage()
        {
            return _localizer["Hello World"];
        }
    }
}
