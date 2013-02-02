// keyword_enum.cs
// enum initialization:
using System;

namespace DataMart_eCPM_WebInterface
{
    public class JobStatus
    {
        public enum Status { NeverExecuted = 1, CurrentlyExecuting, Executed };
        private Status currentStatus = Status.NeverExecuted;
        private String datetime = "";

        public Status CurrentStatus
        {
            get { return currentStatus; }
            set { currentStatus = value; }
        }

        public String Datetime
        {
            get { return datetime; }
            set { datetime = value; }
        }
    }
}