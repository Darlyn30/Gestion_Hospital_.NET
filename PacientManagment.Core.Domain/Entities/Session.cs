﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacientManagment.Core.Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Role {  get; set; }
        public DateTime LastSession {  get; set; }

    }
}
