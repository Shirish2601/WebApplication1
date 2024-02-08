using Microsoft.AspNetCore.Components;

namespace AssetManagement.Web.Pages
{
    public partial class DisplayAsset : ComponentBase
    {
        
        [CascadingParameter]
        public IndexBase IndexBase { get; set; }

        [Parameter]
        public int CurrentIndex { get; set; }

    }
}
