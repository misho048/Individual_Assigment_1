# Individual_Assigment_1
Car Bazaar

Getting Started
This app was created using .NET Framework 4.6.1 and is requeired to be installed to run the app.

There are 4 Classed in the program:

Class Car:       Describe car and its properties (ID, production Year,driven Kilometers, brand, type Of Car, price,  place Of Sell,number Of Doors,check if the car is damaged, fuel)

Class CarFactory:Got 2 methods for a car creation, one load from file and second to add car from keyborad, this class insure that ID staus unique

Class CarBazaar: Got _mapOfCars which is dictionary where key is ID and value are Cars created by CarFactory
                 There are methods for Loading from a file and saving to txt file
                 Add Edit and Delete cars from keyboard in app by ID
                 All inputs are checked if they are valid
                 Also Filters are presented
                  
 Class Main:     Create CarBazaar and create Main Menu where you can work.
                  
