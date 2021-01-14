using System;

namespace SimpleManager.Models
{
    class Order
    {

        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public string OrderDescription { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public decimal OrderPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeadLine { get; set; }
        /// <summary>
        /// Возвращает или задает статус заказа
        /// </summary>
        public Status Status { get; set; }

        public override string ToString()
        {
            return $"{OrderName}";
        }
    }
}
