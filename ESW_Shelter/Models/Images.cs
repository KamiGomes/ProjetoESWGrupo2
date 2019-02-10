
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESW_Shelter.Models
{
    public class Images
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }

        public int AnimalFK { get; set; }

        [StringLength(1000, ErrorMessage = "Nome não pode ter mais que 1000 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Nome em falta!")]
        [Display(Prompt = "Examplo: Nome do Ficheiro", Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Tamanho em falta!")]
        [Display(Prompt = "Examplo: 555kb", Name = "Tamanho")]
        public long Length { get; set; }

        [StringLength(4000, ErrorMessage = "Nome não pode ter mais que 1000 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Nome do Ficheiro em falta!")]
        [Display(Prompt = "Examplo: Nome do Ficheiro", Name = "Nome do Ficheiro")]
        public string FileName { get; set; }

        [StringLength(4000, ErrorMessage = "TIpo de Conteudo não pode ter mais que 1000 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Tipo de conteudo em falta!")]
        [Display(Prompt = "Examplo: Png", Name = "Tipo de Conteudo")]
        public string ContentType { get; set; }

        [StringLength(4000, ErrorMessage = "Conteudo do Dispositivo não pode ter mais que 1000 caracteres!", MinimumLength = 1)]
        [Required(ErrorMessage = "Conteudo do Dispositivo em falta!")]
        [Display(Prompt = "Examplo: Conteudo do Dispositivo", Name = "Conteudo do Dispositivo")]
        public string ContentDisposition { get; set; }

    }
}
