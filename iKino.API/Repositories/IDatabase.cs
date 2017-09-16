using Microsoft.EntityFrameworkCore;
using System;

namespace iKino.API.Repositories
{
    public interface IDatabase<out T> where T : DbContext, IDisposable
    {
        T Context { get; }
    }
}