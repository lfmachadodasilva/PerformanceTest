/* 
*   Project: PerformanceTestCollections
*   Author: Luiz Felipe Machado da Silva
*   Github: http://github.com/lfmachadodasilva/PerformanceTest
*/

namespace PerformanceTestCollections
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    static class Program
    {
        private const int NUMBER_OF_ITERACTIONS = int.MaxValue;

        private const int FROM_VALUE = 1;
        private const int TO_VALUE = 2;

        private static readonly Data[] _dataArray = new Data[NUMBER_OF_ITERACTIONS];
        private static readonly List<Data> _dataList = new List<Data>();
        private static readonly IDictionary<string, Action> _tests = new Dictionary<string, Action>();

        static void Main()
        {
            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                _dataList.Add(new Data { Value = FROM_VALUE });
                _dataArray[i] = new Data { Value = FROM_VALUE };
            }

            _tests.Add(nameof(CreateArrayAndChangeAllValues), CreateArrayAndChangeAllValues);
            _tests.Add(nameof(CreateListAndChangeAllValues), CreateListAndChangeAllValues);
            _tests.Add(nameof(CreateListAndChangeAllValuesLinq), CreateListAndChangeAllValuesLinq);
            _tests.Add(nameof(InitListParallelLinq), InitListParallelLinq);
            _tests.Add(nameof(UpdateArrayUsingAFor), UpdateArrayUsingAFor);
            _tests.Add(nameof(UpdateListUsingAFor), UpdateListUsingAFor);
            _tests.Add(nameof(UpdateListLinq), UpdateListLinq);
            _tests.Add(nameof(UpdateValueListParallelLinq), UpdateValueListParallelLinq);
            _tests.Add(nameof(UpdateValueListParallelLinq2), UpdateValueListParallelLinq2);
            
            foreach (KeyValuePair<string, Action> test in _tests)
            {
                for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
                {
                    // reset values
                    _dataList[i].Value = FROM_VALUE;
                    _dataArray[i].Value = FROM_VALUE;
                }

                Console.WriteLine("Test: " + test.Key);

                Stopwatch stopWatch = new Stopwatch();

                stopWatch.Start();
                test.Value();
                stopWatch.Stop();

                Console.WriteLine($"Time: {stopWatch.ElapsedMilliseconds} ms\n");
            }
        }

        private static void UpdateArrayUsingAFor()
        {
            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                _dataArray[i].Value = TO_VALUE;
            }
        }

        private static void UpdateListUsingAFor()
        {
            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                _dataList[i].Value = TO_VALUE;
            }
        }

        private static void UpdateListLinq()
        {
            _dataList.ForEach(x => x.Value = 2);
        }

        private static void UpdateValueListParallelLinq()
        {
            Parallel.ForEach(_dataList, x => { x.Value = 2; });
        }

        private static void UpdateValueListParallelLinq2()
        {
            _dataList.AsParallel().ForAll(x => { x.Value = 2; });
        }

        private static void CreateArrayAndChangeAllValues()
        {
            Data[] test = new Data[NUMBER_OF_ITERACTIONS];

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test[i] = new Data { Value = FROM_VALUE };
            }

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test[i].Value = TO_VALUE;
            }
        }

        private static void CreateListAndChangeAllValues()
        {
            List<Data> test = new List<Data>();

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test.Add(new Data { Value = FROM_VALUE });
            }

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test[i].Value = TO_VALUE;
            }
        }

        private static void CreateListAndChangeAllValuesLinq()
        {
            List<Data> test = new List<Data>();

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test.Add(new Data { Value = FROM_VALUE });
            }

            test.ForEach(x => x.Value = TO_VALUE);
        }

        private static void InitListParallelLinq()
        {
            List<Data> test = new List<Data>();

            for (int i = 0; i < NUMBER_OF_ITERACTIONS; i++)
            {
                test.Add(new Data { Value = FROM_VALUE });
            }

            Parallel.ForEach(test, x => { x.Value = TO_VALUE; });
        }

        public class Data
        {
            public int Value { get; set; }
        }
    }
}
