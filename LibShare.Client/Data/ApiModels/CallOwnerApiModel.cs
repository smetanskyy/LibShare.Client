using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Client.Data.ApiModels
{
    public class CallOwnerApiModel
    {
        public string BookId { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}
