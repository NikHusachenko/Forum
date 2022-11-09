using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Views.Shared.Components.HeaderComponent
{
    public class HeaderComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
