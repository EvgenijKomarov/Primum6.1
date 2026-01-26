using CoreConnection.Enums;

namespace PrimumCore.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class AvailableIncendent : Attribute
    {
        public IncendentMeaningDto Meaning { get; }
        public IncendentDecisionDto Decision { get; }

        public AvailableIncendent(IncendentMeaningDto meaning, IncendentDecisionDto decision)
        {
            Meaning = meaning;
            Decision = decision;
        }
    }
}
