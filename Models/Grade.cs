using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp_Core_Web_API_test.Models {

    public class Grade {

        public int Year { get; set; }
        public bool Semester { get; set; }
        public string Subject { get; set; }
        public double Value { get; set; }
        public byte AV { get; set; }

    }
}
