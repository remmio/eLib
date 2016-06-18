using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.ServiceProcess;
using System.Threading;

namespace eLib.Utils
{
    public static class SqlServerHelper
    {

        public static void StartSqlBrowserService(List<string> activeMachines)
        {
            var myService = new ServiceController {ServiceName = "SQLBrowser"};

            foreach (var machine in activeMachines)
            {
                try
                {
                    myService.MachineName = machine;
                    var svcStatus = myService.Status.ToString();
                    switch (svcStatus)
                    {
                        case "ContinuePending":
                            Console.WriteLine("Service is attempting to continue.");
                            break;

                        case "Paused":
                            Console.WriteLine("Service is paused.");
                            Console.WriteLine("Attempting to continue the service.");
                            myService.Continue();
                            break;

                        case "PausePending":
                            Console.WriteLine("Service is pausing.");
                            Thread.Sleep(5000);
                            try
                            {
                                Console.WriteLine("Attempting to continue the service.");
                                myService.Start();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;

                        case "Running":
                            Console.WriteLine("Service is already running.");
                            break;

                        case "StartPending":
                            Console.WriteLine("Service is starting.");
                            break;

                        case "Stopped":
                            Console.WriteLine("Service is stopped.");
                            Console.WriteLine("Attempting to start service.");
                            myService.Start();
                            break;

                        case "StopPending":
                            Console.WriteLine("Service is stopping.");
                            Thread.Sleep(5000);
                            try
                            {
                                Console.WriteLine("Attempting to restart service.");
                                myService.Start();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public static void SqlTestInfo()
        {
            var instance = SqlDataSourceEnumerator.Instance;
            var table = instance.GetDataSources();
            DisplayData(table);
        }


        private static void DisplayData(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn dataColumn in table.Columns)
                    Console.WriteLine("{0} = {1}", dataColumn.ColumnName, row[dataColumn]);
                Console.WriteLine();
            }
        }

    }
}
