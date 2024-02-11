using Microsoft.AspNetCore.Components;

namespace AssetManagement.Web.Pages
{
    public partial class MachineList
    {
        [CascadingParameter]
        public Index Index { get; set; }
    }
}