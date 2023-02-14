using Fire.Entity.Abstrack;

namespace Fire.Entity.Concrete
{
    public class ExpenseDetail: IBaseEntity
    {
        public int id { get; set; }
        public int ExpenseCategoriaId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int BranchId { get; set; }
    }
}
