using Clebra.Loopus.DataAccess;
using Clebra.Loopus.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clebra.Loopus.Service.AboutDataService
{
    public class AboutDataService : IAboutService
    {
        private readonly LoopusDataContext dataContext;

        public AboutDataService(LoopusDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<About> GetAsync(Expression<Func<About, bool>> query)
            => await dataContext.Abouts.FirstOrDefaultAsync(query);

        public async Task<IEnumerable<About>> GetListAsync(Expression<Func<About, bool>> query)
            => await dataContext.Abouts.Where(query).ToListAsync();

        public async Task SubmitChangeAsync(About entity)
        {
            try
            {

                dataContext.Entry<About>(entity).State = await dataContext.Abouts.AnyAsync(a => a.Id == entity.Id)
                    ? EntityState.Modified
                    : EntityState.Added;

                await dataContext.SaveChangesAsync();

                dataContext.Entry<About>(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

