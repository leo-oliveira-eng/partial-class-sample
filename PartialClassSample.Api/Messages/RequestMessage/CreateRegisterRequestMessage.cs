using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PartialClassSample.Api.Messages.RequestMessage
{
    [DataContract]
    public class CreateRegisterRequestMessage
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string PasswordConfirmation { get; set; }
    }
}
