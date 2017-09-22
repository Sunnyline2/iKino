using System;
using iKino.API.Repositories.Interfaces;

namespace iKino.API.Repositories
{
    public class Database : IDatabase<CinemaContext>, IDisposable
    {
        public Database(CinemaContext context)
        {
            Context = context;
        }

        public CinemaContext Context { get; }


        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}