using CoreConnection.Enums;

namespace CoreDBModel.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class AvailableIncident : Attribute
    {
        public IncidentMeaningDto Meaning { get; }
        public IncidentDecisionDto Decision { get; }

        public AvailableIncident(IncidentMeaningDto meaning, IncidentDecisionDto decision)
        {
            Meaning = meaning;
            Decision = decision;
        }
    }
}
