namespace AssetManagement.Web.Pages
{
    public partial class Query
    {
        public List<string>? MachinesThatUsesLatestAsset { get; set; }
        public async void GetMachinesWithLatestAssetsButtonHandler()
        {
            try
            {
                MachinesThatUsesLatestAsset = await MachineService.GetMachineThatUsesLatestAsset();
            }
            catch (Exception)
            {
                MachinesThatUsesLatestAsset = new();
            }
            finally
            {
                StateHasChanged();
            }
        }
    }
}
