﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.DTOs.Page
{
    public class DeletePageDto
    {
        public Guid PageId { get; set; }
    }
}
