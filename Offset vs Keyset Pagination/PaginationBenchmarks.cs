using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offset_vs_Keyset_Pagination
{
    [RankColumn]
    public class PaginationBenchmarks
    {
        private readonly DataContext _dbContext;
        private readonly int _totalItems;
        private readonly int _pageSize;

        public PaginationBenchmarks()
        {
            _dbContext = new DataContext();
            _totalItems = _dbContext.Entities.Count();
            _pageSize = 50;
        }

        //[Benchmark]
        public void GetLastPageWithOffset()
        {
            var pages = _totalItems / _pageSize;
            var skip = (pages - 1) * _pageSize;
            var page = _dbContext.Entities
                        .OrderBy(x => x.Id)
                        .Skip(skip)
                        .Take(_pageSize)
                        .ToList();
        }

        //[Benchmark]
        public void GetLastPageWithKeyset()
        {
            var id = _totalItems - _pageSize + 1;
            var page = _dbContext.Entities
                        .Where(x => x.Id >= id)
                        .Take(_pageSize)
                        .ToList();
        }

        [Benchmark]
        public void OffsetPagination()
        {
            for (int i = 0; i < _totalItems; i += _pageSize)
            {
                var page = _dbContext.Entities
                        .OrderBy(x => x.Id)
                        .Skip(i)
                        .Take(_pageSize)
                        .ToList();
            }
        }

        [Benchmark]
        public  void KeysetPagination()
        {
            int? lastEntityId = null;
            while (true)
            {
                var page = _dbContext.Entities
                    .Where(e => lastEntityId == null || e.Id > lastEntityId)
                    .OrderBy(x => x.Id)
                    .Take(_pageSize)
                    .ToList();

                if (!page.Any()) break;

                lastEntityId = page.Last().Id;
            }
        }

    }
}
