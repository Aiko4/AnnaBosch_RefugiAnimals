using AnnaBosch_RefugiAnimals.AnnaBosch_RefugiAnimals.Model;
using RefugiContext.Services;

namespace RefugiContext
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using
                (var
                 ctx = new Model.RefugiContext()) // com el try-with-resources de Java: tanca el context en acabar el bloc
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();

                var luna = new Animal()
                {
                    Nom = "Luna", Especie = Especie.GOS, EsFemella = true, Pes = 22.5,
                    DataNaixement = new DateTime(2020, 3, 14), DataEntrada = new DateTime(2025, 2, 10),
                    Xip = new Xip() { Codi = "724098100000001" }
                };
                var scooby = new Animal()
                {
                    Nom = "Scooby", Especie = Especie.GOS, EsFemella = false, Pes = 31,
                    DataNaixement = new DateTime(2019, 7, 2), DataEntrada = new DateTime(2024, 11, 5),
                    Xip = new Xip() { Codi = "724098100000002" }
                };
                var lassie = new Animal()
                {
                    Nom = "Lassie", Especie = Especie.GOS, EsFemella = true, Pes = 12.8,
                    DataNaixement = new DateTime(2023, 1, 20), DataEntrada = new DateTime(2025, 9, 1),
                    Xip = new Xip() { Codi = "724098100000003" }
                };
                var milu = new Animal()
                {
                    Nom = "Milú", Especie = Especie.GOS, EsFemella = false, Pes = 18,
                    DataNaixement = new DateTime(2021, 5, 9), DataEntrada = new DateTime(2024, 6, 15),
                    Xip = new Xip() { Codi = "724098100000004" }
                };

                // 3 gats
                var felix = new Animal()
                {
                    Nom = "Felix", Especie = Especie.GAT, EsFemella = false, Pes = 3.9,
                    DataNaixement = new DateTime(2022, 4, 11), DataEntrada = new DateTime(2025, 3, 22),
                    Xip = new Xip() { Codi = "724098100000005" }
                };
                var duquessa = new Animal()
                {
                    Nom = "Duquessa", Especie = Especie.GAT, EsFemella = true, Pes = 4.2,
                    DataNaixement = new DateTime(2021, 12, 1), DataEntrada = new DateTime(2024, 12, 30),
                    Xip = new Xip() { Codi = "724098100000006" }
                };
                var garfield = new Animal()
                {
                    Nom = "Garfield", Especie = Especie.GAT, EsFemella = false, Pes = 5.6,
                    DataNaixement = new DateTime(2018, 8, 17), DataEntrada = new DateTime(2025, 7, 7),
                    Xip = new Xip() { Codi = "724098100000007" }
                };

                // 1 conill i 1 au
                var bugs = new Animal()
                {
                    Nom = "Bugs", Especie = Especie.CONILL, EsFemella = false, Pes = 1.8,
                    DataNaixement = new DateTime(2024, 2, 3), DataEntrada = new DateTime(2025, 5, 12),
                    Xip = new Xip() { Codi = "724098100000008" }
                };
                var piolin = new Animal()
                {
                    Nom = "Piolin", Especie = Especie.AU, EsFemella = false, Pes = 0.3,
                    DataNaixement = new DateTime(2023, 9, 25), DataEntrada = new DateTime(2025, 8, 19),
                    Xip = new Xip() { Codi = "724098100000009" }
                };

                // Persistim: un sol Add de l'animal insereix també el seu xip, NO cal ctx.Xip.Add(...)
                ctx.Animal.Add(luna);
                ctx.Animal.Add(scooby);
                ctx.Animal.Add(lassie);
                ctx.Animal.Add(milu);
                ctx.Animal.Add(felix);
                ctx.Animal.Add(duquessa);
                ctx.Animal.Add(garfield);
                ctx.Animal.Add(bugs);
                ctx.Animal.Add(piolin);

                Adoptant adoptant1 = new Adoptant()
                    { DNI = "12345678j", Nom = "Marta", Cognoms = "Puig Soler", TeJardi = true };
                Adoptant adoptant2 = new Adoptant()
                    { DNI = "87654321X", Nom = "Pau", Cognoms = "Ferrer Vidal", TeJardi = false };

                ctx.Adoptant.Add(adoptant1);
                ctx.Adoptant.Add(adoptant2);

                ctx.SaveChanges();

                Console.WriteLine("\n=== ANIMALS I XIPS ===");
                List<Animal> animals = ctx.Animal.ToList();
                foreach (Animal a in animals)
                {
                    Console.WriteLine($"{a} · {a.Xip}");
                }
            }
            
            //Exercici 1 - Obtenir animals disponibles per espècie
            using (var ctx3 = new Model.RefugiContext())
            {
                Console.WriteLine("\n=== EXERCICI 1: LLISTAT D'ANIMALS DISPONIBLES PER ESPÈCIE ===");
                AnimalService animalService = new AnimalService(ctx3);
 
                //Obtenir animals disponibles de la espècie GOS
                List<Animal> animals = animalService.GetDisponibles(Especie.GOS);
                if (animals.Count == 0){
                    Console.WriteLine($"No hi ha animals de la especie GOS");
                }else{
                    Console.WriteLine($"Hi ha {animals.Count} animals de la especie GOS");
                    foreach (var a in animals){
                        Console.WriteLine($"{a}");
                    }
                }
 
                //Obtenir animals disponibles de l'espècie ALTRES
                List<Animal> altres = animalService.GetDisponibles(Especie.ALTRES);
                if (altres.Count == 0){
                    Console.WriteLine("No hi ha animals disponibles de l'espècie ALTRES");
                }
            }

            using (var ctx4 = new Model.RefugiContext())
            {
                ctx4.Adoptant.Add(new Adoptant()
                    { DNI = "12345678Z", Nom = "Marta", Cognoms = "Puig Soler", TeJardi = true });
                ctx4.Adoptant.Add(new Adoptant()
                    { DNI = "87654321X", Nom = "Pau", Cognoms = "Ferrer Vidal", TeJardi = false });
                ctx4.SaveChanges();

                //abans de llegir: buida el que recorda i et força a llegir de la BD
                ctx4.ChangeTracker.Clear();

                AdoptantService adoptantService = new AdoptantService(ctx4);
                Console.WriteLine("\n=== EXERCICI 2: LLISTAT D'ADOPTANTS ===");
                foreach (Adoptant adoptant in adoptantService.GetAdoptants())
                {
                    Console.WriteLine($" > {adoptant} · animals: {adoptant.Animals.Count}");
                    foreach (Animal animal in adoptant.Animals)
                    {
                        Console.WriteLine($"  - {animal.Nom} ({animal.Especie})");
                    }
                }
            }

            //Exercici 3 - Adopció d'animals
            using (var ctx5 = new Model.RefugiContext())
            {
                AdoptantService adoptantService = new AdoptantService(ctx5);
                Console.WriteLine("\n=== EXERCICI 3: ADOPTEM ANIMALS  ===");
                adoptantService.Adoptar("00000000A", "724098100000001");   // l'adoptant no existeix
                adoptantService.Adoptar("12345678Z", "000000000000000");   // l'animal no existeix
                adoptantService.Adoptar("87654321X", "724098100000001");   // l'adoptant no te jardí
                adoptantService.Adoptar("12345678Z", "724098100000001");   // tot correcte, s'adopta
                adoptantService.Adoptar("87654321X", "724098100000001");   // l'animal ja esta adoptat
 
                //abans de llegir: buida el que recorda i et força a llegir de la BD
                ctx5.ChangeTracker.Clear();
 
                //Mostrem llistat d'adoptants
                Console.WriteLine("\nMostrem llistat d'adoptants actualitzada:");
                foreach (Adoptant adoptant in adoptantService.GetAdoptants()){
                    Console.WriteLine($" > {adoptant} · animals: {adoptant.Animals.Count}");
                    foreach (Animal animal in adoptant.Animals){
                        Console.WriteLine($"  - {animal.Nom} ({animal.Especie})");
                    }
                }
            }
            
            //Exercici 4 - Retornem animals al refugi
            using (var ctx6 = new Model.RefugiContext())
            {
                Console.WriteLine("\n=== EXERCICI 4: RETORN D'ANIMALS ===");
                AdoptantService adoptantService = new AdoptantService(ctx6);
                adoptantService.Retornar("724098100000001");    //es retorna un adoptat
                adoptantService.Retornar("724098100000007");    //es retorna un NO adoptat
                adoptantService.Retornar("000000000000000");    //es retorna que no existeix
 
                //Mostrem llistat d'adoptants
                Console.WriteLine("\nMostrem llistat d'adoptants actualitzada:");
                foreach (Adoptant adoptant in adoptantService.GetAdoptants()){
                    Console.WriteLine($" > {adoptant} · animals: {adoptant.Animals.Count}");
                    foreach (Animal animal in adoptant.Animals){
                        Console.WriteLine($"  - {animal.Nom} ({animal.Especie})");
                    }
                }
            }
        }
    }
}