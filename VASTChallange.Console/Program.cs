using CsvHelper;
using numl.Math.Metrics;
using numl.Model;
using numl.Unsupervised;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VASTChallange.Core.Entities;

namespace VASTChallange.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new VASTChallangeContext())
            {
                int groups = 4;

                ClusteredCommData[] com = db.CommData
                    .Where(t => t.Timestamp.Day == 6)
                    .GroupBy(t => new { t.Timestamp.Hour, t.Timestamp.Minute, t.Location }, (c, t) => new ClusteredCommData
                    {
                        Hour = c.Hour,
                        Minute = c.Minute,
                        Location = c.Location,
                        Messages = t.Count()
                    })
                    .ToArray();

                Descriptor desc = Descriptor.Create<ClusteredCommData>();
                KMeans kmeans = new KMeans();
                kmeans.Descriptor = desc;

                int[] grouping = kmeans.Generate(com, groups, new EuclidianDistance());

                for (int i = 0; i < grouping.Length; i++)
                {
                    com[i].Cluster = grouping[i];
                }

                using (StreamWriter writer = File.CreateText("Temp.csv"))
                {
                    var csvWriter = new CsvWriter(writer);
                    csvWriter.WriteRecords(com);
                }
            }
        }

        // Define other methods and classes here
        class ClusteredCommData
        {
            [Feature]
            public int Hour { get; set; }

            [Feature]
            public int Minute { get; set; }

            [StringFeature]
            public string Location { get; set; }

            [Feature]
            public int Messages { get; set; }

            public int Cluster { get; set; }
        }
    }
}
