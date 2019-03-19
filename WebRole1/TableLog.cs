using Microsoft.Azure.CosmosDB.Table;
using Microsoft.Azure.Storage;
using System;
using System.IO;

namespace TableLog
{
    public class Log
    {
        static bool logInit = false;

        static string path = @"C:\log.txt";
        static StreamWriter sw;
        static CloudTable table;

        static public void Trace(string message)
        {
            if (logInit == false)
            {
                sw = File.AppendText(path);

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("<ConnectionString>");

                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                table = tableClient.GetTableReference("cslogs");
                table.CreateIfNotExists();
                logInit = true;
            }

            // Write log to Table
            LogEntity l = new LogEntity("cs1", String.Format("{0:o}", DateTime.UtcNow));
            l.Message = message;

            TableOperation insertOperation = TableOperation.Insert(l);
            table.Execute(insertOperation);

            // Write log to local
            string logStr = String.Format("{0:o}", DateTime.UtcNow) + " " + message;
            sw.WriteLine(logStr);
            sw.Flush();
        }
    }

    public class LogEntity : TableEntity
    {
        public LogEntity(string partition, string row)
        {
            this.PartitionKey = partition;
            this.RowKey = row;
        }

        public LogEntity() { }
        public string Message { get; set; }
    }
}


