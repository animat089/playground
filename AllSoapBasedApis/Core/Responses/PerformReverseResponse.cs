using System.Runtime.Serialization;

namespace Core.Responses
{
    [DataContract]
    public class PerformReverseResponse
    {
        [DataMember]
        public PerformReverseResponseBody Body { get; set; }
    }
}