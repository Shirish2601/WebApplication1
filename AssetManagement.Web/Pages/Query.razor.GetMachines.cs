using AssetManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace AssetManagement.Web.Pages
{
    public partial class Query
    {
        [Inject]
        public IMachineService MachineService { get; set; }
        public string? AssetName { get; set; }
        public string? SeriesNumber { get; set; }
        public string? DisplayAssetName { get; set; }
        public List<string>? MachineNames { get; set; }
        protected async void GetMachinesByAssetButtonHandler()
        {
            try
            {
                AssetName = AssetName?.Trim();
                SeriesNumber = SeriesNumber?.Trim();
                MachineNames = (await MachineService.GetMachinesByAssetAndSeries(AssetName, SeriesNumber)).ToList();
                DisplayAssetName = AssetName;
            }
            catch (Exception)
            {
                MachineNames = new();
            }
            finally
            {
                StateHasChanged();
            }
        }
    }
}
