using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen.Validators
{
    public class ValidationResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public int Line { get; set; }
    }
}
