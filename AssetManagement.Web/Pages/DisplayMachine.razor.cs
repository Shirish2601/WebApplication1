using Microsoft.AspNetCore.Components;

namespace AssetManagement.Web.Pages
{
    public partial class DisplayMachine
    {
        [CascadingParameter]
        public Index Index { get; set; }
    }
}
