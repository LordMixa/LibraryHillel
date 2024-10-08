﻿namespace LibraryDAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(int id);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
    }
}
