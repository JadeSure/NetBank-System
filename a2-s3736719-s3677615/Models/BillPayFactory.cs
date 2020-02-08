using a2_s3736719_s3677615.Data;
using a2_s3736719_s3677615.Models;
using System;
using System.Collections.Generic;
using a2_s3736719_s3677615.Utilities;

namespace a2_s3736719_s3677615
{
    internal class BillPayFactory
    {
        private List<BillPay> BillPayList;
        private NwbaDbContext Context;

        public BillPayFactory(List<BillPay> billPayList, NwbaDbContext context)
        {
            this.BillPayList = billPayList;
            this.Context = context;
        }

        public void RunBillPaySchedule()
        {
            BillPayList.ForEach(bill =>
           {
               var rightnow = DateTime.UtcNow;

                // ignore second
                rightnow.AddSeconds(rightnow.Second);

                // ignore milliseconds
                rightnow.AddMilliseconds(rightnow.Millisecond);

               var billPeriod = bill.Period;

               // check eatch billPeriod to match that in the system
               switch (billPeriod)
               {
                   // monthly
                   case Period.M:
                       if (DateTimeChecking.SameDifferentMonth(bill.ScheduleDate, rightnow) && bill.BillPayStatus == BillPayStatus.Ready)
                       {
                           PayBill(bill);
                       }
                       break;

                   // annually
                   case Period.Y:
                       if (DateTimeChecking.SameDifferentYear(bill.ScheduleDate, rightnow) && bill.BillPayStatus == BillPayStatus.Ready)
                       {
                           PayBill(bill);
                       }
                       break;

                    // quarterly
                   case Period.Q:
                       if (DateTimeChecking.SameDifferentQuarter(bill.ScheduleDate, rightnow) && bill.BillPayStatus == BillPayStatus.Ready)
                       {
                           PayBill(bill);
                       }
                       break;

                    // once only
                   case Period.S:
                       if (DateTimeChecking.SameDifferentMin(bill.ScheduleDate, rightnow) && bill.BillPayStatus == BillPayStatus.Ready)
                       { 
                           PayBill(bill);
                       }
                       break;
               }

           }
                );
        }


        // real inplementation for bill pay
        private bool PayBill(BillPay billPay)
        {
            try
            {
                billPay.Account.MakeBillPay(billPay.Amount);
                Context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }

        }



    }
}