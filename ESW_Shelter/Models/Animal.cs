using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ESW_Shelter.Models
{
    public class Animal
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalId {get;set;}

        [StringLength(256, ErrorMessage = "Nome não pode ter mais que 256 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Nome em falta!")]
        [Display(Prompt = "Examplo: Jack", Name = "Nome")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Idade em falta!")]
        [Display(Name = "Idade")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "Por favor preencha este campo!")]
        [Display(Name = "Desinfestação")]
        public bool Desinfection { get; set; }

        [Required(ErrorMessage = "Por favor preencha este campo!")]
        [Display(Name = "Castrado")]
        public bool Castrated { get; set; }

        [StringLength(1000, ErrorMessage = "Nome não pode ter mais que 1000 caracteres!", MinimumLength = 1)]
        [Display(Prompt = "Faça uma breve descrição sobre o animal!", Name = "Descrição")]
        public string Description{ get;set;}

        [Display(Name = "Fotografia")]
        public Byte Picture { get; set; }

        [ForeignKey("AnimalType")]
        [Required(ErrorMessage = "Escolha um tipo para este Animal!")]
        [Display(Prompt = "Examplo: Cachorro", Name = "Tipo de Animal")]
        public int AnimalTypeFK { get; set; }

        [ForeignKey("AnimalRace")]
        [Required(ErrorMessage = "Escolha uma raça para o animal!")]
        [Display(Prompt = "Examplo: Pitbull", Name = "Raça")]
        public int AnimalRaceFK { get; set; }

        [ForeignKey("Users")]
        public int OwnerFK { get; set; }

        [NotMapped]
        public string OwnerName { get; set; }

        [NotMapped]
        public String AnimaltypeName { get; set; }

        /*
        [DataType(DataType.ImageUrl)]
        public String foto{get;set;}
         */
    }
}