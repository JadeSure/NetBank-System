using a2_s3736719_s3677615.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace a2_s3736719_s3677615
{
    public static class MainHolding
    {
        // make this thread keep running in the background
        static System.Threading.Timer timer;
        public static void RunBillPayHolding(IServiceProvider service)
        {
            var startTime = TimeSpan.Zero;
            // periodically check by 1 min
            var periodTime = TimeSpan.FromMinutes(1);

            timer = new System.Threading.Timer((e) =>
            {
                RunBillPayShedule(service);
            }, null, startTime, periodTime);
        }

        
        private static void RunBillPayShedule(IServiceProvider service)
        {
            // get the database connection
            var context = new NwbaDbContext(service.GetRequiredService<DbContextOptions<NwbaDbContext>>());
            BillPayFactory billPayFactory = new BillPayFactory(context.BillPays.ToList(), context);
            billPayFactory.RunBillPaySchedule();

            context.SaveChangesAsync();


        }




    }
}