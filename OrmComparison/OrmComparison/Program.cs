using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrmComparison.Dapper;
using OrmComparison.Debonair;
using OrmComparison.EntityFramework;
using OrmComparison.EntityFrameworkCore;
using OrmComparison.LinqToSql;
using OrmComparison.MicroLite;
using OrmComparison.NPoco;
using OrmComparison.OrmLite;
using OrmComparison.PetaPoco;
using OrmComparison.Raw;

namespace OrmComparison
{
    class Program
    {
        static void Main(string[] args)
        {

            var testObjects = new List<IDataAccess>()
            {
                new LinqToSqlDataAccess(),
                new EFDataAccess(),
                new EFCoreDataAccess(),
                new RawDataAccess(),
                new DapperDataAccess(),
                new PetaPocoDataAccess(),
                new NPocoDataAccess(),
                new MicroLiteDataAccess(),
                new DebonairDataAccess(),
                new OrmLiteDataAccess()
            };


            //Console.BufferWidth = 100;
            Console.WriteLine($"Begin test of {testObjects.Count} orms");
            Console.WriteLine();

            using (var resultOutput = new ResultOutput())
            {
                var tester = new OrmTester(testObjects, resultOutput);
                tester.RunTest();
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }
    }
}
