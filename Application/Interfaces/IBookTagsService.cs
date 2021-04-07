using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookTagsService
    {
        Task<IEnumerable<BookTag>> GetTags();
        Task<BookTag> GetTagById(Guid id);
        Task<bool> InsertTag(BookTag tag);
        Task<BookTag> UpdateTag(BookTag tag);
        Task<bool> DeleteTag(Guid Id);
    }
}
