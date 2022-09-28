using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.COMMON.PrivacyDTO.AboutUsDto;
using cerensoytuna.COMMON.PrivacyDTO.PolicyDto;
using cerensoytuna.COMMON.PrivacyDTO.PrivacyDto;
using cerensoytuna.COMMON.PrivacyDTO.TermsOfUsDto;
using cerensoytuna.COMMON.SetingsDTO;

namespace cerensoytuna.ENGINES.Interface
{
    public interface ISettingService
    {
        #region SettingBaseService

        SettingsDto getSettings(int id);
        Task<bool> editSiteSettings(SettingsDto model);
        AboutUsDto getAboutUs(int id);
        Task<bool> editAboutUs(AboutUsDto model);
        PrivacyDto getPrivacy(int id);
        Task<bool> editPrivacy(PrivacyDto model);
        CookiePolicyDto getCookiePrivacy(int id);
        Task<bool> editCookiePrivacy(CookiePolicyDto model);
        BrandPolicyDto getBrandPrivacy(int id);
        Task<bool> editBrandPrivacy(BrandPolicyDto model);
        StreamPolicyDto getStreamPrivacy(int id);
        Task<bool> editStreamPrivacy(StreamPolicyDto model);
        TermsOfUsDto getTermsOfUs(int id);
        Task<bool> editTermsOfUs(TermsOfUsDto model);

        #endregion
    }
}
