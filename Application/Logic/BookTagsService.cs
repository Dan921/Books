using Application.Interfaces;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;

namespace Application.Logic
{
    public class BookTagsService : IBookTagsService
    {
        private readonly ITagsRepository tagsRepository;

        public BookTagsService(ITagsRepository tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        public async Task<IEnumerable<BookTag>> GetTags()
        {
            var tags = await tagsRepository.GetAll();
            return tags;
        }

        public async Task<BookTag> GetTagById(Guid id)
        {
            var tag = await tagsRepository.GetById(id);
            return null;
        }

        public async Task<bool> InsertTag(BookTag tag)
        {
            try
            {
                await tagsRepository.Insert(tag);
                await tagsRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BookTag> UpdateTag(BookTag tag)
        {
            try
            {
                await tagsRepository.Update(tag);
                await tagsRepository.Save();
                return tag;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteTag(Guid id)
        {
            try
            {
                await tagsRepository.Delete(id);
                await tagsRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
