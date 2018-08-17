using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;

namespace JewelShopProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AbstractMaterialFactory abstractMaterialFactory;
        JewelShopDataDB jewelShopData;

        List<Beverage> beverages;
        List<Materials> materials;
        Dictionary<string, float> materialGrammPairs;
        Beverage beverage;

        public MainWindow()
        {
            InitializeComponent();
            InitializeObject();
            FillAllCollection();
            FillComboBoxMaterials();
        }

        void InitializeObject()
        {
            jewelShopData = new JewelShopDataDB();
            abstractMaterialFactory = new BeverageFactory();
            materialGrammPairs = new Dictionary<string, float>();
        }

        void FillAllCollection()
        {
            beverages = FillBeverageList();
            materials = FillMaterialsList();
        }
        void FillComboBoxMaterials()
        {
            for (int i = 0; i < materials.Count; i++)
            {
                MaterialListComboBox.Items.Add(materials.ElementAt(i).Name);
            }
        }


        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void AddMaterialToListButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MaterialListComboBox.SelectedIndex == -1)
                    throw new EmptyComboBoxException();
                else if (string.IsNullOrEmpty(GrammsTextBox.Text) || GrammsTextBox.Text == "0" || Convert.ToSingle(GrammsTextBox.Text) < 0)
                    throw new EmptyTextBoxException();
                CheckAddToMaterialList(MaterialListComboBox.SelectionBoxItem.ToString(), MaterialListComboBox.SelectedIndex);
                GrammsTextBox.Clear();
            }
            catch (EmptyComboBoxException)
            {
                MessageBox.Show("Вы не выбрали материал, выберите и попробуйте заного");
            }
            catch (EmptyTextBoxException)
            {
                MessageBox.Show("Введите граммы корректно (целым числом или десятичным через запятую) и попробуйте заного");
            }
        }

        private void OutTableOnDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataOutListComboBox.SelectedIndex == 0)
            {
                dgForOutTable.ItemsSource = OutBeveragesOnDataGrid().DefaultView;
            }
            else if (DataOutListComboBox.SelectedIndex == 1)
            {
                dgForOutTable.ItemsSource = OutBeverageAndMaterial().DefaultView;
            }
            else if (DataOutListComboBox.SelectedIndex == 2)
            {
                dgForOutTable.ItemsSource = materials;
            }
        }

        private void MakeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (materialGrammPairs.Count == 0)
                    throw new EmptyMaterialListException();
                else if (string.IsNullOrEmpty(SetWareNameTextBox.Text))
                    throw new EmptyTextBoxException();
                else if (TypeOfWareComboBox.SelectedIndex == -1)
                    throw new EmptyComboBoxException();

                beverage = AssembleAnBeverage();
                SendBeverageToDataBase(beverage);
                ClearPrevOrder();
            }
            catch (EmptyMaterialListException)
            {
                MessageBox.Show("Вы не выбрали ни одного материала, сделайте список и закажите заного");
            }
            catch (EmptyComboBoxException)
            {
                MessageBox.Show("Вы не выбрали тип изделия, выберите и нажмите кнопку заказать");
            }
            catch (EmptyTextBoxException)
            {
                MessageBox.Show("Вы не ввели имя, введите и нажмите кнопку заказать");
            }
        }

        Beverage AssembleAnBeverage()
        {
            Beverage beverage = new Beverage();
            beverage.Name = SetWareNameTextBox.Text;
            beverage.Type = TypeOfWareComboBox.Text;

            foreach (var item in materialGrammPairs)
            {
                switch (item.Key)
                {
                    case "Золото":
                        abstractMaterialFactory.CreateGoldMaterial(beverage, item.Value);
                        break;
                    case "Бриллиант":
                        abstractMaterialFactory.CreateDiamondMaterial(beverage, item.Value);
                        break;
                    case "Платина":
                        abstractMaterialFactory.CreatePlatinumMaterial(beverage, item.Value);
                        break;
                    case "Сапфир":
                        abstractMaterialFactory.CreateSaphireMaterial(beverage, item.Value);
                        break;
                    case "Серебро":
                        abstractMaterialFactory.CreateSilverMaterial(beverage, item.Value);
                        break;
                }
            }
            return beverage;
        }

        void SendBeverageToDataBase(Beverage beverage)
        {
            jewelShopData.AddBeveragesToDataBase(beverage);
        }

        void ClearPrevOrder()
        {
            MaterialsForWareList.Items.Clear();
            materialGrammPairs.Clear();
            SetWareNameTextBox.Clear();
            GrammsTextBox.Clear();
        }

        List<Beverage> FillBeverageList()
        {
            return jewelShopData.GetDataFromDataBaseToFillBeverageCollection();
        }

        List<Materials> FillMaterialsList()
        {

            return jewelShopData.GetDataFromDataBaseToFillMaterialsCollection();
        }

        List<string> GetBeverageAndMaterials()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < beverages.Count; i++)
            {
                foreach (var material in beverages[i].Materials)
                {
                    list.Add(beverages[i].Name + material.Key + material.Value);
                }
            }
            return list;
        }

        void CheckAddToMaterialList(string material, int index)
        {
            try
            {
                if (MaterialListComboBox.SelectedIndex == index && materialGrammPairs.ContainsKey(material) != true)
                {
                    materialGrammPairs.Add(material, Convert.ToSingle(GrammsTextBox.Text));
                    MaterialsForWareList.Items.Add(material + " " + materialGrammPairs[material]);
                    SetVisibilityControls();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите граммы корректно(целым числом или десятичным через запятую) и попробуйте заного");
            }
        }

        void SetVisibilityControls()
        {
            SetWareNameTextBox.Visibility = Visibility.Visible;
            NameWareTB.Visibility = Visibility.Visible;
            MakeOrderButton.Visibility = Visibility.Visible;
            TypeOfWareComboBox.Visibility = Visibility.Visible;
            CbTypeWare.Visibility = Visibility.Visible;
        }

        private void MaterialsForWareList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DeleteMAterialsFromList();
            }
        }

        void DeleteMAterialsFromList()
        {
            try
            {
                if (MaterialsForWareList.SelectedIndex == -1)
                    throw new EmptyMaterialListException();
                materialGrammPairs.Remove(materialGrammPairs.ElementAt(MaterialsForWareList.SelectedIndex).Key);
                MaterialsForWareList.Items.Remove(MaterialsForWareList.SelectedItem);
            }
            catch (EmptyMaterialListException)
            {
                MessageBox.Show("Удаление не допустимо, список пуст");
            }
        }

        DataTable OutBeveragesOnDataGrid()
        {
            var table = new DataTable();
            table.Columns.Add("Название");
            table.Columns.Add("Тип");
            table.Columns.Add("Вес");
            table.Columns.Add("Цена");

            for (int i = 0; i < beverages.Count; i++)
            {
                table.Rows.Add(new object[] { beverages[i].Name, beverages[i].Type, beverages[i].Weight, beverages[i].Cost });
            }
            return table;
        }

        DataTable OutBeverageAndMaterial()
        {
            var table = new DataTable();
            table.Columns.Add("Название");
            table.Columns.Add("Материал");

            for (int i = 0; i < beverages.Count; i++)
            {
                for (int j = 0; j < beverages[i].Materials.Count; j++)
                {
                    table.Rows.Add(new object[] { beverages[i].Name,beverages[i].Materials.Keys.ElementAt(j)});
                }
            }
            return table;
        }
    }
}
