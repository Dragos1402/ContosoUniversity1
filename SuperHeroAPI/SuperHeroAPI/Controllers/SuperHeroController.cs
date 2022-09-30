using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
        { 
                new SuperHero
                {
                    ID = 1,
                    Name = "Spider Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "New York City"
                },
                new SuperHero
                {
                    ID=2,
                    Name="Iron Man",
                    FirstName="Gigel",
                    LastName="Frone",
                    Place="Ferentari"
                }
            };
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(heroes);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            heroes.Add(hero);  
            return Ok(heroes);
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> Edit()
        {
           
            return Ok(heroes);
        }
    }
            
}
