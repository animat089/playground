﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SampleAsmxService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SampleAsmxService.SampleAsmxServiceSoap")]
    internal interface SampleAsmxServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/PerformCount", ReplyAction="*")]
        System.Threading.Tasks.Task<SampleAsmxService.PerformCountResponse> PerformCountAsync(SampleAsmxService.PerformCountRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/PerformReverse", ReplyAction="*")]
        System.Threading.Tasks.Task<SampleAsmxService.PerformReverseResponse> PerformReverseAsync(SampleAsmxService.PerformReverseRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class PerformCountRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="PerformCount", Namespace="http://tempuri.org/", Order=0)]
        public SampleAsmxService.PerformCountRequestBody Body;
        
        public PerformCountRequest()
        {
        }
        
        public PerformCountRequest(SampleAsmxService.PerformCountRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    internal partial class PerformCountRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string input;
        
        public PerformCountRequestBody()
        {
        }
        
        public PerformCountRequestBody(string input)
        {
            this.input = input;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class PerformCountResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="PerformCountResponse", Namespace="http://tempuri.org/", Order=0)]
        public SampleAsmxService.PerformCountResponseBody Body;
        
        public PerformCountResponse()
        {
        }
        
        public PerformCountResponse(SampleAsmxService.PerformCountResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    internal partial class PerformCountResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int PerformCountResult;
        
        public PerformCountResponseBody()
        {
        }
        
        public PerformCountResponseBody(int PerformCountResult)
        {
            this.PerformCountResult = PerformCountResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class PerformReverseRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="PerformReverse", Namespace="http://tempuri.org/", Order=0)]
        public SampleAsmxService.PerformReverseRequestBody Body;
        
        public PerformReverseRequest()
        {
        }
        
        public PerformReverseRequest(SampleAsmxService.PerformReverseRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    internal partial class PerformReverseRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string input;
        
        public PerformReverseRequestBody()
        {
        }
        
        public PerformReverseRequestBody(string input)
        {
            this.input = input;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    internal partial class PerformReverseResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="PerformReverseResponse", Namespace="http://tempuri.org/", Order=0)]
        public SampleAsmxService.PerformReverseResponseBody Body;
        
        public PerformReverseResponse()
        {
        }
        
        public PerformReverseResponse(SampleAsmxService.PerformReverseResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    internal partial class PerformReverseResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string PerformReverseResult;
        
        public PerformReverseResponseBody()
        {
        }
        
        public PerformReverseResponseBody(string PerformReverseResult)
        {
            this.PerformReverseResult = PerformReverseResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    internal interface SampleAsmxServiceSoapChannel : SampleAsmxService.SampleAsmxServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    internal partial class SampleAsmxServiceSoapClient : System.ServiceModel.ClientBase<SampleAsmxService.SampleAsmxServiceSoap>, SampleAsmxService.SampleAsmxServiceSoap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public SampleAsmxServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(SampleAsmxServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), SampleAsmxServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SampleAsmxServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(SampleAsmxServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SampleAsmxServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(SampleAsmxServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SampleAsmxServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SampleAsmxService.PerformCountResponse> SampleAsmxService.SampleAsmxServiceSoap.PerformCountAsync(SampleAsmxService.PerformCountRequest request)
        {
            return base.Channel.PerformCountAsync(request);
        }
        
        public System.Threading.Tasks.Task<SampleAsmxService.PerformCountResponse> PerformCountAsync(string input)
        {
            SampleAsmxService.PerformCountRequest inValue = new SampleAsmxService.PerformCountRequest();
            inValue.Body = new SampleAsmxService.PerformCountRequestBody();
            inValue.Body.input = input;
            return ((SampleAsmxService.SampleAsmxServiceSoap)(this)).PerformCountAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SampleAsmxService.PerformReverseResponse> SampleAsmxService.SampleAsmxServiceSoap.PerformReverseAsync(SampleAsmxService.PerformReverseRequest request)
        {
            return base.Channel.PerformReverseAsync(request);
        }
        
        public System.Threading.Tasks.Task<SampleAsmxService.PerformReverseResponse> PerformReverseAsync(string input)
        {
            SampleAsmxService.PerformReverseRequest inValue = new SampleAsmxService.PerformReverseRequest();
            inValue.Body = new SampleAsmxService.PerformReverseRequestBody();
            inValue.Body.input = input;
            return ((SampleAsmxService.SampleAsmxServiceSoap)(this)).PerformReverseAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.SampleAsmxServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.SampleAsmxServiceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
                httpsBindingElement.AllowCookies = true;
                httpsBindingElement.MaxBufferSize = int.MaxValue;
                httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpsBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.SampleAsmxServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("https://localhost:44362/SampleAsmxService.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.SampleAsmxServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("https://localhost:44362/SampleAsmxService.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            SampleAsmxServiceSoap,
            
            SampleAsmxServiceSoap12,
        }
    }
}
