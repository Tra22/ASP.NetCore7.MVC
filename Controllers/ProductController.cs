using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Payloads.Dtos.Products;
using MVC.Payloads.Requests;
using MVC.Payloads.Requests.Products;
using MVC.Repositories;
using MVC.Services.Products;
namespace MVC.Controllers;
public class ProductController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductController(ILogger<HomeController> logger, IProductService productService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _productService = productService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View(new Product());
    }
    [HttpPost]
    public async Task<IActionResult> GetProducts(DataTableParameters parameters)
    {
        return await _productService.GetAllProduct(parameters);
    }
    [HttpGet]
    public IActionResult Create(){
        return View(new CreateProduct());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProduct createProduct, CancellationToken cancellationToken){
        if(ModelState.IsValid){
            ProductDto product = await _productService.CreateProduct(createProduct, cancellationToken);
            return View(_mapper.Map<CreateProduct>(product));
        }
        return View(createProduct);
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id){
        ProductDto product = await _productService.GetAllProductById(id);
        return View(_mapper.Map<UpdateProduct>(product));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, UpdateProduct updateProduct, CancellationToken cancellationToken){
        if(ModelState.IsValid){
            ProductDto product = await _productService.UpdateProduct(id, updateProduct, cancellationToken);
            return View(_mapper.Map<UpdateProduct>(product));
        }
        return View(updateProduct);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return new JsonResult(new 
        {
            Success = await _productService.DeleteProduct(id, cancellationToken)
        });
    }
}
