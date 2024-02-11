using Microsoft.AspNetCore.Components;

namespace AssetManagement.Web.Pages
{
    public partial class DisplayAsset
    {
        [CascadingParameter]
        public Index Index { get; set; }

        [Parameter]
        public int CurrentIndex { get; set; }

    }
}
