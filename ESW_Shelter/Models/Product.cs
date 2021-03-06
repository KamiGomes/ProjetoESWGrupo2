﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESW_Shelter.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [StringLength(256, ErrorMessage = "Nome não pode ter mais que 256 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Nome em falta!")]
        [Display(Prompt = "Examplo: Secos Continente", Name = "Nome")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Stock atual em falta!")]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor, insira só números inteiros!")]
        [Display(Prompt = "Examplo: 10", Name = "Stock atual")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Stock semanal em falta!")]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor, insira só números inteiros!")]
        [Display(Prompt = "Examplo: 10", Name = "Stock semanal necessário")]
        public int WeekStock { get; set; }

        [Required(ErrorMessage = "Stock mensal em falta!")]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor, insira só números inteiros!")]
        [Display(Prompt = "Examplo: 10", Name = "Stock mensal necessário")]
        public int MonthStock { get; set; }

        [ForeignKey("AnimalType")]
        [Required(ErrorMessage = "Escolha um animal para este alimento!")]
        [Display(Prompt = "Examplo: Cão", Name = "Para animal")]
        public int AnimalTypeFK { get; set; }

        [ForeignKey("ProductType")]
        [Required(ErrorMessage = "Escolha um tipo para este produto!")]
        [Display(Prompt = "Examplo: Comida", Name = "Tipo de produto")]
        public int ProductTypeFK { get; set; }


        [NotMapped]
        public String ProductTypeName { get; set; }

        [NotMapped]
        public String AnimaltypeName { get; set; }

        [NotMapped]
        public bool Checked { get; set; }
    }
}
