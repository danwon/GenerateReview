using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenerateReview.Models;
using Newtonsoft.Json;

namespace GenerateReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : ControllerBase
    {
        [HttpGet]
        public string generate()
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 5);
            var sent = ReviewList.sentence[randomNumber];
            var reviewResult = new ReviewResult
            {
                overallNew = randomNumber.ToString(),
                reviewTextNew = sent
            };
            return JsonConvert.SerializeObject(reviewResult);

        }
    }
}
