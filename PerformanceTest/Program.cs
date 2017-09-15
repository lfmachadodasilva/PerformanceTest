namespace PerformanceTest
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    static class Program
    {
        private const int NUMBER_OF_ITERACTIONS = 10000000;
        private const int WAINT_TIME_SECONDS = 0 * 1000;

        private static readonly Data[] _dataArray = new Data[NUMBER_OF_ITERACTIONS];
        private static readonly List<Data> _dataList = new List<Data>();
        private static readonly IDictionary<string, Action> _tests = new Dictionary<string, Action>();

        static void Main()
        {
            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                _dataList.Add(new Data { Value = 1 });
                _dataArray[i] = new Data { Value = 1 };
            }

            _tests.Add(nameof(SimpleArray), SimpleArray);
            _tests.Add(nameof(SimpleList), SimpleList);
            _tests.Add(nameof(ListLinq), ListLinq);
            _tests.Add(nameof(ListParallelLinq), ListParallelLinq);
            _tests.Add(nameof(UpdateArray), UpdateArray);
            _tests.Add(nameof(UpdateList), UpdateList);
            _tests.Add(nameof(UpdateListLinq), UpdateListLinq);
            _tests.Add(nameof(UpdateListParallelLinq), UpdateListParallelLinq);
            _tests.Add(nameof(UpdateListParallelLinq2), UpdateListParallelLinq2);
            
            foreach (KeyValuePair<string, Action> test in _tests)
            {
                Console.WriteLine("Test: " + test.Key);

                Stopwatch stopWatch = new Stopwatch();

                stopWatch.Start();
                test.Value();
                stopWatch.Stop();

                Console.WriteLine($"Time: {stopWatch.ElapsedMilliseconds} ms\n");
            }
        }

        private static void UpdateArray()
        {
            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                _dataArray[i].Value = 2;
            }
        }

        private static void UpdateList()
        {
            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                _dataList[i].Value = 2;
            }
        }

        private static void UpdateListLinq()
        {
            _dataList.ForEach(x => x.Value = 2);
        }

        private static void UpdateListParallelLinq()
        {
            Parallel.ForEach(_dataList, x => { x.Value = 2; });
        }

        private static void UpdateListParallelLinq2()
        {
            _dataList.AsParallel().ForAll(x => { x.Value = 2; });
        }

        private static void SimpleArray()
        {
            Data[] test = new Data[NUMBER_OF_ITERACTIONS];

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test[i] = new Data { Value = 1 };
            }

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test[i].Value = 2;
            }
        }

        private static void SimpleList()
        {
            List<Data> test = new List<Data>();

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test.Add(new Data { Value = 1 });
            }

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test[i].Value = 2;
            }
        }

        private static void ListLinq()
        {
            List<Data> test = new List<Data>();

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test.Add(new Data { Value = 1 });
            }

            test.ForEach(x => x.Value = 2);
        }

        private static void ListParallelLinq()
        {
            List<Data> test = new List<Data>();

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test.Add(new Data { Value = 1});
            }

            Parallel.ForEach(test, x => { x.Value = 2; });
        }

        public class TestData
        {
            public string TestName { get; set; }

            public Action TestAction { get; set; }
        }

        public class Data
        {
            public int Value { get; set; }
        }
    }
}
