using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markov;

namespace GenerateReview.Models
{
    public class ReviewList
    {
        public List<Dictionary<string, object>> reviewList;
        public static List<string> reviewTextList;
        public static List<string> sentence;
        public static Dictionary<string, string> sentenseList;
    }
}
