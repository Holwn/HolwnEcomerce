﻿using HolwnEcommerce.ProductAttributes;
using HolwnEcommerce.ProductCategories;
using HolwnEcommerce.Products;
using HolwnEcommerce.Public.Catalog.ProductCategories;
using HolwnEcommerce.Public.Catalog.Products.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;

namespace HolwnEcommerce.Public.Catalog.Products
{
    public class ProductsAppService : ReadOnlyAppService
        <Product,
        ProductDto,
        Guid,
        PagedResultRequestDto>, IProductsAppService
    {
        private readonly IBlobContainer<ProductThumbnailPictureContainer> _fileContainer;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IRepository<ProductAttributeDateTime> _productAttributeDateTimeRepository;
        private readonly IRepository<ProductAttributeInt> _productAttributeIntRepository;
        private readonly IRepository<ProductAttributeDecimal> _productAttributeDecimalRepository;
        private readonly IRepository<ProductAttributeVarchar> _productAttributeVarcharRepository;
        private readonly IRepository<ProductAttributeText> _productAttributeTextRepository;
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductsAppService(IRepository<Product, Guid> repository,
            IBlobContainer<ProductThumbnailPictureContainer> fileContainer,
            IRepository<ProductAttribute> productAttributeRepository,
            IRepository<ProductAttributeDateTime> productAttributeDateTimeRepository,
            IRepository<ProductAttributeInt> productAttributeIntRepository,
            IRepository<ProductAttributeDecimal> productAttributeDecimalRepository,
            IRepository<ProductAttributeVarchar> productAttributeVarcharRepository,
            IRepository<ProductAttributeText> productAttributeTextRepository,
            IRepository<Product, Guid> productRepository)
            : base(repository)
        {
            _fileContainer = fileContainer;
            _productAttributeRepository = productAttributeRepository;
            _productAttributeDateTimeRepository = productAttributeDateTimeRepository;
            _productAttributeDecimalRepository = productAttributeDecimalRepository;
            _productAttributeIntRepository = productAttributeIntRepository;
            _productAttributeTextRepository = productAttributeTextRepository;
            _productAttributeVarcharRepository = productAttributeVarcharRepository;
            _productRepository = productRepository;
        }

        public async Task<List<ProductInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Product>, List<ProductInListDto>>(data);
        }

