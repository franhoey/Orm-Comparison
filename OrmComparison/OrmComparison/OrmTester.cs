using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OrmComparison
{
    public class OrmTester
    {
        private const int PAD_NAME_COLUMN_TO = 25;
        private const int TEST_ITERATION_COUNT = 5000;
        private readonly List<IDataAccess> _orms;
        private readonly ResultOutput _resultOutput;

        public OrmTester(List<IDataAccess> orms, ResultOutput resultOutput)
        {
            _orms = orms;
            _resultOutput = resultOutput;
        }

        public void RunTest()
        {
            _resultOutput.WriteLine($"{"Orm".PadRight(PAD_NAME_COLUMN_TO)}\tSelectOne       \tSelectAll       \tRunNonQuery");
            foreach (var orm in _orms)
            {
                Test(orm);
            }
        }

        private void Test(IDataAccess orm)
        {
            _resultOutput.Write(orm.Name.PadRight(PAD_NAME_COLUMN_TO));
            var random = new Random(1);
            Test(() =>
            {
                var data = orm.SelectOne(random.Next(1, 300));
            });

            Test(() =>
            {
                var data = orm.SelectAll();
            });

            Test(() =>
            {
                var data = orm.RunNonQuery();
            });
            _resultOutput.WriteLine();
        }

        private void Test(Action action)
        {
            Warmup(action);

            var sw = new Stopwatch();
            sw.Start();

            RunIt(action, TEST_ITERATION_COUNT);

            sw.Stop();
            _resultOutput.Write($"\t{sw.Elapsed}");
        }

        private void Warmup(Action action)
        {
            RunIt(action, TEST_ITERATION_COUNT/10);
        }

        private void RunIt(Action action, int iterations)
        {
            for (var i = 0; i < iterations; i++)
            {
                action();
            }
        }
    }
}