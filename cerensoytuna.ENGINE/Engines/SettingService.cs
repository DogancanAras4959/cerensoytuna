
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.COMMON.PrivacyDTO.AboutUsDto;
using cerensoytuna.COMMON.PrivacyDTO.PolicyDto;
using cerensoytuna.COMMON.PrivacyDTO.PrivacyDto;
using cerensoytuna.COMMON.PrivacyDTO.TermsOfUsDto;
using cerensoytuna.COMMON.SetingsDTO;
using cerensoytuna.CORE.UnitOfWork;
using cerensoytuna.DAL;
using cerensoytuna.DAL.Models;
using cerensoytuna.ENGINES.Interface;

namespace cerensoytuna.ENGINES.Engines
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork<cerensoytunadbcontext> _unitOfWork;
        public SettingService(IUnitOfWork<cerensoytunadbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Setting Base

     

      

        public async Task<bool> editAboutUs(AboutUsDto model)
        {
            AboutUs aboutUsGet = await _unitOfWork.GetRepository<AboutUs>().FindAsync(x => x.Id == model.Id);

            AboutUs getAboutUs = await _unitOfWork.GetRepository<AboutUs>().UpdateAsync(new AboutUs
            {
                Id = model.Id,
                CreatedTime = aboutUsGet.CreatedTime,
                UpdatedTime = model.UpdatedTime,
                Content = model.Content,
                Title = model.Title,

            });

            return getAboutUs != null;
        }

        public async Task<bool> editPrivacy(PrivacyDto model)
        {
            Privacy privacyGet = await _unitOfWork.GetRepository<Privacy>().FindAsync(x => x.Id == model.Id);

            Privacy getPrivacy = await _unitOfWork.GetRepository<Privacy>().UpdateAsync(new Privacy
            {
                Id = model.Id,
                CreatedTime = privacyGet.CreatedTime,
                UpdatedTime = model.UpdatedTime,
                Content = model.Content,
                Title = model.Title,

            });

            return getPrivacy != null;
        }

        public async Task<bool> editSiteSettings(SettingsDto model)
        {
            Settings settingsGet = await _unitOfWork.GetRepository<Settings>().FindAsync(x => x.Id == model.Id);

            if (model.Logo == null)
            {
                model.Logo = settingsGet.Logo;
            }

            Settings getSettings = await _unitOfWork.GetRepository<Settings>().UpdateAsync(new Settings
            {
                Id = model.Id,
                Logo = model.Logo,
                SiteName = model.SiteName,
                SiteSlogan = model.SiteSlogan,
                LogIsActive = model.LogIsActive,
                IsCurrencyService = model.IsCurrencyService,
                GetAgencyPostService = model.GetAgencyPostService,
                IsActiveSettings = model.IsActiveSettings,
                LogSystemErrorActive = model.LogSystemErrorActive,
                FooterLogo = model.FooterLogo,
                CopyrightText = model.CopyrightText,
                CopyrightTextTitle = model.CopyrightTextTitle,

            });

            return getSettings != null;
        }

        public async Task<bool> editTermsOfUs(TermsOfUsDto model)
        {
            TermsOfUse termsOfUsGet = await _unitOfWork.GetRepository<TermsOfUse>().FindAsync(x => x.Id == model.Id);

            TermsOfUse getTermsOfUs = await _unitOfWork.GetRepository<TermsOfUse>().UpdateAsync(new TermsOfUse
            {
                Id = model.Id,
                CreatedTime = termsOfUsGet.CreatedTime,
                UpdatedTime = model.UpdatedTime,
                Content = model.Content,
                Title = model.Title,
   
            });

            return getTermsOfUs != null;
        }

        public AboutUsDto getAboutUs(int id)
        {
            AboutUs getAboutUs = _unitOfWork.GetRepository<AboutUs>().FindAsync(x => x.Id == id).Result;

            if (getAboutUs == null)
            {
                return new AboutUsDto();
            }

            return new AboutUsDto
            {
                Id = getAboutUs.Id,
                Content = getAboutUs.Content,
                Title = getAboutUs.Title,
                CreatedTime = getAboutUs.CreatedTime,
                UpdatedTime = getAboutUs.UpdatedTime,
  
            };
        }
        
        public PrivacyDto getPrivacy(int id)
        {
            Privacy getPrivacy = _unitOfWork.GetRepository<Privacy>().FindAsync(x => x.Id == id).Result;

            if (getPrivacy == null)
            {
                return new PrivacyDto();
            }

            return new PrivacyDto
            {
                Id = getPrivacy.Id,
                Content = getPrivacy.Content,
                Title = getPrivacy.Title,
                CreatedTime = getPrivacy.CreatedTime,
                UpdatedTime = getPrivacy.UpdatedTime,
         
            };
        }

        public SettingsDto getSettings(int id)
        {
            Settings getSetting = _unitOfWork.GetRepository<Settings>().FindAsync(x => x.Id == id).Result;

            if (getSetting == null)
            {
                return new SettingsDto();
            }

            return new SettingsDto
            {
                Id = getSetting.Id,
                LogIsActive = getSetting.LogIsActive,
                Logo = getSetting.Logo,
                SiteName = getSetting.SiteName,
                IsCurrencyService = getSetting.IsCurrencyService,
                SiteSlogan = getSetting.SiteSlogan,
                IsActiveSettings = getSetting.IsActiveSettings,
                GetAgencyPostService = getSetting.GetAgencyPostService,
                LogSystemErrorActive = getSetting.LogSystemErrorActive,
                FooterLogo = getSetting.FooterLogo,
                CopyrightText = getSetting.CopyrightText,
                CopyrightTextTitle = getSetting.CopyrightTextTitle,
       
            };
        }

        public TermsOfUsDto getTermsOfUs(int id)
        {
            TermsOfUse getTermsOfUs = _unitOfWork.GetRepository<TermsOfUse>().FindAsync(x => x.Id == id).Result;

            if (getTermsOfUs == null)
            {
                return new TermsOfUsDto();
            }

            return new TermsOfUsDto
            {
                Id = getTermsOfUs.Id,
                Content = getTermsOfUs.Content,
                Title = getTermsOfUs.Title,
                CreatedTime = getTermsOfUs.CreatedTime,
                UpdatedTime = getTermsOfUs.UpdatedTime,
          
            };
        }

        #endregion

        #region Policy

        public CookiePolicyDto getCookiePrivacy(int id)
        {
            CookiePolicy getPrivacy = _unitOfWork.GetRepository<CookiePolicy>().FindAsync(x => x.Id == id).Result;

            if (getPrivacy == null)
            {
                return new CookiePolicyDto();
            }

            return new CookiePolicyDto
            {
                Id = getPrivacy.Id,
                Content = getPrivacy.Content,
                Title = getPrivacy.Title,
                CreatedTime = getPrivacy.CreatedTime,
                UpdatedTime = getPrivacy.UpdatedTime,
 
            };
        }

        public async Task<bool> editCookiePrivacy(CookiePolicyDto model)
        {
            CookiePolicy privacyGet = await _unitOfWork.GetRepository<CookiePolicy>().FindAsync(x => x.Id == model.Id);

            CookiePolicy getPrivacy = await _unitOfWork.GetRepository<CookiePolicy>().UpdateAsync(new CookiePolicy
            {
                Id = model.Id,
                CreatedTime = privacyGet.CreatedTime,
                UpdatedTime = model.UpdatedTime,
                Content = model.Content,
                Title = model.Title,
     
            });

            return getPrivacy != null;
        }

        public BrandPolicyDto getBrandPrivacy(int id)
        {
            BrandPolicy getPrivacy = _unitOfWork.GetRepository<BrandPolicy>().FindAsync(x => x.Id == id).Result;

            if (getPrivacy == null)
            {
                return new BrandPolicyDto();
            }

            return new BrandPolicyDto
            {
                Id = getPrivacy.Id,
                Content = getPrivacy.Content,
                Title = getPrivacy.Title,
                CreatedTime = getPrivacy.CreatedTime,
                UpdatedTime = getPrivacy.UpdatedTime,
    
            };
        }

        public async Task<bool> editBrandPrivacy(BrandPolicyDto model)
        {
            BrandPolicy privacyGet = await _unitOfWork.GetRepository<BrandPolicy>().FindAsync(x => x.Id == model.Id);

            BrandPolicy getPrivacy = await _unitOfWork.GetRepository<BrandPolicy>().UpdateAsync(new BrandPolicy
            {
                Id = model.Id,
                CreatedTime = privacyGet.CreatedTime,
                UpdatedTime = model.UpdatedTime,
                Content = model.Content,
                Title = model.Title,

            });

            return getPrivacy != null;
        }

        public StreamPolicyDto getStreamPrivacy(int id)
        {
            StreamPolicy getPrivacy = _unitOfWork.GetRepository<StreamPolicy>().FindAsync(x => x.Id == id).Result;

            if (getPrivacy == null)
            {
                return new StreamPolicyDto();
            }

            return new StreamPolicyDto
            {
                Id = getPrivacy.Id,
                Content = getPrivacy.Content,
                Title = getPrivacy.Title,
                CreatedTime = getPrivacy.CreatedTime,
                UpdatedTime = getPrivacy.UpdatedTime,
  
            };
        }

        public async Task<bool> editStreamPrivacy(StreamPolicyDto model)
        {
            StreamPolicy privacyGet = await _unitOfWork.GetRepository<StreamPolicy>().FindAsync(x => x.Id == model.Id);

            StreamPolicy getPrivacy = await _unitOfWork.GetRepository<StreamPolicy>().UpdateAsync(new StreamPolicy
            {
                Id = model.Id,
                CreatedTime = privacyGet.CreatedTime,
                UpdatedTime = model.UpdatedTime,
                Content = model.Content,
                Title = model.Title,
    
            });

            return getPrivacy != null;
        }

        #endregion
    }
}
