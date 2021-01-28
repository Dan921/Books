using Data;
using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BookRepository : RepositoryBase, IBookRepository
    {
        public BookRepository(BooksContext context)
        {
            this.context = context;
        }

        public async Task<Book> GetBookById(Guid Id)
        {
            return await context.Books.FindAsync(Id);
        }

        public async Task<IEnumerable<Book>> GetBooks()
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

        public async Task UpdateBook(Book book)
        {
            await Task.Run(() => context.Entry(book).State = EntityState.Modified);
        }

        public async Task DeleteBook(Guid id)
        {
            await Task.Run(() =>
            {
                var book = context.Books.Find(id);
                context.Books.Remove(book);
            });
        }
    }
}
