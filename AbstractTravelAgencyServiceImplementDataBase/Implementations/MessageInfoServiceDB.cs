using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

namespace AbstractTravelAgencyServiceImplementDataBase.Implementations
{
    public class MessageInfoServiceDB : IMessageInfoService
    {
        private AbstractDbScope context;

        public MessageInfoServiceDB(AbstractDbScope context)
        {
            this.context = context;
        }

        public List<InfoMessageViewModel> GetList()
        {
            List<InfoMessageViewModel> result = context.InfoMessages
             .Where(rec => !rec.CustomerId.HasValue)
             .Select(rec => new InfoMessageViewModel
             {
                 MessageId = rec.MessageId,
                 CustomerName = rec.FromMailAddress,
                 DateDelivery = rec.DateDelivery,
                 Subject = rec.Subject,
                 Body = rec.Body
             }).ToList();
            return result;
        }

        public InfoMessageViewModel GetElement(int id)
        {
            InfoMessage element = context.InfoMessages.Include(rec => rec.Customer)
            .FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new InfoMessageViewModel
                {
                    MessageId = element.MessageId,
                    CustomerName = element.Customer.CustomerFIO,
                    DateDelivery = element.DateDelivery,
                    Subject = element.Subject,
                    Body = element.Body
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(InfoMessageBindingModel model)
        {
            InfoMessage element = context.InfoMessages.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element != null)
            {
                return;
            }
            var message = new InfoMessage
            {
                MessageId = model.MessageId,
                FromMailAddress = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            };
            var mailAddress = Regex.Match(model.FromMailAddress,
           @"(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9az])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))");
            if (mailAddress.Success)
            {
                var client = context.Customers.FirstOrDefault(rec => rec.Mail ==
                mailAddress.Value);
                if (client != null)
                {
                    message.CustomerId = client.Id;
                }
            }
            context.InfoMessages.Add(message);
            context.SaveChanges();
        }
    }
}
