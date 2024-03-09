using Core.Constants;
using Core.Utils.Hashing;
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

public class EfSeeder : ISeeder
{
    public void Seed(IApplicationBuilder builder)
    {
        var context = builder.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<EfEfDbContext>();

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
            Role = UserRoles.Business
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
            Role = UserRoles.Business
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
            Role = UserRoles.Business
        };

        var businesses = new List<Business> { migros, carrefour, bim };

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

        #endregion Category

        #region Store Product

        // when expiration dates are coming, the discount rate will be increased
        var bimMilk = new StoreProduct
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = bim.Id
        };

        var bimCheese = new StoreProduct
        {
            Name = "Cheese",
            Description =
                "Cheese is a dairy product, derived from milk and produced in wide ranges of flavors, textures and forms by coagulation of the milk protein casein.",
            OriginalPrice = 9.99m,
            PercentDiscount = 30,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = bim.Id
        };

        var bimButter = new StoreProduct
        {
            Name = "Butter",
            Description =
                "Butter is a dairy product made from the fat and protein components of milk or cream. It is a semi-solid emulsion at room temperature, consisting of approximately 80% butterfat.",
            OriginalPrice = 7.99m,
            PercentDiscount = 40,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = bim.Id
        };

        var bimGarlic = new StoreProduct
        {
            Name = "Garlic",
            Description =
                "Garlic is a species in the onion genus, Allium. Its close relatives include the onion, shallot, leek, chive, and Chinese onion.",
            OriginalPrice = 3.99m,
            PercentDiscount = 70,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(10),
            CreatedUserId = bim.Id
        };

        var bimCarrot = new StoreProduct
        {
            Name = "Carrot",
            Description =
                "The carrot is a root vegetable, usually orange in color, though purple, black, red, white, and yellow cultivars exist.",
            OriginalPrice = 2.99m,
            PercentDiscount = 80,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = bim.Id
        };

        var bimYogurt = new StoreProduct
        {
            Name = "Yogurt",
            Description =
                "Yogurt, yoghurt or yoghourt is a food produced by bacterial fermentation of milk. The bacteria used to make yogurt are known as yogurt cultures.",
            OriginalPrice = 3.99m,
            PercentDiscount = 60,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = bim.Id
        };

        var bimCoffee = new StoreProduct
        {
            Name = "Coffee",
            Description =
                "Coffee is a brewed drink prepared from roasted coffee beans, the seeds of berries from certain Coffea species.",
            OriginalPrice = 19.99m,
            PercentDiscount = 20,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = bim.Id
        };

        var bimOil = new StoreProduct
        {
            Name = "Oil",
            Description =
                "An oil is any nonpolar chemical substance that is a viscous liquid at ambient temperatures and is both hydrophobic and lipophilic.",
            OriginalPrice = 11.99m,
            PercentDiscount = 10,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(42),
            CreatedUserId = bim.Id
        };

        var bimPasta = new StoreProduct
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = bim.Id
        };

        var bimOnion = new StoreProduct
        {
            Name = "Onion",
            Description =
                "The onion, also known as the bulb onion or common onion, is a vegetable that is the most widely cultivated species of the genus Allium.",
            OriginalPrice = 3.99m,
            PercentDiscount = 70,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(10),
            CreatedUserId = bim.Id
        };

        var bimApple = new StoreProduct
        {
            Name = "Apple",
            Description =
                "An apple is an edible fruit produced by an apple tree. Apple trees are cultivated worldwide and are the most widely grown species in the genus Malus.",
            OriginalPrice = 2.99m,
            PercentDiscount = 80,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = bim.Id
        };

        var bimBanana = new StoreProduct
        {
            Name = "Banana",
            Description =
                "A banana is an elongated, edible fruit – botanically a berry – produced by several kinds of large herbaceous flowering plants in the genus Musa.",
            OriginalPrice = 1.99m,
            PercentDiscount = 90,
            BusinessId = bim.Id,
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = bim.Id
        };

        var migrosMilk = new StoreProduct
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = migros.Id,
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = migros.Id
        };

        var migrosCheese = new StoreProduct
        {
            Name = "Cheese",
            Description =
                "Cheese is a dairy product, derived from milk and produced in wide ranges of flavors, textures and forms by coagulation of the milk protein casein.",
            OriginalPrice = 9.99m,
            PercentDiscount = 30,
            BusinessId = migros.Id,
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = migros.Id
        };

        var migrosPork = new StoreProduct
        {
            Name = "Pork",
            Description =
                "Pork is the culinary name for the meat of a domestic pig. It is the most commonly consumed meat worldwide, with evidence of pig husbandry dating back to 5000 BC.",
            OriginalPrice = 15.99m,
            PercentDiscount = 20,
            BusinessId = migros.Id,
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = migros.Id
        };

        var migrosChicken = new StoreProduct
        {
            Name = "Chicken",
            Description =
                "The chicken is a type of domesticated fowl, a subspecies of the red junglefowl. It is one of the most common and widespread domestic animals, with a total population of more than 30 billion as of 2020.",
            OriginalPrice = 12.99m,
            PercentDiscount = 40,
            BusinessId = migros.Id,
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = migros.Id
        };

        var migrosApple = new StoreProduct
        {
            Name = "Apple",
            Description =
                "An apple is an edible fruit produced by an apple tree. Apple trees are cultivated worldwide and are the most widely grown species in the genus Malus.",
            OriginalPrice = 2.99m,
            PercentDiscount = 80,
            BusinessId = migros.Id,
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = migros.Id
        };

        var migrosKetchup = new StoreProduct
        {
            Name = "Ketchup",
            Description =
                "Ketchup is a table condiment or sauce. Although original recipes used egg whites, mushrooms, oysters, grapes, mussels, or walnuts, among other ingredients, the unmodified term now typically refers to tomato ketchup.",
            OriginalPrice = 7.99m,
            PercentDiscount = 60,
            BusinessId = migros.Id,
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = migros.Id
        };

        var migrosRice = new StoreProduct
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            OriginalPrice = 9.99m,
            PercentDiscount = 50,
            BusinessId = migros.Id,
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = migros.Id
        };

        var migrosPasta = new StoreProduct
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = migros.Id,
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = migros.Id
        };

        var carrefourMilk = new StoreProduct
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = carrefour.Id
        };

        var carrefourChocolate = new StoreProduct
        {
            Name = "Chocolate",
            Description =
                "Chocolate is a food product made from roasted and ground cacao seeds that is made in the form of a liquid, paste, or in a block, which may also be used as a flavoring ingredient in other foods.",
            OriginalPrice = 11.99m,
            PercentDiscount = 40,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = carrefour.Id
        };

        var carrefourPasta = new StoreProduct
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.Id
        };

        var carrefourFish = new StoreProduct
        {
            Name = "Fish",
            Description =
                "Fish are gill-bearing aquatic craniate animals that lack limbs with digits. They form a sister group to the tunicates, together forming the olfactores.",
            OriginalPrice = 19.99m,
            PercentDiscount = 20,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = carrefour.Id
        };

        var carrefourRice = new StoreProduct
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            OriginalPrice = 9.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.Id
        };

        var carrefourEgg = new StoreProduct
        {
            Name = "Egg",
            Description =
                "An egg is the organic vessel containing the zygote in which an animal embryo develops until it can survive on its own, at which point the animal hatches.",
            OriginalPrice = 3.99m,
            PercentDiscount = 70,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(10),
            CreatedUserId = carrefour.Id
        };

        var carrefourPalmoil = new StoreProduct
        {
            Name = "Palm Oil",
            Description =
                "Palm oil is an edible vegetable oil derived from the mesocarp of the fruit of the oil palms, primarily the African oil palm Elaeis guineensis, and to a lesser extent from the American oil palm Elaeis oleifera and the maripa palm Attalea maripa.",
            OriginalPrice = 7.99m,
            PercentDiscount = 60,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = carrefour.Id
        };

        var carrefourTomato = new StoreProduct
        {
            Name = "Tomato",
            Description =
                "The tomato is the edible, often red berry of the plant Solanum lycopersicum, commonly known as a tomato plant.",
            OriginalPrice = 3.99m,
            PercentDiscount = 80,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = carrefour.Id
        };

        var carrefourOrange = new StoreProduct
        {
            Name = "Orange",
            Description =
                "The orange is the fruit of various citrus species in the family Rutaceae; it primarily refers to Citrus × sinensis, which is also called sweet orange, to distinguish it from the related Citrus × aurantium, referred to as bitter orange.",
            OriginalPrice = 2.99m,
            PercentDiscount = 90,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = carrefour.Id
        };

        var carrefourBanana = new StoreProduct
        {
            Name = "Banana",
            Description =
                "A banana is an elongated, edible fruit – botanically a berry – produced by several kinds of large herbaceous flowering plants in the genus Musa.",
            OriginalPrice = 1.99m,
            PercentDiscount = 90,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = carrefour.Id
        };

        var carrefourNoodle = new StoreProduct
        {
            Name = "Noodle",
            Description =
                "Noodles are a type of food made from unleavened dough which is rolled flat and cut, stretched or extruded, into long strips or strings.",
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.Id
        };

        var carrefourCannedFish = new StoreProduct
        {
            Name = "Canned Fish",
            Description =
                "Canned fish are fish which have been processed, sealed in an airtight container such as a sealed tin can, and subjected to heat.",
            OriginalPrice = 9.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.Id
        };

        var carrefourCannedSoup = new StoreProduct
        {
            Name = "Canned Soup",
            Description =
                "Canned soup is a type of soup designed for long shelf life and is a convenient form of food.",
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.Id
        };

        var carrefourCannedFruit = new StoreProduct
        {
            Name = "Canned Fruit",
            Description =
                "Canned fruit is fruit that has been processed, sealed in an airtight container such as a sealed tin can, and subjected to heat.",
            OriginalPrice = 7.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.Id,
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.Id
        };

        var storeProducts = new List<StoreProduct>
        {
            bimMilk, bimCheese, bimButter, bimGarlic, bimCarrot, bimYogurt, bimCoffee, bimOil, bimPasta, bimOnion,
            bimApple, bimBanana, migrosMilk, migrosCheese, migrosPork, migrosChicken, migrosApple, migrosKetchup,
            migrosRice, migrosPasta, carrefourMilk, carrefourChocolate, carrefourPasta, carrefourFish, carrefourRice,
            carrefourEgg, carrefourPalmoil, carrefourTomato, carrefourOrange, carrefourBanana, carrefourNoodle,
            carrefourCannedFish, carrefourCannedSoup, carrefourCannedFruit
        };

        #endregion Store Product

        #region Monitored Product

        var adarsMilk = new MonitoredProduct
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = adar.Id
        };

        var adarsCheese = new MonitoredProduct
        {
            Name = "Cheese",
            Description =
                "Cheese is a dairy product, derived from milk and produced in wide ranges of flavors, textures and forms by coagulation of the milk protein casein.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = adar.Id
        };

        var adarsChicken = new MonitoredProduct
        {
            Name = "Chicken",
            Description =
                "The chicken is a type of domesticated fowl, a subspecies of the red junglefowl. It is one of the most common and widespread domestic animals, with a total population of more than 30 billion as of 2020.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = adar.Id
        };

        var adarsApple = new MonitoredProduct
        {
            Name = "Apple",
            Description =
                "An apple is an edible fruit produced by an apple tree. Apple trees are cultivated worldwide and are the most widely grown species in the genus Malus.",
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = adar.Id
        };

        var adarsFish = new MonitoredProduct
        {
            Name = "Fish",
            Description =
                "Fish are gill-bearing aquatic craniate animals that lack limbs with digits. They form a sister group to the tunicates, together forming the olfactores.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = adar.Id
        };

        var adarsPasta = new MonitoredProduct
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = adar.Id
        };

        var adarsRice = new MonitoredProduct
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = adar.Id
        };

        var adarsTea = new MonitoredProduct
        {
            Name = "Tea",
            Description =
                "Tea is a beverage made by steeping processed or fresh tea leaves in water. The term also refers to the plant Camellia sinensis from which the tea is made.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = adar.Id
        };

        var adarsCoffee = new MonitoredProduct
        {
            Name = "Coffee",
            Description =
                "Coffee is a brewed drink prepared from roasted coffee beans, the seeds of berries from certain Coffea species.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = adar.Id
        };

        var adarsWiskey = new MonitoredProduct
        {
            Name = "Wiskey",
            Description =
                "Whisky or whiskey is a type of distilled alcoholic beverage made from fermented grain mash. Various grains are used for different varieties, including barley, corn, rye, and wheat.",
            ExpirationDate = DateTime.Now.AddDays(365),
            CreatedUserId = adar.Id
        };

        var erensYogurt = new MonitoredProduct
        {
            Name = "Yogurt",
            Description =
                "Yogurt, yoghurt or yoghourt is a food produced by bacterial fermentation of milk. The bacteria used to make yogurt are known as yogurt cultures.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = eren.Id
        };

        var erensButter = new MonitoredProduct
        {
            Name = "Butter",
            Description =
                "Butter is a dairy product made from the fat and protein components of milk or cream. It is a semi-solid emulsion at room temperature, consisting of approximately 80% butterfat.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = eren.Id
        };

        var erensOrange = new MonitoredProduct
        {
            Name = "Orange",
            Description =
                "The orange is the fruit of various citrus species in the family Rutaceae; it primarily refers to Citrus × sinensis, which is also called sweet orange, to distinguish it from the related Citrus × aurantium, referred to as bitter orange.",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = eren.Id
        };

        var erensBanana = new MonitoredProduct
        {
            Name = "Banana",
            Description =
                "A banana is an elongated, edible fruit – botanically a berry – produced by several kinds of large herbaceous flowering plants in the genus Musa.",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = eren.Id
        };

        var erensPasta = new MonitoredProduct
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = eren.Id
        };

        var erensRice = new MonitoredProduct
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = eren.Id
        };

        var erensVodka = new MonitoredProduct
        {
            Name = "Volka",
            Description =
                "Vodka is a clear distilled alcoholic beverage with different varieties originating in Poland, Russia, and Sweden. It is composed primarily of water and ethanol, but sometimes with traces of impurities and flavorings.",
            ExpirationDate = DateTime.Now.AddDays(365),
            CreatedUserId = eren.Id
        };

        var erensTea = new MonitoredProduct
        {
            Name = "Tea",
            Description =
                "Tea is a beverage made by steeping processed or fresh tea leaves in water. The term also refers to the plant Camellia sinensis from which the tea is made.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = eren.Id
        };

        var erensChocolate = new MonitoredProduct
        {
            Name = "Chocolate",
            Description =
                "Chocolate is a food product made from roasted and ground cacao seeds that is made in the form of a liquid, paste, or in a block, which may also be used as a flavoring ingredient in other foods.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = eren.Id
        };

        var erensBeef = new MonitoredProduct
        {
            Name = "Beef",
            Description =
                "Beef is the culinary name for meat from cattle, particularly skeletal muscle. Humans have been eating beef since prehistoric times.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = eren.Id
        };

        var erensLamb = new MonitoredProduct
        {
            Name = "Lamb",
            Description =
                "Lamb, hogget, and mutton, generically sheep meat, are the meat of domestic sheep, Ovis aries.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = eren.Id
        };

        var barissMilk = new MonitoredProduct
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = baris.Id
        };

        var barissBeer = new MonitoredProduct
        {
            Name = "Beer",
            Description =
                "Beer is one of the oldest and most widely consumed alcoholic drinks in the world, and the third most popular drink overall after water and tea.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = baris.Id
        };

        var barissPork = new MonitoredProduct
        {
            Name = "Pork",
            Description =
                "Pork is the culinary name for the meat of a domestic pig. It is the most commonly consumed meat worldwide, with evidence of pig husbandry dating back to 5000 BC.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = baris.Id
        };

        var barissChicken = new MonitoredProduct
        {
            Name = "Chicken",
            Description =
                "The chicken is a type of domesticated fowl, a subspecies of the red junglefowl. It is one of the most common and widespread domestic animals, with a total population of more than 30 billion as of 2020.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = baris.Id
        };

        var barissPie = new MonitoredProduct
        {
            Name = "Pie",
            Description =
                "A pie is a baked dish that is usually made of a pastry dough casing that covers or completely contains a filling of various sweet or savory ingredients.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = baris.Id
        };

        var monitoredProducts = new List<MonitoredProduct>
        {
            adarsMilk, adarsCheese, adarsChicken, adarsApple, adarsFish, adarsPasta, adarsRice, adarsTea, adarsCoffee,
            adarsWiskey, erensYogurt, erensButter, erensOrange, erensBanana, erensPasta, erensRice, erensVodka,
            erensTea,
            erensChocolate, erensBeef, erensLamb, barissMilk, barissBeer, barissPork, barissChicken, barissPie
        };

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

        #endregion Store Product Category

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

        #endregion Report

        context.User.AddRange(customers);
        context.User.AddRange(businesses);
        context.Category.AddRange(categories);
        context.Product.AddRange(storeProducts);
        context.Product.AddRange(monitoredProducts);
        context.CategoryProduct.AddRange(categoryProducts);
        context.Report.AddRange(reports);

        context.SaveChanges();
    }
}