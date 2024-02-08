using Microsoft.AspNetCore.Components;

namespace AssetManagement.Web.Pages
{
    public partial class MachineList : ComponentBase
    {
        [CascadingParameter]
        public IndexBase IndexBase { get; set; }
    }
}
