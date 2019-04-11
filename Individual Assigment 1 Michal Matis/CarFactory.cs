
using System.Collections.Generic;


namespace Individual_Assigment_1_Michal_Matis
{
    class CarFactory
    {
        //This class is creating new cars and holding unique ID 
        private int ID;
        private CarRepository repository = new CarRepository();
        
        public CarFactory( )
        {          
            
        }       
                      
        public void CreateCar(int productionYear, int drivenKilometers, string brand, string typeOfCar, decimal price, string placeOfSell, int numberOfDoors, bool isDamaged, Program.FuelTypes fuel)
        {
            
            CarModel car = new CarModel();
            car.MyID= repository.GetMaxID() + 1;
            car.ProductionYear = productionYear;
            car.DrivenKilometers = drivenKilometers;
            car.Brand = brand;
            car.TypeOfCar = typeOfCar;
            car.Price = price;
            car.PlaceOfSell = placeOfSell;
            car.NumberOfDoors = numberOfDoors;
            car.IsDamaged = isDamaged;
            car.Fuel = fuel;
            repository.AddNewCar(car);           
        }

        public void CreateCar(int ID,int productionYear, int drivenKilometers, string brand, string typeOfCar, decimal price, string placeOfSell, int numberOfDoors, bool isDamaged, Program.FuelTypes fuel)
        {

            CarModel car = new CarModel();
            car.MyID = ID;
            car.ProductionYear = productionYear;
            car.DrivenKilometers = drivenKilometers;
            car.Brand = brand;
            car.TypeOfCar = typeOfCar;
            car.Price = price;
            car.PlaceOfSell = placeOfSell;
            car.NumberOfDoors = numberOfDoors;
            car.IsDamaged = isDamaged;
            car.Fuel = fuel;
            repository.AddNewCar(car);
        }

        public Dictionary<int, CarModel> GetAllCars()
        {
            return repository.GetAll();
        }

        public CarModel GetCarByID(int id)
        {
            return repository.GetCarByID(id);
        }

        public void RemoveCar (int id)
        {
            repository.DeleteCar(id);
        }

        

    }
}
