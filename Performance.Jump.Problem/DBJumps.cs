using Performance.Jump.Problem.EF.Context;
using System.Linq;
using System;
using Performance.Jump.Problem.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Performance.Jump.Problem
{
    class DBJumps
    {
        public static void Test1()
        {
            using var ctx = new PurchasingContext();

            var orders = ctx.PurchaseOrderHeader
                                    .Take(100)
                                    .Select(p => new PurchaseOrderHeader()
                                    {
                                        VendorId = p.VendorId
                                    }).ToList();

            foreach (var item in orders)
            {
                var vendor = ctx.Vendor.AsNoTracking().Where(v => v.BusinessEntityId == item.VendorId).First();

                Console.WriteLine(vendor.Name);
            }

            var query = (from header in ctx.PurchaseOrderHeader
                         join vendor in ctx.Vendor
                            on header.VendorId equals vendor.BusinessEntityId
                         select new
                         {
                             VendorName = vendor.Name
                         }).Take(100);

            foreach (var obj in query)
            {
                Console.WriteLine(obj.VendorName);
            }

        }

        public static void Test2()
        {
            using var ctx = new PurchasingContext();

            var query = (from header in ctx.PurchaseOrderHeader
                         join vendor in ctx.Vendor
                            on header.VendorId equals vendor.BusinessEntityId
                         select new
                         {
                             VendorName = vendor.Name
                         }).Take(100);

            foreach (var obj in query)
            {
                Console.WriteLine(obj.VendorName);
            }
        }
    }
}
