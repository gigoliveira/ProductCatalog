using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Admin.Mobile.Helpers
{
    public interface IFavoriteCacheService
    {
        bool GetFavorite(long id);
        void SetFavorite(long id, bool value);
    }
}
