

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("settings")]

    public class Settings : GeneralModel, IEntity
    {
        public Settings()
        {

        }
        public string Logo { get; set; }
        public bool LogIsActive { get; set; }
        public bool LogSystemErrorActive { get; set; }
        public bool GetAgencyPostService { get; set; }
        public string SiteSlogan { get; set; }
        public string SiteName { get; set; }
        public bool IsActiveSettings { get; set; }
        public bool IsCurrencyService { get; set; }
        public string FooterLogo { get; set; }
        public string CopyrightText { get; set; }
        public string CopyrightTextTitle { get; set; }

    }
}
