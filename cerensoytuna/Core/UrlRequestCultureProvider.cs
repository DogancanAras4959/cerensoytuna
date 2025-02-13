using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cerensoytuna.Core
{
    public class UrlRequestCultureProvider : RequestCultureProvider
    {
        private static readonly Regex PartLocalePattern = new Regex(@"^[a-z]{2}(-[a-z]{2,4})?$", RegexOptions.IgnoreCase);
        private static readonly Regex FullLocalePattern = new Regex(@"^[a-z]{2}-[A-Z]{2}$", RegexOptions.IgnoreCase);

        private static readonly Dictionary<string, string> LanguageMap = new Dictionary<string, string>
        {
            { "tr", "tr-TR" },
            { "en", "en-US" },
            { "de", "de-DE" }
        };

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var parts = httpContext.Request.Path.Value.Split('/');
            var culture = parts[1];

            if (parts.Length < 3)
            {
                return Task.FromResult<ProviderCultureResult>(null);
            }

            if (FullLocalePattern.IsMatch(culture))
            {
                return Task.FromResult(new ProviderCultureResult(culture));
            }

            if (PartLocalePattern.IsMatch(culture))
            {
                var fullCulture = LanguageMap[culture];
                return Task.FromResult(new ProviderCultureResult(fullCulture));
            }

            return Task.FromResult<ProviderCultureResult>(null);
        }
    }
}
