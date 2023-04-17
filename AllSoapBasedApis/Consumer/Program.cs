using System.ServiceModel;

var httpsBinding = new BasicHttpBinding()
{
    Security = new BasicHttpSecurity()
    {
        Mode = BasicHttpSecurityMode.Transport, //Required for calls over Https
    }
};
const string strInput = "hannah";

#region Asmx

Console.WriteLine("\nTesting Asmx...");

// Using the service registeration method
var testAsmxEndpoint = "https://localhost:44362/SampleAsmxService.asmx?wsdl";
var testAsmxEndpointAddress = new EndpointAddress(testAsmxEndpoint);
using (var testWcfClient = new SampleAsmxService.SampleAsmxServiceSoapClient(httpsBinding, testAsmxEndpointAddress))
{
    // Point to note, if the all contracts return Task<T> instead of T
    var intResult = testWcfClient.PerformCountAsync(strInput).Result;
    Console.WriteLine("With Client - Count - {0}", intResult.Body.PerformCountResult);

    var strResult = testWcfClient.PerformReverseAsync(strInput).Result;
    Console.WriteLine("With Client - Reverse - {0}", strResult.Body.PerformReverseResult);

}

/// Note - This needs more work, does not work, directly off of as it works for others
//// Without using service registration
//using (var factory = new ChannelFactory<Core.ISampleService>(httpsBinding, testAsmxEndpointAddress))
//{
//    // Point to note all contracts can be used with T and not Task<T> in contrast with the above
//    var channel = factory.CreateChannel();

//    // Point to note, if the all contracts return Task<T> instead of T
//    var intResult = channel.PerformCount(strInput);
//    Console.WriteLine("Without Client - Count - {0}", intResult);

//    var strResult = channel.PerformReverse(strInput);
//    Console.WriteLine("Without Client - Reverse - {0}", strResult);
//}

#endregion

#region Wcf

Console.WriteLine("\nTesting Wcf...");

// Using the service registeration method
var testWcfEndpoint = "https://localhost:44362/SampleWcfService.svc?wsdl";
var testWcfEndpointAddress = new EndpointAddress(testWcfEndpoint);
using (var testWcfClient = new SampleWcfService.SampleServiceExtendedClient(httpsBinding, testWcfEndpointAddress))
{
    // Point to note, if the all contracts return Task<T> instead of T
    var intResult = testWcfClient.PerformCountAsync(strInput).Result;
    Console.WriteLine("With Client - Count - {0}", intResult);

    var asmxIntResult = testWcfClient.PerformCountWithAsmxResponseFormatAsync(strInput).Result;
    Console.WriteLine("With Client - AsmxFormatCount - {0}", asmxIntResult.Body.PerformCountResult);

    var strResult = testWcfClient.PerformReverseAsync(strInput).Result;
    Console.WriteLine("With Client - Reverse - {0}", strResult);

    var asmxStrResult = testWcfClient.PerformReverseWithAsmxResponseFormatAsync(strInput).Result;
    Console.WriteLine("With Client - AsmxFormatReverse - {0}", asmxIntResult.Body.PerformCountResult);

}

// Without using service registration
using (var factory = new ChannelFactory<Core.ISampleServiceExtended>(httpsBinding, testWcfEndpointAddress))
{
    // Point to note all contracts can be used with T and not Task<T> in contrast with the above
    var channel = factory.CreateChannel();

    // Point to note, if the all contracts return Task<T> instead of T
    var intResult = channel.PerformCount(strInput);
    Console.WriteLine("Without Client - Count - {0}", intResult);

    var asmxIntResult = channel.PerformCountWithAsmxResponseFormat(strInput);
    Console.WriteLine("Without Client - AsmxFormatCount - {0}", asmxIntResult.Body.PerformCountResult);

    var strResult = channel.PerformReverse(strInput);
    Console.WriteLine("Without Client - Reverse - {0}", strResult);

    var asmxStrResult = channel.PerformReverseWithAsmxResponseFormat(strInput);
    Console.WriteLine("Without Client - AsmxFormatReverse - {0}", asmxIntResult.Body.PerformCountResult);
}

#endregion

#region SoapCore

Console.WriteLine("\nTesting SoapCore...");

