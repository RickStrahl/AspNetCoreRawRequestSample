using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Westwind.AspnetCore;

namespace LocalizationSample.Controllers
{    
    public class BodyTypesController : Controller
    {

        [HttpGet]
        [Route("api/bodytypes")]
        [Route("api/BodyTypes/HelloWorld")]
        public string HelloWorld(string name)
        {
            return $"Hello World from Sample BodyTypes Controller, {name}.";
        }

        // GET api/values
        [HttpPost]
        [Route("api/BodyTypes/JsonStringBody")]
        public string JsonPlainBody([FromBody] string content)
        {
            return content;
        }

        [HttpPost]
        [Route("api/BodyTypes/JsonPlainBody")]
        public string PlainStringBody([FromBody] string content)
        {
            return content;
        }




        [HttpPost]
        [Route("api/BodyTypes/ReadStringDataManual")]
        public async Task<string> ReadStringDataManual()
        {
            return await Request.GetRawBodyStringAsync();

            string result;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                result = await reader.ReadToEndAsync();
            return result;
        }

        [HttpPost]
        [Route("api/BodyTypes/ReadBinaryDataManual")]
        public async Task<byte[]> RawBinaryDataManual()
        {
            return await Request.GetRawBodyBytesAsync();

            using (var ms = new MemoryStream(2048))
            {
                await Request.Body.CopyToAsync(ms);
                return  ms.ToArray();  // returns base64 encoded string JSON result
            }
        }


        /// <summary>
        /// For this to work with RAW content request has to have text/plain or no content type
        /// and the RawRequestBodyFormatter has to be installed
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/BodyTypes/RawStringFormatter")]        
        public string RawStringFormatter([FromBody] string rawString)
        {
            return rawString;
        }

        /// <summary>
        /// For this to work with RAW content request has to have application/octet-stream
        /// and the RawRequestBodyFormatter has to be installed
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/BodyTypes/RawBytesFormatter")]
        public byte[] RawBytesFormatter([FromBody] byte[] rawData)
        {
            return rawData;
        }      
    }
}
