namespace Fire.UI.Models
{
    public class FactoryAndCustomerViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        /// <summary>
        /// type true ise müşteri false ise fabrika
        /// </summary>
        public bool Type { get; set; }
    }
}
