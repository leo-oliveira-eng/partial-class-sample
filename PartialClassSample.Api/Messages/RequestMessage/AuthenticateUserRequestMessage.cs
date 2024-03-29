﻿using System.Runtime.Serialization;

namespace PartialClassSample.Api.Messages.RequestMessage
{
    [DataContract]
    public class AuthenticateUserRequestMessage
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
