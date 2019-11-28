using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence
{
    public class ReportingDbContext : IdentityDbContext<ApplicationUser>
    {
        public ReportingDbContext(DbContextOptions<ReportingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<TypeOfOwnership> TypesOfOwnership { get; set; }
        public DbSet<LegalForm> LegalForms { get; set; }
        public DbSet<CompanySupplier> Suppliers { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<CertificateSubRating> CertificatesSubRating { get; set; }
        public DbSet<CompanyCertificate> CompanyCertificates { get; set; }
        public DbSet<CompanyCertificateSubRating> CompanyCertificateSubRatings { get; set; }
        public DbSet<CompanyCertificateSubRatingAction> CompanyCertificateSubRatingActions { get; set; }
        public new DbSet<Role> Roles { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<AssessmentQuestion> AssessmentQuestions { get; set; }
        public DbSet<ResponsiblePerson> ResponsiblePersons { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<QuestionSubCategory> QuestionSubCategories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SubQuestion> SubQuestions { get; set; }
        public DbSet<ReportingStandardQuestion> ReportingStandardQuestions { get; set; }
        public DbSet<ReportingStandard> ReportingStandards { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<DataEntry> DataEntries { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Worksite> Worksites { get; set; }
        // Translations
        public DbSet<QuestionCategoryTranslation> QuestionCategoryTranslations { get; set; }
        public DbSet<QuestionSubCategoryTranslation> QuestionSubCategoryTranslations { get; set; }
        public DbSet<QuestionTranslation> QuestionTranslations { get; set; }
        public DbSet<SubQuestionTranslation> SubQuestionTranslations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationUser>().Ignore(au => au.UserName);

            builder.Entity<ReportingStandardQuestion>()
                .HasKey(rpq => new
                {
                    rpq.QuestionId,
                    rpq.ReportingStandardId
                });

            // builder.Entity<SubQuestionUnit>()
            //     .HasKey(squ => new
            //     {
            //         squ.SubQuestionId,
            //         squ.UnitId
            //     });

            builder.Entity<CompanySupplier>(companySupplier =>
            {
                companySupplier.HasKey(cs => new
                {
                    cs.CompanyId,
                    cs.SupplierId
                });
                companySupplier.HasOne(s => s.Company)
                    .WithMany(c => c.Suppliers)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<AssessmentQuestion>()
                .HasOne(aq => aq.Assessment)
                .WithMany(a => a.AssessmentQuestions)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}