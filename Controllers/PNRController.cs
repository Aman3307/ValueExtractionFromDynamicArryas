using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ValueExtractionFromDynamicArrays.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PNRController : ControllerBase
    {
        private const string JsonFilePath = @"E:\Aman\ValueExtractionFromDynamicArryas\Defaultsms.json";


        // displays whole array
        //[HttpGet("{queueType}/{queueName}")]
        //public ActionResult<string> GetQueueValues(string queueType, string queueName)
        //{
        //    JObject pnrQueue = null;

        //    try
        //    {
        //        string jsonString = System.IO.File.ReadAllText(JsonFilePath);
        //        pnrQueue = JsonConvert.DeserializeObject<JObject>(jsonString);

        //        Console.WriteLine($"Debug: pnrQueue content: {pnrQueue}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error during deserialization: {ex.Message}");
        //        return BadRequest($"Error during deserialization: {ex.Message}");
        //    }

        //    if (pnrQueue?["ArrayOfPNRQueue"] is JObject arrayOfPNRQueue)
        //    {
        //        var targetQueue = queueType.ToLower() switch
        //        {
        //            "pnr" => arrayOfPNRQueue["PNRQueue"],
        //            "sms" => arrayOfPNRQueue["SMSQueue"],
        //            _ => null
        //        };

        //        if (targetQueue is JArray array)
        //        {
        //            var matchingItems = array
        //                .FirstOrDefault(item => String.Equals(item["QueueName"]?.ToString()?.Trim(), queueName.Trim(), StringComparison.OrdinalIgnoreCase));

        //            if (matchingItems != null)
        //            {
        //                Console.WriteLine($"Debug: matchingItems content: {matchingItems}");
        //                return Ok(matchingItems.ToString());
        //            }
        //        }
        //    }

        //    return NotFound($"Array with name '{queueName}' not found in the {queueType} queue.");
        //}


       //specific row on basis of name
        [HttpGet("{queueType}/{queueName}")]
        public ActionResult<string> GetQueueValues111111(string queueType, string queueName)
        {
            JObject pnrQueue = null;

            try
            {
                string jsonString = System.IO.File.ReadAllText(JsonFilePath);
                pnrQueue = JsonConvert.DeserializeObject<JObject>(jsonString);

                Console.WriteLine($"Debug: pnrQueue content: {pnrQueue}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deserialization: {ex.Message}");
                return BadRequest($"Error during deserialization: {ex.Message}");
            }

            if (pnrQueue?["ArrayOfPNRQueue"] is JObject arrayOfPNRQueue)
            {
                var targetQueue = queueType.ToLower() switch
                {
                    "pnr" => arrayOfPNRQueue["PNRQueue"],
                    "sms" => arrayOfPNRQueue["SMSQueue"],
                    _ => null
                };

                if (targetQueue is JArray array)
                {
                    var matchingItems = array
                        .FirstOrDefault(item => String.Equals(item["QueueName"]?.ToString()?.Trim(), queueName.Trim(), StringComparison.OrdinalIgnoreCase));

                    if (matchingItems != null)
                    {
                        var paymentMode = matchingItems["PaymentMode"];
                        if (paymentMode != null)
                        {
                            Console.WriteLine($"Debug: paymentMode content: {paymentMode}");
                            return Ok(paymentMode.ToString());
                        }
                    }
                }
            }

            return NotFound($"Array with name '{queueName}' not found in the {queueType} queue.");
        }

        //passing the name of property in url
        [HttpGet("{queueType}/{queueName}/{property}")]
        public ActionResult<string> GetQueueValues(string queueType, string queueName, string property)
        {
            JObject pnrQueue = null;

            try
            {
                string jsonString = System.IO.File.ReadAllText(JsonFilePath);
                pnrQueue = JsonConvert.DeserializeObject<JObject>(jsonString);

                // Add logging to inspect the structure of pnrQueue
                Console.WriteLine($"Debug: pnrQueue content: {pnrQueue}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deserialization: {ex.Message}");
                return BadRequest($"Error during deserialization: {ex.Message}");
            }

            if (pnrQueue?["ArrayOfPNRQueue"] is JObject arrayOfPNRQueue)
            {
                var targetQueue = queueType.ToLower() switch
                {
                    "pnr" => arrayOfPNRQueue["PNRQueue"],
                    "sms" => arrayOfPNRQueue["SMSQueue"],
                    _ => null
                };

                if (targetQueue is JArray array)
                {
                    var matchingItems = array
                        .FirstOrDefault(item => String.Equals(item["QueueName"]?.ToString()?.Trim(), queueName.Trim(), StringComparison.OrdinalIgnoreCase));

                    if (matchingItems != null)
                    {
                        var value = matchingItems[property];
                        if (value != null)
                        {
                            Console.WriteLine($"Debug: {property} content: {value}");
                            return Ok(value.ToString());
                        }
                    }
                }
            }

            return NotFound($"Array with name '{queueName}' not found in the {queueType} queue.");
        }



    }
}