using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace JewelShopProject
{
    public class JewelShopDataDB
    {
        String connection = @"Data Source = DESKTOP-GV41M9P\SQLEXPRESS;Initial Catalog = JewelShop; Integrated Security = True";

        AbstractMaterialFactory abstractMaterialFactory = new BeverageFactory();
        SqlDataReader sqlDataReader;
        Materials material;
        Beverage beverage;
        List<Beverage> beverages;
        List<Materials> materials;

        public List<Beverage> GetDataFromDataBaseToFillBeverageCollection()
        {
            beverages = new List<Beverage>();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    beverages = GetBeverageCorteges(conn);
                    beverages = GetCompositeMaterials();
                }
                catch (SqlException se)
                {
                    MessageBox.Show(se.Message);
                }
            }
            return beverages;
        }

        public List<Materials> GetDataFromDataBaseToFillMaterialsCollection()
        {
            materials = new List<Materials>();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    GetMaterialsCorteges(conn);
                }
                catch (SqlException se)
                {
                    MessageBox.Show(se.Message);
                }
            }
            return materials;
        }

        List<Materials> GetMaterialsCorteges(SqlConnection conn)
        {
            SqlCommand sqlCommand = new SqlCommand("Select * from vwMaterialsAndPriceForGramm", conn);

            using (sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        material = new Materials();
                        material.Name = sqlDataReader.GetValue(0).ToString().Trim();
                        material.PriceForGramm = Convert.ToSingle(sqlDataReader.GetValue(1));
                        materials.Add(material);
                    }
                }
            }
            return materials;
        }
        
        List<Beverage> GetBeverageCorteges(SqlConnection conn)
        {
            
            SqlCommand sqlCommand = new SqlCommand("Select * from vwWares", conn);

            using (sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        beverage = new Beverage();
                        beverage.SetID(Convert.ToInt32(sqlDataReader.GetValue(0)));
                        beverage.Name = sqlDataReader.GetValue(1).ToString().Trim();
                        beverage.Type = sqlDataReader.GetValue(2).ToString().Trim();
                        beverages.Add(beverage);
                    }
                }
            }
            return beverages;
        }

        List<Beverage> GetCompositeMaterials()
        {
            for (int i = 0; i < beverages.Count; i++)
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("Select * from vwWaresAndMaterial where Название=" + "'" + (beverages.ElementAt(i)).Name + "'", conn);

                    using (sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                CreateBeverageByFactory((beverages[i]), sqlDataReader.GetValue(1).ToString().Trim(), Convert.ToSingle(sqlDataReader.GetValue(2)));
                            }
                        }
                    }
                }
            }
            return beverages;
        }

        void CreateBeverageByFactory(Beverage beverage, string material, float gramm)
        {
            switch (material)
            {
                case "Золото":
                    abstractMaterialFactory.CreateGoldMaterial(beverage, gramm);
                    break;
                case "Бриллиант":
                    abstractMaterialFactory.CreateDiamondMaterial(beverage, gramm);
                    break;
                case "Платина":
                    abstractMaterialFactory.CreatePlatinumMaterial(beverage, gramm);
                    break;
                case "Сапфир":
                    abstractMaterialFactory.CreateSaphireMaterial(beverage, gramm);
                    break;
                case "Серебро":
                    abstractMaterialFactory.CreateSilverMaterial(beverage, gramm);
                    break;
            }
        }

        public void AddBeveragesToDataBase(Beverage beverage)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    AddToWares(beverage, cmd);
                    AddToLinkWareAndMaterials(beverage, cmd);
                }
                conn.Close();
            }
        }

        void AddToWares(Beverage beverage, SqlCommand cmd)
        {
            cmd.CommandText = "AddToWaresTable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beverage.Name);
            cmd.Parameters.AddWithValue("@Type", beverage.Type);
            cmd.Parameters.AddWithValue("@Weight", beverage.Weight);
            cmd.Parameters.AddWithValue("@Price", beverage.Cost);
            var reader = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            reader.Close();
        }

        void AddToLinkWareAndMaterials(Beverage beverage, SqlCommand cmd)
        {
            cmd.CommandText = "AddMaterialsToTable";
            for (int i = 0; i < beverage.Materials.Count; i++)
            {
                cmd.Parameters.AddWithValue("@Name", beverage.Name);
                cmd.Parameters.AddWithValue("@NameMaterial", beverage.Materials.Keys.ElementAt(i));
                cmd.Parameters.AddWithValue("@Gramms", beverage.Materials.Values.ElementAt(i));
                var reader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                reader.Close();
            }
        }
    }
}
