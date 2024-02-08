using AssetManagement.Models;
using Microsoft.AspNetCore.Components;

namespace AssetManagement.Web.Pages
{
    public partial class DisplayMachine
    {
        [CascadingParameter]
        public Index Index { get; set; }
        public string SearchQuery { get; set; }
        
        public bool ToggleSubmitButton => string.IsNullOrEmpty(SearchQuery);
    }
}
