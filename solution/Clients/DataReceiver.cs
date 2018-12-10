using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Clients
{
    public class DataReceiver
    {
        private static readonly string dataUrl = "http://localhost:8083/clients";
        public static IEnumerable<Client> GetClients()
        {
            // 6. Add a metric which records the url of the http server (Hint - Address metric will do the job)
            // 6. Add a metric which will record how much time it took to get the response with the JSON (Hint - TimeSpan metric is great in this situations)
            // 6. Add a metric that logs exceptions (Hint - There is an Excepton metric)
            var metricsRepository = App.Glue.Metrics.MetricsRepository;
            var metricSystem = metricsRepository.Root.AddChild("Clients Http Requests", "Measuring the response times"); ;

            var timespanMetric = metricSystem.GetOrCreateTimespanMetric("Time", new DOT.Metrics.TimespanMetricOptions());
            var addressMetric = metricSystem.GetOrCreateAddressMetric("URL", new DOT.Metrics.AddressMetricOptions());

            addressMetric.SetValue(dataUrl);

            var exceptionMetric = metricSystem.GetOrCreateExceptionMetric("Request fails", new DOT.Metrics.ExceptionMetricOptions());


            timespanMetric.Start();
            Client[] result = new Client[0];
            try
            {
                var rawResponse = WebRequest.Create(dataUrl).GetResponse();
                timespanMetric.Stop();
                var stream = rawResponse.GetResponseStream();

                using (StreamReader reader = new StreamReader(stream))
                {
                    var response = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<Client[]>(response);
                }
            }
            catch (Exception e)
            {
                exceptionMetric.AddException(e);
            }

            return result.ToList();
        }
    }
}
