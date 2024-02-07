﻿//using AssetManagement.Models;
using AssetManagement.Api.Repository;
using MongoDB.Driver;
using AssetManagement.Api.MongoDBModels;
using AssetManagement.Models;

namespace AssetManagement.Api.MongoDB
{
    public class MongoDbRepository
    {
        private readonly IMongoCollection<MachineModel> _machineCollection;
        public MongoDbRepository(IMachineDataStoreSetting machineDataStoreSetting, IMongoClient client)
        {
            var db = client.GetDatabase(machineDataStoreSetting.DatabaseName);
            _machineCollection = db.GetCollection<MachineModel>(machineDataStoreSetting.CollectionName);

            var countOfCollectionsInMachineCollection = _machineCollection.CountDocuments(machine => true);

            if (countOfCollectionsInMachineCollection == 0)
            {
                List<MachineModel> machines = new()
                {
                    new MachineModel()
                    {
                        MachineName = "C300",
                        Assets = new List<Asset>()
                        {
                            new Asset() { AssetName = "Cutter head", SeriesNumber = "S6" },
                            new Asset() { AssetName = "Blade safety cover", SeriesNumber = "S10" },
                            new Asset() { AssetName = "Deburring blades", SeriesNumber = "S6" }
                        }
                    },
                    new MachineModel()
                    {
                        MachineName = "C40",
                        Assets = new List<Asset>()
                        {
                            new Asset() { AssetName = "Cutter head", SeriesNumber = "S7" },
                            new Asset() { AssetName = "Blade safety cover", SeriesNumber = "S11" },
                            new Asset() { AssetName = "Shutter gripper", SeriesNumber = "S3" }
                        }
                    },
                    new MachineModel()
                    {
                        MachineName = "C60",
                        Assets = new List<Asset>()
                        {
                            new Asset() { AssetName = "Blade safety cover", SeriesNumber = "S11" },
                            new Asset() { AssetName = "Cutter head", SeriesNumber = "S8" },
                            new Asset() { AssetName = "Clamping fixture", SeriesNumber = "S2" }
                        }
                    }
                };

                foreach (var machine in machines)
                {
                    _machineCollection.InsertOne(machine);
                }
            }

        }

        public List<Asset> GetAsset(string? machineName)
        {
            var filterQuery = Builders<MachineModel>.Filter.Eq(machine => machine.MachineName, machineName.ToUpper());
            return _machineCollection.Find(filterQuery).Project(machine => machine.Assets).FirstOrDefault();
        }

        public List<Machine> GetMachines()
        {
            var result = _machineCollection.Find(machine => true).Project(machine => new Machine { MachineName = machine.MachineName, Assets = machine.Assets }).ToList();
            return result;
        }
        public List<string> GetMachinesByAssetName(string? assetName)
        {
            var filterQuery = Builders<MachineModel>.Filter.ElemMatch(machine => machine.Assets, asset => asset.AssetName.ToLower() == assetName.ToLower());
            return _machineCollection.Find(filterQuery).Project(machine => machine.MachineName).ToList();
        }

        public List<string> GetMachineThatUsesLatestAssets()
        {
            List<MachineModel> machineList = _machineCollection.Find(machine => true).ToList();

            Dictionary<string, int> assetDictionary = new();

            foreach (var machine in machineList)
            {
                foreach (var asset in machine.Assets)
                {
                    if (!assetDictionary.ContainsKey(asset.AssetName))
                    {
                        assetDictionary.Add(asset.AssetName, Convert.ToInt32(asset.SeriesNumber.Substring(1)));
                    }
                    else
                    {
                        var currentSeriesNumber = Convert.ToInt32(asset.SeriesNumber.Substring(1));
                        var currentSeriesNumberFromDictionary = assetDictionary[asset.AssetName];

                        assetDictionary[asset.AssetName] = Math.Max(currentSeriesNumber, currentSeriesNumberFromDictionary);
                    }
                }
            }

            List<string> machineThatUsesLatestAsset = new();

            foreach (var machine in machineList)
            {
                bool found = true;
                foreach (var asset in machine.Assets)
                {
                    var currentAssetSeriesNumber = Convert.ToInt32(asset.SeriesNumber.Substring(1));
                    var maximumSeriesNumberOfCurrentAsset = assetDictionary[asset.AssetName];

                    if (currentAssetSeriesNumber != maximumSeriesNumberOfCurrentAsset)
                    {
                        found = false;
                    }
                }
                if (found)
                {
                    machineThatUsesLatestAsset.Add(machine.MachineName);
                }
            }
            return machineThatUsesLatestAsset;
        }
    }
}
