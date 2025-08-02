using ELECON.Domain.Interface.IViewRenderRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using ActionDescriptor = Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor;

namespace Elecon.Infrastructure.Repositories.ViewRenderRepository
{
    public class ViewRenderService : IViewRenderRepository
    {
        #region Ctor

        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public ViewRenderService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        #endregion

        public string RenderToStringAsync(string viewName, object model)
        {
            DefaultHttpContext httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };

            ActionContext actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (StringWriter sw = new StringWriter())
            {
                string viewPath = $"~/Views/Shared/{viewName}.cshtml";

                ViewEngineResult viewResult = _razorViewEngine.GetView(viewPath, viewPath, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                viewResult.View.RenderAsync(viewContext);

                return sw.ToString();
            }
        }
    }
    
}
