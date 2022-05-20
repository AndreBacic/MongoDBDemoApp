using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("AddressBook");

            //InsertName(db);
            //DisplayNames(db);

            //InsertPerson(db);
            //List<PersonModel> recs = DisplayPeople(db);

            //RUD_Person(db, recs);

            //CreateCar(db);
            var car = db.LoadRecords<CarModel>("Cars").First(c => c.YearMade == 2013);

            car = db.LookupRecord<CarModel>("Cars", "Users", car.Id, "OwnerId", "_id", "Owner");

            Console.WriteLine($"Car with id { car.Id }:\nYear - { car.YearMade }\nOwner name - { car.Owner.FirstName }");

            Console.ReadLine();
        }

        private static CarModel CreateCar(MongoCRUD db)
        {
            var person = db.LoadRecords<PersonModel>("Users")[1];

            var car = new CarModel
            {
                OwnerId = person.Id,
                YearMade = 2013
            };

            db.InsertRecord("Cars", car);
            return car;
        }

        private static void RUD_Person(MongoCRUD db, List<PersonModel> recs)
        {
            var oneRec = db.LoadRecordById<PersonModel>("Users", recs[2].Id);

            oneRec.DateOfBirth = new DateTime(2003, 12, 28, 0, 0, 0, DateTimeKind.Utc);
            db.UpsertRecord("Users", oneRec.Id, oneRec);

            db.DeleteRecord<PersonModel>("Users", oneRec.Id);
        }

        private static List<PersonModel> DisplayPeople(MongoCRUD db)
        {
            var recs = db.LoadRecords<PersonModel>("Users");
            foreach (var rec in recs) // for PersonModel display
            {
                Console.WriteLine($"{ rec.Id }: { rec.FirstName } { rec.LastName }");

                if (rec.PrimaryAddress != null)
                {
                    Console.WriteLine(rec.PrimaryAddress.City);
                }
                Console.WriteLine();
            }

            return recs;
        }

        private static List<NameModel> DisplayNames(MongoCRUD db)
        {
            var recs = db.LoadRecords<NameModel>("Users");
            foreach (var rec in recs) // for NameModel display
            {
                Console.WriteLine($"{ rec.FirstName } { rec.LastName }");

                Console.WriteLine();
            }

            return recs;
        }

        private static void InsertName(MongoCRUD db)
        {
            NameModel person = new NameModel
            {
                FirstName = "Andre",
                LastName = "Bacic"
            };
            db.InsertRecord("Users", person);
        }

        private static PersonModel InsertPerson(MongoCRUD db)
        {
            PersonModel person = new PersonModel
            {
                FirstName = "Joe",
                LastName = "Smith",
                PrimaryAddress = new AddressModel
                {
                    StreetAddress = "101 Oak Street",
                    City = "Scranton",
                    State = "PA",
                    ZipCode = "18512"
                }
            };
            db.InsertRecord("Users", person);
            return person;
        }
    }
}
