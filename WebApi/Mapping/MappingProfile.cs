using AutoMapper;
using RespaunceV2.Core.Models;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserResource>()
                .ForMember(ur => ur.CompanyId, opt => opt.MapFrom(au => au.Company.Id))
                .ForMember(ur => ur.CompanyName, opt => opt.MapFrom(au => au.Company.Name))
                .ForMember(ur => ur.Role, opt => opt.MapFrom(au => au.Role.Name))
                .ForMember(ur => ur.Language, opt => opt.MapFrom(au => au.Language.Name));

            CreateMap<UserResource, ApplicationUser>()
                .ForMember(au => au.Role, opt => opt.Ignore())
                .ForMember(au => au.Company, opt => opt.Ignore())
                .ForMember(au => au.Language, opt => opt.Ignore())
                .ForMember(au => au.CompanyId, opt => opt.MapFrom(ur => ur.CompanyId))
                .ForMember(au => au.RoleId, opt => opt.MapFrom(ur => ur.Role))
                .ForMember(au => au.LanguageId, opt => opt.MapFrom(ur => ur.Language));

            CreateMap<SupplierUserResource, ApplicationUser>()
                .ForMember(au => au.Role, opt => opt.Ignore())
                .ForMember(au => au.Company, opt => opt.Ignore())
                .ForMember(au => au.Language, opt => opt.Ignore())
                .ForMember(au => au.LanguageId, opt => opt.MapFrom(ur => ur.Language))
                .ForMember(au => au.Email, opt => opt.MapFrom(ur => ur.ContactPersonEmail));

            CreateMap<UpdateProfileResource, ApplicationUser>()
                .ForMember(au => au.Role, opt => opt.Ignore())
                .ForMember(au => au.Company, opt => opt.Ignore())
                .ForMember(au => au.Language, opt => opt.Ignore())
                .ForMember(au => au.LanguageId, opt => opt.MapFrom(ur => ur.Language));

            CreateMap<UpdateProfileResource, UserResource>()
                .ForMember(ur => ur.CompanyName, opt => opt.Ignore())
                .ForMember(ur => ur.Role, opt => opt.Ignore());

            CreateMap<ManageCompanyResource, Company>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.LegalForm, opt => opt.Ignore())
                .ForMember(c => c.TypeOfOwnership, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreatedOn, opt => opt.Ignore());

            CreateMap<LegalForm, LegalFormResource>();
            CreateMap<LegalFormResource, LegalForm>();

            CreateMap<TypeOfOwnership, TypeOfOwnershipResource>();
            CreateMap<TypeOfOwnershipResource, TypeOfOwnership>();

            CreateMap<Company, ManageCompanyResource>();

            CreateMap<Role, RoleResource>();

            CreateMap<RoleResource, Role>();

            CreateMap<LanguageResource, Language>()
                .ForMember(l => l.CreatedBy, opt => opt.Ignore())
                .ForMember(l => l.CreatedOn, opt => opt.Ignore());

            CreateMap<Language, LanguageResource>();

            CreateMap<QuestionCategory, QuestionCategoryResource>()
                .ForMember(qcr => qcr.Expanded, opt => opt.Ignore());

            CreateMap<QuestionSubCategory, QuestionSubCategoryResource>()
                .ForMember(qscr => qscr.Expanded, opt => opt.Ignore());

            CreateMap<Question, QuestionResource>()
                .ForMember(q => q.Expanded, opt => opt.Ignore());

            CreateMap<SubQuestion, SubQuestionResource>()
                .ForMember(q => q.Selected, opt => opt.Ignore())
                .ForMember(q => q.ResponsiblePerson, opt => opt.Ignore());

            CreateMap<Assessment, AssessmentResource>();

            CreateMap<AssessmentResource, Assessment>()
                .ForMember(a => a.Company, opt => opt.Ignore());

            CreateMap<Assessment, UpdateAssessmentResource>();

            CreateMap<UpdateAssessmentResource, Assessment>()
                .ForMember(a => a.Company, opt => opt.Ignore());

            CreateMap<Assessment, CreateAssessmentResource>();

            CreateMap<CreateAssessmentResource, Assessment>()
                .ForMember(a => a.Company, opt => opt.Ignore());

            CreateMap<AssessmentQuestionResource, AssessmentQuestion>()
                .ForMember(aq => aq.Assessment, opt => opt.Ignore())
                .ForMember(aq => aq.SubQuestion, opt => opt.Ignore())
                .ForMember(aq => aq.CreatedBy, opt => opt.Ignore())
                .ForMember(aq => aq.CreatedOn, opt => opt.Ignore())
                .ForMember(aq => aq.SubQuestion, opt => opt.Ignore())
                .ForMember(aq => aq.ResponsiblePerson, opt => opt.Ignore());

            CreateMap<AssessmentQuestion, AssessmentQuestionResource>()
                .ForMember(aqr => aqr.ResponsiblePerson, opt => opt.MapFrom(aq => aq.ResponsiblePerson.ApplicationUser));

            CreateMap<UpdateAssessmentResource, Assessment>();
  
            CreateMap<SubQuestion, DataEntrySubQuestionResource>();

            CreateMap<Question, DataEntryQuestionResource>();

            CreateMap<ManageSupplierResource, CompanySupplier>()
                .ForMember(cs => cs.Company, opt => opt.Ignore())
                .ForMember(cs => cs.Supplier, opt => opt.Ignore());

            CreateMap<CompanyCertificate, CompanyCertificateResource>()
                .ForMember(ccr => ccr.CertificateName, opt => opt.MapFrom(cc => cc.Certificate.Name));

            CreateMap<CompanyCertificateResource, CompanyCertificate>();

            CreateMap<CertificateResource, Certificate>();

            CreateMap<Certificate, CertificateResource>();

            CreateMap<DataEntry, DataEntryResource>();
        }
    }
}