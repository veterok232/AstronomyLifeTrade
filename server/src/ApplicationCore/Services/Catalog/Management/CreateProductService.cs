using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Exceptions;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Catalog.Management;
using ApplicationCore.Interfaces.Files;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Files;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Catalog;
using ApplicationCore.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Services.Catalog.Management;

[ScopedDependency]
public class CreateProductService : ICreateProductService
{
    private readonly IRepository<Product> _productsRepository;
    private readonly IRepository<Telescope> _telescopesRepository;
    private readonly IRepository<Category> _categoriesRepository;
    private readonly IRepository<ProductFile> _productFilesRepository;
    private readonly IRepository<Accessory> _accessoriesRepository;
    private readonly IMapper _mapper;
    private readonly IFileUploader _fileUploader;
    private readonly IRepository<Binocular> _binocularsRepository;

    public CreateProductService(
        IRepository<Product> productsRepository,
        IMapper mapper,
        IRepository<Category> categoriesRepository,
        IRepository<Telescope> telescopesRepository,
        IFileUploader fileUploader,
        IRepository<ProductFile> productFilesRepository,
        IRepository<Binocular> binocularsRepository,
        IRepository<Accessory> accessoriesRepository)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
        _categoriesRepository = categoriesRepository;
        _telescopesRepository = telescopesRepository;
        _fileUploader = fileUploader;
        _productFilesRepository = productFilesRepository;
        _binocularsRepository = binocularsRepository;
        _accessoriesRepository = accessoriesRepository;
    }

    public async Task<Result<Guid>> Create(CreateProductModel model)
    {
        var product = await _productsRepository.Add(_mapper.Map<Product>(model));

        await SaveProductFiles(product.Id, model.ProductImages, model.ProductFiles);

        return ResultBuilder.BuildSucceeded(product.Id);
    }

    public async Task<Result<Guid>> Edit(EditProductModel model)
    {
        var product = await _productsRepository.GetSingleOrDefault(
            new ProductForEditSpecification(model.ProductId));

        _mapper.Map(model, product);

        product.Quantity += model.Quantity;

        await _productsRepository.Update(product);

        return ResultBuilder.BuildSucceeded(product.Id);
    }

    public async Task CreateCharacteristics(ProductCharacteristics model)
    {
        var category = await _categoriesRepository.GetById(model.CategoryId);
        
        switch (category.Name)
        {
            case "Telescopes":
                var telescope = _mapper.Map<Telescope>(model.TelescopeCharacteristics);
                telescope.ProductId = model.ProductId;

                await _telescopesRepository.Add(telescope);

                break;
            case "Binoculars":
                var binocular = _mapper.Map<Binocular>(model.BinocularCharacteristics);
                binocular.ProductId = model.ProductId;

                await _binocularsRepository.Add(binocular);

                break;
            case "Accessories":
                var accessory = _mapper.Map<Accessory>(model.AccessoryCharacteristics);
                accessory.ProductId = model.ProductId;

                await _accessoriesRepository.Add(accessory);

                break;
            default:
                throw new InvalidInputException($"Unknown category with id {model.CategoryId}");
        }
    }

    public async Task EditCharacteristics(ProductCharacteristics model)
    {
        var category = await _categoriesRepository.GetById(model.CategoryId);
        
        switch (category.Name)
        {
            case "Telescopes":
                var telescope = await _telescopesRepository.GetSingleOrDefault(
                    new TelescopeByProductIdSpecification(model.ProductId));
                
                _mapper.Map(model.TelescopeCharacteristics, telescope);

                await _telescopesRepository.Update(telescope);
                break;
            case "Binoculars":
                var binocular = await _binocularsRepository.GetSingleOrDefault(
                    new BinocularByProductIdSpecification(model.ProductId));

                _mapper.Map(model.BinocularCharacteristics, binocular);

                await _binocularsRepository.Update(binocular);

                break;
            case "Accessories":
                var accessory = await _accessoriesRepository.GetSingleOrDefault(
                    new AccessoryByProductIdSpecification(model.ProductId));
                
                _mapper.Map(model.AccessoryCharacteristics, accessory);

                await _accessoriesRepository.Update(accessory);

                break;
            default:
                throw new InvalidInputException($"Unknown category with id {model.CategoryId}");
        }
    }

    public async Task<Result> Delete(Guid productId)
    {
        var product = await _productsRepository.GetById(productId);

        product.DeletedAt = DateTime.UtcNow;

        await _productsRepository.Update(product);

        return ResultBuilder.BuildSucceeded();
    }

    public async Task<ProductForEditModel> GetProductForEditModel(Guid productId)
    {
        var productForEdit = await _productsRepository.GetSingleOrDefault(
            new ProductForEditModelByProductIdSpecification(productId));
        
        var category = await _categoriesRepository.GetById(productForEdit.CategoryId);
        
        switch (category.Name)
        {
            case "Telescopes":
                productForEdit.Characteristics.TelescopeCharacteristics = await _telescopesRepository.GetSingleOrDefault(
                    new TelescopesCharacteristicsByProductIdSpecification(productId));
                break;
            case "Binoculars":
                productForEdit.Characteristics.BinocularCharacteristics = await _binocularsRepository.GetSingleOrDefault(
                    new BinocularsCharacteristicsByProductIdSpecification(productId));
                break;
            case "Accessories":
                productForEdit.Characteristics.AccessoryCharacteristics = await _accessoriesRepository.GetSingleOrDefault(
                    new AccessoriesCharacteristicsByProductIdSpecification(productId));
                break;
            default:
                throw new InvalidInputException($"Unknown category with id {productForEdit.CategoryId}");
        }

        return productForEdit;
    }

    private async Task SaveProductFiles(Guid productId, ICollection<IFormFile> images, ICollection<IFormFile> documents)
    {
        var productFiles = new List<ProductFile>();

        if (!images.IsNullOrEmpty())
        {
            foreach (var productImage in images)
            {
                var file = await _fileUploader.Upload(
                    _mapper.Map<ReadableFileModel>(productImage),
                    AttachmentType.ProductImage,
                    false);

                productFiles.Add(new ProductFile
                {
                    File = file,
                    ProductId = productId,
                    ProductFileType = ProductFileType.Image,
                });
            }
        }

        if (!documents.IsNullOrEmpty())
        {
            foreach (var productDocument in documents)
            {
                var file = await _fileUploader.Upload(
                    _mapper.Map<ReadableFileModel>(productDocument),
                    AttachmentType.ProductFile,
                    false);

                productFiles.Add(new ProductFile
                {
                    File = file,
                    ProductId = productId,
                    ProductFileType = ProductFileType.File,
                });
            }
        }
        
        if (!productFiles.IsNullOrEmpty())
        {
            await _productFilesRepository.Add(productFiles.ToArray());
        }
    }
}