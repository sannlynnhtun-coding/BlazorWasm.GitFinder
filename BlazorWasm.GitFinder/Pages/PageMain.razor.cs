using BlazorWasm.GitFinder.Models;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BlazorWasm.GitFinder.Pages
{
    public partial class PageMain
    {
        private string? _search;
        private bool _searching;
        private int counter = 1;
        private string? Mode = "light_mode";

        private Profile? _profile;
        private List<Repository>? _repositories;
        private List<Follower>? _follower;
        private List<Following>? _following;
        private bool _notFound;

        private string? _repoUrl;
        private string? _followerUrl;
        private string? _followingUrl;

        private EnumTabType _tabType = EnumTabType.Repositories;
        const string DefaultUser = "sannlynnhtun-coding";

        protected override async Task OnInitializedAsync()
        {
            _search = DefaultUser;
            await Search();
        }

        async Task Search()
        {
            try
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
                        int index = _profile.following_url.IndexOf("{", StringComparison.Ordinal);
                        _followingUrl = _profile.following_url.Substring(0, index);

                        await Repository();
                        await Follower();
                        await Following();
                    }
                }

                _searching = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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

        private async Task ClickUser(string? name)
        {
            _search = name;
            _tabType = EnumTabType.Repositories;
            await Search();
        }

        private async Task Default()
        {
            _search = DefaultUser;
            _tabType = EnumTabType.Repositories;
            await Search();
        }

        void ChangeTheme()
        {
            if (++counter % 2 != 0)
            {
                jsRuntime.InvokeVoidAsync("toggleTheme", "dark");
                Mode = "light_mode";
            }
            else
            {
                jsRuntime.InvokeVoidAsync("toggleTheme", "light");
                Mode = "dark_mode"; 
            }
        }
    }
}
