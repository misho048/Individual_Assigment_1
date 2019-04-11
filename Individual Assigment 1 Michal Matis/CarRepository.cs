using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace Individual_Assigment_1_Michal_Matis
{
    class CarRepository
    {
        private const string CONNECTIONSTRING = @"Server = TRANSFORMER1\SQLEXPRESS2016; Database = CarBazaar; Trusted_Connection=True";

        public void AddNewCar(CarModel car)
        {
            string addQuery = @"insert into [dbo].[Car] (ID,Brand,TypeOfCar,ProductionYear,DrivenKilometers,Price,
						                NumberOfDoors,PlaceOfSell,IsDamaged,FuelID)
                                Values (@ID,@Brand,@TypeOfCar,@ProductionYear,@DrivenKilometers,
                                        @Price,@NumberOfDoors,@PlaceOfSell,@IsDamaged,@FuelID)";


            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTIONSTRING))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand command = new SqlCommand(addQuery, connection);
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = car.MyID;
                        command.Parameters.Add("@Brand", SqlDbType.NVarChar).Value = car.Brand;
                        command.Parameters.Add("@TypeOfCar", SqlDbType.NVarChar).Value = car.TypeOfCar;
                        command.Parameters.Add("@ProductionYear", SqlDbType.Int).Value = car.ProductionYear;
                        command.Parameters.Add("@DrivenKilometers", SqlDbType.Int).Value = car.DrivenKilometers;
                        command.Parameters.Add("@Price", SqlDbType.Money).Value = car.Price;
                        command.Parameters.Add("@NumberOfDoors", SqlDbType.Int).Value = car.NumberOfDoors;
                        command.Parameters.Add("@PlaceOfSell", SqlDbType.NVarChar).Value = car.PlaceOfSell;
                        command.Parameters.Add("@IsDamaged", SqlDbType.Bit).Value = (car.IsDamaged) ? 1 : 0;
                        command.Parameters.Add("@FuelID", SqlDbType.Int).Value = Convert.ToInt32(car.Fuel);

                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error happend during query \n {ex.Message}");
                    }


                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"Error happend during connecting \n {e.Message}");
            }

        }

        public Dictionary<int, CarModel> GetAll()
        {
            Dictionary<int, CarModel> mapOfCars = new Dictionary<int, CarModel>(); 
            string selectAllQuery = @"select car.ID,Brand,TypeOfCar,ProductionYear,DrivenKilometers,Price,
                                            NumberOfDoors,PlaceOfSell,IsDamaged,FuelName
                                      from [dbo].[Car] as car
                                      left join [dbo].[FuelType] as fuel on fuel.ID= car.FuelID";


            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTIONSTRING))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(selectAllQuery, connection);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CarModel car = new CarModel();
                                car.MyID = reader.GetInt32(0);
                                car.Brand = reader.GetString(1);
                                car.TypeOfCar = reader.GetString(2);
                                car.ProductionYear = reader.GetInt32(3);
                                car.DrivenKilometers = reader.GetInt32(4);
                                car.Price = reader.GetDecimal(5);
                                car.NumberOfDoors = reader.GetInt32(6);
                                car.PlaceOfSell = reader.GetString(7);
                                car.IsDamaged = reader.GetBoolean(8);                                
                                Enum.TryParse(reader.GetString(9), out Program.FuelTypes something);
                                car.Fuel = something;
                                mapOfCars[car.MyID] = car;

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error happend during query \n {ex.Message}");
                        return mapOfCars;
                    }


                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"Error happend during connecting \n {e.Message}");
                return mapOfCars;
            }
            return mapOfCars;

        }

        public CarModel GetCarByID (int id)
        {
            Dictionary<int, CarModel> mojSlovnicek = new Dictionary<int, CarModel>(GetAll());
            if (!mojSlovnicek.Keys.Contains(id))
            {
                Console.WriteLine("NeplatneID");
                return null;
            }
            else
            {
                return mojSlovnicek[id];
            }
        }

        public void DeleteCar(int id)
        {
            string deleteQuery = @"DELETE FROM [dbo].[Car] WHERE ID=@id;";


            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTIONSTRING))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand command = new SqlCommand(deleteQuery, connection);
                        command.Parameters.Add("@ID", SqlDbType.Int).Value =id;


                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error happend during query \n {ex.Message}");
                    }


                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"Error happend during connecting \n {e.Message}");
            }

        }
       
        public int GetMaxID()
        {
            string getMaxID = @"select max(ID) from[dbo].[Car]";


            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTIONSTRING))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand command = new SqlCommand(getMaxID, connection);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error happend during query \n {ex.Message}");
                        return -1;
                    }


                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"Error happend during connecting \n {e.Message}");
                return -1;
            }



        }

    }
}
