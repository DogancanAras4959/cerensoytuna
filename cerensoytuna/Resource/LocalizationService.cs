using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace cerensoytuna.Resource
{
    public class LocalizationService
    {
        private readonly IStringLocalizer _localize;
        public LocalizationService(IStringLocalizerFactory factory)
        {
            var type = typeof(ApplicationResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName!);
            _localize = factory.Create("ApplicationResource", assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return _localize[key];
        }
    }
}
