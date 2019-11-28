using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;


namespace RespaunceV2.Core.Interfaces {
    public interface IDataEntryRepository {
        Task<List<DataEntry>> GetByCompany(string companyId);
        Task<DataEntry> GetById(string id);
        Task<List<DataEntry>> GetAll();
        Task<List<DataEntry>> GetByDivisions(string[] DivisionIds);
        Task<List<DataEntry>> GetByWorksites(string[] WorksiteIds);
        Task<DataEntry> Add(DataEntry dataEntry);
        Task Update(DataEntry dataEntry);
        Task Delete(DataEntry dataEntry);
    }
}
