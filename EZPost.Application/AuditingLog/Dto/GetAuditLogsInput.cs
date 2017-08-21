using System;
using Abp.Runtime.Validation;
using Abp.Timing;
using EZPost.Common.WebControl;

namespace EZPost.AuditingLog.Dto
{
    public class GetAuditLogsInput:DataTablesParameters,IShouldNormalize
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserName { get; set; }

        public string ServiceName { get; set; }

        public string MethodName { get; set; }

        public string BrowserInfo { get; set; }

        public bool? HasException { get; set; }

        public int? MinExecutionDuration { get; set; }

        public int? MaxExecutionDuration { get; set; }

        public void Normalize()
        {
            OrderPerfix = "AuditLog.";

            if (StartDate == DateTime.MinValue)
            {
                StartDate = Clock.Now;
            }

            StartDate = StartDate.Date;

            if (EndDate == DateTime.MinValue)
            {
                EndDate = Clock.Now;
            }
        }
    }
}