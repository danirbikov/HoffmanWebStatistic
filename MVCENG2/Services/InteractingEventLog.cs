using HoffmanWebstatistic.Repository;

namespace HoffmanWebstatistic.Services
{
    public class InteractingEventLog
    {
        private readonly SendingStatusLogRepository _sendingStatusLogRepository;

        public InteractingEventLog(SendingStatusLogRepository sendingStatusLogRepository)
        {
            _sendingStatusLogRepository = sendingStatusLogRepository;
        }
        public void AddErrorStatusLog(SendingStatusLog sendingStatusLog)
        {

        }

    }
}
