using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopProject
{
    public abstract class AbstractMaterialFactory
    {
        public abstract void CreateGoldMaterial(Beverage beverage, float gramms);
        public abstract void CreateDiamondMaterial(Beverage beverage, float gramms);
        public abstract void CreateSilverMaterial(Beverage beverage, float gramms);
        public abstract void CreatePlatinumMaterial(Beverage beverage, float gramms);
        public abstract void CreateSaphireMaterial(Beverage beverage, float gramms);

    }
}
