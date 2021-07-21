using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationPrueba.Data;
using WebApplicationPrueba.Entities;

namespace WebApplicationPrueba.Controllers
{
    public class ConsultasController : Controller
    {
        readonly ApplicationDbContext _context;
        public ConsultasController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;

        }

        public IEnumerable<Departamento> Departamentos()
        {
            return _context.Departamentos;
             
        }

        public IEnumerable<Empleado> Empleados()
        {
            //_context.Empleados.OrderBy(x => x.Apellido)//ordenado ascendendete

            //_context.Empleados.OrderBy(x => x.NombreCompleto); //Falla

            //_context.Empleados.OrderByDescending(x => x.Apellido); //orden descendente

            //_context.Empleados.OrderBy(x => x.Nombre).ThenBy(x => x.Apellido);

            IOrderedQueryable<Empleado> consulta = _context.Empleados.OrderBy(x => x.Nombre); //IQueareable<Empleado>

            List<Empleado> empleados  = consulta.ToList(); //List<Empleado>

            var jorges = empleados.Where(x => x.Nombre == "Jorge");

            var menor = empleados[0].Salario;
            foreach (var item in empleados)
            {
                if (item.Salario < menor)
                    menor = item.Salario;
            }

            for (var i=0; i<empleados.Count; i++)
            {
                if (empleados[i].Salario < menor)
                    menor = empleados[i].Salario;
            }

            var menorRapido = empleados.Min(e => e.Salario); //No procedimental
            var mayor = empleados.Min(e => e.Nacimiento);

            empleados.OrderBy(e => e.Salario);

            return empleados.OrderBy(e => e.NombreCompleto);
        }
    }
}
