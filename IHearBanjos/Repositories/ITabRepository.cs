using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using IHearBanjos.Models;
using Azure;

namespace IHearBanjos.Repositories
{
    public interface ITabRepository
    {
        List<Tab> GetAllTabs();
        public Tab GetTabById(int id);
        void Add(Tab tab);
        List<Tab> GetTabsByBanjoistId(int id);
        public void UpdateTab(Tab tab);
        public void DeleteTab(int id);
        public void DeleteFavorite(int tabId, int banjoistId);
        public void AddFavorite(BanjoistFavorite banjoistFavorite);
        List<Tab> GetBanjoistFavoriteTabs(int id);
    }
}
