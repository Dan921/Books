using Data;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(BooksContext context)
        {
            this.context = context;
        }

        public async Task<Book> GetBookById(Guid Id)
        {
            return await context.Books.FindAsync(Id);
        }

        public async Task<List<Book>> GetBooks()
        {
            return await context.Books.ToListAsync();
        }

        public async Task InsertBook(Book book)
        {
            await context.Books.AddAsync(book);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<bool> BookExist(Guid id)
        {
            return await context.Books.AnyAsync(e => e.Id == id);
        }

        public async Task UpdateBook(Book book)
        {
            await Task.Run(() => context.Entry(book).State = EntityState.Modified);
        }
    }
}
