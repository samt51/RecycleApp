using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.DataAccess.Models.DashboardReportModel
{
    public class DashboardViewModel
    {
        /// <summary>
        /// Günlük Giren Toplam Kg
        /// </summary>
        public decimal GetDailySumKg { get; set; }
        /// <summary>
        /// Günlük Çıkan Toplam Kg
        /// </summary>
        public decimal GiveDailySumKg { get; set; }
        /// <summary>
        /// Günlük Kazanç
        /// </summary>
        public decimal DailyEarning { get; set; }
        /// <summary>
        /// Günlük Sermaye Toplam Ücret
        /// </summary>
        public decimal GiveDailyPrice { get; set; }
        /// <summary>
        /// Günlük En Fazla Stok Arttıran Ürün ismi
        /// </summary>
        /// 
        public string StockBoosterProductName { get; set; }
        /// <summary>
        /// Günlük En Fazla Stok Arttıran Ürün kg
        /// </summary>
        /// 
        public decimal StockBoosterProduct { get; set; }
        /// <summary>
        /// Günlük En Fazla Stoktan Eksilen Ürün İsmi
        /// </summary>
        public string StockMostProduckName { get; set; }
        /// <summary>
        /// Günlük En Fazla Stoktan Eksilen Ürün
        /// </summary>
        public decimal StockMostProduck { get; set; }
        /// <summary>
        /// Günlük En Fazla Alım Yapılan Müş. Name
        /// </summary>
        public string GetDailyProductByCustomerName { get; set; }
        /// <summary>
        /// Günlük En Fazla Alım Yapılan Müş.  Kg
        /// </summary>
        public decimal GetDailyProductByCustomerKg { get; set; }
        /// <summary>
        /// En Çok Satılan Mallar
        /// </summary>
        public List<ProductTypeViewModel> TopSellingGoods { get; set; } = new List<ProductTypeViewModel>();
        /// <summary>
        /// En Çok Satış Yapılan Firmalar
        /// </summary>
        public List<Factory> TopSellingCompanies { get; set; } = new List<Factory>();
        /// <summary>
        /// En Çok Alım Yapılan Müşteriler
        /// </summary>
        public List<Customer> TopPurchasedCustomers { get; set; } = new List<Customer>();
        /// <summary>
        /// Stokta Bulunan Mallar Kg
        /// </summary>
        public List<StockResponse> StockResponse { get; set; } = new List<StockResponse>();
    }
    public class StockResponse
    {
        public string ProductName { get; set; }
        public decimal TotalKg { get; set; }
    }
}
