using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AnnaBosch_RefugiAnimals.AnnaBosch_RefugiAnimals.Model;

public class Animal
{
    [Required]
    public int AnimalID { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nom { get; set; }
    [Required]
    public Especie Especie { get; set; }
    [Required]
    public bool EsFemella  { get; set; }
    [Required]
    public DateTime DataNaixement { get; set; }
    [Required]
    public DateTime DataEntrada { get; set; }
    [Range(0.1, 100)] // falta if de comprovació
    public double Pes { get; set; }
    public bool EstaAdoptat { get; set; }
    public DateTime? DataAdopcio { get; set; }
    [Precision(8,2)]
    public decimal QuotaAdopcio { get; set; }
    public virtual Xip Xip { get; set; }
    public virtual Adoptant? Adoptant { get; set; }

    public Animal()
    {
        DataEntrada = DateTime.Now;
        EstaAdoptat = false;
        QuotaAdopcio = 0;
    }
    public override string ToString()
    {
        return $"Animal {{AnimalID = {AnimalID}, Nom = {Nom}, Especie = {Especie}}}";
    }
    
}