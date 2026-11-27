using AnnaBosch_RefugiAnimals.AnnaBosch_RefugiAnimals.Model;

namespace RefugiContext.Services;

public class AnimalService
{
    private readonly Model.RefugiContext _context;

    public AnimalService(Model.RefugiContext context){
        _context = context;
    }

    public List<Animal> GetDisponibles(Especie especie)
    {
        return _context.Animal
            .Where(a => a.Especie == especie && a.EstaAdoptat == false)
            .OrderBy(a => a.DataEntrada)
            .ToList();
    }
}