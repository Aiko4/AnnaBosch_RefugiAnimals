using AnnaBosch_RefugiAnimals.AnnaBosch_RefugiAnimals.Model;

namespace RefugiContext.Services;

public class AdoptantService
{
    private readonly Model.RefugiContext _context;

    public AdoptantService(Model.RefugiContext context){
        _context = context;
    }
    
    public List<Adoptant> GetAdoptants()
    {
        return _context.Adoptant
            .ToList();
    }

    public bool Adoptar(string dni, string codiXip)
    {
        Adoptant? adoptant = _context.Adoptant.Find(dni);
        if (adoptant == null) { Console.WriteLine("L'adoptant no existeix"); return false; }

        Animal? animal = _context.Animal.Find(codiXip);
        if (animal == null) { Console.WriteLine("L'animal no existeix"); return false; }
        if (animal.EstaAdoptat) { Console.WriteLine("L'animal ja esta adoptat"); return false; }
        if (animal.Especie == Especie.GOS && !adoptant.TeJardi) { Console.WriteLine("L'animal es un gos i l'adoptant no te jardí"); return false; }
        adoptant.Animals.Add(animal);
        animal.EstaAdoptat = true;
        animal.DataAdopcio = DateTime.Now;
        
        _context.SaveChanges();
        return true;
    }

    public bool Retornar(string codiXip)
    {
        Animal? animal = _context.Animal.Find(codiXip);
        if (animal == null) { Console.WriteLine("L'animal no existeix"); return false; }
        if (!animal.EstaAdoptat) { Console.WriteLine("L'animal no esta adoptat"); return false; }

        Adoptant? anterior = animal.Adoptant;
        animal.Adoptant = null; {Console.WriteLine($"{animal.Nom} ha estat retornat al refugi per {anterior?.Nom}");}
        animal.EstaAdoptat = false;
        animal.DataAdopcio = null;
        animal.DataEntrada = DateTime.Now;
        _context.SaveChanges();
        return true;
    }
}