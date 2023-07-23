﻿using BlazorWasm.GitFinder.Models;
using System.Net.Http.Json;

namespace BlazorWasm.GitFinder.Pages
{
    public partial class PageMain
    {
        private string? _search;
        private bool _searching;
        
        private Profile? _profile;
        private List<Repository>? _repositories;
        private bool _notFound;

        private string? _repoUrl;
        private string? _followerUrl;
        private string? _followingUrl;

        protected override async Task OnInitializedAsync()
        {
            _search = "sannlynnhtun-coding";
            await Search();
        }

        async Task Search()
        {
            _searching = true;
            _notFound = false;
            var responseProfile = await HttpClient.GetAsync($"https://api.github.com/users/{_search}");
            if (!responseProfile.IsSuccessStatusCode)
            {
                _notFound = true;
            }
            else
            {
                _profile = await responseProfile.Content.ReadFromJsonAsync<Profile>();
                if (_profile != null)
                {
                    _repoUrl = _profile.repos_url;
                    _followerUrl = _profile.followers_url;
                    _followingUrl = _profile.following_url;

                    await Repository();
                }
            }

            _searching = false;
            StateHasChanged();
        }

        async Task Repository()
        {
            var responseProfile = await HttpClient.GetAsync($"{_repoUrl}?sort=created&per_page=50");
            if (responseProfile.IsSuccessStatusCode)
            {
                _repositories = await responseProfile.Content.ReadFromJsonAsync<List<Repository>?>();
            }
        }
    }
}
