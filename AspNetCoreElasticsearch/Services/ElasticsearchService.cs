using AspNetCoreElasticsearch.Models;
using Elastic.Clients.Elasticsearch;
using System.Collections.Immutable;

namespace AspNetCoreElasticsearch.Services
{
	public class ElasticsearchService
	{
		private readonly ElasticsearchClient _client;

		public ElasticsearchService(ElasticsearchClient client)
		{
			_client = client;
		}

		private const string IndexName = "products";


		public async Task<ImmutableList<Product>> SearchByProductName(string productName)
		{

			var result = await _client.SearchAsync<Product>(s => s.Index(IndexName) 
				.Size(1000).Query(q => q
					.MatchPhrasePrefix(m => m
						.Field(f => f.ProductName)
						.Query(productName))));

			foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
			return result.Documents.ToImmutableList();

		}

		public async Task<Product> Create(Product product)
		{
			var result = await _client.IndexAsync(product, index:IndexName);
			product.Id = result.Id;
			return product;
		}


		public async Task<Boolean> Update(Product product)
		{
			var result = await _client.UpdateAsync<Product, BaseProduct>(index:IndexName,id: product.Id, u => u.Doc(product));
			return result.IsSuccess();
		}

		public async Task<bool> Delete(string id)
		{
			var result = await _client.DeleteAsync(index:IndexName, id);

			return result.IsSuccess();
		}

	}
}
