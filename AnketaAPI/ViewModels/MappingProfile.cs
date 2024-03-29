﻿using AnketaAPI.Models;
using AnketaAPI.Models.Identity;
using AnketaAPI.ViewModels.IdentitiVM;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using static Azure.Core.HttpHeader;

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

            CreateMap<UserCatalogSurveryVM, UserCatalogSurvery>();
            CreateMap<UserCatalogSurvery, UserCatalogSurveryVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                ;

            CreateMap<UserQuestionAnswerVM, CatalogSurveyQuestion>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
                .ForPath(dest => dest.Question.Name, opt => opt.MapFrom(src => src.QuestionName));
                //.ForMember(dest => dest.Question.Name, opt => opt.MapFrom(src => src.QuestionName != null ? src.QuestionName : "")); //Don't working if ?Question
            CreateMap<CatalogSurveyQuestion, UserQuestionAnswerVM>();

            CreateMap<QuestionAnswerUserVM, QuestionAnswer>();
            CreateMap<QuestionAnswer, QuestionAnswerUserVM>();

            CreateMap<UserVM, User>();
            CreateMap<User, UserVM>();

            CreateMap<UserTestsVM, UserCatalogSurvery>();
            CreateMap<UserCatalogSurvery, UserTestsVM>()
                .ForMember(dest => dest.CatalogSurveyId, opt => opt.MapFrom(src => src.CatalogSurveyId))
                .ForMember(dest => dest.CatalogSurveyName, opt => opt.MapFrom(src => src.CatalogSurvey.Name))
                .ForMember(dest => dest.CatalogSurveyCreated, opt => opt.MapFrom(src => src.CatalogSurvey.Created));

            //Identity
            CreateMap<RoleVM, IdentityRole>();
            CreateMap<IdentityRole, RoleVM>();

            CreateMap<ApplicationUserVM, ApplicationUser>();
            CreateMap<ApplicationUser, ApplicationUserVM>();
        }
    }
}
