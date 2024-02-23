using Core.Constants;
using Core.Utils.Hashing;
using Core.Utils.Seed;
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
            .GetRequiredService<EfDbContext>();

        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();

        // If there is no data in the database, then seed the database
        if (context.Category.Any())
            return;

        #region Customer

        HashingHelper.CreatePasswordHash("Su123456789.", out var passwordHash, out var passwordSalt);
        var adarParent = new User
        {
            Id = Guid.Empty,
            Username = "adars",
            Email = "adarsonmez@outlook.com",
            PhoneNumber = "+905452977501",
            UseMultiFactorAuthentication = true,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        var adar = new Customer // Super User
        {
            FirstName = "Adar",
            LastName = "Sönmez",
            Role = UserRoles.Admin,
            UserId = adarParent.Id
        };

        HashingHelper.CreatePasswordHash("Eren123456789.", out passwordHash, out passwordSalt);
        var erenParent = new User
        {
            Username = "erenk",
            PhoneNumber = "+905365070289",
            Email = "erenkarakaya93@gmail.com",
            UseMultiFactorAuthentication = true,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        var eren = new Customer
        {
            FirstName = "Eren",
            LastName = "Karakaya",
            Role = UserRoles.Customer,
            UserId = erenParent.Id
        };

        HashingHelper.CreatePasswordHash("Baris123456789.", out passwordHash, out passwordSalt);
        var barisParent = new User
        {
            Username = "barisa",
            PhoneNumber = "+90 5061809945",
            Email = "baris.acr99@gmail.com", UseMultiFactorAuthentication = false,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        var baris = new Customer
        {
            FirstName = "Barış",
            LastName = "Acar",
            Role = UserRoles.Customer,
            UserId = barisParent.Id
        };

        var customers = new List<Customer> { adar, eren, baris };
        var parentCustomers = new List<User> { adarParent, erenParent, barisParent };

        #endregion Customer

        #region Business

        HashingHelper.CreatePasswordHash("Migros123456789.", out passwordHash, out passwordSalt);
        var parentMigros = new User
        {
            Username = "migros",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Email = "migros@gmail.com",
            PhoneNumber = "+908504550444",
            UseMultiFactorAuthentication = false
        };

        var migros = new Business
        {
            Name = "Migros",
            Description =
                "Migros Ticaret A.Ş. (Migros Ticaret T.A.Ş.) is a Turkish retail company, part of the larger Koç Holding. It is one of the two big players in the Turkish supermarket industry, the other one being BİM. It has more than 2,000 branches in Turkey and 5 branches in North Macedonia.",
            Address = "Migros Ticaret A.Ş. Esentepe Mah. Büyükdere Cad. No: 185 34394 Şişli / İstanbul",
            Website = "https://www.migros.com.tr/",
            Logo = "https://www.logovector.org/wp-content/uploads/logos/png/m/migros_logo.png",
            UserId = parentMigros.Id
        };

        HashingHelper.CreatePasswordHash("Carrefour123456789.", out passwordHash, out passwordSalt);
        var parentCarrefour = new User
        {
            Username = "carrefour",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Email = "carrefour@gmail.com",
            PhoneNumber = "+905452977501",
            UseMultiFactorAuthentication = true
        };

        var carrefour = new Business
        {
            Name = "Carrefour",
            Description =
                "Carrefour S.A. is a French multinational retail corporation headquartered in Massy, France. The eighth-largest retailer in the world by revenue, it operates a chain of hypermarkets, groceries and convenience stores, which as of January 2021, comprises its 12,225 stores in over 30 countries.",
            Address = "Carrefour S.A. 33, avenue Émile Zola 92100 Boulogne-Billancourt France",
            Website = "https://www.carrefour.com/",
            Logo = "https://www.drupal.org/files/carrefour-logo.jpg",
            UserId = parentCarrefour.Id
        };

        HashingHelper.CreatePasswordHash("BIM123456789.", out passwordHash, out passwordSalt);
        var parentBim = new User
        {
            Username = "bim",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Email = "bim@gmail.com",
            PhoneNumber = "+908504550444",
            UseMultiFactorAuthentication = false
        };

        var bim = new Business
        {
            Name = "BİM",
            Description =
                "BİM Birleşik Mağazalar A.Ş. is a Turkish retail company, known for offering a limited range of basic food items and consumer goods at competitive prices. It is one of the two big players in the Turkish supermarket industry, the other one being Migros. It has more than 7,000 branches in Turkey and 1 branch in Morocco.",
            Address = "BİM Birleşik Mağazalar A.Ş. Esentepe Mah. Büyükdere Cad. No: 185 34394 Şişli / İstanbul",
            Website = "https://www.bim.com.tr/",
            Logo = "https://www.logovector.org/wp-content/uploads/logos/png/b/bim_logo.png",
            UserId = parentBim.Id
        };

        var businesses = new List<Business> { migros, carrefour, bim };
        var parentBusinesses = new List<User> { parentMigros, parentCarrefour, parentBim };

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
        var parentBimMilk = new Product
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = bim.UserId
        };

        var bimMilk = new StoreProduct
        {
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = bim.UserId,
            ProductId = parentBimMilk.Id
        };

        var parentBimCheese = new Product
        {
            Name = "Cheese",
            Description =
                "Cheese is a dairy product, derived from milk and produced in wide ranges of flavors, textures and forms by coagulation of the milk protein casein.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = bim.UserId
        };

        var bimCheese = new StoreProduct
        {
            OriginalPrice = 9.99m,
            PercentDiscount = 30,
            BusinessId = bim.UserId,
            ProductId = parentBimCheese.Id
        };

        var parentBimButter = new Product
        {
            Name = "Butter",
            Description =
                "Butter is a dairy product made from the fat and protein components of milk or cream. It is a semi-solid emulsion at room temperature, consisting of approximately 80% butterfat.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = bim.UserId
        };

        var bimButter = new StoreProduct
        {
            OriginalPrice = 7.99m,
            PercentDiscount = 40,
            BusinessId = bim.UserId,
            ProductId = parentBimButter.Id
        };

        var parentBimGarlic = new Product
        {
            Name = "Garlic",
            Description =
                "Garlic is a species in the onion genus, Allium. Its close relatives include the onion, shallot, leek, chive, and Chinese onion.",
            ExpirationDate = DateTime.Now.AddDays(10),
            CreatedUserId = bim.UserId
        };


        var bimGarlic = new StoreProduct
        {
            OriginalPrice = 3.99m,
            PercentDiscount = 70,
            BusinessId = bim.UserId,
            ProductId = parentBimGarlic.Id
        };

        var parentBimCarrot = new Product
        {
            Name = "Carrot",
            Description =
                "The carrot is a root vegetable, usually orange in color, though purple, black, red, white, and yellow cultivars exist.",
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = bim.UserId
        };

        var bimCarrot = new StoreProduct
        {
            OriginalPrice = 2.99m,
            PercentDiscount = 80,
            BusinessId = bim.UserId,
            ProductId = parentBimCarrot.Id
        };

        var parentBimYogurt = new Product
        {
            Name = "Yogurt",
            Description =
                "Yogurt, yoghurt or yoghourt is a food produced by bacterial fermentation of milk. The bacteria used to make yogurt are known as yogurt cultures.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = bim.UserId
        };

        var bimYogurt = new StoreProduct
        {
            OriginalPrice = 3.99m,
            PercentDiscount = 60,
            BusinessId = bim.UserId,
            ProductId = parentBimYogurt.Id
        };

        var parentBimCoffee = new Product
        {
            Name = "Coffee",
            Description =
                "Coffee is a brewed drink prepared from roasted coffee beans, the seeds of berries from certain Coffea species.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = bim.UserId
        };

        var bimCoffee = new StoreProduct
        {
            OriginalPrice = 19.99m,
            PercentDiscount = 20,
            BusinessId = bim.UserId,
            ProductId = parentBimCoffee.Id
        };

        var parentBimOil = new Product
        {
            Name = "Oil",
            Description =
                "An oil is any nonpolar chemical substance that is a viscous liquid at ambient temperatures and is both hydrophobic and lipophilic.",
            ExpirationDate = DateTime.Now.AddDays(42),
            CreatedUserId = bim.UserId
        };

        var bimOil = new StoreProduct
        {
            OriginalPrice = 11.99m,
            PercentDiscount = 10,
            BusinessId = bim.UserId,
            ProductId = parentBimOil.Id
        };

        var parentBimPasta = new Product
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = bim.UserId
        };

        var bimPasta = new StoreProduct
        {
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = bim.UserId,
            ProductId = parentBimPasta.Id
        };

        var parentBimOnion = new Product
        {
            Name = "Onion",
            Description =
                "The onion, also known as the bulb onion or common onion, is a vegetable that is the most widely cultivated species of the genus Allium.",
            ExpirationDate = DateTime.Now.AddDays(10),
            CreatedUserId = bim.UserId
        };

        var bimOnion = new StoreProduct
        {
            OriginalPrice = 3.99m,
            PercentDiscount = 70,
            BusinessId = bim.UserId,
            ProductId = parentBimOnion.Id
        };

        var parentBimApple = new Product
        {
            Name = "Apple",
            Description =
                "An apple is an edible fruit produced by an apple tree. Apple trees are cultivated worldwide and are the most widely grown species in the genus Malus.",
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = bim.UserId
        };

        var bimApple = new StoreProduct
        {
            OriginalPrice = 2.99m,
            PercentDiscount = 80,
            BusinessId = bim.UserId,
            ProductId = parentBimApple.Id
        };

        var parentBimBanana = new Product
        {
            Name = "Banana",
            Description =
                "A banana is an elongated, edible fruit – botanically a berry – produced by several kinds of large herbaceous flowering plants in the genus Musa.",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = bim.UserId
        };

        var bimBanana = new StoreProduct
        {
            OriginalPrice = 1.99m,
            PercentDiscount = 90,
            BusinessId = bim.UserId,
            ProductId = parentBimBanana.Id
        };

        var parentMigrosMilk = new Product
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = migros.UserId
        };

        var migrosMilk = new StoreProduct
        {
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = migros.UserId,
            ProductId = parentMigrosMilk.Id
        };

        var parentMigrosCheese = new Product
        {
            Name = "Cheese",
            Description =
                "Cheese is a dairy product, derived from milk and produced in wide ranges of flavors, textures and forms by coagulation of the milk protein casein.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = migros.UserId
        };

        var migrosCheese = new StoreProduct
        {
            OriginalPrice = 9.99m,
            PercentDiscount = 30,
            BusinessId = migros.UserId,
            ProductId = parentMigrosCheese.Id
        };

        var parentMigrosPork = new Product
        {
            Name = "Pork",
            Description =
                "Pork is the culinary name for the meat of a domestic pig. It is the most commonly consumed meat worldwide, with evidence of pig husbandry dating back to 5000 BC.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = migros.UserId
        };

        var migrosPork = new StoreProduct
        {
            OriginalPrice = 15.99m,
            PercentDiscount = 20,
            BusinessId = migros.UserId,
            ProductId = parentMigrosPork.Id
        };

        var parentMigrosChicken = new Product
        {
            Name = "Chicken",
            Description =
                "The chicken is a type of domesticated fowl, a subspecies of the red junglefowl. It is one of the most common and widespread domestic animals, with a total population of more than 30 billion as of 2020.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = migros.UserId
        };

        var migrosChicken = new StoreProduct
        {
            OriginalPrice = 12.99m,
            PercentDiscount = 40,
            BusinessId = migros.UserId,
            ProductId = parentMigrosChicken.Id
        };

        var parentMigrosApple = new Product
        {
            Name = "Apple",
            Description =
                "An apple is an edible fruit produced by an apple tree. Apple trees are cultivated worldwide and are the most widely grown species in the genus Malus.",
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = migros.UserId
        };

        var migrosApple = new StoreProduct
        {
            OriginalPrice = 2.99m,
            PercentDiscount = 80,
            BusinessId = migros.UserId,
            ProductId = parentMigrosApple.Id
        };

        var parentMigrosKetchup = new Product
        {
            Name = "Ketchup",
            Description =
                "Ketchup is a table condiment or sauce. Although original recipes used egg whites, mushrooms, oysters, grapes, mussels, or walnuts, among other ingredients, the unmodified term now typically refers to tomato ketchup.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = migros.UserId
        };

        var migrosKetchup = new StoreProduct
        {
            OriginalPrice = 7.99m,
            PercentDiscount = 60,
            BusinessId = migros.UserId,
            ProductId = parentMigrosKetchup.Id
        };

        var parentMigrosRice = new Product
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = migros.UserId
        };

        var migrosRice = new StoreProduct
        {
            OriginalPrice = 9.99m,
            PercentDiscount = 50,
            BusinessId = migros.UserId,
            ProductId = parentMigrosRice.Id
        };

        var parentMigrosPasta = new Product
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = migros.UserId
        };

        var migrosPasta = new StoreProduct
        {
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = migros.UserId,
            ProductId = parentMigrosPasta.Id
        };

        var parentCarrefourMilk = new Product
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = carrefour.UserId
        };

        var carrefourMilk = new StoreProduct
        {
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourMilk.Id
        };

        var parentCarrefourChocolate = new Product
        {
            Name = "Chocolate",
            Description =
                "Chocolate is a food product made from roasted and ground cacao seeds that is made in the form of a liquid, paste, or in a block, which may also be used as a flavoring ingredient in other foods.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = carrefour.UserId
        };

        var carrefourChocolate = new StoreProduct
        {
            OriginalPrice = 11.99m,
            PercentDiscount = 40,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourChocolate.Id
        };

        var parentCarrefourPasta = new Product
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.UserId
        };

        var carrefourPasta = new StoreProduct
        {
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourPasta.Id
        };

        var parentCarrefourFish = new Product
        {
            Name = "Fish",
            Description =
                "Fish are gill-bearing aquatic craniate animals that lack limbs with digits. They form a sister group to the tunicates, together forming the olfactores.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = carrefour.UserId
        };

        var carrefourFish = new StoreProduct
        {
            OriginalPrice = 19.99m,
            PercentDiscount = 20,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourFish.Id
        };

        var parentCarrefourRice = new Product
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.UserId
        };

        var carrefourRice = new StoreProduct
        {
            OriginalPrice = 9.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourRice.Id
        };

        var parentCarrefourEgg = new Product
        {
            Name = "Egg",
            Description =
                "An egg is the organic vessel containing the zygote in which an animal embryo develops until it can survive on its own, at which point the animal hatches.",
            ExpirationDate = DateTime.Now.AddDays(10),
            CreatedUserId = carrefour.UserId
        };

        var carrefourEgg = new StoreProduct
        {
            OriginalPrice = 3.99m,
            PercentDiscount = 70,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourEgg.Id
        };

        var parentCarrefourPalmoil = new Product
        {
            Name = "Palm Oil",
            Description =
                "Palm oil is an edible vegetable oil derived from the mesocarp of the fruit of the oil palms, primarily the African oil palm Elaeis guineensis, and to a lesser extent from the American oil palm Elaeis oleifera and the maripa palm Attalea maripa.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = carrefour.UserId
        };

        var carrefourPalmoil = new StoreProduct
        {
            OriginalPrice = 7.99m,
            PercentDiscount = 60,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourPalmoil.Id
        };

        var parentCarrefourTomato = new Product
        {
            Name = "Tomato",
            Description =
                "The tomato is the edible, often red, berry of the plant Solanum lycopersicum, commonly known as a tomato plant.",
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = carrefour.UserId
        };

        var carrefourTomato = new StoreProduct
        {
            OriginalPrice = 3.99m,
            PercentDiscount = 80,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourTomato.Id
        };

        var parentCarrefourOrange = new Product
        {
            Name = "Orange",
            Description =
                "The orange is the fruit of various citrus species in the family Rutaceae; it primarily refers to Citrus × sinensis, which is also called sweet orange, to distinguish it from the related Citrus × aurantium, referred to as bitter orange.",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = carrefour.UserId
        };

        var carrefourOrange = new StoreProduct
        {
            OriginalPrice = 2.99m,
            PercentDiscount = 90,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourOrange.Id
        };

        var parentCarrefourBanana = new Product
        {
            Name = "Banana",
            Description =
                "A banana is an elongated, edible fruit – botanically a berry – produced by several kinds of large herbaceous flowering plants in the genus Musa.",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = carrefour.UserId
        };

        var carrefourBanana = new StoreProduct
        {
            OriginalPrice = 1.99m,
            PercentDiscount = 90,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourBanana.Id
        };

        var parentCarrefourNoodle = new Product
        {
            Name = "Noodle",
            Description =
                "Noodles are a type of food made from unleavened dough which is rolled flat and cut, stretched or extruded, into long strips or strings.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.UserId
        };

        var carrefourNoodle = new StoreProduct
        {
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourNoodle.Id
        };

        var parentCarrefourCannedFish = new Product
        {
            Name = "Canned Fish",
            Description =
                "Canned fish are fish which have been processed, sealed in an airtight container such as a sealed tin can, and subjected to heat.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.UserId
        };

        var carrefourCannedFish = new StoreProduct
        {
            OriginalPrice = 9.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourCannedFish.Id
        };

        var parentCarrefourCannedSoup = new Product
        {
            Name = "Canned Soup",
            Description =
                "Canned soup is a type of soup designed for long shelf life and is a convenient form of food.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.UserId
        };

        var carrefourCannedSoup = new StoreProduct
        {
            OriginalPrice = 5.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourCannedSoup.Id
        };

        var parentCarrefourCannedFruit = new Product
        {
            Name = "Canned Fruit",
            Description =
                "Canned fruit is fruit that has been processed, sealed in an airtight container such as a sealed tin can, and subjected to heat.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = carrefour.UserId
        };

        var carrefourCannedFruit = new StoreProduct
        {
            OriginalPrice = 7.99m,
            PercentDiscount = 50,
            BusinessId = carrefour.UserId,
            ProductId = parentCarrefourCannedFruit.Id
        };

        var storeProducts = new List<StoreProduct>
        {
            bimMilk, bimCheese, bimButter, bimGarlic, bimCarrot, bimYogurt, bimCoffee, bimOil, bimPasta, bimOnion,
            bimApple, bimBanana, migrosMilk, migrosCheese, migrosPork, migrosChicken, migrosApple, migrosKetchup,
            migrosRice, migrosPasta, carrefourMilk, carrefourChocolate, carrefourPasta, carrefourFish, carrefourRice,
            carrefourEgg, carrefourPalmoil, carrefourTomato, carrefourOrange, carrefourBanana, carrefourNoodle,
            carrefourCannedFish, carrefourCannedSoup, carrefourCannedFruit
        };

        var parentStoreProducts = new List<Product>
        {
            parentBimMilk, parentBimCheese, parentBimButter, parentBimGarlic, parentBimCarrot, parentBimYogurt,
            parentBimCoffee, parentBimOil, parentBimPasta, parentBimOnion, parentBimApple, parentBimBanana,
            parentMigrosMilk, parentMigrosCheese, parentMigrosPork, parentMigrosChicken, parentMigrosApple,
            parentMigrosKetchup, parentMigrosRice, parentMigrosPasta, parentCarrefourMilk, parentCarrefourChocolate,
            parentCarrefourPasta, parentCarrefourFish, parentCarrefourRice, parentCarrefourEgg, parentCarrefourPalmoil,
            parentCarrefourTomato, parentCarrefourOrange, parentCarrefourBanana, parentCarrefourNoodle,
            parentCarrefourCannedFish, parentCarrefourCannedSoup, parentCarrefourCannedFruit
        };

        #endregion Store Product

        #region Monitored Product

        var parentAdarsMilk = new Product
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = adar.UserId
        };

        var adarsMilk = new MonitoredProduct
        {
            ProductId = parentAdarsMilk.Id
        };

        var parentAdarsCheese = new Product
        {
            Name = "Cheese",
            Description =
                "Cheese is a dairy product, derived from milk and produced in wide ranges of flavors, textures and forms by coagulation of the milk protein casein.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = adar.UserId
        };

        var adarsCheese = new MonitoredProduct
        {
            ProductId = parentAdarsCheese.Id
        };

        var parentAdarsChicken = new Product
        {
            Name = "Chicken",
            Description =
                "The chicken is a type of domesticated fowl, a subspecies of the red junglefowl. It is one of the most common and widespread domestic animals, with a total population of more than 30 billion as of 2020.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = adar.UserId
        };


        var adarsChicken = new MonitoredProduct
        {
            ProductId = parentAdarsChicken.Id
        };

        var parentAdarsApple = new Product
        {
            Name = "Apple",
            Description =
                "An apple is an edible fruit produced by an apple tree. Apple trees are cultivated worldwide and are the most widely grown species in the genus Malus.",
            ExpirationDate = DateTime.Now.AddDays(5),
            CreatedUserId = adar.UserId
        };

        var adarsApple = new MonitoredProduct
        {
            ProductId = parentAdarsApple.Id
        };

        var parentAdarsFish = new Product
        {
            Name = "Fish",
            Description =
                "Fish are gill-bearing aquatic craniate animals that lack limbs with digits. They form a sister group to the tunicates, together forming the olfactores.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = adar.UserId
        };

        var adarsFish = new MonitoredProduct
        {
            ProductId = parentAdarsFish.Id
        };

        var parentAdarsPasta = new Product
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = adar.UserId
        };

        var adarsPasta = new MonitoredProduct
        {
            ProductId = parentAdarsPasta.Id
        };

        var parentAdarsRice = new Product
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = adar.UserId
        };

        var adarsRice = new MonitoredProduct
        {
            ProductId = parentAdarsRice.Id
        };

        var parentAdarsTea = new Product
        {
            Name = "Tea",
            Description =
                "Tea is a beverage made by steeping processed or fresh tea leaves in water. The term also refers to the plant Camellia sinensis from which the tea is made.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = adar.UserId
        };

        var adarsTea = new MonitoredProduct
        {
            ProductId = parentAdarsTea.Id
        };

        var parentAdarsCoffee = new Product
        {
            Name = "Coffee",
            Description =
                "Coffee is a brewed drink prepared from roasted coffee beans, the seeds of berries from certain Coffea species.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = adar.UserId
        };

        var adarsCoffee = new MonitoredProduct
        {
            ProductId = parentAdarsCoffee.Id
        };

        var parentAdarsWiskey = new Product
        {
            Name = "Wiskey",
            Description =
                "Whisky or whiskey is a type of distilled alcoholic beverage made from fermented grain mash. Various grains are used for different varieties, including barley, corn, rye, and wheat.",
            ExpirationDate = DateTime.Now.AddDays(365),
            CreatedUserId = adar.UserId
        };

        var adarsWiskey = new MonitoredProduct
        {
            ProductId = parentAdarsWiskey.Id
        };

        var parentErensYogurt = new Product
        {
            Name = "Yogurt",
            Description =
                "Yogurt, yoghurt or yoghourt is a food produced by bacterial fermentation of milk. The bacteria used to make yogurt are known as yogurt cultures.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = eren.UserId
        };

        var erensYogurt = new MonitoredProduct
        {
            ProductId = parentErensYogurt.Id
        };

        var parentErensButter = new Product
        {
            Name = "Butter",
            Description =
                "Butter is a dairy product made from the fat and protein components of milk or cream. It is a semi-solid emulsion at room temperature, consisting of approximately 80% butterfat.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = eren.UserId
        };

        var erensButter = new MonitoredProduct
        {
            ProductId = parentErensButter.Id
        };

        var parentErensOrange = new Product
        {
            Name = "Orange",
            Description =
                "The orange is the fruit of various citrus species in the family Rutaceae; it primarily refers to Citrus × sinensis, which is also called sweet orange, to distinguish it from the related Citrus × aurantium, referred to as bitter orange.",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = eren.UserId
        };

        var erensOrange = new MonitoredProduct
        {
            ProductId = parentErensOrange.Id
        };

        var parentErensBanana = new Product
        {
            Name = "Banana",
            Description =
                "A banana is an elongated, edible fruit – botanically a berry – produced by several kinds of large herbaceous flowering plants in the genus Musa.",
            ExpirationDate = DateTime.Now.AddDays(3),
            CreatedUserId = eren.UserId
        };

        var erensBanana = new MonitoredProduct
        {
            ProductId = parentErensBanana.Id
        };

        var parentErensPasta = new Product
        {
            Name = "Pasta",
            Description =
                "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = eren.UserId
        };

        var erensPasta = new MonitoredProduct
        {
            ProductId = parentErensPasta.Id
        };

        var parentErensRice = new Product
        {
            Name = "Rice",
            Description =
                "Rice is the seed of the grass species Oryza sativa or less commonly Oryza glaberrima. As a cereal grain, it is the most widely consumed staple food for a large part of the world's human population, especially in Asia.",
            ExpirationDate = DateTime.Now.AddDays(20),
            CreatedUserId = eren.UserId
        };

        var erensRice = new MonitoredProduct
        {
            ProductId = parentErensRice.Id
        };

        var parentErensVodka = new Product
        {
            Name = "Volka",
            Description =
                "Vodka is a clear distilled alcoholic beverage with different varieties originating in Poland, Russia, and Sweden. It is composed primarily of water and ethanol, but sometimes with traces of impurities and flavorings.",
            ExpirationDate = DateTime.Now.AddDays(365),
            CreatedUserId = eren.UserId
        };

        var erensVodka = new MonitoredProduct
        {
            ProductId = parentErensVodka.Id
        };

        var parentErensTea = new Product
        {
            Name = "Tea",
            Description =
                "Tea is a beverage made by steeping processed or fresh tea leaves in water. The term also refers to the plant Camellia sinensis from which the tea is made.",
            ExpirationDate = DateTime.Now.AddDays(35),
            CreatedUserId = eren.UserId
        };

        var erensTea = new MonitoredProduct
        {
            ProductId = parentErensTea.Id
        };

        var parentErensChocolate = new Product
        {
            Name = "Chocolate",
            Description =
                "Chocolate is a food product made from roasted and ground cacao seeds that is made in the form of a liquid, paste, or in a block, which may also be used as a flavoring ingredient in other foods.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = eren.UserId
        };

        var erensChocolate = new MonitoredProduct
        {
            ProductId = parentErensChocolate.Id
        };

        var parentErensBeef = new Product
        {
            Name = "Beef",
            Description =
                "Beef is the culinary name for meat from cattle, particularly skeletal muscle. Humans have been eating beef since prehistoric times.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = eren.UserId
        };

        var erensBeef = new MonitoredProduct
        {
            ProductId = parentErensBeef.Id
        };

        var parentErensLamb = new Product
        {
            Name = "Lamb",
            Description =
                "Lamb, hogget, and mutton, generically sheep meat, are the meat of domestic sheep, Ovis aries.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = eren.UserId
        };

        var erensLamb = new MonitoredProduct
        {
            ProductId = parentErensLamb.Id
        };

        var parentBarissMilk = new Product
        {
            Name = "Milk",
            Description =
                "Milk is a nutrient-rich, white liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed human infants before they are able to digest solid food.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = baris.UserId
        };

        var barissMilk = new MonitoredProduct
        {
            ProductId = parentBarissMilk.Id
        };

        var parentBarissBeer = new Product
        {
            Name = "Beer",
            Description =
                "Beer is one of the oldest and most widely consumed alcoholic drinks in the world, and the third most popular drink overall after water and tea.",
            ExpirationDate = DateTime.Now.AddDays(14),
            CreatedUserId = baris.UserId
        };

        var barissBeer = new MonitoredProduct
        {
            ProductId = parentBarissBeer.Id
        };

        var parentBarissPork = new Product
        {
            Name = "Pork",
            Description =
                "Pork is the culinary name for the meat of a domestic pig. It is the most commonly consumed meat worldwide, with evidence of pig husbandry dating back to 5000 BC.",
            ExpirationDate = DateTime.Now.AddDays(21),
            CreatedUserId = baris.UserId
        };

        var barissPork = new MonitoredProduct
        {
            ProductId = parentBarissPork.Id
        };

        var parentBarissChicken = new Product
        {
            Name = "Chicken",
            Description =
                "The chicken is a type of domesticated fowl, a subspecies of the red junglefowl. It is one of the most common and widespread domestic animals, with a total population of more than 30 billion as of 2020.",
            ExpirationDate = DateTime.Now.AddDays(28),
            CreatedUserId = baris.UserId
        };

        var barissChicken = new MonitoredProduct
        {
            ProductId = parentBarissChicken.Id
        };

        var parentBarissPie = new Product
        {
            Name = "Pie",
            Description =
                "A pie is a baked dish that is usually made of a pastry dough casing that covers or completely contains a filling of various sweet or savory ingredients.",
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedUserId = baris.UserId
        };

        var barissPie = new MonitoredProduct
        {
            ProductId = parentBarissPie.Id
        };

        var monitoredProducts = new List<MonitoredProduct>
        {
            adarsMilk, adarsCheese, adarsChicken, adarsApple, adarsFish, adarsPasta, adarsRice, adarsTea, adarsCoffee,
            adarsWiskey, erensYogurt, erensButter, erensOrange, erensBanana, erensPasta, erensRice, erensVodka,
            erensTea,
            erensChocolate, erensBeef, erensLamb, barissMilk, barissBeer, barissPork, barissChicken, barissPie
        };

        var parentMonitoredProducts = new List<Product>
        {
            parentAdarsMilk, parentAdarsCheese, parentAdarsChicken, parentAdarsApple, parentAdarsFish, parentAdarsPasta,
            parentAdarsRice, parentAdarsTea, parentAdarsCoffee, parentAdarsWiskey, parentErensYogurt, parentErensButter,
            parentErensOrange, parentErensBanana, parentErensPasta, parentErensRice, parentErensVodka, parentErensTea,
            parentErensChocolate, parentErensBeef, parentErensLamb, parentBarissMilk, parentBarissBeer,
            parentBarissPork,
            parentBarissChicken, parentBarissPie
        };

        #endregion Monitored Product

        #region Store Product Category

        var categoryProducts = new List<CategoryProduct>
        {
            new() { ProductId = bimMilk.ProductId, CategoryId = diary.Id },
            new() { ProductId = bimCheese.ProductId, CategoryId = diary.Id },
            new() { ProductId = bimButter.ProductId, CategoryId = diary.Id },
            new() { ProductId = bimGarlic.ProductId, CategoryId = vegetable.Id },
            new() { ProductId = bimCarrot.ProductId, CategoryId = vegetable.Id },
            new() { ProductId = bimYogurt.ProductId, CategoryId = diary.Id },
            new() { ProductId = bimCoffee.ProductId, CategoryId = drink.Id },
            new() { ProductId = bimOil.ProductId, CategoryId = oil.Id },
            new() { ProductId = bimPasta.ProductId, CategoryId = grain.Id },
            new() { ProductId = bimOnion.ProductId, CategoryId = vegetable.Id },
            new() { ProductId = bimApple.ProductId, CategoryId = fruit.Id },
            new() { ProductId = bimBanana.ProductId, CategoryId = fruit.Id },
            new() { ProductId = migrosMilk.ProductId, CategoryId = diary.Id },
            new() { ProductId = migrosCheese.ProductId, CategoryId = diary.Id },
            new() { ProductId = migrosPork.ProductId, CategoryId = meat.Id },
            new() { ProductId = migrosChicken.ProductId, CategoryId = meat.Id },
            new() { ProductId = migrosApple.ProductId, CategoryId = fruit.Id },
            new() { ProductId = migrosKetchup.ProductId, CategoryId = sauce.Id },
            new() { ProductId = migrosRice.ProductId, CategoryId = grain.Id },
            new() { ProductId = migrosPasta.ProductId, CategoryId = grain.Id },
            new() { ProductId = carrefourMilk.ProductId, CategoryId = diary.Id },
            new() { ProductId = carrefourChocolate.ProductId, CategoryId = dessert.Id },
            new() { ProductId = carrefourPasta.ProductId, CategoryId = grain.Id },
            new() { ProductId = carrefourFish.ProductId, CategoryId = seafood.Id },
            new() { ProductId = carrefourRice.ProductId, CategoryId = grain.Id },
            new() { ProductId = carrefourEgg.ProductId, CategoryId = egg.Id },
            new() { ProductId = carrefourPalmoil.ProductId, CategoryId = oil.Id },
            new() { ProductId = carrefourTomato.ProductId, CategoryId = vegetable.Id },
            new() { ProductId = carrefourOrange.ProductId, CategoryId = fruit.Id },
            new() { ProductId = carrefourBanana.ProductId, CategoryId = fruit.Id },
            new() { ProductId = carrefourNoodle.ProductId, CategoryId = pasta.Id },
            new() { ProductId = carrefourCannedFish.ProductId, CategoryId = canned.Id },
            new() { ProductId = carrefourCannedSoup.ProductId, CategoryId = canned.Id },
            new() { ProductId = carrefourCannedFruit.ProductId, CategoryId = canned.Id },
            new() { ProductId = adarsMilk.ProductId, CategoryId = diary.Id },
            new() { ProductId = adarsCheese.ProductId, CategoryId = diary.Id },
            new() { ProductId = adarsChicken.ProductId, CategoryId = meat.Id },
            new() { ProductId = adarsApple.ProductId, CategoryId = fruit.Id },
            new() { ProductId = adarsFish.ProductId, CategoryId = seafood.Id },
            new() { ProductId = adarsPasta.ProductId, CategoryId = grain.Id },
            new() { ProductId = adarsRice.ProductId, CategoryId = grain.Id },
            new() { ProductId = adarsTea.ProductId, CategoryId = drink.Id },
            new() { ProductId = adarsCoffee.ProductId, CategoryId = drink.Id },
            new() { ProductId = adarsWiskey.ProductId, CategoryId = drink.Id },
            new() { ProductId = erensYogurt.ProductId, CategoryId = diary.Id },
            new() { ProductId = erensButter.ProductId, CategoryId = diary.Id },
            new() { ProductId = erensOrange.ProductId, CategoryId = fruit.Id },
            new() { ProductId = erensBanana.ProductId, CategoryId = fruit.Id },
            new() { ProductId = erensPasta.ProductId, CategoryId = grain.Id },
            new() { ProductId = erensRice.ProductId, CategoryId = grain.Id },
            new() { ProductId = erensVodka.ProductId, CategoryId = drink.Id },
            new() { ProductId = erensTea.ProductId, CategoryId = drink.Id },
            new() { ProductId = erensChocolate.ProductId, CategoryId = dessert.Id },
            new() { ProductId = erensBeef.ProductId, CategoryId = meat.Id },
            new() { ProductId = erensLamb.ProductId, CategoryId = meat.Id },
            new() { ProductId = barissMilk.ProductId, CategoryId = diary.Id },
            new() { ProductId = barissBeer.ProductId, CategoryId = drink.Id },
            new() { ProductId = barissPork.ProductId, CategoryId = meat.Id },
            new() { ProductId = barissChicken.ProductId, CategoryId = meat.Id },
            new() { ProductId = barissPie.ProductId, CategoryId = dessert.Id }
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

        context.User.AddRange(parentCustomers);
        context.User.AddRange(parentBusinesses);
        context.Customer.AddRange(customers);
        context.Business.AddRange(businesses);
        context.Category.AddRange(categories);
        context.Product.AddRange(parentMonitoredProducts);
        context.Product.AddRange(parentStoreProducts);
        context.StoreProduct.AddRange(storeProducts);
        context.MonitoredProduct.AddRange(monitoredProducts);
        context.CategoryProduct.AddRange(categoryProducts);
        context.Report.AddRange(reports);

        context.SaveChanges();
    }
}