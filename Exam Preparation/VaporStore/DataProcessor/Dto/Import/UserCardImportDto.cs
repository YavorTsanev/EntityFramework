﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class UserCardImportDto
    {
        [Required, RegularExpression(@"^[A-Z][a-z]+ [A-Z][a-z]+$")]
        public string FullName { get; set; }

        [Required, MinLength(3), MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }


        [Range(3, 103)]
        public int Age { get; set; }

        [MinLength(1)]
        public CardImportDto[] Cards { get; set; }
    }

    public class CardImportDto
    {
        [Required, RegularExpression(@"^[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}$")]
        public string Number { get; set; }

        [Required, RegularExpression(@"^[0-9]{3}$")]
        public string CVC { get; set; }

        [Required]
        public CardType? Type { get; set; }
    }

}
