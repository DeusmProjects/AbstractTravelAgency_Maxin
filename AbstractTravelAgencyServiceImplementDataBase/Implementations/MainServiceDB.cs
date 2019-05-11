using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace AbstractTravelAgencyServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private AbstractDbScope scope;
        public MainServiceDB(AbstractDbScope scope)
        {
            this.scope = scope;
        }
        public List<BookingViewModel> GetList()
        {
            List<BookingViewModel> result = scope.Bookings.Select(rec => new BookingViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                VoucherId = rec.VoucherId,
                DateCreateBooking = SqlFunctions.DateName("dd", rec.DateCreateBooking) + " " +
            SqlFunctions.DateName("mm", rec.DateCreateBooking) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreateBooking),
                DateImplementBooking = rec.DateImplementBooking == null ? "" :
            SqlFunctions.DateName("dd",
           rec.DateImplementBooking.Value) + " " +
            SqlFunctions.DateName("mm",
           rec.DateImplementBooking.Value) + " " +
            SqlFunctions.DateName("yyyy",
           rec.DateImplementBooking.Value),
                StatusBooking = rec.StatusBooking.ToString(),
                Amount = rec.Amount,
                TotalSum = rec.TotalSum,
                CustomerFIO = rec.Customer.CustomerFIO,
                VoucherName = rec.Voucher.VoucherName,
                ExecutorId = rec.ExecutorId,
                ExecutorName = rec.Executor.ExecutorFIO
            })
            .ToList();
            return result;
        }

        public void CreateBooking(BookingBindingModel model)
        {
            var booking = new Booking
            {
                CustomerId = model.CustomerId,
                VoucherId = model.VoucherId,
                DateCreateBooking = DateTime.Now,
                Amount = model.Amount,
                TotalSum = model.TotalSum,
                StatusBooking = BookingStatus.Принят,
                ExecutorId = model.ExecutorId
            };

            scope.Bookings.Add(booking);
            scope.SaveChanges();

            var customer = scope.Customers.FirstOrDefault(x => x.Id == model.CustomerId);
            SendEmail(customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} создан успешно",                 booking.Id, booking.DateCreateBooking.ToShortDateString()));
        }

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            try
            {
                objMailMessage.From = new
               MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = Encoding.UTF8;
                objMailMessage.BodyEncoding = Encoding.UTF8;
                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new
               NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
               ConfigurationManager.AppSettings["MailPassword"]);
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }

        public void TakeBookingInWork(BookingBindingModel model)
        {
            using (var transaction = scope.Database.BeginTransaction())
            {
                Booking element = scope.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
                try
                {
                    
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.StatusBooking != BookingStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var productConditions = scope.VoucherConditions.Include(rec =>
                    rec.Condition).Where(rec => rec.VoucherId == element.VoucherId).ToList();
                    // списываем
                    foreach (var productCondition in productConditions)
                    {
                        int countOnCitys = productCondition.Amount * element.Amount;
                        var stockConditions = scope.CityConditions.Where(rec =>
                        rec.ConditionId == productCondition.ConditionId).ToList();
                        foreach (var stockCondition in stockConditions)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockCondition.Amount >= countOnCitys)
                            {
                                stockCondition.Amount -= countOnCitys;
                                countOnCitys = 0;
                                scope.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnCitys -= stockCondition.Amount;
                                stockCondition.Amount = 0;
                                scope.SaveChanges();
                            }
                        }
                        if (countOnCitys > 0)
                        {
                            throw new Exception("Не достаточно условий " +
                           productCondition.Condition.ConditionName + " требуется " + productCondition.Amount + ", не хватает " + countOnCitys);
                         }
                    }
                    element.DateImplementBooking = DateTime.Now;
                    element.ExecutorId = model.ExecutorId;
                    element.StatusBooking = BookingStatus.Выполняется;
                    scope.SaveChanges();
                    SendEmail(element.Customer.Mail, "Оповещение по заказам", 
                        string.Format("Заказ №{0} от {1} передеан в работу", element.Id,
                        element.DateCreateBooking.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    element.StatusBooking = BookingStatus.НедостаточноРесурсов;
                    scope.SaveChanges();
                    transaction.Commit();
                    throw;
                }
            }
        }

        public void FinishBooking(BookingBindingModel model)
        {
            Booking element = scope.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.StatusBooking != BookingStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.StatusBooking = BookingStatus.Готов;
            scope.SaveChanges();
            SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передан на оплату",                 element.Id, element.DateCreateBooking.ToShortDateString()));
        }

        public void PayBooking(BookingBindingModel model)
        {
            Booking element = scope.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.StatusBooking != BookingStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.StatusBooking = BookingStatus.Оплачен;
            scope.SaveChanges();
            SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} оплачен успешно", 
                element.Id, element.DateCreateBooking.ToShortDateString()));
        }

        public void PutConditionOnCity(CityConditionBindingModel model)
        {
            CityCondition element = scope.CityConditions.FirstOrDefault(rec =>
           rec.CityId == model.CityId && rec.ConditionId == model.ConditionId);
            if (element != null)
            {
                element.Amount += model.Amount;
            }
            else
            {
                scope.CityConditions.Add(new CityCondition
                {
                    CityId = model.CityId,
                    ConditionId = model.ConditionId,
                    Amount = model.Amount
                });
            }
            scope.SaveChanges();
        }

        public List<BookingViewModel> GetFreeBookings()
        {
            List<BookingViewModel> result = scope.Bookings
             .Where(x => x.StatusBooking == BookingStatus.Принят || x.StatusBooking ==
            BookingStatus.НедостаточноРесурсов)
             .Select(rec => new BookingViewModel
             {
                 Id = rec.Id
             })
             .ToList();
            return result;
        }
    }
}