        public async Task<PagedResult<ProductInListDto>> GetListFilterAsync(ProductListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));
            query = query.WhereIf(input.CategoryId.HasValue, x => x.CategoryId == input.CategoryId);

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter
               .ToListAsync(
                  query.Skip((input.CurrentPage - 1) * input.PageSize)
               .Take(input.PageSize));

            return new PagedResult<ProductInListDto>(
                ObjectMapper.Map<List<Product>, List<ProductInListDto>>(data),
                totalCount,
                input.CurrentPage,
                input.PageSize
            );
        }

        public async Task<string> GetThumbnailImageAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }
            var thumbnailContent = await _fileContainer.GetAllBytesOrNullAsync(fileName);

            if (thumbnailContent is null)
            {
                return null;
            }
            var result = Convert.ToBase64String(thumbnailContent);
            return result;
        }

        public async Task<List<ProductAttributeValueDto>> GetListProductAttributeAllAsync(Guid productId)
        {
            var attributeQuery = await _productAttributeRepository.GetQueryableAsync();

            var attributeDateTimeQuery = await _productAttributeDateTimeRepository.GetQueryableAsync();
            var attributeDecimalQuery = await _productAttributeDecimalRepository.GetQueryableAsync();
            var attributeIntQuery = await _productAttributeIntRepository.GetQueryableAsync();
            var attributeVarcharQuery = await _productAttributeVarcharRepository.GetQueryableAsync();
            var attributeTextQuery = await _productAttributeTextRepository.GetQueryableAsync();

            var query = from a in attributeQuery
                        join adate in attributeDateTimeQuery on a.Id equals adate.AttributeId into aDateTimeTable
                        from adate in aDateTimeTable.DefaultIfEmpty()
                        join adecimal in attributeDecimalQuery on a.Id equals adecimal.AttributeId into aDecimailTable
                        from adecimal in aDecimailTable.DefaultIfEmpty()
                        join aint in attributeIntQuery on a.Id equals aint.AttributeId into aIntTable
                        from aint in aIntTable.DefaultIfEmpty()
                        join avarchar in attributeVarcharQuery on a.Id equals avarchar.AttributeId into aVarcharTable
                        from avarchar in aVarcharTable.DefaultIfEmpty()
                        join atext in attributeTextQuery on a.Id equals atext.AttributeId into aTextTable
                        from atext in aTextTable.DefaultIfEmpty()
                        where (adate == null || adate.ProductId == productId)
                        && (adecimal == null || adecimal.ProductId == productId)
                        && (aint == null || aint.ProductId == productId)
                        && (avarchar == null || avarchar.ProductId == productId)
                        && (atext == null || atext.ProductId == productId)
                        select new ProductAttributeValueDto()
                        {
                            Label = a.Label,
                            AttributeId = a.Id,
                            DataType = a.DataType,
                            Code = a.Code,
                            ProductId = productId,
                            DateTimeValue = adate != null ? adate.Value : null,
                            DecimalValue = adecimal != null ? adecimal.Value : null,
                            IntValue = aint != null ? aint.Value : null,
                            VarcharValue = avarchar != null ? avarchar.Value : null,
                            TextValue = atext != null ? atext.Value : null,
                            DateTimeId = adate != null ? adate.Id : null,
                            DecimalId = adecimal != null ? adecimal.Id : null,
                            IntId = aint != null ? aint.Id : null,
                            VarcharId = avarchar != null ? avarchar.Id : null,
                            TextId = atext != null ? atext.Id : null,
                        };
            query = query.Where(x => x.DateTimeId != null || x.DecimalId != null || x.IntId != null || x.VarcharId != null || x.TextId != null);
            return await AsyncExecuter.ToListAsync(query);
        }

        public async Task<PagedResult<ProductAttributeValueDto>> GetListProductAttributesAsync(ProductAttributeListFilterDto input)
        {
            var attributeQuery = await _productAttributeRepository.GetQueryableAsync();

            var attributeDateTimeQuery = await _productAttributeDateTimeRepository.GetQueryableAsync();
            var attributeDecimalQuery = await _productAttributeDecimalRepository.GetQueryableAsync();
            var attributeIntQuery = await _productAttributeIntRepository.GetQueryableAsync();
            var attributeVarcharQuery = await _productAttributeVarcharRepository.GetQueryableAsync();
            var attributeTextQuery = await _productAttributeTextRepository.GetQueryableAsync();

            var query = from a in attributeQuery
                        join adate in attributeDateTimeQuery on a.Id equals adate.AttributeId into aDateTimeTable
                        from adate in aDateTimeTable.DefaultIfEmpty()
                        join adecimal in attributeDecimalQuery on a.Id equals adecimal.AttributeId into aDecimailTable
                        from adecimal in aDecimailTable.DefaultIfEmpty()
                        join aint in attributeIntQuery on a.Id equals aint.AttributeId into aIntTable
                        from aint in aIntTable.DefaultIfEmpty()
                        join avarchar in attributeVarcharQuery on a.Id equals avarchar.AttributeId into aVarcharTable
                        from avarchar in aVarcharTable.DefaultIfEmpty()
                        join atext in attributeTextQuery on a.Id equals atext.AttributeId into aTextTable
                        from atext in aTextTable.DefaultIfEmpty()
                        where (adate == null || adate.ProductId == input.ProductId)
                        && (adecimal == null || adecimal.ProductId == input.ProductId)
                        && (aint == null || aint.ProductId == input.ProductId)
                        && (avarchar == null || avarchar.ProductId == input.ProductId)
                        && (atext == null || atext.ProductId == input.ProductId)
                        select new ProductAttributeValueDto()
                        {
                            Label = a.Label,
                            AttributeId = a.Id,
                            DataType = a.DataType,
                            Code = a.Code,
                            ProductId = input.ProductId,
                            DateTimeValue = adate != null ? adate.Value : null,
                            DecimalValue = adecimal != null ? adecimal.Value : null,
                            IntValue = aint != null ? aint.Value : null,
                            VarcharValue = avarchar != null ? avarchar.Value : null,
                            TextValue = atext != null ? atext.Value : null,
                            DateTimeId = adate != null ? adate.Id : null,
                            DecimalId = adecimal != null ? adecimal.Id : null,
                            IntId = aint != null ? aint.Id : null,
                            VarcharId = avarchar != null ? avarchar.Id : null,
                            TextId = atext != null ? atext.Id : null,
                        };
            query = query.Where(x => x.DateTimeId != null || x.DecimalId != null || x.IntId != null || x.VarcharId != null || x.TextId != null);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter
               .ToListAsync(
                  query.OrderByDescending(x => x.Label).Skip((input.CurrentPage - 1) * input.PageSize)
               .Take(input.PageSize));

            return new PagedResult<ProductAttributeValueDto>(data,
                totalCount,
                input.CurrentPage,
                input.PageSize
            );
        }

        public async Task<List<ProductInListDto>> GetListTopSellerAsync(int numberOfRecords)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true)
                .OrderByDescending(x=>x.CreationTime)
                .Take(numberOfRecords);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Product>, List<ProductInListDto>>(data);
        }

        public async Task<ProductDto> GetBySlugAsync(string slug)
        {
            var product = await _productRepository.GetAsync(x => x.Slug == slug);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }
    }
}