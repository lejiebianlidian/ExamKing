using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace ExamKing.Database.Core
{
    [AppDbContext("ExamConnectionString")]
    public class ExamDbContext : AppDbContext<ExamDbContext>
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {
        }
    }
}