using Core;
using System.Web.Services;

namespace AsmxNWcfServices
{
    /// <summary>
    /// Summary description for SampleAsmxService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class SampleAsmxService : WebService, ISampleService
    {
        [WebMethod]
        public int PerformCount(string input)
        {
            return input.CountString();
        }

        [WebMethod]
        public string PerformReverse(string input)
        {
            return input.ReverseString();
        }
    }
}