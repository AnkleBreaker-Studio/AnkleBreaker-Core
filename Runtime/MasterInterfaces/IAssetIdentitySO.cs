using System.Collections.Generic;
using AnkleBreaker.Core.MasterClasses;
using AnkleBreaker.Core.MasterStructs;

namespace AnkleBreaker.Core.MasterInterfaces
{
    public interface IAssetIdentitySO
    {
        public string name { get; }
        public ulong ID { get; }
        public List<AnkleBreakerCategory> Categories { get; }
        public List<AssetIdentityStruct> IdentityPerLevel { get; }
    }
}
