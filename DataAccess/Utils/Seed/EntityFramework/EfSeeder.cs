using Core.Constants.StringConstants;
using Core.Utils.Hashing;
using Core.Utils.Price;
using Core.Utils.Seed.Abstract;
using DataAccess.Context.EntityFramework;
using Domain.Entities.Analytics;
using Domain.Entities.Association;
using Domain.Entities.Marketing;
using Domain.Entities.Membership;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Utils.Seed.EntityFramework;

/// <summary>
/// Entity framework based implementation of <seealso cref="ISeeder"/> interface
/// </summary>
public class EfSeeder : ISeeder
{
    /// <inheritdoc/>
    public void Seed(IApplicationBuilder builder)
    {
        var context = builder.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<EfDbContext>();

        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();

        // If there is no data in the database, then seed the database
        if (context.Category.Any())
            return;

        #region Customer

        HashingHelper.CreatePasswordHash("Su123456789.", out var passwordHash, out var passwordSalt);
        var adar = new Customer // Super User
        {
            Id = Guid.Empty,
            FirstName = "Adar",
            LastName = "Sönmez",
            Username = "adars",
            Email = "adarsonmez@outlook.com",
            PhoneNumber = "+905452977501",
            Role = UserRoles.Admin,
            UseMultiFactorAuthentication = true,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        HashingHelper.CreatePasswordHash("Eren123456789.", out passwordHash, out passwordSalt);
        var eren = new Customer
        {
            FirstName = "Eren",
            LastName = "Karakaya",
            PhoneNumber = "+905365070289",
            Username = "erenk",
            Email = "erenkarakaya93@gmail.com",
            Role = UserRoles.Customer,
            UseMultiFactorAuthentication = true,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        HashingHelper.CreatePasswordHash("Baris123456789.", out passwordHash, out passwordSalt);
        var baris = new Customer
        {
            FirstName = "Barış",
            LastName = "Acar",
            PhoneNumber = "+90 5061809945",
            Username = "barisa",
            Email = "baris.acr99@gmail.com",
            Role = UserRoles.Customer,
            UseMultiFactorAuthentication = false,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        var customers = new List<Customer> { adar, eren, baris };
        context.User.AddRange(customers);

        #endregion Customer

        #region Business

        HashingHelper.CreatePasswordHash("Migros123456789.", out passwordHash, out passwordSalt);
        var migros = new Business
        {
            Name = "Migros",
            Description =
                "Migros Ticaret A.Ş. (Migros Ticaret T.A.Ş.) is a Turkish retail company, part of the larger Koç Holding. It is one of the two big players in the Turkish supermarket industry, the other one being BİM. It has more than 2,000 branches in Turkey and 5 branches in North Macedonia.",
            Address = "Migros Ticaret A.Ş. Esentepe Mah. Büyükdere Cad. No: 185 34394 Şişli / İstanbul",
            Website = "https://www.migros.com.tr/",
            Logo = "https://www.logovector.org/wp-content/uploads/logos/png/m/migros_logo.png",
            Username = "migros",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Email = "migros@gmail.com",
            PhoneNumber = "+908504550444",
            UseMultiFactorAuthentication = false,
            Role = UserRoles.Business,
            PhoneNumberVerified = true,
            EmailVerified = false
        };

        HashingHelper.CreatePasswordHash("Carrefour123456789.", out passwordHash, out passwordSalt);
        var carrefour = new Business
        {
            Name = "Carrefour",
            Description =
                "Carrefour S.A. is a French multinational retail corporation headquartered in Massy, France. The eighth-largest retailer in the world by revenue, it operates a chain of hypermarkets, groceries and convenience stores, which as of January 2021, comprises its 12,225 stores in over 30 countries.",
            Address = "Carrefour S.A. 33, avenue Émile Zola 92100 Boulogne-Billancourt France",
            Website = "https://www.carrefour.com/",
            Logo = "https://www.drupal.org/files/carrefour-logo.jpg",
            Username = "carrefour",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Email = "carrefour@gmail.com",
            PhoneNumber = "+905452977501",
            UseMultiFactorAuthentication = true,
            Role = UserRoles.Business,
            EmailVerified = true,
            PhoneNumberVerified = true
        };

        HashingHelper.CreatePasswordHash("BIM123456789.", out passwordHash, out passwordSalt);
        var bim = new Business
        {
            Name = "BİM",
            Description =
                "BİM Birleşik Mağazalar A.Ş. is a Turkish retail company, known for offering a limited range of basic food items and consumer goods at competitive prices. It is one of the two big players in the Turkish supermarket industry, the other one being Migros. It has more than 7,000 branches in Turkey and 1 branch in Morocco.",
            Address = "BİM Birleşik Mağazalar A.Ş. Esentepe Mah. Büyükdere Cad. No: 185 34394 Şişli / İstanbul",
            Website = "https://www.bim.com.tr/",
            Logo = "https://www.logovector.org/wp-content/uploads/logos/png/b/bim_logo.png",
            Username = "bim",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Email = "bim@gmail.com",
            PhoneNumber = "+908504550444",
            UseMultiFactorAuthentication = false,
            Role = UserRoles.Business,
            PhoneNumberVerified = false,
            EmailVerified = true
        };

        var businesses = new List<Business> { migros, carrefour, bim };

        context.User.AddRange(businesses);
        context.SaveChanges();

        #endregion Business

        #region Category

        var diary = new Category { Name = "Diary", Description = "Milk, Cheese, Butter, Yogurt, Cream, Kefir" };
        var meat = new Category { Name = "Meat", Description = "Beef, Chicken, Lamb, Pork, Turkey" };
        var fruit = new Category { Name = "Fruit", Description = "Apple, Banana, Orange, Strawberry, Watermelon" };
        var vegetable = new Category { Name = "Vegetable", Description = "Carrot, Cucumber, Onion, Potato, Tomato" };
        var bakery = new Category { Name = "Bakery", Description = "Bread, Bagel, Biscuit, Cake, Cookie" };
        var drink = new Category { Name = "Drink", Description = "Coffee, Tea, Juice, Soda, Water" };
        var snack = new Category { Name = "Snack", Description = "Chips, Chocolate, Popcorn, Pretzel, Nuts" };
        var canned = new Category { Name = "Canned", Description = "Beans, Fish, Fruit, Meat, Soup" };
        var frozen = new Category { Name = "Frozen", Description = "Fruit, Meat, Pizza, Vegetable, Ice Cream" };
        var grain = new Category { Name = "Grain", Description = "Barley, Corn, Oat, Rice, Wheat" };
        var spice = new Category { Name = "Spice", Description = "Cinnamon, Garlic, Pepper, Salt, Turmeric" };
        var sauce = new Category { Name = "Sauce", Description = "Ketchup, Mayonnaise, Mustard, Soy, Vinegar" };
        var oil = new Category { Name = "Oil", Description = "Canola, Coconut, Olive, Palm, Sunflower" };
        var egg = new Category { Name = "Egg", Description = "Chicken, Duck, Goose, Quail, Turkey" };
        var dessert = new Category { Name = "Dessert", Description = "Candy, Ice Cream, Pudding, Cake, Pie" };
        var seafood = new Category { Name = "Seafood", Description = "Fish, Crab, Lobster, Shrimp, Squid" };
        var pasta = new Category { Name = "Pasta", Description = "Macaroni, Noodle, Spaghetti, Vermicelli, Lasagna" };
        var cereal = new Category
        { Name = "Cereal", Description = "Cornflakes, Oatmeal, Rice Krispies, Wheaties, Cheerios" };

        var categories = new List<Category>
        {
            diary, meat, fruit, vegetable, bakery, drink, snack, canned, frozen, grain, spice, sauce, oil, egg, dessert,
            seafood, pasta, cereal
        };

        context.Category.AddRange(categories);
        context.SaveChanges();

        #endregion Category

        #region Store Product

        // when expiration dates are coming, the discount rate will be increased
        var bimMilk = new StoreProduct
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            OriginalPrice = 25.0m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(7)),
            BusinessId = bim.Id,
            Photo = "https://cdn.cimri.io/market/260x260/dost-1-lt-laktozsuz-sut-_1430964.jpg",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = bim.Id,
            Barcode = "1"
        };

        var bimCheese = new StoreProduct
        {
            Name = "Cheese",
            Description =
                "Cheese is a dairy product, derived from milk and produced in wide ranges of flavors, textures and forms by coagulation of the milk protein casein.",
            OriginalPrice = 140m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(14)),
            BusinessId = bim.Id,
            Photo = "https://cdn.cimri.io/market/260x260/dost-500-gr-suzme-beyaz-peynir-_1431458.jpg",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = bim.Id,
            Barcode = "2"
        };

        var bimButter = new StoreProduct
        {
            Name = "Butter",
            Description =
                "Butter is a dairy product made from the fat and protein components of milk or cream. It is a semi-solid emulsion at room temperature, consisting of approximately 80% butterfat.",
            OriginalPrice = 160m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(0)),
            BusinessId = bim.Id,
            Photo = "https://www.bim.com.tr/Uploads/aktuel-urunler/1098_buyuk_543X467_tereyag.jpg",
            ExpirationDate = DateTime.Now.AddDays(0),
            CreatedUserId = bim.Id,
            Barcode = "3"
        };

