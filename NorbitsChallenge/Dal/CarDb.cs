using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Bll;

namespace NorbitsChallenge.Dal
{
    public class CarDb
    {
        private readonly IConfiguration _config;

        public CarDb(IConfiguration config)
        {
            _config = config;
        }
        //Method for retreiving all info about a single car
        public Car getCar(string licensePlate) {
            Car car = new Car();
            var connectionString = _config.GetSection("ConnectionString").Value;
            using (var connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text }) {
                    command.CommandText = $"SELECT * FROM car WHERE LicensePlate = '{licensePlate}'";
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            car.LicensePlate = reader["LicensePlate"].ToString();
                            car.TireCount = (int)reader["TireCount"];
                            car.CompanyId = (int)reader["CompanyId"];
                            car.Brand = reader["Brand"].ToString();
                            car.Model = reader["Model"].ToString();
                            car.Description = reader["Description"].ToString();
                            
                        }
                    }
                }
                connection.Close();
            }
            return car;

        }
        //Method for retrieving info about all the cars in the database
        public List<Car> getCars(int companyId) {
            var cars = new List<Car>();
            var connectionString = _config.GetSection("ConnectionString").Value;
            using (var connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text}){
                    command.CommandText = $"select * from car where CompanyId = {companyId}";
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var car = new Car();
                            car.LicensePlate = reader["Licenseplate"].ToString();
                            car.TireCount = (int)reader["TireCount"];
                            car.Brand = reader["Brand"].ToString();
                            car.Model = reader["Model"].ToString();
                            car.Description = reader["Description"].ToString();
                            cars.Add(car);
                        }
                    }
                }
                connection.Close();
            }
            return cars;
        }
        //Method for adding a single car to the database
        public void addCarToDB(string newLicense, int newTireCount, int companyId, string model, string brand, string description) {
            string licenseCheck = "";
            if(newLicense.Length == 7) {
                if (!carExists(newLicense)) { 
                    var connectionString = _config.GetSection("ConnectionString").Value;
                    using (var connection = new SqlConnection(connectionString)) {
                        connection.Open();
                        using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text }) {
                                    command.CommandText = $"INSERT INTO car (CompanyId,Licenseplate,TireCount, Model, Brand, Description) VALUES({companyId},'{newLicense}',{newTireCount},'{model}','{brand}','{description}')";
                                    command.ExecuteNonQuery();                        
                        }
                        connection.Close();
                    }
                }
            }
        }

        //Method which checks if a car exists in the database
        public bool carExists(string licensePlate) {
            bool licenseCheck = false;
            string licenseFromDB = "";
            var connectionString = _config.GetSection("ConnectionString").Value;
            using (var connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text }) {
                    command.CommandText = $"SELECT LicensePlate FROM car WHERE LicensePlate = '{licensePlate}'";
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read())
                        {
                            licenseFromDB = reader["LicensePlate"].ToString();
                        }
                        if (licenseFromDB.Equals(licensePlate)) {
                            licenseCheck = true;
                        }
                    }
                }
                connection.Close();
            }
            return licenseCheck;
        }
        //Method for removing a single car from the database
        public void removeCarFromDB(string carLicense) {
            var connectionString = _config.GetSection("ConnectionString").Value;
            using (var connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text }) {
                    command.CommandText = $"DELETE FROM car WHERE LicensePlate = '{carLicense}'";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        //Method for updating the info about a single car in the database
        public void updateCar(int newTireCount, string carLicense, string model, string brand, string description) {
            var connectionString = _config.GetSection("ConnectionString").Value;
            using (var connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text }) {
                    command.CommandText = $"UPDATE car SET TireCount={newTireCount}, Model='{model}',Brand='{brand}',Description='{description}' WHERE LicensePlate='{carLicense}'";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
