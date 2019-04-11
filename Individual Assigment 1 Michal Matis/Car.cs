using System;


namespace Individual_Assigment_1_Michal_Matis
{
    class Car
    {
        //class of Car 
        #region properties
        public int MyID { get; }
        public int ProductionYear { get; set; }
        public int DrivenKilometers { get; set; }
        public string Brand { get; set; }
        public string TypeOfCar { get; set; }
        public decimal Price { get; set; }
        public string PlaceOfSell { get; set; }
        public int NumberOfDoors { get; set; }
        public bool IsDamaged { get; set; }
        public Program.FuelTypes Fuel { get; set; }
        #endregion
        #region constructor
        public Car(int ID, int productionYear, int drivenKilometers, string brand, string typeOfCar, decimal price, string placeOfSell, int numberOfDoors, bool isDamaged, Program.FuelTypes fuel)
        {
            MyID = ID;
            ProductionYear = productionYear;
            DrivenKilometers = drivenKilometers;
            Brand = brand;
            TypeOfCar = typeOfCar;
            Price = price;
            PlaceOfSell = placeOfSell;
            NumberOfDoors = numberOfDoors;
            IsDamaged = isDamaged;
            Fuel = fuel;
        }

        #endregion

        public void DescribeMe()
        {
            Console.WriteLine($"" +
    $"ID:                {MyID}\n" +
    $"Production year:   {ProductionYear}\n" +
    $"Kilometers driven: {DrivenKilometers}\n" +
    $"Brand:             {Brand}\n" +
    $"Type:              {TypeOfCar}\n" +
    $"Price:             {Price}\n" +
    $"Place of sell:     {PlaceOfSell}\n" +
    $"Number of doors:   {NumberOfDoors}\n" +
    $"Damaged?:          {IsDamaged}\n" +
    $"Fuel type:         {Fuel}");
        }

    }
}
