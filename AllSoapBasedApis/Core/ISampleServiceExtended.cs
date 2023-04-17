using Core.Responses;
using System.ServiceModel;

namespace Core
{
    [ServiceContract]
    public interface ISampleServiceExtended : ISampleService
    {
        [OperationContract]
        PerformCountResponse PerformCountWithAsmxResponseFormat(string input);

        [OperationContract]
        PerformReverseResponse PerformReverseWithAsmxResponseFormat(string input);
    }
}