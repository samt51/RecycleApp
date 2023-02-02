using Fire.Entity.Abstrack;
using System.ComponentModel.DataAnnotations;

namespace Fire.Entity.Concrete
{
    public class Expenses:IBaseEntity
    {
        [Key]
        public int id { get; set; }
        /// <summary>
        /// verilen veya alınan para miktaru
        /// </summary>
        public decimal Debt { get; set; }
        /// <summary>
        /// borç alan veya borç alınan
        /// </summary>
        public  int CustomerOrFactory { get; set; }
        public string Description { get; set; }
        public int branchid { get; set; }
        public int CompanyId { get; set; }
        /// <summary>
        /// true ise alacak false ise verecek şirketin
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        /// eğer sistemde kayıtlı değilse eklemek için
        /// </summary>
        public string NameAndSurname { get; set; }
    }
}
