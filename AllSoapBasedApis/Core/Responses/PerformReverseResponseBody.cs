using System.Runtime.Serialization;

namespace Core.Responses
{
    [DataContract]
    public class PerformReverseResponseBody
    {
        [DataMember]
        public string PerformReverseResult { get; set; }
    }
}