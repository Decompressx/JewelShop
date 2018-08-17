using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopProject
{
    public class BeverageFactory : AbstractMaterialFactory
    {
        public override void CreateGoldMaterial(Beverage beverage, float gramms)
        {
            new GoldDecorator(beverage, gramms);
        }
        public override void CreateDiamondMaterial(Beverage beverage, float gramms)
        {
            new DiamondDecorator(beverage, gramms);
        }
        public override void CreateSilverMaterial(Beverage beverage, float gramms)
        {
            new SilverDecorator(beverage, gramms);
        }
        public override void CreatePlatinumMaterial(Beverage beverage, float gramms)
        {
            new PlatinumDecorator(beverage, gramms);
        }
        public override void CreateSaphireMaterial(Beverage beverage, float gramms)
        {
            new SaphireDecorator(beverage, gramms);
        }
    }
}
