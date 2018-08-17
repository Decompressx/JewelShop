using System.Collections.Generic;

namespace JewelShopProject
{
    public class Beverage
    {
        int id;
        public string Name { get; set; }
        public string Type { get; set; }
        public float Weight { get; set; }
        public float Cost { get; set; }

        public Dictionary<string, float> Materials { get; set; }

        public void SetID(int id)
        {
            this.id = id;
        }

        public int GetID()
        {
            return id;
        }
    }
}
