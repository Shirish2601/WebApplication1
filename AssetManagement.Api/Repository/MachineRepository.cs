﻿using AssetManagement.Api.Models;
using AssetManagement.Models;

namespace AssetManagement.Api.Repository
{
    public class MachineRepository : IMachineRepository
    {
        private readonly IFileReader _fileReader;

        public MachineRepository(IFileReader fileReader)
        {
            _fileReader = fileReader;
            _fileReader.Read();
        }

        public List<Asset> GetAssetsByMachineName(string? machineName)
        {
            return AppConstants.Machines.Where(machine => machine.MachineName?.ToLower() == machineName?.ToLower())?.FirstOrDefault()?.Assets;
        }

        public List<Machine> GetMachines()
        {
            return AppConstants.Machines;
        }

        public List<string> GetMachineThatUsesLatestAssets()
        {
            Dictionary<string, int> assetDictionary = new();
            AppConstants.Machines?.ForEach(m => { 
                m.Assets.ForEach(a =>
                {
                    if (a.AssetName != null && !assetDictionary.ContainsKey(a.AssetName))
                    {
                        assetDictionary.Add(a.AssetName, Convert.ToInt32(a.SeriesNumber?.Substring(1)));
                    }
                    else
                    {
                        int currentSeriesNumber = Convert.ToInt32(a.SeriesNumber?.Substring(1));
                        int seriesNumberInDictionary = assetDictionary[a.AssetName];

                        assetDictionary[a.AssetName] = Math.Max(currentSeriesNumber, seriesNumberInDictionary);
                    }
                });
            });
            var machinesThatUsesLatestAssets = AppConstants.Machines?.FindAll(m => m.Assets.All(a => assetDictionary.ContainsKey(a.AssetName) && assetDictionary[a.AssetName] == int.Parse(a.SeriesNumber.Substring(1))))
                .Select(m => m.MachineName)
                .ToList();
            return machinesThatUsesLatestAssets;
        }

        public List<string> GetMachinesByAssetAndSeries(string? assetName, string? seriesNumber)
        {
            var result = new List<string>();
            
            if (!string.IsNullOrEmpty(assetName) && !string.IsNullOrEmpty(seriesNumber))
            {
                result = AppConstants.Machines?.Where(machine => machine.Assets.Any(asset =>
                        asset.AssetName.ToLower() == assetName.ToLower() &&
                        asset.SeriesNumber.ToLower() == seriesNumber.Trim().ToLower()))
                    .Select(machine => machine.MachineName).ToList();
            }
            else if (!string.IsNullOrEmpty(assetName))
            {
                result = AppConstants.Machines.Where(machine => machine.Assets.Any(asset => asset.AssetName?.ToLower() == assetName.Trim().ToLower()))
                    .Select(machine => machine.MachineName)
                    .ToList();
            }
            else if (!string.IsNullOrEmpty(seriesNumber))
            {
                result = AppConstants.Machines
                    ?.Where(machine =>
                        machine.Assets.Any(asset => asset.SeriesNumber.ToLower() == seriesNumber.Trim().ToLower()))
                    .Select(machine => machine.MachineName).ToList();
            }
            else
            {
                result = AppConstants.Machines?.Select(machine => machine.MachineName).ToList();
            }

            return result;
        }
    }
}
