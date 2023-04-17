using System.Runtime.Serialization;

namespace Core.Responses
{
    [DataContract]
    public class PerformCountResponseBody
    {
        [DataMember]
        public int PerformCountResult { get; set; }
    }
}