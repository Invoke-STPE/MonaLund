using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MonaLund.Web.Authentication;
using MonaLund.Web.Models;
using MonaLund.Web.Models.Contexts;

namespace MonaLund.Web.Shared
{
    public partial class Navbar
    {
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        [Inject]
        public AuthenticationStateProvider _authenticationStateProvider { get; set; }
        [Inject]
        public MonaContext _context { get; set; }
        public List<CategoryModel> Categories { get; set; }

        private bool _isHomePage = false;
        private string _homePageCss = "";
        private string _calculateSize = "";

        protected override void OnInitialized()
        {
            _isHomePage = _navigationManager.Uri == "https://localhost:7190/";
            Categories = _context.Categories.ToList();
            if (CheckForHomePageUrl())
            {
                _homePageCss = "collapse show navbar navbar-vertical navbar-light align-items-start p-0 border border-top-0 border-bottom-0";
            }
            else
            {
                _homePageCss = "collapse position-absolute navbar navbar-vertical navbar-light align-items-start p-0 border border-top-0 border-bottom-0 bg-light";
                _calculateSize = "width: calc(100% - 30px); z-index: 1;";
            }
        }
        private async Task Logout()
        {
            var customAuthStateProvider = (CustomAuthenticationStateProvider)_authenticationStateProvider;
            await customAuthStateProvider.UpdateAuthenticationState(null);
            _navigationManager.NavigateTo("/", true);

        }
        private bool CheckForHomePageUrl()
        {
            return _navigationManager.Uri == "https://localhost:7190/";
        }

        private void NavigateToURI(string uri)
        {
            _navigationManager.NavigateTo(uri, forceLoad: true);
        }
    }
}
