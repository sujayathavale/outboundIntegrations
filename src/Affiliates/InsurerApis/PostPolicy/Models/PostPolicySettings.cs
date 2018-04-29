namespace InsuranceApis.PostPolicy.Models
{
    public class PostPolicySettings
    {
        public EventGridSetting PolicyCreated { get; set; }

        public EventGridSetting PolicyRefunded { get; set; }
    }

    public class EventGridSetting
    {
        public string Uri { get; set; }

        public string SaSKey { get; set; }
    }
}
