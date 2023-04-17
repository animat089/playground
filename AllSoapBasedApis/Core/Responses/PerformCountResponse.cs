using System.Runtime.Serialization;

namespace Core.Responses
{
    [DataContract]
    public class PerformCountResponse
    {
        [DataMember]
        public PerformCountResponseBody Body { get; set; }
    }
}