using Accord.Math;
using Business.Constants.Messages.Services.AI;
using Business.Services.AI.Abstract;
using Business.Services.Marketing.Abstract;
using Business.Services.Membership.Abstract;
using Core.ExceptionHandling;
using Core.Services.Messages;
using Core.Services.Result;
using Core.Utils.Auth;
using Core.Utils.IoC;
using Domain.DTOs.Marketing;

namespace Business.Services.AI.Concrete;

public class ProductRecommendationManager : IProductRecommendationService
{
    private const int NumberOfRecommendations = 7;
    private readonly ICustomerService _customerService = ServiceTool.GetService<ICustomerService>()!;
    private readonly IStoreProductService _storeProductService = ServiceTool.GetService<IStoreProductService>()!;

    public async Task<ServiceCollectionResult<StoreProductGetDto>> GetRecommendedProductsAsync()
    {
        var result = new ServiceCollectionResult<StoreProductGetDto>();

        try
        {
            var userId = AuthHelper.GetUserId()!;
            var currentShoppingListDto = await _customerService.GetShoppingListAsync(userId);
            var currentShoppingList = currentShoppingListDto.Data;
            var currentProductIds = currentShoppingList?.Select(p => p.Id.ToString()).ToList();

            if (currentProductIds == null || currentProductIds.Count == 0)
            {
                result.Fail("PRCD-458762", ProductRecommendationServiceMessages.NoProductsInShoppingList);
                return result;
            }

            var shoppingListsOfOtherCustomersDto = await _storeProductService.GetAllShoppingListsAnonymously();
            var shoppingListsOfOtherCustomers = shoppingListsOfOtherCustomersDto.Data!;

            // Add current user's shopping list to the list of other customers
            shoppingListsOfOtherCustomers.Add(currentProductIds);

            // Create DataFrame
            var allProducts = shoppingListsOfOtherCustomers.SelectMany(x => x).Distinct().ToList();
            var df = shoppingListsOfOtherCustomers
                .Select(favList => allProducts.Select(product => favList.Contains(product) ? 1.0 : 0.0).ToArray())
                .ToArray();

            var currentUser = df.Last();
            var similarities = df.Take(df.Length - 1).Select(user => Distance.Cosine(currentUser, user)).ToArray();

            // Compute weighted sums
            var weightedSums = df.Take(df.Length - 1)
                .Select((user, i) => user.Select(x => x * similarities[i]).ToArray())
                .Aggregate((a, b) => a.Zip(b, (x, y) => x + y).ToArray());

            // Recommend products
            var currentShoppingSet = new HashSet<string>(currentProductIds);
            var sortedSums = allProducts
                .Zip(weightedSums, (product, weight) => new { Product = product, Weight = weight })
                .OrderByDescending(x => x.Weight).ToList();
            var recommendations = sortedSums.Where(x => !currentShoppingSet.Contains(x.Product))
                .Take(NumberOfRecommendations)
                .Select(x => x.Product).ToList();

            var recommendedProducts = new List<StoreProductGetDto>();

            foreach (var recommendation in recommendations)
            {
                var product = await _storeProductService.GetByIdAsync(recommendation);
                recommendedProducts.Add(product.Data!);
            }

            result.SetData(recommendedProducts,
                successMessage: ProductRecommendationServiceMessages.ProductsRecommended);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("PRCD-753659", e.Message));
        }

        return result;
    }
}