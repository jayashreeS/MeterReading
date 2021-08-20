using MeterReaderWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MeterReaderWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            try
            {
                string meterReadingFile = "C:\\Users\\Rakesh Jayashree\\Source\\Repos\\MeterReaderWebAPI\\MeterReaderWebAPI\\Files\\Meter_Reading.csv";// "~\\MeterReaderWebAPI\\Files\\Meter_Reading.csv";
                DataTable DT_meterReadingFile = ReadCsvFile(meterReadingFile);
                int successCount = 0;
                int failureCount = 0;

                using (MeterReaderDBContext dbContext = new MeterReaderDBContext())
                {
                    ObjectParameter obj_successCount = new ObjectParameter("@ReadingSuccessCount", typeof(int));
                    ObjectParameter obj_failureCount = new ObjectParameter("@ReadingFailureCount", typeof(int));
                    dbContext.spInsertMeterReading(DT_meterReadingFile, obj_successCount, obj_failureCount);

                    successCount = Convert.ToInt32(obj_successCount.Value);
                    failureCount = Convert.ToInt32(obj_failureCount.Value);
                }
            }
            catch (Exception ex)
            {
                string logFilePath = @"C:\Users\Rakesh Jayashree\Source\Repos\MeterReaderWebAPI\MeterReaderWebAPI\Log\Log.txt";


                if (File.Exists(logFilePath))
                {
                    File.AppendAllText(logFilePath, ex.Message);
                }
                else
                {
                    File.Create(logFilePath);
                    File.AppendAllText(logFilePath, ex.Message);
                }
                throw new Exception("There is a error while impporting.Please contact support");

            }
        }

        private DataTable ReadCsvFile(string meterReadingFile)
        {
            DataTable dtCsv = new DataTable();
            string Fulltext;


            using (StreamReader sr = new StreamReader(meterReadingFile))
            {
                while (!sr.EndOfStream)
                {
                    Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    dtCsv.Columns.Add(rowValues[j]); //add headers  
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    if (string.IsNullOrEmpty(rowValues[k]))
                                        dr[k] = string.Empty;
                                    else
                                        dr[k] = rowValues[k].ToString();
                                }
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
                }
            }

            return dtCsv;
        }


        //private void readTextFile()
        //    {
        //        try
        //        {
        //            string meterReadingFile = "~\\MeterReaderWebAPI\\Files\\Meter_Reading.csv";
        //            var reader = new StreamReader(File.OpenRead(meterReadingFile));
        //            var listA = new List<String>();

        //            while (!reader.EndOfStream)
        //            {
        //                var line = reader.ReadLine();
        //                var values = line.Split(',');
        //                listA.Add(values[0]);

        //            }
        //            var firstlistA = listA.ToArray();
        //        }
        //        catch (Exception ex)
        //        {
        //            string logFilePath = "~\\MeterReaderWebAPI\\MeterReaderWebAPI\\Log\\Log.txt";
        //            if (File.Exists(logFilePath))
        //            {
        //                File.AppendAllText(logFilePath, ex.Message);
        //            }
        //            else
        //            {
        //                File.Create(logFilePath);
        //                File.AppendAllText(logFilePath, ex.Message);
        //            }
        //            throw new Exception("There is a error while impporting.Please contact support");

        //        }


        //    }

        private bool checkReadingExists(Array List)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
