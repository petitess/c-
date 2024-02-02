﻿using FinShark.Dtos.Stock;
using FinShark.Helpers;
using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<IEnumerable<object>?> GetAllForUserAsync(string userId);
        Task<List<Stock>> GetAllTinyAsync(bool OrderByDesc);
        Task<List<Stock>> SearchAsync(string search);
        Task<List<Stock>> GetAllFilteredAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetByIdTinyAsync(int id);
        Task<Stock?> GetByIdUserAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
