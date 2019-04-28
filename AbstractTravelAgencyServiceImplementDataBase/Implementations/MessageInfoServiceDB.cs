using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceImplementDataBase.Implementations
{
    public class MessageInfoServiceDB : IMessageInfoService
    {
        private AbstractDbScope context;

        public MessageInfoServiceDB(AbstractDbScope context)
        {
            this.context = context;
        }

        public void AddElement(InfoMessageBindingModel model)
        {
            throw new NotImplementedException();
        }

        public InfoMessageViewModel GetElement(int id)
        {
            throw new NotImplementedException();
        }

        public List<InfoMessageViewModel> GetList()
        {
            List<InfoMessageViewModel> result = context
             .Where(rec => !rec.ClientId.HasValue)
             .Select(rec => new MessageInfoViewModel
             {
                 MessageId = rec.MessageId,
                 ClientName = rec.FromMailAddress,
                 DateDelivery = rec.DateDelivery,
                 Subject = rec.Subject,
                 Body = rec.Body
             })
        }
    }
}
