using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopProject
{
    public class ParentDecorator : Beverage
    {
        protected Beverage beverage;
        protected Materials material;

        void CalculateGramms(float gramms)
        {
            beverage.Weight += gramms;
        }

        protected void CreateMaterial()
        {
            material = new Materials();
        }

        void CalculateCost(float gramms)
        {
            beverage.Cost += material.PriceForGramm * gramms;
        }

        void SetMaterialOnBeverage(float gramm)
        {
            CheckOnNull();
            beverage.Materials.Add(material.Name,gramm);
        }

        void CheckOnNull()
        {
            if (beverage.Materials == null)
                beverage.Materials = new Dictionary<string, float>();
        }

        public void SetAllInformation(float gramms)
        {
            CalculateGramms(gramms);
            CalculateCost(gramms);
            SetMaterialOnBeverage(gramms);

        }
    }
}
