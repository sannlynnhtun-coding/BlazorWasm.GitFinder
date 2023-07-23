using BlazorWasm.GitFinder.Models;
using System.Net.Http.Json;

namespace BlazorWasm.GitFinder.Pages
{
    public partial class _PageMain
    {
        private string? _search;
        private Profile? _profile;
        private bool _notFound;

        protected override async Task OnInitializedAsync()
        {
            _search = "sannlynnhtun-coding";
            await Search();
        }

        async Task Search()
        {
            _notFound = false;
            var responseProfile = await HttpClient.GetAsync($"https://api.github.com/users/{_search}");
            if (!responseProfile.IsSuccessStatusCode)
            {
                _notFound = true;
            }
            else
            {
                _profile = await responseProfile.Content.ReadFromJsonAsync<Profile>();
            }
            StateHasChanged();
        }
    }
}