// Using the service registeration method
var testSoapCoreEndpoint = "https://localhost:7107/SampleSoapCoreService?wsdl";
var testSoapCoreEndpointAddress = new EndpointAddress(testSoapCoreEndpoint);
using (var testSoapCoreClient = new SampleSoapCoreService.SampleServiceExtendedClient(httpsBinding, testSoapCoreEndpointAddress))
{
    // Point to note, if the all contracts return Task<T> instead of T
    var intResult = testSoapCoreClient.PerformCountAsync(strInput).Result;
    Console.WriteLine("With Client - Count - {0}", intResult);

    var asmxIntResult = testSoapCoreClient.PerformCountWithAsmxResponseFormatAsync(strInput).Result;
    Console.WriteLine("With Client - AsmxFormatCount - {0}", asmxIntResult.Body.PerformCountResult);

    var strResult = testSoapCoreClient.PerformReverseAsync(strInput).Result;
    Console.WriteLine("With Client - Reverse - {0}", strResult);

    var asmxStrResult = testSoapCoreClient.PerformReverseWithAsmxResponseFormatAsync(strInput).Result;
    Console.WriteLine("With Client - AsmxFormatReverse - {0}", asmxStrResult.Body.PerformReverseResult);

}

// Without using service registration
using (var factory = new ChannelFactory<Core.ISampleServiceExtended>(httpsBinding, testSoapCoreEndpointAddress))
{
    // Point to note all contracts can be used with T and not Task<T> in contrast with the above
    var channel = factory.CreateChannel();

    // Point to note, if the all contracts return Task<T> instead of T
    var intResult = channel.PerformCount(strInput);
    Console.WriteLine("Without Client - Count - {0}", intResult);

    var asmxIntResult = channel.PerformCountWithAsmxResponseFormat(strInput);
    Console.WriteLine("Without Client - AsmxFormatCount - {0}", asmxIntResult.Body.PerformCountResult);

    var strResult = channel.PerformReverse(strInput);
    Console.WriteLine("Without Client - Reverse - {0}", strResult);

    var asmxStrResult = channel.PerformReverseWithAsmxResponseFormat(strInput);
    Console.WriteLine("Without Client - AsmxFormatReverse - {0}", asmxStrResult.Body.PerformReverseResult);
}

#endregion

#region CoreWcf

Console.WriteLine("\nTesting CoreWcf...");

// Using the service registeration method
var testCoreWcfEndpoint = "https://localhost:7107/SampleCoreWcfService?wsdl";
var testCoreWcfEndpointAddress = new EndpointAddress(testCoreWcfEndpoint);
using (var testCoreWcfClient = new SampleCoreWcfService.SampleServiceExtendedClient(httpsBinding, testCoreWcfEndpointAddress))
{
    // Point to note, if the all contracts return Task<T> instead of T
    var intResult = testCoreWcfClient.PerformCountAsync(strInput).Result;
    Console.WriteLine("With Client - Count - {0}", intResult);

    var asmxIntResult = testCoreWcfClient.PerformCountWithAsmxResponseFormatAsync(strInput).Result;
    Console.WriteLine("With Client - AsmxFormatCount - {0}", asmxIntResult.Body.PerformCountResult);

    var strResult = testCoreWcfClient.PerformReverseAsync(strInput).Result;
    Console.WriteLine("With Client - Reverse - {0}", strResult);

    var asmxStrResult = testCoreWcfClient.PerformReverseWithAsmxResponseFormatAsync(strInput).Result;
    Console.WriteLine("With Client - AsmxFormatReverse - {0}", asmxStrResult.Body.PerformReverseResult);

}

// Without using service registration
using (var factory = new ChannelFactory<Core.ISampleServiceExtended>(httpsBinding, testCoreWcfEndpointAddress))
{
    // Point to note all contracts can be used with T and not Task<T> in contrast with the above
    var channel = factory.CreateChannel();

    // Point to note, if the all contracts return Task<T> instead of T
    var intResult = channel.PerformCount(strInput);
    Console.WriteLine("Without Client - Count - {0}", intResult);

    var asmxIntResult = channel.PerformCountWithAsmxResponseFormat(strInput);
    Console.WriteLine("Without Client - AsmxFormatCount - {0}", asmxIntResult.Body.PerformCountResult);

    var strResult = channel.PerformReverse(strInput);
    Console.WriteLine("Without Client - Reverse - {0}", strResult);

    var asmxStrResult = channel.PerformReverseWithAsmxResponseFormat(strInput);
    Console.WriteLine("Without Client - AsmxFormatReverse - {0}", asmxStrResult.Body.PerformReverseResult);
}

#endregion

Console.Write("\nCompleted. Hit enter to exit...");
Console.ReadLine();