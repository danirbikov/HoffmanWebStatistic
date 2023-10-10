using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Models.General;

namespace HoffmanWebstatistic.Repository
{
    public class SendingStatusLogRepository
    {
        private readonly ApplicationDbContext _context;
        public SendingStatusLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public bool AddOrUpdate(SendingStatusLog sendingStatusLog)
        {
            SendingStatusLog sendingStatusLogObject = _context.sending_status_log.Where(k=>k.FileName==sendingStatusLog.FileName && k.Stand.StandName==sendingStatusLog.Stand.StandName).FirstOrDefault();


            if (sendingStatusLogObject!=null)
            {
                sendingStatusLogObject.FileName = sendingStatusLog.FileName;
                sendingStatusLogObject.FileSize = sendingStatusLog.FileSize;
                sendingStatusLogObject.SourceFilePath = sendingStatusLog.SourceFilePath;
                sendingStatusLogObject.TargetFilePath = sendingStatusLog.TargetFilePath;
                sendingStatusLogObject.UserId = sendingStatusLog.UserId;
                sendingStatusLogObject.Status = sendingStatusLog.Status;
                sendingStatusLogObject.ErrorMessage = sendingStatusLog.ErrorMessage;
                sendingStatusLogObject.StandId = sendingStatusLog.StandId;
                sendingStatusLogObject.Date = sendingStatusLog.Date;

                return Save();
            }
            else
            {
                _context.Add(sendingStatusLog);
                return Save();
            }                    

        }

        public IEnumerable<SendingStatusLog> GetAll()
        {
            return _context.sending_status_log.ToList();

        }

        public IEnumerable<SendingStatusLog> GetAllWithInclude()
        {
            return _context.sending_status_log.Include(k=>k.User).Include(k=>k.Stand).ToList();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

      
    }
}
