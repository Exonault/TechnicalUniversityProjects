﻿using System;
using System.Threading;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            new MergeSortThreads().Run();
        }
    }
}