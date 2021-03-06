﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nest;


namespace SearchApi.Models
{

    public class Elastic
    {
        string indexName { get; set; }
        public ElasticClient client { get; set; }

        public Elastic(string indexName, Uri uri)
        {
            this.indexName = indexName;
            client = this.CreateClient(uri);
        }

        public ElasticClient CreateClient(Uri uri)
        {
            var connectionSettings = new ConnectionSettings(uri);
            connectionSettings.EnableDebugMode();
            return new ElasticClient(connectionSettings);
        }

        public ISearchResponse<T> GetResponseOfQuery<T>(QueryContainer queryContainer, int size = 20) where T : class
        {
            return client.Search<T>(s => s.Index(indexName).Query(q => queryContainer).Size(size));
        }
        public static QueryContainer MakeFuzzyQuery(string query, string field, int fuzziness = -1)
        {
            return new FuzzyQuery
            {
                Field = field,
                Value = query,
                Fuzziness = fuzziness == -1 ? Fuzziness.Auto : Fuzziness.EditDistance(fuzziness)
            };
        }

        public static QueryContainer MakeMatchQuery(string query, string field, int fuzziness = 0)
        {
            return new MatchQuery
            {
                Query = query,
                Field = field,
                Fuzziness = Fuzziness.EditDistance(fuzziness)
            };
        }

        public static QueryContainer MakeMultiMatchQuery(string query, string[] fields,
            int fuzziness = 1)
        {
            return new MultiMatchQuery
            {
                Query = query,
                Fields = fields,
                Fuzziness = Fuzziness.EditDistance(fuzziness)
            };
        }

        public static QueryContainer MakeTermQuery(string query, string field, double boost = 1)
        {
            return new TermQuery
            {
                Field = field,
                Value = query,
                Boost = boost
            };
        }

        public static QueryContainer MakeTermsQuery(string[] queries, string field, double boost = 1)
        {
            return new TermsQuery
            {
                Field = field,
                Terms = queries,
                Boost = boost
            };
        }

        public static QueryContainer MakeRangeQuery(string type, string gte, string lte,
            string field, double boost = 1)
        {
            switch (type.ToLower())
            {
                case "long":
                    return new LongRangeQuery()
                    {
                        Field = field,
                        LessThan = long.Parse(lte),
                        GreaterThan = long.Parse(gte),
                        Boost = boost
                    };
                case "date":
                    return new DateRangeQuery()
                    {
                        Field = field,
                        LessThan = DateMath.FromString(lte),
                        GreaterThan = DateMath.FromString(gte),
                        Boost = boost
                    };
                case "term":
                    return new TermRangeQuery()
                    {
                        Field = field,
                        LessThan = lte,
                        GreaterThan = gte,
                        Boost = boost
                    };

                default:
                    return new TermRangeQuery()
                    {
                        Field = field,
                        LessThan = lte,
                        GreaterThan = gte,
                        Boost = boost
                    };
            }
        }

        public static QueryContainer MakeBoolQuery(QueryContainer[] must = null, QueryContainer[] filter = null,
            QueryContainer[] should = null, QueryContainer[] mustNot = null, double boost = 1)
        {
            return new BoolQuery
            {
                Must = must,
                Should = should,
                Filter = filter,
                MustNot = mustNot,
                Boost = boost
            };
        }

        public static QueryContainer MakeGeoDistanceQuery(string distance, double latitude,
            double longitude, Field distanceField, double boost = 1)
        {
            return new GeoDistanceQuery
            {
                Field = distanceField,
                DistanceType = GeoDistanceType.Arc,
                Location = new GeoLocation(latitude, longitude),
                Distance = distance,
                Boost = boost
            };
        }
        public ISearchResponse<T> GetResponseOfAggs<T>(TermsAggregation termsAggregation) where T : class
        {

            return client.Search<T>(s => s.Index(indexName).Aggregations(
                termsAggregation));
        }
        public static TermsAggregation MakeTermsAggQuery(string field, string name = "", bool keyword = false)
        {
            if (name == "")
                name = field;
            return new TermsAggregation(name)
            {
                Field = field + (keyword ? ".keyword" : "")
            };
        }


        public ResponseBase CreateIndex<T>(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> settingSelector = null,
            Func<TypeMappingDescriptor<T>, ITypeMapping> mapSelector = null) where T : class
        {
            return client.Indices.Create(indexName,
                s => s.Settings(settingSelector).Map<T>(mapSelector));
        }

        public ResponseBase DeleteIndex()
        {
            return client.Indices.Delete(indexName);
        }

        public BulkResponse BulkIndex<T>(List<T> dataList, string idFieldName) where T : class
        {
            var bulkDescriptor = new BulkDescriptor();
            foreach (var data in dataList)
            {
                bulkDescriptor.Index<T>(x => x
                    .Index(indexName)
                    .Document(data)
                    .Id((string)data.GetType().GetProperty(idFieldName).GetValue(data))
                );
            }
            return client.Bulk(bulkDescriptor);
        }

        public IndexResponse Index<T>(T document, string idFieldName) where T : class
        {
            return client.Index<T>(document, x => x
                .Index(indexName)
                .Id((string)document.GetType().GetProperty(idFieldName).GetValue(document)));
        }

        public T GetDocument<T>(string id) where T : class
        {
            return client.Get<T>(id, g => g.Index(indexName)).Source;
        }

        public RefreshResponse Refresh()
        {
            return client.Indices.Refresh(indexName);
        }

        public CatResponse<CatNodesRecord> GetCatNodes()
        {
            return client.Cat.Nodes();
        }
        public CatResponse<CatIndicesRecord> GetCatIndices()
        {
            return client.Cat.Indices();
        }

        public ClusterHealthResponse GetClusterHealth(
            Func<ClusterHealthDescriptor, IClusterHealthRequest> healthSelector = null)
        {
            return client.Cluster.Health(indexName, healthSelector);
        }

        public static void QueryResponsePrinter<T>(string queryType, ISearchResponse<T> response) where T : class
        {
            Console.WriteLine(queryType + " query:  ---------------------");
            response.Hits.ToList().ForEach(x => Console.WriteLine(x.Source.ToString()));
        }
        public static void TermAggResponsePrinter<T>(ISearchResponse<T> response, string name) where T : class
        {
            Console.WriteLine(name + " Terms Aggregation:  ---------------------");
            response.Aggregations.Terms(name).Buckets.ToList().ForEach(x => Console.WriteLine(x.Key + " : " + x.DocCount));
        }
    }
}

