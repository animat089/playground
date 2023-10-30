using Core.Responses;

namespace Core
{
    public class SampleService : ISampleServiceExtended
    {
        public int PerformCount(string input)
        {
            return input.CountString();
        }

        public PerformCountResponse PerformCountWithAsmxResponseFormat(string input)
        {
            return new PerformCountResponse
            {
                Body = new PerformCountResponseBody
                {
                    PerformCountResult = input.CountString()
                }
            };
        }

        public string PerformReverse(string input)
        {
            return input.ReverseString();
        }

        public PerformReverseResponse PerformReverseWithAsmxResponseFormat(string input)
        {
            return new PerformReverseResponse
            {
                Body = new PerformReverseResponseBody
                {
                    PerformReverseResult = input.ReverseString()
                }
            };
        }
    }
}