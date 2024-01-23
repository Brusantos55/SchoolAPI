using SchoolApi.Entities;

namespace SchoolApi.Data
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.AlumnosProfes.Any())
            {
                var alumnosProfes = new List<AlumnosProfes>()
                {
                    new AlumnosProfes()
                    {
                        Alumno = new Alumno()
                        {
                            Age=23,
                            Name="Br1",
                            Surname="Santos Lanza",
                            Examenes=new List<Examen>()
                            {
                                new Examen
                                {
                                    Nota = 10,
                                    Descripcion = "ASP.NET API"
                                },
                                new Examen
                                {
                                    Nota = 6,
                                    Descripcion = "consistency"
                                }
                            },
                        },
                        Profesor = new Profesor()
                        {
                            FullName = "Teddy Smith",
                            NickName = "elVer",
                            Subject = "programing"
                        }
                    }
                };
                dataContext.AlumnosProfes.AddRange(alumnosProfes);
                dataContext.SaveChanges();
            }
        }
    }
}
