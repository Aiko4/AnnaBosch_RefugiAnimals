using System.ComponentModel.DataAnnotations;
using AnnaBosch_RefugiAnimals.AnnaBosch_RefugiAnimals.Model;
using Microsoft.EntityFrameworkCore;


[Index(nameof(Codi), IsUnique = true)]
public class Xip
{
    public int XipID { get; set; }
    [Required]
    [StringLength(15, MinimumLength = 15)]
    public string Codi { get; set; }
    [Required]
    public DateTime DataImplantacion { get; set; }
    public virtual Animal? Animal { get; set; }

    public Xip()
    {
        DataImplantacion = DateTime.Now;
    }
    
    public override string ToString()
    {
        return $"Xip {{XipID = {XipID}, Codi = {Codi}}}";
    }
}