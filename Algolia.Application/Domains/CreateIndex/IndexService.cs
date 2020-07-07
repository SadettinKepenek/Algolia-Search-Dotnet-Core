using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algolia.Application.Enums;
using Algolia.Application.Models.CreateIndex;
using Algolia.Application.Models.IndexSettings;
using Algolia.Application.Models.UpdateIndex;
using Algolia.Search.Clients;
using Algolia.Search.Models.Rules;
using Algolia.Search.Models.Search;
using Algolia.Search.Models.Settings;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Algolia.Application.Domains.CreateIndex
{
    public class IndexService : IIndexService
    {
        private IConfiguration _configuration;
        private readonly string AdminAPIKey;
        private readonly string SearchOnlyAPIKey;
        private readonly string ApplicationId;
        private readonly SearchClient client;

        public IndexService(IConfiguration configuration)
        {
            _configuration = configuration;
            var section = _configuration.GetSection("ApiKeys");
            AdminAPIKey = section["Admin_API_Key"];
            SearchOnlyAPIKey = section["Search_Only_API_Key"];
            ApplicationId = section["ApplicationId"];
            client = new SearchClient(ApplicationId, AdminAPIKey);
        }

        public async Task<CreateIndexResponseDto> CreateIndexAsync<T>(CreateIndexRequestDto<T> dto) where T : class, new()
        {

            try
            {
                SearchIndex index = client.InitIndex(dto.IndexName);
                await index.SaveObjectsAsync(JArray.FromObject(dto.Data),autoGenerateObjectId:false);
                return new CreateIndexResponseDto("Index created successfully", true);
            }
            catch (Exception e)
            {
                return new CreateIndexResponseDto(e.Message, false);
            }
        }

        public async Task<UpdateIndexRecordsResponseDto> UpdateRecordsAsync<T>(UpdateIndexRecordsRequestDto<T> dto) where T : class, new()
        {
            try
            {
                SearchIndex index = client.InitIndex(dto.IndexName);
                await index.PartialUpdateObjectsAsync(JArray.FromObject(dto.Data),createIfNotExists:true);
                return new UpdateIndexRecordsResponseDto("Records updated successfully", true);
            }
            catch (Exception e)
            {
                return new UpdateIndexRecordsResponseDto(e.Message, false);
            }
        }

        public async Task<UpdateIndexSettingsResponseDto> UpdateIndexSettings<T>(UpdateIndexSettingsRequestDto dto) where T : class, new()
        {
            try
            {
                SearchIndex index = client.InitIndex(dto.IndexName);
                List<string> CustomRankingOptions = new List<string>();
                List<string> SearchableAttributeOptions = new List<string>();
                StringBuilder sb = new StringBuilder();
                if (dto.IndexOptions != null)
                {
                    #region SetCustomRankingOptions

                    if (dto.IndexOptions.CustomRankingAttributes != null)
                    {
                        foreach (var rankingAttribute in dto.IndexOptions.CustomRankingAttributes)
                        {
                            FillCustomRankingOptions<T>(rankingAttribute, sb, CustomRankingOptions);
                        }
                    }

                    #endregion

                    #region SearchableAttributeOptions

                    if (dto.IndexOptions.SearchableAttributes != null)
                    {
                        foreach (var searchableAttribute in dto.IndexOptions.SearchableAttributes)
                        {
                            FillSearchableAttributesOptions<T>(searchableAttribute, sb, SearchableAttributeOptions);
                        }
                    }
                    #endregion
                }
                IndexSettings settings = new IndexSettings();
                settings.CustomRanking = CustomRankingOptions;
                settings.SearchableAttributes = SearchableAttributeOptions;
                await index.SetSettingsAsync(settings);
                return new UpdateIndexSettingsResponseDto("Index settings updated", true);
            }
            catch (Exception e)
            {
                return new UpdateIndexSettingsResponseDto(e.Message, false);
            }
        }
        private static void FillCustomRankingOptions<T>(CustomRankingAttribute rankingAttribute, StringBuilder sb, List<string> CustomRankingOptions)
            where T : class, new()
        {
            var propertyInfo = typeof(T).GetProperty(rankingAttribute.Attribute);
            if (propertyInfo == null)
                throw new ArgumentNullException("Custom ranking attribute cannot be null");
            //Sample => asc(Productname)
            sb.Append(rankingAttribute.RankingType.ToString().ToLower());
            sb.Append("(");
            sb.Append(rankingAttribute.Attribute);
            sb.Append(")");
            CustomRankingOptions.Add(sb.ToString());
            sb.Clear();
        }
        private static void FillSearchableAttributesOptions<T>(SearchableAttribute searchableAttribute, StringBuilder sb, List<string> searchableAttributeOptions)
            where T : class, new()
        {
            var propertyInfo = typeof(T).GetProperty(searchableAttribute.Attribute);
            if (propertyInfo == null)
                throw new ArgumentNullException("Searchable attribute cannot be null");
            //Sample => ordered(Productname)
            if (searchableAttribute.SearchableAttributeFunctionType != null)
            {
                sb.Append(searchableAttribute.SearchableAttributeFunctionType.ToString().ToLower());
                sb.Append("(");
                sb.Append(searchableAttribute.Attribute);
                sb.Append(")");
            }
            else
            {
                sb.Append(searchableAttribute.Attribute);
            }

            searchableAttributeOptions.Add(sb.ToString());
            sb.Clear();
        }
        public async Task<SearchIndexResponseDto<T>> SearchIndexAsync<T>(SearchIndexRequestDto dto) where T : class, new()
        {
            try
            {
                SearchIndex index = client.InitIndex(dto.IndexName);
                var page = dto.SkipCount / dto.MaxResultCount;
                dto.Query.Length = dto.MaxResultCount;
                dto.Query.Offset = dto.SkipCount;
                await index.SetSettingsAsync(new IndexSettings()
                {
                    PaginationLimitedTo = long.MaxValue
                });
                var response = await index.SearchAsync<T>(dto.Query);
                if (response.Hits.Count == 0)
                    return new SearchIndexResponseDto<T>("Sonuç bulunamadı", true, new List<T>(), 0, 0);
                return new SearchIndexResponseDto<T>(response.Message, true, response.Hits, response.HitsPerPage, response.NbHits);
            }
            catch (Exception e)
            {
                return new SearchIndexResponseDto<T>(e.Message, false, null, 0, 0);
            }

        }


    }
}