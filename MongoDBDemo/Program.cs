using System;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("AddressBook");

            //PersonModel person = new PersonModel
            //{
            //    FirstName = "Joe",
            //    LastName = "Smith",
            //    PrimaryAddress = new AddressModel
            //    {
            //        StreetAddress = "101 Oak Street",
            //        City = "Scranton",
            //        State = "PA",
            //        ZipCode = "18512"
            //    }
            //};
            //db.InsertRecord("Users", person);


            //var recs = db.LoadRecords<NameModel>("Users");
            var recs = db.LoadRecords<PersonModel>("Users");

            //foreach (var rec in recs) // for NameModel display
            //{
            //    Console.WriteLine($"{ rec.FirstName } { rec.LastName }");

            //    Console.WriteLine();
            //}

            foreach (var rec in recs) // for PersonModel display
            {
                Console.WriteLine($"{ rec.Id }: { rec.FirstName } { rec.LastName }");

                if (rec.PrimaryAddress != null)
                {
                    Console.WriteLine(rec.PrimaryAddress.City);
                }
                Console.WriteLine();
            }

            var oneRec = db.LoadRecordById<PersonModel>("Users", recs[2].Id);

            oneRec.DateOfBirth = new DateTime(1982, 10, 31, 0, 0, 0, DateTimeKind.Utc);
            db.UpsertRecord("Users", oneRec.Id, oneRec);

            //db.DeleteRecord<PersonModel>("Users", oneRec.Id);

            Console.ReadLine();
        }
    }
}
