using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Admin.Mobile.Helpers
{
    public class FavoriteCacheService : IFavoriteCacheService
    {

        public bool GetFavorite(long productId)
        {
            return Preferences.Get(GetFavoriteKey(productId), false);
        }

        public void SetFavorite(long productId, bool value)
        {
            Preferences.Set(GetFavoriteKey(productId), value);
        }
        private static string GetFavoriteKey(long productId) => $"product_favorite_{productId}";
    }
}
