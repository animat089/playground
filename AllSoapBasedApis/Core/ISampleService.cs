using System.ServiceModel;

namespace Core
{
    [ServiceContract]
    public interface ISampleService
    {
        [OperationContract]
        int PerformCount(string input);

        [OperationContract]
        string PerformReverse(string input);
    }
}