using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendapi.Data.Dto.features.Command
{
    public class CommandReadDto
    {

        public int Id { get; set; }
        public string HowTo { get; set; }
        public string Line { get; set; }
    }
}