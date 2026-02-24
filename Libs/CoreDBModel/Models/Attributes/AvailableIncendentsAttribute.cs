using CoreDBModel.Models.Enums;

namespace CoreDBModel.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class AvailableIncident : Attribute
    {
        public IncidentMeaning Meaning { get; }
        public IncidentDecision Decision { get; }

        public AvailableIncident(IncidentMeaning meaning, IncidentDecision decision)
        {
            Meaning = meaning;
            Decision = decision;
        }
    }
}
