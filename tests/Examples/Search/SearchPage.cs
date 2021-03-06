// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Examples.Models;
using System.ComponentModel;
using Nest;

namespace Examples.Search
{
	public class SearchPage : ExampleBase
	{
		[U]
		[Description("search/search.asciidoc:10")]
		public void Line10()
		{
			// tag::d2153f3100bf12c2de98f14eb86ab061[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
			);
			// end::d2153f3100bf12c2de98f14eb86ab061[]

			searchResponse.MatchesExample(@"GET /twitter/_search");
		}

		[U]
		[Description("search/search.asciidoc:555")]
		public void Line555()
		{
			// tag::be49260e1b3496c4feac38c56ebb0669[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.QueryOnQueryString("user:kimchy")
			);
			// end::be49260e1b3496c4feac38c56ebb0669[]

			searchResponse.MatchesExample(@"GET /twitter/_search?q=user:kimchy");
		}

		[U]
		[Description("search/search.asciidoc:601")]
		public void Line601()
		{
			// tag::f5569945024b9d664828693705c27c1a[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index(new[] { "kimchy", "elasticsearch" })
				.QueryOnQueryString("user:kimchy")
			);
			// end::f5569945024b9d664828693705c27c1a[]

			searchResponse.MatchesExample(@"GET /kimchy,elasticsearch/_search?q=user:kimchy");
		}

		[U]
		[Description("search/search.asciidoc:613")]
		public void Line613()
		{
			// tag::168bfdde773570cfc6dd3ab3574e413b[]
			var searchResponse = client.Search<Tweet>(s => s
				.AllIndices()
				.QueryOnQueryString("user:kimchy")
			);
			// end::168bfdde773570cfc6dd3ab3574e413b[]

			searchResponse.MatchesExample(@"GET /_search?q=user:kimchy");
		}

		[U]
		[Description("search/search.asciidoc:622")]
		public void Line622()
		{
			// tag::8022e6a690344035b6472a43a9d122e0[]
			var searchResponse = client.Search<Tweet>(s => s
				.AllIndices()
				.QueryOnQueryString("user:kimchy")
			);
			// end::8022e6a690344035b6472a43a9d122e0[]

			searchResponse.MatchesExample(@"GET /_all/_search?q=user:kimchy");
		}

		[U]
		[Description("search/search.asciidoc:628")]
		public void Line628()
		{
			// tag::43682666e1abcb14770c99f02eb26a0d[]
			var searchResponse = client.Search<Tweet>(s => s
				.AllIndices()
				.QueryOnQueryString("user:kimchy")
			);
			// end::43682666e1abcb14770c99f02eb26a0d[]

			searchResponse.MatchesExample(@"GET /*/_search?q=user:kimchy", e =>
			{
				e.Uri.Path = "/_all/_search";
			});
		}

		[U]
		[Description("search/search.asciidoc:637")]
		public void Line637()
		{
			// tag::0ce3606f1dba490eef83c4317b315b62[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::0ce3606f1dba490eef83c4317b315b62[]

			searchResponse.MatchesExample(@"GET /twitter/_search
			{
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}", e =>
			{
				e.ApplyBodyChanges(json =>
				{
					json["query"]["term"]["user"].ToLongFormTermQuery();
				});
			});
		}
	}
}
