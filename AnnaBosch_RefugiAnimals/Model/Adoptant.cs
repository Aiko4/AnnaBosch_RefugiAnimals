using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AnnaBosch_RefugiAnimals.AnnaBosch_RefugiAnimals.Model;

[Index(nameof(DNI), IsUnique = true)]
public class Adoptant
{
    public int AdoptantId { get; set; }

    [Required]
    [MaxLength(9)]
    public string DNI { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nom { get; set; }
    [Required]
    [MaxLength(100)]
    public string Cognoms { get; set; }
    [Required]
    public bool TeJardi { get; set; }
    [Required]
    public DateTime DataRegistre { get; set; }
    public virtual ICollection<Animal> Animals { get; set; }
    
    public Adoptant()
    {
        DataRegistre = DateTime.Now;
        Animals = new HashSet<Animal>();
    }

    public override string ToString()
    {
        return $"Adoptant {{AdoptantId = {AdoptantId}, DNI = {DNI}, Nom = {Nom}, Cognoms = {Cognoms}, Te Jardi = {TeJardi}, Data Registre = {DataRegistre}}}";
    }
}