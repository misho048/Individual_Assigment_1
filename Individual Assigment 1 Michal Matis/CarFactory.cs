using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Assigment_1_Michal_Matis
{
    class CarFactory
    {
        //This class is creating new cars and holding unique ID 
        private int ID;
        
        public CarFactory( )
        {          
            
        }

        private void FixID(int input)
        {
            if (input > ID)
            {
                ID = input;
            }

            
        }

        public Car CreateCarFromFile(int idFromFile, int productionYear, int drivenKilometers, string brand, string typeOfCar, decimal price, string placeOfSell, int numberOfDoors, bool isDamaged, Program.FuelTypes fuel)
        {
            FixID(idFromFile);
            return new Car(idFromFile, productionYear, drivenKilometers, brand, typeOfCar, price, placeOfSell, numberOfDoors, isDamaged, fuel);
        }
               
        public Car CreateCar(int productionYear, int drivenKilometers, string brand, string typeOfCar, decimal price, string placeOfSell, int numberOfDoors, bool isDamaged, Program.FuelTypes fuel)
        {
            ID++;
            return new Car(ID,productionYear,drivenKilometers,brand,typeOfCar,price,placeOfSell,numberOfDoors, isDamaged,fuel);
        }
    }
}
