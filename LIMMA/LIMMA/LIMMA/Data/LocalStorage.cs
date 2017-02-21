using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Helper;
using SQLite;

namespace LIMMA.Data
{
    public class LocalStorage
    {
        private readonly SQLiteAsyncConnection storage;

        public LocalStorage(string dbPath)
        {
            storage = new SQLiteAsyncConnection(dbPath);
            storage.CreateTableAsync<UserToken>().Wait();

        }

        public Task<List<UserToken>> GetTokensAsync()
        {
            return storage.Table<UserToken>().ToListAsync();
        }

        public Task<List<UserToken>> GetLastTokenAsync()
        {
            return storage.QueryAsync<UserToken>("SELECT TOP 1 * FROM [UserToken] ORDER BY [Expires] DESC");
        }

        public Task<UserToken> GetUserTokenAsync()
        {
            return
                storage.Table<UserToken>()
                    .Where(i => Convert.ToDateTime(i.Expires) >= DateTime.Now)
                    .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(UserToken token)
        {
            if (token.ID != 0)
            {
                return storage.UpdateAsync(token);
            }
            else
            {
                return storage.InsertAsync(token);
            }
        }

        public Task<int> DeleteItemAsync(UserToken token)
        {
            return storage.DeleteAsync(token);
        }

    }
}
