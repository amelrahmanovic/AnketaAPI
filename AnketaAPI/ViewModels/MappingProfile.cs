using AnketaAPI.Models;
using AutoMapper;

namespace AnketaAPI.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatalogSurvey, CatalogSurveyVM>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.CatalogSurveyQuestion.Count()))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserCatalogSurvery.Count()))
                ; // Mapping from CatalogSurvey to CatalogSurveyVM
            CreateMap<CatalogSurveyVM, CatalogSurvey>();

            CreateMap<CatalogSurveyQuestionVM, CatalogSurveyQuestion>();
            CreateMap<CatalogSurveyQuestion, CatalogSurveyQuestionVM>();

            CreateMap<QuestionVM, Question>();
            CreateMap<Question, QuestionVM>();

            CreateMap<QuestionAnswerVM, QuestionAnswer>();
            CreateMap<QuestionAnswer, QuestionAnswerVM>();

            CreateMap<QuestionAnswerNewVM, QuestionAnswer>();
            CreateMap<QuestionAnswer, QuestionAnswerNewVM>();
        }
    }
}
