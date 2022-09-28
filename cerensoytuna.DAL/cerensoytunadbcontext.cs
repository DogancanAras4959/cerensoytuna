using cerensoytuna.DAL.Mapping;
using cerensoytuna.DAL.Models;
using DOMAIN.DataAccessLayer.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cerensoytuna.DAL
{
    public class cerensoytunadbcontext : DbContext
    {
        public cerensoytunadbcontext(DbContextOptions<cerensoytunadbcontext> options) : base(options)
        {

        }
        public virtual DbSet<Users> users { get; set; }
        public virtual DbSet<Post> news { get; set; }
        public virtual DbSet<PublishType> publishTypes { get; set; }
        public virtual DbSet<Tags> tagNames { get; set; }
        public virtual DbSet<TagPost> tagNews { get; set; }
        public virtual DbSet<Settings> setting { get; set; }
        public virtual DbSet<Privacy> privacy { get; set; }
        public virtual DbSet<AboutUs> aboutus { get; set; }
        public virtual DbSet<TermsOfUse> termsofUse { get; set; }
        public virtual DbSet<BrandPolicy> brand { get; set; }
        public virtual DbSet<CookiePolicy> cookie { get; set; }
        public virtual DbSet<StreamPolicy> stream { get; set; }
        public virtual DbSet<SeoScore> seoScores { get; set; }
        public virtual DbSet<SeoCheckMeta> seoCheckMeta { get; set; }
        public virtual DbSet<Media> medias { get; set; }
        public virtual DbSet<RoleUsers> roleUsers { get; set; }
        public virtual DbSet<Roles> roles { get; set; }
        public virtual DbSet<Language> langs { get; set; }
        public virtual DbSet<PostLanguage> postLanguages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AboutUsMapping());
            modelBuilder.ApplyConfiguration(new BrandMapping());
            modelBuilder.ApplyConfiguration(new CookieMapping());
            modelBuilder.ApplyConfiguration(new MediaMapping());
            modelBuilder.ApplyConfiguration(new PostMapping());
            modelBuilder.ApplyConfiguration(new PrivacyMapping());
            modelBuilder.ApplyConfiguration(new PublishTypeMapping());
            modelBuilder.ApplyConfiguration(new RolesMapping());
            modelBuilder.ApplyConfiguration(new SeoCheckMetaMapping());
            modelBuilder.ApplyConfiguration(new SeoScoreMapping());
            modelBuilder.ApplyConfiguration(new SettingsMapping());
            modelBuilder.ApplyConfiguration(new StreamMapping());
            modelBuilder.ApplyConfiguration(new TagMapping());
            modelBuilder.ApplyConfiguration(new TagPostMapping());
            modelBuilder.ApplyConfiguration(new TermsOfUseMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new LanguageMapping());
            modelBuilder.ApplyConfiguration(new PostLanguageMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
