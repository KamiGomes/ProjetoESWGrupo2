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
        public int AnimalID {get;set;}

        [StringLength(256, ErrorMessage = "Nome não pode ter mais que 256 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Nome em falta!")]
        [Display(Prompt = "Examplo: Jack", Name = "Nome")]
        public String Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Por favor preencha este campo!")]
        [Display(Name = "Desinfestação")]
        public bool Disinfection { get; set; }

        [Required(ErrorMessage = "Por favor preencha este campo!")]
        [Display(Name = "Castrado")]
        public bool Neutered { get; set; }

        [StringLength(2000, ErrorMessage = "Descrição não pode ter mais que 2000 caracteres!", MinimumLength = 1)]
        [Display(Prompt = "Faça uma breve descrição sobre o animal!", Name = "Descrição")]
        public string Description{ get;set;}

        [Display(Name = "Fotografia")]
        public IFormFile Picture { get; set; }

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

        [NotMapped]
        [Display( Name = "Idade")]
        public String Age { get; set; }

        /*
        [DataType(DataType.ImageUrl)]
        public String foto{get;set;}
         */
    }
}