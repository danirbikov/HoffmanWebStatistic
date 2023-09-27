using HoffmanWebstatistic.Models.General;
using System;
using System.Collections.Generic;

namespace HoffmanWebstatistic
{
    public partial class SendingStatusLog
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public int? FileSize { get; set; }
        public string? SourceFilePath { get; set; }
        public string? TargetFilePath { get; set; }
        public int? UserId { get; set; }
        public string? Status { get; set; }
        public string? ErrorMessage { get; set; }
        public int? StandId { get; set; }
        public DateTime Date { get; set; }

        public virtual Stand? Stand { get; set; }
        public virtual User? User { get; set; }
    }
}
