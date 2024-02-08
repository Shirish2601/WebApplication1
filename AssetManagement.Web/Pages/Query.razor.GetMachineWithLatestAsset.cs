namespace AssetManagement.Web.Pages
{
    public partial class Query
    {
        public List<string>? MachineThatUsesLatestAsset { get; set; }
        public async void GetMachinesWithLatestAssetsButtonHandler()
        {
            try
            {
                MachineThatUsesLatestAsset = await MachineService.GetMachineThatUsesLatestAsset();
            }
            catch (Exception)
            {
                MachineThatUsesLatestAsset = null;
            }
            finally
            {
                StateHasChanged();
            }
        }
    }
}
