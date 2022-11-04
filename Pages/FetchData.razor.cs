using System.Net.Http;
using System.Text.Json;
using ApiViewerBlazor.DTO;
using ApiViewerBlazor.ViewModels;
using Microsoft.AspNetCore.Components.Web;

namespace ApiViewerBlazor.Pages
{
    public partial class FetchData
    {
        private IEnumerable<ShowViewModel> shows = new ShowViewModel[0];

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5254/api/show");
                var client = new HttpClient();
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    this.shows = FetchShows(responseString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static List<ShowViewModel> FetchShows(string responseString)
        {
            return JsonSerializer.Deserialize<ShowDto[]>(
                    responseString,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    })
                .Select(dto => new ShowViewModel
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Persons = dto.Cast.Select(c => new PersonViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Birthday = c.Birthday
                    })
                })
                .ToList();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        private async Task OnExpandClick(ShowViewModel show)
        {
            show.IsExpanded = !show.IsExpanded;
            await this.InvokeAsync(() => this.StateHasChanged());
        }

        private string GetRowDetailsCss(ShowViewModel show)
        {
            if (show.IsExpanded)
                return "shows-row-details-expanded";
            else
                return "shows-row-details-collapsed";
        }

        private string GetCastExpanderCaption(ShowViewModel show)
        {
            return show.IsExpanded ? "-" : "+";
        }
    }
}
