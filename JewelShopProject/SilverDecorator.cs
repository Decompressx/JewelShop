using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopProject
{
    public class SilverDecorator : ParentDecorator
    {
        public SilverDecorator(Beverage beverage, float gramms)
        {
            this.beverage = beverage;
            CreateMaterial();
            SetMaterial();
            SetAllInformation(gramms);
        }

        void SetMaterial()
        {
            material.Name = "Серебро";
            material.PriceForGramm = 1000;

        }
    }
}
