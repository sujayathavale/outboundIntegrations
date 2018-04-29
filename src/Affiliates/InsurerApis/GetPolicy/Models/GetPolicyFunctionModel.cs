using System;

namespace InsuranceApis.GetPolicy.Models
{
    public class GetPolicyFunctionModel
    {
        public Uri ResourceUri { get; set; }

        public long PolicyId { get; set; }
    }
}
