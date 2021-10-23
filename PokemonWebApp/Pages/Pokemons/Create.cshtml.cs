using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PokemonWebApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PokemonWebApp.Pages.Pokemons
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Pokemon Pokemon { get; set; }

        string baseUrl = "https://localhost:44387/";
        public async Task<IActionResult> OnPostAsync()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client
                    .PostAsJsonAsync("api/Pokemons", Pokemon);
                
                if(response.IsSuccessStatusCode)
                {
                    //Pokemons/Index
                    return RedirectToPage("./Index");
                } else {
                    return RedirectToPage("./Create");
                }
            }
        }
    }
}
