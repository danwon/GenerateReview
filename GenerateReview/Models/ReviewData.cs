using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerateReview.Models
{
    public class ReviewData
    {
        public float overall { get; set; }
        public bool verifiled { get; set; }
        public DateTime reviewTime { get; set; }
        public string reviewerID { get; set; }
        public string reviewerName { get; set; }
        public string reviewText { get; set; }
        public string summary { get; set; }
        public string unixReviewTime { get; set; }
        public static List<string> reviewWordList { get; set; }
    }
}
