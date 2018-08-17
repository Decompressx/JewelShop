using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopProject
{
    public class SaphireDecorator : ParentDecorator
    {
        public SaphireDecorator(Beverage beverage, float gramms)
        {
            this.beverage = beverage;
            CreateMaterial();
            SetMaterial();
            SetAllInformation(gramms);
        }

        void SetMaterial()
        {
            material.Name = "Сапфир";
            material.PriceForGramm = 1800;

        }
    }
}
