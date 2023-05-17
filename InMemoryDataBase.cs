using Bogus;
using Exam_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_2
{
    public static class InMemoryDataBase
    {
        public static List<User> Users { get; private set; }
        public static List<Car> Cars { get; private set; }
        public static List<Visit> Visits { get; private set; }

        public static void Create()
        {
            SetStaticUserData();
            SetStaticCarData();
            RelateUsersAndCars();
            SetStaticVisitData();
        }

        private static List<User> SetStaticUserData()
        {
            if (Users.Count() != 0)
                return Users;

            int count = 10;
            var users = new List<User>();
            CreateUser(0, true);
            CreateUser(1, true);
            CreateUser(2, true);
            for (int i = 3; i < count; i++)
            {
                users.Add(CreateUser(i));
            }

            Users = users;
            return users;
        }

        private static User CreateUser(int i, bool isMechanic = false)
        {
            var faker = new Faker("en");
            return new User() { Id = i + 1, Username = faker.Internet.UserName(), Password = faker.Internet.Password(), IsMechanic = isMechanic};
        }

        private static List<Car> SetStaticCarData()
        {
            int count = 10;
            var users = new List<Car>();
            CreateCar(0);
            CreateCar(1);
            CreateCar(2);
            for (int i = 3; i < count; i++)
            {
                users.Add(CreateCar(i));
            }

            return users;
        }

        private static Car CreateCar(int i) 
        {
            var faker = new Faker("en");
            var car = new Car()
            { 
                Id = i + 1, 
                Model = faker.Random.String(7)
            };
            return car;
        }

        private static void RelateUsersAndCars()
        {
            Faker faker = new Faker("en");
            for (int i = 0; i < Users.Count(); i++)
            {
                if (Users[i].IsMechanic)
                    continue;
                Users[i].Cars = faker.PickRandom<Car>(Cars, faker.Random.Int(1,2)).ToList();
            }

            for (int i = 0; i < Cars.Count(); i++)
            {
                Cars[i].MechanicId = faker.PickRandom<User>(Users.Where(u => u.IsMechanic)).Id;
            }
        }

        private static void SetStaticVisitData()
        {
            Faker faker = new Faker("en");
            int j = 0;
            for (int i = 0; i < Cars.Count(); i++)
            {
                Cars[i].VisitIds = new List<int>{CreateVisit(Cars[i].Id, j).Id};
                j++;
            }
        }

        private static Visit CreateVisit(int carId, int index)
        {            
            long ticks;
            if(Visits.Count()==0)
                ticks = DateTime.UtcNow.Ticks;
            else
                ticks = (new DateTime(Visits.Last().Ticks)).AddHours(1).Ticks;

            return new Visit() { Id = index , CarId = carId, Ticks = ticks};
        }
    }
}