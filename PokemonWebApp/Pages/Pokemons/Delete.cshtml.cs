using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PokemonWebApp.Models;

namespace PokemonWebApp.Pages.Pokemons
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Pokemon Pokemon { get; set; }
        string baseUrl = "https://localhost:44387/";
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                //Fazendo método GET https://localhost:44387/api/Pokemons/{id}
                HttpResponseMessage response = await client.GetAsync("api/Pokemons/" + id);

                //Booleano que nos diz se deu certo ou se teve algum erro
                if(response.IsSuccessStatusCode)
                {
                    //Captura a string do json
                    string result = response.Content.ReadAsStringAsync().Result;
                    //Tranformar a string json em um objeto do tipo Pokemon
                    Pokemon = JsonConvert.DeserializeObject<Pokemon>(result);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            if(Pokemon.ID != id)
            {
                return BadRequest();
            }

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client
                    .DeleteAsync("api/Pokemons/" + Pokemon.ID);
                if(response.IsSuccessStatusCode)
                {
                    //Sucesso! Quero ir para a minha página http://localhost:port/Pokemons
                    return RedirectToPage("./Index");
                } else {
                    return Page();
                }
            }
        }
    }
}
