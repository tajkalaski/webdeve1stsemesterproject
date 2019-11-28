using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface IQuestionRepository
    {
        Task<Question> GetById(string id);
        Task<List<Question>> GetAll();

        Task<Question> Add(Question entity);
        Task Update(Question entity);
        Task Delete(Question entity);
    }
}