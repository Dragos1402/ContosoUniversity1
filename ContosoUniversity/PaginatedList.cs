using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); // numara paginile
            this.AddRange(items);
        }
        // Imi adauga butonul PreviousPage daca PageIndex(numarul paginii) > 1
        public bool HasPreviousPage => PageIndex > 1; 

        public bool HasNextPage => PageIndex < TotalPages;

      /*Metoda CreateAsync este utilizată pentru a crea PaginatedList<T>.
        Un constructor nu poate crea obiectul PaginatedList<T>; constructorii nu pot
        executa cod asincron.
      Metoda CreateAsync din codul precedent ia dimensiunea și numărul paginii și aplică 
      instrucțiunile corespunzătoare Skip și Take la IQueryable. Atunci când ToListAsync
      este apelat pe IQueryable, acesta returnează o listă care conține doar pagina 
      solicitată. Proprietățile HasPreviousPage și HasNextPage sunt utilizate pentru a 
      activa sau dezactiva butoanele de paginare Previous și Next. */

        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex -1) * pageSize)
                                    .Take(pageSize).ToListAsync(); 
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}

/* Dupa acest cod trebuie sa adaugam aceste functionalitati pe pagina pe care ne-o dorim.
 In acest caz ne dorim ca aceste butoane sa fie pe pagina Index. */