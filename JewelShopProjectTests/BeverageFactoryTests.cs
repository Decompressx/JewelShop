using Microsoft.VisualStudio.TestTools.UnitTesting;
using JewelShopProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopProject.Tests
{
    [TestClass()]
    public class BeverageFactoryTests
    {
        AbstractMaterialFactory abstractMaterial = new BeverageFactory();
        Beverage beverage;
        [TestMethod()]
        public void CreateDiamondMaterialTest()
        {
            beverage = new Beverage();
            abstractMaterial.CreateDiamondMaterial(beverage,1);
            Assert.AreEqual(3000,beverage.Cost);
            Assert.AreEqual(1,beverage.Weight);
        }

        [TestMethod()]
        public void CreateGoldMaterialTest()
        {
            beverage = new Beverage();
            abstractMaterial.CreateGoldMaterial(beverage, 1);
            Assert.AreEqual(2370, beverage.Cost);
            Assert.AreEqual(1, beverage.Weight);
        }

        [TestMethod()]
        public void CreateSilverMaterialTest()
        {
            beverage = new Beverage();
            abstractMaterial.CreateSilverMaterial(beverage, 1);
            Assert.AreEqual(1000, beverage.Cost);
            Assert.AreEqual(1, beverage.Weight);
        }

        [TestMethod()]
        public void CreatePlatinumMaterialTest()
        {
            beverage = new Beverage();
            abstractMaterial.CreatePlatinumMaterial(beverage, 1);
            Assert.AreEqual(2000, beverage.Cost);
            Assert.AreEqual(1, beverage.Weight);
        }

        [TestMethod()]
        public void CreateSaphireMaterialTest()
        {
            beverage = new Beverage();
            abstractMaterial.CreateSaphireMaterial(beverage, 1);
            Assert.AreEqual(1800, beverage.Cost);
            Assert.AreEqual(1, beverage.Weight);
        }
    }
}