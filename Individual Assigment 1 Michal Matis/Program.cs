using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Assigment_1_Michal_Matis
{
    class Program
    {
        static void Main(string[] args)
        {

            CarBazaar autoSalon = new CarBazaar();            
            MainMenu(autoSalon);
        }

        private static void MainMenu(CarBazaar autoSalon)
        {

            bool repeat = true;
            while (repeat)
            {
                Console.Clear();
                Console.WriteLine("WELCOME TO THE CAR BAZAAR\n" +
                    "Choose what you want:\n" +
                    "1: Show all cars\n" +
                    "2: Add new Car\n" +
                    "3: Edit car \n" +
                    "4: Delete car \n" +
                    "5: Save Changes \n" +
                    "6: Filters \n" +
                    "7: Load Cars from File\n" +
                    "8: Save and Exit\n" +
                    "9: Exit without Saving");

                int choice = CarBazaar.CheckInt();
                switch (choice)
                {
                    case 1:
                        {
                            autoSalon.WriteAllCars();
                            Console.ReadLine();
                            break;
                        }
                    case 2:
                        {
                            autoSalon.AddCarFromKeyboard();
                            break;
                        }
                    case 3:
                        {
                            autoSalon.EditCarByID();
                            break;
                        }
                    case 4:
                        {
                            autoSalon.DeleteCarByID();
                            break;
                        }
                    case 5:
                        {
                            autoSalon.SaveToFile();
                            break;
                        }
                    case 6:
                        {
                            autoSalon.FiltersUI();
                            break;
                        }
                    case 7:
                        {
                            autoSalon.LoadFromFile();
                            break;
                        }
                    case 8:
                        {
                            autoSalon.SaveToFile();
                            repeat = false;
                            break;
                        }

                    case 9:
                        {
                            repeat = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong Choice");
                            break;
                        }

                }
            }
        }

        public enum FuelTypes
        {
            Gasoline,
            Diesel,
            Gas,
            Electricity,
            Hybrid
        }



    }
}
