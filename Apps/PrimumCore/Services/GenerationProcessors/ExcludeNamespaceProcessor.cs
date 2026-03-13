using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace PrimumCore.Services.GenerationProcessors
{
    public class ExcludeNamespaceProcessor() : IDocumentProcessor
    {
        public void Process(DocumentProcessorContext context)
        {
            var keysToRemove = context.Document.Definitions
                .Where(kvp => kvp.Key.StartsWith("PageResultOf") == false)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in keysToRemove)
            {
                context.Document.Definitions.Remove(key);
            }
        }
    }
}
