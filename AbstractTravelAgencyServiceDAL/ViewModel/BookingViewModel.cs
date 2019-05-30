using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceDAL.ViewModel
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }

        public int CustomerId { get; set; }

        [DisplayName("ФИО клиента")]
        public string CustomerFIO { get; set; }

        public int VoucherId { get; set; }

        [DisplayName("Путевка")]
        public string VoucherName { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }

        [DisplayName("Сумма")]
        public decimal TotalSum { get; set; }

        [DisplayName("Статус")]
        public string StatusBooking { get; set; }

        [DisplayName("Дата создания")]
        public string DateCreateBooking { get; set; }

        [DisplayName("Дата выполнения")]
        public string DateImplementBooking { get; set; }
    }
}
