using System;

namespace InsurerApis.GetPolicy.Models
{
    public class GetPolicyFunctionModel
    {
        public Uri ResourceUri { get; set; }

        public long PolicyId { get; set; }
    }
}
