using System;
using Microsoft.EntityFrameworkCore;

namespace iKino.API.Repositories.Interfaces
{
    public interface IDatabase<out T> where T : DbContext, IDisposable
    {
        T Context { get; }
    }
}