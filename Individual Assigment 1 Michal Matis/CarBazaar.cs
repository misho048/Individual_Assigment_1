using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Assigment_1_Michal_Matis
{
    class CarBazaar
    {
        public Dictionary<int, Car> _mapOfCars = new Dictionary<int, Car>();
        public CarFactory carFactory;

        public CarBazaar()
        {
            carFactory = new CarFactory();
            LoadFromFile();
        }

        public void SaveToFile()
        {
            try
            {
                if (!File.Exists("CarList.txt"))
                {
                    File.Create("CarList.txt");
                }
                else
                {
                    TextWriter writer = new StreamWriter("CarList.txt");
                    foreach (KeyValuePair<int, Car> key in _mapOfCars)
                    {

                        Car car = _mapOfCars[key.Key];
                        writer.WriteLine($"{car.MyID}\t{car.Brand}\t{car.TypeOfCar}\t{car.ProductionYear}\t{car.DrivenKilometers}\t{car.Price}\t" +
                            $"{car.PlaceOfSell}\t{car.NumberOfDoors}\t{car.IsDamaged}\t{car.Fuel}");

                    }
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected Error Happend\n" +
                    $"Error Info:{e.Message}");
            }
        }

        public void LoadFromFile()
        {
            try
            {
                if (!File.Exists("CarList.txt"))
                {
                    File.Create("CarList.txt");

                }
                else
                {

                    string[] splitLine;
                    string[] lines = File.ReadAllLines("CarList.txt");
                    foreach (string line in lines)
                    {
                        splitLine = line.Split('\t');

                        Car car = carFactory.CreateCarFromFile(Convert.ToInt32(splitLine[0]), Convert.ToInt32(splitLine[3]), Convert.ToInt32(splitLine[4]), splitLine[1], splitLine[2],
                            Convert.ToDecimal(splitLine[5]), splitLine[6], Convert.ToInt32(splitLine[7]), Convert.ToBoolean(splitLine[8]),
                            (Program.FuelTypes)Enum.Parse(typeof(Program.FuelTypes), splitLine[9]));
                        _mapOfCars[car.MyID] = car;
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Corrupted File");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Reseting File to Default");
                Console.ReadLine();
                Console.Clear();
                File.Delete("CarList.txt");
                File.Create("CarList.txt").Close();

                Car car = carFactory.CreateCarFromFile(0, 0, 0, "Default Brand of the  Car", "Default type of the Car", 0, "Default Place", 0, false, Program.FuelTypes.Diesel);
                _mapOfCars.Clear();
                _mapOfCars[car.MyID] = car;
                SaveToFile();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected Error Happend\n" +
                                    $"Error Info:{e.Message}");
            }
         
        }

        public void AddCarFromKeyboard()
        {
            Console.Clear();
            Console.WriteLine("Enter year of the production");
            int year = CheckYear();
            Console.WriteLine("Enter number of kilometers");
            int numberOfKilometers = CheckInt();
            Console.WriteLine("Enter brand of the car");
            string brand = CheckString();
            Console.WriteLine("Enter type of the car");
            string type = CheckString();
            Console.WriteLine("Enter place where you are selling the car");
            string placeOfSell = CheckString();
            Console.WriteLine("Enter price");
            decimal price = CheckDecimal();
            Console.WriteLine("Enter number of doors");
            int numOfDoors = CheckInt();
            Console.WriteLine("Is car damaged? y/n");
            bool isDamaged = CheckBoolean();
            Console.Write("Choose fuel type:");
            foreach (var something in (Program.FuelTypes[])Enum.GetValues(typeof(Program.FuelTypes)))
            {
                Console.Write($" {something},");
            }
            Console.WriteLine();
            Program.FuelTypes typeOfFuel = CheckFuel();
            Car car = carFactory.CreateCar(year, numberOfKilometers, brand, type, price, placeOfSell, numOfDoors, isDamaged, typeOfFuel);
            _mapOfCars[car.MyID] = car;
            SaveToFile();
        }

        public void EditCarByID()
        {
            Console.Clear();
            if (_mapOfCars.Count == 0)
            {
                Console.WriteLine("List is empty add some cars first");
                Console.ReadLine();
            }
            else
            {


                WriteAllCars();
                int id = CheckID();
                try
                {
                    //
                    bool helpme = true;
                    while (helpme)
                    {

                        Console.WriteLine("What you want to edit (Enter Number)\n" +
                            "1: Year of prodution\n" +
                            "2: Number of Kilometers\n" +
                            "3: Brand of the car\n" +
                            "4: Type of the car \n" +
                            "5: Place of sell \n" +
                            "6: Price \n" +
                            "7: Number of doors \n" +
                            "8: Car Condition (Is still damaged?) \n" +
                            "9: Fuel Type\n" +
                            "0: Return");

                        int choice = CheckInt();
                        switch (choice)
                        {
                            case 1:
                                {
                                    Console.WriteLine($"Old Year :{ _mapOfCars[id].ProductionYear}\n" +
                                        $"Enter new Year");
                                    _mapOfCars[id].ProductionYear = CheckYear();
                                    break;
                                }
                            case 2:
                                {
                                    Console.WriteLine($"Old number of kilometers :{ _mapOfCars[id].DrivenKilometers}\n" +
                                        $"Enter new number of kilometers");
                                    _mapOfCars[id].DrivenKilometers = CheckInt();
                                    break;
                                }
                            case 3:
                                {
                                    Console.WriteLine($"Old Brand of the car :{ _mapOfCars[id].Brand}\n" +
                                        $"Enter new Brand of the car");
                                    _mapOfCars[id].Brand = CheckString();
                                    break;
                                }

                            case 4:
                                {
                                    Console.WriteLine($"Old Type of the car :{ _mapOfCars[id].TypeOfCar}\n" +
                                        $"Enter new Type of the car");
                                    _mapOfCars[id].TypeOfCar = CheckString();
                                    break;
                                }

                            case 5:
                                {
                                    Console.WriteLine($"Old Place of sell :{ _mapOfCars[id].PlaceOfSell}\n" +
                                        $"Enter new Place of sell");
                                    _mapOfCars[id].PlaceOfSell = CheckString();
                                    break;
                                }

                            case 6:
                                {
                                    Console.WriteLine($"Old Price :{ _mapOfCars[id].Price}\n" +
                                        $"Enter new Price");
                                    _mapOfCars[id].Price = CheckDecimal();
                                    break;

                                }

                            case 7:
                                {
                                    Console.WriteLine($"Old Number of Doors :{ _mapOfCars[id].NumberOfDoors}\n" +
                                        $"Enter new Number of Doors");
                                    _mapOfCars[id].NumberOfDoors = CheckInt();
                                    break;
                                }

                            case 8:
                                {
                                    Console.WriteLine($"Old Condition (Is car damaged?) :{ _mapOfCars[id].IsDamaged}\n" +
                                        $"What is new condition, Is car damaged? y/n");
                                    _mapOfCars[id].IsDamaged = CheckBoolean();
                                    break;

                                }
                            case 9:
                                {
                                    Console.Write("Choose fuel type:");
                                    foreach (var something in (Program.FuelTypes[])Enum.GetValues(typeof(Program.FuelTypes)))
                                    {
                                        Console.Write($" {something},");
                                    }
                                    Console.WriteLine();
                                    _mapOfCars[id].Fuel = CheckFuel();
                                    break;
                                }
                            case 0:
                                {
                                    helpme = false;
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Wrong choice");
                                    break;
                                }
                        }

                        if (helpme == true)
                        {
                            Console.WriteLine("Do you want to Edit something else on This car? y/n ");
                            if (!CheckBoolean())
                            {
                                helpme = false;
                            }
                        }

                    }
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Error: Car List is empty" +
                        "\nadd some Cars first");
                    Console.ReadLine();
                }
            }
        }

        public void WriteAllCars()
        {
            Console.Clear();
            foreach (KeyValuePair<int, Car> key in _mapOfCars)
            {
                _mapOfCars[key.Key].DescribeMe();
                Console.WriteLine("______________________________________________");
            }


        }

        public void DeleteCarByID()
        {
            Console.Clear();
            if (_mapOfCars.Count == 0)
            {
                Console.WriteLine("List is empty add some cars first");
                Console.ReadLine();
            }
            else
            {
                WriteAllCars();
                int id = CheckID();

                _mapOfCars[id].DescribeMe();
                Console.WriteLine("Do you really want to remove this car? y/n");
                if (CheckBoolean())
                {
                    _mapOfCars.Remove(id);
                }

            }
        }

        public void FiltersUI()
        {
            
            
            Console.Clear();
            if (_mapOfCars.Count == 0)
            {
                Console.WriteLine("You can't filter empty list\n" +
                    "Add some cars first");
            }
            else
            {
                Dictionary<int, Car> currentList = new Dictionary<int, Car>(_mapOfCars);
                Console.WriteLine("Enter number of Fillters \n" +
                    "for example for using filters 1 5 and 7 enter 157\n" +
                    "1: Year of production \n" +
                    "2: Number of Kilometers driven \n" +
                    "3: Brand of the car\n" +
                    "4: Fuel type\n" +
                    "5: Price \n" +
                    "6: Place of Sell\n" +
                    "7: Number of Doors\n" +
                    "8: Is car damaged?");
                string choice = Convert.ToString(CheckInt());
                //prechadza kazdou moznostou filtra a postupne upravuje zoznam

                if (choice.Contains("1"))
                {
                    currentList = FilterYear(currentList);
                }
                if (choice.Contains("2"))
                {
                    currentList = FilterKilometers(currentList);
                }
                if (choice.Contains("3"))
                {
                    currentList = FilterBrand(currentList);
                }
                if (choice.Contains("4"))
                {
                    currentList = FilterFuels(currentList);
                }
                if (choice.Contains("5"))
                {
                    currentList = FilterPrice(currentList);
                }
                if (choice.Contains("6"))
                {
                    currentList = FilterPlaces(currentList);
                }
                if (choice.Contains("7"))
                {
                    currentList = FilterNumOfDoors(currentList);
                }
                if (choice.Contains("8"))
                {
                    currentList = FilterDamaged(currentList);
                }
                //vypis
                Console.Clear();
                if (currentList.Count == 0)
                {
                    Console.WriteLine("Sorry no match found");
                }
                else
                {
                    foreach (KeyValuePair<int, Car> key in currentList)
                    {
                        _mapOfCars[key.Key].DescribeMe();
                        Console.WriteLine("______________________________________________");
                    }
                }
            }
            Console.ReadLine();
            

        }
        #region input control
        public static int CheckInt()
        {
            //checks if the input is good
            int result;
            while (true)
            {

                string input = Console.ReadLine();

                if (int.TryParse(input, out result))
                {
                    if (result < 0)
                    {
                        Console.WriteLine("Entered number needs to be more than 0");

                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong format Try Again");
                }
            }

        }

        private string CheckString()
        {
            while (true)
            {
                string input = Console.ReadLine();
                input = RemoveUnwantedCharacters(input, " 0123456789AÁÄBCČDĎŽEÉFGHIÍJKLĹĽaáäbcčdďeéfghiíjklĺľMNŇOÓÔPQRŔSŠTŤUÚVWXYÝZŽmnňoóôpqrŕsštťuúvwxyýzž.,-'");
                if (input.Length == 0)
                {
                    Console.WriteLine("You can't leave free space Try again");
                }
                else if (input.Contains('\t'))
                {
                    Console.WriteLine("You can't use TAB Try again");
                }
                else
                {
                    return input;
                }


            }
        }

        public int CheckYear()
        {
            //first car ever made was in 1870, 
            while (true)
            {
                int input = CheckInt();
                if (input > Convert.ToInt32(DateTime.Now.Year) || input < 1870)
                {
                    Console.WriteLine($"Wrong input, year needs to be between 1870 and {Convert.ToInt32(DateTime.Now.Year)}");
                }
                else
                {
                    return input;
                }


            }
        }

        private Program.FuelTypes CheckFuel()
        {
            //checks if the input is good
            Program.FuelTypes result;
            while (true)
            {
                if (Enum.TryParse<Program.FuelTypes>(Console.ReadLine(), out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Wrong input Try Again");
                    Console.Write("Here are valid fuel types:");
                    foreach (var something in (Program.FuelTypes[])Enum.GetValues(typeof(Program.FuelTypes)))
                    {
                        Console.Write($" {something},");
                    }
                    Console.WriteLine();
                }
            }
        }

        private decimal CheckDecimal()
        {
            //checks if the input is good
            decimal result;
            decimal outResult;
            
            while (true)
            {

                string input = Console.ReadLine();
                if (input.Contains(',') && (decimal.TryParse(input, out  outResult)))
                {               
                        if (outResult%1==0)
                        {                           
                            input=input.Replace(',', '.');
                        }                       
                   
                }
                else if(input.Contains('.') && (decimal.TryParse(input, out  outResult)))
                {
                    if (outResult % 1 == 0)
                    {
                        input = input.Replace('.', ',');
                    }

                }
                
                if (decimal.TryParse(input, out result))
                {
                    if (result < 0)
                    {
                        Console.WriteLine("Entered number needs to be more than 0");

                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong format Try Again");
                }
            }

        }

        private bool CheckBoolean()
        {
            //checks if the input is good
            while (true)
            {

                string input = Console.ReadLine();

                if (input.Equals("y") || input.Equals("Y"))
                {
                    return true;
                }
                else if (input.Equals("n") || input.Equals("N"))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Wrong format Try Again");
                }
            }
        }

        private static string RemoveUnwantedCharacters(string input, IEnumerable<char> allowedCharacters)
        {
            var filtered = input.ToCharArray()
                .Where(c => allowedCharacters.Contains(c))
                .ToArray();

            if (filtered.Length == 0)
            {
                return "";
            }

            return new String(filtered);
        }

        private int CheckID()
        {
            //checks if the input is good
            int id;
            if (_mapOfCars.Count != 0)
            {
                while (true)
                {
                    Console.WriteLine("Enter ID of the car");

                    bool isInt = int.TryParse(Console.ReadLine(), out id);
                    if (isInt && _mapOfCars.ContainsKey(id))
                    {
                        break;
                    }
                    else
                    {
                        if (isInt)
                        {
                            Console.WriteLine("Entered ID is not valid. Try again");
                        }
                        else
                        {
                            Console.WriteLine("Wrong input format, Try Again");
                        }

                    }
                }
                return id;

            }
            else
            {
                Console.WriteLine("There are no cars");
                return -1;
            }

        }
        #endregion
        #region filters
        private Dictionary<int, Car> FilterFuels(Dictionary<int, Car> listOfCars)
        {

            List<Program.FuelTypes> fuels = new List<Program.FuelTypes>();

            Console.WriteLine("Enter Type of fuel: (Gasoline,Diesel,Gas,Electricity)");
            do
            {
                fuels.Add(CheckFuel());
                Console.WriteLine("Do you want to add another type of fuel ? y/n");
            } while (CheckBoolean());

            List<int> deleteList = new List<int>();

            foreach (var car in listOfCars)
            {
                Car thiscar = listOfCars[car.Key];
                bool helpme = false;
                foreach (var fuel in fuels)

                    if (thiscar.Fuel.Equals(fuel))
                    {
                        helpme = true;
                    }
                if (!helpme)
                {
                    deleteList.Add(thiscar.MyID);
                }
            }

            foreach (var item in deleteList)
            {
                listOfCars.Remove(item);
            }
            return listOfCars;
        }

        private Dictionary<int, Car> FilterPlaces(Dictionary<int, Car> listOfCars)
        {

            List<string> places = new List<string>();
            Console.WriteLine("Enter Place:");
            do
            {
                places.Add(CheckString());
                Console.WriteLine("Do you want to add another Place?y/n");
            } while (CheckBoolean());

            List<int> deleteList = new List<int>();

            foreach (var car in listOfCars)
            {
                Car thiscar = listOfCars[car.Key];
                bool helpme = false;
                foreach (var place in places)

                    if (thiscar.PlaceOfSell.Equals(place))
                    {
                        helpme = true;
                    }
                if (!helpme)
                {
                    deleteList.Add(thiscar.MyID);
                }
            }

            foreach (var item in deleteList)
            {
                listOfCars.Remove(item);
            }
            return listOfCars;
        }

        private Dictionary<int, Car> FilterBrand(Dictionary<int, Car> listOfCars)
        {

            List<string> brands = new List<string>();

            Console.WriteLine("Enter Brand:");
            do
            {
                brands.Add(CheckString());
                Console.WriteLine("Do you want to add another brand?y/n");
            } while (CheckBoolean());

            List<int> deleteList = new List<int>();

            foreach (var car in listOfCars)
            {
                Car thiscar = listOfCars[car.Key];
                bool keepThatFucker = false;
                foreach (var brand in brands)
                {

                    if (thiscar.Brand.Equals(brand))
                    {
                        keepThatFucker = true;
                    }
                }
                if (!keepThatFucker)
                {
                    deleteList.Add(thiscar.MyID);
                }
            }

            foreach (var item in deleteList)
            {
                listOfCars.Remove(item);
            }




            return listOfCars;
        }

        private Dictionary<int, Car> FilterNumOfDoors(Dictionary<int, Car> listOfCars)
        {
            int numOfDoors;
            do
            {
                Console.WriteLine("Do You want 3 or 5 doors?");
                numOfDoors = CheckInt();
            } while ((numOfDoors != 3) && (numOfDoors != 5));


            List<int> deleteList = new List<int>();

            foreach (var car in listOfCars)
            {
                Car thiscar = listOfCars[car.Key];
                if (thiscar.NumberOfDoors != numOfDoors)
                {

                    deleteList.Add(thiscar.MyID);
                }
            }

            foreach (var item in deleteList)
            {
                listOfCars.Remove(item);
            }
            return listOfCars;
        }

        private Dictionary<int, Car> FilterYear(Dictionary<int, Car> listOfCars)
        {

            Console.WriteLine("Year of production From?");
            int from = CheckInt();
            Console.WriteLine("Year of production To?");
            int to = CheckInt();
            List<int> deleteList = new List<int>();

            foreach (var car in listOfCars)
            {
                Car thiscar = listOfCars[car.Key];
                if (thiscar.ProductionYear < from || thiscar.ProductionYear > to)
                {

                    deleteList.Add(thiscar.MyID);
                }
            }

            foreach (var item in deleteList)
            {
                listOfCars.Remove(item);
            }
            return listOfCars;
        }

        private Dictionary<int, Car> FilterKilometers(Dictionary<int, Car> listOfCars)
        {

            Console.WriteLine("Enter Number of Kilometers From?");
            int from = CheckInt();
            Console.WriteLine("Enter Number of Kilometers To?");
            int to = CheckInt();
            List<int> deleteList = new List<int>();

            foreach (var car in listOfCars)
            {
                Car thiscar = listOfCars[car.Key];
                if (thiscar.DrivenKilometers < from || thiscar.DrivenKilometers > to)
                {

                    deleteList.Add(thiscar.MyID);
                }
            }

            foreach (var item in deleteList)
            {
                listOfCars.Remove(item);
            }
            return listOfCars;
        }

        private Dictionary<int, Car> FilterPrice(Dictionary<int, Car> listOfCars)
        {

            Console.WriteLine("Enter Price From?");
            decimal from = CheckDecimal();
            Console.WriteLine("Enter Price To?");
            decimal to = CheckDecimal();
            List<int> deleteList = new List<int>();

            foreach (var car in listOfCars)
            {
                Car thiscar = listOfCars[car.Key];
                if (thiscar.Price < from || thiscar.Price > to)
                {

                    deleteList.Add(thiscar.MyID);
                }
            }

            foreach (var item in deleteList)
            {
                listOfCars.Remove(item);
            }
            return listOfCars;
        }

        private Dictionary<int, Car> FilterDamaged(Dictionary<int, Car> listOfCars)
        {
            bool damaged;
            Console.WriteLine("Do You want to include damaged cars? y/n");
            damaged = CheckBoolean();
            List<int> deleteList = new List<int>();

            foreach (var car in listOfCars)
            {
                Car thiscar = listOfCars[car.Key];
                if (thiscar.IsDamaged != damaged)
                {

                    deleteList.Add(thiscar.MyID);
                }
            }

            foreach (var item in deleteList)
            {
                listOfCars.Remove(item);
            }
            return listOfCars;
        }
        #endregion
    }
}