        var bimGarlic = new StoreProduct
        {
            Name = "Garlic",
            Description =
                "Garlic is a species in the onion genus, Allium. Its close relatives include the onion, shallot, leek, chive, and Chinese onion.",
            OriginalPrice = 35m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(10)),
            BusinessId = bim.Id,
            Photo = "https://cf.kizlarsoruyor.com/q15507114/5969faa7-da73-418e-8154-b5b84556e25a.jpg",
            ExpirationDate = DateTime.Now.AddDays(10),
            CreatedUserId = bim.Id,
            Barcode = "4"
        };

        var bimCarrot = new StoreProduct
        {
            Name = "Carrot",
            Description =
                "The carrot is a root vegetable, usually orange in color, though purple, black, red, white, and yellow cultivars exist.",
            OriginalPrice = 15m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(5)),
            BusinessId = bim.Id,
            Photo = "https://cdn.cimri.io/market/260x260/havuc-kg-_127978.jpg",
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = bim.Id,
            Barcode = "5"
        };

        var bimYogurt = new StoreProduct
        {
            Name = "Yogurt",
            Description =
                "Yogurt, yoghurt or yoghourt is a food produced by bacterial fermentation of milk. The bacteria used to make yogurt are known as yogurt cultures.",
            OriginalPrice = 25m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(0)),
            BusinessId = bim.Id,
            Photo = "https://cdn.cimri.io/market/260x260/dost-3-kg-homojenize-yogurt-_1431008.jpg",
            ExpirationDate = DateTime.Now.AddDays(0),
            CreatedUserId = bim.Id,
            Barcode = "6"
        };

        var bimCoffee = new StoreProduct
        {
            Name = "Coffee",
            Description =
                "Coffee is a brewed drink prepared from roasted coffee beans, the seeds of berries from certain Coffea species.",
            OriginalPrice = 200m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(6)),
            BusinessId = bim.Id,
            Photo = "https://cdn.cimri.io/market/260x260/vip-100-gr-gold-kahve-_929183.jpg",
            ExpirationDate = DateTime.Now.AddDays(6),
            CreatedUserId = bim.Id,
            Barcode = "7"
        };

        var bimOil = new StoreProduct
        {
            Name = "Oil",
            Description =
                "An oil is any nonpolar chemical substance that is a viscous liquid at ambient temperatures and is both hydrophobic and lipophilic.",
            OriginalPrice = 250m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(42)),
            BusinessId = bim.Id,
            Photo = "https://www.ssk.biz.tr/wp-content/uploads/2022/06/yudum-aycicek-yagi.webp",
            ExpirationDate = DateTime.Now.AddDays(42),
            CreatedUserId = bim.Id,
            Barcode = "8"
        };

        var bimPasta = new StoreProduct
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            OriginalPrice = 10m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(8)),
            BusinessId = bim.Id,
            Photo = "https://cdn.cimri.io/market/260x260/cardella-500-gr-ince-uzun-makarna-_1430690.jpg",
            ExpirationDate = DateTime.Now.AddDays(8),
            CreatedUserId = bim.Id,
            Barcode = "9"
        };

        var bimOnion = new StoreProduct
        {
            Name = "Onion",
            Description =
                "The onion, also known as the bulb onion or common onion, is a vegetable that is the most widely cultivated species of the genus Allium.",
            OriginalPrice = 12m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(10)),
            BusinessId = bim.Id,
            Photo = "https://www.bim.com.tr/uploads/markalar/6_sogankuru.jpg",
            ExpirationDate = DateTime.Now.AddDays(10),
            CreatedUserId = bim.Id,
            Barcode = "10"
        };

        var bimApple = new StoreProduct
        {
            Name = "Apple",
            Description =
                "An apple is an edible fruit produced by an apple tree. Apple trees are cultivated worldwide and are the most widely grown species in the genus Malus.",
            OriginalPrice = 13m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(5)),
            BusinessId = bim.Id,
            Photo = "https://www.bim.com.tr/uploads/markalar/6_elma.jpg",
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = bim.Id,
            Barcode = "11"
        };

        var bimBanana = new StoreProduct
        {
            Name = "Banana",
            Description =
                "A banana is an elongated, edible fruit – botanically a berry – produced by several kinds of large herbaceous flowering plants in the genus Musa.",
            OriginalPrice = 40m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(3)),
            BusinessId = bim.Id,
            Photo =
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQwnEqhNDq77I-sMzd0yMQt6lR4nMY5qEbwZ-jp8qCspA&s",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = bim.Id,
            Barcode = "12"
        };

        var migrosMilk = new StoreProduct
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            OriginalPrice = 24m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(7)),
            BusinessId = migros.Id,
            Photo = "https://images.migrosone.com/sanalmarket/product/11012039/11012039-312495-1650x1650.jpg",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = migros.Id,
            Barcode = "13"
        };

        var migrosCheese = new StoreProduct
        {
            Name = "Cheese",
            Description =
                "Cheese is a dairy product, derived from milk and produced in wide ranges of flavors, textures and forms by coagulation of the milk protein casein.",
            OriginalPrice = 145m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(14)),
            BusinessId = migros.Id,
            Photo =
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR7329uYBvVIN4LucLXaQZ6QoMT7ASDzvAznB4AufUFGg&s",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = migros.Id,
            Barcode = "14"
        };

        var migrosPork = new StoreProduct
        {
            Name = "Pork",
            Description =
                "Pork is the culinary name for the meat of a domestic pig. It is the most commonly consumed meat worldwide, with evidence of pig husbandry dating back to 5000 BC.",
            OriginalPrice = 120m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(10)),
            BusinessId = migros.Id,
            Photo =
                "https://images.migrosone.com/sanalmarket/product/14150430/tulip-pisirilmis-domuz-kol-eti-urunu-340g-7eea2a-1650x1650.jpg",
            ExpirationDate = DateTime.Now.AddDays(10),
            CreatedUserId = migros.Id,
            Barcode = "15"
        };

        var migrosChicken = new StoreProduct
        {
            Name = "Chicken",
            Description =
                "The chicken is a type of domesticated fowl, a subspecies of the red junglefowl. It is one of the most common and widespread domestic animals, with a total population of more than 30 billion as of 2020.",
            OriginalPrice = 200m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(11)),
            BusinessId = migros.Id,
            Photo = "https://images.migrosone.com/sanalmarket/product/13029269/13029269-5fe34b-1650x1650.jpg",
            ExpirationDate = DateTime.Now.AddDays(11),
            CreatedUserId = migros.Id,
            Barcode = "16"
        };

        var migrosApple = new StoreProduct
        {
            Name = "Apple",
            Description =
                "An apple is an edible fruit produced by an apple tree. Apple trees are cultivated worldwide and are the most widely grown species in the genus Malus.",
            OriginalPrice = 16m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(5)),
            BusinessId = migros.Id,
            Photo = "https://images.migrosone.com/sanalmarket/product/27133000/elma-granny-smith-kg-0e9183.jpg",
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = migros.Id,
            Barcode = "17"
        };

        var migrosKetchup = new StoreProduct
        {
            Name = "Ketchup",
            Description =
                "Ketchup is a table condiment or sauce. Although original recipes used egg whites, mushrooms, oysters, grapes, mussels, or walnuts, among other ingredients, the unmodified term now typically refers to tomato ketchup.",
            OriginalPrice = 18m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(35)),
            Photo =
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQEUdI2nZe3bepobVZR-KcG8NA3i2SQgiBYe4IlcPaug&s",
            BusinessId = migros.Id,
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = migros.Id,
            Barcode = "18"
        };

        var migrosRice = new StoreProduct
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            OriginalPrice = 32m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(12)),
            BusinessId = migros.Id,
            Photo =
                "https://images.migrosone.com/sanalmarket/product/01011179/01011179-ff1326-1650x1650.jpg",
            ExpirationDate = DateTime.Now.AddDays(12),
            CreatedUserId = migros.Id,
            Barcode = "19"
        };

        var migrosPasta = new StoreProduct
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            OriginalPrice = 12m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(13)),
            BusinessId = migros.Id,
            Photo = "https://images.migrosone.com/sanalmarket/product/05030887/05030887-edb3d1-1650x1650.jpg",
            ExpirationDate = DateTime.Now.AddDays(13),
            CreatedUserId = migros.Id,
            Barcode = "20"
        };

        var carrefourMilk = new StoreProduct
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            OriginalPrice = 30m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(7)),
            BusinessId = carrefour.Id,
            Photo =
                "https://reimg-carrefour.mncdn.com/mnresize/600/600/productimage/30261237/30261237_0_MC/8815821226034_1581408069490.jpg",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = carrefour.Id,
            Barcode = "21"
        };

        var carrefourChocolate = new StoreProduct
        {
            Name = "Chocolate",
            Description =
                "Chocolate is a food product made from roasted and ground cacao seeds that is made in the form of a liquid, paste, or in a block, which may also be used as a flavoring ingredient in other foods.",
            OriginalPrice = 40m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(15)),
            BusinessId = carrefour.Id,
            Photo =
                "https://d39i9qfivfbklq.cloudfront.net/photo_SEO_EN/K/carrefour-alpine-milk-chocolate-bar-3x100g-3082706950.jpg",
            ExpirationDate = DateTime.Now.AddDays(15),
            CreatedUserId = carrefour.Id,
            Barcode = "22"
        };

        var carrefourPasta = new StoreProduct
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            OriginalPrice = 16m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(16)),
            BusinessId = carrefour.Id,
            Photo = "https://cdn.cimri.io/market/260x260/carrefour-burgu-500-gr-makarna-_270568.jpg",
            ExpirationDate = DateTime.Now.AddDays(16),
            CreatedUserId = carrefour.Id,
            Barcode = "23"
        };

        var carrefourFish = new StoreProduct
        {
            Name = "Fish",
            Description =
                "Fish are gill-bearing aquatic craniate animals that lack limbs with digits. They form a sister group to the tunicates, together forming the olfactores.",
            OriginalPrice = 75m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(28)),
            BusinessId = carrefour.Id,
            Photo = "https://cdn.cimri.io/market/260x260/carrefour-1-kg-alabalik-_897705.jpg",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = carrefour.Id,
            Barcode = "24"
        };

        var carrefourRice = new StoreProduct
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            OriginalPrice = 40m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(18)),
            BusinessId = carrefour.Id,
            Photo = "https://cdn.cimri.io/market/260x260/carrefour-pilavlik-1-kg-pirinc-_249633.jpg",
            ExpirationDate = DateTime.Now.AddDays(18),
            CreatedUserId = carrefour.Id,
            Barcode = "25"
        };

        var carrefourEgg = new StoreProduct
        {
            Name = "Egg",
            Description =
                "An egg is the organic vessel containing the zygote in which an animal embryo develops until it can survive on its own, at which point the animal hatches.",
            OriginalPrice = 100m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(10)),
            BusinessId = carrefour.Id,
            Photo =
                "https://reimg-carrefour.mncdn.com/mnresize/600/600/productimage/30239968/30239968_0_MC/8812739264562_1554103023703.jpg",
            ExpirationDate = DateTime.Now.AddDays(10),
            CreatedUserId = carrefour.Id,
            Barcode = "26"
        };

        var carrefourPalmoil = new StoreProduct
        {
            Name = "Palm Oil",
            Description =
                "Palm oil is an edible vegetable oil derived from the mesocarp of the fruit of the oil palms, primarily the African oil palm Elaeis guineensis, and to a lesser extent from the American oil palm Elaeis oleifera and the maripa palm Attalea maripa.",
            OriginalPrice = 330m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(35)),
            BusinessId = carrefour.Id,
            Photo =
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS5W8L1qdRHEkqFI8BUJjATFa8owp0dcd9Xrv17utcm8A&s",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = carrefour.Id,
            Barcode = "27"
        };

        var carrefourTomato = new StoreProduct
        {
            Name = "Tomato",
            Description =
                "The tomato is the edible, often red berry of the plant Solanum lycopersicum, commonly known as a tomato plant.",
            OriginalPrice = 11m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(5)),
            BusinessId = carrefour.Id,
            Photo =
                "https://reimg-carrefour.mncdn.com/mnresize/600/600/productimage/30097558/30097558_0_MC/8796990930994_1569494130525.jpg",
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = carrefour.Id,
            Barcode = "28"
        };

        var carrefourOrange = new StoreProduct
        {
            Name = "Orange",
            Description =
                "The orange is the fruit of various citrus species in the family Rutaceae; it primarily refers to Citrus × sinensis, which is also called sweet orange, to distinguish it from the related Citrus × aurantium, referred to as bitter orange.",
            OriginalPrice = 25m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(3)),
            BusinessId = carrefour.Id,
            Photo =
                "https://reimg-carrefour.mncdn.com/mnresize/600/600/productimage/30007733/30007733_0_MC/8812741787698_1548850739021.jpg",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = carrefour.Id,
            Barcode = "29"
        };

        var carrefourBanana = new StoreProduct
        {
            Name = "Banana",
            Description =
                "A banana is an elongated, edible fruit – botanically a berry – produced by several kinds of large herbaceous flowering plants in the genus Musa.",
            OriginalPrice = 55m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(3)),
            BusinessId = carrefour.Id,
            Photo =
                "https://reimg-carrefour.mncdn.com/mnresize/600/600/productimage/30038808/30038808_0_MC/8796528771122_1528879509786.jpg",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = carrefour.Id,
            Barcode = "30"
        };

        var carrefourNoodle = new StoreProduct
        {
            Name = "Noodle",
            Description =
                "Noodles are a type of food made from unleavened dough which is rolled flat and cut, stretched or extruded, into long strips or strings.",
            OriginalPrice = 16m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(4)),
            BusinessId = carrefour.Id,
            Photo =
                "https://reimg-carrefour.mncdn.com/mnresize/600/600/productimage/30247177/30247177_0_MC/8842509254706_1657725795069.jpg",
            ExpirationDate = DateTime.Now.AddDays(4),
            CreatedUserId = carrefour.Id,
            Barcode = "31"
        };

        var carrefourCannedFish = new StoreProduct
        {
            Name = "Canned Fish",
            Description =
                "Canned fish are fish which have been processed, sealed in an airtight container such as a sealed tin can, and subjected to heat.",
            OriginalPrice = 30m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(2)),
            BusinessId = carrefour.Id,
            Photo =
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTpAUnVFG8P6IvJ5FwWJT5AfGPR1njSmOnzrW2610keJw&s",
            ExpirationDate = DateTime.Now.AddDays(2),
            CreatedUserId = carrefour.Id,
            Barcode = "32"
        };

        var carrefourCannedSoup = new StoreProduct
        {
            Name = "Canned Soup",
            Description =
                "Canned soup is a type of soup designed for long shelf life and is a convenient form of food.",
            OriginalPrice = 12m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(20)),
            BusinessId = carrefour.Id,
            Photo =
                "https://reimg-carrefour.mncdn.com/mnresize/600/600/productimage/30412725/30412725_0_MC/8848930078770_1679989283688.jpg",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.Id,
            Barcode = "33"
        };

        var carrefourCannedFruit = new StoreProduct
        {
            Name = "Canned Fruit",
            Description =
                "Canned fruit is fruit that has been processed, sealed in an airtight container such as a sealed tin can, and subjected to heat.",
            OriginalPrice = 22m,
            PercentDiscount = PriceHelper.CalculateDiscountRate(DateTime.Now.AddDays(1)),
            BusinessId = carrefour.Id,
            Photo =
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSToMpQKEU1rAKDzVekIkV8YUrIuQN8FYVf_f1Pqc9VEg&s",
            ExpirationDate = DateTime.Now.AddDays(1),
            CreatedUserId = carrefour.Id,
            Barcode = "34"
        };

        var storeProducts = new List<StoreProduct>
        {
            bimMilk, bimCheese, bimButter, bimGarlic, bimCarrot, bimYogurt, bimCoffee, bimOil, bimPasta, bimOnion,
            bimApple, bimBanana, migrosMilk, migrosCheese, migrosPork, migrosChicken, migrosApple, migrosKetchup,
            migrosRice, migrosPasta, carrefourMilk, carrefourChocolate, carrefourPasta, carrefourFish, carrefourRice,
            carrefourEgg, carrefourPalmoil, carrefourTomato, carrefourOrange, carrefourBanana, carrefourNoodle,
            carrefourCannedFish, carrefourCannedSoup, carrefourCannedFruit
        };

        context.Product.AddRange(storeProducts);
        context.SaveChanges();

        #endregion Store Product

        #region Monitored Product

        var adarsMilk = new MonitoredProduct
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = adar.Id,
            OwnerId = adar.Id,
            Barcode = "35"
        };

        var adarsCheese = new MonitoredProduct
        {
            Name = "Cheese",
            Description =
                "Cheese is a dairy product, derived from milk and produced in wide ranges of flavors, textures and forms by coagulation of the milk protein casein.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = adar.Id,
            OwnerId = adar.Id,
            Barcode = "36"
        };

        var adarsChicken = new MonitoredProduct
        {
            Name = "Chicken",
            Description =
                "The chicken is a type of domesticated fowl, a subspecies of the red junglefowl. It is one of the most common and widespread domestic animals, with a total population of more than 30 billion as of 2020.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = adar.Id,
            OwnerId = adar.Id,
            Barcode = "37"
        };

        var adarsApple = new MonitoredProduct
        {
            Name = "Apple",
            Description =
                "An apple is an edible fruit produced by an apple tree. Apple trees are cultivated worldwide and are the most widely grown species in the genus Malus.",
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = adar.Id,
            OwnerId = adar.Id,
            Barcode = "38"
        };

        var adarsFish = new MonitoredProduct
        {
            Name = "Fish",
            Description =
                "Fish are gill-bearing aquatic craniate animals that lack limbs with digits. They form a sister group to the tunicates, together forming the olfactores.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = adar.Id,
            OwnerId = adar.Id,
            Barcode = "39"
        };

        var adarsPasta = new MonitoredProduct
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = adar.Id,
            OwnerId = adar.Id,
            Barcode = "40"
        };

        var adarsRice = new MonitoredProduct
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = adar.Id,
            OwnerId = adar.Id,
            Barcode = "41"
        };

        var adarsTea = new MonitoredProduct
        {
            Name = "Tea",
            Description =
                "Tea is a beverage made by steeping processed or fresh tea leaves in water. The term also refers to the plant Camellia sinensis from which the tea is made.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = adar.Id,
            OwnerId = adar.Id,
            Barcode = "42"
        };

        var adarsCoffee = new MonitoredProduct
        {
            Name = "Coffee",
            Description =
                "Coffee is a brewed drink prepared from roasted coffee beans, the seeds of berries from certain Coffea species.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = adar.Id,
            OwnerId = adar.Id,
            Barcode = "43"
        };

        var adarsWiskey = new MonitoredProduct
        {
            Name = "Wiskey",
            Description =
                "Whisky or whiskey is a type of distilled alcoholic beverage made from fermented grain mash. Various grains are used for different varieties, including barley, corn, rye, and wheat.",
            ExpirationDate = DateTime.Now.AddDays(365),
            CreatedUserId = adar.Id,
            OwnerId = adar.Id,
            Barcode = "44"
        };

        var erensYogurt = new MonitoredProduct
        {
            Name = "Yogurt",
            Description =
                "Yogurt, yoghurt or yoghourt is a food produced by bacterial fermentation of milk. The bacteria used to make yogurt are known as yogurt cultures.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = eren.Id,
            OwnerId = eren.Id,
            Barcode = "45"
        };

        var erensButter = new MonitoredProduct
        {
            Name = "Butter",
            Description =
                "Butter is a dairy product made from the fat and protein components of milk or cream. It is a semi-solid emulsion at room temperature, consisting of approximately 80% butterfat.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = eren.Id,
            OwnerId = eren.Id,
            Barcode = "46"
        };

        var erensOrange = new MonitoredProduct
        {
            Name = "Orange",
            Description =
                "The orange is the fruit of various citrus species in the family Rutaceae; it primarily refers to Citrus × sinensis, which is also called sweet orange, to distinguish it from the related Citrus × aurantium, referred to as bitter orange.",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = eren.Id,
            OwnerId = eren.Id,
            Barcode = "47"
        };

        var erensBanana = new MonitoredProduct
        {
            Name = "Banana",
            Description =
                "A banana is an elongated, edible fruit – botanically a berry – produced by several kinds of large herbaceous flowering plants in the genus Musa.",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = eren.Id,
            OwnerId = eren.Id,
            Barcode = "48"
        };

        var erensPasta = new MonitoredProduct
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = eren.Id,
            OwnerId = eren.Id,
            Barcode = "49"
        };

        var erensRice = new MonitoredProduct
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = eren.Id,
            OwnerId = eren.Id,
            Barcode = "50"
        };

        var erensVodka = new MonitoredProduct
        {
            Name = "Volka",
            Description =
                "Vodka is a clear distilled alcoholic beverage with different varieties originating in Poland, Russia, and Sweden. It is composed primarily of water and ethanol, but sometimes with traces of impurities and flavorings.",
            ExpirationDate = DateTime.Now.AddDays(365),
            CreatedUserId = eren.Id,
            OwnerId = eren.Id,
            Barcode = "51"
        };

        var erensTea = new MonitoredProduct
        {
            Name = "Tea",
            Description =
                "Tea is a beverage made by steeping processed or fresh tea leaves in water. The term also refers to the plant Camellia sinensis from which the tea is made.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = eren.Id,
            OwnerId = eren.Id,
            Barcode = "52"
        };

        var erensChocolate = new MonitoredProduct
        {
            Name = "Chocolate",
            Description =
                "Chocolate is a food product made from roasted and ground cacao seeds that is made in the form of a liquid, paste, or in a block, which may also be used as a flavoring ingredient in other foods.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = eren.Id,
            OwnerId = eren.Id,
            Barcode = "53"
        };

        var erensBeef = new MonitoredProduct
        {
            Name = "Beef",
            Description =
                "Beef is the culinary name for meat from cattle, particularly skeletal muscle. Humans have been eating beef since prehistoric times.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = eren.Id,
            OwnerId = eren.Id,
            Barcode = "54"
        };

        var erensLamb = new MonitoredProduct
        {
            Name = "Lamb",
            Description =
                "Lamb, hogget, and mutton, generically sheep meat, are the meat of domestic sheep, Ovis aries.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = eren.Id,
            OwnerId = eren.Id,
            Barcode = "55"
        };

        var barissMilk = new MonitoredProduct
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = baris.Id,
            OwnerId = baris.Id,
            Barcode = "56"
        };

        var barissBeer = new MonitoredProduct
        {
            Name = "Beer",
            Description =
                "Beer is one of the oldest and most widely consumed alcoholic drinks in the world, and the third most popular drink overall after water and tea.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = baris.Id,
            OwnerId = baris.Id,
            Barcode = "57"
        };

        var barissPork = new MonitoredProduct
        {
            Name = "Pork",
            Description =
                "Pork is the culinary name for the meat of a domestic pig. It is the most commonly consumed meat worldwide, with evidence of pig husbandry dating back to 5000 BC.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = baris.Id,
            OwnerId = baris.Id,
            Barcode = "58"
        };

        var barissChicken = new MonitoredProduct
        {
            Name = "Chicken",
            Description =
                "The chicken is a type of domesticated fowl, a subspecies of the red junglefowl. It is one of the most common and widespread domestic animals, with a total population of more than 30 billion as of 2020.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = baris.Id,
            OwnerId = baris.Id,
            Barcode = "59"
        };

        var barissPie = new MonitoredProduct
        {
            Name = "Pie",
            Description =
                "A pie is a baked dish that is usually made of a pastry dough casing that covers or completely contains a filling of various sweet or savory ingredients.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = baris.Id,
            OwnerId = baris.Id,
            Barcode = "60"
        };

        var monitoredProducts = new List<MonitoredProduct>
        {
            adarsMilk, adarsCheese, adarsChicken, adarsApple, adarsFish, adarsPasta, adarsRice, adarsTea, adarsCoffee,
            adarsWiskey, erensYogurt, erensButter, erensOrange, erensBanana, erensPasta, erensRice, erensVodka,
            erensTea,
            erensChocolate, erensBeef, erensLamb, barissMilk, barissBeer, barissPork, barissChicken, barissPie
        };

        context.Product.AddRange(monitoredProducts);
        context.SaveChanges();

        #endregion Monitored Product

        #region Store Product Category

        var categoryProducts = new List<CategoryProduct>
        {
            new() { ProductId = bimMilk.Id, CategoryId = diary.Id },
            new() { ProductId = bimCheese.Id, CategoryId = diary.Id },
            new() { ProductId = bimButter.Id, CategoryId = diary.Id },
            new() { ProductId = bimGarlic.Id, CategoryId = vegetable.Id },
            new() { ProductId = bimCarrot.Id, CategoryId = vegetable.Id },
            new() { ProductId = bimYogurt.Id, CategoryId = diary.Id },
            new() { ProductId = bimCoffee.Id, CategoryId = drink.Id },
            new() { ProductId = bimOil.Id, CategoryId = oil.Id },
            new() { ProductId = bimPasta.Id, CategoryId = grain.Id },
            new() { ProductId = bimOnion.Id, CategoryId = vegetable.Id },
            new() { ProductId = bimApple.Id, CategoryId = fruit.Id },
            new() { ProductId = bimBanana.Id, CategoryId = fruit.Id },
            new() { ProductId = migrosMilk.Id, CategoryId = diary.Id },
            new() { ProductId = migrosCheese.Id, CategoryId = diary.Id },
            new() { ProductId = migrosPork.Id, CategoryId = meat.Id },
            new() { ProductId = migrosChicken.Id, CategoryId = meat.Id },
            new() { ProductId = migrosApple.Id, CategoryId = fruit.Id },
            new() { ProductId = migrosKetchup.Id, CategoryId = sauce.Id },
            new() { ProductId = migrosRice.Id, CategoryId = grain.Id },
            new() { ProductId = migrosPasta.Id, CategoryId = grain.Id },
            new() { ProductId = carrefourMilk.Id, CategoryId = diary.Id },
            new() { ProductId = carrefourChocolate.Id, CategoryId = dessert.Id },
            new() { ProductId = carrefourPasta.Id, CategoryId = grain.Id },
            new() { ProductId = carrefourFish.Id, CategoryId = seafood.Id },
            new() { ProductId = carrefourRice.Id, CategoryId = grain.Id },
            new() { ProductId = carrefourEgg.Id, CategoryId = egg.Id },
            new() { ProductId = carrefourPalmoil.Id, CategoryId = oil.Id },
            new() { ProductId = carrefourTomato.Id, CategoryId = vegetable.Id },
            new() { ProductId = carrefourOrange.Id, CategoryId = fruit.Id },
            new() { ProductId = carrefourBanana.Id, CategoryId = fruit.Id },
            new() { ProductId = carrefourNoodle.Id, CategoryId = pasta.Id },
            new() { ProductId = carrefourCannedFish.Id, CategoryId = canned.Id },
            new() { ProductId = carrefourCannedSoup.Id, CategoryId = canned.Id },
            new() { ProductId = carrefourCannedFruit.Id, CategoryId = canned.Id },
            new() { ProductId = adarsMilk.Id, CategoryId = diary.Id },
            new() { ProductId = adarsCheese.Id, CategoryId = diary.Id },
            new() { ProductId = adarsChicken.Id, CategoryId = meat.Id },
            new() { ProductId = adarsApple.Id, CategoryId = fruit.Id },
            new() { ProductId = adarsFish.Id, CategoryId = seafood.Id },
            new() { ProductId = adarsPasta.Id, CategoryId = grain.Id },
            new() { ProductId = adarsRice.Id, CategoryId = grain.Id },
            new() { ProductId = adarsTea.Id, CategoryId = drink.Id },
            new() { ProductId = adarsCoffee.Id, CategoryId = drink.Id },
            new() { ProductId = adarsWiskey.Id, CategoryId = drink.Id },
            new() { ProductId = erensYogurt.Id, CategoryId = diary.Id },
            new() { ProductId = erensButter.Id, CategoryId = diary.Id },
            new() { ProductId = erensOrange.Id, CategoryId = fruit.Id },
            new() { ProductId = erensBanana.Id, CategoryId = fruit.Id },
            new() { ProductId = erensPasta.Id, CategoryId = grain.Id },
            new() { ProductId = erensRice.Id, CategoryId = grain.Id },
            new() { ProductId = erensVodka.Id, CategoryId = drink.Id },
            new() { ProductId = erensTea.Id, CategoryId = drink.Id },
            new() { ProductId = erensChocolate.Id, CategoryId = dessert.Id },
            new() { ProductId = erensBeef.Id, CategoryId = meat.Id },
            new() { ProductId = erensLamb.Id, CategoryId = meat.Id },
            new() { ProductId = barissMilk.Id, CategoryId = diary.Id },
            new() { ProductId = barissBeer.Id, CategoryId = drink.Id },
            new() { ProductId = barissPork.Id, CategoryId = meat.Id },
            new() { ProductId = barissChicken.Id, CategoryId = meat.Id },
            new() { ProductId = barissPie.Id, CategoryId = dessert.Id }
        };

        context.CategoryProduct.AddRange(categoryProducts);
        context.SaveChanges();

        #endregion Store Product Category

        # region Shopping List

        var shoppingListItems = new List<CustomerStoreProduct>
        {
            new() { ProductId = bimMilk.Id, CustomerId = adar.Id },
            new() { ProductId = bimCheese.Id, CustomerId = baris.Id },
            new() { ProductId = bimButter.Id, CustomerId = eren.Id },
            new() { ProductId = bimGarlic.Id, CustomerId = adar.Id },
            new() { ProductId = bimCarrot.Id, CustomerId = baris.Id },
            new() { ProductId = bimYogurt.Id, CustomerId = baris.Id },
            new() { ProductId = bimCoffee.Id, CustomerId = adar.Id },
            new() { ProductId = bimOil.Id, CustomerId = baris.Id },
            new() { ProductId = bimPasta.Id, CustomerId = eren.Id },
            new() { ProductId = bimOnion.Id, CustomerId = adar.Id },
            new() { ProductId = bimApple.Id, CustomerId = baris.Id },
            new() { ProductId = bimBanana.Id, CustomerId = eren.Id },
            new() { ProductId = migrosMilk.Id, CustomerId = adar.Id },
            new() { ProductId = migrosCheese.Id, CustomerId = baris.Id },
            new() { ProductId = migrosPork.Id, CustomerId = eren.Id },
            new() { ProductId = migrosChicken.Id, CustomerId = adar.Id },
            new() { ProductId = migrosApple.Id, CustomerId = baris.Id },
            new() { ProductId = migrosKetchup.Id, CustomerId = eren.Id },
            new() { ProductId = migrosRice.Id, CustomerId = adar.Id },
            new() { ProductId = migrosPasta.Id, CustomerId = baris.Id },
            new() { ProductId = carrefourMilk.Id, CustomerId = eren.Id },
            new() { ProductId = carrefourChocolate.Id, CustomerId = adar.Id },
            new() { ProductId = carrefourPasta.Id, CustomerId = baris.Id },
            new() { ProductId = carrefourFish.Id, CustomerId = eren.Id },
            new() { ProductId = carrefourRice.Id, CustomerId = adar.Id },
            new() { ProductId = carrefourEgg.Id, CustomerId = baris.Id },
            new() { ProductId = carrefourPalmoil.Id, CustomerId = eren.Id },
            new() { ProductId = carrefourTomato.Id, CustomerId = adar.Id },
            new() { ProductId = carrefourOrange.Id, CustomerId = baris.Id },
            new() { ProductId = carrefourBanana.Id, CustomerId = eren.Id },
            new() { ProductId = carrefourNoodle.Id, CustomerId = eren.Id }
        };

        context.CustomerStoreProduct.AddRange(shoppingListItems);
        context.SaveChanges();

        # endregion Shopping List

        # region Report

        var overMilkProductionInBursa = new Report
        {
            ReportName = "Over Milk Production In Bursa",
            ProductName = "Milk",
            Manufacturer = "Pınar",
            Location = "Bursa",
            StartDate = DateTime.Today.AddMonths(-12),
            EndDate = DateTime.Today.AddMonths(-6),
            SuppliedAmount = 24000,
            SoldAmount = 18000,
            Content =
                "The milk production in Bursa is 24000 liters in the last 12 months. The sold amount is 18000 liters. The production is 6000 liters more than the sold amount. The production should be decreased or the sales should be increased."
        };

        var notSoldChickensInEskisehir = new Report
        {
            ReportName = "Not Sold Chickens In Eskisehir",
            ProductName = "Chicken",
            Manufacturer = "Banvit",
            Location = "Eskisehir",
            StartDate = DateTime.Today.AddMonths(-6),
            EndDate = DateTime.Today,
            SuppliedAmount = 10000,
            SoldAmount = 5000,
            Content =
                "The chicken production in Eskisehir is 10000 chickens in the last 6 months. The sold amount is 5000 chickens. The production is 5000 chickens more than the sold amount. The production should be decreased or the sales should be increased."
        };

        var bigLossInFishProductionInTunceli = new Report
        {
            ReportName = "Big Loss In Fish Production In Tunceli",
            ProductName = "Fish",
            Manufacturer = "Dardanel",
            Location = "Tunceli",
            StartDate = DateTime.Today.AddMonths(-12),
            EndDate = DateTime.Today.AddMonths(-6),
            SuppliedAmount = 5000,
            SoldAmount = 1000,
            Content =
                "The fish production in Tunceli is 5000 fishes in the last 12 months. The sold amount is 1000 fishes. The production is 4000 fishes more than the sold amount. The production should be decreased or the sales should be increased."
        };

        var reports = new List<Report>
        {
            overMilkProductionInBursa, notSoldChickensInEskisehir, bigLossInFishProductionInTunceli
        };

        context.Report.AddRange(reports);
        context.SaveChanges();

        #endregion Report
    }
}