using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaireTaxi.Database.Entities
{
    public class Notification
    {
        public long ID
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public long Recipient
        {
            get;
            set;
        }

        public long Sender
        {
            get;
            set;
        }

        public long ReservationID
        {
            get;
            set;
        }
    }
}
