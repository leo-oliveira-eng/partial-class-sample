using System.Runtime.Serialization;

namespace PartialClassSample.Api.Messages.ResponseMessage
{
    [DataContract]
    public class RegisterResponseMessage
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}
