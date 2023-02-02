using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.DataAccess.Models.DashboardReportModel;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfReceiptDal : EfRepositoryDal<Receipt, DataContext>, IReceiptDal
    {
        public DashboardViewModel dashboard(int companyid, int branchid)
        {
            using (var db = new DataContext())
            {
                var entity = new DashboardViewModel();
                decimal sumKg = decimal.Zero;
                decimal dailyEarning = decimal.Zero;

                #region 1.Günlük Giren Toplam Kg
                var query = db.Receipt.Where(x => x.ReceiptDate.Year == DateTime.Now.Year && x.ReceiptDate.Month == DateTime.Now.Month && x.ReceiptDate.Day == DateTime.Now.Day && x.BranchId == branchid && x.İsDelete == false);
                #endregion
                #region  //Günlük En Fazla Stok Arttıran Ürün ismi
                var customer = query.Where(x => x.IsWhat == true);
                if (customer.Count() > 0)
                {
                    var customerProductQuantity = db.productQuantity.Where(x => x.CreatedDate.Year == customer.Select(x => x.ReceiptDate).FirstOrDefault().Year && x.CreatedDate.Month == customer.Select(x => x.ReceiptDate).FirstOrDefault().Month
                    && x.CreatedDate.Day == customer.Select(x => x.ReceiptDate).FirstOrDefault().Day).GroupBy(x => x.Name).Select(y => new
                    {
                        name = y.Key,
                        sum = y.Sum(x => x.Kg)
                    });
                    var orderDescding = customerProductQuantity.OrderByDescending(x => x.sum).FirstOrDefault();
                    entity.StockBoosterProductName = orderDescding.name;
                    entity.StockBoosterProduct = orderDescding.sum;

                    //Günlük En Fazla Alım Yapılan Müş.
                    var productCustomer = db.productQuantity.Where(x => x.CreatedDate.Year == customer.ToList()[0].ReceiptDate.Year && x.CreatedDate.Month == customer.ToList()[0].ReceiptDate.Month
                   && x.CreatedDate.Day == customer.ToList()[0].ReceiptDate.Day).GroupBy(x => x.ReceiptId).Select(y => new
                   {
                       receiptId = y.Key,
                       sum = y.Sum(x => x.Kg),
                       customerId = customer.Where(x => x.id == y.Key).FirstOrDefault().CustomerId
                   }).ToList();
                    var productOrderDesceding = productCustomer.OrderByDescending(x => x.sum).FirstOrDefault();
                    var receiptdata = db.Receipt.Where(x => x.BranchId == branchid && x.id == productOrderDesceding.receiptId).FirstOrDefault();
                    var customerNameAndSurname = db.Customers.Where(x => x.id == receiptdata.CustomerId).FirstOrDefault();
                    entity.GetDailyProductByCustomerName = String.Concat(customerNameAndSurname.Name, " ", customerNameAndSurname.Surname);
                    entity.GetDailyProductByCustomerKg = productOrderDesceding.sum;
                    var customerList = new List<Customer>();
                    var value = productCustomer.OrderByDescending(x => x.sum).Take(5).ToList();
                    var customerData = db.Customers.Where(x => x.İsDelete == false).ToList();
                    foreach (var item in value)
                    {
                        customerList.Add(new Customer
                        {
                            Name = customerData.FirstOrDefault(x => x.id == item.customerId).Name
                        });
                    }
                    entity.TopPurchasedCustomers = customerList;







                }
                #endregion
                #region  //Günlük En Fazla Stok eksilten Ürün ismi
                var factory = query.Where(x => x.IsWhat == false);
                if (factory.Count() > 0)
                {
                    var factoryProductQuantity = db.factoryQuantities.Where(x => x.CreatedDate.Year == factory.ToList()[0].ReceiptDate.Year && x.CreatedDate.Month == factory.ToList()[0].ReceiptDate.Month
                    && x.CreatedDate.Day == factory.ToList()[0].ReceiptDate.Day).GroupBy(x => x.Name).Select(y => new
                    {
                        name = y.Key,
                        sum = y.Sum(x => x.Kg)
                    });
                    var orderDescding = factoryProductQuantity.OrderByDescending(x => x.sum).FirstOrDefault();
                    if (orderDescding is not null)
                    {
                        entity.StockMostProduckName = orderDescding.name;
                        entity.StockMostProduck = orderDescding.sum;
                    }

                }

                #endregion

                foreach (var item in query.Where(x => x.IsWhat == true).ToList())
                {

                    sumKg += db.productQuantity.Where(x => x.ReceiptId == item.id).ToList().Sum(x => x.Kg);
                    //Günlük Sermaye Toplam Ücret
                    dailyEarning += db.PaymentCont.Where(x => x.ReceiptId == item.id && x.CreatedDate.Year == DateTime.Now.Year && x.CreatedDate.Month == DateTime.Now.Month && x.CreatedDate.Day == DateTime.Now.Day).Sum(x => x.TotalPrice);
                }
                entity.GetDailySumKg = sumKg;
                entity.GiveDailyPrice = dailyEarning;
                dailyEarning = decimal.Zero;
                sumKg = decimal.Zero;
                //2.Günlük Çıkan Toplam Kg
                foreach (var item in query.Where(x => x.IsWhat == false).ToList())
                {
                    sumKg += db.factoryQuantities.Where(x => x.ReceiptId == item.id).ToList().Sum(x => x.Kg);
                    //Günlük Kazanç
                    dailyEarning += db.PaymentCont.Where(x => x.ReceiptId == item.id && x.CreatedDate.Year == DateTime.Now.Year && x.CreatedDate.Month == DateTime.Now.Month && x.CreatedDate.Day == DateTime.Now.Day).Sum(x => x.TotalPrice);

                }
                entity.DailyEarning = dailyEarning;
                entity.GiveDailySumKg = sumKg;


                var factoryInfo = query.Where(x => x.IsWhat == false);
                #region  //En Çok Satılan Mallar
                if (factoryInfo.Count() > 0)
                {
                    var factoryProductQuantity = from c in db.factoryQuantities.Where(x => x.CreatedDate.Year == factoryInfo.ToList()[0].ReceiptDate.Year && x.CreatedDate.Month == factoryInfo.ToList()[0].ReceiptDate.Month && x.CreatedDate.Day == factoryInfo.ToList()[0].ReceiptDate.Day)
                                                 group c by new
                                                 {
                                                     c.Name,
                                                     c.ReceiptId
                                                 } into gcs
                                                 select new FactoryQuantity
                                                 {
                                                     Name = gcs.Key.Name,
                                                     Kg = gcs.Sum(x => x.Kg),
                                                     ReceiptId = gcs.Key.ReceiptId
                                                 };




                    var prd = factoryProductQuantity.ToList();

                    prd.ToList().OrderByDescending(x => x.Kg).Take(5).ToList();
                    var listProductType = new List<ProductType>();
                    var totalSalesFactory = new List<Factory>();
                    var factories = db.factories.Where(x => x.İsDelete == false).ToList();
                    var receipts = db.Receipt.Where(x => x.İsDelete == false).ToList();
                    int sayac = -1;
                    foreach (var item in factoryProductQuantity)
                    {

                        listProductType.Add(new ProductType
                        {
                            Name = item.Name,
                            kgPrice = item.Kg
                        });
                        sayac++;
                        var factoryname = factories.FirstOrDefault(x => x.id == receipts.FirstOrDefault(x => x.id == item.ReceiptId).CustomerId);
                        if (sayac == 0)
                        {

                            totalSalesFactory.Add(new Factory
                            {
                                name = factoryname.name,
                                id = factoryname.id,
                                CreatedDate = factoryname.CreatedDate,
                                factoryProductTypes = factoryname.factoryProductTypes,
                                ModifyDate = factoryname.ModifyDate,
                                Phone = factoryname.Phone,
                                İsDelete = factoryname.İsDelete
                            });

                        }
                        else
                        {
                            var dataid = totalSalesFactory.FirstOrDefault();
                            if (dataid != null)
                            {
                                var sal = totalSalesFactory.FirstOrDefault(x => x.id == dataid.id);
                                if (sal == null)
                                {
                                    totalSalesFactory.Add(new Factory
                                    {
                                        name = factoryname.name,
                                        id = factoryname.id,
                                        CreatedDate = factoryname.CreatedDate,
                                        factoryProductTypes = factoryname.factoryProductTypes,
                                        ModifyDate = factoryname.ModifyDate,
                                        Phone = factoryname.Phone,
                                        İsDelete = factoryname.İsDelete
                                    });

                                }

                            }
                        }



                    }
                    entity.TopSellingGoods = listProductType.OrderByDescending(x => x.kgPrice).ToList();
                    entity.TopSellingCompanies = totalSalesFactory;


                }
                #endregion



                //stock
                var stockList = db.StockTracking.Where(x => x.İsDelete == false).ToList();
                var data = from d1 in stockList
                           join d2 in db.ProductTypes on d1.productid equals d2.id
                           where d2.İsDelete == false
                           select new StockResponse
                           {
                               ProductName = d2.Name,
                               TotalKg = d1.totalkg
                           };
                entity.StockResponse = data.ToList();
                return entity;



            }

        }

        public List<Receipt> GetReceiptByCustomerId(int customerid, int branchId, bool IsWhat)
        {
            using (var db = new DataContext())
            {
                var query = db.Receipt.Where(x => x.CustomerId == customerid && x.BranchId == branchId && x.İsDelete == false && x.IsWhat == IsWhat);
                return query.ToList();
            }
        }

        public List<Receipt> GetReceiptsByDateAndBranchId(DateTime date, int branchid, bool IsWhat)
        {
            using (var db = new DataContext())
            {
                var query = db.Receipt.Where(x => x.ReceiptDate.Year == date.Year && x.ReceiptDate.Month == date.Month && x.ReceiptDate.Day == date.Day && x.BranchId == branchid && x.İsDelete == false && x.IsWhat == IsWhat);
                return query.ToList();
            }
        }
        public List<Receipt> GetReceiptBeetwenDate(DateTime startDate, int branchid, bool IsWhat, DateTime endDate)
        {
            using (var db = new DataContext())
            {
                var query = db.Receipt.Where(x => x.ReceiptDate >= startDate && x.ReceiptDate <= endDate && x.BranchId == branchid && x.IsWhat == IsWhat);
                return query.ToList();
            }
        }
    }
}
