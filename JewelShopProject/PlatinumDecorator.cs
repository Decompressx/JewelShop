using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopProject
{
    public class PlatinumDecorator : ParentDecorator
    {
        public PlatinumDecorator(Beverage beverage, float gramms)
        {
            this.beverage = beverage;
            CreateMaterial();
            SetMaterial();
            SetAllInformation(gramms);
        }

        void SetMaterial()
        {
            material.Name = "Платина";
            material.PriceForGramm = 2000;

        }
    }
}
