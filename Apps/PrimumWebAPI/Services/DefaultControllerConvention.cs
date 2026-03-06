using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PrimumWebAPI.Controllers;

namespace PrimumWebAPI.Services
{
    public class DefaultControllerConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                if (controller.ControllerType.BaseType == typeof(DefaultController))
                {
                    foreach (var selector in controller.Selectors)
                    {
                        if (selector.AttributeRouteModel != null)
                        {
                            selector.AttributeRouteModel.Template =
                                AttributeRouteModel.CombineTemplates("api", selector.AttributeRouteModel.Template);
                        }
                    }
                }
            }
        }
    }
}
