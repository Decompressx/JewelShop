using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopProject
{
    public class DiamondDecorator : ParentDecorator
    {
        public DiamondDecorator(Beverage beverage, float gramms)
        {
            this.beverage = beverage;
            CreateMaterial();
            SetMaterial();
            SetAllInformation(gramms);
        }

        void SetMaterial()
        {
            material.Name = "Бриллиант";
            material.PriceForGramm = 3000;

        }
    }
}
