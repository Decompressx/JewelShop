using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopProject
{
    public class GoldDecorator : ParentDecorator
    {
        public GoldDecorator(Beverage beverage, float gramms)
        {
            this.beverage = beverage;
            CreateMaterial();
            SetMaterial();
            SetAllInformation(gramms);
        }

        void SetMaterial()
        {
            material.Name = "Золото";
            material.PriceForGramm = 2370;

        }
    }
}
