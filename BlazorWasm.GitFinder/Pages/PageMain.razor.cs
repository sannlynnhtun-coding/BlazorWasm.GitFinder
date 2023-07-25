using BlazorWasm.GitFinder.Models;
using System;
using System.Net.Http.Json;

namespace BlazorWasm.GitFinder.Pages
{
    public partial class PageMain
    {
        private string? _search;
        private bool _searching;
        
        private Profile? _profile;
        private List<Repository>? _repositories;
        private List<Follower>? _follower;
        private List<Following>? _following;
        private bool _notFound;

        private string? _repoUrl;
        private string? _followerUrl;
        private string? _followingUrl;

        private EnumTabType _tabType = EnumTabType.Repositories;

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
                    //_followingUrl = _profile.following_url;
                    int index = _profile.following_url.IndexOf("{");
                    _followingUrl = _profile.following_url.Substring(0, index);

                    await Repository();
                    await Follower();
                    await Following();
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

        async Task Follower()
        {
            var responseFollower = await HttpClient.GetAsync(_followerUrl);
            if (responseFollower.IsSuccessStatusCode)
            {
                _follower = await responseFollower.Content.ReadFromJsonAsync<List<Follower>>();
            }
        }
        async Task Following()
        {
            var responseFollowing = await HttpClient.GetAsync(_followingUrl);
            if (responseFollowing.IsSuccessStatusCode)
            {
                _following = await responseFollowing.Content.ReadFromJsonAsync<List<Following>>();
            }
        }

        void ChangeTab(EnumTabType tabType)
        {
            _tabType = tabType;
            StateHasChanged();
        }

        string Hidden(EnumTabType tabType)
        {
            return _tabType != tabType ? "display: none;" : "";
        }

        private string SelectedTab(EnumTabType tabType)
        {
            return (_tabType == tabType).ToString().ToLower();
        }

        public async Task SearchRepro(string? name)
        {
            _search = name;
            _tabType = EnumTabType.Repositories;
            await Search();
            StateHasChanged();
        }
    }
}
